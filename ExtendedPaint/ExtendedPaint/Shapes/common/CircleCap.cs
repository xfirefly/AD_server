using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class CircleCap : CustomLineCap
    {
        public CircleCap()
            : this(5.0f, 5.0f)
        {
        }

        public CircleCap(float width, float height)
            : base(getFillPath(width, height), null)
        {
        }

        private static GraphicsPath getFillPath(float w, float h)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new RectangleF(-h / 2, -w / 2, h, w));
            return path;
        }
    }
}
