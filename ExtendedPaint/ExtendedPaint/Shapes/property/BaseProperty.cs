using System;

namespace Gdu.ExtendedPaint
{
    public abstract class BaseProperty
    {
        public abstract ShapePropertyType PropertyType { get; }
        public bool Antialias { get; set; }

        public BaseProperty()
        {
            Antialias = true;
        }
    }
}
