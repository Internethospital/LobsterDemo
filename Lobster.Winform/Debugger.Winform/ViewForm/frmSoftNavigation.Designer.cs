namespace Debugger.Winform.ViewForm
{
    partial class frmSoftNavigation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSoftNavigation));
            this.treePlugin = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除此控制器缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnreload = new System.Windows.Forms.ToolStripButton();
            this.btnClearAll = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treePlugin
            // 
            this.treePlugin.ContextMenuStrip = this.contextMenuStrip1;
            this.treePlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePlugin.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treePlugin.ImageIndex = 0;
            this.treePlugin.ImageList = this.imageList1;
            this.treePlugin.Location = new System.Drawing.Point(0, 25);
            this.treePlugin.Name = "treePlugin";
            this.treePlugin.SelectedImageIndex = 0;
            this.treePlugin.Size = new System.Drawing.Size(337, 466);
            this.treePlugin.TabIndex = 2;
            this.treePlugin.DoubleClick += new System.EventHandler(this.treePlugin_DoubleClick);
            this.treePlugin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treePlugin_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.清除此控制器缓存ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 48);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 清除此控制器缓存ToolStripMenuItem
            // 
            this.清除此控制器缓存ToolStripMenuItem.Name = "清除此控制器缓存ToolStripMenuItem";
            this.清除此控制器缓存ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.清除此控制器缓存ToolStripMenuItem.Text = "清除此控制器缓存";
            this.清除此控制器缓存ToolStripMenuItem.Click += new System.EventHandler(this.清除此控制器缓存ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "service.png");
            this.imageList1.Images.SetKeyName(1, "controller.png");
            this.imageList1.Images.SetKeyName(2, "method.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnreload,
            this.btnClearAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(337, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnreload
            // 
            this.btnreload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnreload.Image = ((System.Drawing.Image)(resources.GetObject("btnreload.Image")));
            this.btnreload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnreload.Name = "btnreload";
            this.btnreload.Size = new System.Drawing.Size(23, 22);
            this.btnreload.Text = "刷新";
            this.btnreload.Click += new System.EventHandler(this.btnreload_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
            this.btnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(23, 22);
            this.btnClearAll.Text = "清空缓存";
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // frmSoftNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 491);
            this.Controls.Add(this.treePlugin);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmSoftNavigation";
            this.Text = "云软件导航";
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treePlugin;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnreload;
        private System.Windows.Forms.ToolStripButton btnClearAll;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除此控制器缓存ToolStripMenuItem;
    }
}