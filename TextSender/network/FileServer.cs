using Common;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Net
{
    //发送广告文件用
    public class FileServer
    {

        private string _ip;
        private int _port;
        private string _file;
        private ManualResetEvent _doneEvent;
        private bool _success = false;
        private Log log;

        public bool IsSuccess { get { return _success; } }

        // Constructor.
        public FileServer(int n, ManualResetEvent doneEvent, string ip, int port, string file)
        {
            log = new Log(this.GetType());
            _doneEvent = doneEvent;
            _ip = ip;
            _port = port;
            _file = file;
        }

        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(Object threadContext)
        {
            try
            {
                int threadIndex = (int)threadContext;
                Console.WriteLine("thread {0} started...", threadIndex);
                using (var fileIO = File.OpenRead(_file))
                using (var client = new TcpClient(_ip, _port).GetStream())
                {
                    // Send Length (Int64)
                    byte[] len = BitConverter.GetBytes(fileIO.Length);
                    client.Write(len, 0, len.Length);
                    log.i("filesize: " + fileIO.Length + " -- " + len.Length);

                    var buffer = new byte[1024 * 16];
                    int count;
                    while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                        client.Write(buffer, 0, count);
                }
                Console.WriteLine("thread {0} result calculated...", threadIndex);
                _success = true;
            }
            catch (Exception e)
            {
                log.d("ex", e);
            }
            finally
            {
                _doneEvent.Set();
            }
        }

    }

    public class FileServerExample
    {
        public static void reader()
        {
            const int boxCount = 6;

            // One event is used for each Fibonacci object.
            ManualResetEvent[] doneEvents = new ManualResetEvent[boxCount];
            FileServer[] fsArray = new FileServer[boxCount];

            // Configure and start threads using ThreadPool.
            Console.WriteLine("launching {0} tasks...", boxCount);
            for (int i = 0; i < boxCount; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                //发送到各个box
                FileServer f = new FileServer(0, doneEvents[i], "192.168.1.218", 8283, @"f:\[4K123]MV 20140225.mp4");
                fsArray[i] = f;
                ThreadPool.QueueUserWorkItem(f.ThreadPoolCallback, i);
            }

            // Wait for all threads in pool to calculate.
            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("All calculations are complete.");

            // Display the results.
            for (int i = 0; i < boxCount; i++)
            {
                FileServer f = fsArray[i];
                //Console.WriteLine("Fibonacci({0}) = {1}", f.N, f.FibOfN);
            }
        }

        public static void writer()
        {
            Int64 bytesReceived = 0;
            int count;
            var buffer = new byte[1024 * 8];
 
            var listener = new TcpListener(IPAddress.Parse("192.168.1.218"), 8283);
            listener.Start();

            using (var incoming = listener.AcceptTcpClient())
            using (var networkStream = incoming.GetStream())
            using (var fileIO = File.OpenWrite(@"f:\ttt.bin"))
            {
                //networkStream.CopyTo(fileStream);
                // Read length - Int64
                networkStream.Read(buffer, 0, 8);
                Int64 numberOfBytes = BitConverter.ToInt64(buffer, 0);

                while (bytesReceived < numberOfBytes && (count = networkStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileIO.Write(buffer, 0, count);
                    bytesReceived += count;
                }
            }

            listener.Stop();
        }
    }
}