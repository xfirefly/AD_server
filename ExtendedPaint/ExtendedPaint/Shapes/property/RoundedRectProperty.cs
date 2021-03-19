using System;

namespace Gdu.ExtendedPaint
{
    public class RoundedRectProperty : FillableProperty
    {
        public override ShapePropertyType PropertyType
        {
            get { return ShapePropertyType.RoundedRectProperty; }
        }

        public int RadiusTL { get; set; }
        public int RadiusTR { get; set; }
        public int RadiusBR { get; set; }
        public int RadiusBL { get; set; }
        public int RadiusAll { get; set; }

        public RoundedRectProperty()
        {
            RadiusAll = 8;
            RadiusTL = RadiusTR = RadiusBR = RadiusBL = 8;
        }

        public new RoundedRectProperty Clone()
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
            rrp.RadiusTL = this.RadiusTL;
            rrp.RadiusTR = this.RadiusTR;
            rrp.RadiusBR = this.RadiusBR;
            rrp.RadiusBL = this.RadiusBL;

            return rrp;
        }
    }
}
