using Common;
using Net;
using ProgressTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextSender
{
    public partial class ProgramRelease : UserControl, IAdUserControl
    {
        private bool isLoopPlay = true;
        private Log log;
        private AdScene ad;
        private ProgressForm progressForm;

        public ProgramRelease()
        {
            log = new Log(GetType());
            InitializeComponent();

            App.Instance.progRelease = this;

            ad = AdScene.Instance;
        }

        public void addBox(Box box)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Box>(addBox), new object[] { box });
                return;
            }

            lbBoxOnline.Items.Add(box);
        }

        public void OnActivate()
        {
            log.i("PR OnActivate");
            string[] filePaths = Directory.GetDirectories(App.SceneDir);
            lbAllSceme.Items.Clear();
            foreach (string f in filePaths)
            {
                lbAllSceme.Items.Add(Path.GetFileName(f));
            }
        }

        public void removeBox(Box box)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Box>(removeBox), new object[] { box });
                return;
            }

            lbBoxOnline.Items.Remove(box);
        }
        public void updateProgress(int value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(updateProgress), new object[] { value });
                return;
            }

            if (progressForm != null)
            {
                progressForm.SetProgress(value);
            }
        }

        private void btDeselect_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lbSelScene, lbAllSceme);
        }

        private void btPlayLoop_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    playLoop();
                }
            }
        }

        TextBox tbIntval;
        private void playLoop()
        {
            isLoopPlay = true;

            gbModeSet.Controls.Clear();
            int y = 20;

            Label lb = new Label();
            lb.AutoSize = true;
            lb.Text = "轮流播放的时间间隔(单位: 秒):";
            lb.Location = new Point(5, y + 4);
            gbModeSet.Controls.Add(lb);

            Label lb2 = new Label();
            lb2.AutoSize = true;
            lb2.Text = "设为 0 表示接收完立即播放.";
            lb2.Location = new Point(5, y + 4 + 20);
            gbModeSet.Controls.Add(lb2);

            tbIntval = new TextBox();
            tbIntval.Location = new Point(5 + lb.Width, y);
            tbIntval.KeyPress += txt_KeyPress;
            tbIntval.Text = "0";
            gbModeSet.Controls.Add(tbIntval);
        }

        private void btPlayLoop_Click(object sender, EventArgs e)
        {
        }

        private void btPlayTiming_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    playTiming();
                }
            }
        }

        List<SetSceneTiming> sstList = new List<SetSceneTiming>();
        private void playTiming()
        {
            isLoopPlay = false;

            gbModeSet.Controls.Clear();
            sstList.Clear();
            int y = 20;

            for (int i = 0; i < lbSelScene.Items.Count; i++)
            {
                SetSceneTiming sst = new SetSceneTiming();
                sst.Location = new Point(5, y + sst.Height * i);
                sst.SceneName = lbSelScene.Items[i].ToString();
                sst.SceneTiming = "00:00";

                gbModeSet.Controls.Add(sst);
                sstList.Add(sst);
            }
        }

        private void btPlayTiming_Click(object sender, EventArgs e)
        {
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lbAllSceme, lbSelScene);
        }

        private void btSend_Click(object sender, EventArgs e)
        {
            if (lbBoxOnline.CheckedItems.Count == 0)
            {
                App.MsgBoxE(Resource1.not_select_box);
                return ;
            }

            if (App.MsgBox2(Resource1.send_to_box, Resource1.notice) != DialogResult.OK)
            {
                return;
            }

            if (doCreateZip())
            {
                long length = new System.IO.FileInfo(App.ZipPath).Length;
                foreach (var item in lbBoxOnline.CheckedItems)
                {
                    Box box = item as Box;
                    byte[] b = Cmd.create(Command.send_ad, length.ToString());
                    box.CmdChan.send(b);
                }

                FileChannel.BoxCount = lbBoxOnline.CheckedItems.Count;
                FileChannel.Done = 0;
                showSendData(FileChannel.BoxCount);
            }
        }

        private bool doCreateZip()
        {         
            if (lbSelScene.Items.Count == 0)
            {
                App.MsgBoxE(Resource1.sel_scene_first);
                return false;
            }

            // 选择了哪些scene
            //List<string> scene = new List<string>();
            //foreach (var item in lbSelScene.Items)
            //{
            //    scene.Add(item.ToString());
            //}
            var scenes = lbSelScene.Items.Cast<String>().ToList();
            
            if (isLoopPlay)
            {
                try
                {
                    ad.adProgram.sceneIntval = int.Parse(tbIntval.Text);
                }
                catch (Exception)
                {
                    App.MsgBoxE(Resource1.no_set_scene_loop); 
                    return false;
                }

                //有 >= 2个场景, 但是没设置轮播时间
                if (scenes.Count > 1 && ad.adProgram.sceneIntval <= 0)
                {
                    App.MsgBoxE("有多个场景需要播放, 必须设定一个轮播间隔时间!");
                    return false;
                }
                ad.selectedScene(scenes, new List<string>());
            }
            else
            {
                ad.adProgram.sceneIntval = -1;

                //每个场景的定时
                List<string> timing = new List<string>();
                foreach (var item in sstList)
                {
                    timing.Add(item.SceneTiming);
                }
                ad.selectedScene(scenes, timing);
            }

            return ad.mergeCreateZip();
        }

        private void btSendUsb_Click(object sender, EventArgs e)
        {
            //frmMain.adProgram.sceneIntval = int.Parse(txtIntval.Text);

            if (doCreateZip())
            {
                ad.writeToUSB(App.ZipPath);
            }
        }

        private void cbSelAllBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lbBoxOnline.Items.Count; i++)
            {
                lbBoxOnline.SetItemChecked(i, cbSelAllBox.Checked);
            }
            btSend.Enabled = cbSelAllBox.Checked;
        }
        private void lbAllSceme_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonsEditable();
        }

        private void lbBoxOnline_MouseUp(object sender, MouseEventArgs e)
        {
            btSend.Enabled = lbBoxOnline.CheckedItems.Count != 0;
        }

        private void lbBoxOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // Move selected items from one ListBox to another.
        private void MoveSelectedItems(ListBox lstFrom, ListBox lstTo)
        {
            while (lstFrom.SelectedItems.Count > 0)
            {
                string item = (string)lstFrom.SelectedItems[0];
                if (lstTo.Items.Contains(item) == false )
                    lstTo.Items.Add(item);
                lstFrom.Items.Remove(item);
            }
            SetButtonsEditable();
        }
        private void SetButtonsEditable()
        {
            btSelect.Enabled = (lbAllSceme.SelectedItems.Count > 0);
            //btnSelectAll.Enabled = (lstUnselected.Items.Count > 0);
            btDeselect.Enabled = (lbSelScene.SelectedItems.Count > 0);
            //btnDeselectAll.Enabled = (lstSelected.Items.Count > 0);

            if (isLoopPlay)
            {
                playLoop();
            }
            else
            {
                playTiming();
            }
        }

        private DialogResult showSendData(int value)
        {
            progressForm = new ProgressForm(value);
            progressForm.Text = "发送数据";
            //StartForm(progressForm);

            //check how the background worker finished
            DialogResult result = progressForm.ShowDialog();
            if (result == DialogResult.Cancel)
                App.MsgBox("取消操作");
            if (result == DialogResult.Abort)
                MessageBox.Show("Exception:" + Environment.NewLine);

            progressForm = null;
            return result;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // http://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ucProgSend_Load(object sender, EventArgs e)
        {
            SetButtonsEditable();
            log.w("ucProgSend_Load");
        }

 
    }
}