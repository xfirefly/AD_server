using Common;
using Gdu.ExtendedPaint;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextSender
{
    public partial class frmMain : UserControl, IAdUserControl
    {
        private Log log;
        private const string AD_CONFIG_JSON = AdScene.AD_CONFIG_JSON; // "config.json";

        public frmMain()
        {
            log = new Log(GetType());
            InitializeComponent();

            //App.Instance.MainForm = this;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("frmMain_Load");
            this.ParentForm.FormClosing += (s, evt) => { OnHandleDestroyed(new EventArgs()); };

            //frmSend fs = new frmSend();
            //fs.ShowDialog();
            if (!App.VIDEO)
            {
                tsAddVideo.Enabled = tsAddVideo.Visible = false;
            }

            //miHDMIFullscr.Enabled = miHDMIFullscr.Visible = false;

            //FormBorderStyle = FormBorderStyle.FixedSingle;
            ///TODO: 时间,日期, 星期几

            gbDrawTool.Enabled = false;
            txtSceneName.Enabled = false;
            btSceneRename.Enabled = btSceneDel.Enabled = btOpenScene.Enabled = false;
            //btCloseScene.Enabled = false;

            //btSceneDown.Enabled = btSceneUp.Enabled = false;
            //btnSend.Enabled =   false;

            //btSceneNewSave.Enabled = false;
            muDelElement.Enabled = false;
            updateCtrlEnable(AdItemType.Select);
            tsFontname.SelectedIndex = 0;


            initialCavans();
            initialData();

            //_pScreen = pScreen.FindForm().PointToClient(pScreen.Parent.PointToScreen(pScreen.Location));

            for (int i = 10; i <= 200; i += 5)
            {
                tsFontsize.Items.Add(i);
            }

            loadScene();

            //cmBox.Items.Insert(0, new ToolStripLabel("终端控制"));
            //cmBox.Items.Insert(1, new ToolStripSeparator());

            btSceneNewSave.Focus();
            btSceneNewSave.Select();

            setScreenWH();

            log.e("CanvasMain: " + CanvasMain.Size);
            LocationCalc.setCanvas(CanvasMain.Size);
        }



        private void frmMain_Resize(object sender, EventArgs e)
        {
            log.e("frmMain_Resize canvas   " + gbCanvas.Width + " " + gbCanvas.Height);
            //log.e("CanvasMain: " + CanvasMain.Size);
            //initialCavans();
            //initialData();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            frmMain_FormClosing(null, null);
        }

#if false
        private Point _pScreen;
        int _txtIndex = 1;

        private Ctrl.SubtitleCtrl AddSubtitleText()
        {
            // Settings to generate a New TextBox
            Ctrl.SubtitleCtrl txt = new Ctrl.SubtitleCtrl();   // Create the Variable for TextBox
            _currentFocus = txt;
            ChangeTxtAttr(txt);

            //txt.TextChanged += new System.EventHandler(txt_TextChanged);
            txt.Click += Txt_Click;
            txt.Tag = _txtIndex;
            _txtIndex++;

            // Create Variables to Define "X" and "Y" Locations
            //var txtLocX = txt.Location.X;
            //var txtLocY = txt.Location.Y;

            //Set your TextBox Location Here
            //txt.Location.X = x;
            //txt.Location.Y = y;
            //txt.Location = new Point(x, y);

            //txt.TextWrapping = TextWrapping.Wrap;
            //txt.WordWrap = true;
            //txt.Multiline = true;
            //txt.Height = h;
            //txt.Width = w;
            txt.AutoSize = false;

            //txt.AcceptsReturn = true;
            //txt.Margin = new Thickness(10, 15, 950, 0);

            // This adds a new TextBox
            //txt.MouseDown += new MouseEventHandler(textbox_MouseDown);
            //txt.MouseMove += new MouseEventHandler(textbox_MouseMove);
            //txt.MouseUp += new MouseEventHandler(textbox_MouseUp);
            //txt.MouseLeave += delegate (object sender, EventArgs e)
            //{
            //    Cursor = Cursors.Default;
            //};

            ControlMoverOrResizer.Init(txt);
            this.pScreen.Controls.Add(txt);

            return txt;
        }

        private Control activeControl;
        private Point previousLocation;
        void textbox_MouseDown(object sender, MouseEventArgs e)
        {
            activeControl = sender as Control;
            previousLocation = e.Location;
        }

        void textbox_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeAll;
            if (activeControl == null || activeControl != sender)
                return;

            var location = activeControl.Location;
            location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            activeControl.Location = location;
        }

        void textbox_MouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;
        }

        Ctrl.SubtitleCtrl _currentFocus;

        // 点击之后, 在属性界面加载本字幕控件的 属性
        private void Txt_Click(object sender, EventArgs e)
        {
            //MessageBox.Show (((Control)sender).Name);
            Ctrl.SubtitleCtrl txt = (Ctrl.SubtitleCtrl)sender;
            _currentFocus = txt;

            txtAttr.LoadBasicAttr((Control)txt);
            txtAttr.X = txt.X;
            txtAttr.Y = txt.Y;
            txtAttr.W = txt.W;
            txtAttr.H = txt.H;
            txtAttr.Fontname = txt.fontname;
            txtAttr.Fontsize = txt.fontsize;
            txtAttr.Transparent = txt.transparent;
            txtAttr.Speed = txt.Speed;
            txtAttr.Direction = txt.Direction;
        }

        // 创建 字幕控件时调用, 初始化默认属性
        // 属性界面改变属性之后要调用这个更新 字幕控件
        private void ChangeTxtAttr(Ctrl.SubtitleCtrl txt)
        {
            txt.Name = "subtitle_" + _txtIndex;
            txt.BackColor = ColorTranslator.FromHtml(txtAttr.Bgcolor);
            txt.ForeColor = ColorTranslator.FromHtml(txtAttr.Color);
            txt.Text = txtAttr.Txt;
            txt.Direction = txtAttr.Direction;
            txt.Speed = txtAttr.Speed;

            int x = ToFormX(txtAttr.X);
            int y = ToFormY(txtAttr.Y);
            txt.Location = new Point(x, y);
            txt.Width = ToFormX(txtAttr.W);
            txt.Height = ToFormY(txtAttr.H);

            txt.X = txtAttr.X;
            txt.Y = txtAttr.Y;
            txt.W = txtAttr.W;
            txt.H = txtAttr.H;

            txt.fontname = txtAttr.Fontname;
            txt.fontsize = txtAttr.Fontsize;
            txt.transparent = txtAttr.Transparent;

            txt.Font = new Font("宋体", float.Parse(txtAttr.Fontsize), FontStyle.Regular);
            //txt.Name1 = "";
        }

        private void pScreen_MouseMove(object sender, MouseEventArgs e)
        {
            Point endLoc = new Point(_pScreen.X + e.X, _pScreen.Y + e.Y);
            //scrLeft = e.X;
            //scrTop = e.Y;
            //_start = e.Location;
            //_start = endLoc;

            if (_start.HasValue)
            {
                this.Refresh();
                ReverseFrame();
                DrawFrame(endLoc);

                txtAttr.X = ToScreenX(_start.Value.X - _pScreen.X);
                txtAttr.Y = ToScreenY(_start.Value.Y - _pScreen.Y);
                txtAttr.W = ToScreenX(endLoc.X - _start.Value.X);
                txtAttr.H = ToScreenY(endLoc.Y - _start.Value.Y);
                txtAttr.Name1 = "subtitle_" + _txtIndex;
            }
        }
        private void pScreen_MouseUp(object sender, MouseEventArgs e)
        {
            ReverseFrame();
            _start = null;
            if (txtAttr.W <= 0 || txtAttr.W <= 0)
            {
                MessageBox.Show("宽度或高度为负数!");
                return;
            }
            Ctrl.SubtitleCtrl txt = AddSubtitleText();

            _ht.Add(_scrLeft + " " + _scrTop, txt);
            //lbTxt.Items.Add(_scrLeft + " " + _scrTop);
        }
        private void pScreen_MouseDown(object sender, MouseEventArgs e)
        {
            _scrLeft = e.X;
            _scrTop = e.Y;
            //_start = e.Location;
            _start = new Point(_pScreen.X + e.X, _pScreen.Y + e.Y);

            Console.Write(e.X + " " + e.Y);
            Console.WriteLine();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
        }
        */

        private int ToScreenX(int i)
        {
            return 1920 * i / pScreen.Width;
        }

        private int ToFormX(int i)
        {
            return pScreen.Width * i / 1920;
        }

        private int ToScreenY(int i)
        {
            return 1080 * i / pScreen.Height;
        }

        private int ToFormY(int i)
        {
            return pScreen.Height * i / 1080;
        }

        private void ReverseFrame()
        {
            ControlPaint.DrawReversibleFrame(_previousBounds, Color.Red, FrameStyle.Dashed);
        }
        private void DrawFrame(Point end)
        {
            ReverseFrame();

            var size = new Size(end.X - _start.Value.X, end.Y - _start.Value.Y);
            _previousBounds = new Rectangle(_start.Value, size);
            _previousBounds = this.RectangleToScreen(_previousBounds);
            ControlPaint.DrawReversibleFrame(_previousBounds, Color.Yellow, FrameStyle.Dashed);
        }

        private Point? _start;
        private Rectangle _previousBounds;

        private int _scrLeft;
        private int _scrTop;
        private Hashtable _ht = new Hashtable();

        private void txtAttr_AttrChanged(object sender, EventArgs ev)
        {
            try
            {
                //ChangeTxtAttr(_currentFocus);
            }
            catch (Exception e)
            {
                log.w("err", e);
                //Console.WriteLine("Source = " + e.Source);
                //Console.WriteLine("StackTrace = " + e.StackTrace);
                //Console.WriteLine("TargetSite = " + e.TargetSite);
            }
        }

        private void txtAttr_Load(object sender, EventArgs e)
        {
        }

        private void pScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int numOfCells = 10;
            int xcellSize = pScreen.Width / 10;
            int ycellSize = pScreen.Height / 10;
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
#endif

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.d("close");
            //List<string> list = new List<string>();
            //foreach (var item in lbScene.Items)
            //{
            //    log.t(lbScene.GetItemText(item));
            //    list.Add(lbScene.GetItemText(item));
            //}
            //if (list.Count > 0)
            //{
            //    Profile.setSceneName(list);
            //}
        }

        private void panelEx1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void chSelScene_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lbScene.Items.Count; i++)
            {
                lbScene.SetItemChecked(i, chSelScene.Checked);
            }
            btSceneDel.Enabled = chSelScene.Checked;
        }

        private void chSelIP_CheckedChanged(object sender, EventArgs e)
        {
        }

        public void sendUSB_Click(object sender, EventArgs e)
        {
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            if (CanvasMain.Kernel.SelectedShapesCount == 1)
            {
                MarqueeLabel sub = setTextProp(TextProperty.Text, txtContent.Text);
                //CanvasMain.Controls.Add(sub);
            }
        }

        protected void TxtChanged(string text)
        {
            txtContent.Text = text;
        }

        private void txtContent_DoubleClick(object sender, EventArgs e)
        {
            // 用代理 传递信息
            frmInputText f = new frmInputText(txtContent.Text);
            f.TxtChanged = new frmInputText.TxtChangedHandler(TxtChanged);
            f.ShowDialog();
        }

        private void lbScene_MouseUp(object sender, MouseEventArgs e)
        {
            btSceneDel.Enabled = lbScene.CheckedItems.Count != 0;
        }

        private void lbScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbScene.SelectedItem != null)
            {
                //btSceneUp.Enabled = lbScene.SelectedIndex != 0;
                //btSceneDown.Enabled = lbScene.SelectedIndex != (lbScene.Items.Count - 1);
            }
            else
            {
                //btSceneDown.Enabled = btSceneUp.Enabled = false;
            }
            btSceneRename.Enabled = btOpenScene.Enabled = lbScene.SelectedItem != null;
        }

        private void btSceneUp_Click(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void btSceneDown_Click(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void btSceneDel_Click_1(object sender, EventArgs e)
        {
            if (App.MsgBox2(Resource1.do_delete, Resource1.notice) == DialogResult.OK)
            {
                while (lbScene.CheckedItems.Count != 0)
                {
                    try
                    {
                        removeScene();
                    }
                    catch (Exception)
                    {
                        log.w("delete error!");
                        // 清除原有场景
                        CanvasMain.Kernel.DeleteAllShapse();
                        endSceneEditing();
                        removeScene();
                    }
                }
            }
        }

        private void removeScene()
        {
            string dir = Path.Combine(App.SceneDir, lbScene.CheckedItems[0].ToString());

            log.t(dir);
            Directory.Delete(dir, true);
            lbScene.Items.Remove(lbScene.CheckedItems[0]);
        }

        private void btSceneRename_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox(Resource1.rename_scene, Resource1.rename, lbScene.GetItemText(lbScene.SelectedItem), -1, -1);
            if (string.IsNullOrEmpty(s))
            {
                log.e("cancel btSceneSettime_Click");
            }
            else
            {
                string source = Path.Combine(App.SceneDir, lbScene.GetItemText(lbScene.SelectedItem));
                string dest = Path.Combine(App.SceneDir, s);

                try
                {
                    Directory.Move(source, dest);
                    lbScene.Items[lbScene.SelectedIndex] = s;
                }
                catch (System.Exception ex)
                {
                    App.MsgBoxE(Resource1.rename_fail);
                }
                //修改场景文件夹名称
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void tsSelect_Click(object sender, EventArgs e)
        {
            updateCtrlEnable(AdItemType.Select);
            CanvasMain.Kernel.SetRect(ToolType.ShapeSelect, AdItemType.Select);
        }

        private void tsVideo_Click(object sender, EventArgs e)
        {
            //if (!newScene())
            //{
            //    return;
            //}

            //updateCtrlEnable(RectType.Video);
            tsSelect.Checked = tsPic.Checked = tsSub.Checked = false;

            CanvasMain.Kernel.ClearSelectedShapes();
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.FillColor, Color.Blue);
            CanvasMain.Kernel.SetRect(ToolType.Rectangle, AdItemType.Video);
        }

        private void tsPic_Click(object sender, EventArgs e)
        {
            //if (!newScene())
            //{
            //    return;
            //}
            //updateCtrlEnable(RectType.Picture);
            tsSelect.Checked = tsVideo.Checked = tsSub.Checked = false;

            CanvasMain.Kernel.ClearSelectedShapes();
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.FillColor, Color.Yellow);
            CanvasMain.Kernel.SetRect(ToolType.Rectangle, AdItemType.Picture);
        }

        private void tsSub_Click(object sender, EventArgs e)
        {
            //if (!newScene())
            //{
            //    return;
            //}
            //updateCtrlEnable(RectType.Subtitle);
            tsSelect.Checked = tsVideo.Checked = tsPic.Checked = false;

            CanvasMain.Kernel.ClearSelectedShapes();
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.FillColor, Color.Gray);
            CanvasMain.Kernel.SetRect(ToolType.Rectangle, AdItemType.Subtitle);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void muDelElement_Click(object sender, EventArgs e)
        {
            CanvasMain.Kernel.DeleteSelectedShapes();
        }

        protected void ImgChanged(object sender, frmAddFile.FileChangedEventArgs e)
        {
            log.t("img intval " + e.intval);
            if (e.fileList.Count == 0)
            {
                App.MsgBoxE(Resource1.need_one_img);
                return;
            }
            showImageOnPic(e.fileList, e.intval);

            foreach (var item in e.fileList)
            {
                log.t(item);
            }
        }

        /// <summary>
        /// 为图片元素添加图片
        /// </summary>
        private void tsAddPic_Click(object sender, EventArgs e)
        {
#if true    //多张图片
            Picture pic = getSelectedRect().Prop as Picture;
            string tips = Resource1.img_res + pic.w + " * " + pic.h + Resource1.best_res;

            // 用自定义事件 传递信息
            frmAddFile addPic = new frmAddFile(tips, pic.intval, pic.filelist, frmAddFile.FileType.Image);
            addPic.FileChanged += new frmAddFile.FileChangedEventHandler(ImgChanged);
            addPic.ShowDialog();

#else   //单张图片
            using (OpenFileDialog dlg = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "选择一张图片",
                Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*"
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    showImageOnPic(@dlg.FileName);
                }
            }
#endif
        }

        private void addDefItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mu = sender as ToolStripMenuItem;
            int i = int.Parse(mu.Tag.ToString());

            //Point p = CanvasMain.Location;
            Point start = new Point(0, 0);
            Point end = new Point(0, 0);
            Size sz = CanvasMain.Size;
            log.t("CanvasMain.Size " + CanvasMain.Size);
            const int height = 15;
            switch (i)
            {
                case 1:
                    start = new Point(0, 0);
                    end = new Point(sz.Width, 0 + sz.Height / height);
                    tsSub_Click(null, null);    //选择字幕工具
                    addRectOnCanvas(start, end);
                    break;

                case 2:
                    start = new Point(0, sz.Height / 2);
                    end = new Point(sz.Width, sz.Height / 2 + sz.Height / height);
                    tsSub_Click(null, null);    //选择字幕工具
                    addRectOnCanvas(start, end);
                    break;

                case 3:
                    start = new Point(0, sz.Height - sz.Height / height);
                    end = new Point(sz.Width, sz.Height);
                    tsSub_Click(null, null);    //选择字幕工具
                    addRectOnCanvas(start, end);
                    break;

                case 4:
                    if (MouseDownHook(null))
                    {
                        return;
                    }
                    start = new Point(0, 0);
                    end = new Point(sz.Width, sz.Height);
                    tsVideo_Click(null, null);    //选择hdmi工具
                    addRectOnCanvas(start, end);

                    break;

                case 5:
                    start = new Point(0, 0);
                    end = new Point(sz.Width, sz.Height);
                    tsPic_Click(null, null);
                    addRectOnCanvas(start, end);

                    break;

                default:
                    break;
            }
            //tsSelect_Click(null, null);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void tsFontname_Click(object sender, EventArgs e)
        {
        }

        private void tsFontsize_Click(object sender, EventArgs e)
        {
        }

        private void tsBold_Click(object sender, EventArgs e)
        {
            setTextProp(TextProperty.Bold, tsBold.Checked);
        }

        private void tsItalic_Click(object sender, EventArgs e)
        {
            setTextProp(TextProperty.Italic, tsItalic.Checked);
        }

        private void tsUnderline_Click(object sender, EventArgs e)
        {
            setTextProp(TextProperty.Underline, tsUnderline.Checked);
        }

        private void tsFontcolor_ColorChanged(object sender, EventArgs e)
        {
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.StrokeColor, tsFontcolor.Color);
            _proCollector.StrokeColor = tsFontcolor.Color;

            setTextProp(TextProperty.FontColor, tsFontcolor.Color);
        }

        private void tsBackcolor_ColorChanged(object sender, EventArgs e)
        {
            log.t(System.Reflection.MethodBase.GetCurrentMethod().Name + " " + tsBackcolor.Color);
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.FillColor, tsBackcolor.Color);
            _proCollector.FillColor = tsBackcolor.Color;

            setTextProp(TextProperty.BackColor, tsBackcolor.Color);
        }

        private void tsTransparent_Click(object sender, EventArgs e)
        {
            CanvasMain.Kernel.SetNewShapePropertyValue(ShapePropertyValueType.FillColor, Color.Transparent);
            _proCollector.FillColor = Color.Transparent;

            setTextProp(TextProperty.BackColor, Color.Transparent);
        }

        private void tsSpeed_Click(object sender, EventArgs e)
        {
        }

        // http://csharphelper.com/blog/2014/08/make-menu-items-act-like-radio-buttons-in-c/
        // Uncheck all menu items in this menu except checked_item.
        private void CheckMenuItem(ToolStripMenuItem mnu,
            ToolStripMenuItem checked_item)
        {
            // Uncheck the menu items except checked_item.
            foreach (ToolStripItem item in mnu.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menu_item =
                        item as ToolStripMenuItem;
                    menu_item.Checked = (menu_item == checked_item);
                }
            }
        }

        private void CheckToolbarMenuItem(ToolStripDropDownButton mnu, ToolStripMenuItem checked_item)
        {
            // Uncheck the menu items except checked_item.
            foreach (ToolStripItem item in mnu.DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem menu_item =
                        item as ToolStripMenuItem;
                    menu_item.Checked = (menu_item == checked_item);
                }
            }
        }

        // Check this menu item and uncheck the others.
        private void tsSpeed_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Check the menu item.

            CheckToolbarMenuItem(tsSpeed, e.ClickedItem as ToolStripMenuItem);

            // Do something with the menu selection.
            // You could use a switch statement here.
            // This example just displays the menu item's text.
            //MessageBox.Show(item.Text);

            setTextProp(TextProperty.Speed, int.Parse(e.ClickedItem.Text));
        }

        private void tsDirection_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //int d = 0;
            //if (e.ClickedItem.tag == "从左往右")
            //    d = 1;
            log.e(e.ClickedItem.Tag.ToString());
            setTextProp(TextProperty.Direction, (Direction)Enum.Parse(typeof(Direction), e.ClickedItem.Tag.ToString()));
        }

        private void tsFontsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTextProp(TextProperty.FontSize, int.Parse(tsFontsize.Text));
        }

        private void tsFontsize_TextUpdate(object sender, EventArgs e)
        {
            tsFontsize_SelectedIndexChanged(sender, e);
        }

        private void tsFontname_DropDownClosed(object sender, EventArgs e)
        {
            setTextProp(TextProperty.FontName, FontEx.getTtfName(tsFontname.Text));
        }

        //保存到 scene文件夹, zip 文件
        private void btSaveScene_Click(object sender, EventArgs e)
        {
            if (!sceneEditing)
            {
                string sName = Interaction.InputBox("请输入新建场景名称: ", "新建场景", "未命名场景", -1, -1);  //-1表示在屏幕的中间
                if (string.IsNullOrEmpty(sName))
                {
                    log.e("cancel newScene");
                    App.MsgBox("你取消了新建场景!");
                }
                else
                {
                    createScene(sName);
                }
            }
            else
            {
                if (lbScene.Items.Contains(txtSceneName.Text)) 
                {
                    if (App.MsgBox2(Resource1.scene_over_wr, Resource1.notice) != DialogResult.OK)
                    {
                        return;
                    }
                }

                saveScene();
            }
        }

        private void panelEx1_Enter(object sender, EventArgs e)
        {
        }

        private void cmOpenScene_Click(object sender, EventArgs e)
        {
            OpenScene();

            btCloseScene.Enabled = true;
        }

        private void cmCloseScene_Click(object sender, EventArgs e)
        {
            // 清除原有场景
            CanvasMain.Kernel.DeleteAllShapse();
            endSceneEditing();

            // btCloseScene.Enabled = false;
        }

        private void cmScene_Opening(object sender, CancelEventArgs e)
        {
            cmOpenScene.Enabled =
                cmScene.Enabled =
                cmDelete.Enabled =
                cmCooy.Enabled =
                cmRename.Enabled =
                lbScene.SelectedItem != null;
        }

        private void lbScene_DoubleClick(object sender, EventArgs e)
        {
            if (lbScene.SelectedItem == null)
            {
                return;
            }
            OpenScene();
        }

        private float ToFormX(string str)
        {
            float v = LocationCalc.ToFormX(float.Parse(str), CanvasMain.Width);

            log.t("ToFormX " + str + " " + v);
            return v;
        }

        private float ToFormY(string str)
        {
            float v = LocationCalc.ToFormY(float.Parse(str), CanvasMain.Height);
            log.t("ToFormY " + str + " " + v);
            return v;
        }

        private void txtPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            // http://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPosition_KeyUp(object sender, KeyEventArgs e)
        {
            if (hasInvalidNum())
            {
                return;
            }

            RectShape rect = getSelectedRect();
            TextBox txt = sender as TextBox;

            // form 上面的坐标位置. 左上角
            float fx = rect.Prop.fx;
            float fy = rect.Prop.fy;
            // form 上面的坐标位置. 右下角
            float fx1 = rect.Prop.fx1;
            float fy1 = rect.Prop.fy1;

            rect.UserSetPos = true;
            rect.LocationChanged -= LocationChg;    //修改过程中不更新 4 个文本框
            switch (txt.Tag.ToString())
            {
                case "x":
                    rect.Prop.x = int.Parse(txtX.Text);
                    fx = ToFormX(txtX.Text);
                    fx1 = rect.Prop.fx1 + (ToFormX(txtX.Text) - rect.Prop.fx); // x-offset
                    break;

                case "y":
                    rect.Prop.y = int.Parse(txtY.Text);
                    fy = ToFormY(txtY.Text);
                    fy1 = rect.Prop.fy1 + (ToFormY(txtY.Text) - rect.Prop.fy);  // y-offset
                    break;

                case "w":
                    rect.Prop.w = int.Parse(txtWidth.Text);
                    fx1 = rect.Prop.fx + ToFormX(txtWidth.Text);
                    break;

                case "h":
                    rect.Prop.h = int.Parse(txtHeight.Text);
                    fy1 = rect.Prop.fy + ToFormY(txtHeight.Text);
                    break;

                default:
                    break;
            }
            Point pos = new Point((int)fx, (int)fy);
            rect.SetNewPosForHotAnchor(0, pos);

            Point pos2 = new Point((int)fx1, (int)fy1);
            rect.SetNewPosForHotAnchor(2, pos2);

            log.i("PP " + pos + pos2);
            rect.UserSetPos = true;
            rect.LocationChanged += LocationChg;
        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {
            if (hasInvalidNum())
            {
                return;
            }

            TextBox txt = sender as TextBox;
            checkValue(txt);
        }

        private void tsFontname_Click_1(object sender, EventArgs e)
        {
        }

        private void gbCanvas_Enter(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        }

        private void displayScreenshot(List<string> filelist, int intval)
        {
            string firstVid = filelist[0];  //  advid_96ce7e0eef98c6245048fc56bf46530d.mp4 OR 广告宣传.mp4
            string md5 = FFMPEG.GetThumbnail(firstVid);

            if (CanvasMain.Kernel.SelectedShapesCount == 1)
            {
                getSelectedRect().setVideoIntval(intval);
                getSelectedRect().setVideo(filelist);
                getSelectedRect().displayImage(md5);
                CanvasMain.reDraw();
                CanvasMain.Kernel.RefleshBitmap();
            }
        }

        protected void VideoChanged(object sender, frmAddFile.FileChangedEventArgs e)
        {
            log.t("video intval " + e.intval);
            if (e.fileList.Count == 0)
            {
                App.MsgBoxE(Resource1.need_one_video);
                return;
            }

            displayScreenshot(e.fileList, e.intval);

            foreach (var item in e.fileList)
            {
                log.t(item);
            }
        }

        private void tsAddVideo_Click(object sender, EventArgs e)
        {
            Video v = getSelectedRect().Prop as Video;
            string tips = Resource1.img_res + v.w + " * " + v.h + Resource1.best_res;

            // 用自定义事件 传递信息
            frmAddFile addVid = new frmAddFile(tips, v.intval, v.filelist, frmAddFile.FileType.Video);
            addVid.FileChanged += new frmAddFile.FileChangedEventHandler(VideoChanged);
            addVid.ShowDialog();
        }

        private void tsAddBackimage_Click(object sender, EventArgs e)
        {
            string tips = Resource1.screen_res + LocationCalc.SCREEN_W + " * " + LocationCalc.SCREEN_H + Resource1.best_res;
            int intval = int.Parse(Profile.getBackImageIntval());

            // 用自定义事件 传递信息
            frmAddFile ab = new frmAddFile(tips, intval, Profile.getBackImage(), frmAddFile.FileType.Image);
            ab.FileChanged += new frmAddFile.FileChangedEventHandler(BgChanged);
            ab.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }

        private void btOpenScene_Click(object sender, EventArgs e)
        {
            cmOpenScene_Click(sender, e);
        }

        private void btCloseScene_Click(object sender, EventArgs e)
        {
            cmCloseScene_Click(sender, e);
        }

        private void setScreenWH()
        {
            lbScreenWH.Text = LocationCalc.SCREEN_W + "," + LocationCalc.SCREEN_H;
        }

        public void OnActivate()
        {
            setScreenWH();
        }

        private void cmModify_Click(object sender, EventArgs e)
        {
            OpenScene();
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            btSceneDel_Click_1(sender, e);
        }

        private void cmCooy_Click(object sender, EventArgs e)
        {
            string dir = Path.Combine(App.SceneDir, lbScene.GetItemText(lbScene.SelectedItem));
            string dir2 = Path.Combine(App.SceneDir, lbScene.GetItemText(lbScene.SelectedItem) + "_copy");

            Util.DirectoryCopy(dir, dir2, true);
            lbScene.Items.Add(lbScene.GetItemText(lbScene.SelectedItem) + "_copy");
        }

        private void cmRename_Click(object sender, EventArgs e)
        {
            btSceneRename_Click(sender, e);
        }

    }
}