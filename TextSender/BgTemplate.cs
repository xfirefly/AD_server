using Common;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextSender
{
    public partial class BgTemplate : UserControl
    {
        private const int imgh = 108;
        private const int imgw = 192;

        private Button btn;
        private FileChangedEventArgs evArgs = new FileChangedEventArgs();

        private string filter;

        // ListViewItem.ListViewSubItem mouseMoveHitTestSubItem = null;
        private int iCol_MouseMove = -1;

        private int iRow_MouseMove = -1;

        private ListViewItem mouseMoveHitTestItem = null;
        private MaterialTabControl tbcHome;
        private frmMain progEdit;

        public BgTemplate(MaterialTabControl tbcHome, frmMain progEdit)
        {
            InitializeComponent();

            this.tbcHome = tbcHome;
            this.progEdit = progEdit;
            btn = new MaterialSkin.Controls.MaterialRaisedButton();
            btn.AutoSize = false;
            btn.Text = "使用此模板";
            //btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn.AutoEllipsis = false;
            btn.Hide();
            btn.Click += selectBg;
            this.Controls.Add(btn);

            this.Text = Resource1.select_file;

            //lbTips.ForeColor = Color.Red;
            filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
        }

        private void selectBg(object sender, EventArgs e)
        {
            ListViewItem lvi = (ListViewItem)btn.Tag;
            Console.WriteLine(lvi.Name);

            tbcHome.SelectedIndex = 0;
            progEdit.useBgTemplate(lvi.Name);
        }

        // 声明委托
        public delegate void FileChangedEventHandler(object sender, FileChangedEventArgs e);

        // 定义事件
        public event FileChangedEventHandler FileChanged;

        // 触发事件的方法
        protected virtual void OnFileChanged(FileChangedEventArgs e)
        {
            if (FileChanged != null)
                FileChanged(this, e);
        }

        private void AddBackgroud_Load(object sender, EventArgs e)
        {
            //ShowImages(@"E:\video_music_photo");
            imgList.ImageSize = new Size(imgw, imgh);
            imgList.ColorDepth = ColorDepth.Depth24Bit;

            lvImg.HideSelection = false;
            // Allow the user to select multiple items.
            lvImg.MultiSelect = true;
            // Show CheckBoxes in the ListView.
            lvImg.CheckBoxes = true;
            lvImg.View = View.LargeIcon;
            lvImg.LargeImageList = imgList;

            workerDecode.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (lvImg.CheckedItems.Count > 0)
            {
                ListViewItem item = lvImg.CheckedItems[0];

                for (int i = item.Index + 1; i < lvImg.Items.Count; i++)
                {
                    // adjust the image index of any list view items that follow this one
                    lvImg.Items[i].ImageIndex--;
                }
                lvImg.Items.Remove(item);

                //Image img = imgList.Images[item.ImageIndex];
                imgList.Images.RemoveAt(item.ImageIndex);

                File.Delete(item.Name);
                //img.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog()
            {
                Multiselect = true,
                Title = Resource1.select_file,
                Filter = filter
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //imgList.Images.Clear();
                    //lvImg.Items.Clear();

                    int lvItemCnt = lvImg.Items.Count;
                    //lvImg.CheckBoxes = true;
                    //imgList.ImageSize = new Size(100, 100);

                    foreach (string item in dlg.FileNames)
                    {
                        //using (Image img = Image.FromFile(fname))
                        {
                            //Image imgThumb = img.GetThumbnailImage(2000, 2000, null, IntPtr.Zero);
                            //imgList.Images.Add(imgThumb);
                            //lvImg.Items.Add(fname, lvItemCnt++);
                            string text;
                            Image thumb = getThumb(item, out text);
                            imgList.Images.Add(thumb);
                            lvImg.Items.Add(item, text, lvItemCnt++);

                            File.Copy(item, Path.Combine(App.BgDir, Util.md5(item) + "." + Path.GetExtension(item)));
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvImg.Items)
            {
                //_ev.fileList.Add(item.Text);
                evArgs.fileList.Add(item.Name);
                Console.WriteLine(item.Text);
                Console.WriteLine(item.Name);
            }
            OnFileChanged(evArgs);// 触发事件

            //Close();
        }

        private Image getThumb(string file, out string info)
        {
            using (Image orig = Image.FromFile(file))
            {
                Image thumb = Util.GetReducedImage(imgw, imgh, orig);
                info = string.Format("{0}. {1} * {2}", Resource1.res, orig.Width, orig.Height);
                return thumb;
            }
        }

        private void lvImg_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                e.Item.Selected = false;
        }

        private void lvImg_MouseCaptureChanged(object sender, EventArgs e)
        {
            Console.WriteLine("lvImg_MouseCaptureChanged");
        }

        private void lvImg_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePos = lvImg.PointToClient(Control.MousePosition);
            var hitTest = lvImg.HitTest(mousePos);
            mouseMoveHitTestItem = hitTest.Item;
            //mouseMoveHitTestSubItem = hitTest.SubItem;
            if (mouseMoveHitTestItem != null)
            {
                //var iCol = hitTest.Item.SubItems.IndexOf(hitTest.SubItem);
                //var iRow = hitTest.Item.Index;
                //if (iCol != iCol_MouseMove || iRow != iRow_MouseMove)
                { //Reposition button if the cursor moved to a different cell
                    var bounds = hitTest.Item.Bounds;
                    btn.SetBounds(
                        bounds.Right - btn.Width / 2 - bounds.Width / 2 + lvImg.Left + 15,
                        bounds.Top + lvImg.Top + 10,
                        btn.Width, btn.Height);

                    btn.Tag = hitTest.Item;
                    if (!btn.Visible)
                    {
                        btn.Show();
                        btn.BringToFront();
                        btn.Invalidate();
                    }
                }
            }
        }

        private void lvImg_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //更好是，在子窗体中定义一个自定义事件及其事件参数。代码如下：
        public class FileChangedEventArgs : EventArgs // 事件参数类
        {
            /// <summary>
            /// filename with path
            /// </summary>
            public List<string> fileList = new List<string>();
        }

        private void workerDecode_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string[] filePaths = Directory.GetFiles(App.BgDir);

            foreach (var item in filePaths)
            {
                Console.WriteLine(item);
                try
                {
                    string text;
                    Image thumb = getThumb(item, out text);

                    lstImage.Add(thumb);
                    lstImageName.Add(item);
                    lstImageTxt.Add(text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void workerDecode_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            int i = 0;
            foreach (var img in lstImage)
            {
                imgList.Images.Add(img);
                lvImg.Items.Add(lstImageName[i], lstImageTxt[i], i++);
            }
        }

        private List<Image> lstImage = new List<Image>();
        private List<string> lstImageName = new List<string>();
        private List<string> lstImageTxt = new List<string>();
    }
}