using DevComponents.DotNetBar.Controls;
using LoginWeb.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using efwplusWinform.Common;
using System.Threading;
using EFWCoreLib.WinformFrame.Controller;
using EFWCoreLib.WinformFrame;
using LoginWeb.Winform.Model;
using System.Reflection;
using DevComponents.DotNetBar;

namespace LoginWeb.Winform.ViewForm
{
    public partial class FrmBrowserMain : TabParentForm, IfrmBrowserMain
    {
        SynchronizationContext SyncContext = null;
        public FrmBrowserMain()
        {
            InitializeComponent();

            this.tabFormMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));

            SyncContext = SynchronizationContext.Current;
            //Control.CheckForIllegalCrossThreadCalls = false;
            tabFormMain.CreateNewTab += TabFormMain_CreateNewTab;
            //清理菜单
            biMenu.SubItems.Clear();
            //显示版权公司
            object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
            {
                labProduct.Text = "";
            }
            labProduct.Text = ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
        //新增标签页面
        private void TabFormMain_CreateNewTab(object sender, EventArgs e)
        {
            InvokeController("NewTab");
        }

        #region 界面操作接口
        public void UiThreadOpenDocument(string url, string title)
        {
            SyncContext.Post(OpenDocument, new string[] { url, title });
        }

        public void UiThreadCloseDocument(string hashcode)
        {
            SyncContext.Post(CloseDocument, hashcode);
        }

        public void UiThreadOpenWindow(string url, string title)
        {
            SyncContext.Post(OpenWindow, new string[] { url, title });
        }

        public void UiThreadUserLoginPrefect(string userId)
        {
            SyncContext.Post(UserLoginPrefect, userId);
        }
        public void OpenWindow(Object state)
        {
            if (state != null)
            {
                string url = ((string[])state)[0];
                string title = ((string[])state)[1];
                string SoftName = (InvokeController("this") as WinformController).SoftName;
                IBaseView view = ControllerHelper.ShowView(SoftName, Guid.NewGuid().ToString(), "BrowserPageController", "FrmBrowserPage", false);
                (view as IfrmBrowserPage).ShowOrHideTools(false);
                (view as IfrmBrowserPage).IsWindowMode = true;
                (view as IfrmBrowserPage).SetUrl(url.ToString());
                Form form = view as Form;
                if (String.IsNullOrEmpty(title) == false)
                    form.Text = title;
                form.ShowDialog();
            }
        }

        public void OpenDocument(Object state)
        {
            if (state == null)
            {
                CreateView(null, Guid.NewGuid().ToString(), null);
            }
            else
            {
                string url = ((string[])state)[0];
                string title = ((string[])state)[1];
                string tabId = url;

                if (tabFormMain.Items.Contains(tabId) == false)
                {
                    CreateView(url, tabId, title);
                }
                else
                {
                    tabFormMain.SelectedTab = tabFormMain.Items[tabId] as TabFormItem;
                    //重新登录的再次跳转到登录界面
                    if (tabFormMain.SelectedTab.Tag is IfrmBrowserPage)
                    {
                        (tabFormMain.SelectedTab.Tag as IfrmBrowserPage).SetUrl(url.ToString());
                    }
                }
            }
        }

        //打开页面
        public void CreateView(string url, string tabId, string title)
        {
            string SoftName = (InvokeController("this") as WinformController).SoftName;
            IBaseView view = ControllerHelper.ShowView(SoftName, Guid.NewGuid().ToString(), "BrowserPageController", "FrmBrowserPage", false);
            
            Form form = view as Form;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false;
            form.Show();
            TabFormItem tab = tabFormMain.CreateTab(string.IsNullOrEmpty(title) ? form.Text : title, tabId);
            if (title == "首页")
            {
                tab.CloseButtonVisible = false;
                (view as IfrmBrowserPage).ShowOrHideTools(false);
            }
            tab.Tag = form;
            // Tab标题颜色
            //Array colors = Enum.GetValues(typeof(eTabFormItemColor));
            //int i = new Random().Next(colors.Length - 1);
            //tab.ColorTable = (eTabFormItemColor)colors.GetValue(i);
            tab.ColorTable = eTabFormItemColor.Silver;

            if (form is IfrmBrowserPage)
            {
                (form as IfrmBrowserPage).PageTitleChanged = (string text) =>
                {
                    tab.Text = string.IsNullOrEmpty(title) ? text : title;
                    tab.Tag = form;
                    tabFormMain.Refresh();
                };
            }
            //注册窗体打开事件
            //if (form is BaseFormBusiness)
            //{
            //    (form as BaseFormBusiness).ExecOpenWindowBefore(form, null);
            //}
            tab.Panel.Controls.Add(form);
            tabFormMain.SelectedTab = tab;

            if (string.IsNullOrEmpty(url) == false)
                (view as IfrmBrowserPage).SetUrl(url.ToString());
        }
      
