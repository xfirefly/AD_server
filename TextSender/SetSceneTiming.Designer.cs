namespace TextSender
{
    partial class SetSceneTiming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetSceneTiming));
            this.name = new System.Windows.Forms.Label();
            this.timing = new System.Windows.Forms.Label();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.setup = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // name
            // 
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
            // 
            // timing
            // 
            resources.ApplyResources(this.timing, "timing");
            this.timing.Name = "timing";
            // 
            // materialDivider1
            // 
            resources.ApplyResources(this.materialDivider1, "materialDivider1");
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            // 
            // setup
            // 
            resources.ApplyResources(this.setup, "setup");
            this.setup.Depth = 0;
            this.setup.MouseState = MaterialSkin.MouseState.HOVER;
            this.setup.Name = "setup";
            this.setup.Primary = true;
            this.setup.UseVisualStyleBackColor = true;
            this.setup.Click += new System.EventHandler(this.setup_Click);
            // 
            // SetSceneTiming
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.setup);
            this.Controls.Add(this.timing);
            this.Controls.Add(this.name);
            this.Name = "SetSceneTiming";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label timing;
        private MaterialSkin.Controls.MaterialRaisedButton setup;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
    }
}
