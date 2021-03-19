using Common;
using ControlManager;
using Ctrl;
using Gdu.ExtendedPaint;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ProgressTest;
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
using ToastNotifications;

namespace TextSender
{
    public partial class frmMain : UserControl
    {
        private const string PicPrefix = AdScene.PicPrefix; // "pic_";
        public const string VideoPrefix = AdScene.VideoPrefix; //"advid_";

        private bool sceneEditing;


        /// <summary>
        ///  文件夹名称就是场景名称, json 保存 video, subtitle, picture 位置等信息,
        ///  图片视频等资源拷贝进来即可
        /// </summary>
        //private void doSaveScene(string sceneName)
        void doSaveScene(MarqueeProgress sender, DoWorkEventArgs e)
        {
            Toast("正在保存场景文件, 请稍候!", 3);

            string sceneName = (string)e.Argument;
            string dir = Path.Combine(App.SceneDir, sceneName);
            Scene scene = new Scene()
            {
                timing = AdScene.Timing_0000,
                name = sceneName, // + " (定时 " + SceneTiming.Replace(':', '.') + ")",
            };

            for (int i = 0; i < CanvasMain.Kernel.getShapeList().Count; i++)
            {
                RectShape rect = CanvasMain.Kernel.getShapeList()[i] as RectShape;
                log.w("saveScene: " + rect.ItemType);
                scene.layers.Add(rect.ItemType);
                switch (rect.ItemType)
                {
                    case AdItemType.Video:
                        if (!App.VIDEO)
                        {
                            scene.video.Add(rect.Prop as Video);
                        }
                        else
                        {
                            Video video = rect.Prop as Video;
                            if (video.filelist.Count == 0 && App.HDMI == false)
                            {
                                App.MsgBoxE("没有设置视频元素的视频源!");
                                Directory.Delete(dir, true);
                                e.Result = false;
                                return;
                            }
                            scene.video.Add(createVid(video, dir));
                        }
                        break;

                    case AdItemType.Picture:
                        Picture picture = rect.Prop as Picture;
                        if (picture.filelist.Count == 0)
                        {
                            App.MsgBoxE("没有设置图片元素的图片源!");
                            Directory.Delete(dir, true);
                            e.Result = false;
                            return;
                        }
                        scene.picture.Add(createPic(picture, dir));
                        break;

                    case AdItemType.Subtitle:
                        scene.subtitle.Add(rect.Prop as Subtitle);
                        break;

                    case AdItemType.Select:
                        break;

                    default:
                        break;
                }
            }

            string json = JsonConvert.SerializeObject(scene, Formatting.Indented);
            log.t(json);
            json = BluCodec.Encode(json);
            //Console.WriteLine(json);
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(dir, AD_CONFIG_JSON), FileMode.Create)))
            {
                sw.WriteLine(json);
            }

            e.Result = true;
            //lbScene.Items.Add(scene.name);
            //endSceneEditing();
            //App.MsgBox("保存成功!");
        }

        /*
        void form_DoWork(ProgressForm sender, DoWorkEventArgs e)
        {
            bool throwException = (bool)e.Argument;

            for (int i = 0; i < 100; i++)
            {
                if (throwException && i == 50)
                    throw new Exception("Throwing exception!");

                System.Threading.Thread.Sleep(50);
                sender.SetProgress(i, "Step " + i.ToString() + " / 100...");
                if (sender.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        */

        void saveSceneEnd(MarqueeProgress sender, RunWorkerCompletedEventArgs e)
        {
            /*           if ((bool)e.Result == true)
                       {
                           lbScene.Items.Add(txtSceneName.Text);
                           endSceneEditing();

                       } */
            string sceneName = txtSceneName.Text;
            if ((bool)e.Result == true)
            {
                cmCloseScene_Click(null, null);

                string destination = Path.Combine(App.SceneDir, sceneName);
                Util.DirDelete(destination);
                string source = Path.Combine(App.SceneDir, "_temp");
                Directory.Move(source, destination);

                if (lbScene.Items.Contains(sceneName) == false)
                    lbScene.Items.Add(sceneName);

                endSceneEditing();
                //App.MsgBox("保存成功!");
            }
        }

        private void showSaveScene(string arg)
        {
            MarqueeProgress form = new MarqueeProgress();
            form.Text = Resource1.save;
            //pass the check box state as an argument for our background worker
            form.Argument = arg; // checkBoxThrowException.Checked;
            form.DoWork += new MarqueeProgress.DoWorkEventHandler(doSaveScene);
            form.WorkCompleted += new MarqueeProgress.WorkCompletedEventHandler(saveSceneEnd);

            //check how the background worker finished
            DialogResult result = form.ShowDialog();
            //if (result == DialogResult.Cancel)
            //    MessageBox.Show("Operation has been cancelled");
            //if (result == DialogResult.Abort)
            //    MessageBox.Show("Exception:" + Environment.NewLine + form.Result.Error.Message);
        }



        private void saveScene()
        {
            string sceneNam = "_temp";
            string dir = Path.Combine(App.SceneDir, sceneNam);
            Util.DirDelete(dir);
            try
            {
                Directory.CreateDirectory(dir);
            }
            catch (Exception)
            {
                App.MsgBoxE("名字不能有特殊字符!");
                return;
            }


            showSaveScene(sceneNam);
        }

        private Video createVid(Video video, string dir)
        {
            Video vid = video.Clone() as Video;

            List<string> tmp = new List<string>();
            foreach (var item in vid.filelist)
            {
                string vfile = VideoPrefix + Util.md5(item) + Path.GetExtension(item).ToLower();
                tmp.Add(vfile);
                File.Copy(item, Path.Combine(dir, vfile), true);

                log.i(item + " => " + Path.Combine(dir, vfile));
            }

            vid.filelist.Clear();       //绝对路径
            vid.filelist.AddRange(tmp);  // 重命名的 

            return vid;
        }

        private Picture createPic(Picture picture, string dir)
        {
            Picture pic = picture.Clone() as Picture;

            List<string> tmp = new List<string>();
            foreach (var item in pic.filelist)
            {
                string img = PicPrefix + Util.md5(item) + Path.GetExtension(item).ToLower();
                tmp.Add(img);
                File.Copy(item, Path.Combine(dir, img), true);

                log.i(item + " => " + Path.Combine(dir, img));
            }

            pic.filelist.Clear();       //绝对路径
            pic.filelist.AddRange(tmp);  // 重命名的图片

            return pic;
        }

        private void endSceneEditing()
        {
            sceneEditing = false;
            btSceneNewSave.Text = "新建场景";
            txtSceneName.Text = "";
            txtSceneName.Enabled = false;
            gbDrawTool.Enabled = false;
        }



        //创建发送给box 的文件
        private void createAD()
        {
        }

        private bool createScene(string sName)
        {
            if (!sceneEditing)
            {
                //SceneTiming = Timing_0000;  //恢复默认定时值

                txtSceneName.Text = sName;
                sceneEditing = true;
                btSceneNewSave.Text = "保存场景";
                txtSceneName.Enabled = true;
                gbDrawTool.Enabled = true;
                // 清除原有场景
                CanvasMain.Kernel.DeleteAllShapse();

                // Toast("请使用工具栏创建场景文件");

            }
            return sceneEditing;
        }

        private void loadScene()
        {
#if true
            string[] filePaths = Directory.GetDirectories(App.SceneDir);
            foreach (string f in filePaths)
            {
                lbScene.Items.Add(Path.GetFileName(f));
            }

#else
            //保持上次关闭时的顺序
            List<string> list = Profile.getSceneName();
            foreach (var item in list)
            {
                string dir = Path.Combine(App.SceneDir, item);
                if (Directory.Exists(dir))
                {
                    lbScene.Items.Add(item);
                }
            }
#endif
        }

        private void addRectOnCanvas(Point start, Point end)
        {
            CanvasMain.Kernel.MouseDown(CanvasMain.TranslatePointForKernelImage(start));
            CanvasMain.Kernel.MouseMove(CanvasMain.TranslatePointForKernelImage(end));
            CanvasMain.Kernel.MouseUp(CanvasMain.TranslatePointForKernelImage(end));
        }

        private void Toast(string msg, int duration = 3, string title = "提示")
        {
            return;

            var toastNotification = new Notification(title, msg, duration, FormAnimator.AnimationMethod.Slide,
                 FormAnimator.AnimationDirection.Up);
            toastNotification.Show();
        }

        public Scene jsonToScene(string sceneDir)
        {
            log.e("jsonToScene  " + sceneDir);
            string jsonStr = TextFile.fileToText(Path.Combine(App.SceneDir, sceneDir, AD_CONFIG_JSON));

            return JsonConvert.DeserializeObject<Scene>(BluCodec.Decode(jsonStr));
        }

        public void sceneToJson(string sceneDir, Scene scene)
        {
            string json = JsonConvert.SerializeObject(scene, Formatting.Indented);
            Console.WriteLine(json);
            json = BluCodec.Encode(json);
            //Console.WriteLine(json);
            using (StreamWriter sw = new StreamWriter(File.Open(Path.Combine(App.SceneDir, sceneDir, AD_CONFIG_JSON), FileMode.Create)))
            {
                sw.WriteLine(json);
            }
        }

        private void OpenScene()
        {
            CanvasMain.Kernel.EndShapeCreate -= new EPKernel.EndShapeCreateHandler(EndShapeCreate);
            Toast("正在打开场景文件");

            string dir = Path.Combine(App.SceneDir, lbScene.GetItemText(lbScene.SelectedItem));
            string json = Path.Combine(dir, AD_CONFIG_JSON);
            var scene = JsonConvert.DeserializeObject<Scene>(BluCodec.Decode(TextFile.fileToText(json)));
            // 重新画在虚拟屏幕区
            CanvasMain.Kernel.DeleteAllShapse();

            Console.WriteLine(JsonConvert.SerializeObject(scene, Formatting.Indented));

            int i_vid = 0;
            int i_pic = 0;
            int i_sub = 0;
            foreach (var layer in scene.layers)
            {
                switch (layer)
                {
                    case AdItemType.Video:
                        tsVideo_Click(null, null);
                        drawRect(scene.video[i_vid]);
                        if (App.VIDEO)
                        {
                            Video v = scene.video[i_vid];
                            List<string> vlist = new List<string>();
                            foreach (var vfile in v.filelist)
                            {
                                vlist.Add(Path.Combine(dir, vfile));
                            }
                            if (vlist.Count > 0)
                            {
                                displayScreenshot(vlist, v.intval);
                            }
                        }
                        i_vid++;
                        break;
                    case AdItemType.Picture:
                        Picture item = scene.picture[i_pic];
                        tsPic_Click(null, null);
                        drawRect(item);

                        List<string> list = new List<string>();
                        foreach (var picFile in item.filelist)
                        {
                            list.Add(Path.Combine(dir, picFile));
                        }
                        showImageOnPic(list, item.intval);
                        i_pic++;
                        break;
                    case AdItemType.Subtitle:
                        tsSub_Click(null, null);
                        drawRect(scene.subtitle[i_sub]);
                        setSubtitleProp(scene.subtitle[i_sub]);
                        i_sub++;
                        break;
                    default:
                        break;
                }

            }

            //CanvasMain.reDraw();
            //CanvasMain.Kernel.RefleshBitmap();

            gbDrawTool.Enabled = true;
            txtSceneName.Text = lbScene.GetItemText(lbScene.SelectedItem);
            txtSceneName.Enabled = true;
            btSceneNewSave.Text = "保存场景";
            btSceneNewSave.Enabled = true;
            sceneEditing = true;
            //SceneTiming = scene.timing; //恢复

            CanvasMain.Kernel.EndShapeCreate += new EPKernel.EndShapeCreateHandler(EndShapeCreate);
        }

        private void setSubtitleProp(Subtitle sub)
        {
            if (CanvasMain.Kernel.SelectedShapesCount == 1)
            {
                RectShape rect = getSelectedRect();

                if (sub.transparent)
                {
                    rect.setTextProperty(TextProperty.BackColor, Color.Transparent);
                }
                else
                {
                    rect.setTextProperty(TextProperty.BackColor, ColorEx.FromHtml(sub.backcolor));
                }
                rect.setTextProperty(TextProperty.FontName, sub.fontname);
                rect.setTextProperty(TextProperty.FontSize, sub.fontsize);
                rect.setTextProperty(TextProperty.Bold, sub.bold);
                rect.setTextProperty(TextProperty.Italic, sub.italic);
                rect.setTextProperty(TextProperty.Underline, sub.underline);
                rect.setTextProperty(TextProperty.Direction, sub.direction);
                rect.setTextProperty(TextProperty.Speed, sub.speed);
                rect.setTextProperty(TextProperty.Text, sub.text);
                rect.setTextProperty(TextProperty.FontColor, ColorEx.FromHtml(sub.fontcolor));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filelist"> file name with path </param>
        /// <param name="intval"></param>
        /// <param name="dir"></param>
        private void showImageOnPic(List<string> filelist, int intval)
        {
            string imgPath = filelist[0];
            //List<string> list = new List<string>(filelist); 

            if (CanvasMain.Kernel.SelectedShapesCount == 1)
            {
                getSelectedRect().setImageIntval(intval);
                getSelectedRect().setImage(filelist);
                getSelectedRect().displayImage(imgPath);
                CanvasMain.reDraw();
                CanvasMain.Kernel.RefleshBitmap();
            }
        }

        private int _toInt(float f)
        {
            int i = (int)Math.Round(f, MidpointRounding.AwayFromZero);
            log.t("_toInt " + f + " => " + i);
            return i;
        }

        private void drawRect(ItemBase item)
        {
            int w = CanvasMain.Width;
            int h = CanvasMain.Height;

            Point start = new Point(_toInt(item.fx), _toInt(item.fy));
            Point end = new Point(_toInt(item.fx1), _toInt(item.fy1));

            addRectOnCanvas(start, end);
        }

        private void MoveUp()
        {
            MoveItem(-1);

        }

        private void MoveDown()
        {
            MoveItem(1);
        }

        private void MoveItem(int direction)
        {
            // Checking selected item
            if (lbScene.SelectedItem == null || lbScene.SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = lbScene.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= lbScene.Items.Count)
                return; // Index out of range - nothing to do

            object selected = lbScene.SelectedItem;

            // Removing removable element
            lbScene.Items.Remove(selected);
            // Insert it in new position
            lbScene.Items.Insert(newIndex, selected);
            // Restore selection
            lbScene.SetSelected(newIndex, true);
        }

        public void useBgTemplate(string name)
        {

            cmCloseScene_Click(null, null);
            createScene("未命名场景");

            Point start = new Point(0, 0);
            Point end = new Point(0, 0);
            start = new Point(0, 0);
            end = new Point(CanvasMain.Size.Width, CanvasMain.Size.Height);
            tsPic_Click(null, null);
            addRectOnCanvas(start, end);

            List<string> list = new List<string>();
            list.Add(name);
            showImageOnPic(list, 0);
        }


    }
}