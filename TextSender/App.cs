using Common;
using Net;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace TextSender
{
    public class App
    {
        public const int VERSION = 25;  //服务器json ver 配置也是整型

        private const string CHIP = "dfsdfsd";

        public const bool HDMI = false; // 带 hdmi in
        public const bool VIDEO = true; // 支持视频播放
 
        /// //////////////////////////////////////////////////////////////////////
       
        private static volatile App instance;
        private static object syncRoot = new Object();
        private BroadcastSrvr broadcastServer;
        private ThreadedTcpSrvr cmdServer;
        private ThreadPoolTcpSrvr fileServer;

        private Log log;

        private App()
        {
            log = new Log(GetType());
        }

        /// <summary>
        /// app base directory
        /// </summary>
        public static string Dir
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static App Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new App();
                    }
                }
                return instance;
            }
        }

        public static string BgDir
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template");
            }
        }

        /// <summary>
        /// scene directory, store scene zip
        /// </summary>
        public static string SceneDir
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scene");
            }
        }


        /// <summary>
        /// scrshot directory, store screen shot image
        /// </summary>
        public static string ScrshotDir
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scrshot");
            }
        }

        /// <summary>
        /// tmp directory, store tmp scene
        /// </summary>
        public static string TmpDir
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");
            }
        }

        /// <summary>
        /// ad zip file path
        /// </summary>
        public static string ZipPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export_subtitles_tk.zip");
            }
        }

        public bool Alive { get; set; }

        public static void MsgBox(string msg)
        {
            MessageBox.Show(msg, Resource1.notice, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult MsgBox2(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static void MsgBoxE(string msg)
        {
            MessageBox.Show(msg, Resource1.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void restart()
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C ping 127.0.0.1 -n 3 && \"" + Application.ExecutablePath + "\"";
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            Application.Exit();
        }

        public void init()
        {
            Alive = true;
            Directory.CreateDirectory(TmpDir);
            Directory.CreateDirectory(SceneDir);

            // Thread t = new Thread(new ThreadStart(ThreadedTcpSrvr.main));
            //  t.Start();
            startBroadcast();
            cmdServer = new ThreadedTcpSrvr();
            fileServer = new ThreadPoolTcpSrvr();

            new Thread(new ThreadStart(update_cfg.updater)).Start();

            test();
        }

        class update_cfg
        {
            public int ver;
            public string url;
            public bool force;
            public string add;

            public static void updater()
            {
                Thread.Sleep(10000);
                string cfgurl = "http://file.blueberry-tek.com/apk/ad/update_cfg.json";

                try
                {
                    using (WebClient myWebClient = new WebClient())
                    {
                        var cfg = JsonConvert.DeserializeObject<update_cfg>(myWebClient.DownloadString(cfgurl));
                        Console.WriteLine("update_cfg " + VERSION + " => " + cfg.ver);
                        File.Delete("new_update_file.exe");
                        if (cfg.ver > VERSION)
                        {
                            string exeurl = cfg.url + "?ts=" + Util.TimeStamp;
                            myWebClient.DownloadFile(exeurl, "new_update_file.exe");

                            if (App.MsgBox2("已下载新版软件, 是否现在开始升级? ", Resource1.notice) == DialogResult.OK)
                            {
                                Process p = Process.Start("new_update_file.exe");
                                Application.Exit();
                            }
                            else if (cfg.force)
                            {
                                Application.Exit();
                            }
                        }
                    }

                    var response = Post("http://api.blueberry-tek.com/blumedia/fireplayer.php", new NameValueCollection() {
                        { "mac", "11aa11aa11aa" },
                        { "chip", CHIP }
                    });
                    string sret = System.Text.Encoding.ASCII.GetString(response);
                    if (sret.Contains("obacdf8e4o813bo1")  )
                    { 
                        restart();  // apk直接退出
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }

            public static byte[] Post(string uri, NameValueCollection pairs)
            {
                byte[] response = null;
                using (WebClient client = new WebClient())
                {
                    response = client.UploadValues(uri, pairs);
                }
                return response;
            }
        }

        public void unInit()
        {
            log.e("unInit 1");
            Alive = false;
            Profile.Save();
            cmdServer.destory();
            log.e("unInit 2");
            Log.Free();
            Thread.Sleep(2000);
            Process.GetCurrentProcess().Kill();
        }

        public void stopBroadcast()
        {
            if (broadcastServer != null)
            {
                broadcastServer.stop();
            }

            log.t("broadcastServer stop");
        }

        public void startBroadcast()
        {
            if (Profile.networkMode() == Profile.NetworkMode.LAN)
            {
                stopBroadcast();
                broadcastServer = new BroadcastSrvr();
            }
        }


        private void test()
        {

        }

        public ProgramRelease progRelease;
        public BoxCtrl boxCtrl;
        public void boxConnected(Box box)
        {
            progRelease.addBox(box);
            boxCtrl.addBox(box);
        }

        public void boxDisconnected(Box box)
        {
            progRelease.removeBox(box);
            boxCtrl.removeBox(box);
        }


    }
}