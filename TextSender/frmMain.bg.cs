using Common;
using ControlManager;
using Ctrl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmMain : UserControl
    {
        protected void BgChanged(object sender, frmAddFile.FileChangedEventArgs e)
        {
            AdScene.Instance.adProgram.backImage.Clear();
            string prefix = "bg_img_";
            //var files = Directory.EnumerateFiles(tmp, "*.*", SearchOption.TopDirectoryOnly)
            //    .Where(s => s.Contains("bg_")  );
            //foreach (string f in files)
            //{
            //}

            string[] filePaths = Directory.GetFiles(App.TmpDir);
            foreach (string f in filePaths)
            {
                if (f.Contains(prefix))
                {
                    File.Delete(f);
                }
            }

            int i = 1;
            AdScene.Instance.adProgram.backImageIntval = e.intval;
            foreach (var item in e.fileList)
            {
                string img = prefix + i + Path.GetExtension(item).ToLower();
                AdScene.Instance.adProgram.backImage.Add(img);

                //var source = Path.Combine(Environment.CurrentDirectory, fileName);
                var destination = Path.Combine(App.TmpDir, img);
                File.Copy(item, destination, true);
                log.i(item + " => " + destination);
                i++;
            }

            if (e.fileList.Count > 0)
            {
                Profile.setBackImage(e.fileList);
            }
            Profile.setBackImageIntval(e.intval);
        }
    }
}
