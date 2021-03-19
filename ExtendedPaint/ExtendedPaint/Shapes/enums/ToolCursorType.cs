using System;

namespace Gdu.ExtendedPaint
{
    public enum ToolCursorType
    {
        Default,
        LineTool,
        RectTool,
        EllipseTool,
        HandTool,
        ShapeSelect_Default,
        
        ShapeSelect_Move,
        //ShapeSelect_Rotate,
        ShapeSelect_Scale_0,
        ShapeSelect_Scale_90,
        ShapeSelect_Scale_45,
        ShapeSelect_Scale_135,
        ShapeSelect_DragLineVertex,

        CustomTool        
    }
}
