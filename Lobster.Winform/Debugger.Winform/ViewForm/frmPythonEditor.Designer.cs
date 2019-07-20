namespace Debugger.Winform.ViewForm
{
    partial class frmPythonEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPythonEditor));
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.打开OToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.剪切UToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.复制CToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.粘贴PToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.删除toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.撤销toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.重做toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEditorControl
            // 
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.IsIconBarVisible = true;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.FullRow;
            this.textEditorControl.Location = new System.Drawing.Point(0, 25);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.ShowHRuler = true;
            this.textEditorControl.ShowSpaces = true;
            this.textEditorControl.ShowTabs = true;
            this.textEditorControl.Size = new System.Drawing.Size(992, 507);
            this.textEditorControl.TabIndex = 1;
            this.textEditorControl.Text = resources.GetString("textEditorControl.Text");
            this.textEditorControl.TextChanged += new System.EventHandler(this.textEditorControl_TextChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripButton,
            this.保存SToolStripButton,
            this.toolStripSeparator,
            this.剪切UToolStripButton,
            this.复制CToolStripButton,
            this.粘贴PToolStripButton,
            this.删除toolStripButton,
            this.toolStripSeparator1,
            this.撤销toolStripButton,
            this.重做toolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(992, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // 打开OToolStripButton
            // 
            this.打开OToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打开OToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripButton.Image")));
            this.打开OToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripButton.Name = "打开OToolStripButton";
            this.打开OToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打开OToolStripButton.Text = "打开(&O)";
            this.打开OToolStripButton.Click += new System.EventHandler(this.打开OToolStripButton_Click);
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.保存SToolStripButton.Text = "保存(&S)";
            this.保存SToolStripButton.Click += new System.EventHandler(this.保存SToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // 剪切UToolStripButton
            // 
            this.剪切UToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.剪切UToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("剪切UToolStripButton.Image")));
            this.剪切UToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.剪切UToolStripButton.Name = "剪切UToolStripButton";
            this.剪切UToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.剪切UToolStripButton.Text = "剪切(&T)";
            this.剪切UToolStripButton.Click += new System.EventHandler(this.剪切UToolStripButton_Click);
            // 
            // 复制CToolStripButton
            // 
            this.复制CToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.复制CToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("复制CToolStripButton.Image")));
            this.复制CToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.复制CToolStripButton.Name = "复制CToolStripButton";
            this.复制CToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.复制CToolStripButton.Text = "复制(&C)";
            this.复制CToolStripButton.Click += new System.EventHandler(this.复制CToolStripButton_Click);
            // 
            // 粘贴PToolStripButton
            // 
            this.粘贴PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.粘贴PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("粘贴PToolStripButton.Image")));
            this.粘贴PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.粘贴PToolStripButton.Name = "粘贴PToolStripButton";
            this.粘贴PToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.粘贴PToolStripButton.Text = "粘贴(&V)";
            this.粘贴PToolStripButton.Click += new System.EventHandler(this.粘贴PToolStripButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "脚本文件|*.py|所有文件|*.*";
            this.openFileDialog.Title = "打开脚本文件";
            // 
            // 删除toolStripButton
            // 
            this.删除toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.删除toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("删除toolStripButton.Image")));
            this.删除toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.删除toolStripButton.Name = "删除toolStripButton";
            this.删除toolStripButton.Size = new System.Drawing.Size(23, 22);
            this.删除toolStripButton.Text = "删除";
            this.删除toolStripButton.Click += new System.EventHandler(this.删除toolStripButton_Click);
            // 
            // 撤销toolStripButton
            // 
            this.撤销toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.撤销toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("撤销toolStripButton.Image")));
            this.撤销toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.撤销toolStripButton.Name = "撤销toolStripButton";
            this.撤销toolStripButton.Size = new System.Drawing.Size(23, 22);
            this.撤销toolStripButton.Text = "撤销";
            this.撤销toolStripButton.Click += new System.EventHandler(this.撤销toolStripButton_Click);
            // 
            // 重做toolStripButton
            // 
            this.重做toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.重做toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("重做toolStripButton.Image")));
            this.重做toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.重做toolStripButton.Name = "重做toolStripButton";
            this.重做toolStripButton.Size = new System.Drawing.Size(23, 22);
            this.重做toolStripButton.Text = "重做";
            this.重做toolStripButton.Click += new System.EventHandler(this.重做toolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // frmPythonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 532);
            this.Controls.Add(this.textEditorControl);
            this.Controls.Add(this.toolStrip);
            this.Name = "frmPythonEditor";
            this.Text = "代码编辑器";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPythonEditor_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPythonEditor_KeyPress);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton 打开OToolStripButton;
        private System.Windows.Forms.ToolStripButton 保存SToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton 剪切UToolStripButton;
        private System.Windows.Forms.ToolStripButton 复制CToolStripButton;
        private System.Windows.Forms.ToolStripButton 粘贴PToolStripButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton 删除toolStripButton;
        private System.Windows.Forms.ToolStripButton 撤销toolStripButton;
        private System.Windows.Forms.ToolStripButton 重做toolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}