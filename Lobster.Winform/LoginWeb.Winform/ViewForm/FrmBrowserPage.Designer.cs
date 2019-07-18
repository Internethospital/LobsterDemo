namespace LoginWeb.Winform.ViewForm
{
    partial class FrmBrowserPage
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
            return;
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.barTool = new DevComponents.DotNetBar.Bar();
            this.backButton = new DevComponents.DotNetBar.ButtonItem();
            this.forwardButton = new DevComponents.DotNetBar.ButtonItem();
            this.refreshButton = new DevComponents.DotNetBar.ButtonItem();
            this.urlTextBox = new DevComponents.DotNetBar.TextBoxItem();
            this.biClose = new DevComponents.DotNetBar.ButtonItem();
            this.panelPage = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.barTool)).BeginInit();
            this.SuspendLayout();
            // 
            // barTool
            // 
            this.barTool.AntiAlias = true;
            this.barTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.barTool.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.barTool.IsMaximized = false;
            this.barTool.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.backButton,
            this.forwardButton,
            this.refreshButton,
            this.urlTextBox,
            this.biClose});
            this.barTool.Location = new System.Drawing.Point(0, 0);
            this.barTool.Name = "barTool";
            this.barTool.Size = new System.Drawing.Size(755, 36);
            this.barTool.Stretch = true;
            this.barTool.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barTool.TabIndex = 2;
            this.barTool.TabStop = false;
            this.barTool.Text = "barTool";
            // 
            // backButton
            // 
            this.backButton.Name = "backButton";
            this.backButton.Symbol = "58135";
            this.backButton.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.backButton.Text = "buttonItem1";
            // 
            // forwardButton
            // 
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Symbol = "58824";
            this.forwardButton.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.forwardButton.Text = "buttonItem2";
            // 
            // refreshButton
            // 
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Symbol = "58837";
            this.refreshButton.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.refreshButton.Text = "buttonItem3";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Stretch = true;
            this.urlTextBox.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // biClose
            // 
            this.biClose.Name = "biClose";
            this.biClose.Text = "关闭(&C)";
            // 
            // panelPage
            // 
            this.panelPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPage.Location = new System.Drawing.Point(0, 36);
            this.panelPage.Name = "panelPage";
            this.panelPage.Size = new System.Drawing.Size(755, 417);
            this.panelPage.TabIndex = 3;
            // 
            // FrmBrowserPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 453);
            this.Controls.Add(this.panelPage);
            this.Controls.Add(this.barTool);
            this.MinimizeBox = false;
            this.Name = "FrmBrowserPage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新标签页";
            ((System.ComponentModel.ISupportInitialize)(this.barTool)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar barTool;
        private DevComponents.DotNetBar.ButtonItem backButton;
        private DevComponents.DotNetBar.ButtonItem forwardButton;
        private DevComponents.DotNetBar.ButtonItem refreshButton;
        private DevComponents.DotNetBar.TextBoxItem urlTextBox;
        private System.Windows.Forms.Panel panelPage;
        private DevComponents.DotNetBar.ButtonItem biClose;
    }
}