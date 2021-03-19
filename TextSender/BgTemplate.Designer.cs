namespace TextSender
{
    partial class BgTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BgTemplate));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxEx1 = new Ctrl.GroupBoxEx();
            this.lvImg = new System.Windows.Forms.ListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxEx3 = new Ctrl.GroupBoxEx();
            this.button3 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.button2 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label1 = new System.Windows.Forms.Label();
            this.workerDecode = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxEx1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imgList, "imgList");
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.groupBoxEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // groupBoxEx1
            // 
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(169)))), ((int)(((byte)(254)))));
            this.groupBoxEx1.Controls.Add(this.lvImg);
            this.groupBoxEx1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            // 
            // lvImg
            // 
            resources.ApplyResources(this.lvImg, "lvImg");
            this.lvImg.CheckBoxes = true;
            this.lvImg.Name = "lvImg";
            this.lvImg.UseCompatibleStateImageBehavior = false;
            this.lvImg.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvImg_ItemSelectionChanged);
            this.lvImg.SelectedIndexChanged += new System.EventHandler(this.lvImg_SelectedIndexChanged);
            this.lvImg.MouseCaptureChanged += new System.EventHandler(this.lvImg_MouseCaptureChanged);
            this.lvImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvImg_MouseMove);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.groupBoxEx3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // groupBoxEx3
            // 
            resources.ApplyResources(this.groupBoxEx3, "groupBoxEx3");
            this.groupBoxEx3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(169)))), ((int)(((byte)(254)))));
            this.groupBoxEx3.Controls.Add(this.button3);
            this.groupBoxEx3.Controls.Add(this.button2);
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.TabStop = false;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Depth = 0;
            this.button3.MouseState = MaterialSkin.MouseState.HOVER;
            this.button3.Name = "button3";
            this.button3.Primary = true;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Depth = 0;
            this.button2.MouseState = MaterialSkin.MouseState.HOVER;
            this.button2.Name = "button2";
            this.button2.Primary = true;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // workerDecode
            // 
            this.workerDecode.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerDecode_DoWork);
            this.workerDecode.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerDecode_RunWorkerCompleted);
            // 
            // BgTemplate
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BgTemplate";
            this.Load += new System.EventHandler(this.AddBackgroud_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxEx1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Ctrl.GroupBoxEx groupBoxEx1;
        private MaterialSkin.Controls.MaterialRaisedButton button2;
        private MaterialSkin.Controls.MaterialRaisedButton button3;
        private System.Windows.Forms.ListView lvImg;
        private System.Windows.Forms.ImageList imgList;
        private Ctrl.GroupBoxEx groupBoxEx3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.ComponentModel.BackgroundWorker workerDecode;
        private System.Windows.Forms.Label label1;
    }
}