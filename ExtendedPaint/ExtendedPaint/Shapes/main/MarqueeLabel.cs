using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gdu.ExtendedPaint
{
    [ToolboxBitmap(typeof(Label))]
    public class MarqueeLabel : Label
    {
        private Direction dir = Direction.ToLeft;
        private EventHandler mOnClicked;  //事件触发器
        private EPCanvas parent;
        private int speed = 7;
        private float widthOffset;
        private float currentPosition { get; set; }
        private Timer timer { get; set; }

        public MarqueeLabel(EPCanvas parent)
        {
            UseCompatibleTextRendering = true;
            TextAlign = ContentAlignment.MiddleCenter;
            AutoSize = false;
            AutoEllipsis = false;
            MaximumSize = new Size(8888, 0);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            this.parent = parent;
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

        //[DefaultValue(typeof(Direction), "0"), Description("012")]
        public Direction direction
        {
            get { return dir; }
            set
            {
                dir = value;
                if (dir == Direction.ToLeft) //to L
                {
                    currentPosition = widthOffset;
                }
                else if (dir == Direction.ToRight) // to R
                {
                    currentPosition = 0;
                }
                else //static
                {
                    currentPosition = 0;
                }
                Invalidate();
            }
        }

        [DefaultValue(typeof(int), "7"), Description("0 - 15")]
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                timer.Interval = 300 / (speed + 1);
                Invalidate();
            }
        }

       

        public void destory()
        {
            timer.Stop();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (timer != null)
                    timer.Dispose();
            }
            timer = null;
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
            //e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 0, 0);
            e.Graphics.TranslateTransform((float)currentPosition, 0);

            // Create solid brush.
            //SolidBrush blueBrush = new SolidBrush(Color.Blue);
            // Create rectangle.
            //Rectangle rect = new Rectangle(this.X, this.Y, 30, 30);
            // Fill rectangle to screen.
            //e.Graphics.FillRectangle(blueBrush, rect);

            base.OnPaint(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            Graphics graphics = CreateGraphics();
            SizeF sizeF = graphics.MeasureString(Text, Font);
            //MessageBox.Show(string.Format("字体宽度：{0}，高度：{1}", sizeF.Width, sizeF.Height));
            graphics.Dispose();

            widthOffset = Width - sizeF.Width;
            if (widthOffset < 0)    //文字宽度大于 label 宽度
            {
                widthOffset = Width;
            }
            currentPosition = widthOffset;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dir == Direction.ToLeft) //to L
            {
                if (currentPosition < (-widthOffset))
                    currentPosition = widthOffset;
                else
                    currentPosition -= 2;
            }
            else if (dir == Direction.ToRight) // to R
            {
                Graphics graphics = CreateGraphics();
                SizeF sizeF = graphics.MeasureString(Text, Font);
                //MessageBox.Show(string.Format("字体宽度：{0}，高度：{1}", sizeF.Width, sizeF.Height));
                graphics.Dispose();

                if (currentPosition > Width)
                    currentPosition = -(int)sizeF.Width;
                else
                    currentPosition += 2;
            }
            else //static
            {
                currentPosition = 0;
            }

            //Graphics g = CreateGraphics();
            //SizeF ss = g.MeasureString(Text, Font);
            //MessageBox.Show(string.Format("字体宽度：{0}，高度：{1}", sizeF.Width, sizeF.Height));
            //g.Dispose();

           // Console.WriteLine(currentPosition);
            //Console.WriteLine("width: " + Width);
            // Console.WriteLine("Text width: " + ss);

            Invalidate();
        }
    }
}