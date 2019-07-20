using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.Common
{
    public class ChromiumWebBrowserEx : CefSharp.WinForms.ChromiumWebBrowser
    {
        public ChromiumWebBrowserEx()
            : base(null)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
            //this.DownloadHandler = new DownloadHandler(this);
        }
        public ChromiumWebBrowserEx(string url) : base(url)
        {
            this.LifeSpanHandler = new CefLifeSpanHandler();
        }
        public event EventHandler<NewWindowEventArgs> StartNewWindow;
        public void OnNewWindow(NewWindowEventArgs e)
        {
            if (StartNewWindow != null)
            {
                StartNewWindow(this, e);
            }
        }
    }

    public class CefLifeSpanHandler : CefSharp.ILifeSpanHandler
    {
        public CefLifeSpanHandler()
        {
        }
        public bool DoClose(IWebBrowser browserControl, CefSharp.IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }
            return true;
        }
        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }
        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }
        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            var chromiumWebBrowser = (ChromiumWebBrowserEx)browserControl;
            chromiumWebBrowser.Invoke(new Action(() =>
            {
                NewWindowEventArgs e = new NewWindowEventArgs(windowInfo, targetUrl);
                chromiumWebBrowser.OnNewWindow(e);
            }));
            newBrowser = null;
            return true;
        }
    }

    public class NewWindowEventArgs : EventArgs
    {
        private IWindowInfo _windowInfo;
        public IWindowInfo WindowInfo
        {
            get { return _windowInfo; }
            set { value = _windowInfo; }
        }
        public string url { get; set; }
        public NewWindowEventArgs(IWindowInfo windowInfo, string url)
        {
            _windowInfo = windowInfo;
            this.url = url;
        }
    }
}
