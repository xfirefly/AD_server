using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class EPCanvas : Panel
    {
        #region private const

        // canvas half width
        private static readonly int CW = 0;//60;

        // canvas half height
        private static readonly int CH = 0;//40;

        #endregion

        #region private var

        private EPKernel kernel;

        private Size imageSize;
        private Size canvasSizeMin;
        private Size canvasSizeActual;
        private Rectangle rectImageOutLine;
        private Rectangle rectImage;

        #endregion

        #region constructors

        public EPCanvas()
        {
            base.SetStyle(ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer, true);
            base.ResizeRedraw = true;
            base.BorderStyle = BorderStyle.FixedSingle;
            base.AutoScrollMinSize = Size.Empty;
            //initialValue();
        }

        private Size _size;
        public EPCanvas(Size sz)
            : this()
        {
            _size = sz;
            Console.WriteLine("canvas size " + _size.ToString());
        }

        public void initialValue()
        {
            kernel = new EPKernel(_size, this);//(new Size(600, 480));
            imageSize = kernel.FinalBitmap.Size;

            WhenImageSizeChanged();
            RecalculateCanvas();

            kernel.FinalBitmapChanged += new EventHandler(RefleshFinalBitmapChanged);
            kernel.SelectedShapesChanged += new EventHandler(kernel_SelectedObjectsChanged);
            kernel.SelectToolInSelecting += new EventHandler(kernel_SelectToolInSelecting);
            kernel.CursorTypeChanged += new EventHandler(kernel_CursorTypeChanged);

            kernel.SetNewTool(ToolType.ShapeSelect);
        }

        #endregion

        #region event handler

        private void RefleshFinalBitmapChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void kernel_SelectedObjectsChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void kernel_SelectToolInSelecting(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        private void kernel_CursorTypeChanged(object sender, EventArgs e)
        {
            switch (kernel.CursorType)
            {
                case ToolCursorType.EllipseTool:
                    base.Cursor = CursorsHolder.Instance.ToolEllipse;
                    break;
                case ToolCursorType.LineTool:
                    base.Cursor = CursorsHolder.Instance.ToolLine;
                    break;
                case ToolCursorType.RectTool:
                    base.Cursor = CursorsHolder.Instance.ToolRect;
                    break;
                case ToolCursorType.HandTool:
                    base.Cursor = CursorsHolder.Instance.Pan;
                    break;
                case ToolCursorType.ShapeSelect_Move:
                    base.Cursor = Cursors.SizeAll;
                    break;
                case ToolCursorType.ShapeSelect_DragLineVertex:
                    base.Cursor = CursorsHolder.Instance.Select2;
                    break;
                //case ToolCursorType.ShapeSelect_Scale:
                //    base.Cursor = CursorsHolder.Size0;
                //    break;
                case ToolCursorType.ShapeSelect_Scale_0:
                    base.Cursor = Cursors.SizeWE;
                    break;
                case ToolCursorType.ShapeSelect_Scale_90:
                    base.Cursor = Cursors.SizeNS;
                    break;
                case ToolCursorType.ShapeSelect_Scale_45:
                    base.Cursor = Cursors.SizeNESW;
                    break;
                case ToolCursorType.ShapeSelect_Scale_135:
                    base.Cursor = Cursors.SizeNWSE;
                    break;

                //case ToolCursorType.ShapeSelect_Rotate:
                //    base.Cursor = CursorsHolder.Rotate;
                //    break;
                case ToolCursorType.ShapeSelect_Default:
                    base.Cursor = CursorsHolder.Instance.Select;
                    break;
                case ToolCursorType.Default:
                    base.Cursor = Cursors.Default;
                    break;
                case ToolCursorType.CustomTool:
                    //..
                    break;

            }
        }

        #endregion

        #region override methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(Point.Empty, ClientSize));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            e.Graphics.FillRectangle(Brushes.LightGray, rectImage); // 
            e.Graphics.DrawRectangle(Pens.DarkGray, rectImageOutLine);

            e.Graphics.DrawImage(kernel.FinalBitmap, rectImage.Location);

            e.Graphics.ResetTransform();

            e.Graphics.TranslateTransform(
                rectImage.Location.X + AutoScrollPosition.X,
                rectImage.Location.Y + AutoScrollPosition.Y);

            if (kernel.IsInSelecting)
            {
                kernel.DrawSelectToolSelectingRect(e.Graphics);
            }

            // draw selected rect
            if (kernel.SelectedShapesCount > 0)
            {
                kernel.DrawSizableRects(e.Graphics);
            }

            e.Graphics.ResetTransform();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
           
            RecalculateCanvas();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (kernel.CurrentRectType == AdItemType.Video && e.Button == MouseButtons.Left )
            {
                if (MouseDownHook(e))
                {
                    return;
                }
            }

            if ( e.Button == MouseButtons.Left)
                kernel.MouseDown(TranslatePointForKernelImage(e.Location));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            kernel.MouseMove(TranslatePointForKernelImage(e.Location));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            kernel.MouseUp(TranslatePointForKernelImage(e.Location));


        }

        /// <summary>
        /// 返回true 表示已处理消息, 终止下面的处理
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public delegate bool MouseHookProc(MouseEventArgs e);
        private MouseHookProc MouseDownHook;

        public void setMouseHookProc(MouseHookProc func)
        {
            MouseDownHook = func;
        }
        #endregion

        #region private methods

        public Point TranslatePointForKernelImage(Point pos)
        {
            pos.Offset(-rectImage.Location.X - AutoScrollPosition.X,
                -rectImage.Location.Y - AutoScrollPosition.Y);
            return pos;
        }

        private void RecalculateCanvas ()
        {
            int w = (ClientSize.Width - imageSize.Width) / 2;
            int h = (ClientSize.Height - imageSize.Height) / 2;
            int cw = Math.Max(w, CW);
            int ch = Math.Max(h, CH);
            canvasSizeActual = new Size(imageSize.Width + cw * 2, imageSize.Height + ch * 2);

            rectImage = new Rectangle(new Point(cw, ch), imageSize);
            rectImageOutLine = new Rectangle(cw - 1, ch - 1, imageSize.Width + 1, imageSize.Height + 1);

            if (kernel != null && kernel.FinalBitmap != null)
            {
                //kernel.FinalBitmap = new Bitmap(canvasSizeActual.Width, canvasSizeActual.Height);
            }

            //RecalculateCanvas imageSize{ Width = 890, Height = 396}
            //{ Width = 1384, Height = 708}
            //{ X = 247,Y = 156,Width = 890,Height = 396}
            //{ X = 246,Y = 155,Width = 891,Height = 397}
            //frmMain_Resize canvas   1393 731
            Console.WriteLine("RecalculateCanvas imageSize" + imageSize);
            Console.WriteLine(canvasSizeActual.ToString());
            Console.WriteLine(rectImage.ToString());
            Console.WriteLine(rectImageOutLine.ToString());
        }

        private void RecalculateCanvas_err()
        {
            int w = imageSize.Width;// (ClientSize.Width - imageSize.Width) / 2;
            int h = imageSize.Height;// (ClientSize.Height - imageSize.Height) / 2;
            int cw = Math.Max(w, CW);
            int ch = Math.Max(h, CH);
            canvasSizeActual = imageSize; // imageSize.Width + cw * 2, imageSize.Height + ch * 2);

      
            rectImage = new Rectangle(new Point(1, 1), imageSize);
            rectImageOutLine = new Rectangle(0,0, imageSize.Width + 1, imageSize.Height + 1);

            if (kernel != null && kernel.FinalBitmap != null)
            {
                //kernel.FinalBitmap = new Bitmap(canvasSizeActual.Width, canvasSizeActual.Height);
            }

            Console.WriteLine(" " + this.Size );
            Console.WriteLine(" " + this.Location);

            Console.WriteLine("RecalculateCanvas ClientSize" + ClientSize);
            Console.WriteLine("RecalculateCanvas imageSize" + imageSize);
            Console.WriteLine(canvasSizeActual.ToString());
            Console.WriteLine(rectImage.ToString());
            Console.WriteLine(rectImageOutLine.ToString());
        }

        private void WhenImageSizeChanged()
        {
            Console.WriteLine("WhenImageSizeChanged");
            canvasSizeMin = new Size(imageSize.Width + CW * 2, imageSize.Height + CH * 2);
            base.AutoScrollMinSize = canvasSizeMin;

            Console.WriteLine("WhenImageSizeChanged canvasSizeMin" + canvasSizeMin );
            Console.WriteLine("WhenImageSizeChanged imageSize" + imageSize);
        }

        #endregion

        #region private properties



        #endregion

        #region public properties

        public EPKernel Kernel
        {
            get { return kernel; }
        }

        #endregion

        #region public methods

        public void reDraw()
        {

        }

        #endregion
    }
}
