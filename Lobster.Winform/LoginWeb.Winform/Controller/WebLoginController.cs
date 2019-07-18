using CefSharp;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WinformFrame.Controller;
using LoginWeb.Winform.IView;
using LoginWeb.Winform.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginWeb.Winform.Controller
{

    [WinformController(DefaultViewName = "FrmBrowserMain", Memo = "Web浏览器")]//与系统菜单对应
    [WinformView(Name = "FrmLogin", ViewTypeName = "LoginWeb.Winform.ViewForm.FrmLogin")]
    [WinformView(Name = "FrmBrowserMain", ViewTypeName = "LoginWeb.Winform.ViewForm.FrmBrowserMain")]
    public class WebLoginController : WcfClientController
    {
        private IfrmBrowserMain _ifrmBrowserMain;
        private IfrmLogin _ifrmLogin;
        public override void Init()
        {
            _ifrmBrowserMain = (IfrmBrowserMain)iBaseView["FrmBrowserMain"];
            // _ifrmLogin = (IfrmLogin)iBaseView["FrmLogin"];
            //StartNewTab("http://localhost:60591/admin/login/login.html", "登录");
          
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
            settings.Locale = "zh-CN";
            settings.AcceptLanguageList = "zh-CN";
            settings.LogSeverity = LogSeverity.Verbose;
            settings.CachePath = @"CefSharp\cache";
            settings.CefCommandLineArgs.Add("--enable-media-stream", "--enable-media-stream");
            settings.CefCommandLineArgs.Add("--enable-usermedia-screen-capturing", "--enable-usermedia-screen-capturing");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing", "enable-usermedia-screen-capturing");
            settings.CefCommandLineArgs.Add("enable-media-stream", "enable-media-stream");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.LogFile = @"CefSharp\debug.log";
            //禁用GPU，防止切换时黑屏
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
            settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            settings.CefCommandLineArgs.Add("disable-direct-write", "1");

            Cef.Initialize(settings, false, false);
        }

        [WinformMethod]
        public void CefShutdown()
        {
            Cef.Shutdown();
        }

        /// <summary>
        /// 打开新标签
        /// </summary>
        [WinformMethod]
        public void NewTab()
        {
            _ifrmBrowserMain.OpenDocument(null);
        }

        //打开指定界面
        [WinformMethod]
        public void StartNewTab(string url, string title)
        {
            _ifrmBrowserMain.UiThreadOpenDocument(url, title);
        }

        //打开指定界面
        [WinformMethod]
        public void StartNewWindow(string url, string title)
        {
            _ifrmBrowserMain.UiThreadOpenWindow(url, title);
        }

        [WinformMethod]
        public void StartCloseTab(string hashcode)
        {
            _ifrmBrowserMain.UiThreadCloseDocument(hashcode);
        }

        [WinformMethod]
        public void UserLoginWinform(string userId)
        {
            _ifrmBrowserMain.UiThreadUserLoginPrefect(userId);
        }

        //新标签
        [WinformMethod]
        public void NewTab2(string url, string title)
        {
            _ifrmBrowserMain.OpenDocument(new string[] { url, title });
        }

        /// <summary>
        /// 关闭Tab
        /// </summary>
        /// <param name="hashcode">页面的Hash代码</param>
        [WinformMethod]
        public void CloseTab(string hashcode)
        {
            _ifrmBrowserMain.CloseDocument(hashcode);
        }

        [WinformMethod]
        public string GetWebUrl()
        {
            ServiceResponseData retData = InvokeWcfService("MainFrame.Service", "LoginController", "GetUrlLoginValue");
            string result = retData.GetData<string>(0);
            if (result != "")
                result = result.Substring(0, result.IndexOf("login"));
            return result;
        }
        //显示首页
        [WinformMethod]
        public void ShowHome()
        {
            //string Urlresult = "http://ih.efwplus.cn/admin/login/login.html";

            ServiceResponseData retData = InvokeWcfService("MainFrame.Service", "LoginController", "GetUrlLoginValue");
            string result = retData.GetData<string>(0);
            string Urlresult = result == "" ? "http://ih.efwplus.cn/admin/login/login.html" : result;

            string title = "首页";
            _ifrmBrowserMain.OpenDocument(new string[] { Urlresult, title });
        }

        //打开登录窗体
        [WinformMethod]
        public void ShowLogin()
        {
            //((Form)_ifrmLogin).ShowDialog();
            ShowHome();
        }

        //登录
        [WinformMethod]
        public void UserLogin()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(_ifrmLogin.usercode);
                request.AddData(_ifrmLogin.password);
            });

            ServiceResponseData retdata = InvokeWcfService("MainFrame.Service", "LoginController", "UserLogin", requestAction);

            string UserName = retdata.GetData<string>(0);
            string DeptName = retdata.GetData<string>(1);
            string WorkName = retdata.GetData<string>(2);

            List<BaseModule> modules = retdata.GetData<List<BaseModule>>(3);
            List<BaseMenu> menus = retdata.GetData<List<BaseMenu>>(4);
            List<BaseDept> depts = retdata.GetData<List<BaseDept>>(5);

            SetUserInfo(retdata.GetData<SysLoginRight>(6));

            //如果是重新登录则需要重新加载云软件
            if (_ifrmLogin.isReLogin)
            {
                EFWCoreLib.WinformFrame.CloudSoftClientManager.ReLoadCloudSoft(this);
            }

            _ifrmBrowserMain.LoadLoginData(UserName, DeptName, WorkName, modules, menus, depts);
        }

        /// <summary>
        /// 登录改造 updated by linda on 2018-12-11
        /// </summary>
        /// <param name="userId">用户Id</param>
        [WinformMethod]
        public void UserLoginPrefect(string userId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(Convert.ToInt32(userId));
            });

            ServiceResponseData retdata = InvokeWcfService("MainFrame.Service", "LoginController", "UserLoginPrefect", requestAction);

            string UserName = retdata.GetData<string>(0);
            string DeptName = retdata.GetData<string>(1);
            string WorkName = retdata.GetData<string>(2);

            List<BaseModule> modules = retdata.GetData<List<BaseModule>>(3);
            List<BaseMenu> menus = retdata.GetData<List<BaseMenu>>(4);
            List<BaseDept> depts = retdata.GetData<List<BaseDept>>(5);

            SetUserInfo(retdata.GetData<SysLoginRight>(6));

            //如果是重新登录则需要重新加载云软件

            EFWCoreLib.WinformFrame.CloudSoftClientManager.ReLoadCloudSoft(this);

            _ifrmBrowserMain.LoadLoginData(UserName, DeptName, WorkName, modules, menus, depts);
        }

        //切换科室
        [WinformMethod]
        public void ChangedDeptName(string DeptName)
        {
            _ifrmBrowserMain.ChangedDeptName(DeptName);
        }

        //显示修改密码界面
        [WinformMethod]
        public void ShowAlterPass()
        {
            InvokeController("WebLoginOtherController", "OpenPass");
        }
        //显示切换科室界面
        [WinformMethod]
        public void ShowReDept(List<BaseDept> depts)
        {
            InvokeController("WebLoginOtherController", "OpenReDept",depts);
        }
        //显示系统设置界面
        [WinformMethod]
        public void ShowSetting()
        {
            InvokeController("WebLoginOtherController", "OpenSetting");
        }
        //获取系统消息
        [WinformMethod]
        public List<BaseMessage> GetNotReadMessages()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.UserId);
            });
            ServiceResponseData retdata = InvokeWcfService("MainFrame.Service", "LoginController", "GetNotReadMessages", requestAction);
            return retdata.GetData<List<BaseMessage>>(0);
        }
        [WinformMethod]
        public void MessageRead(int[] messageIds)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(messageIds);
                //request.AddData(LoginUserInfo.EmpId);
            });
            Object retdata = InvokeWcfService("MainFrame.Service", "LoginController", "MessageRead", requestAction);
        }

    }
}
