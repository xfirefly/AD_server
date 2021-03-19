using Common;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TextSender;

namespace Net
{
    class ThreadPoolTcpSrvr
    {
        private Log log;
        private TcpListener listener;
        private Thread _tmain;
        public void dowork()
        {
            listener = new TcpListener(IPAddress.Any, Cmd.file_port);
            listener.Start();
            
            while (true)
            {
                while (!listener.Pending())
                {
                    Thread.Sleep(1000);
                }
                TcpClient client = listener.AcceptTcpClient();  //不能放在  HandleFileSender thread
                FileChannel newconnection = new FileChannel(client);
                ThreadPool.QueueUserWorkItem(new WaitCallback(newconnection.HandleFileSender));
            }
        }

        public ThreadPoolTcpSrvr()
        {
            log = new Log(GetType());
            log.t("File server start ...");

            _tmain = new Thread(new ThreadStart(dowork));
            _tmain.Start();
        }

    }

    class FileChannel
    {
        private static int activeConn = 0;
        private TcpClient _client;

        public static int BoxCount { get; set; }
        public static int Done { get; set; }


        public FileChannel(TcpClient client)
        {
            _client = client;
        }

        public void HandleFileSender(object state)
        {
            Console.WriteLine("File: {0} active connections", ++activeConn);
 
            //using (var incoming = listener.AcceptTcpClient())
            using (var networkStream = _client.GetStream())
            using (var fileIO = File.OpenRead(App.ZipPath))
            {
                try
                {
                    fileIO.CopyTo(networkStream);
                }
                catch (Exception)
                {
                    Console.WriteLine("CopyTo Exception");
                }
                // Read length - Int64
                // Send Length (Int64)
                //byte[] len = BitConverter.GetBytes(fileIO.Length);
                //networkStream.Write(len, 0, len.Length);
                //Console.WriteLine("filesize: " + fileIO.Length + " -- " + len.Length);

                //var buffer = new byte[1024 * 16];
                //int count;
                //while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                //    networkStream.Write(buffer, 0, count);
            }

            Console.WriteLine("File: {0} active connections", --activeConn);
             
            App.Instance.progRelease.updateProgress(++Done);
            _client.Close();
        }
     

        //public void HandleConnection(object state)
        //{
        //    int recv;
        //    byte[] data = new byte[1024];
        //    TcpClient client = listener.AcceptTcpClient();
        //    NetworkStream ns = client.GetStream();
        //    activeConn++;
        //    Console.WriteLine("New client accepted: {0} active connections",
        //    activeConn);

        //    string welcome = "Welcome to my test server";
        //    data = Encoding.ASCII.GetBytes(welcome);
        //    ns.Write(data, 0, data.Length);
        //    while (true)
        //    {
        //        data = new byte[1024];
        //        recv = ns.Read(data, 0, data.Length);
        //        if (recv == 0)
        //            break;

        //        ns.Write(data, 0, recv);
        //    }
        //    ns.Close();
        //    client.Close();
        //    activeConn--;
        //    Console.WriteLine("Client disconnected: {0} active connections",
        //    activeConn);
        //}


    }
}