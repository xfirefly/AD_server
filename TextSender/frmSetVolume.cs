using System;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmSetVolume : Form
    {
        private VolumeChangedEventArgs evArgs = new VolumeChangedEventArgs();

        public frmSetVolume()
        {
            InitializeComponent();
        }

        public void init(int vol)
        {
            // 0 - 100  ,  0:mute
            tbar.Minimum = 0;
            tbar.Maximum = 100;
            tbar.Value = vol;

            tbar.SmallChange = 2;
            tbar.LargeChange = 10;

            tbar.TickFrequency = 5;
            tbar.TickStyle = TickStyle.BottomRight;

            label1.Text = "当前音量: " + tbar.Value;
        }

        // 声明委托
        public delegate void VolumeChangedEventHandler(object sender, VolumeChangedEventArgs e);

        // 定义事件
        public event VolumeChangedEventHandler VolumeChanged;

        // 触发事件的方法
        //protected virtual void OnImgChanged(TimeChangedEventArgs e)

        private void btnSetTimeData_Click(object sender, EventArgs e)
        {
            if (VolumeChanged != null)
            {
                evArgs.Value = tbar.Value;
                VolumeChanged(this, evArgs);
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetTime_Load(object sender, EventArgs e)
        {
        }

        //更好是，在子窗体中定义一个自定义事件及其事件参数。代码如下：
        public class VolumeChangedEventArgs : EventArgs // 事件参数类
        {
            public int Value;
        }

        private void tbar_Scroll_1(object sender, EventArgs e)
        {
            label1.Text = "当前音量: " + tbar.Value;
        }
    }
}