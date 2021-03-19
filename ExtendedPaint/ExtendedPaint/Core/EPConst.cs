using System;
using System.Drawing;

namespace Gdu.ExtendedPaint
{
    public class EPConst
    {        
        public static readonly Pen Pen_Hit_Detect;
        public const int Pen_Hit_Width = 14;
        public const int Acceptable_Min_Move_Distance = 4;
        public const int Anchor_Rect_Half_Width = 3;
        public const int Line_Vertex_Half_Width = 5;
        public const int RotatingRect_Offset = 60;
        public const int RotatingRect_Half_Width = 12;
        public const int CrossSign_Half_Width = 2;
        public const float MagicBezier = 0.55f;
        public const int Max_Stroke_Width = 120;

        static EPConst()
        {
            Pen_Hit_Detect = new Pen(Color.Black, Pen_Hit_Width);
        }
    }
}
