namespace TextSender
{
    partial class AddBackgroud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBackgroud));
            this.groupBoxEx1 = new Ctrl.GroupBoxEx();
            this.lbTips = new System.Windows.Forms.Label();
            this.groupBoxEx3 = new Ctrl.GroupBoxEx();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBoxEx2 = new Ctrl.GroupBoxEx();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvImg = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxEx1.SuspendLayout();
            this.groupBoxEx3.SuspendLayout();
            this.groupBoxEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEx1
            // 
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(169)))), ((int)(((byte)(254)))));
            this.groupBoxEx1.Controls.Add(this.lbTips);
            this.groupBoxEx1.Controls.Add(this.groupBoxEx3);
            this.groupBoxEx1.Controls.Add(this.groupBoxEx2);
            this.groupBoxEx1.Controls.Add(this.lvImg);
            this.groupBoxEx1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            this.groupBoxEx1.Enter += new System.EventHandler(this.groupBoxEx1_Enter);
            // 
            // lbTips
            // 
            resources.ApplyResources(this.lbTips, "lbTips");
            this.lbTips.Name = "lbTips";
            // 
            // groupBoxEx3
            // 
            resources.ApplyResources(this.groupBoxEx3, "groupBoxEx3");
            this.groupBoxEx3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(169)))), ((int)(((byte)(254)))));
            this.groupBoxEx3.Controls.Add(this.button3);
            this.groupBoxEx3.Controls.Add(this.button2);
            this.groupBoxEx3.Controls.Add(this.button4);
            this.groupBoxEx3.Name = "groupBoxEx3";
            this.groupBoxEx3.TabStop = false;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBoxEx2
            // 
            resources.ApplyResources(this.groupBoxEx2, "groupBoxEx2");
            this.groupBoxEx2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(169)))), ((int)(((byte)(254)))));
            this.groupBoxEx2.Controls.Add(this.textBox1);
            this.groupBoxEx2.Controls.Add(this.label1);
            this.groupBoxEx2.Name = "groupBoxEx2";
            this.groupBoxEx2.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lvImg
            // 
            resources.ApplyResources(this.lvImg, "lvImg");
            this.lvImg.CheckBoxes = true;
            this.lvImg.Name = "lvImg";
            this.lvImg.UseCompatibleStateImageBehavior = false;
            this.lvImg.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvImg_ItemSelectionChanged);
            this.lvImg.SelectedIndexChanged += new System.EventHandler(this.lvImg_SelectedIndexChanged);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imgList, "imgList");
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // AddBackgroud
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEx1);
            this.Name = "AddBackgroud";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddBackgroud_FormClosed);
            this.Load += new System.EventHandler(this.AddBackgroud_Load);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            this.groupBoxEx3.ResumeLayout(false);
            this.groupBoxEx2.ResumeLayout(false);
            this.groupBoxEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Ctrl.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView lvImg;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button button4;
        private Ctrl.GroupBoxEx groupBoxEx3;
        private Ctrl.GroupBoxEx groupBoxEx2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTips;
    }
}