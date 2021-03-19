namespace TextSender
{
    partial class frmHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHome));
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.tbcHome = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rb1920 = new MaterialSkin.Controls.MaterialRadioButton();
            this.rb1280 = new MaterialSkin.Controls.MaterialRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbWan = new MaterialSkin.Controls.MaterialRadioButton();
            this.rbLan = new MaterialSkin.Controls.MaterialRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbReadme = new System.Windows.Forms.RichTextBox();
            this.btTheme = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btLang = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tbcHome.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            resources.ApplyResources(this.materialTabSelector1, "materialTabSelector1");
            this.materialTabSelector1.BaseTabControl = this.tbcHome;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            // 
            // tbcHome
            // 
            resources.ApplyResources(this.tbcHome, "tbcHome");
            this.tbcHome.Controls.Add(this.tabPage1);
            this.tbcHome.Controls.Add(this.tabPage2);
            this.tbcHome.Controls.Add(this.tabPage5);
            this.tbcHome.Controls.Add(this.tabPage3);
            this.tbcHome.Controls.Add(this.tabPage4);
            this.tbcHome.Controls.Add(this.tabPage6);
            this.tbcHome.Depth = 0;
            this.tbcHome.MouseState = MaterialSkin.MouseState.HOVER;
            this.tbcHome.Name = "tbcHome";
            this.tbcHome.SelectedIndex = 0;
            this.tbcHome.SelectedIndexChanged += new System.EventHandler(this.tbcHome_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Name = "tabPage1";
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Name = "tabPage2";
            // 
            // tabPage5
            // 
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Name = "tabPage3";
            // 
            // tabPage4
            // 
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Name = "tabPage4";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.materialDivider2);
            this.groupBox1.Controls.Add(this.materialDivider1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.rb1920);
            this.panel2.Controls.Add(this.rb1280);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Name = "panel2";
            // 
            // rb1920
            // 
            resources.ApplyResources(this.rb1920, "rb1920");
            this.rb1920.Depth = 0;
            this.rb1920.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rb1920.MouseState = MaterialSkin.MouseState.HOVER;
            this.rb1920.Name = "rb1920";
            this.rb1920.Ripple = true;
            this.rb1920.UseVisualStyleBackColor = true;
            this.rb1920.CheckedChanged += new System.EventHandler(this.rb1920_CheckedChanged);
            // 
            // rb1280
            // 
            resources.ApplyResources(this.rb1280, "rb1280");
            this.rb1280.Checked = true;
            this.rb1280.Depth = 0;
            this.rb1280.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rb1280.MouseState = MaterialSkin.MouseState.HOVER;
            this.rb1280.Name = "rb1280";
            this.rb1280.Ripple = true;
            this.rb1280.TabStop = true;
            this.rb1280.UseVisualStyleBackColor = true;
            this.rb1280.CheckedChanged += new System.EventHandler(this.rb1280_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rbWan);
            this.panel1.Controls.Add(this.rbLan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Name = "panel1";
            // 
            // rbWan
            // 
            resources.ApplyResources(this.rbWan, "rbWan");
            this.rbWan.Depth = 0;
            this.rbWan.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rbWan.MouseState = MaterialSkin.MouseState.HOVER;
            this.rbWan.Name = "rbWan";
            this.rbWan.Ripple = true;
            this.rbWan.UseVisualStyleBackColor = true;
            this.rbWan.CheckedChanged += new System.EventHandler(this.rbWan_CheckedChanged);
            // 
            // rbLan
            // 
            resources.ApplyResources(this.rbLan, "rbLan");
            this.rbLan.Checked = true;
            this.rbLan.Depth = 0;
            this.rbLan.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rbLan.MouseState = MaterialSkin.MouseState.HOVER;
            this.rbLan.Name = "rbLan";
            this.rbLan.Ripple = true;
            this.rbLan.TabStop = true;
            this.rbLan.UseVisualStyleBackColor = true;
            this.rbLan.CheckedChanged += new System.EventHandler(this.rbLan_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // materialDivider2
            // 
            resources.ApplyResources(this.materialDivider2, "materialDivider2");
            this.materialDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider2.Depth = 0;
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            // 
            // materialDivider1
            // 
            resources.ApplyResources(this.materialDivider1, "materialDivider1");
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            // 
            // tabPage6
            // 
            resources.ApplyResources(this.tabPage6, "tabPage6");
            this.tabPage6.BackColor = System.Drawing.Color.White;
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Name = "tabPage6";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.rtbReadme);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // rtbReadme
            // 
            resources.ApplyResources(this.rtbReadme, "rtbReadme");
            this.rtbReadme.BackColor = System.Drawing.Color.White;
            this.rtbReadme.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbReadme.Name = "rtbReadme";
            this.rtbReadme.ReadOnly = true;
            // 
            // btTheme
            // 
            resources.ApplyResources(this.btTheme, "btTheme");
            this.btTheme.BackColor = System.Drawing.SystemColors.Control;
            this.btTheme.Depth = 0;
            this.btTheme.MouseState = MaterialSkin.MouseState.HOVER;
            this.btTheme.Name = "btTheme";
            this.btTheme.Primary = true;
            this.btTheme.UseVisualStyleBackColor = false;
            this.btTheme.Click += new System.EventHandler(this.btTheme_Click);
            // 
            // btLang
            // 
            resources.ApplyResources(this.btLang, "btLang");
            this.btLang.BackColor = System.Drawing.SystemColors.Control;
            this.btLang.Depth = 0;
            this.btLang.MouseState = MaterialSkin.MouseState.HOVER;
            this.btLang.Name = "btLang";
            this.btLang.Primary = true;
            this.btLang.UseVisualStyleBackColor = false;
            this.btLang.Click += new System.EventHandler(this.btLang_Click);
            // 
            // frmHome
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btLang);
            this.Controls.Add(this.btTheme);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.tbcHome);
            this.MaximizeBox = false;
            this.Name = "frmHome";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHome_FormClosing);
            this.Load += new System.EventHandler(this.frmHome_Load);
            this.tbcHome.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl tbcHome;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private MaterialSkin.Controls.MaterialRaisedButton btTheme;
        private MaterialSkin.Controls.MaterialRaisedButton btLang;
        private System.Windows.Forms.TabPage tabPage6;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialRadioButton rbWan;
        private MaterialSkin.Controls.MaterialRadioButton rbLan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbReadme;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialRadioButton rb1920;
        private MaterialSkin.Controls.MaterialRadioButton rb1280;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage5;
    }
}