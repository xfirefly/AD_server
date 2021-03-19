using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class BrokenLine : StrokableShape
    {
        private DraggableHotSpot[] _hotspots;

        public BrokenLine(EPKernel container, StrokableProperty pro)
            :base(container, pro)
        {
            _hotspots = new DraggableHotSpot[4];
            for (int i = 0; i < 4; i++)
                _hotspots[i] = new DraggableHotSpot(HotSpotType.LineVertex);
        }

        public override ToolType Type
        {
            get { return ToolType.BrokenLine; }
        }

        public override string ReadableName
        {
            get { return "折线"; }
        }

        public override void SetEndPoint(Point pt)
        {
            if (IsInCreating)
            {
                base.BeforePathTransforming();
                base.Path.Reset();
                base.Path.AddLine(base.StartPoint, pt);
                AfterPathTransformed(PathTransformType.Scale, true);
            }
            else
            {
                PointF[] pf = ShapeHelper.GetBrokenLinePoints(StartPoint, pt);
                base.SetNewScaledPath(pf, new byte[] { 0, 1, 1, 1 });
            }
        }

        public override void SetNewPosForHotAnchor(int index, Point newPos)
        {
            PointF[] pf = base.Path.PathPoints;
            pf[index] = newPos;
            base.SetNewScaledPath(pf, new byte[] { 0, 1, 1, 1 });
        }

        protected override void RecalculateDraggableHotSpots()
        {
            PointF[] pf = Path.PathPoints;
            int hw = EPConst.Anchor_Rect_Half_Width;
            int w = hw * 2;
            for (int i = 0; i < _hotspots.Length; i++)
                _hotspots[i].Rect = new Rectangle((int)(pf[i].X - hw), (int)(pf[i].Y - hw), w, w);
        }

        public override DraggableHotSpot[] DraggableHotSpots
        {
            get { return _hotspots; }
        }        

        public override void DrawSelectedRect(Graphics g, bool withAnchors)
        {
            SmoothingMode old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            for (int i = 0; i < _hotspots.Length; i++)
            {
                g.FillRectangle(Brushes.White, _hotspots[i].Rect);
                g.DrawRectangle(Pens.Black, _hotspots[i].Rect);
            }
            g.SmoothingMode = old;
        }
        
        public override bool AnyPointContainedByRect(Rectangle rect)
        {
            PointF[] pf = Path.PathPoints;
            for (int i = 0; i < pf.Length - 1; i++)
            {
                Point linePt1 = new Point((int)pf[i].X, (int)pf[i].Y);
                Point linePt2 = new Point((int)pf[i + 1].X, (int)pf[i + 1].Y);
                if (GeometricHelper.RectContainsPointOnLine(rect, linePt1, linePt2))
                    return true;
            }
            return false;
        }

        public override bool ContainsPoint(Point pos)
        {
            return base.Path.IsOutlineVisible(pos, EPConst.Pen_Hit_Detect);
        }
    }
}
