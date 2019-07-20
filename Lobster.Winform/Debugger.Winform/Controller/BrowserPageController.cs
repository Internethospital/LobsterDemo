using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WinformFrame.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Debugger.Winform.IView;

namespace Debugger.Winform.Controller
{

    /// <summary>
    /// 浏览器页面控制器
    /// </summary>
    [WinformController(DefaultViewName = "frmBrowser", Memo = "浏览器页面")]//在菜单上显示
    [WinformView(Name = "frmBrowser", ViewTypeName = "Debugger.Winform.ViewForm.frmBrowser")]
    public class BrowserPageController: WinformController
    {
        IfrmBrowser _ifrmBrowser;
        public override void Init()
        {
            _ifrmBrowser = (IfrmBrowser)DefaultView;
        }


        [WinformMethod]
        public void OpenUrl(string url)
        {
            _ifrmBrowser.SetUrl(url);
        }


        [WinformMethod]
        public void CreateHyperlink(WebKit.WebKitBrowser browser)
        {
            IBaseView view = ControllerHelper.ShowView(this.SoftName, Guid.NewGuid().ToString(), "BrowserPageController", "frmBrowser", false);
            (view as IfrmBrowser).SetWebBrowser(browser);
            InvokeController("SoftBrowserController", "OpenView", view);
        }
    }
}
