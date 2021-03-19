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
    public class AdScene
    {
        private Log log;

        public const string PicPrefix = "pic_";
        public const string VideoPrefix = "advid_";
        public const string Timing_0000 = "00:00";
        public const string AD_CONFIG_JSON = "config.json";

        // =========== Singleton ==============
        private static readonly Lazy<AdScene> lazy = new Lazy<AdScene>(() => new AdScene());
        public static AdScene Instance { get { return lazy.Value; } }

        private AdScene()
        {
            log = new Log(GetType());
        }
        // =========== Singleton ==============

        public AdProgram adProgram = new AdProgram()
        {
            ver = 1,
            sceneIntval = -1,
            backImageIntval = -1,
        };


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

        // 检查是否设置正确: 底图, 字幕, 定时, 选中场景也要检查
        private bool checkDataValid()
        {

            return true;
        }

        public bool mergeCreateZip()
        {
            if (!checkDataValid())
            {
                log.w("Data not Valid: "   );
                return false;
            }

            showMergeCreateZip();

            return true;
        }

        //每个场景的定时时间
        private List<string> mSelectedSceneTiming= new List<string>();
        
        private List<string> mSelectedSceneList = new List<string>();
        // merge 场景之前必须调用, 设置选择的场景文件夹名称
        public void selectedScene(List<string> scenes, List<string> timing )
        {
            mSelectedSceneList.Clear();
            mSelectedSceneList.AddRange(scenes);
            foreach (var item in mSelectedSceneList)
            {
                log.w("add scene: " + item);
            }

            mSelectedSceneTiming.Clear();
            mSelectedSceneTiming.AddRange(timing);
            foreach (var item in mSelectedSceneTiming)
            {
                log.w("sceneTimingList " + item);
            }
        }

        private void doMergeCreateZip(MarqueeProgress sender, DoWorkEventArgs e)
        {
            log.w("mSelectedSceneList.Count: " + mSelectedSceneList.Count );
            if (mSelectedSceneList.Count > 0)
            {
                mergeScene(mSelectedSceneList, mSelectedSceneTiming );
                //string zipfile = CreateAdZip();
            }
        }

        public void showMergeCreateZip()
        {
            MarqueeProgress form = new MarqueeProgress();
            form.Text = Resource1.save;
            //pass the check box state as an argument for our background worker
            //form.Argument = arg;  
            form.DoWork += new MarqueeProgress.DoWorkEventHandler(doMergeCreateZip);
            DialogResult result = form.ShowDialog();
        }

        private void removeTmpImage()
        {
            string[] filePaths = Directory.GetFiles(App.TmpDir);
            foreach (string f in filePaths)
            {
                if (f.Contains(PicPrefix))
                {
                    File.Delete(f);
                }
            }
        }

        private void removeTmpVideo()
        {
            string[] filePaths = Directory.GetFiles(App.TmpDir);
            foreach (string f in filePaths)
            {
                if (f.Contains(VideoPrefix))
                {
                    File.Delete(f);
                }
            }
        }

        /// <summary>
        ///  把场景数据拷贝到 tmp 目录, 合并场景json , 生成ad json文件
        /// </summary>
        /// <returns></returns>
        private bool mergeScene(List<string> sceneList, List<string> timingList )
        {
            string[] files = Directory.GetFiles(App.TmpDir);
            foreach (string fn in files)
            {
                File.Delete( fn);
            }

            Toast("正在创建文件, 请稍候!", 5);
            adProgram.scene.Clear();
            removeTmpImage();
            removeTmpVideo();
            //foreach (var item in lbScene.CheckedItems)
            //foreach (var item in sceneList)

            AdZip.Instance.init();
            for (int i = 0; i < sceneList.Count; i++)
            {
                string sceneDir = Path.Combine(App.SceneDir, sceneList[i] );
                log.w("mergeScene " + sceneDir);
                string jsonStr = TextFile.fileToText(Path.Combine(sceneDir, AD_CONFIG_JSON));

                var scene = JsonConvert.DeserializeObject<Scene>(BluCodec.Decode(jsonStr));
                if (timingList.Count > 0)
                {
                    scene.timing = timingList[i];
                }
                adProgram.scene.Add(scene);

                var filteredFiles = Directory
                    .GetFiles(sceneDir, "*.*")
                    .Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png") || file.ToLower().EndsWith("bmp")
                        || Path.GetFileName(file).StartsWith(VideoPrefix))
                    .ToList();
                foreach (var f in filteredFiles)
                {
                    string tmp = Path.Combine(App.TmpDir, Path.GetFileName(f));
                    log.t(f + " => " + tmp);
                    //File.Copy(f, tmp, true);
                    AdZip.Instance.add(f);
                }
            }
            
            string output = JsonConvert.SerializeObject(adProgram, Formatting.Indented);
            log.t(output);
            output = BluCodec.Encode(output);
            string json = Path.Combine(App.TmpDir, AD_CONFIG_JSON);
            using (StreamWriter sw = new StreamWriter(File.Open(json, FileMode.Create)))
            {
                sw.WriteLine(output);
            }
            AdZip.Instance.add(json);
            AdZip.Instance.save(App.ZipPath);

            return true;
        }

        

        public void writeToUSB(string zipfile)
        {
            var drives = DriveInfo.GetDrives()
                .Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);
            if (drives.Count() == 0)
            {
                MessageBox.Show(Resource1.no_usb, Resource1.save, MessageBoxButtons.OK, MessageBoxIcon.Information);
                string des = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.GetFileName(zipfile));
                //File.Copy(zipfile, des, true);
                Util.copy(zipfile, des);
            }
            else
            {
                foreach (var item in drives)
                {
                    string des = Path.Combine(item.RootDirectory.FullName, Path.GetFileName(zipfile));
                    MessageBox.Show("保存到 " + des, "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //File.Copy(zipfile, des, true);
                    Util.copy(zipfile, des);
                }
            }
        }

        private void Toast(string msg, int duration = 3, string title = "提示")
        {
            log.t(msg);
        }

    }
}