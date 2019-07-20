namespace Debugger.Winform.ViewForm
{
    partial class frmClientDebugger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientDebugger));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSoftName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtmethodname = new System.Windows.Forms.TextBox();
            this.txtcontrollername = new System.Windows.Forms.TextBox();
            this.txtcontrollerid = new System.Windows.Forms.TextBox();
            this.btnRequest = new System.Windows.Forms.Button();
            this.txtparams = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnreload = new System.Windows.Forms.ToolStripButton();
            this.btnClearAll = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treePlugin = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSoftName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtmethodname);
            this.panel1.Controls.Add(this.txtcontrollername);
            this.panel1.Controls.Add(this.txtcontrollerid);
            this.panel1.Controls.Add(this.btnRequest);
            this.panel1.Controls.Add(this.txtparams);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 532);
            this.panel1.TabIndex = 0;
            // 
            // txtSoftName
            // 
            this.txtSoftName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoftName.Location = new System.Drawing.Point(82, 11);
            this.txtSoftName.Name = "txtSoftName";
            this.txtSoftName.Size = new System.Drawing.Size(541, 21);
            this.txtSoftName.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "云软件名称";
            // 
            // txtmethodname
            // 
            this.txtmethodname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtmethodname.Location = new System.Drawing.Point(82, 90);
            this.txtmethodname.Name = "txtmethodname";
            this.txtmethodname.Size = new System.Drawing.Size(541, 21);
            this.txtmethodname.TabIndex = 11;
            // 
            // txtcontrollername
            // 
            this.txtcontrollername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcontrollername.Location = new System.Drawing.Point(82, 64);
            this.txtcontrollername.Name = "txtcontrollername";
            this.txtcontrollername.Size = new System.Drawing.Size(541, 21);
            this.txtcontrollername.TabIndex = 10;
            // 
            // txtcontrollerid
            // 
            this.txtcontrollerid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcontrollerid.Location = new System.Drawing.Point(82, 38);
            this.txtcontrollerid.Name = "txtcontrollerid";
            this.txtcontrollerid.Size = new System.Drawing.Size(541, 21);
            this.txtcontrollerid.TabIndex = 9;
            // 
            // btnRequest
            // 
            this.btnRequest.Image = ((System.Drawing.Image)(resources.GetObject("btnRequest.Image")));
            this.btnRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRequest.Location = new System.Drawing.Point(82, 468);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(75, 25);
            this.btnRequest.TabIndex = 8;
            this.btnRequest.Text = "  请求";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // txtparams
            // 
            this.txtparams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtparams.Location = new System.Drawing.Point(82, 117);
            this.txtparams.Multiline = true;
            this.txtparams.Name = "txtparams";
            this.txtparams.Size = new System.Drawing.Size(541, 345);
            this.txtparams.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "参数Json";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "方法名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "控制器名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "控制器标识";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "service.png");
            this.imageList1.Images.SetKeyName(1, "controller.png");
            this.imageList1.Images.SetKeyName(2, "method.png");
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 22);
            this.btnClose.Text = "关闭";
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnreload,
            this.btnClearAll,
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(353, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnreload
            // 
            this.btnreload.Image = ((System.Drawing.Image)(resources.GetObject("btnreload.Image")));
            this.btnreload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnreload.Name = "btnreload";
            this.btnreload.Size = new System.Drawing.Size(52, 22);
            this.btnreload.Text = "刷新";
            this.btnreload.Click += new System.EventHandler(this.btnreload_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
            this.btnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(76, 22);
            this.btnClearAll.Text = "清空缓存";
            this.btnClearAll.Visible = false;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treePlugin);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(992, 532);
            this.splitContainer1.SplitterDistance = 353;
            this.splitContainer1.TabIndex = 2;
            // 
            // treePlugin
            // 
            this.treePlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePlugin.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treePlugin.ImageIndex = 0;
            this.treePlugin.ImageList = this.imageList1;
            this.treePlugin.Location = new System.Drawing.Point(0, 25);
            this.treePlugin.Name = "treePlugin";
            this.treePlugin.SelectedImageIndex = 0;
            this.treePlugin.Size = new System.Drawing.Size(353, 507);
            this.treePlugin.TabIndex = 0;
            this.treePlugin.DoubleClick += new System.EventHandler(this.treePlugin_DoubleClick);
            // 
            // frmClientDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 532);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmClientDebugger";
            this.Text = "调试客户端";
            this.Load += new System.EventHandler(this.frmClientDebugger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.TextBox txtparams;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnreload;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treePlugin;
        private System.Windows.Forms.TextBox txtcontrollerid;
        private System.Windows.Forms.TextBox txtmethodname;
        private System.Windows.Forms.TextBox txtcontrollername;
        private System.Windows.Forms.ToolStripButton btnClearAll;
        private System.Windows.Forms.TextBox txtSoftName;
        private System.Windows.Forms.Label label5;
    }
}