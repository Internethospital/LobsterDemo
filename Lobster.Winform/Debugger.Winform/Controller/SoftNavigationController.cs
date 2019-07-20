using Debugger.Winform.IView;
using efwplusWinform;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.Controller
{
    [WinformController(DefaultViewName = "frmSoftNavigation", Memo = "云软件导航地址")]//在菜单上显示
    [WinformView(Name = "frmSoftNavigation",ViewTypeName = "Debugger.Winform.ViewForm.frmSoftNavigation")]//控制器关联的界面
    public class SoftNavigationController : WinformController
    {
        /// <summary>
        /// 嵌入主界面显示
        /// </summary>
        private bool Embedded = false;
        IfrmSoftNavigation _ifrmSoftNavigation;
        public override void Init()
        {
            _ifrmSoftNavigation = (IfrmSoftNavigation)DefaultView;
            GetRefreshController();
        }

        [WinformMethod]
        public void GetRefreshController()
        {
            List<CloudSoftClient> CSoftClientList = WinformGlobal.CSoftClientList.FindAll(x => (x.SoftName != "default" && x.SoftName != "Login"));
            _ifrmSoftNavigation.LoadControllerAddress(CSoftClientList);
        }

        [WinformMethod]
        public void OpenNode(string adress)
        {
            string[] args = adress.Split('#');
            if (Embedded)
            {
                IBaseView view = ControllerHelper.ShowView(args[0], args[1], args[1], args[2], false);
                InvokeController("SoftBrowserController", "OpenView", view);
            }
            else//非嵌入式，独立窗口显示
            {
                //打开界面
                ControllerHelper.ShowView(args[0], args[1], args[1], args[2], true);
            }
        }

        /// <summary>
        /// 显示云软件导航界面，提供给外部打开
        /// </summary>
        [WinformMethod]
        public object ShowSoftNavigationView()
        {
            Embedded = true;
            return _ifrmSoftNavigation;
        }

        [WinformMethod]
        public void ClearAllCache()
        {
            foreach (var client in WinformGlobal.CSoftClientList)
            {
                if (client.SoftName == SoftName)
                {
                    client.ClearAllWinformController("SoftBrowserController", "SoftNavigationController", "ScriptManageController", "TraceDebuggerController");
                }
                else
                {
                    client.ClearAllWinformController();
                }
            }

            MessageBoxShowSimple("操作成功！");
        }

        [WinformMethod]
        public void ClearCacheByName(string softname,string controllername)
        {
            CloudSoftClient client = WinformGlobal.CSoftClientList.Find(x => x.SoftName == softname);
            client.ClearWinformController(controllername);
            MessageBoxShowSimple("操作成功！");
        }
    }
}