        //关闭文档
        public void CloseDocument(Object state)
        {
            //判断窗体的HashCode
            string hashcode = state.ToString();
            TabFormItem formItem = null;
            foreach (Object item in tabFormMain.Items)
            {
                if (item is TabFormItem)
                {
                    if ((item as TabFormItem).Tag != null && ((item as TabFormItem).Tag as IfrmBrowserPage).ViewHashCode == hashcode)
                    {
                        if ((item as TabFormItem).Text == "首页")
                        {
                            break;
                        }
                        else
                        {
                            formItem = item as TabFormItem;
                            break;
                        }
                    }
                }
            }
            if (formItem != null)
            {
                if (formItem.Tag is BaseFormBusiness)
                {
                    BaseFormBusiness basefrom = ((formItem.Tag as BaseFormBusiness).Controls.Find("panelPage", true)[0].Controls[0] as BaseFormBusiness);
                    if (basefrom != null)
                        basefrom.ExecOpenWindowBefore(basefrom, null);
                }
                tabFormMain.TabStrip.CloseTab(formItem, DevComponents.DotNetBar.eEventSource.Code);
                if (formItem.Tag is BaseFormBusiness)
                {
                    BaseFormBusiness basefrom = ((formItem.Tag as BaseFormBusiness).Controls.Find("panelPage", true)[0].Controls[0] as BaseFormBusiness);
                    if (basefrom != null)
                        basefrom.ExecCloseWindowAfter(basefrom, null);
                }
            }
        }

        public void UserLoginPrefect(Object state)
        {
            string userId = state.ToString();
            InvokeController("UserLoginPrefect", userId);
        }
        public string frmName
        {
            get;
            set;
        }

        public ControllerEventHandler InvokeController
        {
            get;
            set;
        }
        #endregion

