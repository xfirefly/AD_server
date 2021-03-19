using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmAddFile : Form
    {
        public enum FileType
        {
            Video,
            Image,
        }


        private const int imgh = 108;
        private const int imgw = 192;

        private FileChangedEventArgs evArgs = new FileChangedEventArgs();
        private int initIntval = -1;
        private List<string> initList;
        private FileType filetype;

        private string filter;

        public frmAddFile()
        {
            InitializeComponent();
        }

        public frmAddFile(string tips, int i, List<string> filelist, FileType type)
            : this()
        {
            this.Text = Resource1.select_file;

            //lbTips.ForeColor = Color.Red;
            lbTips.Text = tips;
            initIntval = i;
            initList = filelist;
            filetype = type;

            if (filetype == FileType.Image)
            {
                filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            }
            else
            {
                filter = "Videos (*.MP4;*.AVI;*.MPG)|*.MP4;*.AVI;*.MPG|All files (*.*)|*.*";
            }
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

            txtSecond.Text = initIntval.ToString();
            if (initIntval <= 0)
            {
                cbClose.Checked = true;
            }

            int i = 0;
            foreach (var item in initList)
            {
                Console.WriteLine(item);
                try
                {
                    string text;
                    Image thumb = getThumb(item, out text);
                    imgList.Images.Add(thumb);
                    lvImg.Items.Add(item, text, i++);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private Image getThumb(string file, out string info)
        {
            if (filetype == FileType.Image)
            {
                using (Image orig = Image.FromFile(file))
                {
                    Image thumb = Util.GetReducedImage(imgw, imgh, orig);
                    info = string.Format("{0}. {1} * {2}", Resource1.res, orig.Width, orig.Height);
                    return thumb;
                }
            }
            else
            {
               
                info = "s";
                return Image.FromFile(FFMPEG.GetThumbnail(file));
            }
        }

        private void AddImg(ref FileInfo fi, ref int j, string ex)
        {
            imgList.Images.Add(Image.FromFile(fi.FullName));
            lvImg.Items.Add(fi.Name.Replace(ex, ""), j);
            j++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtSecond.Text) < 0)
            {
                App.MsgBoxE(Resource1.backimage_loop);
                return;
            }

            evArgs.intval = int.Parse(txtSecond.Text);
            foreach (ListViewItem item in lvImg.Items)
            {
                //_ev.fileList.Add(item.Text);
                evArgs.fileList.Add(item.Name);
                Console.WriteLine(item.Text);
                Console.WriteLine(item.Name);
            }
            OnFileChanged(evArgs);// 触发事件

            Close();
        }

        private void cbClose_CheckedChanged(object sender, EventArgs e)
        {
            //txtSecond.Enabled = !cbClose.Checked;
            //txtSecond.Text = "0";
        }

        private void lvImg_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                e.Item.Selected = false;
        }

        private void lvImg_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ShowImages(string filePath)
        {
            lvImg.View = View.LargeIcon;
            lvImg.LargeImageList = imgList;

            DirectoryInfo di = new DirectoryInfo(filePath);
            FileInfo[] afi = di.GetFiles("*.*");

            string temp;
            int j = 0;
            for (int i = 0; i < afi.Length; i++)
            {
                temp = afi[i].Name.ToLower();
                if (temp.EndsWith(".jpg"))
                {
                    AddImg(ref afi[i], ref j, ".jpg");
                }
                else if (temp.EndsWith(".jpeg"))
                {
                    AddImg(ref afi[i], ref j, ".jpeg");
                }
                else if (temp.EndsWith(".gif"))
                {
                    AddImg(ref afi[i], ref j, ".gif");
                }
                else if (temp.EndsWith(".png"))
                {
                    AddImg(ref afi[i], ref j, ".png");
                }
                else if (temp.EndsWith(".bmp"))
                {
                    AddImg(ref afi[i], ref j, ".bmp");
                }
                else if (temp.EndsWith(".tiff"))
                {
                    AddImg(ref afi[i], ref j, ".tiff");
                }
            }
        }

        //更好是，在子窗体中定义一个自定义事件及其事件参数。代码如下：
        public class FileChangedEventArgs : EventArgs // 事件参数类
        {
            /// <summary>
            /// filename with path
            /// </summary>
            public List<string> fileList = new List<string>();
            public int intval = 0;
        }
    }
}