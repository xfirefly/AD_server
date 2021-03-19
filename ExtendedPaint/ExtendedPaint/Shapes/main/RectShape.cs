using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Gdu.ExtendedPaint
{
    public class RectShape : FillableShape
    {
        private Rectangle rect;

        private DraggableHotSpot[] _hotspots;
        private const int HotspotCount = 9; //9

        // 矩形为 Picture 时使用
        private Bitmap _bitmap;

        // 矩形为 subtitle  时使用
        public MarqueeLabel Mlabel;

        // 记录需要保存的属性
        public ItemBase Prop;

        private EPCanvas parent;
        //public LocationChgEventArgs LastLocation;

        public RectShape(EPKernel container, FillableProperty pro)
                : base(container, pro)
        {
            initialProperties();
            initialHotspotRects();
        }

        public RectShape(EPKernel container, FillableProperty pro, EPCanvas p, AdItemType rt)
                : this(container, pro)
        {
            parent = p;
            ItemType = rt;

            switch (ItemType)
            {
                case AdItemType.Video:
                    Prop = new Video();
                    break;

                case AdItemType.Picture:
                    Prop = new Picture();
                    break;

                case AdItemType.Subtitle:
                    Prop = new Subtitle();
                    break;

                default:
                    break;
            }
        }

        #region video 
        public void setVideo(List<string> filelist)
        {
            Video v = Prop as Video;
            v.filelist.AddRange(filelist); ;

        }

        public void setVideoIntval(int i)
        {
            Video v = Prop as Video;
            v.intval = i;

        }
        #endregion

        #region image  
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="path"></param>
        public void displayImage(string path)
        {
            //Picture pic = Prop as Picture;
            //pic.filename = path;
            try
            {
                _bitmap = new Bitmap(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("StackTrace = " + e.StackTrace);
            }
        }

        public void setImage(List<string> filelist)
        {
            Picture pic = Prop as Picture;
            pic.filelist.AddRange(filelist); ;

        }

        public void setImageIntval(int i)
        {
            Picture pic = Prop as Picture;
            pic.intval = i;

        }

        #endregion

        // 从构造函数移动到这里, 优化性能
        public void CreateEnd()
        {
            if (IsInCreating)
            {
                return;
            }

            switch (ItemType)
            {
                case AdItemType.Video:

                    break;

                case AdItemType.Picture:
                    break;

                case AdItemType.Subtitle:
                    Subtitle sub = Prop as Subtitle;
                    Mlabel = new MarqueeLabel(parent);
                    Mlabel.Name = "subtitle_";
                    Mlabel.AutoSize = false;
                    Mlabel.Width = (int)Path.GetBounds().Width;
                    Mlabel.Height = (int)Path.GetBounds().Height;
                    Mlabel.Location = new Point((int)(Path.GetBounds().Location.X), (int)(Path.GetBounds().Location.Y));
                    //Mlabel.MaximumSize = new Size(1900, 0);

                    setTextProperty(TextProperty.FontName, sub.fontname);
                    setTextProperty(TextProperty.FontSize, sub.fontsize);
                    setTextProperty(TextProperty.Bold, sub.bold);
                    setTextProperty(TextProperty.Italic, sub.italic);
                    setTextProperty(TextProperty.Underline, sub.underline);

                    setTextProperty(TextProperty.Direction, sub.direction);
                    setTextProperty(TextProperty.Speed, sub.speed);
                    setTextProperty(TextProperty.Text, sub.text);
                    setTextProperty(TextProperty.FontColor, ColorEx.FromHtml(sub.fontcolor));
                    setTextProperty(TextProperty.BackColor, ColorEx.FromHtml(sub.backcolor));

                    parent.Controls.Add(Mlabel); // todo , remove func
                    break;

                case AdItemType.Select:
                    break;

                default:
                    break;
            }
        }


        Direction lastDirection = Direction.ToLeft;

        public MarqueeLabel setTextProperty(TextProperty attr, object o)
        {
            Subtitle sub = Prop as Subtitle;
            Console.WriteLine("set " + attr.ToString() + " => " + o);
            Font f = Mlabel.Font;
            switch (attr)
            {
                case TextProperty.FontName:
                    sub.fontname = (string)o;

                    //System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
                    //string fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ttf", sub.fontname);
                    //Console.WriteLine("font ==> " + fn );
                    //privateFonts.AddFontFile(fn );
                    //System.Drawing.Font font = new Font(privateFonts.Families[0], 12);

                    Mlabel.Font = new Font(FontEx.getFontFamily(sub.fontname), f.Size, f.Style);
                    break;

                case TextProperty.FontSize:
                    sub.fontsize = (int)(o);
                    Mlabel.Font = new Font(f.Name, sub.fontsize / 2, f.Style | FontStyle.Regular);
                    break;

                case TextProperty.Bold:
                    sub.bold = (bool)o;
                    if (sub.bold)
                        Mlabel.Font = new Font(f.FontFamily, f.Size, f.Style | FontStyle.Bold);
                    else
                        Mlabel.Font = new Font(f.FontFamily, f.Size, f.Style & ~FontStyle.Bold);
                    break;

                case TextProperty.Italic:
                    sub.italic = (bool)o;
                    if (sub.italic)
                        Mlabel.Font = new Font(f.FontFamily, f.Size, f.Style | FontStyle.Italic);
                    else
                        Mlabel.Font = new Font(f.FontFamily, f.Size, f.Style & ~FontStyle.Italic);
                    break;

                case TextProperty.Underline:
                    sub.underline = (bool)o;
                    if (sub.underline)
                        Mlabel.Font = new Font(f.FontFamily, f.Size, f.Style | FontStyle.Underline);
                    else
                        Mlabel.Font = new Font(f.FontFamily, f.Size, f.Style & ~FontStyle.Underline);
                    break;

                case TextProperty.Direction:
                    lastDirection = sub.direction = (Direction)o;
                    Mlabel.direction = sub.direction;
                    break;

                case TextProperty.Speed:
                    sub.speed = (int)o;
                    Mlabel.Speed = sub.speed;
                    if (sub.speed == 0)
                    {
                        sub.direction = Direction.Static;
                        Mlabel.direction = sub.direction;
                    }
                    else
                    {
                        setTextProperty(TextProperty.Direction, lastDirection);
                    }
                    break;

                case TextProperty.Text:
                    sub.text = (string)o;
                    Mlabel.Text = sub.text;
                    break;

                case TextProperty.FontColor:
                    Mlabel.ForeColor = (Color)o;
                    sub.fontcolor = ColorEx.ColorToHex(Mlabel.ForeColor);
                    break;

                case TextProperty.BackColor:
                    Mlabel.BackColor = (Color)o;
                    sub.backcolor = ColorEx.ColorToHex(Mlabel.BackColor);
                    sub.transparent = false;
                    if (Mlabel.BackColor == Color.Transparent)
                    {
                        sub.transparent = true;
                        sub.backcolor = "#null";
                    }
                    break;

                default:
                    break;
            }

            //sub.BackColor = ColorTranslator.FromHtml(txtAttr.Bgcolor);
            //sub.ForeColor = ColorTranslator.FromHtml(txtAttr.Color);
            //mlabel.BackColor = Color.Transparent;
            // mlabel.Text = "谁的手机发斯里兰卡哗啦哗啦就好了";
            //sub.Direction = txtAttr.Direction;
            //sub.Speed = txtAttr.Speed;
            //int x = ToFormX(txtAttr.X);
            //int y = ToFormY(txtAttr.Y);
            //sub.Location = new Point(x, y);
            // mlabel.Width = (int)base.Path.GetBounds().Width;
            // mlabel.Height = (int)base.Path.GetBounds().Height;
            //sub.X = txtAttr.X;
            //sub.Y = txtAttr.Y;
            //sub.W = txtAttr.W;
            //sub.H = txtAttr.H;
            //sub.fontname = txtAttr.Fontname;
            //sub.fontsize = txtAttr.Fontsize;
            //sub.transparent = txtAttr.Transparent;
            //sub.Font = new Font("宋体", float.Parse(txtAttr.Fontsize), FontStyle.Regular);
            // mlabel.Location = new Point((int)(base.Path.GetBounds().Location.X), (int)(base.Path.GetBounds().Location.Y));

            return Mlabel;
        }


        //删除 MarqueeLabel 等资源
        public override void destory()
        {
            base.destory();

            if (Mlabel != null)
            {
                Mlabel.destory();
                parent.Controls.Remove(Mlabel);
            }

            if (_bitmap != null)
            {
                _bitmap.Dispose();
            }

            Console.WriteLine("destory  ");
        }

        //保存按 1920*1080 计算的屏幕坐标
        //public class LocationChgEventArgs : EventArgs // 事件参数类
        //{
        //    //public int x;
        //    //public int y;
        //    //public int w;
        //    //public int h;
        //    public ItemBase item;
        //}
        // 声明委托
        public delegate void LocationChgEventHandler(object sender, ItemBase item);

        // 定义事件
        public event LocationChgEventHandler LocationChanged;

        /// <summary>
        /// 用户设定坐标后,  无需转换得到屏幕坐标值
        /// </summary>
        public bool UserSetPos { get; set; }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            //Console.WriteLine(parent.Location.ToString());
            //Console.WriteLine(parent.FindForm().PointToClient(parent.Parent.PointToScreen(parent.Location)).ToString());
            //Console.WriteLine(" " + ToScreenX(Path.GetBounds().X));
            //Console.WriteLine(" " + ToScreenY(Path.GetBounds().Y));
            //Console.WriteLine(" " + ToScreenX(Path.GetBounds().Width));
            //Console.WriteLine(" " + ToScreenY(Path.GetBounds().Height));

            //LocationChgEventArgs e = new LocationChgEventArgs();

            // 用户设定坐标后,  无需转换得到屏幕坐标值
            // 除非又再移动 /缩放 
            if (UserSetPos == false)
            {
                Prop.x = LocationCalc.ToScreenX(Path.GetBounds().X, parent.Width);
                Prop.y = LocationCalc.ToScreenY(Path.GetBounds().Y, parent.Height);
                Prop.w = LocationCalc.ToScreenX(Path.GetBounds().Width, parent.Width);
                Prop.h = LocationCalc.ToScreenY(Path.GetBounds().Height, parent.Height);
            }

            Prop.fx = Path.GetBounds().X;
            Prop.fy = Path.GetBounds().Y;
            Prop.fx1 = Path.GetBounds().X + Path.GetBounds().Width;
            Prop.fy1 = Path.GetBounds().Y + Path.GetBounds().Height;

            //Prop.fx = Path.GetBounds()X

            if (LocationChanged != null)
            {
                LocationChanged(this, Prop);
            }

            //LastLocation = e;
            if (ItemType == AdItemType.Video && Path.GetBounds().Width != 0)
            {
                //g.DrawString("没有添加视频文件,为HDMI输入窗口", new Font("Arial", 10), new SolidBrush(Color.White), Path.GetBounds());
                g.DrawString("请添加视频!", new Font("Arial", 10), new SolidBrush(Color.White), Path.GetBounds());
            }

            if (ItemType == AdItemType.Picture && Path.GetBounds().Width != 0)
            {
                g.DrawString("请添加图片!", new Font("Arial", 10), new SolidBrush(Color.Black), Path.GetBounds());
            }

            if (Mlabel != null)
            {
                Mlabel.Width = (int)Path.GetBounds().Width;
                Mlabel.Height = (int)Path.GetBounds().Height;
                Mlabel.Location = new Point((int)(Path.GetBounds().Location.X), (int)(Path.GetBounds().Location.Y));
            }

            if (_bitmap != null)
            {
                g.DrawImage(_bitmap, base.Path.GetBounds());
            }
        }

        private void initialProperties()
        {
            RightSideAngle = 0;
            TopSideAngle = -Math.PI / 2;
        }

        protected virtual void initialHotspotRects()
        {
            _hotspots = new DraggableHotSpot[HotspotCount];

            int i;
            for (i = 0; i <= _hotspots.Length - 2; i++)
            {
                _hotspots[i] = new DraggableHotSpot(HotSpotType.AnchorToScale);
            }
            // the last one is the rect used to rotate the shape
            //_hotspots[i] = new DraggableHotSpot(HotSpotType.RotatingRect);
        }

        public override void SetStartPoint(Point pt)
        {
            base.SetStartPoint(pt);
            rect.Location = pt;
        }

        public override void SetEndPoint(Point pt)
        {
            CreateEnd();

            rect.Width = pt.X - base.StartPoint.X;
            rect.Height = pt.Y - base.StartPoint.Y;

            rect.Location = base.StartPoint;
            if (rect.Width < 0)
            {
                rect.Width = -rect.Width;
                rect.X = base.StartPoint.X - rect.Width;
            }
            if (rect.Height < 0)
            {
                rect.Height = -rect.Height;
                rect.Y = base.StartPoint.Y - rect.Height;
            }

            ResetPath();
        }

        public override void SetNewPosForHotAnchor(int index, Point newPos)
        {
            Console.WriteLine("rectshape SetNewPosForHotAnchor  ");

            if (index == _hotspots.Length - 1)
            {
                // shape rotating
                base.Rotate(newPos);
            }
            else
            {
                // shape scaling

                //PointF[] pf = TransformHelper.GetScaledRectPathPoints(base.Path, index,
                //    base.LastTransformPoint, newPos, false);
                PointF[] pf = ScaleHelper.GetScaledRectPathPoints(base.Path, index, base.LastTransformPoint,
                    newPos, false, TopSideAngle, RightSideAngle);
                SetNewScaledPath(pf);
            }
            LastTransformPoint = newPos;
        }

        public override ToolType Type
        {
            get { return ToolType.Rectangle; }
        }

        public AdItemType ItemType { get; set; }

        public override string ReadableName
        {
            get { return "矩形"; }
        }

        public override void DrawSelectedRect(Graphics g, bool withAnchors)
        {
            PointF[] pf = CornerAnchors;
            using (Pen p = new Pen(Color.Gray))
            {
                p.DashPattern = new float[] { 4.0F, 4.0F, 4.0F, 4.0F };
                g.DrawLines(p, pf);
                g.DrawLine(p, pf[3], pf[0]);

                //if (withAnchors)
                //    g.DrawLine(p, CenterPoint, RotateLocation);
            }

            // cross on center pont
            // DrawHelper.DrawCrossSign(g, CenterPoint, EPConst.CrossSign_Half_Width);

            if (!withAnchors)
                return;

            // cross on rotate point
            // DrawHelper.DrawCrossSign(g, RotateLocation, EPConst.CrossSign_Half_Width);

            DraggableHotSpot[] hs = DraggableHotSpots;
            int i;
            for (i = 0; i < hs.Length - 1; i++)
            {
                if (!hs[i].Visible)
                    continue;

                g.FillRectangle(Brushes.White, hs[i].Rect);
                if (i < 4)
                    g.DrawRectangle(Pens.Red, hs[i].Rect);
                else
                    g.DrawRectangle(Pens.Black, hs[i].Rect);
            }

            // for rotate
            //g.DrawEllipse(Pens.Blue, hs[i].Rect);
        }

        protected override void AfterPathTransformed(PathTransformType transformType, bool refleshPath)
        {
            // Console.WriteLine("rectshape AfterPathTransformed  ");

            if (!IsInCreating)
            {
                ResetPathExtraInfo(transformType);
                if (transformType == PathTransformType.Scale)
                    ResetHotSpotsVisibility();
            }
            base.AfterPathTransformed(transformType, refleshPath);
        }

        protected virtual void ResetHotSpotsVisibility()
        {
            for (int i = 0; i <= _hotspots.Length - 2; i++)
                _hotspots[i].Visible = false;

            int w = EPConst.Anchor_Rect_Half_Width * 2;
            PointF[] pf = CornerAnchors;

            float dx = pf[1].X - pf[0].X;
            float dy = pf[1].Y - pf[0].Y;
            float top = (float)Math.Sqrt(dx * dx + dy * dy);
            dx = pf[3].X - pf[0].X;
            dy = pf[3].Y - pf[0].Y;
            float left = (float)Math.Sqrt(dx * dx + dy * dy);

            if (top < w * 4 && left < w * 4)
            {
                _hotspots[2].Visible = true;
            }
            else if (top < w * 4 || left < w * 4)
            {
                _hotspots[2].Visible = true;
                _hotspots[0].Visible = true;
            }
            else if (top < w * 6 && left < w * 6)
            {
                _hotspots[2].Visible = true;
                _hotspots[0].Visible = true;
                _hotspots[1].Visible = true;
                _hotspots[3].Visible = true;
            }
            else
            {
                for (int i = 0; i <= 3; i++)
                    _hotspots[i].Visible = true;
                if (top > w * 6)
                {
                    _hotspots[4].Visible = true;
                    _hotspots[6].Visible = true;
                }
                if (left > w * 6)
                {
                    _hotspots[5].Visible = true;
                    _hotspots[7].Visible = true;
                }
            }
        }

        /// <summary>
        /// reset center point, rotate point, corner/side anchors, top/right side angle
        /// </summary>
        protected virtual void ResetPathExtraInfo(PathTransformType transType)
        {
            // Console.WriteLine("ResetPathExtraInfo");
            PointF[] pf = base.Path.PathPoints;
            PointF[] side = new PointF[4];
            base.CenterPoint = new PointF((pf[0].X + pf[2].X) / 2, (pf[1].Y + pf[3].Y) / 2);

            side[0] = new PointF((pf[0].X + pf[1].X) / 2, (pf[0].Y + pf[1].Y) / 2);
            side[1] = new PointF((pf[1].X + pf[2].X) / 2, (pf[1].Y + pf[2].Y) / 2);
            side[2] = new PointF((pf[2].X + pf[3].X) / 2, (pf[2].Y + pf[3].Y) / 2);
            side[3] = new PointF((pf[3].X + pf[0].X) / 2, (pf[3].Y + pf[0].Y) / 2);

            if (transType == PathTransformType.Rotate || transType == PathTransformType.Scale)
            {
                double tmp = Math.Atan2(pf[1].Y - pf[0].Y, pf[1].X - pf[0].X);
                if (tmp != 0)
                    RightSideAngle = tmp;
                tmp = Math.Atan2(pf[0].Y - pf[3].Y, pf[0].X - pf[3].X);
                if (tmp != 0)
                    TopSideAngle = tmp;
            }

            double angle = TopSideAngle + Math.PI;
            PointF bottomM = new PointF((pf[2].X + pf[3].X) / 2, (pf[2].Y + pf[3].Y) / 2);
            PointF rotatePoint = new PointF();
            rotatePoint.X = (float)(bottomM.X + EPConst.RotatingRect_Offset * Math.Cos(angle));
            rotatePoint.Y = (float)(bottomM.Y + EPConst.RotatingRect_Offset * Math.Sin(angle));

            base.RotateLocation = rotatePoint;
            CornerAnchors = pf;
            SideAnchors = side;
        }

        protected override void RecalculateDraggableHotSpots()
        {
            //Console.WriteLine("RecalculateDraggableHotSpots");
            PointF[] ca = CornerAnchors;
            PointF[] sa = SideAnchors;

            PointF[] keyPoints = new PointF[8];
            for (int j = 0; j <= 7; j++)
                keyPoints[j] = (j < 4) ? ca[j] : sa[j - 4];

            int hw = EPConst.Anchor_Rect_Half_Width;
            int w = hw * 2;
            int i;
            for (i = 0; i <= _hotspots.Length - 2; i++)
            {
                _hotspots[i].Rect = new Rectangle(
                    (int)(keyPoints[i].X - hw), (int)(keyPoints[i].Y - hw), w, w);
            }
            hw = EPConst.RotatingRect_Half_Width;
            w = hw * 2;
            _hotspots[i].Rect = new Rectangle(
                    (int)(RotateLocation.X - hw), (int)(RotateLocation.Y - hw), w, w);
        }

        public override DraggableHotSpot[] DraggableHotSpots
        {
            get { return _hotspots; }
        }

        protected virtual void ResetPath()
        {
            //Console.WriteLine("reset path");
            base.BeforePathTransforming();
            base.Path.Reset();
            base.Path.AddRectangle(rect);

            this.AfterPathTransformed(PathTransformType.Scale, true);
        }

        /// <summary>
        /// 这个是给子类 EllipseTool 用的，用于 path.AddEllipse()
        /// </summary>
        protected Rectangle Rect
        {
            get { return rect; }
        }

        protected PointF[] CornerAnchors { get; set; }
        protected PointF[] SideAnchors { get; set; }
        protected double RightSideAngle { get; set; }
        protected double TopSideAngle { get; set; }
    }
}