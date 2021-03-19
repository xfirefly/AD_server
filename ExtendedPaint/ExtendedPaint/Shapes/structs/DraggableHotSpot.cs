using System;
using System.Drawing;

namespace Gdu.ExtendedPaint
{
    /// <summary>
    /// 用于表示一个矩形区域，可以是可拖动的线段顶点，虚线选择框上面的小方块（拖动可对形状缩放），
    /// 以及一个旋转形状的区域
    /// </summary>
    public struct DraggableHotSpot
    {
        public Rectangle Rect;
        HotSpotType _type;
        public int AnchorAngle;
        public bool Visible;

        public DraggableHotSpot(HotSpotType type)
        {
            Rect = Rectangle.Empty;
            _type = type;
            AnchorAngle = 0;
            Visible = true;
        }

        public DraggableHotSpot(Rectangle rect, HotSpotType type)
        {
            Rect = rect;
            _type = type;
            AnchorAngle = 0;
            Visible = true;
        }

        public HotSpotType Type { get { return _type; } }
    }
}
