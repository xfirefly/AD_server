using Gdu.ExtendedPaint;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmMain : UserControl
    {
        private EPCanvas CanvasMain;
        private bool comingBackFromShape;
        private PropertyCollector _proCollector;

        private void initialCavans()
        {
            Console.WriteLine("canvas   " + gbCanvas.Width + " " + gbCanvas.Height);
            CanvasMain = new EPCanvas(gbCanvas.Size);
            CanvasMain.Size = new Size(gbCanvas.Width, gbCanvas.Height);

            CanvasMain.setMouseHookProc(MouseDownHook);
            CanvasMain.initialValue();
            CanvasMain.Location = new Point(0, 0);
            CanvasMain.AutoScroll = false;
            CanvasMain.BorderStyle = BorderStyle.FixedSingle;
            CanvasMain.Dock = DockStyle.Fill;
            gbCanvas.Controls.Add(CanvasMain);

            CanvasMain.Kernel.SelectedShapesChanged += new EventHandler(Kernel_SelectedShapesChanged);
            CanvasMain.Kernel.PropertyCollectorChanged += new EventHandler(Kernel_PropertyCollectorChanged);
            CanvasMain.Kernel.ShapesListChangedChanged += new EventHandler(Kernel_ShapesListChangedChanged);
            CanvasMain.Kernel.BeginShapeCreate += new EPKernel.BeginShapeCreateHandler(BeginShapeCreate);
            CanvasMain.Kernel.EndShapeCreate += new EPKernel.EndShapeCreateHandler(EndShapeCreate);
        }

        private void initialData()
        {
            _proCollector = new PropertyCollector();

            //for (int i = 1; i <= 24; i++)
            //    ts_StrokeWidth.Items.Add(i);
            //ts_StrokeWidth.SelectedIndex = 0;

            //ts_cm_indicator_size.SelectedIndex = 2;
            //ts_cm_PaintType.SelectedIndex = 0;

            //ts_cm_round_radius.Items.Add(4);
            //ts_cm_round_radius.Items.Add(6);
            //ts_cm_round_radius.Items.Add(8);
            //ts_cm_round_radius.SelectedIndex = 2;
        }

        private bool MouseDownHook(MouseEventArgs e)
        {
            if (App.HDMI && !App.VIDEO)
            {
                foreach (var item in CanvasMain.Kernel.getShapeList())
                {
                    RectShape rect = item as RectShape;
                    if (rect.ItemType == AdItemType.Video)
                    {
                        MessageBox.Show("一个场景下面只有有一个HDMI 窗口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                }
            }
            return false;
        }

        private void BeginShapeCreate(DrawableBase db)
        {
            log.i("BeginShapeCreate");
            RectShape rec = db as RectShape;
            rec.LocationChanged += LocationChg;
        }

        private void EndShapeCreate(DrawableBase db)
        {
            log.i("EndShapeCreate");
            //RectShape rec = db as RectShape;
            //if (rec.ItemType == AdItemType.Picture)
            //{
            //    tsAddPic_Click(null, null);
            //}
            //else if (rec.ItemType == AdItemType.Video && App.VIDEO)
            //{
            //    tsAddVideo_Click(null, null);
            //}
        }
        

        private void Kernel_ShapesListChangedChanged(object sender, EventArgs e)
        {
            log.i("Kernel_ShapesListChangedChanged");
            string[] s = CanvasMain.Kernel.ShapesInfoList;
            //lbShapes.Items.Clear();
            //if (s != null && s.Length > 0)
            //    lbShapes.Items.AddRange(s);
        }

        private void Kernel_SelectedShapesChanged(object sender, EventArgs e)
        {
            log.i("Kernel_SelectedShapesChanged " + CanvasMain.Kernel.SelectedShapesCount);
            //if (radioButton7.Checked)
            //{
            //    if (CanvasMain.Kernel.SelectedShapesCount == 1)
            //    {
            //        DrawableBase sp = CanvasMain.Kernel.SelectedShapes[0];
            //        setOptionsVisibility(sp.ShapeProperty.PropertyType);
            //    }
            //    else
            //    {CurrentTool
            //        setOptionsVisibility(ShapePropertyType.NotDrawable);
            //    }
            //}

            muDelElement.Enabled = (CanvasMain.Kernel.SelectedShapesCount != 0);
            if (CanvasMain.Kernel.SelectedShapesCount == 1)
            {
                RectShape rect = CanvasMain.Kernel.SelectedShapes[0] as RectShape;
                updateCtrlEnable(rect.ItemType, rect);

                txtX.Text = rect.Prop.x.ToString();
                txtY.Text = rect.Prop.y.ToString();
                txtWidth.Text = rect.Prop.w.ToString();
                txtHeight.Text = rect.Prop.h.ToString();
                //string output = JsonConvert.SerializeObject(rec);
                //Console.WriteLine(output);
            }
            else
            {
                subtitleCtrlEnable(false);
                tsAddPic.Enabled = false;
                tsAddVideo.Enabled = false;
                gbxywh.Enabled = false;

                if (CanvasMain.Kernel.CurrentTool == ToolType.ShapeSelect)
                {
                    //updateCtrlEnable(RectType.Select);
                }
                //tsSelect.Checked = true;  //在 canvas 里面空白处点击鼠标
                //tsSub.Checked = tsVideo.Checked = tsPic.Checked = false;    //不用
                txtX.Text = txtY.Text = txtWidth.Text = txtHeight.Text = "0";
            }
        }

        protected void LocationChg(object sender, ItemBase item)
        {
            DrawableBase db = sender as DrawableBase;

            if (db.IsSelected || db.IsInCreating)
            {
                //log.w("LocationChg " + item);

                txtX.Text = item.x.ToString();
                txtY.Text = item.y.ToString();
                txtWidth.Text = item.w.ToString();
                txtHeight.Text = item.h.ToString();
            }
        }

        private void Kernel_PropertyCollectorChanged(object sender, EventArgs e)
        {
            log.i("Kernel_PropertyCollectorChanged");
            useNewProperty(CanvasMain.Kernel.ShapePropertyCollector);
        }

        private void useNewProperty(PropertyCollector newPro)
        {
            comingBackFromShape = true;

            bool isnull = (_proCollector == null);

            // anti-alias
            if (isnull || _proCollector.Antialias != newPro.Antialias)
            {
                //ts_Antialias.Image = newPro.Antialias ?
                //    Properties.Resources.anti_true : Properties.Resources.anti_false;
            }

            // indicator arrow size
            if (isnull || _proCollector.IndicatorLineSize != newPro.IndicatorLineSize)
            {
                switch (newPro.IndicatorLineSize)
                {
                    case IndicatorSize.Small:
                        // ts_cm_indicator_size.SelectedIndex = 0;
                        break;

                    case IndicatorSize.Medium:
                        //ts_cm_indicator_size.SelectedIndex = 1;
                        break;

                    case IndicatorSize.Large:
                        //ts_cm_indicator_size.SelectedIndex = 2;
                        break;
                }
            }

            // stroke color
            if (isnull || _proCollector.StrokeColor != newPro.StrokeColor)
            {
                //MainColor.StrokeColor = newPro.StrokeColor;
                tsFontcolor.Color = newPro.StrokeColor;
            }

            // stroke width
            if (isnull || _proCollector.PenWidth != newPro.PenWidth)
            {
                //ts_StrokeWidth.Text = newPro.PenWidth.ToString();
            }

            // line start cap
            if (isnull || _proCollector.StartLineCap != newPro.StartLineCap)
            {
                Bitmap bm = null;
                switch (newPro.StartLineCap)
                {
                    case LineCapType.Rounded:
                        //bm = Properties.Resources.cap_round_left;
                        break;

                    case LineCapType.Square:
                        //bm = Properties.Resources.cap_square_left;
                        break;

                    case LineCapType.Rectangle:
                        //bm = Properties.Resources.cap_rect_left;
                        break;

                    case LineCapType.Circle:
                        //bm = Properties.Resources.cap_circle_left;
                        break;

                    case LineCapType.LineArrow:
                        //bm = Properties.Resources.line_arrow_left;
                        break;

                    case LineCapType.SharpArrow:
                        //bm = Properties.Resources.sharp_arrow_left;
                        break;

                    case LineCapType.SharpArrow2:
                        //bm = Properties.Resources.sharp_arrow2_left;
                        break;

                    case LineCapType.NormalArrow:
                        //bm = Properties.Resources.normal_arrow_left;
                        break;
                }
                //tsbtn_StartCap.Image = bm;
            }

            // line end cap
            if (isnull || _proCollector.EndLineCap != newPro.EndLineCap)
            {
                Bitmap bm = null;
                switch (newPro.EndLineCap)
                {
                    case LineCapType.Rounded:
                        //bm = Properties.Resources.cap_round_right;
                        break;

                    case LineCapType.Square:
                        //bm = Properties.Resources.cap_square_right;
                        break;

                    case LineCapType.Rectangle:
                        //bm = Properties.Resources.cap_rect_right;
                        break;

                    case LineCapType.Circle:
                        //bm = Properties.Resources.cap_circle_right;
                        break;

                    case LineCapType.LineArrow:
                        //bm = Properties.Resources.line_arrow_right;
                        break;

                    case LineCapType.SharpArrow:
                        //bm = Properties.Resources.sharp_arrow_right;
                        break;

                    case LineCapType.SharpArrow2:
                        //bm = Properties.Resources.sharp_arrow2_right;
                        break;

                    case LineCapType.NormalArrow:
                        //bm = Properties.Resources.normal_arrow_right;
                        break;
                }
                //ts_endcap.Image = bm;
            }

            // line dash type
            if (isnull || _proCollector.LineDash != newPro.LineDash)
            {
                Bitmap bm = null;
                switch (newPro.LineDash)
                {
                    case LineDashType.Solid:
                        //bm = Properties.Resources.solid;
                        break;

                    case LineDashType.Dot:
                        //bm = Properties.Resources.dot;
                        break;

                    case LineDashType.DashedDot:
                        //bm = Properties.Resources.dash_dot;
                        break;

                    case LineDashType.DashedDotDot:
                        //bm = Properties.Resources.dash_dot_dot;
                        break;

                    case LineDashType.Dash1:
                        //bm = Properties.Resources.dash;
                        break;

                    case LineDashType.Dash2:
                        //bm = Properties.Resources.dash2;
                        break;
                }
                //ts_line_dash.Image = bm;
            }

            // paint type
            if (isnull || _proCollector.PaintType != newPro.PaintType)
            {
                switch (newPro.PaintType)
                {
                    case ShapePaintType.Stroke:
                        //ts_cm_PaintType.SelectedIndex = 0;
                        break;

                    case ShapePaintType.Fill:
                        //ts_cm_PaintType.SelectedIndex = 1;
                        break;

                    case ShapePaintType.StrokeAndFill:
                        //ts_cm_PaintType.SelectedIndex = 2;
                        break;
                }
            }

            // fill color
            if (isnull || _proCollector.FillColor != newPro.FillColor)
            {
                //MainColor.FillColor = newPro.FillColor;

                // 避免触发event
                this.tsBackcolor.ColorChanged -= this.tsBackcolor_ColorChanged;
                tsBackcolor.Color = newPro.FillColor; ;
                this.tsBackcolor.ColorChanged += this.tsBackcolor_ColorChanged;
            }

            // round radius
            if (isnull || _proCollector.RadiusAll != newPro.RadiusAll)
            {
                //ts_cm_round_radius.Text = newPro.RadiusAll.ToString();
            }

            _proCollector = newPro;
            comingBackFromShape = false;
        }

        private void selectNewTool(int index)
        {
            switch (index)
            {
                case 0: // line
                    CanvasMain.Kernel.SetNewTool(ToolType.Line);
                    setOptionsVisibility(ShapePropertyType.StrokableProperty);
                    break;

                case 1: // indicator arrow
                    CanvasMain.Kernel.SetNewTool(ToolType.IndicatorArrow);
                    setOptionsVisibility(ShapePropertyType.IndicatorArrowProperty);
                    break;

                case 2: // broken line
                    CanvasMain.Kernel.SetNewTool(ToolType.BrokenLine);
                    setOptionsVisibility(ShapePropertyType.StrokableProperty);
                    break;

                case 3: // rect
                    CanvasMain.Kernel.SetNewTool(ToolType.Rectangle);
                    setOptionsVisibility(ShapePropertyType.FillableProperty);
                    break;

                case 4: // rounded rect
                    CanvasMain.Kernel.SetNewTool(ToolType.RoundedRect);
                    setOptionsVisibility(ShapePropertyType.RoundedRectProperty);
                    break;

                case 5: // ellipse
                    CanvasMain.Kernel.SetNewTool(ToolType.Ellipse);
                    setOptionsVisibility(ShapePropertyType.FillableProperty);
                    break;

                case 6: // shape select
                    CanvasMain.Kernel.SetNewTool(ToolType.ShapeSelect);
                    if (CanvasMain.Kernel.SelectedShapesCount != 1)
                        setOptionsVisibility(ShapePropertyType.NotDrawable);
                    break;
            }
        }

        private void setOptionsVisibility(ShapePropertyType type)
        {
        }

        public void subtitleCtrlEnable(bool enable)
        {
            tsStatic.Enabled =
            tsSpeed.Enabled =
            tsDirection.Enabled =
            tsTransparent.Enabled =
            tsBackcolor.Enabled =
            tsFontcolor.Enabled =
            tsItalic.Enabled =
            tsUnderline.Enabled =
            tsBold.Enabled =
            tsFontname.Enabled =
            tsFontsize.Enabled =
            lbAttrTip.Enabled =
            txtContent.Enabled = enable;
        }

        public void updateCtrlEnable(AdItemType itemType, RectShape rectShape = null)
        {
            switch (itemType)
            {
                case AdItemType.Video:
                    subtitleCtrlEnable(false);
                    tsAddPic.Enabled = false;
                    tsAddVideo.Enabled = true;
                    gbxywh.Enabled = true;

                    tsVideo.Checked = true;
                    tsSelect.Checked = tsPic.Checked = tsSub.Checked = false;
                    break;

                case AdItemType.Picture:
                    subtitleCtrlEnable(false);
                    tsAddPic.Enabled = true;
                    tsAddVideo.Enabled = false;
                    gbxywh.Enabled = true;

                    tsPic.Checked = true;
                    tsSelect.Checked = tsVideo.Checked = tsSub.Checked = false;
                    break;

                case AdItemType.Subtitle:
                    subtitleCtrlEnable(true);
                    tsAddPic.Enabled = false;
                    tsAddVideo.Enabled = false;
                    gbxywh.Enabled = true;

                    tsSub.Checked = true;
                    tsSelect.Checked = tsVideo.Checked = tsPic.Checked = false;

                    if (rectShape != null)
                    {
                        txtContent.Text = rectShape.Mlabel.Text;
                    }
                    break;

                case AdItemType.Select:
                    subtitleCtrlEnable(false);
                    tsAddPic.Enabled = false;
                    tsAddVideo.Enabled = false;
                    gbxywh.Enabled = false;

                    tsSelect.Checked = true;
                    tsSub.Checked = tsVideo.Checked = tsPic.Checked = false;

                    txtX.Text = txtY.Text = txtWidth.Text = txtHeight.Text = "0";
                    break;

                default:
                    break;
            }
        }

        private RectShape getSelectedRect()
        {
            if (CanvasMain.Kernel.SelectedShapesCount != 1)
            {
                MessageBox.Show("没有选中元素");
                return null;
            }
            return CanvasMain.Kernel.SelectedShapes[0] as RectShape;
        }

        private MarqueeLabel setTextProp(TextProperty tp, object o)
        {
            RectShape rs = getSelectedRect();
            if (rs != null && rs.ItemType == AdItemType.Subtitle)
            {
                return rs.setTextProperty(tp, o);
            }
            return null;
        }

        private bool hasInvalidNum()
        {
            bool ret = false;
            try
            {
                int t = int.Parse(txtX.Text);
                t = int.Parse(txtY.Text);
                t = int.Parse(txtWidth.Text);
                t = int.Parse(txtHeight.Text);
            }
            catch (Exception)
            {
                ret = true;
            }

            return ret;
        }

        private void checkValue(TextBox txt)
        {
            switch (txt.Tag.ToString())
            {
                case "x":
                case "w":
                    if (int.Parse(txt.Text) > LocationCalc.SCREEN_W || int.Parse(txt.Text) < 0)
                    { 
                        txt.BackColor = Color.Red;
                        txt.ForeColor = Color.White;
                    }
                    else
                    {
                        txt.BackColor = SystemColors.Window;
                        txt.ForeColor = SystemColors.WindowText;
                    }
                    break;

                case "y":
                case "h":
                    if (int.Parse(txt.Text) > LocationCalc.SCREEN_H || int.Parse(txt.Text) < 0)
                    {
                        txt.BackColor = Color.Red;
                        txt.ForeColor = Color.White;
                    }
                    else
                    {
                        txt.BackColor = SystemColors.Window;
                        txt.ForeColor = SystemColors.WindowText;
                    }
                    break;

                default:
                    break;
            }
        }
    } //class
}