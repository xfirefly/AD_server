using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TextSender;

namespace Net
{
    class ThreadedTcpSrvr
    {
        private Log log;
        private TcpListener listener;
        private Thread tmain;
        private Thread tmonitor;

        private List<CommandChannel> chanList;

        private void accepter()
        {
            listener = new TcpListener(IPAddress.Any, Cmd.cmd_port);
            listener.Start();

            while (true)
            {
                while (!listener.Pending())
                {
                    Thread.Sleep(1000);
                }

                TcpClient client = listener.AcceptTcpClient();
                CommandChannel newconnection = new CommandChannel(client);
                chanList.Add(newconnection);
            }
        }

        private void monitor()
        {
            while (App.Instance.Alive)
            {
                Thread.Sleep(5000);
                for (int i = 0; i < chanList.Count; i++)
                {
                    if (chanList[i].Exit || chanList[i].ClientExit)
                    {
                        App.Instance.boxDisconnected(chanList[i].box);
                        chanList[i].close();
                        chanList.RemoveAt(i);
                    }
                }
            }
        }

        public ThreadedTcpSrvr()
        {
            log = new Log(GetType());
            log.t("Command server start ");

            chanList = new List<CommandChannel>();
            tmain = new Thread(new ThreadStart(accepter));
            tmain.Start();

            tmonitor = new Thread(new ThreadStart(monitor));
            tmonitor.Start();
        }


        public void destory()
        {
            tmain.Abort();
            foreach (CommandChannel t in chanList)
            {
                if (!t.Exit)
                {
                    t.close();
                }
            }
        }
    }

    public class CommandChannel
    {
        private Log log;
        //private TcpListener _listener;
        private static int connections = 0;
        private TcpClient client;
        private Thread thCommand;
        public Box box;   //本command channel 所属的box

        private long lastTs = Util.TimeStamp;

        public bool Exit { get; set; }
        public bool ClientExit
        {
            get
            {
                if (Util.TimeStamp - lastTs > 30)
                {
                    return true;
                }
                return false;
            }
        }

        public CommandChannel(TcpClient client)
        {
            log = new Log(GetType());
            this.client = client;
            box = new Box { CmdChan = this };

            thCommand = new Thread(new ThreadStart(HandleConnection));
            thCommand.Start();
        }

        public void close()
        {
            if (Exit)
            {
                return;
            }

            Exit = true;
            try
            {
                byte[] b = Cmd.create(Command.server_close, " ");
                client.GetStream().Write(b, 0, b.Length); ;
                thCommand.Abort();
            }
            catch (Exception)
            {
                log.e("BoxConnThread err");
            }
        }

        public void send(byte[] buffer)
        {
            try
            {
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                App.MsgBoxE("发送命令出错" );
                log.e("send " + ex.Message);
            }
        }

        private void HandleConnection()
        {
            int recv;
            //byte[] data = new byte[1024];
            NetworkStream ns = client.GetStream();
            connections++;
            log.w("Command: {0} active connections", connections);
            
            //string welcome = "Welcome to my test server";
            //data = Encoding.ASCII.GetBytes(welcome);
            //ns.Write(data, 0, data.Length);
            StreamReader reader = new StreamReader(ns);
            try
            {
                while (!Exit)
                {
                    //data = new byte[1024];
                    //recv = ns.Read(data, 0, data.Length);
                    //if (recv == 0)
                    //   break;
                    //ns.Write(data, 0, recv);
#if true
                    var message = reader.ReadLine();
                    lastTs = Util.TimeStamp;
                    if (message == null)
                    {
                        break;
                    }
                    process(message);
#endif                    
                }

            }
            catch (Exception ex)
            {
                // Clean-up code can go here.
                // If there is no Finally clause, ThreadAbortException is
                // re-thrown by the system at the end of the Catch clause. 
                //Console.WriteLine("errin " + ex.Message);
                log.d("Exception " , ex);
            }
            finally
            {
                ns.Close();
                client.Close();
                connections--;
                Exit = true;
                log.w("Command: active remain => " + connections);
            }
        }

        private  void process(string message)
        {
            string[] m = Regex.Split(message.Trim(), "/:");
            log.w("recv:  " + m[0]);
            switch ((Command)Enum.Parse(typeof(Command), m[0].Trim().Substring(1)) ) // (m[0].Substring(1))
            {
                case Command.heartbeat:

                    break;
                case Command.connect:
                    IPAddress address = ((IPEndPoint)client.Client.RemoteEndPoint).Address;

                    box.ip = address.ToString();
                    box.name = m[1];
                    box.id = m[2];
                    App.Instance.boxConnected(box);
                    break;

                case Command.screen_image:
                    var bytes = Convert.FromBase64String(m[1]);
                    using (var imageFile = new FileStream("screen.jpg", FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                        imageFile.Close();
                    }
                    //ProcessStartInfo psi = new ProcessStartInfo("screen.jpg");
                    //psi.UseShellExecute = true;
                    //Process.Start(psi);

                    App.Instance.boxCtrl.showImage("screen.jpg");
                    break;

                case Command.curr_volume:
                    var vol = int.Parse(m[1]);
  
                    //防止block this function
                    new Thread(() => App.Instance.boxCtrl.showfrmSetVolume(vol)).Start() ;
                    break;

                case Command.curr_time:
                    //防止block this function
                    new Thread(() => App.Instance.boxCtrl.showfrmSetTime(m[1]) ).Start();
                    break;


                default:
                    break;
            }
        }
    }
}