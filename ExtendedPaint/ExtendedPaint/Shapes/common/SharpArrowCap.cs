using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class SharpArrowCap : CustomLineCap
    {
        public SharpArrowCap()
            : this(6.8f, 5.2f, 3.6f)
        {
        }

        public SharpArrowCap(float width, float height, float sharpIn)
            : base(getFillPath(width,height, sharpIn), null)
        {
            base.BaseInset = 2.0f;
        }

        private static GraphicsPath getFillPath(float w, float h, float sharp)
        {
            GraphicsPath path = new GraphicsPath();
            PointF p1 = new PointF(0, 0);
            PointF p2 = new PointF(-h / 2, -w);
            PointF p3 = new PointF(0, -sharp);
            PointF p4 = new PointF(h / 2, -w);
            path.AddLines(new PointF[] { p1, p2, p3, p4 });
            path.CloseFigure();
            return path;
        }
    }
}
