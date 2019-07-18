using CefSharp;
using CefSharp.WinForms;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.WinformFrame.Controller;
using efwplusWinform.Common;
using LoginWeb.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginWeb.Winform.ViewForm
{
    public partial class FrmBrowserPage : BaseFormBusiness, IfrmBrowserPage
    {
        private string efwplushead = "efwplus://";
        private ChromiumWebBrowser browser;
        public FrmBrowserPage()
        {
            InitializeComponent();
            backButton.Enabled = false;
            forwardButton.Enabled = false;
            urlTextBox.Focus();
            urlTextBox.KeyDown += UrlTextBox_KeyDown;
            refreshButton.Click += RefreshButton_Click;
            backButton.Click += BackButton_Click;
            forwardButton.Click += ForwardButton_Click;

            this.KeyDown += FrmBrowserPage_KeyDown;
            biClose.Click += BiClose_Click;
        }

        private void BiClose_Click(object sender, EventArgs e)
        {
            InvokeController("StartCloseTab");
        }

        private void FrmBrowserPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                if (browser != null)
                    browser.ShowDevTools();
            }
        }

        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetUrl(urlTextBox.Text);
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (browser != null)
                browser.Forward();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (browser != null)
                browser.Back();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SetUrl(urlTextBox.Text);
        }

        public Action<string> PageTitleChanged
        {
            get;
            set;
        }

        private bool _IsWindow = false;
        public bool IsWindowMode
        {
            get
            {
                return _IsWindow;
            }

            set
            {
                _IsWindow = value;
            }
        }

        private string _ViewHashCode;
        public string ViewHashCode
        {
            get
            {
                if (_ViewHashCode == null)
                {
                    _ViewHashCode= this.GetHashCode().ToString();
                }
                return _ViewHashCode;
            }
        }

        public void SetUrl(string url)
        {
            //移除界面
            if (panelPage.Controls.Count > 0)
            {
                if(panelPage.Controls[0] is Form)
                {
                    //先移除掉事件，不然一刷新就关闭了
                    panelPage.Controls[0].RemoveControlEvent("EventVisibleChanged");
                    //panelPage.Controls[0].VisibleChanged -= Page_VisibleChanged;
                }
                panelPage.Controls.Clear();
            }
            urlTextBox.Text = url;
            if (url.ToLower().IndexOf(efwplushead) == 0)
            {
                try
                {
                    string[] param = url.Remove(0, efwplushead.Length).Split(new char[] { '/', '\\' });
                    string softname = param[0];
                    string controller = param[1];
                    string viewname = param.Length > 2 ? param[2] : null;

                    IBaseView view = ControllerHelper.ShowView(softname, controller, controller, viewname, false);

                    Form form = view as Form;

                    //界面关闭
                    form.VisibleChanged += Page_VisibleChanged;
                    //this.FormBorderStyle = form.FormBorderStyle;
                    //if (form.FormBorderStyle == FormBorderStyle.None)
                    //{
                    //    this.Height = form.Height;
                    //    this.Width = form.Width;
                    //}
                    //else
                    //{
                    if (barTool.Visible)//放在panelPage.Controls.Add(form); 这段代码的前面
                    {
                        this.Height = barTool.Height + form.Height + 39;
                        this.Width = form.Width;
                    }
                    else
                    {

                        this.Height = form.Height + 39;
                        this.Width = form.Width + 16;
                    }
                    //}
                    form.Dock = DockStyle.Fill;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.TopLevel = false;
                    form.Show();
                    panelPage.Controls.Add(form);
                    //放PageTitleChanged前面
                    _ViewHashCode = "view" + form.GetHashCode().ToString();
                    if (PageTitleChanged != null)
                    {
                        PageTitleChanged(form.Text);
                    }
                    backButton.Enabled = false;
                    forwardButton.Enabled = false;

                    //注册窗体打开事件
                    if (form is BaseFormBusiness)
                    {
                        (form as BaseFormBusiness).ExecOpenWindowBefore(form, null);
                    }
                }
                catch (Exception err)
                {
                    SetUrl((string)InvokeController("GetWeb404"));
                }
            }
            else
            {
                _ViewHashCode = this.GetHashCode().ToString();
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
                    browser.MenuHandler = new MenuHandler();
                    browser.Dock = DockStyle.Fill;
                    //注册事件
                    browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
                    browser.LoadingStateChanged += OnLoadingStateChanged;
                    browser.ConsoleMessage += OnBrowserConsoleMessage;
                    browser.StatusMessage += OnBrowserStatusMessage;
                    browser.TitleChanged += OnBrowserTitleChanged;
                    browser.AddressChanged += OnBrowserAddressChanged;
                    //注册对象给JS调用
                    browser.RegisterAsyncJsObject("efwplusController", InvokeController("this"), false);
                }
                panelPage.Controls.Add(browser);
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    browser.Load(url);
                }

                this.Width = 800;
                this.Height = 600;


                backButton.Enabled = true;
                forwardButton.Enabled = true;
            }
        }

        private void Page_VisibleChanged(object sender, EventArgs e)
        {
            if ((sender as Form).Visible == false)
            {
                if (IsWindowMode)
                {
                    this.Close(); 
                }
                else
                {
                    //InvokeController("StartCloseTab");
                }
            }
        }

        public void ShowOrHideTools(bool val)
        {
            barTool.Visible = val;
        }

        #region 浏览器事件
        private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
            {
                var b = ((ChromiumWebBrowser)sender);

                this.InvokeOnUiThreadIfRequired(() => b.Focus());
            }
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            //DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            //this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            //this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => {
                Text = args.Title;
                if (PageTitleChanged != null)
                {
                    PageTitleChanged(args.Title);
                }
            });
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }
        #endregion

     

        public string GetUrl()
        {
            return urlTextBox.Text;
        }
    }

    internal class MenuHandler : IContextMenuHandler
    {

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear();
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return true;
        }
    }

   
}
