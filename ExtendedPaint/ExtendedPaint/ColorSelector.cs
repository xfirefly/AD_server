using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gdu.ExtendedPaint
{
    public partial class ColorSelector : UserControl
    {
        private static readonly Color SelectedBackColor = Color.FromArgb(255, 221, 138);

        private EventHandler stroke_handler;
        private EventHandler fill_handler;

        public event EventHandler StrokeColorChange
        {
            add { stroke_handler = value; }
            remove { stroke_handler = null; }
        }

        public event EventHandler FillColorChange
        {
            add { fill_handler = value; }
            remove { fill_handler = null; }
        }

        private bool _strokeSelected = true;
        private Color _strokeColor;
        private Color _fillColor;

        public ColorSelector()
        {
            InitializeComponent();

            _strokeColor = Color.Black;
            _fillColor = Color.White;

            panel1.BackColor = SelectedBackColor;
        }

        public Color StrokeColor
        {
            get { return lblStroke.BackColor; }
            set { lblStroke.BackColor = value; }
        }

        public Color FillColor
        {
            get { return lblFill.BackColor; }
            set { lblFill.BackColor = value; }
        }

        private void ColorSelector_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            _strokeSelected = true;
            panel1.BackColor = SelectedBackColor;
            panel2.BackColor = this.BackColor;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            _strokeSelected = false;
            panel2.BackColor = SelectedBackColor;
            panel1.BackColor = this.BackColor;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Label lb = (Label)sender;
            if (_strokeSelected)
            {
                lblStroke.BackColor = lb.BackColor;
                if (stroke_handler != null)
                    stroke_handler(this, EventArgs.Empty);
            }
            else
            {
                lblFill.BackColor = lb.BackColor;
                if (fill_handler != null)
                    fill_handler(this, EventArgs.Empty);
            }
        }
    }
}
