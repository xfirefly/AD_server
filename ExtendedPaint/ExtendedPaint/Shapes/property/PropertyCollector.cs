using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public class PropertyCollector
    {
        public bool Antialias { get; set; }

        public IndicatorSize IndicatorLineSize { get; set; }

        public float PenWidth { get; set; }
        public Color StrokeColor { get; set; }
        public LineDashType LineDash { get; set; }
        public LineCapType StartLineCap { get; set; }
        public LineCapType EndLineCap { get; set; }
        public PenAlignment PenAlign { get; set; }
        public LineJoin HowLineJoin { get; set; }

        public ShapePaintType PaintType { get; set; }
        public ShapeFillType FillType { get; set; }
        public Color FillColor { get; set; }

        public int RadiusAll { get; set; }

        public PropertyCollector()
        {
            Antialias = true;
            
            IndicatorLineSize = IndicatorSize.Large;

            PenWidth = 1.0f;
            StrokeColor = Color.Black;
            LineDash = LineDashType.Solid;
            StartLineCap = LineCapType.Square;
            EndLineCap = LineCapType.Square;
            PenAlign = PenAlignment.Center;
            HowLineJoin = LineJoin.Round;

            PaintType = ShapePaintType.StrokeAndFill;
            FillType = ShapeFillType.SolidColor;
            FillColor = Color.White;

            RadiusAll = 8;
        }

        public IndicatorArrowProperty GetIndicatorProperty()
        {
            IndicatorArrowProperty iap = new IndicatorArrowProperty();
            iap.Antialias = this.Antialias;
            iap.LineColor = this.StrokeColor;
            iap.LineSize = this.IndicatorLineSize;
            return iap;
        }

        public StrokableProperty GetStrokableProperty()
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

        public FillableProperty GetFillableProperty()
        {
            FillableProperty fp = new FillableProperty();
            fp.Antialias = this.Antialias;

            fp.PenWidth = this.PenWidth;
            fp.StrokeColor = this.StrokeColor;
            fp.LineDash = this.LineDash;
            fp.StartLineCap = this.StartLineCap;
            fp.EndLineCap = this.EndLineCap;
            fp.PenAlign = this.PenAlign;
            fp.HowLineJoin = this.HowLineJoin;

            fp.PaintType = this.PaintType;
            fp.FillType = this.FillType;
            fp.FillColor = this.FillColor;

            return fp;
        }

        public RoundedRectProperty GetRoundedRectProperty()
        {
            RoundedRectProperty rrp = new RoundedRectProperty();

            rrp.Antialias = this.Antialias;

            rrp.PenWidth = this.PenWidth;
            rrp.StrokeColor = this.StrokeColor;
            rrp.LineDash = this.LineDash;
            rrp.StartLineCap = this.StartLineCap;
            rrp.EndLineCap = this.EndLineCap;
            rrp.PenAlign = this.PenAlign;
            rrp.HowLineJoin = this.HowLineJoin;

            rrp.PaintType = this.PaintType;
            rrp.FillType = this.FillType;
            rrp.FillColor = this.FillColor;

            rrp.RadiusAll = this.RadiusAll;
            rrp.RadiusBL = rrp.RadiusBR = rrp.RadiusTR = rrp.RadiusTL = this.RadiusAll;
            return rrp;
        }

        public PropertyCollector Clone()
        {
            PropertyCollector pc = new PropertyCollector();
            pc.Antialias = this.Antialias;
            
            pc.IndicatorLineSize = this.IndicatorLineSize;

            pc.PenWidth = this.PenWidth;
            pc.StrokeColor = this.StrokeColor;
            pc.LineDash = this.LineDash;
            pc.StartLineCap = this.StartLineCap;
            pc.EndLineCap = this.EndLineCap;
            pc.PenAlign = this.PenAlign;
            pc.HowLineJoin = this.HowLineJoin;

            pc.PaintType = this.PaintType;
            pc.FillType = this.FillType;
            pc.FillColor = this.FillColor;

            pc.RadiusAll = this.RadiusAll;

            return pc;
        }
    }
}
