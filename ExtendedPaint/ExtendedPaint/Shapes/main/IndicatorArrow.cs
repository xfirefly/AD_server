using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class IndicatorArrow : LineShape
    {
        private byte[] types = new byte[] { 0, 1, 1, 1, 1, 1, 0x81 };
        private IndicatorArrowProperty _property;

        public IndicatorArrow(EPKernel container, IndicatorArrowProperty pro)
            :base(container, null)
        {
            _property = pro;
        }

        public override ToolType Type
        {
            get { return ToolType.IndicatorArrow; }
        }

        public override string ReadableName
        {
            get { return "提示箭头"; }
        }

        public override BaseProperty ShapeProperty
        {
            get
            {
                return _property;
            }
            set
            {
                _property = (IndicatorArrowProperty)value;

                // reset the size
                Point p = new Point((int)VertexAnchors[0].X, (int)VertexAnchors[0].Y);
                SetNewPosForHotAnchor(0, p);
            }
        }

        public override void SetEndPoint(Point pt)
        {
            PointF[] pf = ShapeHelper.GetIndicatorArrowPoints(base.StartPoint, pt, _property.LineSize);
            base.SetNewScaledPath(pf, types);
        }

        public override void SetNewPosForHotAnchor(int index, Point newPos)
        {
            PointF[] pf = base.VertexAnchors;
            pf[index] = newPos;

            PointF[] newps = ShapeHelper.GetIndicatorArrowPoints(pf[0], pf[1], _property.LineSize);
            base.SetNewScaledPath(newps, types);
        }

        //protected override void AfterPropertyChanged(BaseProperty oldValue, BaseProperty newValue)
        //{
        //    _property = (IndicatorArrowProperty)newValue;
        //}

        protected override void UpdateVertexAnchors()
        {
            PointF[] pf = Path.PathPoints;
            PointF[] va = new PointF[2];
            va[0] = new PointF((pf[0].X + pf[6].X) / 2, (pf[0].Y + pf[6].Y) / 2);
            va[1] = pf[3];
            VertexAnchors = va;
        }

        public override void Draw(Graphics g)
        {
            SmoothingMode old = g.SmoothingMode;
            g.SmoothingMode = (_property.Antialias) ? SmoothingMode.AntiAlias : SmoothingMode.HighSpeed;
            
            using (SolidBrush sb = new SolidBrush(_property.LineColor))
            {
                g.FillPath(sb, Path);
            }

            g.SmoothingMode = old;
        }

        public override bool ContainsPoint(Point pos)
        {
            return base.Path.IsVisible(pos);
        }
    }
}
