using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ctrl
{
    [ToolboxBitmap(typeof(Label))]
    public class SubtitleCtrl : Label
    {
        private EventHandler mOnClicked;         //事件触发器

        private float _currentPosition { get; set; }
        private Timer _timer { get; set; }

        public SubtitleCtrl()
        {
            UseCompatibleTextRendering = true;
            
            _timer = new Timer();
            _timer.Interval = 50;
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        public event EventHandler ClickEventHandler
        {
            add
            {
                mOnClicked += value;
            }
            remove
            {
                mOnClicked = value;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (mOnClicked != null)
            {
                mOnClicked.Invoke(this, EventArgs.Empty);
            }
        }

     

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform((float)_currentPosition, 0);

            // Create solid brush.
            SolidBrush blueBrush = new SolidBrush(Color.Blue);

            // Create rectangle.
            Rectangle rect = new Rectangle(this.X  , this.Y , 30, 30);

            // Fill rectangle to screen.
            e.Graphics.FillRectangle(blueBrush, rect);

           base.OnPaint(e);
 
        }

        private int _direction = 0; // L R static
        [DefaultValue(typeof(int), "0"), Description("012")]
        public int Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
                if (_direction == 0) //to L
                {
                    _currentPosition = Width;
                }
                else if (_direction == 1) // to R
                {
                    _currentPosition = 0;
                }
                else //static
                {
                    _currentPosition = 0;
                }
                Invalidate();
            }
        }

        private int _speed = 50;
        [DefaultValue(typeof(int), "50"), Description("0 - 100")]
        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                Invalidate();
            }
        }

        public string fontname { get; internal set; }
        public string fontsize { get; internal set; }
        public bool transparent { get; internal set; }
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public int W { get; internal set; }
        public int H { get; internal set; }

        void Timer_Tick(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            SizeF sizeF = graphics.MeasureString( Text, new Font("宋体", 9));
            //MessageBox.Show(string.Format("字体宽度：{0}，高度：{1}", sizeF.Width, sizeF.Height));
            graphics.Dispose();

            if (_direction == 0) //to L
            {
                if (_currentPosition < 0)
                    _currentPosition = Width;
                else
                    _currentPosition -= 2;
            }
            else if (_direction == 1) // to R
            {
                if (_currentPosition > Width)
                    _currentPosition = -(int)sizeF.Width;
                else
                    _currentPosition += 2;
            }
            else //static
            {
                _currentPosition = 0;
            }

            //Console.WriteLine(_currentPosition);
            Invalidate();
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_timer != null)
                    _timer.Dispose();
            }
            _timer = null;
        }
    }
}
