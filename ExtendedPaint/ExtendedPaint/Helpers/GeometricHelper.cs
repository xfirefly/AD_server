using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public static class GeometricHelper
    {
        /// <summary>
        /// 判断矩形是否包含了线段上的任何点
        /// </summary>       
        public static bool RectContainsPointOnLine(Rectangle rect, Point linePt1, Point linePt2)
        {
            if (rect.Contains(linePt1) || rect.Contains(linePt2))
                return true;
            Point rectPt1 = rect.Location;
            Point rectPt2 = new Point(rect.Left + rect.Width, rect.Top);
            Point rectPt3 = new Point(rect.Left + rect.Width, rect.Top + rect.Height);
            Point rectPt4 = new Point(rect.Left, rect.Top + rect.Height);

            return TwoLinesIntersect(linePt1, linePt2, rectPt1, rectPt2) ||
                TwoLinesIntersect(linePt1, linePt2, rectPt2, rectPt3) ||
                TwoLinesIntersect(linePt1, linePt2, rectPt3, rectPt4) ||
                TwoLinesIntersect(linePt1, linePt2, rectPt1, rectPt4);
        }

        /// <summary>
        /// 判断两条线段是否相交
        /// </summary>
        public static bool TwoLinesIntersect(Point pt11, Point pt12, Point pt21, Point pt22)
        {
            LineInfo info1 = GetLineInfo(pt11, pt12);
            LineInfo info2 = GetLineInfo(pt21, pt22);

            int pt1MinX = Math.Min(pt11.X, pt12.X);
            int pt1MaxX = Math.Max(pt11.X, pt12.X);
            int pt1MinY = Math.Min(pt11.Y, pt12.Y);
            int pt1MaxY = Math.Max(pt11.Y, pt12.Y);

            int pt2MinX = Math.Min(pt21.X, pt22.X);
            int pt2MaxX = Math.Max(pt21.X, pt22.X);
            int pt2MinY = Math.Min(pt21.Y, pt22.Y);
            int pt2MaxY = Math.Max(pt21.Y, pt22.Y);

            float x, y;

            if (info1.type == LineType.Vertical && info2.type == LineType.Vertical)
            {
                if (info1.x == info2.x)
                {
                    // 重合的平行线，有可能相交的
                    return (pt11.Y >= pt2MinY && pt11.Y <= pt2MaxY) || (pt12.Y >= pt2MinY && pt12.Y <= pt2MaxY)
                        || (pt21.Y >= pt1MinY && pt21.Y <= pt1MaxY);
                }
            }
            else if (info1.type == LineType.Horizontal && info2.type == LineType.Horizontal)
            {
                if (info1.y == info2.y)
                {
                    return (pt11.X >= pt2MinX && pt11.X <= pt2MaxX) || (pt12.X >= pt2MinX && pt12.X <= pt2MaxX)
                        || (pt21.X >= pt1MinX && pt21.X <= pt1MaxX);
                }
            }
            else if (info1.type == LineType.Normal && info2.type == LineType.Normal)
            {
                if (Math.Abs(info1.a - info2.a) < 0.001f)
                {
                    // 如果平行且重合
                    if (info1.b == info2.b)
                    {
                        return (pt11.X >= pt2MinX && pt11.X <= pt2MaxX)
                            || (pt12.X >= pt2MinX && pt12.X <= pt2MaxX)
                            || (pt21.X >= pt1MinX && pt21.X <= pt1MaxX);
                    }
                }
                else
                {
                    x = (info2.b - info1.b) / (info1.a - info2.a);
                    y = info1.a * x + info1.b;
                    return (y >= pt1MinY && y <= pt1MaxY) && (y >= pt2MinY && y <= pt2MaxY);
                }
            }
            else if (info1.type == LineType.Horizontal && info2.type == LineType.Vertical)
            {
                return (info2.x >= pt1MinX && info2.x <= pt1MaxX && info1.y >= pt2MinY && info1.y <= pt2MaxY);                    
            }
            else if (info1.type == LineType.Vertical && info2.type == LineType.Horizontal)
            {
                return (info1.x >= pt2MinX && info1.x <= pt2MaxX && info2.y >= pt1MinY && info2.y <= pt1MaxY);
            }
            else if (info1.type == LineType.Horizontal && info2.type == LineType.Normal)
            {
                y = info1.y;
                x = (y - info2.b) / info2.a;
                return (x >= pt1MinX && x <= pt1MaxX) && (x >= pt2MinX && x <= pt2MaxX);
            }
            else if (info1.type == LineType.Normal && info2.type == LineType.Horizontal)
            {
                y = info2.y;
                x = (y - info1.b) / info1.a;
                return (x >= pt1MinX && x <= pt1MaxX) && (x >= pt2MinX && x <= pt2MaxX);
            }
            else if (info1.type == LineType.Vertical && info2.type == LineType.Normal)
            {
                x = info1.x;
                y = info2.a * x + info2.b;
                return (y >= pt1MinY && y <= pt1MaxY) && (y >= pt2MinY && y <= pt2MaxY);
            }
            else if (info1.type == LineType.Normal && info2.type == LineType.Vertical)
            {
                x = info2.x;
                y = info1.a * x + info1.b;
                return (y >= pt1MinY && y <= pt1MaxY) && (y >= pt2MinY && y <= pt2MaxY);
            }

            return false;
        }

        private static LineInfo GetLineInfo(Point pt1, Point pt2)
        {
            LineInfo info = new LineInfo();
            if (pt1.X == pt2.X)
            {
                info.type = LineType.Vertical;
                info.x = pt1.X;
            }
            else if (pt1.Y == pt2.Y)
            {
                info.type = LineType.Horizontal;
                info.y = pt1.Y;
                info.a = 0;
            }
            else
            {
                info.type = LineType.Normal;
                info.a = (pt2.Y - pt1.Y * 1.0f) / (pt2.X - pt1.X * 1.0f);
                info.b = ((pt2.Y + pt1.Y) - info.a * (pt2.X + pt1.X)) / 2.0f;
            }
            return info;
        }

        #region inner class

        private class LineInfo
        {
            public float a;
            public float b;
            public float y;
            public float x;
            public LineType type;
        }

        private enum LineType
        {
            Vertical, 
            Horizontal,
            Normal
        }

        #endregion
    }
}
