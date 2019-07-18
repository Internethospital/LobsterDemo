using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WinformFrame.Controller;
using LoginWeb.Winform.IView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginWeb.Winform.Controller
{
    /// <summary>
    /// 浏览器页面控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmBrowserPage", Memo = "浏览器页面")]//在菜单上显示
    [WinformView(Name = "FrmBrowserPage", ViewTypeName = "LoginWeb.Winform.ViewForm.FrmBrowserPage")]
    public class BrowserPageController : WinformController
    {
        IfrmBrowserPage _ifrmBrowserPage;
        public override void Init()
        {
            _ifrmBrowserPage = (IfrmBrowserPage)DefaultView;
        }

        //提供给JS调用
        [WinformMethod]
        public void StartNewTab(string url,string title)
        {
            InvokeController("WebLoginController", "StartNewTab", url,title);
        }

        //提供给JS调用
        [WinformMethod]
        public void StartNewWindow(string url,string title)
        {
            InvokeController("WebLoginController", "StartNewWindow", url,title);
        }

        //提供给JS调用
        [WinformMethod]
        public void StartCloseTab()
        {
            InvokeController("WebLoginController", "StartCloseTab", _ifrmBrowserPage.ViewHashCode);
        }

        //提供给JS调用
        [WinformMethod]
        public void UserLoginPrefect(string userId)
        {
            InvokeController("WebLoginController", "UserLoginWinform", userId);
        }

        [WinformMethod]
        public string GetWeb404()
        {
            return InvokeController("WebLoginController", "GetWebUrl").ToString()+"admin/template/404.html";
        }
    }
}
