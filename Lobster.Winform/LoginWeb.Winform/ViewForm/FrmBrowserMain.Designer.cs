namespace LoginWeb.Winform.ViewForm
{
    partial class FrmBrowserMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBrowserMain));
            this.tabFormMain = new DevComponents.DotNetBar.Controls.TabFormControl();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.biMenu = new DevComponents.DotNetBar.ButtonItem();
            this.biSysSetting = new DevComponents.DotNetBar.ButtonItem();
            this.biChangeDept = new DevComponents.DotNetBar.ButtonItem();
            this.biChangePwd = new DevComponents.DotNetBar.ButtonItem();
            this.biSetting = new DevComponents.DotNetBar.ButtonItem();
            this.biHelp = new DevComponents.DotNetBar.ButtonItem();
            this.biAbout = new DevComponents.DotNetBar.ButtonItem();
            this.biReLogin = new DevComponents.DotNetBar.ButtonItem();
            this.biExit = new DevComponents.DotNetBar.ButtonItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem7 = new DevComponents.DotNetBar.LabelItem();
            this.labProduct = new DevComponents.DotNetBar.LabelItem();
            this.itemContainer5 = new DevComponents.DotNetBar.ItemContainer();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.labUserName = new DevComponents.DotNetBar.LabelItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.labDeptName1 = new DevComponents.DotNetBar.LabelItem();
            this.labDeptName = new DevComponents.DotNetBar.LabelItem();
            this.labWorkName2 = new DevComponents.DotNetBar.LabelItem();
            this.btnMessage2 = new DevComponents.DotNetBar.LabelItem();
            this.labWorkName = new DevComponents.DotNetBar.LabelItem();
            this.btnMessage = new DevComponents.DotNetBar.LabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabFormMain
            // 
            this.tabFormMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tabFormMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tabFormMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormMain.ForeColor = System.Drawing.Color.Black;
            this.tabFormMain.Images = this.imageList;
            this.tabFormMain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.biMenu});
            this.tabFormMain.Location = new System.Drawing.Point(1, 1);
            this.tabFormMain.Name = "tabFormMain";
            this.tabFormMain.NewTabItemVisible = true;
            this.tabFormMain.Size = new System.Drawing.Size(790, 440);
            this.tabFormMain.TabIndex = 1;
            this.tabFormMain.TabStripFont = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "系统参数.png");
            this.imageList.Images.SetKeyName(1, "切换用户.png");
            this.imageList.Images.SetKeyName(2, "系统退出.png");
            this.imageList.Images.SetKeyName(3, "切换科室.png");
            this.imageList.Images.SetKeyName(4, "参数设置.png");
            this.imageList.Images.SetKeyName(5, "修改密码.png");
            this.imageList.Images.SetKeyName(6, "帮助.png");
            this.imageList.Images.SetKeyName(7, "关于.png");
            // 
            // biMenu
            // 
            this.biMenu.AutoExpandOnClick = true;
            this.biMenu.Name = "biMenu";
            this.biMenu.ShowSubItems = false;
            this.biMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.biSysSetting,
            this.biReLogin,
            this.biExit});
            this.biMenu.Symbol = "59630";
            this.biMenu.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.biMenu.SymbolSize = 18F;
            this.biMenu.Text = "buttonItem3";
            this.biMenu.Click += new System.EventHandler(this.biMenu_Click);
            // 
            // biSysSetting
            // 
            this.biSysSetting.BeginGroup = true;
            this.biSysSetting.ImageIndex = 0;
            this.biSysSetting.Name = "biSysSetting";
            this.biSysSetting.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.biChangeDept,
            this.biChangePwd,
            this.biSetting,
            this.biHelp,
            this.biAbout});
            this.biSysSetting.Text = "系统设置";
            // 
            // biChangeDept
            // 
            this.biChangeDept.ImageIndex = 3;
            this.biChangeDept.Name = "biChangeDept";
            this.biChangeDept.Text = "切换科室";
            this.biChangeDept.Click += new System.EventHandler(this.biChangeDept_Click);
            // 
            // biChangePwd
            // 
            this.biChangePwd.ImageIndex = 5;
            this.biChangePwd.Name = "biChangePwd";
            this.biChangePwd.Text = "修改密码";
            this.biChangePwd.Click += new System.EventHandler(this.biChangePwd_Click);
            // 
            // biSetting
            // 
            this.biSetting.ImageIndex = 4;
            this.biSetting.Name = "biSetting";
            this.biSetting.Text = "参数设置";
            this.biSetting.Click += new System.EventHandler(this.biSetting_Click);
            // 
            // biHelp
            // 
            this.biHelp.BeginGroup = true;
            this.biHelp.ImageIndex = 6;
            this.biHelp.Name = "biHelp";
            this.biHelp.Text = "帮助";
            this.biHelp.Click += new System.EventHandler(this.biHelp_Click);
            // 
            // biAbout
            // 
            this.biAbout.ImageIndex = 7;
            this.biAbout.Name = "biAbout";
            this.biAbout.Text = "关于";
            this.biAbout.Click += new System.EventHandler(this.biAbout_Click);
            // 
            // biReLogin
            // 
            this.biReLogin.ImageIndex = 1;
            this.biReLogin.Name = "biReLogin";
            this.biReLogin.Text = "重新登录";
            this.biReLogin.Click += new System.EventHandler(this.biReLogin_Click);
            // 
            // biExit
            // 
            this.biExit.ImageIndex = 2;
            this.biExit.Name = "biExit";
            this.biExit.Text = "退出系统";
            this.biExit.Click += new System.EventHandler(this.biExit_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerColorTint = System.Drawing.Color.Silver;
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Silver;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199))))));
            // 
            // bar1
            // 
            this.bar1.AccessibleDescription = "bar1 (bar1)";
            this.bar1.AccessibleName = "bar1";
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.bar1.AntiAlias = true;
            this.bar1.AutoSyncBarCaption = true;
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(139)))), ((int)(((byte)(202)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.ForeColor = System.Drawing.Color.Black;
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem7,
            this.labProduct,
            this.itemContainer5,
            this.btnMessage2,
            this.labWorkName,
            this.btnMessage});
            this.bar1.Location = new System.Drawing.Point(1, 441);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 5;
            this.bar1.SingleLineColor = System.Drawing.Color.White;
            this.bar1.Size = new System.Drawing.Size(790, 21);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem7
            // 
            this.labelItem7.ForeColor = System.Drawing.Color.White;
            this.labelItem7.Name = "labelItem7";
            this.labelItem7.Text = "版权所有：";
            // 
            // labProduct
            // 
            this.labProduct.ForeColor = System.Drawing.Color.White;
            this.labProduct.Name = "labProduct";
            this.labProduct.Text = "无";
            // 
            // itemContainer5
            // 
            // 
            // 
            // 
            this.itemContainer5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer5.Name = "itemContainer5";
            this.itemContainer5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.labUserName,
            this.labelItem3,
            this.labDeptName1,
            this.labDeptName,
            this.labWorkName2});
            // 
            // 
            // 
            this.itemContainer5.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer5.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // labelItem1
            // 
            this.labelItem1.ForeColor = System.Drawing.Color.White;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "用户名：";
            // 
            // labUserName
            // 
            this.labUserName.ForeColor = System.Drawing.Color.White;
            this.labUserName.Name = "labUserName";
            this.labUserName.Text = "未登录";
            // 
            // labelItem3
            // 
            this.labelItem3.ForeColor = System.Drawing.Color.White;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "    ";
            // 
            // labDeptName1
            // 
            this.labDeptName1.ForeColor = System.Drawing.Color.White;
            this.labDeptName1.Name = "labDeptName1";
            this.labDeptName1.Text = "科室：";
            // 
            // labDeptName
            // 
            this.labDeptName.ForeColor = System.Drawing.Color.White;
            this.labDeptName.Name = "labDeptName";
            this.labDeptName.Text = "无";
            // 
            // labWorkName2
            // 
            this.labWorkName2.ForeColor = System.Drawing.Color.White;
            this.labWorkName2.Name = "labWorkName2";
            this.labWorkName2.Text = "    ";
            // 
            // btnMessage2
            // 
            this.btnMessage2.ForeColor = System.Drawing.Color.White;
            this.btnMessage2.Name = "btnMessage2";
            this.btnMessage2.Text = "机构：";
            // 
            // labWorkName
            // 
            this.labWorkName.ForeColor = System.Drawing.Color.White;
            this.labWorkName.Name = "labWorkName";
            this.labWorkName.Text = "无";
            // 
            // btnMessage
            // 
            this.btnMessage.ForeColor = System.Drawing.Color.White;
            this.btnMessage.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Text = "消息面板";
            this.btnMessage.TextAlignment = System.Drawing.StringAlignment.Far;
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            this.btnMessage.MouseEnter += new System.EventHandler(this.btnMessage_MouseEnter);
            this.btnMessage.MouseLeave += new System.EventHandler(this.btnMessage_MouseLeave);
            // 
            // FrmBrowserMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 463);
            this.Controls.Add(this.tabFormMain);
            this.Controls.Add(this.bar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBrowserMain";
            this.Text = "FrmBrowserMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBrowserMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBrowserMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmBrowserMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TabFormControl tabFormMain;
        private DevComponents.DotNetBar.ButtonItem biMenu;
        private DevComponents.DotNetBar.ButtonItem biSysSetting;
        private DevComponents.DotNetBar.ButtonItem biReLogin;
        private DevComponents.DotNetBar.ButtonItem biExit;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonItem biChangeDept;
        private DevComponents.DotNetBar.ButtonItem biChangePwd;
        private DevComponents.DotNetBar.ButtonItem biSetting;
        private DevComponents.DotNetBar.ButtonItem biHelp;
        private DevComponents.DotNetBar.ButtonItem biAbout;
        private System.Windows.Forms.ImageList imageList;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem labelItem7;
        private DevComponents.DotNetBar.LabelItem labProduct;
        private DevComponents.DotNetBar.ItemContainer itemContainer5;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labUserName;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.LabelItem labDeptName1;
        private DevComponents.DotNetBar.LabelItem labDeptName;
        private DevComponents.DotNetBar.LabelItem labWorkName2;
        private DevComponents.DotNetBar.LabelItem btnMessage2;
        private DevComponents.DotNetBar.LabelItem labWorkName;
        private DevComponents.DotNetBar.LabelItem btnMessage;
    }
}