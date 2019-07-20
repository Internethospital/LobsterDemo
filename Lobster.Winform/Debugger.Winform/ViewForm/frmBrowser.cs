using Debugger.Winform.IView;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.WinformFrame.Controller;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using WebKit;

namespace Debugger.Winform.ViewForm
{
    public partial class frmBrowser : BaseFormBusiness, IfrmBrowser
    {
        private string httphead = "http://";
        private string httpshead = "https://";
        private string efwplushead = "efwplus://";
        WebKit.WebKitBrowser webKitBrowser;//浏览器对象

        public Action<string> PageTitleChanged
        {
            get;
            set;
        }

        public frmBrowser()
        {
            InitializeComponent();

            txturl.Focus();
        }

        #region 浏览器事件
        private WebKitBrowser oldWebKit;
        private void browser_NewWindowCreated(object sender, NewWindowCreatedEventArgs args)
        {
            try
            {
                //if (oldWebKit != null)
                //{
                //    this.Controls.Remove(oldWebKit);
                //}
                //oldWebKit = args.WebKitBrowser;
                //this.Controls.Add(oldWebKit);
                //oldWebKit.Navigated += new WebBrowserNavigatedEventHandler((object sender2, WebBrowserNavigatedEventArgs args2) =>
                //{
                //    InvokeController("CreateHyperlink", args2.Url.ToString());
                //});
                InvokeController("CreateHyperlink", args.WebKitBrowser);
            }
            catch { }
        }

        private void browser_NewWindowRequest(object sender, NewWindowRequestEventArgs args)
        {

        }

        private void browser_DownloadBegin(object sender, FileDownloadBeginEventArgs args)
        {
            //new DownloadForm(args.Download);
        }

        private void browser_Error(object sender, WebKitBrowserErrorEventArgs args)
        {
            try
            {
                webKitBrowser.DocumentText = "<html><head><title>Error</title></head><center><p>" + args.Description + "</p></center></html>";
            }
            catch { }
        }


        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            try
            {
                txturl.Text = webKitBrowser.Url.ToString();
                btnback.Enabled = webKitBrowser.CanGoBack;
                btnadvance.Enabled = webKitBrowser.CanGoForward;
                if (PageTitleChanged != null)
                {
                    PageTitleChanged(webKitBrowser.DocumentTitle);
                }
            }
            catch { }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                txturl.Text = webKitBrowser.Url.ToString();
                btnback.Enabled = webKitBrowser.CanGoBack;
                btnadvance.Enabled = webKitBrowser.CanGoForward;
                if (PageTitleChanged != null)
                {
                    PageTitleChanged(webKitBrowser.DocumentTitle);
                }

