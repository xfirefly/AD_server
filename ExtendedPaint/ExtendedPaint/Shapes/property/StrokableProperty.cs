using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class StrokableProperty : BaseProperty
    {
        public override ShapePropertyType PropertyType
        {
            get { return ShapePropertyType.StrokableProperty; }
        }

        public float PenWidth { get; set; }
        public Color StrokeColor { get; set; }
        public LineDashType LineDash { get; set; }
        public LineCapType StartLineCap { get; set; }
        public LineCapType EndLineCap { get; set; }
        public PenAlignment PenAlign { get; set; }
        public LineJoin HowLineJoin { get; set; }

        public StrokableProperty()
        {
            PenWidth = 1.0f;
            StrokeColor = Color.Black;
            LineDash = LineDashType.Solid;
            StartLineCap = LineCapType.Square;
            EndLineCap = LineCapType.Square;
            PenAlign = PenAlignment.Center;
            HowLineJoin = LineJoin.Round;
        }

        public StrokableProperty Clone()
        {
            StrokableProperty sp = new StrokableProperty();
            sp.Antialias = this.Antialias;
            sp.PenWidth = this.PenWidth;
            sp.StrokeColor = this.StrokeColor;
            sp.LineDash = this.LineDash;
            sp.StartLineCap = this.StartLineCap;
            sp.EndLineCap = this.EndLineCap;
            sp.PenAlign = this.PenAlign;
            sp.HowLineJoin = this.HowLineJoin;
            return sp;
        }
    }
}
