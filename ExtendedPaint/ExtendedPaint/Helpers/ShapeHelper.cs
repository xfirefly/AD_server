using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public static class ShapeHelper
    {
        public static PointF[] GetIndicatorArrowPoints(PointF p1, PointF p2, IndicatorSize size)
        {
            int hw = 0;
            float tw = 0;

            switch (size)
            {
                case IndicatorSize.Small:
                    hw = 10;
                    tw = 0.8f;
                    break;
                case IndicatorSize.Medium:
                    hw = 16;
                    tw = 0.8f;
                    break;
                case IndicatorSize.Large:
                    hw = 22;
                    tw = 0.8f;
                    break;
            }

            PointF[] pf = new PointF[7];
            PointF tmp = new PointF();
            double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            tmp.X = p2.X + (float)(hw * Math.Cos(angle + Math.PI));
            tmp.Y = p2.Y + (float)(hw * Math.Sin(angle + Math.PI));

            pf[0] = new PointF(
                p1.X + (float)(tw * Math.Cos(angle - Math.PI / 2)),
                p1.Y + (float)(tw * Math.Sin(angle - Math.PI / 2)));
            pf[6] = new PointF(
                p1.X + (float)(tw * Math.Cos(angle + Math.PI / 2)),
                p1.Y + (float)(tw * Math.Sin(angle + Math.PI / 2)));

            pf[3] = p2;

            pf[1] = new PointF(
                tmp.X + (float)(hw / 4 * Math.Cos(angle - Math.PI / 2)),
                tmp.Y + (float)(hw / 4 * Math.Sin(angle - Math.PI / 2)));
            pf[2] = new PointF(
                tmp.X + (float)(hw / 2 * Math.Cos(angle - Math.PI / 2)),
                tmp.Y + (float)(hw / 2 * Math.Sin(angle - Math.PI / 2)));
            pf[4] = new PointF(
                tmp.X + (float)(hw / 2 * Math.Cos(angle + Math.PI / 2)),
                tmp.Y + (float)(hw / 2 * Math.Sin(angle + Math.PI / 2)));
            pf[5] = new PointF(
                tmp.X + (float)(hw / 4 * Math.Cos(angle + Math.PI / 2)),
                tmp.Y + (float)(hw / 4 * Math.Sin(angle + Math.PI / 2)));

            return pf;
        }

        public static PointF[] GetRoundedRectPathPoints(int radius, PointF pt0, PointF pt2)
        {
            PointF[] pf = new PointF[16];
            PointF pt1 = new PointF(pt2.X, pt0.Y);
            PointF pt3 = new PointF(pt0.X, pt2.Y);

            float magic = 0.55f;
            float len = radius * magic;

            pf[0] = new PointF(pt0.X, pt0.Y + radius);
            pf[1] = new PointF(pt0.X, pf[0].Y - len);
            pf[3] = new PointF(pt0.X + radius, pt0.Y);
            pf[2] = new PointF(pf[3].X - len, pt0.Y);

            pf[4] = new PointF(pt1.X - radius, pt1.Y);
            pf[5] = new PointF(pf[4].X + len, pt1.Y);
            pf[7] = new PointF(pt1.X, pt1.Y + radius);
            pf[6] = new PointF(pt1.X, pf[7].Y - len);

            pf[8] = new PointF(pt2.X, pt2.Y - radius);
            pf[9] = new PointF(pt2.X, pf[8].Y + len);
            pf[11] = new PointF(pt2.X - radius, pt2.Y);
            pf[10] = new PointF(pf[11].X + len, pt2.Y);

            pf[12] = new PointF(pt3.X + radius, pt3.Y);
            pf[13] = new PointF(pf[12].X - len, pt3.Y);
            pf[15] = new PointF(pt3.X, pt3.Y - radius);
            pf[14] = new PointF(pt3.X, pf[15].Y + len);

            return pf;
        }

        public static PointF[] GetBrokenLinePoints(Point p1, Point p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
            PointF p3 = new PointF(
                p1.X + (float)(length / 3 * Math.Cos(angle)),
                p1.Y + (float)(length / 3 * Math.Sin(angle)));
            PointF p4 = new PointF(
                p1.X + (float)(length / 3 * 2 * Math.Cos(angle)),
                p1.Y + (float)(length / 3 * 2 * Math.Sin(angle)));
            return new PointF[] { p1, p3, p4, p2 };
        }
    }
}
