using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    internal class NetUtils
    {
        private static Log log = new Log("NetUtils");

        private UdpClient _listener = null;

        public NetUtils()
        {
        }

        public static void broadcast(byte[] msg)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.EnableBroadcast = true;
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast, Cmd.broadcast_port);

            //IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            s.SendTo(msg, ep);
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public void startRecv()
        {
            _listener = new UdpClient(Cmd.broadcast_port);
            _listener.BeginReceive(new AsyncCallback(recvMsg), null);
        }

        public void stop()
        {
            if (_listener != null)
                _listener.Close();
        }

        private void recvMsg(IAsyncResult res)
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, Cmd.broadcast_port);

            try
            {
                // byte[] bytes = listener.Receive(ref remote);
                byte[] bytes = _listener.EndReceive(res, ref remote);

                log.i(String.Format("recv broadcast from {0} :\n {1}\n",
                    remote.ToString(),
                    Encoding.ASCII.GetString(bytes, 0, bytes.Length)));

                // get next packet
                _listener.BeginReceive(recvMsg, null);
            }
            catch (Exception ex)
            {
                log.e(ex.ToString());
            }
        }
    }
}