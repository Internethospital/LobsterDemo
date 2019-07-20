using CefSharp;
using Debugger.Winform.IView;
using efwplusWinform;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.Controller
{
    [WinformController(DefaultViewName = "frmSoftBrowser", Memo = "云软件浏览器")]//在菜单上显示
    [WinformView(Name = "frmSoftBrowser", ViewTypeName = "Debugger.Winform.ViewForm.frmSoftBrowser")]//控制器关联的界面
    public class SoftBrowserController : WinformController
    {
        IfrmSoftBrowser _ifrmSoftBrowser;
        public override void Init()
        {
            #region 设置Cef浏览器控件
            string lib = string.Empty;
            string browser = string.Empty;
            string locales = string.Empty;
            string res = string.Empty;
            lib = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"CefSharp\libcef.dll");
            browser = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"CefSharp\CefSharp.BrowserSubprocess.exe");
            locales = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"CefSharp\locales");
            res = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"CefSharp\");

            var settings = new CefSettings
            {
                BrowserSubprocessPath = browser,
                LocalesDirPath = locales,
                ResourcesDirPath = res
            };

            settings.LogSeverity = LogSeverity.Verbose;
            settings.CachePath = @"CefSharp\cache";
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.LogFile = @"CefSharp\debug.log";
            //禁用GPU，防止切换时黑屏
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
            settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            settings.CefCommandLineArgs.Add("disable-direct-write", "1");

            Cef.Initialize(settings, false, false);
            #endregion

            _ifrmSoftBrowser = (IfrmSoftBrowser)DefaultView;

            object softnav = InvokeController("SoftNavigationController", "ShowSoftNavigationView");
            object script = InvokeController("ScriptManageController", "ShowScriptView");
            object trace = InvokeController("TraceDebuggerController", "ShowTraceView");

            _ifrmSoftBrowser.InitView(softnav as Control, script as Control, trace as Control);
        }

        [WinformMethod]
        public void CefShutdown()
        {
            Cef.Shutdown();
        }

        [WinformMethod]
        public void OpenView(object view)
        {
            _ifrmSoftBrowser.OpenDocumentView(view as Control);
        }

        /// <summary>
        /// 重新加载云软件
        /// </summary>
        [WinformMethod]
        public void ReLoadSoft()
        {
            List<WinformController> wclist = new List<WinformController>();
            wclist.Add(ControllerHelper.CreateController(SoftName, "SoftBrowserController", "SoftBrowserController"));
            wclist.Add(ControllerHelper.CreateController(SoftName, "SoftNavigationController", "SoftNavigationController"));
            wclist.Add(ControllerHelper.CreateController(SoftName, "ScriptManageController", "ScriptManageController"));
            wclist.Add(ControllerHelper.CreateController(SoftName, "TraceDebuggerController", "TraceDebuggerController"));
            CloudSoftClientManager.ReLoadCloudSoft(wclist.ToArray());
            InvokeController("SoftNavigationController", "GetRefreshController");
            _ifrmSoftBrowser.ClearBarDocument();
        }

    }
}
