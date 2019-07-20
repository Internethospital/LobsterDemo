namespace Debugger.Winform.ViewForm
{
    partial class frmScriptNavigation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScriptNavigation));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnCollapse = new System.Windows.Forms.ToolStripButton();
            this.btnExpand = new System.Windows.Forms.ToolStripButton();
            this.btnSoftConfig = new System.Windows.Forms.ToolStripButton();
            this.toolServer = new System.Windows.Forms.ToolStripButton();
            this.treeScript = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看设计器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.新建云软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controllerpyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.发布云软件到服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取云软件从服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开所在文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新生成configxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnCollapse,
            this.btnExpand,
            this.btnSoftConfig,
            this.toolServer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(342, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCollapse
            // 
            this.btnCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCollapse.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapse.Image")));
            this.btnCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(23, 22);
            this.btnCollapse.Text = "折叠";
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // btnExpand
            // 
            this.btnExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExpand.Image = ((System.Drawing.Image)(resources.GetObject("btnExpand.Image")));
            this.btnExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(23, 22);
            this.btnExpand.Text = "展开";
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // btnSoftConfig
            // 
            this.btnSoftConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSoftConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnSoftConfig.Image")));
            this.btnSoftConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSoftConfig.Name = "btnSoftConfig";
            this.btnSoftConfig.Size = new System.Drawing.Size(23, 22);
            this.btnSoftConfig.Text = "配置CloudSoftConfig.xml";
            this.btnSoftConfig.Visible = false;
            this.btnSoftConfig.Click += new System.EventHandler(this.btnSoftConfig_Click);
            // 
            // toolServer
            // 
            this.toolServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolServer.Image = ((System.Drawing.Image)(resources.GetObject("toolServer.Image")));
            this.toolServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolServer.Name = "toolServer";
            this.toolServer.Size = new System.Drawing.Size(23, 22);
            this.toolServer.Text = "查看已发布的云软件";
            this.toolServer.Click += new System.EventHandler(this.toolServer_Click);
            // 
            // treeScript
            // 
            this.treeScript.ContextMenuStrip = this.contextMenuStrip1;
            this.treeScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeScript.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeScript.ImageIndex = 0;
            this.treeScript.ImageList = this.imageList1;
            this.treeScript.Location = new System.Drawing.Point(0, 25);
            this.treeScript.Name = "treeScript";
            this.treeScript.SelectedImageIndex = 0;
            this.treeScript.Size = new System.Drawing.Size(342, 465);
            this.treeScript.TabIndex = 1;
            this.treeScript.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeScript_AfterLabelEdit);
            this.treeScript.DoubleClick += new System.EventHandler(this.treeScript_DoubleClick);
            this.treeScript.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeScript_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看代码ToolStripMenuItem,
            this.查看设计器ToolStripMenuItem,
            this.toolStripSeparator1,
            this.新建云软件ToolStripMenuItem,
            this.添加文件ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.toolStripSeparator2,
            this.发布云软件到服务器ToolStripMenuItem,
            this.获取云软件从服务器ToolStripMenuItem,
            this.打开所在文件夹ToolStripMenuItem,
            this.重新生成configxmlToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 236);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // 查看代码ToolStripMenuItem
            // 
            this.查看代码ToolStripMenuItem.Name = "查看代码ToolStripMenuItem";
            this.查看代码ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.查看代码ToolStripMenuItem.Text = "查看代码";
            this.查看代码ToolStripMenuItem.Click += new System.EventHandler(this.查看代码ToolStripMenuItem_Click);
            // 
            // 查看设计器ToolStripMenuItem
            // 
            this.查看设计器ToolStripMenuItem.Name = "查看设计器ToolStripMenuItem";
            this.查看设计器ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.查看设计器ToolStripMenuItem.Text = "查看设计器";
            this.查看设计器ToolStripMenuItem.Click += new System.EventHandler(this.查看设计器ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // 新建云软件ToolStripMenuItem
            // 
            this.新建云软件ToolStripMenuItem.Name = "新建云软件ToolStripMenuItem";
            this.新建云软件ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.新建云软件ToolStripMenuItem.Text = "新建云软件";
            this.新建云软件ToolStripMenuItem.Click += new System.EventHandler(this.新建云软件ToolStripMenuItem_Click);
            // 
            // 添加文件ToolStripMenuItem
            // 
            this.添加文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerpyToolStripMenuItem,
            this.modelToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.添加文件ToolStripMenuItem.Name = "添加文件ToolStripMenuItem";
            this.添加文件ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.添加文件ToolStripMenuItem.Text = "添加文件";
            // 
            // controllerpyToolStripMenuItem
            // 
            this.controllerpyToolStripMenuItem.Name = "controllerpyToolStripMenuItem";
            this.controllerpyToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.controllerpyToolStripMenuItem.Text = "Controller";
            this.controllerpyToolStripMenuItem.Click += new System.EventHandler(this.controllerpyToolStripMenuItem_Click);
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.modelToolStripMenuItem.Text = "Model";
            this.modelToolStripMenuItem.Click += new System.EventHandler(this.modelToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.重命名ToolStripMenuItem.Text = "重命名";
            this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // 发布云软件到服务器ToolStripMenuItem
            // 
            this.发布云软件到服务器ToolStripMenuItem.Name = "发布云软件到服务器ToolStripMenuItem";
            this.发布云软件到服务器ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.发布云软件到服务器ToolStripMenuItem.Text = "发布云软件到服务器";
            this.发布云软件到服务器ToolStripMenuItem.Click += new System.EventHandler(this.发布云软件到服务器ToolStripMenuItem_Click);
            // 
            // 获取云软件从服务器ToolStripMenuItem
            // 
            this.获取云软件从服务器ToolStripMenuItem.Name = "获取云软件从服务器ToolStripMenuItem";
            this.获取云软件从服务器ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.获取云软件从服务器ToolStripMenuItem.Text = "获取云软件从服务器";
            this.获取云软件从服务器ToolStripMenuItem.Click += new System.EventHandler(this.获取云软件从服务器ToolStripMenuItem_Click);
            // 
            // 打开所在文件夹ToolStripMenuItem
            // 
            this.打开所在文件夹ToolStripMenuItem.Name = "打开所在文件夹ToolStripMenuItem";
            this.打开所在文件夹ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.打开所在文件夹ToolStripMenuItem.Text = "打开所在文件夹";
            this.打开所在文件夹ToolStripMenuItem.Click += new System.EventHandler(this.打开所在文件夹ToolStripMenuItem_Click);
            // 
            // 重新生成configxmlToolStripMenuItem
            // 
            this.重新生成configxmlToolStripMenuItem.Name = "重新生成configxmlToolStripMenuItem";
            this.重新生成configxmlToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.重新生成configxmlToolStripMenuItem.Text = "重新生成config.xml";
            this.重新生成configxmlToolStripMenuItem.Click += new System.EventHandler(this.重新生成configxmlToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder_16x.png");
            this.imageList1.Images.SetKeyName(1, "PYFile.png");
            this.imageList1.Images.SetKeyName(2, "xml.png");
            this.imageList1.Images.SetKeyName(3, "Document_16x.png");
            this.imageList1.Images.SetKeyName(4, "Config.png");
            this.imageList1.Images.SetKeyName(5, "dll.png");
            this.imageList1.Images.SetKeyName(6, "PYProject.png");
            this.imageList1.Images.SetKeyName(7, "Project.png");
            this.imageList1.Images.SetKeyName(8, "app.png");
            // 
            // frmScriptNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 490);
            this.Controls.Add(this.treeScript);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmScriptNavigation";
            this.Text = "脚本资源管理器";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.TreeView treeScript;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnCollapse;
        private System.Windows.Forms.ToolStripButton btnExpand;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看代码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看设计器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 新建云软件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取云软件从服务器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开所在文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 重新生成configxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnSoftConfig;
        private System.Windows.Forms.ToolStripButton toolServer;
        private System.Windows.Forms.ToolStripMenuItem controllerpyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发布云软件到服务器ToolStripMenuItem;
    }
}