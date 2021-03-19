using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gdu.ExtendedPaint
{
    public abstract class DrawableBase : IDisposable
    {
        #region private var

        /// <summary>
        /// 表示该元素所依附的EPKernel类，该类负责把元素绘制到自己的Bitmap上
        /// </summary>
        private EPKernel _container;

        private GraphicsPath path;
        private Matrix matrix;
        private Point _startPoint;
        private bool is_increating;
        private bool is_selected;
        private bool is_locked;
        private bool is_visible;
        private Rectangle _preRefleshBounds;

        private BaseProperty _shapeProperty;

        #endregion

        #region constructors

        public DrawableBase(EPKernel container, BaseProperty property) 
        {
            path = new GraphicsPath();
            matrix = new Matrix();
            _container = container;
            _shapeProperty = property;

            is_visible = true;
            is_increating = true;
        }

        #endregion

        #region protected properties

        /// <summary>
        /// 用于表示在元素变化前的范围边框。与元素变化后的范围边框进行Union，就可以确定
        /// 这次元素变化引发的需要刷新的视图区域了。该变量应该在path变化前，被赋予
        /// path.GetBounds()值，且该值应该在元素绘制后失效，可将其设置为empty
        /// </summary>
        protected Rectangle PreRefleshBounds
        {
            get { return _preRefleshBounds; }
            set { _preRefleshBounds = value; }
        }

        protected Point StartPoint
        {
            get { return _startPoint; }
        }

        protected GraphicsPath Path
        {
            get { return path; }
        }

        protected Point LastTransformPoint { get; set; }

        protected PointF CenterPoint { get; set; }
        protected PointF RotateLocation { get; set; }

        #endregion

        #region protected methods

        protected void SetNewScaledPath(PointF[] pf)
        {
            byte[] type = path.PathTypes;
            BeforePathTransforming();
            path.Dispose();
            path = new GraphicsPath(pf, type);
            AfterPathTransformed(PathTransformType.Scale, true);
        }

        protected void SetNewScaledPath(PointF[] pf, byte[] types)
        {            
            BeforePathTransforming();
            path.Dispose();
            path = new GraphicsPath(pf, types);
            AfterPathTransformed(PathTransformType.Scale, true);
        }

        protected void Rotate(Point newPos)
        {
            float a1 = (float)(Math.Atan2(LastTransformPoint.Y - CenterPoint.Y,
                    LastTransformPoint.X - CenterPoint.X) * 180 / Math.PI);
            float a2 = (float)(Math.Atan2(newPos.Y - CenterPoint.Y,
                newPos.X - CenterPoint.X) * 180 / Math.PI);
            if (a1 < 0)
                a1 += 360;
            if (a2 < 0)
                a2 += 360;
            matrix.Reset();
            matrix.RotateAt(a2 - a1, CenterPoint);
            BeforePathTransforming();
            path.Transform(matrix);
            AfterPathTransformed(PathTransformType.Rotate, true);
        }

        protected void BeforePathTransforming()
        {
            _preRefleshBounds = RectToReflesh;
        }

        protected virtual void AfterPathTransformed(PathTransformType transformType, bool refleshPath)
        {
            if (refleshPath)
                RefleshContainer();

            if (!IsInCreating)
                RecalculateDraggableHotSpots();
        }

        /// <summary>
        /// 该方法必须在path变化之后(形状正在创建的除外)调用，以便及时刷新 hotspots 区域
        /// </summary>
        protected virtual void RecalculateDraggableHotSpots()
        {

        }

        protected virtual void AfterPropertyChanged(BaseProperty oldValue, BaseProperty newValue)
        {

        }

        private void RefleshContainer()
        {
            //暂时关闭 EPKernel 的局部刷新功能
            _container.RefleshBitmap();
            //_container.RefleshBitmap(Rectangle.Union(PreRefleshBounds, RectToReflesh));
        }

        #endregion

        #region public properties

        public abstract ToolType Type { get; }
        public abstract string ReadableName { get; }
        public abstract DraggableHotSpot[] DraggableHotSpots { get; }

        public bool IsInCreating
        {
            get { return is_increating; }
            set { is_increating = value; }
        }

        public bool IsSelected
        {
            get { return is_selected; }
            set { is_selected = value; }
        }

        public bool IsLocked
        {
            get { return is_locked; }
            set { is_locked = value; }
        }

        public bool IsVisible
        {
            get { return is_visible; }
            set { is_visible = value; }
        }        

        /// <summary>
        /// 这个矩形用于元素刷新操作。注意具体的元素其矩形是不相同的，比如当直线
        /// 带有箭头时，椭圆画线很粗时，这个矩形就要大于path.GetBounds()
        /// </summary>
        public virtual Rectangle RectToReflesh
        {
            get
            {
                RectangleF rf = Path.GetBounds();
                int x = (int)rf.Left - 12;
                int y = (int)rf.Top - 12;
                int w = (int)rf.Width + 24;
                int h = (int)rf.Height + 24;
                return new Rectangle(x, y, w, h);
            }
        }

        public virtual BaseProperty ShapeProperty 
        {
            get { return _shapeProperty; }
            set
            {
                BaseProperty old = _shapeProperty;
                _shapeProperty = value;
                AfterPropertyChanged(old, value);
                
                RefleshContainer();
            }
        }

        #endregion
        
        #region public methods

        public abstract void SetEndPoint(Point pt);
        public abstract void Draw(Graphics g);
        public abstract void SetNewPosForHotAnchor(int index, Point newPos);

        public virtual void SetStartPoint(Point pt)
        {
            _startPoint = pt;
        }

        public void SetStartTransformPoint(Point pt)
        {
            LastTransformPoint = pt;
        }

        public void DrawSelectedRect(Graphics g)
        {
            DrawSelectedRect(g, true);
        }

        public virtual void DrawSelectedRect(Graphics g, bool withAnchors)
        {
            
        }

        /// <summary>
        /// 判断形状是否有任意点被给定的矩形包含
        /// </summary>
        public virtual bool AnyPointContainedByRect(Rectangle rect)
        {
            return path.GetBounds().IntersectsWith(rect);
        }

        public virtual bool ContainsPoint(Point pos)
        {
            return path.IsVisible(pos);
        }

        public virtual bool IsEndPointAcceptable(Point endPoint)
        {
            int deltax = Math.Abs(endPoint.X - _startPoint.X);
            int deltay = Math.Abs(endPoint.Y - _startPoint.Y);
            int d = EPConst.Acceptable_Min_Move_Distance;
            return (deltax >= d || deltay >= d);
        }

        public void Move(int offsetx, int offsety)
        {
            Move(offsetx, offsety, true);
        }

        public void Move(int offsetx, int offsety, bool reflesh)
        {
            matrix.Reset();
            matrix.Translate(offsetx, offsety);
            BeforePathTransforming();
            path.Transform(matrix);
            AfterPathTransformed(PathTransformType.Move,reflesh);
        }
 

        #endregion

        #region IDisposable interface

        public virtual void Dispose()
        {
            if (path != null)
                path.Dispose();
            if (matrix != null)
                matrix.Dispose();
        }

        public virtual void destory()
        {

        }

        #endregion
    }
}
