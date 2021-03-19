using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public static class TransformHelper
    {
        /// <summary>
        /// 返回按旋转前x, y坐标放大后的椭圆的数据点
        /// </summary>
        public static PointF[] GetScaledEllipsePathPoints(GraphicsPath path, int index, Point mousePos, bool shift)
        {
            PointF[] pf = path.PathPoints;
            if (index > 3)
            {
                int[] ex = new int[] { 9, 0, 3, 6 };
                return getScaledEllipsePathPoints_Sides(pf, ex[(index - 4)], mousePos);
            }
            else
            {
                return getScaledEllipsePathPoints_Corners(pf, index, mousePos, shift);
            }
        }

        private static PointF[] getScaledEllipsePathPoints_Corners(PointF[] pf, int index, Point mousePos, bool shift)
        {
            int[] ex = new int[] { 6, 9, 9, 0, 0, 3, 3, 6 };
            pf = getScaledEllipsePathPoints_Sides(pf, ex[index * 2], mousePos);
            return getScaledEllipsePathPoints_Sides(pf, ex[index * 2 + 1], mousePos);
        }

        private static PointF[] getScaledEllipsePathPoints_Sides(PointF[] pf, int index, Point mousePos)
        {            
            int index2 = (index + 6) % 12;
            float dx = mousePos.X - pf[index].X;
            float dy = mousePos.Y - pf[index].Y;
            double r = Math.Sqrt(dx * dx + dy * dy);
            double angle = Math.Atan2(pf[index].Y - pf[index2].Y, pf[index].X - pf[index2].X);
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
        public static PointF[] GetScaledRectPathPoints(GraphicsPath path, int draggedPointIndex, 
            PointF draggedPoint, Point mousePos, bool shift)
        {
            if (draggedPointIndex > 3)
                return getRectScaledPoints_Sides(path, draggedPointIndex, draggedPoint, mousePos);
            else
                return getRectScaledPoints_Corners(path, draggedPointIndex, mousePos, shift);
        }

        private static PointF[] getRectScaledPoints_Sides(GraphicsPath path, 
            int draggedPointIndex, PointF draggedPoint, Point mousePos)
        {
            int targetPt1Index = draggedPointIndex - 4;
            int targetPt2Index = (targetPt1Index + 1) % 4;
            int anglePt2Index = targetPt2Index;
            int anglePt1Index = (anglePt2Index + 1) % 4;

            PointF[] pf = path.PathPoints;

            double angle = Math.Atan2(pf[anglePt2Index].Y - pf[anglePt1Index].Y,
                pf[anglePt2Index].X - pf[anglePt1Index].X);
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

        private static PointF[] getRectScaledPoints_Corners(GraphicsPath path, 
            int draggedPointIndex, Point mousePos, bool shift)
        {
            int targetPt1Index = (draggedPointIndex + 3) % 4;
            int targetPt2Index = (draggedPointIndex + 1) % 4;
            int pinPtIndex = (draggedPointIndex + 2) % 4;

            PointF[] pf = path.PathPoints;

            double angle1 = Math.Atan2(pf[targetPt1Index].Y - pf[pinPtIndex].Y,
                pf[targetPt1Index].X - pf[pinPtIndex].X);
            double angle2 = Math.Atan2(pf[targetPt2Index].Y - pf[pinPtIndex].Y,
                pf[targetPt2Index].X - pf[pinPtIndex].X);
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
