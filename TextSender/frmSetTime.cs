using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmSetTime : Form
    {
        private Type type;
        private string initValue;
        private TimeChangedEventArgs evArgs = new TimeChangedEventArgs();

        public enum Type
        {
            DataTime,
            TimeOnly,
        }

        public frmSetTime()
        {
            InitializeComponent();
        }

        public frmSetTime(Type type, string initValue )
                    : this()
        {
            this.type = type;
            this.initValue = initValue;
        }

        // 声明委托
        public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs e);
        // 定义事件
        public event TimeChangedEventHandler timeChanged;
        // 触发事件的方法
        //protected virtual void OnImgChanged(TimeChangedEventArgs e)


        private void btnSetTimeData_Click(object sender, EventArgs e)
        {
            evArgs.Value = dateTimePicker1.Value.ToString();

            if (type == Type.TimeOnly)
            {
                evArgs.Value = dateTimePicker1.Value.Hour.ToString("D2") + ":" + dateTimePicker1.Value.Minute.ToString("D2");
            }

            if (cbDisableTime.Checked)
            {
                evArgs.Value = "0";
            }

            if (timeChanged != null)
                timeChanged(this, evArgs);

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetTime_Load(object sender, EventArgs e)
        {
            if (type == Type.DataTime)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
                dateTimePicker1.Value = DateTime.Parse(initValue);
                dateTimePicker1.ShowUpDown = true;
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "HH:mm";
                dateTimePicker1.ShowUpDown = true;
                //cbDisableTime.Visible = true;


                try
                {
                    dateTimePicker1.Value = DateTime.Parse(initValue);
                }
                catch (Exception)
                {

                    dateTimePicker1.Value = DateTime.Parse("00:00");
                }
            }

        }

        //更好是，在子窗体中定义一个自定义事件及其事件参数。代码如下：
        public class TimeChangedEventArgs : EventArgs // 事件参数类
        {
            public string Value;
        }

        private void cbDisableTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = !cbDisableTime.Checked;
        }
    }
}