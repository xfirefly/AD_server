using Common;
using Newtonsoft.Json;
using ProgressTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextSender;

namespace Common
{
    public class AdZip
    {
        private Log log;
        private Ionic.Zip.ZipFile zip;

        // =========== Singleton ==============
        private static readonly Lazy<AdZip> lazy = new Lazy<AdZip>(() => new AdZip());
        public static AdZip Instance { get { return lazy.Value; } }

        private AdZip()
        {
            log = new Log(GetType());
        }
        // =========== Singleton ==============

        public void init()
        {
            zip = new Ionic.Zip.ZipFile();
            zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
            zip.StatusMessageTextWriter = System.Console.Out;
        }

        public void add(string path)
        {
            try
            {
                zip.AddFile(path, "");
            }
            catch (Exception ex)
            {
                log.e("zip error " + ex.Message);
            }  
        }

        public void save(string zipfile)
        {
            //string zipfile = App.ZipPath;
            File.Delete(zipfile);
            zip.Save(zipfile);

            zip.Dispose();
        }

        private string CreateAdZip()
        {
            string zipfile = App.ZipPath;
            File.Delete(zipfile);
            using (var zip = new Ionic.Zip.ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                //zip.SaveProgress += SaveProgress;

                zip.StatusMessageTextWriter = System.Console.Out;
                zip.AddDirectory(App.TmpDir, "");
                zip.Save(zipfile);
            }

            return zipfile;
        }
    }
}