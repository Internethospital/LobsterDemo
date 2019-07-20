using EFWCoreLib.WinformFrame.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmBrowser: IBaseView
    {
        /// <summary>
        /// 页面标题改变
        /// </summary>
        Action<string> PageTitleChanged { get; set; }
        /// <summary>
        /// 设置URL
        /// </summary>
        /// <param name="url"></param>
        void SetUrl(string url);
        /// <summary>
        /// 设置浏览器控件
        /// </summary>
        /// <param name="browser"></param>
        void SetWebBrowser(WebKit.WebKitBrowser browser);
    }
}
