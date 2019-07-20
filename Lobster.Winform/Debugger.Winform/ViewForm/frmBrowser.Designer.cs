namespace Debugger.Winform.ViewForm
{
    partial class frmBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrowser));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnhome = new System.Windows.Forms.ToolStripButton();
            this.btnback = new System.Windows.Forms.ToolStripButton();
            this.btnadvance = new System.Windows.Forms.ToolStripButton();
            this.txturl = new Debugger.Winform.Common.ToolStripSpringTextBox();
            this.btnrefresh = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnhome,
            this.btnback,
            this.btnadvance,
            this.txturl,
            this.btnrefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(629, 27);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnhome
            // 
            this.btnhome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnhome.Image = ((System.Drawing.Image)(resources.GetObject("btnhome.Image")));
            this.btnhome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnhome.Name = "btnhome";
            this.btnhome.Size = new System.Drawing.Size(23, 24);
            this.btnhome.Text = "主页";
            this.btnhome.Click += new System.EventHandler(this.btnhome_Click);
            // 
            // btnback
            // 
            this.btnback.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnback.Image = ((System.Drawing.Image)(resources.GetObject("btnback.Image")));
            this.btnback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(23, 24);
            this.btnback.Text = "后退";
            this.btnback.Click += new System.EventHandler(this.btnback_Click);
            // 
            // btnadvance
            // 
            this.btnadvance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnadvance.Image = ((System.Drawing.Image)(resources.GetObject("btnadvance.Image")));
            this.btnadvance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnadvance.Name = "btnadvance";
            this.btnadvance.Size = new System.Drawing.Size(23, 24);
            this.btnadvance.Text = "前进";
            this.btnadvance.Click += new System.EventHandler(this.btnadvance_Click);
            // 
            // txturl
            // 
            this.txturl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txturl.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(503, 23);
            this.txturl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txturl_KeyDown);
            // 
            // btnrefresh
            // 
            this.btnrefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnrefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnrefresh.Image")));
            this.btnrefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(23, 24);
            this.btnrefresh.Text = "刷新";
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 27);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(629, 250);
            this.panelMain.TabIndex = 2;
            // 
            // frmBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 277);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmBrowser";
            this.Text = "浏览器";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnhome;
        private System.Windows.Forms.ToolStripButton btnback;
        private System.Windows.Forms.ToolStripButton btnadvance;
        private Common.ToolStripSpringTextBox txturl;
        private System.Windows.Forms.ToolStripButton btnrefresh;
        private System.Windows.Forms.Panel panelMain;
    }
}