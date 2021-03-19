using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class LineArrowCap : CustomLineCap
    {
        public LineArrowCap()
            : this(7.0f, 7.0f)
        {
        }

        public LineArrowCap(float width, float height)
            : base(null, getFillPath(width, height))
        {
            base.BaseInset = 1.0f;
        }

        private static GraphicsPath getFillPath(float w, float h)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(0, 0, h / 2, -w);
            path.AddLine(0, 0, -h / 2, -w);
            return path;
        }
    }
}
