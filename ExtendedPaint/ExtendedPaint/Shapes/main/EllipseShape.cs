using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class EllipseShape : RectShape
    {
        public EllipseShape(EPKernel container, FillableProperty pro)
            : base(container, pro)
        { }

        public override ToolType Type
        {
            get { return ToolType.Ellipse; }
        }

        public override string ReadableName
        {
            get { return "椭圆"; }
        }

        public override void SetNewPosForHotAnchor(int index, Point newPos)
        {
            if (index == DraggableHotSpots.Length - 1)
            {
                base.Rotate(newPos);
            }
            else
            {
                //PointF[] pf = TransformHelper.GetScaledEllipsePathPoints(base.Path, index, newPos, false);
                PointF[] pf = ScaleHelper.GetScaledEllipsePathPoints(base.Path, index, newPos, false, 
                    TopSideAngle, RightSideAngle);
                base.SetNewScaledPath(pf);
            }
            LastTransformPoint = newPos;
        }        

        /// <summary>
        /// reset center point, rotate point, corner/side anchors, top/right side angle
        /// </summary>
        protected override void ResetPathExtraInfo(PathTransformType transType)
        {
            PointF[] pf = base.Path.PathPoints;
            PointF[] corner = new PointF[4];
            PointF[] side = new PointF[4];

            base.CenterPoint = new PointF((pf[6].X + pf[0].X) / 2, (pf[9].Y + pf[3].Y) / 2);

            side[0] = pf[9];
            side[1] = pf[0];
            side[2] = pf[3];
            side[3] = pf[6];

            float rdx = pf[0].X - CenterPoint.X;
            float rdy = pf[0].Y - CenterPoint.Y;
            float ldx = pf[6].X - CenterPoint.X;
            float ldy = pf[6].Y - CenterPoint.Y;

            corner[0] = new PointF(pf[9].X + ldx, pf[9].Y + ldy);
            corner[1] = new PointF(pf[9].X + rdx, pf[9].Y + rdy);
            corner[2] = new PointF(pf[3].X + rdx, pf[3].Y + rdy);
            corner[3] = new PointF(pf[3].X + ldx, pf[3].Y + ldy);

            CornerAnchors = corner;
            SideAnchors = side;

            if (transType == PathTransformType.Rotate || transType == PathTransformType.Scale)
            {
                double tmp = Math.Atan2(pf[0].Y - pf[6].Y, pf[0].X - pf[6].X);
                if (tmp != 0)
                    RightSideAngle = tmp;
                tmp = Math.Atan2(pf[9].Y - pf[3].Y, pf[9].X - pf[3].X);
                if (tmp != 0)
                    TopSideAngle = tmp;
            }

            double angle = TopSideAngle + Math.PI;
            PointF rotatePoint = new PointF(
                (float)(pf[3].X + EPConst.RotatingRect_Offset * Math.Cos(angle)),
                (float)(pf[3].Y + EPConst.RotatingRect_Offset * Math.Sin(angle)));
            base.RotateLocation = rotatePoint;
        }

        protected override void ResetPath()
        {
            base.BeforePathTransforming();
            base.Path.Reset();
            base.Path.AddEllipse(base.Rect);
            AfterPathTransformed(PathTransformType.Scale, true);
        }
    }
}
