using System;
using System.Drawing;

namespace Gdu.ExtendedPaint
{
    public class FillableProperty : StrokableProperty
    {
        public override ShapePropertyType PropertyType
        {
            get { return ShapePropertyType.FillableProperty; }
        }

        public ShapePaintType PaintType { get; set; }
        public ShapeFillType FillType { get; set; }
        public Color FillColor { get; set; }

        public FillableProperty()
        {
            PaintType = ShapePaintType.Stroke;
            FillType = ShapeFillType.SolidColor;
            FillColor = Color.White;
        }

        public new FillableProperty Clone()
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
    }
}
