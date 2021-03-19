using Common;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using TextSender;

namespace Net
{
    internal class BroadcastSrvr
    {
        private Log log;
        private const int send_intval = 2550;
        private Thread tBroadcast;

        public BroadcastSrvr()
        {
            log = new Log(GetType());
            log.t("Broadcast server start ");

            tBroadcast = new Thread(new ThreadStart(dowork));
            tBroadcast.Start();
        }

        public void dowork()
        {
            string ip = NetUtils.GetLocalIPAddress();
            log.t("GetLocalIPAddress " + ip);
            byte[] msg = Cmd.create(Command.server_ip, ip);

            while (App.Instance.Alive)
            {
                try
                {
                    Thread.Sleep(send_intval);
                    broadcast(msg);
                }
                catch (ThreadInterruptedException )
                {
                    log.t("broadcast Interrupted. exit ");
                    return;
                }
            }
        }

        public void broadcast(byte[] bytes)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.EnableBroadcast = true;
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast, Cmd.broadcast_port);
            s.SendTo(bytes, ep);  
            s.Close();

#if false
            foreach (var ni in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()) {
                // discard because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;

                foreach (var ua in ni.GetIPProperties().UnicastAddresses) { 
                    log.t(ua.Address.ToString());
                }
            }
#endif

            //UdpClient client = new UdpClient();
            //IPEndPoint ip = new IPEndPoint(ua.Address, Cmd.broadcast_port);
            //client.Send(bytes, bytes.Length, ip);
            //client.Close();
            //log.t("send broadcast   ");
        }
 

        public void stop()
        {
            tBroadcast.Interrupt();
            tBroadcast.Join();
        }
    }
}