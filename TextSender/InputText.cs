using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmInputText : Form
    {
        public frmInputText()
        {
            InitializeComponent();
        }

        public frmInputText(string txt)
            : this()
        {
            textBox1.Text = txt;
        }

        private void InputText_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 声明和定义委托
        public delegate void TxtChangedHandler(string title);
        public TxtChangedHandler TxtChanged;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TxtChanged != null)
                TxtChanged(textBox1.Text); //委托调用
        }
    }
}
