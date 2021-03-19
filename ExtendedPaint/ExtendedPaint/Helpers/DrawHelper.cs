using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public static class DrawHelper
    {
        public static void DrawCrossSign(Graphics g, PointF pt, int width)
        {
            SmoothingMode old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(Pens.Blue, pt.X - width, pt.Y - width, pt.X + width, pt.Y + width);
            g.DrawLine(Pens.Blue, pt.X + width, pt.Y - width, pt.X - width, pt.Y + width);
            g.SmoothingMode = old;
        }
    }
}
