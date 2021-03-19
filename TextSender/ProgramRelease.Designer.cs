namespace TextSender
{
    partial class ProgramRelease
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramRelease));
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbSelAllBox = new System.Windows.Forms.CheckBox();
            this.lbBoxOnline = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbAllSceme = new System.Windows.Forms.ListBox();
            this.lbSelScene = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btDeselect = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btSelect = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btPlayLoop = new MaterialSkin.Controls.MaterialRadioButton();
            this.btPlayTiming = new MaterialSkin.Controls.MaterialRadioButton();
            this.gbModeSet = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btSend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btSendUsb = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbBoxOnline, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.cbSelAllBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Name = "panel1";
            // 
            // cbSelAllBox
            // 
            resources.ApplyResources(this.cbSelAllBox, "cbSelAllBox");
            this.cbSelAllBox.Name = "cbSelAllBox";
            this.cbSelAllBox.UseVisualStyleBackColor = true;
            this.cbSelAllBox.CheckedChanged += new System.EventHandler(this.cbSelAllBox_CheckedChanged);
            // 
            // lbBoxOnline
            // 
            resources.ApplyResources(this.lbBoxOnline, "lbBoxOnline");
            this.lbBoxOnline.CheckOnClick = true;
            this.lbBoxOnline.FormattingEnabled = true;
            this.lbBoxOnline.Name = "lbBoxOnline";
            this.lbBoxOnline.SelectedIndexChanged += new System.EventHandler(this.lbBoxOnline_SelectedIndexChanged);
            this.lbBoxOnline.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbBoxOnline_MouseUp);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lbAllSceme
            // 
            resources.ApplyResources(this.lbAllSceme, "lbAllSceme");
            this.lbAllSceme.FormattingEnabled = true;
            this.lbAllSceme.Name = "lbAllSceme";
            this.tableLayoutPanel2.SetRowSpan(this.lbAllSceme, 2);
            this.lbAllSceme.SelectedIndexChanged += new System.EventHandler(this.lbAllSceme_SelectedIndexChanged);
            // 
            // lbSelScene
            // 
            resources.ApplyResources(this.lbSelScene, "lbSelScene");
            this.lbSelScene.FormattingEnabled = true;
            this.lbSelScene.Name = "lbSelScene";
            this.tableLayoutPanel2.SetRowSpan(this.lbSelScene, 2);
            this.lbSelScene.SelectedIndexChanged += new System.EventHandler(this.lbAllSceme_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lbAllSceme, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbSelScene, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btDeselect, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btSelect, 1, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btDeselect
            // 
            resources.ApplyResources(this.btDeselect, "btDeselect");
            this.btDeselect.Depth = 0;
            this.btDeselect.MouseState = MaterialSkin.MouseState.HOVER;
            this.btDeselect.Name = "btDeselect";
            this.btDeselect.Primary = true;
            this.btDeselect.UseVisualStyleBackColor = true;
            this.btDeselect.Click += new System.EventHandler(this.btDeselect_Click);
            // 
            // btSelect
            // 
            resources.ApplyResources(this.btSelect, "btSelect");
            this.btSelect.Depth = 0;
            this.btSelect.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSelect.Name = "btSelect";
            this.btSelect.Primary = true;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.gbModeSet);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Controls.Add(this.btPlayLoop);
            this.groupBox7.Controls.Add(this.btPlayTiming);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // btPlayLoop
            // 
            resources.ApplyResources(this.btPlayLoop, "btPlayLoop");
            this.btPlayLoop.Checked = true;
            this.btPlayLoop.Depth = 0;
            this.btPlayLoop.MouseLocation = new System.Drawing.Point(-1, -1);
            this.btPlayLoop.MouseState = MaterialSkin.MouseState.HOVER;
            this.btPlayLoop.Name = "btPlayLoop";
            this.btPlayLoop.Ripple = true;
            this.btPlayLoop.TabStop = true;
            this.btPlayLoop.UseVisualStyleBackColor = true;
            this.btPlayLoop.CheckedChanged += new System.EventHandler(this.btPlayLoop_CheckedChanged);
            this.btPlayLoop.Click += new System.EventHandler(this.btPlayLoop_Click);
            // 
            // btPlayTiming
            // 
            resources.ApplyResources(this.btPlayTiming, "btPlayTiming");
            this.btPlayTiming.Depth = 0;
            this.btPlayTiming.MouseLocation = new System.Drawing.Point(-1, -1);
            this.btPlayTiming.MouseState = MaterialSkin.MouseState.HOVER;
            this.btPlayTiming.Name = "btPlayTiming";
            this.btPlayTiming.Ripple = true;
            this.btPlayTiming.UseVisualStyleBackColor = true;
            this.btPlayTiming.CheckedChanged += new System.EventHandler(this.btPlayTiming_CheckedChanged);
            this.btPlayTiming.Click += new System.EventHandler(this.btPlayTiming_Click);
            // 
            // gbModeSet
            // 
            resources.ApplyResources(this.gbModeSet, "gbModeSet");
            this.gbModeSet.Name = "gbModeSet";
            this.gbModeSet.TabStop = false;
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.btSend);
            this.groupBox6.Controls.Add(this.btSendUsb);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // btSend
            // 
            resources.ApplyResources(this.btSend, "btSend");
            this.btSend.Depth = 0;
            this.btSend.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSend.Name = "btSend";
            this.btSend.Primary = true;
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // btSendUsb
            // 
            resources.ApplyResources(this.btSendUsb, "btSendUsb");
            this.btSendUsb.Depth = 0;
            this.btSendUsb.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSendUsb.Name = "btSendUsb";
            this.btSendUsb.Primary = true;
            this.btSendUsb.UseVisualStyleBackColor = true;
            this.btSendUsb.Click += new System.EventHandler(this.btSendUsb_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ProgramRelease
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProgramRelease";
            this.Load += new System.EventHandler(this.ucProgSend_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbAllSceme;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lbSelScene;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialRaisedButton btDeselect;
        private MaterialSkin.Controls.MaterialRaisedButton btSelect;
        private System.Windows.Forms.CheckedListBox lbBoxOnline;
        private MaterialSkin.Controls.MaterialRaisedButton btSendUsb;
        private MaterialSkin.Controls.MaterialRaisedButton btSend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbSelAllBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private MaterialSkin.Controls.MaterialRadioButton btPlayLoop;
        private MaterialSkin.Controls.MaterialRadioButton btPlayTiming;
        private System.Windows.Forms.GroupBox gbModeSet;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}
