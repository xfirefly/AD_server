using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public abstract class FillableShape : StrokableShape
    {
        private Brush fill_brush;
        private FillableProperty _pro;

        public FillableShape(EPKernel container, FillableProperty property)
            :base(container, property)
        {
            _pro = property;
            SetNewBrush();
        }

        private void SetNewBrush()
        {
            if (fill_brush != null)
                fill_brush.Dispose();

            fill_brush = new SolidBrush(_pro.FillColor);
        }

        protected override void AfterPropertyChanged(BaseProperty oldValue, BaseProperty newValue)
        {            
            base.AfterPropertyChanged(oldValue, newValue);
            _pro = (FillableProperty)newValue;
            SetNewBrush();
        }

        /// <summary>
        /// 给后面的子类填充用
        /// </summary>
        protected Brush FillBrush
        {
            get { return fill_brush; }
        }

        public override void Draw(Graphics g)
        {
            SmoothingMode old = g.SmoothingMode;
            g.SmoothingMode = (ShapeProperty.Antialias) ? SmoothingMode.AntiAlias : SmoothingMode.HighSpeed;
            
            if (_pro.PaintType == ShapePaintType.Fill || _pro.PaintType == ShapePaintType.StrokeAndFill)
                g.FillPath(FillBrush, base.Path);

            if (_pro.PaintType == ShapePaintType.Stroke || _pro.PaintType == ShapePaintType.StrokeAndFill)
                base.Draw(g);

            g.SmoothingMode = old;
        }

        #region IDisposable interface

        public override void Dispose()
        {            
            if (fill_brush != null)
                fill_brush.Dispose();

            base.Dispose();
        }

        


        #endregion
    }
}
