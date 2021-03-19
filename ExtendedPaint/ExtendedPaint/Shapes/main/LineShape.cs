using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class LineShape : StrokableShape
    {
        private DraggableHotSpot[] _hotspots;

        public LineShape(EPKernel container, StrokableProperty property)
            :base(container, property)
        {
            _hotspots = new DraggableHotSpot[]{
                new DraggableHotSpot(HotSpotType.LineVertex),
                new DraggableHotSpot(HotSpotType.LineVertex)};
        }

        public override ToolType Type
        {
            get { return ToolType.Line; }
        }

        public override string ReadableName
        {
            get { return "普通直线"; }
        }

        public override void SetEndPoint(Point pt)
        {
            base.BeforePathTransforming();
            base.Path.Reset();
            base.Path.AddLine(base.StartPoint, pt);
            AfterPathTransformed(PathTransformType.Scale, true);
        }

        public override void SetNewPosForHotAnchor(int index, Point newPos)
        {
            PointF[] pf = base.Path.PathPoints;
            if (index == 0)
                pf[0] = newPos;
            else
                pf[1] = newPos;

            base.BeforePathTransforming();
            base.Path.Reset();
            base.Path.AddLine(pf[0], pf[1]);
            AfterPathTransformed(PathTransformType.Scale, true);
        }

        public override Rectangle RectToReflesh
        {
            get
            {
                //Rectangle rect = base.RectToReflesh;
                //rect.X -= 12;
                //rect.Y -= 12;
                //rect.Width += 24;
                //rect.Height += 24;
                //return rect;
                return base.RectToReflesh;
            }
        }

        private PointF[] VerticesForSelectedRect
        {
            get
            {
                PointF[] pf = VertexAnchors;
                double angle = Math.Atan2(pf[1].Y - pf[0].Y, pf[1].X - pf[0].X);
                float r = EPConst.Pen_Hit_Width / 2;
                float x1 = (float)(pf[1].X + r * Math.Cos(angle - Math.PI / 2));
                float y1 = (float)(pf[1].Y + r * Math.Sin(angle - Math.PI / 2));
                float x2 = (float)(pf[1].X + r * Math.Cos(angle + Math.PI / 2));
                float y2 = (float)(pf[1].Y + r * Math.Sin(angle + Math.PI / 2));
                float dx = pf[1].X - pf[0].X;
                float dy = pf[1].Y - pf[0].Y;
                float x3 = x1 - dx;
                float y3 = y1 - dy;
                float x4 = x2 - dx;
                float y4 = y2 - dy;

                return new PointF[] { new PointF(x1,y1), new PointF(x2,y2), 
                    new PointF(x4, y4), new PointF(x3, y3)};
            }
        }

        protected PointF[] VertexAnchors { get; set; }       

        protected virtual void UpdateVertexAnchors()
        {
            VertexAnchors = base.Path.PathPoints;
        }

        protected override void AfterPathTransformed(PathTransformType transformType, bool refleshPath)
        {
            if (!IsInCreating)
                UpdateVertexAnchors();
            base.AfterPathTransformed(transformType, refleshPath);
        }

        protected override void RecalculateDraggableHotSpots()
        {
            PointF[] pf = VertexAnchors;
            int hw = EPConst.Line_Vertex_Half_Width;
            int w = hw * 2;
            _hotspots[0].Rect = new Rectangle((int)(pf[0].X - hw), (int)(pf[0].Y - hw), w, w);
            _hotspots[1].Rect = new Rectangle((int)(pf[1].X - hw), (int)(pf[1].Y - hw), w, w);
        }

        public override DraggableHotSpot[] DraggableHotSpots
        {
            get { return _hotspots; }
        }

        public override void DrawSelectedRect(Graphics g, bool withAnchors)
        {
            PointF[] vertices = VerticesForSelectedRect;
            using (Pen p = new Pen(Color.Gray))
            {
                p.DashPattern = new float[] { 4.0F, 4.0F, 4.0F, 4.0F };
                g.DrawLines(p, vertices);
                g.DrawLine(p, vertices[3], vertices[0]);
            }

            if (!withAnchors)
                return;

            PointF[] pf = new PointF[8];
            pf[0] = vertices[0];
            pf[1] = vertices[1];
            pf[2] = vertices[2];
            pf[3] = vertices[3];
            pf[4] = new PointF((vertices[0].X + vertices[1].X) / 2, (vertices[0].Y + vertices[1].Y) / 2);
            pf[5] = new PointF((vertices[1].X + vertices[2].X) / 2, (vertices[1].Y + vertices[2].Y) / 2);
            pf[6] = new PointF((vertices[2].X + vertices[3].X) / 2, (vertices[2].Y + vertices[3].Y) / 2);
            pf[7] = new PointF((vertices[3].X + vertices[0].X) / 2, (vertices[3].Y + vertices[0].Y) / 2);

            SmoothingMode old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = EPConst.Line_Vertex_Half_Width;
            int w2 = w * 2;
            g.FillEllipse(Brushes.White, pf[4].X - w, pf[4].Y - w, w2, w2);
            g.DrawEllipse(Pens.Black, pf[4].X - w, pf[4].Y - w, w2, w2);
            g.FillEllipse(Brushes.White, pf[6].X - w, pf[6].Y - w, w2, w2);
            g.DrawEllipse(Pens.Black, pf[6].X - w, pf[6].Y - w, w2, w2);

            g.SmoothingMode = old;
        }

        /// <summary>
        /// 重写基类的方法，由于直线是线段，其上的点是否被矩形包含需要特殊处理
        /// </summary>        
        public override bool AnyPointContainedByRect(Rectangle rect)
        {
            PointF[] pf = VertexAnchors;
            Point linePt1 = new Point((int)pf[0].X, (int)pf[0].Y);
            Point linePt2 = new Point((int)pf[1].X, (int)pf[1].Y);
            return GeometricHelper.RectContainsPointOnLine(rect, linePt1, linePt2);
        }

        public override bool ContainsPoint(Point pos)
        {
            return base.Path.IsOutlineVisible(pos, EPConst.Pen_Hit_Detect);
        }
    }
}
