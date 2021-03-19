using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Common;
using System.IO;

namespace TextSender
{
    public partial class BoxCtrl : UserControl
    {
        private Log log;
        public BoxCtrl()
        {
            InitializeComponent();

            log = new Log(GetType());
            App.Instance.boxCtrl = this;
            btSetWanIP.Visible = false;
        }

        public void removeBox(Box box)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Box>(removeBox), new object[] { box });
                return;
            }

            lbBox.Items.Remove(box);
        }

        public void addBox(Box box)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Box>(addBox), new object[] { box });
                return;
            }

            lbBox.Items.Add(box);
        }

        private void miSetBoxName_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox(Resource1.set_box_name, Resource1.set, "", -1, -1);

            if (string.IsNullOrEmpty(s))
            {
                log.e("cancel miSetBoxName_Click");
            }
            else
            {
                Box box = lbBox.SelectedItem as Box;
                byte[] b = Cmd.create(Command.set_name, s);
                box.CmdChan.send(b);
                box.name = s;

                lbBox.Items[lbBox.SelectedIndex] = box;
            }
        }

        private void miSetBoxId_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox(Resource1.set_id, Resource1.set, "", -1, -1);

            if (string.IsNullOrEmpty(s))
            {
                log.e("cancel miSetBoxName_Click");
            }
            else
            {
                try
                {
                    int.Parse(s);

                    Box box = lbBox.SelectedItem as Box;
                    byte[] b = Cmd.create(Command.set_id, s);
                    box.CmdChan.send(b);
                    box.id = s;

                    lbBox.Items[lbBox.SelectedIndex] = box;
                }
                catch (Exception)
                {

                }
            }
        }

        internal void showImage(string v)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(showImage), new object[] { v });
                return;
            }

            pbScreen.SizeMode = PictureBoxSizeMode.Zoom;
            pbScreen.Image = Image.FromFile(v);
        }

        private void miScreenCap_Click(object sender, EventArgs e)
        {
            if (pbScreen.Image != null)
            {
                pbScreen.Image.Dispose();
                pbScreen.Image = null; // Not really necessary
            }
            
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.get_screen, " ");
            box.CmdChan.send(b);
        }

        public void showfrmSetTime(string time)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(showfrmSetTime), new object[] { time });
                return;
            }

            frmSetTime frm = new frmSetTime(frmSetTime.Type.DataTime, time);
            frm.timeChanged += boxTimeChanged;
            frm.ShowDialog();

        }

        private void boxTimeChanged(object sender, frmSetTime.TimeChangedEventArgs e)
        {
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.set_time, e.Value);
            box.CmdChan.send(b);
        }

        private void miSetTime_Click(object sender, EventArgs e)
        {
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.get_time, " ");
            box.CmdChan.send(b);


            //string s = Interaction.InputBox("为终端设定时间, 如设定为电脑时间直接按确定即可", "设定时间", 
            //    DateTime.Now.ToLocalTime().ToString()   , -1, -1);

            //if (string.IsNullOrEmpty(s))
            //{
            //    log.e("cancel miSetTime_Click");
            //}
            //else
            //{

            //    Box box = lbBox.SelectedItem as Box;
            //    byte[] b = Command.createBuffer(Command.set_time, s);
            //    box.CmdChan.send(b);
            //    box.name = s;

            //    lbBox.Items[lbBox.SelectedIndex] = box;
            //}
        }



        private void miPrevScene_Click(object sender, EventArgs e)
        {
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.prev_scene, " ");
            box.CmdChan.send(b);
        }

        private void miNextScene_Click(object sender, EventArgs e)
        {
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.next_scene, " ");
            box.CmdChan.send(b);
        }

        private void miSetVolume_Click(object sender, EventArgs e)
        {
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.get_volume, " ");
            box.CmdChan.send(b);

        }

        public void showfrmSetVolume(int vol)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(showfrmSetVolume), new object[] { vol });
                return;
            }

            frmSetVolume frm = new frmSetVolume();
            frm.init(vol);
            frm.VolumeChanged += boxVolumeChanged;
            frm.ShowDialog();
        }


        private void boxVolumeChanged(object sender, frmSetVolume.VolumeChangedEventArgs e)
        {
            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.set_volume, e.Value.ToString());
            box.CmdChan.send(b);
        }

        private void miRebootBox_Click(object sender, EventArgs e)
        {
            //Box box = lbBox.SelectedItem as Box;
            //byte[] b = Cmd.create(Command.reboot, " ");
            //box.CmdChan.send(b);

            Box box = lbBox.SelectedItem as Box;
            byte[] b = Cmd.create(Command.clear, " ");
            box.CmdChan.send(b);
        }

        private void miInstallApk_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "选择apk",
                Filter = "apk (*.APK)|*.APK|All files (*.*)|*.*"
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    log.t(dlg.FileName);
                    log.t(App.ZipPath);
                    File.Copy(dlg.FileName, App.ZipPath, true);

                    Box box = lbBox.SelectedItem as Box;
                    byte[] b = Cmd.create(Command.install_apk, " ");
                    box.CmdChan.send(b);
                }
            }
        }

        private void cmBox_Opening(object sender, CancelEventArgs e)
        {
            miSetTime.Enabled = miScreenCap.Enabled = miSetBoxName.Enabled = miSetBoxId.Enabled =
                miPrevScene.Enabled = miNextScene.Enabled = miScreenCap.Enabled =
                miSetVolume.Enabled = miRebootBox.Enabled =
                gbCtrl.Enabled =
                (lbBox.SelectedItem != null);
        }

        
        private void cbSelAllBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lbBox_MouseUp(object sender, MouseEventArgs e)
        {
            gbCtrl.Enabled = (lbBox.SelectedItem != null);

        }

        private void btSetWanIP_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox("如果终端需要通过互联网控制, 请设定服务器IP." + 
                "局域网不需要设定IP, 直接设为空, 按确定即可. (格式: 8.8.8.4 )", Resource1.set, "", -1, -1);

            if (string.IsNullOrEmpty(s))
            {
                log.e("cancel btSetWanIP_Click");
                Box box = lbBox.SelectedItem as Box;
                byte[] b = Cmd.create(Command.set_wan_ip, "null");
                box.CmdChan.send(b);
            }
            else
            {
                Box box = lbBox.SelectedItem as Box;
                byte[] b = Cmd.create(Command.set_wan_ip, s);
                box.CmdChan.send(b);
            }
        }
    }
}
