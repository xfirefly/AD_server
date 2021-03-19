namespace TextSender
{
    partial class BoxCtrl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoxCtrl));
            this.cbSelAllBox = new System.Windows.Forms.CheckBox();
            this.lbBox = new System.Windows.Forms.CheckedListBox();
            this.cmBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSetBoxName = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetBoxId = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetTime = new System.Windows.Forms.ToolStripMenuItem();
            this.miPrevScene = new System.Windows.Forms.ToolStripMenuItem();
            this.miNextScene = new System.Windows.Forms.ToolStripMenuItem();
            this.miScreenCap = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetVolume = new System.Windows.Forms.ToolStripMenuItem();
            this.miRebootBox = new System.Windows.Forms.ToolStripMenuItem();
            this.miInstallApk = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pbScreen = new System.Windows.Forms.PictureBox();
            this.gbCtrl = new Ctrl.GroupBoxEx();
            this.tlpCtrl = new System.Windows.Forms.TableLayoutPanel();
            this.btSetTime = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btPrevScene = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btNextScene = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btSetWanIP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btSetBoxName = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btSetVolume = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btScreenCap = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btRebootBox = new MaterialSkin.Controls.MaterialRaisedButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreen)).BeginInit();
            this.gbCtrl.SuspendLayout();
            this.tlpCtrl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSelAllBox
            // 
            resources.ApplyResources(this.cbSelAllBox, "cbSelAllBox");
            this.cbSelAllBox.Name = "cbSelAllBox";
            this.cbSelAllBox.UseVisualStyleBackColor = true;
            this.cbSelAllBox.CheckedChanged += new System.EventHandler(this.cbSelAllBox_CheckedChanged);
            // 
            // lbBox
            // 
            resources.ApplyResources(this.lbBox, "lbBox");
            this.lbBox.ContextMenuStrip = this.cmBox;
            this.lbBox.FormattingEnabled = true;
            this.lbBox.Name = "lbBox";
            this.lbBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbBox_MouseUp);
            // 
            // cmBox
            // 
            resources.ApplyResources(this.cmBox, "cmBox");
            this.cmBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSetBoxName,
            this.miSetBoxId,
            this.miSetTime,
            this.miPrevScene,
            this.miNextScene,
            this.miScreenCap,
            this.miSetVolume,
            this.miRebootBox,
            this.miInstallApk});
            this.cmBox.Name = "cmBox";
            this.cmBox.Opening += new System.ComponentModel.CancelEventHandler(this.cmBox_Opening);
            // 
            // miSetBoxName
            // 
            resources.ApplyResources(this.miSetBoxName, "miSetBoxName");
            this.miSetBoxName.Name = "miSetBoxName";
            this.miSetBoxName.Click += new System.EventHandler(this.miSetBoxName_Click);
            // 
            // miSetBoxId
            // 
            resources.ApplyResources(this.miSetBoxId, "miSetBoxId");
            this.miSetBoxId.Image = global::TextSender.Properties.Resources.ID_icon;
            this.miSetBoxId.Name = "miSetBoxId";
            this.miSetBoxId.Click += new System.EventHandler(this.miSetBoxId_Click);
            // 
            // miSetTime
            // 
            resources.ApplyResources(this.miSetTime, "miSetTime");
            this.miSetTime.Name = "miSetTime";
            this.miSetTime.Click += new System.EventHandler(this.miSetTime_Click);
            // 
            // miPrevScene
            // 
            resources.ApplyResources(this.miPrevScene, "miPrevScene");
            this.miPrevScene.Image = global::TextSender.Properties.Resources.Actions_go_previous_view_icon;
            this.miPrevScene.Name = "miPrevScene";
            this.miPrevScene.Click += new System.EventHandler(this.miPrevScene_Click);
            // 
            // miNextScene
            // 
            resources.ApplyResources(this.miNextScene, "miNextScene");
            this.miNextScene.Image = global::TextSender.Properties.Resources.Actions_go_next_view_icon;
            this.miNextScene.Name = "miNextScene";
            this.miNextScene.Click += new System.EventHandler(this.miNextScene_Click);
            // 
            // miScreenCap
            // 
            resources.ApplyResources(this.miScreenCap, "miScreenCap");
            this.miScreenCap.Name = "miScreenCap";
            this.miScreenCap.Click += new System.EventHandler(this.miScreenCap_Click);
            // 
            // miSetVolume
            // 
            resources.ApplyResources(this.miSetVolume, "miSetVolume");
            this.miSetVolume.Image = global::TextSender.Properties.Resources.Status_audio_volume_high_icon;
            this.miSetVolume.Name = "miSetVolume";
            this.miSetVolume.Click += new System.EventHandler(this.miSetVolume_Click);
            // 
            // miRebootBox
            // 
            resources.ApplyResources(this.miRebootBox, "miRebootBox");
            this.miRebootBox.Name = "miRebootBox";
            this.miRebootBox.Click += new System.EventHandler(this.miRebootBox_Click);
            // 
            // miInstallApk
            // 
            resources.ApplyResources(this.miInstallApk, "miInstallApk");
            this.miInstallApk.Name = "miInstallApk";
            this.miInstallApk.Click += new System.EventHandler(this.miInstallApk_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
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
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbScreen, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.gbCtrl, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pbScreen
            // 
            resources.ApplyResources(this.pbScreen, "pbScreen");
            this.pbScreen.Name = "pbScreen";
            this.pbScreen.TabStop = false;
            // 
            // gbCtrl
            // 
            resources.ApplyResources(this.gbCtrl, "gbCtrl");
            this.gbCtrl.BorderColor = System.Drawing.Color.Empty;
            this.gbCtrl.Controls.Add(this.tlpCtrl);
            this.gbCtrl.Name = "gbCtrl";
            this.gbCtrl.TabStop = false;
            // 
            // tlpCtrl
            // 
            resources.ApplyResources(this.tlpCtrl, "tlpCtrl");
            this.tlpCtrl.Controls.Add(this.btSetTime, 1, 0);
            this.tlpCtrl.Controls.Add(this.btPrevScene, 2, 0);
            this.tlpCtrl.Controls.Add(this.btNextScene, 3, 0);
            this.tlpCtrl.Controls.Add(this.btSetWanIP, 7, 0);
            this.tlpCtrl.Controls.Add(this.btSetBoxName, 0, 0);
            this.tlpCtrl.Controls.Add(this.btSetVolume, 4, 0);
            this.tlpCtrl.Controls.Add(this.btScreenCap, 5, 0);
            this.tlpCtrl.Controls.Add(this.btRebootBox, 6, 0);
            this.tlpCtrl.Name = "tlpCtrl";
            // 
            // btSetTime
            // 
            resources.ApplyResources(this.btSetTime, "btSetTime");
            this.btSetTime.Depth = 0;
            this.btSetTime.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSetTime.Name = "btSetTime";
            this.btSetTime.Primary = true;
            this.btSetTime.UseVisualStyleBackColor = true;
            this.btSetTime.Click += new System.EventHandler(this.miSetTime_Click);
            // 
            // btPrevScene
            // 
            resources.ApplyResources(this.btPrevScene, "btPrevScene");
            this.btPrevScene.Depth = 0;
            this.btPrevScene.MouseState = MaterialSkin.MouseState.HOVER;
            this.btPrevScene.Name = "btPrevScene";
            this.btPrevScene.Primary = true;
            this.btPrevScene.UseVisualStyleBackColor = true;
            this.btPrevScene.Click += new System.EventHandler(this.miPrevScene_Click);
            // 
            // btNextScene
            // 
            resources.ApplyResources(this.btNextScene, "btNextScene");
            this.btNextScene.Depth = 0;
            this.btNextScene.MouseState = MaterialSkin.MouseState.HOVER;
            this.btNextScene.Name = "btNextScene";
            this.btNextScene.Primary = true;
            this.btNextScene.UseVisualStyleBackColor = true;
            this.btNextScene.Click += new System.EventHandler(this.miNextScene_Click);
            // 
            // btSetWanIP
            // 
            resources.ApplyResources(this.btSetWanIP, "btSetWanIP");
            this.btSetWanIP.Depth = 0;
            this.btSetWanIP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSetWanIP.Name = "btSetWanIP";
            this.btSetWanIP.Primary = true;
            this.btSetWanIP.UseVisualStyleBackColor = true;
            this.btSetWanIP.Click += new System.EventHandler(this.btSetWanIP_Click);
            // 
            // btSetBoxName
            // 
            resources.ApplyResources(this.btSetBoxName, "btSetBoxName");
            this.btSetBoxName.Depth = 0;
            this.btSetBoxName.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSetBoxName.Name = "btSetBoxName";
            this.btSetBoxName.Primary = true;
            this.btSetBoxName.UseVisualStyleBackColor = true;
            this.btSetBoxName.Click += new System.EventHandler(this.miSetBoxName_Click);
            // 
            // btSetVolume
            // 
            resources.ApplyResources(this.btSetVolume, "btSetVolume");
            this.btSetVolume.Depth = 0;
            this.btSetVolume.MouseState = MaterialSkin.MouseState.HOVER;
            this.btSetVolume.Name = "btSetVolume";
            this.btSetVolume.Primary = true;
            this.btSetVolume.UseVisualStyleBackColor = true;
            this.btSetVolume.Click += new System.EventHandler(this.miSetVolume_Click);
            // 
            // btScreenCap
            // 
            resources.ApplyResources(this.btScreenCap, "btScreenCap");
            this.btScreenCap.Depth = 0;
            this.btScreenCap.MouseState = MaterialSkin.MouseState.HOVER;
            this.btScreenCap.Name = "btScreenCap";
            this.btScreenCap.Primary = true;
            this.btScreenCap.UseVisualStyleBackColor = true;
            this.btScreenCap.Click += new System.EventHandler(this.miScreenCap_Click);
            // 
            // btRebootBox
            // 
            resources.ApplyResources(this.btRebootBox, "btRebootBox");
            this.btRebootBox.Depth = 0;
            this.btRebootBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.btRebootBox.Name = "btRebootBox";
            this.btRebootBox.Primary = true;
            this.btRebootBox.UseVisualStyleBackColor = true;
            this.btRebootBox.Click += new System.EventHandler(this.miRebootBox_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbBox, 0, 1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.cbSelAllBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Name = "panel1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // BoxCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BoxCtrl";
            this.cmBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreen)).EndInit();
            this.gbCtrl.ResumeLayout(false);
            this.tlpCtrl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSelAllBox;
        private System.Windows.Forms.CheckedListBox lbBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip cmBox;
        private System.Windows.Forms.ToolStripMenuItem miSetBoxName;
        private System.Windows.Forms.ToolStripMenuItem miSetBoxId;
        private System.Windows.Forms.ToolStripMenuItem miSetTime;
        private System.Windows.Forms.ToolStripMenuItem miPrevScene;
        private System.Windows.Forms.ToolStripMenuItem miNextScene;
        private System.Windows.Forms.ToolStripMenuItem miScreenCap;
        private System.Windows.Forms.ToolStripMenuItem miSetVolume;
        private System.Windows.Forms.ToolStripMenuItem miRebootBox;
        private System.Windows.Forms.ToolStripMenuItem miInstallApk;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbScreen;
        private Ctrl.GroupBoxEx gbCtrl;
        private MaterialSkin.Controls.MaterialRaisedButton btScreenCap;
        private MaterialSkin.Controls.MaterialRaisedButton btNextScene;
        private MaterialSkin.Controls.MaterialRaisedButton btPrevScene;
        private MaterialSkin.Controls.MaterialRaisedButton btSetTime;
        private MaterialSkin.Controls.MaterialRaisedButton btSetBoxName;
        private MaterialSkin.Controls.MaterialRaisedButton btRebootBox;
        private MaterialSkin.Controls.MaterialRaisedButton btSetVolume;
        private System.Windows.Forms.TableLayoutPanel tlpCtrl;
        private MaterialSkin.Controls.MaterialRaisedButton btSetWanIP;
    }
}
