using CefSharp;
using CefSharp.WinForms;
using Debugger.Winform.IView;
using DevComponents.DotNetBar;
using efwplusWinform.Business;
using efwplusWinform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.ViewForm
{
    public partial class frmSoftBrowser : Form, IfrmSoftBrowser
    {
        private ChromiumWebBrowser browser;

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

        public frmSoftBrowser()
        {
            InitializeComponent();
            
            barDocument.DockTabClosing += BarDocument_DockTabClosing;
            barDocument.DockTabClosed += BarDocument_DockTabClosed;
            ClearBarDocument();
        }
        //初始化界面
        public void InitView(Control softnav, Control script, Control trace)
        {
            softnav.Dock = DockStyle.Fill;
            (softnav as Form).TopLevel = false;
            (softnav as Form).FormBorderStyle = FormBorderStyle.None;
            script.Dock = DockStyle.Fill;
            (script as Form).TopLevel = false;
            (script as Form).FormBorderStyle = FormBorderStyle.None;
            trace.Dock = DockStyle.Fill;
            (trace as Form).TopLevel = false;
            (trace as Form).FormBorderStyle = FormBorderStyle.None;
            pdcSoft.Controls.Add(softnav);
            pdcScript.Controls.Add(script);
            pdcTrace.Controls.Add(trace);
            (softnav as Form).Show();
            (script as Form).Show();
            (trace as Form).Show();
            pdcSoft.Refresh();
            pdcScript.Refresh();
            pdcTrace.Refresh();

            ShowHome();
        }


        private void BarDocument_DockTabClosed(object sender, DockTabClosingEventArgs e)
        {
            DockContainerItem item = e.DockContainerItem;
            int index = barDocument.Items.IndexOf(item);
            if (barDocument.Items.Contains(item.Name) && e.DockContainerItem.Text != "首页")
                barDocument.Items.Remove(item);

            barDocument.SelectedDockContainerItem = (DockContainerItem)barDocument.Items[index - 1];
        }

        private void BarDocument_DockTabClosing(object sender, DockTabClosingEventArgs e)
        {
            if (e.DockContainerItem.Text == "首页")
            {
                e.Cancel = true;
            }
        }

        public void ClearBarDocument()
        {
            List<BaseItem> list = new List<BaseItem>();
            foreach (DockContainerItem item in barDocument.Items)
            {
                list.Add(item);
            }

            foreach (DockContainerItem item in list)
            {
                if (item.Text== "首页")
                    continue;
                barDocument.CloseDockTab(item);
            }
        }

       
        //显示首页
        private void ShowHome()
        {
            //InvokeController("NewTabCloudSoftNav");
            string url = "www.efwplus.cn";
            if (browser == null)
            {
                browser = new ChromiumWebBrowser(url)
                {
                    BrowserSettings =
                        {
                            DefaultEncoding = "UTF-8",
                            WebGl = CefState.Disabled
                        }
                };
                //browser.MenuHandler = new MenuHandler();
                browser.Dock = DockStyle.Fill;
                //注册事件
                browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
                //browser.LoadingStateChanged += OnLoadingStateChanged;
                //browser.ConsoleMessage += OnBrowserConsoleMessage;
                //browser.StatusMessage += OnBrowserStatusMessage;
                //browser.TitleChanged += OnBrowserTitleChanged;
                //browser.AddressChanged += OnBrowserAddressChanged;
                //注册对象给JS调用
                //browser.RegisterAsyncJsObject("efwplusController", InvokeController("this"), false);
            }
            pdcHome.Controls.Add(browser);
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }
        private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
            {
                var b = ((ChromiumWebBrowser)sender);

                this.InvokeOnUiThreadIfRequired(() => b.Focus());
            }
        }

        public void OpenDocumentView(Control view)
        {
            if (view == null || (view is Control) == false) return;
            string tabId = "dci_" + view.GetHashCode().ToString();
            BaseItem tabitem = barDocument.GetItem(tabId);
            if (tabitem == null)
            {
                Form form = view as Form;
                form.Dock = DockStyle.Fill;
                form.FormBorderStyle = FormBorderStyle.None;
                form.TopLevel = false;
               

                DevComponents.DotNetBar.PanelDockContainer pdc = new DevComponents.DotNetBar.PanelDockContainer();
                pdc.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
                pdc.Location = new System.Drawing.Point(3, 28);
                pdc.Name = "pdc_" + form.GetHashCode().ToString();
                pdc.Style.Alignment = System.Drawing.StringAlignment.Center;
                pdc.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
                pdc.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
                pdc.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
                pdc.Style.GradientAngle = 90;

                form.Show();
                pdc.Controls.Add(form);

                DevComponents.DotNetBar.DockContainerItem dcitem = new DevComponents.DotNetBar.DockContainerItem();
                dcitem.Control = pdc;
                dcitem.Name = tabId;
                dcitem.Tag = form;//
                dcitem.Text = form.Text;//修改代码内容，标题增加*
                if (form is IfrmTextEditor)
                {
                    (form as IfrmTextEditor).EditStateEvent = (string text) =>
                    {
                        dcitem.Text = text;
                    };
                }

                barDocument.Controls.Add(pdc);
                barDocument.Items.Add(dcitem);
                barDocument.SelectedDockContainerItem = dcitem;
                pdc.Refresh();
                barDocument.Refresh();
            }
            else
            {
                barDocument.SelectedDockContainerItem = tabitem as DockContainerItem;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            InvokeController("CefShutdown");
            InvokeController("Exit");
        }
        //关闭全部
        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            ClearBarDocument();
        }
        //保存全部
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (DockContainerItem item in barDocument.Items)
            {
                if(item.Tag is IfrmTextEditor && (item.Tag as IfrmTextEditor).EditState)
                {
                    (item.Tag as IfrmTextEditor).SaveFile();
                }
            }
        }
        //界面隐藏则关闭程序
        private void frmSoftBrowser_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
                System.Environment.Exit(0);
        }
        //重新加载云软件
        private void btnReLogin_Click(object sender, EventArgs e)
        {
            InvokeController("ReLoadSoft");
        }

        private void frmSoftBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            InvokeController("CefShutdown");
            InvokeController("Exit");
        }
    }
}
