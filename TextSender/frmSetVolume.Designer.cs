namespace TextSender
{
    partial class frmSetVolume
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetVolume));
            this.groupBoxEx1 = new Ctrl.GroupBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbar = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSetTimeData = new System.Windows.Forms.Button();
            this.groupBoxEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxEx1
            // 
            resources.ApplyResources(this.groupBoxEx1, "groupBoxEx1");
            this.groupBoxEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(169)))), ((int)(((byte)(254)))));
            this.groupBoxEx1.Controls.Add(this.label2);
            this.groupBoxEx1.Controls.Add(this.label1);
            this.groupBoxEx1.Controls.Add(this.tbar);
            this.groupBoxEx1.Controls.Add(this.button1);
            this.groupBoxEx1.Controls.Add(this.btnSetTimeData);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbar
            // 
            resources.ApplyResources(this.tbar, "tbar");
            this.tbar.Name = "tbar";
            this.tbar.Scroll += new System.EventHandler(this.tbar_Scroll_1);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSetTimeData
            // 
            resources.ApplyResources(this.btnSetTimeData, "btnSetTimeData");
            this.btnSetTimeData.Name = "btnSetTimeData";
            this.btnSetTimeData.UseVisualStyleBackColor = true;
            this.btnSetTimeData.Click += new System.EventHandler(this.btnSetTimeData_Click);
            // 
            // frmSetVolume
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSetVolume";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmSetTime_Load);
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSetTimeData;
        private Ctrl.GroupBoxEx groupBoxEx1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TrackBar tbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}