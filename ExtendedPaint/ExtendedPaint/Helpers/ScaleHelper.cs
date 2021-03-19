using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public static class ScaleHelper
    {
        /// <summary>
        /// 返回按旋转前x, y坐标放大后的椭圆的数据点
        /// </summary>
        public static PointF[] GetScaledEllipsePathPoints(GraphicsPath path, int index, Point mousePos,
            bool shift, double topAng, double rightAng)
        {
            PointF[] pf = path.PathPoints;
            if (index > 3)
            {
                int[] ex = new int[] { 9, 0, 3, 6 };
                return getScaledEllipsePathPoints_Sides(pf, ex[(index - 4)], mousePos, topAng, rightAng);
            }
            else
            {
                return getScaledEllipsePathPoints_Corners(pf, index, mousePos, shift, topAng, rightAng);
            }
        }

        private static PointF[] getScaledEllipsePathPoints_Corners(PointF[] pf, int index, Point mousePos,
            bool shift, double topAng, double rightAng)
        {
            int[] ex = new int[] { 6, 9, 9, 0, 0, 3, 3, 6 };
            pf = getScaledEllipsePathPoints_Sides(pf, ex[index * 2], mousePos, topAng, rightAng);
            return getScaledEllipsePathPoints_Sides(pf, ex[index * 2 + 1], mousePos, topAng, rightAng);
        }

        private static PointF[] getScaledEllipsePathPoints_Sides(PointF[] pf, int index, Point mousePos,
            double topAng, double rightAng)
        {
            int index2 = (index + 6) % 12;
            float dx = mousePos.X - pf[index].X;
            float dy = mousePos.Y - pf[index].Y;
            double r = Math.Sqrt(dx * dx + dy * dy);
            double angle = 0;

            switch (index)
            {
                case 0:
                    angle = rightAng;
                    break;
                case 3:
                    angle = topAng + Math.PI;
                    break;
                case 6:
                    angle = rightAng + Math.PI;
                    break;
                case 9:
                    angle = topAng;
                    break;
            }

            double mouseAngle = Math.Atan2(mousePos.Y - pf[index].Y, mousePos.X - pf[index].X);
            double length = r * Math.Cos(mouseAngle - angle);
            dx = (float)(length * Math.Cos(angle));
            dy = (float)(length * Math.Sin(angle));

            int[] indices = new int[] { 9, 11, 12, 13, 15 };
            foreach (int i in indices)
            {
                int j = (index + i) % 12;
                if (i == 11 || i == 12 || i == 13)
                {
                    pf[j].X += dx;
                    pf[j].Y += dy;
                }
                else
                {
                    pf[j].X += dx / 2;
                    pf[j].Y += dy / 2;
                }
            }

            dx = pf[index].X - pf[index2].X;
            dy = pf[index].Y - pf[index2].Y;
            length = Math.Sqrt(dy * dy + dx * dx);
            float len = (float)(length / 2 * EPConst.MagicBezier);
            angle = Math.Atan2(pf[index].Y - pf[index2].Y, pf[index].X - pf[index2].X);
            int t1 = (index + 8) % 12;
            int t2 = (index + 9) % 12;
            int t3 = (index + 10) % 12;
            pf[t3].X = (float)(pf[t2].X + len * Math.Cos(angle));
            pf[t3].Y = (float)(pf[t2].Y + len * Math.Sin(angle));
            pf[t1].X = (float)(pf[t2].X + len * Math.Cos(angle + Math.PI));
            pf[t1].Y = (float)(pf[t2].Y + len * Math.Sin(angle + Math.PI));

            int s1 = (index + 4) % 12;
            int s2 = (index + 3) % 12;
            int s3 = (index + 2) % 12;
            dx = pf[s2].X - pf[t2].X;
            dy = pf[s2].Y - pf[t2].Y;
            pf[s3].X = pf[t3].X + dx;
            pf[s3].Y = pf[t3].Y + dy;
            pf[s1].X = pf[t1].X + dx;
            pf[s1].Y = pf[t1].Y + dy;

            pf[12] = pf[0];
            return pf;
        }

        /// <summary>
        /// 适用于矩形，返回按旋转前x, y坐标放大后的全部4个点
        /// </summary>
        public static PointF[] GetScaledRectPathPoints(GraphicsPath path, int draggedPointIndex, PointF draggedPoint,
            Point mousePos, bool shift, double topAng, double rightAng)
        {
            if (draggedPointIndex > 3)
                return getRectScaledPoints_Sides(path, draggedPointIndex, draggedPoint, mousePos, topAng, rightAng);
            else
                return getRectScaledPoints_Corners(path, draggedPointIndex, mousePos, shift, topAng, rightAng);
        }

        private static PointF[] getRectScaledPoints_Sides(GraphicsPath path, int draggedPointIndex, 
            PointF draggedPoint, Point mousePos, double topAng, double rightAng)
        {
            int targetPt1Index = draggedPointIndex - 4;
            int targetPt2Index = (targetPt1Index + 1) % 4;            
            
            PointF[] pf = path.PathPoints;

            double angle = 0;
            switch (draggedPointIndex)
            {
                case 4:
                    angle = topAng;
                    break;
                case 5:
                    angle = rightAng;
                    break;
                case 6:
                    angle = topAng + Math.PI;
                    break;
                case 7:
                    angle = rightAng + Math.PI;
                    break;
            }

            float dy = mousePos.Y - draggedPoint.Y;
            float dx = mousePos.X - draggedPoint.X;
            double mouseAngle = Math.Atan2(dy, dx);
            double r = Math.Sqrt(dy * dy + dx * dx);
            double length = r * Math.Cos(mouseAngle - angle);

            pf[targetPt1Index].X = (float)(pf[targetPt1Index].X + length * Math.Cos(angle));
            pf[targetPt1Index].Y = (float)(pf[targetPt1Index].Y + length * Math.Sin(angle));
            pf[targetPt2Index].X = (float)(pf[targetPt2Index].X + length * Math.Cos(angle));
            pf[targetPt2Index].Y = (float)(pf[targetPt2Index].Y + length * Math.Sin(angle));

            return pf;
        }

        private static PointF[] getRectScaledPoints_Corners(GraphicsPath path, int draggedPointIndex,
            Point mousePos, bool shift, double topAng, double rightAng)
        {
            int targetPt1Index = (draggedPointIndex + 3) % 4;
            int targetPt2Index = (draggedPointIndex + 1) % 4;
            int pinPtIndex = (draggedPointIndex + 2) % 4;

            PointF[] pf = path.PathPoints;

            double angle1 = 0;
            double angle2 = 0;

            switch (draggedPointIndex)
            {
                case 0:
                    angle1 = rightAng + Math.PI;
                    angle2 = topAng;
                    break;
                case 1:
                    angle1 = topAng;
                    angle2 = rightAng;
                    break;
                case 2:
                    angle1 = rightAng;
                    angle2 = topAng + Math.PI;
                    break;
                case 3:
                    angle1 = topAng + Math.PI;
                    angle2 = rightAng + Math.PI;
                    break;
            }

            float dy = mousePos.Y - pf[pinPtIndex].Y;
            float dx = mousePos.X - pf[pinPtIndex].X;
            double mouseAngle = Math.Atan2(dy, dx);
            double r = Math.Sqrt(dy * dy + dx * dx);
            double length1 = r * Math.Cos(mouseAngle - angle1);
            double length2 = r * Math.Cos(mouseAngle - angle2);

            float x1 = (float)(pf[pinPtIndex].X + length1 * Math.Cos(angle1));
            float y1 = (float)(pf[pinPtIndex].Y + length1 * Math.Sin(angle1));
            float x2 = (float)(pf[pinPtIndex].X + length2 * Math.Cos(angle2));
            float y2 = (float)(pf[pinPtIndex].Y + length2 * Math.Sin(angle2));

            pf[targetPt1Index].X = x1;
            pf[targetPt1Index].Y = y1;
            pf[draggedPointIndex].X = mousePos.X;
            pf[draggedPointIndex].Y = mousePos.Y;
            pf[targetPt2Index].X = x2;
            pf[targetPt2Index].Y = y2;

            return pf;
        }
    }
}
