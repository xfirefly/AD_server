using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    internal class Profile
    {
        private static IniFile ini;

        static Profile()
        {
            ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "Cfg.ini");
        }

        public static List<string> getBackImage()
        {
            List<string> list = new List<string>();
            string img = ini.ReadString("BackImage", "imglist", null);
            if (img != null)
            {
                list = img.Split('|').ToList();
            }

            return list;
        }

        public static string getBackImageIntval()
        {
            return ini.ReadString("BackImage", "intval", "-1");
        }

        public static void LoadProfile()
        {
            //G_BAUDRATE = _ini.ReadString("CONFIG", "BaudRate", "4800");    //读数据，下同
            //G_DATABITS = _ini.ReadString("CONFIG", "DataBits", "8");
            //G_STOP = _ini.ReadString("CONFIG", "StopBits", "1");
            //G_PARITY = _ini.ReadString("CONFIG", "Parity", "NONE");
        }

        public static void Save()
        {
            ini.UpdateFile();
        }

        public static void SaveProfile()
        {
        }

        public static void setBackImage(List<string> list)
        {
            ini.WriteString("BackImage", "imglist", list.Aggregate((a, b) => a + "|" + b));
        }

        public static void setBackImageIntval(int i)
        {
            ini.WriteString("BackImage", "intval", i.ToString());
        }

        public static void setSceneName(List<string> list)
        {
            ini.WriteString("SceneName", "name", list.Aggregate((a, b) => a + "|" + b));
        }

        public static List<string> getSceneName()
        {
            List<string> list = new List<string>();
            string img = ini.ReadString("SceneName", "name", null);
            if (!string.IsNullOrWhiteSpace(img))
            {
                list = img.Split('|').ToList();
            }

            return list;
        }

        public static string getLang()
        {
            return ini.ReadString("sys", "lang", "zh");
        }

        public static void setLang(string lang)
        {
            ini.WriteString("sys", "lang", lang);
        }


        public enum NetworkMode
        {
            LAN,
            WAN,
            NULL
        }
        public static NetworkMode networkMode(NetworkMode mode = NetworkMode.NULL)
        {
            if (mode != NetworkMode.NULL)
            {
                ini.WriteString("sys", "networkmode", mode.ToString());
                Save();
            }

            string ret = ini.ReadString("sys", "networkmode", NetworkMode.LAN.ToString());
            return (NetworkMode)Enum.Parse(typeof(NetworkMode), ret);
        }

        public enum BoxResolution
        {
            R1280_720,
            R1920_1080,
            NULL
        }
        public static BoxResolution boxResolution(BoxResolution mode = BoxResolution.NULL)
        {
            if (mode != BoxResolution.NULL)
            {
                ini.WriteString("sys", "BoxResolution", mode.ToString());
                Save();
            }

            string ret = ini.ReadString("sys", "BoxResolution", BoxResolution.R1920_1080.ToString());
            return (BoxResolution)Enum.Parse(typeof(BoxResolution), ret);
        }
    }
}