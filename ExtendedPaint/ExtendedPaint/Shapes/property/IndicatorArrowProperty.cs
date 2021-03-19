using System;
using System.Drawing;

namespace Gdu.ExtendedPaint
{
    public class IndicatorArrowProperty : BaseProperty
    {
        public override ShapePropertyType PropertyType
        {
            get { return ShapePropertyType.IndicatorArrowProperty; }
        }

        public Color LineColor { get; set; }
        public IndicatorSize LineSize { get; set; }

        public IndicatorArrowProperty()
        {
            LineColor = Color.Gray;
            LineSize = IndicatorSize.Large;
        }

        public IndicatorArrowProperty Clone()
        {
            IndicatorArrowProperty iap = new IndicatorArrowProperty();
            iap.Antialias = this.Antialias;
            iap.LineColor = this.LineColor;
            iap.LineSize = this.LineSize;
            return iap;
        }
    }
}
