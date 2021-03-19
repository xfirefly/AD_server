using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gdu.ExtendedPaint
{
    public class EPKernel
    {
        #region private var

        private System.ComponentModel.EventHandlerList _events;
        private Bitmap finalBitmap;
        private Graphics graphics;

        private ToolCursorType cursorType;

        private SelectToolsLocation selectToolInWhere;
        private bool isInSelecting;

        private bool mousePressed;
        private bool isEverMove; // ??
        private List<DrawableBase> shapesList;
        private DrawableBase shapeInCreating;
        private ToolType currentTool;
        private AdItemType currentRectType;

        private List<int> selectedShapesIndexList;
        private Point lastMovePoint;
        private Point mouseDownLocation;

        private int pressedHotSpotIndex;

        private PropertyCollector proCollector;
        private EPCanvas parent;

        #endregion

        public List<DrawableBase> getShapeList()
        {
            return shapesList;
        }

        //画背景图
        public void drawBackImage(Graphics g, string path)
        {
            //string fileName = AppDomain.CurrentDomain.BaseDirectory + "Cropper/tests/castle.jpg");

            Bitmap image = new Bitmap(@"E:\壁纸 UI\1080p\1 (17).jpg");

            g.DrawImage(image, 0, 0, finalBitmap.Size.Width, finalBitmap.Size.Height);
        }
        #region constructors

        public EPKernel()
            :this(new Size(400, 300)){}

        public EPKernel(Size bitmapSize)
            :this(new Bitmap(bitmapSize.Width, bitmapSize.Height))
        {}

        public EPKernel(Size bitmapSize, EPCanvas parent )
            : this(new Bitmap(bitmapSize.Width, bitmapSize.Height))
        {
            this.parent = parent;
        }
  

        public EPKernel(Bitmap bitmap)
        {
            graphics = Graphics.FromImage(bitmap);
            //drawGrid(graphics, bitmap.Width, bitmap.Height);

            finalBitmap = bitmap;
            //graphics = Graphics.FromImage(bitmap);
            
            _events = new System.ComponentModel.EventHandlerList();

            InitialValue();
        }

        public void drawGrid(Graphics g, int Width, int Height)
        {
            //Graphics g = e.Graphics;
            int numOfCells = 10;
            int xcellSize =  Width / 10;
            int ycellSize =  Height / 10;
            Pen p = new Pen(Color.Gray);

            for (int y = 0; y < numOfCells; ++y)
            {
                g.DrawLine(p, 0, y * ycellSize, numOfCells * xcellSize, y * ycellSize);
            }

            for (int x = 0; x < numOfCells; ++x)
            {
                g.DrawLine(p, x * xcellSize, 0, x * xcellSize, numOfCells * xcellSize);
            }
        }

        private void InitialValue()
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            shapesList = new List<DrawableBase>();
            selectedShapesIndexList = new List<int>();
            mousePressed = false;

            proCollector = new PropertyCollector();
        }

        #endregion

        #region inner methods

        public void ClearSelectedShapes()   //???
        {
            ClearSelectedShapes(true);

            
            OnSelectedShapesChanged(EventArgs.Empty);
        }

        private void ClearSelectedShapes(bool fireEvent)
        {
            if (selectedShapesIndexList.Count > 0)
            {
                for (int i = 0; i < selectedShapesIndexList.Count; i++)
                {
                    shapesList[selectedShapesIndexList[i]].IsSelected = false;                    
                }
                selectedShapesIndexList.Clear();
                //lastSelectedShapeIndex = -1;
                if (fireEvent)
                    OnSelectedShapesChanged(EventArgs.Empty);
            }
        }

        private bool SelectThisShape(int shapeIndex)
        {
            return SelectThisShape(shapeIndex, true);
        }

        /// <summary>
        /// if this shape index is legal and not been select, then return true, otherwise false
        /// </summary>        
        private bool SelectThisShape(int shapeIndex, bool fireEvent)
        {
            if (shapeIndex >= 0 && shapeIndex < shapesList.Count)
            {
                if (!selectedShapesIndexList.Contains(shapeIndex))
                {
                    shapesList[shapeIndex].IsSelected = true;
                    selectedShapesIndexList.Add(shapeIndex);
                    if (fireEvent)
                        OnSelectedShapesChanged(EventArgs.Empty);
                    return true;
                }
            }
            return false;
        }

        private void SelectTheseShapes(int[] indices)
        {
            bool ok = false;
            for (int i = 0; i < indices.Length; i++)
            {
                ok = SelectThisShape(indices[i], false);
            }
            if(ok)
                OnSelectedShapesChanged(EventArgs.Empty);
        }

        private int CheckIndexByPosition(Point pos)
        {
            for (int i = shapesList.Count - 1; i >= 0; i--)
            {
                if (shapesList[i].ContainsPoint(pos))
                    return i;
            }
            return -1;
        }

        private int[] CheckIndexByRect(Rectangle rect)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < shapesList.Count; i++)
            {
                if (shapesList[i].AnyPointContainedByRect(rect))
                    list.Add(i);
            }
            return list.ToArray();
        }

        private void SelectTool_MouseDown(Point pos)
        {
            mouseDownLocation = pos;
            bool inHotSpot = false;
            if (SelectedShapesCount == 1)
            {
                int i = selectedShapesIndexList[0];
                DraggableHotSpot[] hs = shapesList[i].DraggableHotSpots;
                for (int j = 0; j < hs.Length; j++)
                {
                    if (hs[j].Visible && hs[j].Rect.Contains(pos))
                    {
                        inHotSpot = true;
                        pressedHotSpotIndex = j;
                        shapesList[i].SetStartTransformPoint(pos);
                        selectToolInWhere = SelectToolsLocation.ShapeDraggableHotSpot;
                        break;
                    }
                }
            }
            if (!inHotSpot)
            {
                int index = CheckIndexByPosition(pos);
                if (index != -1)
                {
                    if (shapesList[index].IsSelected)
                    {
                        selectToolInWhere = SelectToolsLocation.SelectedShape;
                    }
                    else
                    {
                        selectToolInWhere = SelectToolsLocation.UnselectedShape;
                        ClearSelectedShapes(false);
                        SelectThisShape(index);
                        CursorType = ToolCursorType.ShapeSelect_Move;
                    }
                    lastMovePoint = pos;
                }
                else
                {
                    selectToolInWhere = SelectToolsLocation.Blank;
                }
            }
        }

        private void SelectTool_MouseMove(Point pos)
        {
            if (mousePressed)
            {
                switch (selectToolInWhere)
                {
                    case SelectToolsLocation.Blank:
                        lastMovePoint = pos;
                        isInSelecting = true;
                        OnSelectToolInSelecting(EventArgs.Empty);
                        break;
                    case SelectToolsLocation.UnselectedShape:
                    case SelectToolsLocation.SelectedShape:
                        for (int i = 0; i < selectedShapesIndexList.Count; i++)
                        {
                            shapesList[selectedShapesIndexList[i]].Move(
                                pos.X - lastMovePoint.X, pos.Y - lastMovePoint.Y,
                                i == selectedShapesIndexList.Count - 1);

                            Console.WriteLine("Move : {0} pos {1}", i, pos);
                            RectShape rect = shapesList[selectedShapesIndexList[i]] as RectShape;
                            rect.UserSetPos = false;
                        }                        
                        lastMovePoint = pos;
                        break;
                    case SelectToolsLocation.ShapeDraggableHotSpot:
                        shapesList[selectedShapesIndexList[0]].SetNewPosForHotAnchor(
                            pressedHotSpotIndex, pos);
                        Console.WriteLine("drag : {0} pos {1}", pressedHotSpotIndex, pos);

                        RectShape rect2 = shapesList[selectedShapesIndexList[0]] as RectShape;
                        rect2.UserSetPos = false;
                        break;
                }
            }
            else
            {
                ToolCursorType type = ToolCursorType.ShapeSelect_Default;
                if (SelectedShapesCount == 1)
                {
                    int index = selectedShapesIndexList[0];
                    bool inHotSpot = false;
                    DraggableHotSpot[] hs = shapesList[index].DraggableHotSpots;
                    
                    for (int j = 0; j < hs.Length; j++)
                    {
                        if (hs[j].Visible && hs[j].Rect.Contains(pos))
                        {
                            switch (hs[j].Type)
                            {
                                case HotSpotType.LineVertex:
                                    type = ToolCursorType.ShapeSelect_DragLineVertex;
                                    break;
                                //case HotSpotType.RotatingRect:
                                //    type = ToolCursorType.ShapeSelect_Rotate;
                                //    break;
                                case HotSpotType.AnchorToScale:
                                    switch (j)
                                    {
                                        case 0:
                                        case 2:
                                            type = ToolCursorType.ShapeSelect_Scale_135;
                                            break;
                                        case 1:
                                        case 3:
                                            type = ToolCursorType.ShapeSelect_Scale_45;
                                            break;
                                        case 4:
                                        case 6:
                                            type = ToolCursorType.ShapeSelect_Scale_90;
                                            break;
                                        case 5:
                                        case 7:
                                            type = ToolCursorType.ShapeSelect_Scale_0;
                                            break;
                                    }
                                    
                                    //type = ToolCursorType.ShapeSelect_Scale;
                                    break;
                            }
                            inHotSpot = true;
                            break;
                        }
                    }

                    if (!inHotSpot && shapesList[index].ContainsPoint(pos))
                    {
                        type = ToolCursorType.ShapeSelect_Move;
                    }
                }
                else if (SelectedShapesCount > 1)
                {
                    // multiple selected, then shapes can just be moved
                    for (int i = 0; i < selectedShapesIndexList.Count; i++)
                    {
                        if (shapesList[selectedShapesIndexList[i]].ContainsPoint(pos))
                        {
                            type = ToolCursorType.ShapeSelect_Move;
                            break;
                        }
                    }
                }
                CursorType = type;
            }
        }

        private void SelectTool_MouseUp(Point pos)
        {
            Console.WriteLine("SelectTool_MouseUp " + pos);
            if (!isEverMove)
            {
                switch (selectToolInWhere)
                {
                    case SelectToolsLocation.Blank:
                        ClearSelectedShapes();
                        isInSelecting = false;
                        OnSelectToolInSelecting(EventArgs.Empty);
                        break;
                    case SelectToolsLocation.SelectedShape:
                        if (SelectedShapesCount > 0)
                        {
                            ClearSelectedShapes(false);
                            SelectThisShape(CheckIndexByPosition(pos));
                        }
                        break;
                }
            }
            else
            {
                switch (selectToolInWhere)
                {
                    case SelectToolsLocation.Blank:
                        ClearSelectedShapes();
                        int[] rst = CheckIndexByRect(SelectToolSelectingRect);
                        if (rst.Length > 0)
                        {
                            SelectTheseShapes(rst);
                        }
                        isInSelecting = false;
                        OnSelectToolInSelecting(EventArgs.Empty);
                        break;                    
                }
            }
        }

        private void setShapeProperty(ShapePropertyValueType type, object value)
        {
            switch (type)
            {
                case ShapePropertyValueType.Antialias:
                    proCollector.Antialias = (bool)value;
                    break;

                case ShapePropertyValueType.IndicatorSize:
                    proCollector.IndicatorLineSize = (IndicatorSize)value;
                    break;                    

                case ShapePropertyValueType.StrokeWidth:
                    proCollector.PenWidth = (float)value;
                    break;
                case ShapePropertyValueType.StrokeColor:
                    proCollector.StrokeColor = (Color)value;
                    break;
                case ShapePropertyValueType.StartLineCap:
                    proCollector.StartLineCap = (LineCapType)value;
                    break;
                case ShapePropertyValueType.EndLineCap:
                    proCollector.EndLineCap = (LineCapType)value;
                    break;
                case ShapePropertyValueType.LineDash:
                    proCollector.LineDash = (LineDashType)value;
                    break;
                case ShapePropertyValueType.LineJoin:
                    proCollector.HowLineJoin = (LineJoin)value;
                    break;
                case ShapePropertyValueType.PenAlignment:
                    proCollector.PenAlign = (PenAlignment)value;
                    break;

                case ShapePropertyValueType.FillColor:
                    proCollector.FillColor = (Color)value;
                    break;
                case ShapePropertyValueType.FillType:
                    proCollector.FillType = (ShapeFillType)value;
                    break;
                case ShapePropertyValueType.PaintType:
                    proCollector.PaintType = (ShapePaintType)value;
                    break;

                case ShapePropertyValueType.RoundedRadius:
                    proCollector.RadiusAll = (int)value;
                    break;
            }
            applyNewProperty();
        }

        private void applyNewProperty()
        {
            if (SelectedShapesCount < 1)
                return;

            // currently only support one selected shape
            if (SelectedShapesCount == 1)
            {
                DrawableBase shape = shapesList[selectedShapesIndexList[0]];
                switch (shape.ShapeProperty.PropertyType)
                {
                    case ShapePropertyType.IndicatorArrowProperty:
                        shape.ShapeProperty = proCollector.GetIndicatorProperty();
                        break;

                    case ShapePropertyType.StrokableProperty:
                        shape.ShapeProperty = proCollector.GetStrokableProperty();
                        break;

                    case ShapePropertyType.FillableProperty:
                        shape.ShapeProperty = proCollector.GetFillableProperty();
                        break;

                    case ShapePropertyType.RoundedRectProperty:
                        shape.ShapeProperty = proCollector.GetRoundedRectProperty();
                        break;
                }
            }
        }

        private void CheckShapeProperty()
        {
            if (currentTool != ToolType.ShapeSelect || SelectedShapesCount != 1)
                return;

            bool diff = false;
            BaseProperty basepro = shapesList[selectedShapesIndexList[0]].ShapeProperty;

            if (proCollector.Antialias != basepro.Antialias)
            {
                proCollector.Antialias = basepro.Antialias;
                diff = true;
            }

            if(basepro is IndicatorArrowProperty)
            {
                IndicatorArrowProperty ip = (IndicatorArrowProperty)basepro;
                if (ip.LineColor != proCollector.StrokeColor ||
                    ip.LineSize != proCollector.IndicatorLineSize)
                {
                    diff = true;
                    proCollector.StrokeColor=ip.LineColor;
                    proCollector.IndicatorLineSize = ip.LineSize;
                }
            }

            if (basepro is StrokableProperty)
            {
                StrokableProperty sp = (StrokableProperty)basepro;
                if (proCollector.PenWidth != sp.PenWidth ||
                    proCollector.StrokeColor != sp.StrokeColor ||
                    proCollector.LineDash != sp.LineDash ||
                    proCollector.StartLineCap != sp.StartLineCap ||
                    proCollector.EndLineCap != sp.EndLineCap ||
                    proCollector.PenAlign != sp.PenAlign ||
                    proCollector.HowLineJoin != sp.HowLineJoin)
                {
                    diff = true;
                    proCollector.PenWidth = sp.PenWidth;
                    proCollector.StrokeColor = sp.StrokeColor;
                    proCollector.LineDash = sp.LineDash;
                    proCollector.StartLineCap = sp.StartLineCap;
                    proCollector.EndLineCap = sp.EndLineCap;
                    proCollector.PenAlign = sp.PenAlign;
                    proCollector.HowLineJoin = sp.HowLineJoin;
                }
            }

            if (basepro is FillableProperty)
            {
                FillableProperty fp = (FillableProperty)basepro;
                if (proCollector.PaintType != fp.PaintType ||
                    proCollector.FillColor != fp.FillColor ||
                    proCollector.FillType != fp.FillType)
                {
                    diff = true;
                    proCollector.PaintType = fp.PaintType;
                    proCollector.FillColor = fp.FillColor;
                    proCollector.FillType = fp.FillType;
                }
            }

            if (basepro is RoundedRectProperty)
            {
                RoundedRectProperty rp = (RoundedRectProperty)basepro;
                if (proCollector.RadiusAll != rp.RadiusAll)
                {
                    diff = true;
                    proCollector.RadiusAll = rp.RadiusAll;
                }
            }

            if (diff)
            {
                OnPropertyCollectorChanged(EventArgs.Empty);
            }
        }

        #endregion

        #region private properties

        private Rectangle SelectToolSelectingRect
        {
            get
            {
                int x = mouseDownLocation.X;
                int y = mouseDownLocation.Y;
                int w = lastMovePoint.X - x;
                int h = lastMovePoint.Y - y;
                if (w < 0)
                {
                    w = -w;
                    x -= w;
                }
                if (h < 0)
                {
                    h = -h;
                    y -= h;
                }
                return new Rectangle(x, y, w, h);
            }
        }

        #endregion

        #region public properties

        /// <summary>
        /// 获取绘制了所有形状的最终的Bitmap
        /// </summary>
        public Bitmap FinalBitmap
        {
            get { return finalBitmap; }
            set { finalBitmap = value; }
        }

        

        public DrawableBase[] SelectedShapes
        {
            get
            {
                if (selectedShapesIndexList.Count < 1)
                    return new DrawableBase[] { };
                DrawableBase[] shapes = new DrawableBase[selectedShapesIndexList.Count];
                int i = 0;
                foreach (int index in selectedShapesIndexList)
                    shapes[i++] = shapesList[index];                

                return shapes;
            }
        }

        /// <summary>
        /// 返回被选中的形状的个数
        /// </summary>
        public int SelectedShapesCount
        {
            get { return selectedShapesIndexList.Count; }
        }

        /// <summary>
        /// 表示当前需要的鼠标指针类型
        /// </summary>
        public ToolCursorType CursorType
        {
            get { return cursorType; }
            private set
            {
                if (cursorType != value)
                {
                    cursorType = value;
                    OnCursorTypeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 表示选择工具是否处于拖动选择状态
        /// </summary>
        public bool IsInSelecting
        {
            get { return isInSelecting; }
        }

        public PropertyCollector ShapePropertyCollector
        {
            get { return proCollector.Clone(); }
        }

        public string[] ShapesInfoList
        {
            get
            {
                if (shapesList.Count == 0)
                    return new string[] { };

                string[] list = new string[shapesList.Count];
                for (int i = 0; i < shapesList.Count; i++)
                {
                    list[i] = i.ToString() + ": " + shapesList[i].ReadableName;
                }
                return list;
            }
        }

        public AdItemType CurrentRectType
        {
            get
            {
                return currentRectType;
            }

            set
            {
                currentRectType = value;
            }
        }

        public ToolType CurrentTool
        {
            get
            {
                return currentTool;
            }

            set
            {
                currentTool = value;
            }
        }

        #endregion

        #region public methods

        public void RefleshBitmap()
        {  
            RefleshBitmap(Rectangle.Empty);
        }

        public void RefleshBitmap(Rectangle clipRect)
        {
            bool empty = clipRect.IsEmpty;
            if (!empty)
                graphics.SetClip(clipRect);

            graphics.FillRectangle(Brushes.LightGray, new Rectangle(Point.Empty, finalBitmap.Size));
            //drawGrid(graphics, finalBitmap.Width, finalBitmap.Height);
            //drawBackImage(graphics, "");
            //Console.WriteLine("final bitmap " + finalBitmap.Size.ToString());

            for (int i = 0; i < shapesList.Count; i++)
            {
                if (empty || clipRect.IntersectsWith(shapesList[i].RectToReflesh))
                    shapesList[i].Draw(graphics);
            }
            if (shapeInCreating != null && (empty || clipRect.IntersectsWith(shapeInCreating.RectToReflesh)))
                shapeInCreating.Draw(graphics);

            OnFinalBitmapChanged(EventArgs.Empty);

            graphics.ResetClip();
        }

        public void MouseDown(Point pos)
        {
            Console.WriteLine("MouseDown " + pos);
            mousePressed = true;
            isEverMove = false;            

            if(currentTool != ToolType.ShapeSelect)
                ClearSelectedShapes();

            switch (currentTool)
            {
                case ToolType.Line:
                    shapeInCreating = new LineShape(this, proCollector.GetStrokableProperty());
                    shapeInCreating.SetStartPoint(pos);
                    break;
                case ToolType.BrokenLine:
                    shapeInCreating = new BrokenLine(this, proCollector.GetStrokableProperty());
                    shapeInCreating.SetStartPoint(pos);
                    break;
                case ToolType.IndicatorArrow:
                    shapeInCreating = new IndicatorArrow(this, proCollector.GetIndicatorProperty());
                    shapeInCreating.SetStartPoint(pos);
                    break;

                case ToolType.Rectangle:
                    shapeInCreating = new RectShape(this, proCollector.GetFillableProperty(), parent, CurrentRectType);
                    if (BeginShapeCreate != null) { 
                        BeginShapeCreate(shapeInCreating);
                    }

                    shapeInCreating.SetStartPoint(pos);
                    break;
                case ToolType.RoundedRect:
                    shapeInCreating = new RoundedRectShape(this, proCollector.GetRoundedRectProperty());
                    shapeInCreating.SetStartPoint(pos);
                    break;
                case ToolType.Ellipse:
                    shapeInCreating = new EllipseShape(this, proCollector.GetFillableProperty());
                    shapeInCreating.SetStartPoint(pos);
                    break;

                case ToolType.ShapeSelect:
                    SelectTool_MouseDown(pos);                    
                    break;
            }
        }

        public void MouseMove(Point pos)
        {
             
            isEverMove = true;

            switch (currentTool)
            {
                case ToolType.Line:
                case ToolType.BrokenLine:
                case ToolType.IndicatorArrow:
                case ToolType.Rectangle:
                case ToolType.RoundedRect:
                case ToolType.Ellipse:
                    if (!mousePressed)
                        return;
                    if (shapeInCreating != null)
                    {
                        Console.WriteLine("MouseMove " + pos);
                        if (IsPointInsideCanvas(pos))
                        {
                            lastMouseMovePoint = pos;
                            shapeInCreating.SetEndPoint(pos);
                        }
                    }
                    break;

                case ToolType.ShapeSelect:
                    if (IsPointInsideCanvas(pos))
                    {
                        lastMouseMovePoint = pos;
                        SelectTool_MouseMove(pos);
                    }                
                    break;
            }
        }


        private Point lastMouseMovePoint;
        public bool IsPointInsideCanvas(Point pos)
        {
            return pos.X > -1 && pos.Y > -1 && pos.X <= LocationCalc.CANVAS_W && pos.Y <= LocationCalc.CANVAS_H;
        }

        public void MouseUp(Point pos) 
        {
            Console.WriteLine("MouseUp " + pos);
            switch (currentTool)
            {
                case ToolType.Line:
                case ToolType.BrokenLine:
                case ToolType.IndicatorArrow:
                case ToolType.Rectangle:
                case ToolType.RoundedRect:
                case ToolType.Ellipse:
                    if (!isEverMove)
                    {
                        shapeInCreating = null;
                        mousePressed = false;
                        return;
                    }
                    if (shapeInCreating != null)
                    {                        
                        if (shapeInCreating.IsEndPointAcceptable(pos))
                        {
                            pos = lastMouseMovePoint;
                            shapeInCreating.IsInCreating = false;
                            shapeInCreating.SetEndPoint(pos);
                            shapesList.Add(shapeInCreating);
                            OnShapesListChangedChanged(EventArgs.Empty);
                            SelectThisShape(shapesList.Count - 1);           
                            
                            if( EndShapeCreate!= null)
                            {
                                EndShapeCreate(shapeInCreating);
                            }                 
                        }
                        shapeInCreating = null;
                    }
                    break;
                case ToolType.ShapeSelect:
                    pos = lastMouseMovePoint;
                    SelectTool_MouseUp(pos);
                    break;
            }
            mousePressed = false;
        }

        public void SetNewTool(ToolType type)
        {
            currentTool = type;
            switch (type)
            {
                case ToolType.Ellipse:
                    CursorType = ToolCursorType.EllipseTool;
                    break;
                case ToolType.RoundedRect:
                case ToolType.Rectangle:
                    CursorType = ToolCursorType.RectTool;
                    break;
                case ToolType.IndicatorArrow:
                case ToolType.Line:
                case ToolType.BrokenLine:
                    CursorType = ToolCursorType.LineTool;
                    break;
                case ToolType.ShapeSelect:
                    CursorType = ToolCursorType.ShapeSelect_Default;
                    break;
                case ToolType.Hand:
                    CursorType = ToolCursorType.HandTool;
                    break;
                case ToolType.Custom:
                    CursorType = ToolCursorType.Default;
                    break;
            }
        }

        public void SetRect(ToolType type, AdItemType rt)
        {
            currentTool = type;
            CursorType = ToolCursorType.RectTool;
            CurrentRectType = rt;
        }

        /// <summary>
        /// Draw the sizable rects for selected shapes
        /// </summary>
        public void DrawSizableRects(Graphics g)
        {
            if (SelectedShapesCount < 1)
                return;

            bool one = (SelectedShapesCount == 1);

            for (int i = 0; i < selectedShapesIndexList.Count; i++)
            {
                shapesList[selectedShapesIndexList[i]].DrawSelectedRect(g, one);
            }
        }

        /// <summary>
        /// Draw the rect indicating that the select tool is dragging
        /// </summary>
        public void DrawSelectToolSelectingRect(Graphics g)
        {
            using (Pen p = new Pen(Color.Gray))
            {
                p.DashPattern = new float[] { 4.0f, 4.0f };
                g.DrawRectangle(p, SelectToolSelectingRect);
            }
        }

        public void SetNewShapePropertyValue(ShapePropertyValueType type, object value)
        {
            switch (currentTool)
            {
                case ToolType.Line:
                case ToolType.BrokenLine:
                case ToolType.IndicatorArrow:
                case ToolType.Rectangle:
                case ToolType.Ellipse:
                case ToolType.RoundedRect:
                case ToolType.ShapeSelect:
                    setShapeProperty(type, value);
                    break;
            }
        }

        public void SelectAllShapes()
        {
            if (shapesList.Count == 0 || (shapesList.Count == selectedShapesIndexList.Count))
                return;

            selectedShapesIndexList.Clear();
            for (int i = 0; i < shapesList.Count; i++)
            {
                shapesList[i].IsSelected = true;
                selectedShapesIndexList.Add(i);
            }
            OnSelectedShapesChanged(EventArgs.Empty);
        }

        public void DeleteAllShapse()
        {
            if (shapesList.Count == 0)
                return;

            selectedShapesIndexList.Clear();
            foreach (var item in shapesList)
            {
                item.destory();
            }
            shapesList.Clear();
            
            RefleshBitmap();
            OnSelectedShapesChanged(EventArgs.Empty);
            OnShapesListChangedChanged(EventArgs.Empty);
        }

        public void DeleteSelectedShapes()
        {
            if (shapesList.Count == 0 || selectedShapesIndexList.Count == 0)
                return;
            DrawableBase[] shapes = new DrawableBase[selectedShapesIndexList.Count];
            for (int i = 0; i < selectedShapesIndexList.Count; i++)
                shapes[i] = shapesList[selectedShapesIndexList[i]];
            foreach (DrawableBase b in shapes)
            {
                b.destory();
                shapesList.Remove(b);
            }
                

            selectedShapesIndexList.Clear();
            RefleshBitmap();
            OnSelectedShapesChanged(EventArgs.Empty);
            OnShapesListChangedChanged(EventArgs.Empty);
        }

        // tmp func
        public void GetTransparentBitmap(Bitmap bm)
        {
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < shapesList.Count; i++)
                {                    
                    shapesList[i].Draw(g);
                }
            }
        }

        #endregion

        #region events

        // FinalBitmapChanged
        private static readonly object Event_FinalBitmapChanged = new object();
        public event EventHandler FinalBitmapChanged
        {
            add { _events.AddHandler(Event_FinalBitmapChanged, value); }
            remove { _events.RemoveHandler(Event_FinalBitmapChanged, value); }
        }
        protected virtual void OnFinalBitmapChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)_events[Event_FinalBitmapChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // SelectedObjectsChanged
        private static readonly object Event_SelectedShapesChanged = new object();
        public event EventHandler SelectedShapesChanged
        {
            add { _events.AddHandler(Event_SelectedShapesChanged, value); }
            remove { _events.RemoveHandler(Event_SelectedShapesChanged, value); }
        }
        protected virtual void OnSelectedShapesChanged(EventArgs e)
        {
            CheckShapeProperty();
            EventHandler handler = (EventHandler)_events[Event_SelectedShapesChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // CursorTypeChanged
        private static readonly object Event_CursorTypeChanged = new object();
        public event EventHandler CursorTypeChanged
        {
            add { _events.AddHandler(Event_CursorTypeChanged, value); }
            remove { _events.RemoveHandler(Event_CursorTypeChanged, value); }
        }
        protected virtual void OnCursorTypeChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)_events[Event_CursorTypeChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // SelectToolInSelecting
        private static readonly object Event_SelectToolInSelecting = new object();
        public event EventHandler SelectToolInSelecting
        {
            add { _events.AddHandler(Event_SelectToolInSelecting, value); }
            remove { _events.RemoveHandler(Event_SelectToolInSelecting, value); }
        }
        protected virtual void OnSelectToolInSelecting(EventArgs e)
        {
            EventHandler handler = (EventHandler)_events[Event_SelectToolInSelecting];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // PropertyCollectorChanged
        private static readonly object Event_PropertyCollectorChanged = new object();
        public event EventHandler PropertyCollectorChanged
        {
            add { _events.AddHandler(Event_PropertyCollectorChanged, value); }
            remove { _events.RemoveHandler(Event_PropertyCollectorChanged, value); }
        }
        protected virtual void OnPropertyCollectorChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)_events[Event_PropertyCollectorChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // ShapesListChanged
        private static readonly object Event_ShapesListChangedChanged = new object();
        public event EventHandler ShapesListChangedChanged
        {
            add { _events.AddHandler(Event_ShapesListChangedChanged, value); }
            remove { _events.RemoveHandler(Event_ShapesListChangedChanged, value); }
        }
        protected virtual void OnShapesListChangedChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)_events[Event_ShapesListChangedChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // 声明和定义委托
        public delegate void BeginShapeCreateHandler(DrawableBase db);
        public event BeginShapeCreateHandler BeginShapeCreate;

        public delegate void EndShapeCreateHandler(DrawableBase db);
        public event EndShapeCreateHandler EndShapeCreate;
        #endregion




        #region inner class

        private enum SelectToolsLocation
        {
            Blank,
            UnselectedShape,
            SelectedShape,
            ShapeDraggableHotSpot
        }

        #endregion
    }
}