                Uri uri = new Uri(webKitBrowser.Url.ToString());
                if (string.IsNullOrEmpty(uri.Query) == false)
                {
                    string queryString = uri.Query;
                    NameValueCollection col = GetQueryString(queryString);
                    string efwplus = col["url"];
                    if (efwplus != null)
                        SetUrl(efwplus);
                }
            }
            catch { }
        }
        #endregion
        /// <summary>
        /// 设置URL
        /// </summary>
        /// <param name="url"></param>
        public void SetUrl(string url)
        {
            this.panelMain.Controls.Clear();
            txturl.Text = url;
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
                    form.Dock = DockStyle.Fill;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.TopLevel = false;
                    form.Show();
                    this.panelMain.Controls.Add(form);
                    if (PageTitleChanged != null)
                    {
                        PageTitleChanged(form.Text);
                    }
                }
                catch
                {
                    MessageBoxShowSimple("错误的地址");
                }
            }
            else
            {
                if (webKitBrowser == null)
                {
                    webKitBrowser = new WebKit.WebKitBrowser();
                    webKitBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
                    webKitBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.browser_Navigating);
                    //webKitBrowser.DocumentTitleChanged += new EventHandler(this.browser_DocumentTitleChanged);
                    webKitBrowser.Error += new WebKitBrowserErrorEventHandler(this.browser_Error);
                    webKitBrowser.DownloadBegin += new FileDownloadBeginEventHandler(this.browser_DownloadBegin);
                    webKitBrowser.NewWindowRequest += new NewWindowRequestEventHandler(this.browser_NewWindowRequest);
                    webKitBrowser.NewWindowCreated += new NewWindowCreatedEventHandler(this.browser_NewWindowCreated);
                }
                webKitBrowser.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(webKitBrowser);
                webKitBrowser.Navigate(url);
            }
        }

        public void SetWebBrowser(WebKitBrowser browser)
        {
            //string url = ((WebKit.Interop.IWebView)browser.u()).mainFrameURL();
            webKitBrowser = browser;
            webKitBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            webKitBrowser.Navigating += new WebBrowserNavigatingEventHandler(this.browser_Navigating);
            //webKitBrowser.DocumentTitleChanged += new EventHandler(this.browser_DocumentTitleChanged);
            webKitBrowser.Error += new WebKitBrowserErrorEventHandler(this.browser_Error);
            webKitBrowser.DownloadBegin += new FileDownloadBeginEventHandler(this.browser_DownloadBegin);
            webKitBrowser.NewWindowRequest += new NewWindowRequestEventHandler(this.browser_NewWindowRequest);
            webKitBrowser.NewWindowCreated += new NewWindowCreatedEventHandler(this.browser_NewWindowCreated);
            webKitBrowser.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(webKitBrowser);
        }


        private void txturl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnrefresh.PerformClick();
            }
        }
        /// <summary>
        /// 刷新url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            SetUrl(txturl.Text);
        }
        /// <summary>
        /// 主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhome_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnback_Click(object sender, EventArgs e)
        {
            webKitBrowser.GoBack();
        }
        /// <summary>
        /// 前进
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadvance_Click(object sender, EventArgs e)
        {
            webKitBrowser.GoForward();
        }

        #region 获取URL参数值
        /// 测试.
        /// </summary>
        public void Test()
        {
            string pageURL = "http://www.google.com.hk/search?hl=zh-CN&source=hp&q=%E5%8D%9A%E6%B1%87%E6%95%B0%E7%A0%81&aq=f&aqi=g2&aql=&oq=&gs_rfai=";
            Uri uri = new Uri(pageURL);
            string queryString = uri.Query;
            NameValueCollection col = GetQueryString(queryString);
            string searchKey = col["q"];
            //结果 searchKey = "博汇数码"
        }

        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryString(string queryString)
        {
            return GetQueryString(queryString, null, true);
        }

        /// <summary>
        /// 将查询字符串解析转换为名值集合.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="encoding"></param>
        /// <param name="isEncoded"></param>
        /// <returns></returns>
        public static NameValueCollection GetQueryString(string queryString, Encoding encoding, bool isEncoded)
        {
            queryString = queryString.Replace("?", "");
            NameValueCollection result = new NameValueCollection(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrEmpty(queryString))
            {
                int count = queryString.Length;
                for (int i = 0; i < count; i++)
                {
                    int startIndex = i;
                    int index = -1;
                    while (i < count)
                    {
                        char item = queryString[i];
                        if (item == '=')
                        {
                            if (index < 0)
                            {
                                index = i;
                            }
                        }
                        else if (item == '&')
                        {
                            break;
                        }
                        i++;
                    }
                    string key = null;
                    string value = null;
                    if (index >= 0)
                    {
                        key = queryString.Substring(startIndex, index - startIndex);
                        value = queryString.Substring(index + 1, (i - index) - 1);
                    }
                    else
                    {
                        key = queryString.Substring(startIndex, i - startIndex);
                    }
                    if (isEncoded)
                    {
                        result[MyUrlDeCode(key, encoding)] = MyUrlDeCode(value, encoding);
                    }
                    else
                    {
                        result[key] = value;
                    }
                    if ((i == (count - 1)) && (queryString[i] == '&'))
                    {
                        result[key] = string.Empty;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 解码URL.
        /// </summary>
        /// <param name="encoding">null为自动选择编码</param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MyUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                //首先用utf-8进行解码                     
                string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
                //将已经解码的字符再次进行编码.
                string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
                if (str == encode)
                    encoding = Encoding.UTF8;
                else
                    encoding = Encoding.GetEncoding("gb2312");
            }
            return HttpUtility.UrlDecode(str, encoding);
        }
        #endregion
    }
}