        private void FrmBrowserMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        //退出系统
        private void FrmBrowserMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要退出系统吗？", "询问窗", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                e.Cancel = false;
                InvokeController("CefShutdown");
                //System.Diagnostics.Process.GetCurrentProcess().Kill();
                InvokeController("Exit");
            }
            else
            {
                e.Cancel = true;
            }
        }
        //初始化
        private void FrmBrowserMain_Load(object sender, EventArgs e)
        {
            InvokeController("ShowHome");
            if (WinformGlobal.LoginUserInfo == null)
            {
                InvokeController("ShowLogin");
            }
        }

        //登录后加载菜单
        public void LoadLoginData(string UserName, string DeptName, string WorkName, List<BaseModule> modules, List<BaseMenu> menus, List<BaseDept> depts)
        {
            biChangeDept.Tag = depts;
            labUserName.Text = UserName;
            labDeptName.Text = DeptName;
            labWorkName.Text = WorkName;
            this.Refresh();
            //清理菜单
            biMenu.SubItems.Clear();
            //清理打开的界面
            List<TabFormItem> formItemList = new List<TabFormItem>();
            foreach (Object item in tabFormMain.Items)
            {
                if (item is TabFormItem)
                {
                    formItemList.Add(item as TabFormItem);
                }
            }
            foreach (TabFormItem formItem in formItemList)
            {
                if (formItem.Text != "首页")
                    tabFormMain.TabStrip.CloseTab(formItem, DevComponents.DotNetBar.eEventSource.Code);
            }
            //打开Home
            //InvokeController("ShowHome");
            //循环创建模块和菜单
            DevComponents.DotNetBar.ButtonItem btnItem1;
            //DevComponents.DotNetBar.ButtonItem btnItem2;
            DevComponents.DotNetBar.ButtonItem btnItem3;
            for (int i = 0; i < modules.Count; i++)
            {
                btnItem1 = new DevComponents.DotNetBar.ButtonItem();
                btnItem1.Text = modules[i].Name;
                this.biMenu.SubItems.Add(btnItem1);
                //添加子菜单
                List<BaseMenu> _menus = menus.FindAll(x => (x.ModuleId == modules[i].ModuleId && x.PMenuId == -1)).OrderByDescending(x => x.SortId).ToList();
                for (int j = 0; j < _menus.Count; j++)
                {
                    //添加二级菜单
                    //btnItem2 = new DevComponents.DotNetBar.ButtonItem();
                    //btnItem2.Text = _menus[j].Name;
                    //btnItem1.SubItems.Add(btnItem2);
                    //菜单改为2级菜单，添加分组线
                    DevComponents.DotNetBar.ButtonItem groupbi = null;
                    //添加三级菜单
                    List<BaseMenu> _menus2 = menus.FindAll(x => x.PMenuId == _menus[j].MenuId && x.DelFlag == 0).OrderByDescending(x => x.SortId).ToList();
                    foreach (BaseMenu menu in _menus2)
                    {
                        btnItem3 = new DevComponents.DotNetBar.ButtonItem();
                        btnItem3.Text = menu.Name;
                        btnItem3.Tag = menu;
                        btnItem3.Click += (object sender, EventArgs e) =>
                         {
                             BaseMenu menuTag = ((DevComponents.DotNetBar.ButtonItem)sender).Tag as BaseMenu;
                             string url;
                             //if (!string.IsNullOrEmpty(menuTag.SoftName.Trim()) && !string.IsNullOrEmpty(menuTag.ControllerName.Trim()))
                             if (!string.IsNullOrEmpty(menuTag.UrlName))
                             {
                                 url = (string)InvokeController("GetWebUrl") + menuTag.UrlName;
                             }
                             else
                             {
                                 url = "efwplus://" + menuTag.SoftName.Trim() + "/" + menuTag.ControllerName.Trim() + "/" + menuTag.ViewName.Trim();
                             }
                             string title = menuTag.Name;
                             InvokeController("NewTab2", url, title);
                         };
                        btnItem1.SubItems.Add(btnItem3);

                        if (menu.Equals(_menus2[0]))//获取分组的第一个菜单
                        {
                            groupbi = btnItem3;
                        }
                    }

                    //加上分组线
                    if (j > 0 && groupbi != null)
                    {
                        groupbi.BeginGroup = true;
                    }
                }
            }
            //添加系统设置、重新登录、系统退出
            this.biMenu.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.biSysSetting,
            this.biReLogin,
            this.biExit});

            InitMessageForm();
        }

        //显示切换科室
        public void ChangedDeptName(string DeptName)
        {
            labDeptName.Text = DeptName;
        }

        #region 菜单事件
        //点击菜单
        private void biMenu_Click(object sender, EventArgs e)
        {
            if (WinformGlobal.LoginUserInfo == null)
            {
                InvokeController("ShowLogin");
            }
        }
        //切换科室
        private void biChangeDept_Click(object sender, EventArgs e)
        {
            InvokeController("ShowReDept",biChangeDept.Tag);
        }
        //修改密码
        private void biChangePwd_Click(object sender, EventArgs e)
        {
            InvokeController("ShowAlterPass");
        }
        //系统设置
        private void biSetting_Click(object sender, EventArgs e)
        {
            InvokeController("ShowSetting");
        }
        //关于
        private void biAbout_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog();
        }
        //帮助
        private void biHelp_Click(object sender, EventArgs e)
        {

        }
        //重新登录
        private void biReLogin_Click(object sender, EventArgs e)
        {
            InvokeController("ShowLogin");
            //labUserName.Text = "未登录";
            //labDeptName.Text = "无";
            //labWorkName.Text = "无";
            biMenu.SubItems.Clear();
            //if (TaskbarForm.instance != null)
            //    TaskbarForm.instance.ClearMessages();          
        }
        //系统退出
        private void biExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #endregion

        private void btnMessage_Click(object sender, EventArgs e)
        {
            ShowMessageForm();
        }

        private void btnMessage_MouseEnter(object sender, EventArgs e)
        {
            this.btnMessage.ForeColor = Color.Black;
            this.btnMessage.Cursor = Cursors.Hand;
        }

        private void btnMessage_MouseLeave(object sender, EventArgs e)
        {
            this.btnMessage.ForeColor = Color.White;
            this.btnMessage.Cursor = Cursors.Default;
        }

        MessageTimer mstimer = null;//消息提醒触发器
        public void InitMessageForm()
        {
            if (mstimer != null)
            {
                mstimer.Enabled = false;
                if (TaskbarForm.instance != null)
                    TaskbarForm.instance.ClearMessages();
            }

            mstimer = new MessageTimer();
            mstimer.FrmMain = this;
            mstimer.Interval = 20000;
            mstimer.Enabled = true;
        }

        public void ShowMessageForm()
        {
            TaskbarForm.ShowForm(this);
        }
    }
}
