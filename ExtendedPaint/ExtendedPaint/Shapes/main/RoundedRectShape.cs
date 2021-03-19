using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class RoundedRectShape : RectShape
    {
        #region private var

        private byte[] types;
        private RoundedRectProperty _pro;

        #endregion

        #region constructors & initial

        public RoundedRectShape(EPKernel container, RoundedRectProperty pro)
            : base(container, pro)
        {
            _pro = pro;
            initialValue(); 
        }        

        private void initialValue()
        {
            types = new byte[] { 0, 3, 3, 3, 1, 3, 3, 3, 1, 3, 3, 3, 1, 3, 3, 0x83 };
        }

        #endregion

        #region private methods

        private PointF[] getScaledPoints_Sides(PointF[] pf ,int draggedIndex, Point mousePos)
        {
            PointF draggedPoint = SideAnchors[draggedIndex - 4];
            double angle = 0;
            switch (draggedIndex)
            {
                case 4:
                    angle = RightSideAngle - Math.PI / 2;
                    break;
                case 5:
                    angle = RightSideAngle;
                    break;
                case 6:
                    angle = RightSideAngle + Math.PI / 2;
                    break;
                case 7:
                    angle = RightSideAngle + Math.PI;
                    break;
            }
            double mouseAngle = Math.Atan2(mousePos.Y - draggedPoint.Y, mousePos.X - draggedPoint.X);
            float dx = mousePos.X - draggedPoint.X;
            float dy = mousePos.Y - draggedPoint.Y;
            double draggedLength = Math.Sqrt(dx * dx + dy * dy);
            double shadowLength = draggedLength * Math.Cos(mouseAngle - angle);

            float offsetx = (float)(shadowLength * Math.Cos(angle));
            float offsety = (float)(shadowLength * Math.Sin(angle));
            
            int start = (draggedIndex - 4) * 4;
            int end = start + 7;
            for (int i = start; i <= end; i++)
            {
                int j = i % 16;
                pf[j].X += offsetx;
                pf[j].Y += offsety;
            }
            return pf;
        }

        private PointF[] getScaledPoints_Corner(int draggedIndex, Point mousePos, bool shift)
        {
            int[] ex = new int[] { 7, 4, 4, 5, 5, 6, 6, 7 };
            PointF[] pf = getScaledPoints_Sides(Path.PathPoints, ex[draggedIndex * 2], mousePos);
            return getScaledPoints_Sides(pf, ex[draggedIndex * 2 + 1], mousePos);
        }

        #endregion

        #region public properties

        public override ToolType Type
        {
            get { return ToolType.RoundedRect; }
        }

        public override string ReadableName
        {
            get { return "圆角矩形"; }
        }

        #endregion

        public override void SetNewPosForHotAnchor(int index, Point newPos)
        {
            if (index == DraggableHotSpots.Length - 1)
            {
                base.Rotate(newPos);
            }
            else
            {
                PointF[] pf;
                if (index > 3)
                    pf = getScaledPoints_Sides(Path.PathPoints, index, newPos);
                else
                    pf = getScaledPoints_Corner(index, newPos, false);
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

            if (transType == PathTransformType.Rotate)
                RightSideAngle = Math.Atan2(pf[4].Y - pf[3].Y, pf[4].X - pf[3].X);

            corner[0] = new PointF(
                pf[3].X + (float)(_pro.RadiusTL * Math.Cos(RightSideAngle + Math.PI)),
                pf[3].Y + (float)(_pro.RadiusTL * Math.Sin(RightSideAngle + Math.PI)));

            corner[1] = new PointF(
                pf[4].X + (float)(_pro.RadiusTR * Math.Cos(RightSideAngle)),
                pf[4].Y + (float)(_pro.RadiusTR * Math.Sin(RightSideAngle)));

            corner[2] = new PointF(
                pf[11].X + (float)(_pro.RadiusBR * Math.Cos(RightSideAngle)),
                pf[11].Y + (float)(_pro.RadiusBR * Math.Sin(RightSideAngle)));

            corner[3] = new PointF(
                pf[12].X + (float)(_pro.RadiusBL * Math.Cos(RightSideAngle + Math.PI)),
                pf[12].Y + (float)(_pro.RadiusBL * Math.Sin(RightSideAngle + Math.PI)));

            side[0] = new PointF((corner[0].X + corner[1].X) / 2, (corner[0].Y + corner[1].Y) / 2);
            side[1] = new PointF((corner[2].X + corner[1].X) / 2, (corner[2].Y + corner[1].Y) / 2);
            side[2] = new PointF((corner[2].X + corner[3].X) / 2, (corner[2].Y + corner[3].Y) / 2);
            side[3] = new PointF((corner[0].X + corner[3].X) / 2, (corner[0].Y + corner[3].Y) / 2);            
            
            double angle = RightSideAngle + Math.PI / 2;
            PointF rotatePoint = new PointF(
                (float)(side[2].X + EPConst.RotatingRect_Offset * Math.Cos(angle)),
                (float)(side[2].Y + EPConst.RotatingRect_Offset * Math.Sin(angle)));
            base.RotateLocation = rotatePoint;
            base.CenterPoint = new PointF((side[3].X + side[1].X) / 2, (side[0].Y + side[2].Y) / 2);
            CornerAnchors = corner;
            SideAnchors = side;
        }

        protected override void AfterPropertyChanged(BaseProperty oldValue, BaseProperty newValue)
        {
            _pro = (RoundedRectProperty)newValue;
            base.AfterPropertyChanged(oldValue, newValue);
        }

        protected override void ResetPath()
        {
            PointF[] pf = ShapeHelper.GetRoundedRectPathPoints(
                _pro.RadiusAll, Rect.Location, new PointF(Rect.Right, Rect.Bottom));
            base.SetNewScaledPath(pf, types);           
        }
    }
}
