using System;

namespace Gdu.ExtendedPaint
{
    public class NotDrawableProperty : BaseProperty
    {
        public override ShapePropertyType PropertyType
        {
            get { return ShapePropertyType.NotDrawable; }
        }
    }
}
