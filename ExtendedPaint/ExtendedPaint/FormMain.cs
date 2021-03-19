using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gdu.ExtendedPaint
{
    public partial class FormMain : Form
    {
        private EPCanvas CanvasMain;
        private bool comingBackFromShape;
        private PropertyCollector proCollector;

        public FormMain()
        {
            InitializeComponent();
        }

        private void initialCavans()
        {
            CanvasMain = new EPCanvas();
            CanvasMain.Size = new Size(526, 382);
            CanvasMain.Location = new Point(140, 81);
            CanvasMain.AutoScroll = true;
            CanvasMain.BorderStyle = BorderStyle.FixedSingle;
            CanvasMain.Dock = DockStyle.Fill;
            this.panel5.Controls.Add(CanvasMain);

            CanvasMain.Kernel.SelectedShapesChanged += new EventHandler(Kernel_SelectedShapesChanged);
            CanvasMain.Kernel.PropertyCollectorChanged += new EventHandler(Kernel_PropertyCollectorChanged);
            CanvasMain.Kernel.ShapesListChangedChanged += new EventHandler(Kernel_ShapesListChangedChanged);
        }                

        private void initialData()
        {
            proCollector = new PropertyCollector();

            for (int i = 1; i <= 24; i++)
                ts_StrokeWidth.Items.Add(i);
            ts_StrokeWidth.SelectedIndex = 0;

            ts_cm_indicator_size.SelectedIndex = 2;
            ts_cm_PaintType.SelectedIndex = 0;
            
            ts_cm_round_radius.Items.Add(4);
            ts_cm_round_radius.Items.Add(6);
            ts_cm_round_radius.Items.Add(8);
            ts_cm_round_radius.SelectedIndex = 2;
        }

        private void tagInitial()
        {
            // antialias
            ts_Anlia_True.Tag = true;
            ts_Alia_False.Tag = false;

            // line startcap
            ts_startcap_round.Tag = LineCapType.Rounded;
            ts_startcap_square.Tag = LineCapType.Square;
            ts_startcap_rect.Tag = LineCapType.Rectangle;
            ts_startcap_circle.Tag = LineCapType.Circle;
            ts_startcap_line_arrow.Tag = LineCapType.LineArrow;
            ts_startcap_normal_arrow.Tag = LineCapType.NormalArrow;
            ts_startcap_sharp_arrow.Tag = LineCapType.SharpArrow;
            ts_startcap_sharp_arrow2.Tag = LineCapType.SharpArrow2;

            // line endcap
            ts_endcap_round.Tag = LineCapType.Rounded;
            ts_endcap_sqare.Tag = LineCapType.Square;
            ts_endcap_rect.Tag = LineCapType.Rectangle;
            ts_endcap_circle.Tag = LineCapType.Circle;
            ts_endcap_line_arrow.Tag = LineCapType.LineArrow;
            ts_endcap_normal_arrow.Tag = LineCapType.NormalArrow;
            ts_endcap_sharp_arrow.Tag = LineCapType.SharpArrow;
            ts_endcap_sharp_arrow2.Tag = LineCapType.SharpArrow2;

            // line dash type
            ts_linedash_solid.Tag = LineDashType.Solid;
            ts_linedash_dot.Tag = LineDashType.Dot;
            ts_linedash_dash_dot.Tag = LineDashType.DashedDot;
            ts_linedash_dash_dot_dot.Tag = LineDashType.DashedDotDot;
            ts_linedash_dash1.Tag = LineDashType.Dash1;
            ts_linedash_dash2.Tag = LineDashType.Dash2;
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
            switch (type)
            {
                case ShapePropertyType.NotDrawable:
                    setAntiVty(false);
                    setStrokableVty(false);
                    setFillOptionVty(false);
                    setRoundedVty(false);
                    setIndicatorVty(false);
                    break;
                case ShapePropertyType.StrokableProperty:
                    setAntiVty(true);
                    setStrokableVty(true);
                    setFillOptionVty(false);
                    setRoundedVty(false);
                    setIndicatorVty(false);
                    break;
                case ShapePropertyType.FillableProperty:
                    setAntiVty(true);
                    setStrokableVty(true);
                    setFillOptionVty(true);
                    setRoundedVty(false);
                    setIndicatorVty(false);
                    break;
                case ShapePropertyType.RoundedRectProperty:
                    setAntiVty(true);
                    setStrokableVty(true);
                    setFillOptionVty(true);
                    setRoundedVty(true);
                    setIndicatorVty(false);
                    break;
                case ShapePropertyType.IndicatorArrowProperty:
                    setAntiVty(true);
                    setStrokableVty(false);
                    setFillOptionVty(false);
                    setRoundedVty(false);
                    setIndicatorVty(true);
                    break;
            }
        }

        private void setAntiVty(bool v)
        {
            ts_Antialias.Visible = v;
        }

        private void setIndicatorVty(bool v)
        {            
            ts_sp_indicator.Visible = v;
            tslb_ia_size.Visible = v;
            ts_cm_indicator_size.Visible = v;
        }

        private void setRoundedVty(bool v)
        {
            tslb_round_radius.Visible = v;
            ts_cm_round_radius.Visible = v;
        }

        private void setFillOptionVty(bool v)
        {
            ts_sp_fill.Visible = v;
            tslb_painttype.Visible = v;
            ts_cm_PaintType.Visible = v;
        }

        private void setStrokableVty(bool v)
        {
            tslb_StrokeWidth.Visible = v;
            ts_StrokeWidth.Visible = v;
            
            tslb_LineStyle.Visible = v;
            tsbtn_StartCap.Visible = v;
            ts_line_dash.Visible = v;
            ts_endcap.Visible = v;
        }        

        private void useNewProperty(PropertyCollector newPro)
        {
            comingBackFromShape = true;

            bool isnull = (proCollector == null);

            // anti-alias
            if (isnull || proCollector.Antialias != newPro.Antialias)
            {
                ts_Antialias.Image = newPro.Antialias ? 
                    Properties.Resources.anti_true : Properties.Resources.anti_false;
            }

            // indicator arrow size
            if (isnull || proCollector.IndicatorLineSize != newPro.IndicatorLineSize)
            {
                switch (newPro.IndicatorLineSize)
                {
                    case IndicatorSize.Small:
                        ts_cm_indicator_size.SelectedIndex = 0;
                        break;
                    case IndicatorSize.Medium:
                        ts_cm_indicator_size.SelectedIndex = 1;
                        break;
                    case IndicatorSize.Large:
                        ts_cm_indicator_size.SelectedIndex = 2;
                        break;
                }
            }

            // stroke color
            if (isnull || proCollector.StrokeColor != newPro.StrokeColor)
            {
                MainColor.StrokeColor = newPro.StrokeColor;
            }

            // stroke width
            if (isnull || proCollector.PenWidth != newPro.PenWidth)
            {
                ts_StrokeWidth.Text = newPro.PenWidth.ToString();
            }

            // line start cap
            if (isnull || proCollector.StartLineCap != newPro.StartLineCap)
            {
                Bitmap bm = null;
                switch (newPro.StartLineCap)
                {
                    case LineCapType.Rounded:
                        bm = Properties.Resources.cap_round_left;
                        break;
                    case LineCapType.Square:
                        bm = Properties.Resources.cap_square_left;
                        break;
                    case LineCapType.Rectangle:
                        bm = Properties.Resources.cap_rect_left;
                        break;
                    case LineCapType.Circle:
                        bm = Properties.Resources.cap_circle_left;
                        break;
                    case LineCapType.LineArrow:
                        bm = Properties.Resources.line_arrow_left;
                        break;
                    case LineCapType.SharpArrow:
                        bm = Properties.Resources.sharp_arrow_left;
                        break;
                    case LineCapType.SharpArrow2:
                        bm = Properties.Resources.sharp_arrow2_left;
                        break;
                    case LineCapType.NormalArrow:
                        bm = Properties.Resources.normal_arrow_left;
                        break;
                }
                tsbtn_StartCap.Image = bm;
            }

            // line end cap
            if (isnull || proCollector.EndLineCap != newPro.EndLineCap)
            {
                Bitmap bm = null;
                switch (newPro.EndLineCap)
                {
                    case LineCapType.Rounded:
                        bm = Properties.Resources.cap_round_right;
                        break;
                    case LineCapType.Square:
                        bm = Properties.Resources.cap_square_right;
                        break;
                    case LineCapType.Rectangle:
                        bm = Properties.Resources.cap_rect_right;
                        break;
                    case LineCapType.Circle:
                        bm = Properties.Resources.cap_circle_right;
                        break;
                    case LineCapType.LineArrow:
                        bm = Properties.Resources.line_arrow_right;
                        break;
                    case LineCapType.SharpArrow:
                        bm = Properties.Resources.sharp_arrow_right;
                        break;
                    case LineCapType.SharpArrow2:
                        bm = Properties.Resources.sharp_arrow2_right;
                        break;
                    case LineCapType.NormalArrow:
                        bm = Properties.Resources.normal_arrow_right;
                        break;
                }
                ts_endcap.Image = bm;
            }

            // line dash type
            if (isnull || proCollector.LineDash != newPro.LineDash)
            {
                Bitmap bm = null;
                switch (newPro.LineDash)
                {
                    case LineDashType.Solid:
                        bm = Properties.Resources.solid;
                        break;
                    case LineDashType.Dot:
                        bm = Properties.Resources.dot;
                        break;
                    case LineDashType.DashedDot:
                        bm = Properties.Resources.dash_dot;
                        break;
                    case LineDashType.DashedDotDot:
                        bm = Properties.Resources.dash_dot_dot;
                        break;
                    case LineDashType.Dash1:
                        bm = Properties.Resources.dash;
                        break;
                    case LineDashType.Dash2:
                        bm = Properties.Resources.dash2;
                        break;
                }
                ts_line_dash.Image = bm;
            }

            // paint type
            if (isnull || proCollector.PaintType != newPro.PaintType)
            {
                switch (newPro.PaintType)
                {
                    case ShapePaintType.Stroke:
                        ts_cm_PaintType.SelectedIndex = 0;
                        break;
                    case ShapePaintType.Fill:
                        ts_cm_PaintType.SelectedIndex = 1;
                        break;
                    case ShapePaintType.StrokeAndFill:
                        ts_cm_PaintType.SelectedIndex = 2;
                        break;
                }
            }

            // fill color
            if (isnull || proCollector.FillColor != newPro.FillColor)
            {
                MainColor.FillColor = newPro.FillColor;
            }

            // round radius
            if (isnull || proCollector.RadiusAll != newPro.RadiusAll)
            {
                ts_cm_round_radius.Text = newPro.RadiusAll.ToString();
            }

            proCollector = newPro;
            comingBackFromShape = false;
        }

        private void Kernel_ShapesListChangedChanged(object sender, EventArgs e)
        {
            string[] s = CanvasMain.Kernel.ShapesInfoList;
            lbShapes.Items.Clear();
            if (s != null && s.Length > 0)
                lbShapes.Items.AddRange(s);
        }

        private void Kernel_SelectedShapesChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                if (CanvasMain.Kernel.SelectedShapesCount == 1)
                {
                    DrawableBase sp = CanvasMain.Kernel.SelectedShapes[0];
                    setOptionsVisibility(sp.ShapeProperty.PropertyType);
                }
                else
                {
                    setOptionsVisibility(ShapePropertyType.NotDrawable);
                }
            }
        }

        private void Kernel_PropertyCollectorChanged(object sender, EventArgs e)
        {
            useNewProperty(CanvasMain.Kernel.ShapePropertyCollector);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            initialCavans();
            initialData();
            
            tagInitial();
            
            radioButton2.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            selectNewTool(((Control)sender).TabIndex);
        }

        private void ts_Anlia_True_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            ts_Antialias.Image = mi.Image;

            bool v = (bool)mi.Tag;
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.Antialias, v);
            proCollector.Antialias = v;
        }

        private void ts_startcap_round_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            tsbtn_StartCap.Image = mi.Image;

            LineCapType type = (LineCapType)mi.Tag;
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.StartLineCap, type);
            proCollector.StartLineCap = type;
        }

        private void ts_linedash_solid_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            ts_line_dash.Image = mi.Image;

            LineDashType type = (LineDashType)mi.Tag;
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.LineDash, type);
            proCollector.LineDash = type;
        }

        private void ts_endcap_round_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            ts_endcap.Image = mi.Image;

            LineCapType type = (LineCapType)mi.Tag;
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.EndLineCap, type);
            proCollector.EndLineCap = type;
        }

        private void MainColor_FillColorChange(object sender, EventArgs e)
        {
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.FillColor, MainColor.FillColor);
            proCollector.FillColor = MainColor.FillColor;
        }

        private void MainColor_StrokeColorChange(object sender, EventArgs e)
        {
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.StrokeColor, MainColor.StrokeColor);
            proCollector.StrokeColor = MainColor.StrokeColor;
        }

        private void ts_cm_indicator_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comingBackFromShape)
                return;

            IndicatorSize size;
            switch (ts_cm_indicator_size.SelectedIndex)
            {
                case 0:
                    size = IndicatorSize.Small;
                    break;
                case 1:
                    size = IndicatorSize.Medium;
                    break;
                case 2:
                default:
                    size = IndicatorSize.Large;
                    break;
            }
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.IndicatorSize, size);
            proCollector.IndicatorLineSize = size;
        }

        private void ts_StrokeWidth_TextChanged(object sender, EventArgs e)
        {
            if (comingBackFromShape)
                return;

            float v;
            if(float.TryParse(ts_StrokeWidth.Text,out v))
            {
                CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.StrokeWidth, v);
                proCollector.PenWidth = v;
            }
            else
            {

            }
        }

        private void ts_cm_PaintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comingBackFromShape)
                return;

            ShapePaintType type;
            switch (ts_cm_PaintType.SelectedIndex)
            {
                case 0:
                    type = ShapePaintType.Stroke;
                    break;
                case 1:
                    type = ShapePaintType.Fill;
                    break;
                case 2:
                default:
                    type = ShapePaintType.StrokeAndFill;
                    break;
            }
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.PaintType, type);
            proCollector.PaintType = type;
        }

        private void ts_cm_round_radius_TextChanged(object sender, EventArgs e)
        {
            if (comingBackFromShape)
                return;

            int v;
            if (int.TryParse(ts_cm_round_radius.Text, out v))
            {
                CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.RoundedRadius, v);
                proCollector.RadiusAll = v;
            }
            else
            {

            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasMain.Kernel.SelectAllShapes();
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasMain.Kernel.DeleteAllShapse();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasMain.Kernel.DeleteSelectedShapes();
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "png(*.png)|*.png";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fn = dlg.FileName;
                Bitmap bm = CanvasMain.Kernel.FinalBitmap;
                bm.Save(fn, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FormAbout()).ShowDialog();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
