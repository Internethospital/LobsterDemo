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
    /// <summary>
    /// 客户端调试
    /// </summary>
    [WinformController(DefaultViewName = "frmClientDebugger",Memo = "客户端调试")]//在菜单上显示
    [WinformView(Name = "frmClientDebugger", ViewTypeName = "Debugger.Winform.ViewForm.frmClientDebugger")]
    public class ClientDebuggerController : WinformController
    {
        IfrmClientDebugger _ifrmClientDebugger;
        public override void Init()
        {
            _ifrmClientDebugger = (IfrmClientDebugger)DefaultView;
        }


        [WinformMethod]
        public void GetAllCAttrList()
        {
            _ifrmClientDebugger.LoadControllerAttr(WinformGlobal.CSoftClientList);
        }

        [WinformMethod]
        public void OpenNode(string tag)
        {
            string[] args = tag.Split('#');
            if (args[0] == "0")//打开界面
            {
                ControllerHelper.ShowView(args[1], args[2], args[2], args[3], true);
            }
            else if (args[0] == "1")//
            {
                _ifrmClientDebugger.SetText(args[1], args[2], args[2],args[3], "");
            }
        }

        /// <summary>
        /// 请求控制器
        /// </summary>
        [WinformMethod]
        public void RequestController(string softname, string controllerid, string controllername, string methodname, string parameters_json)
        {
            string[] paramArr = parameters_json.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            ControllerHelper.Execute(softname, controllerid, controllername, methodname, paramArr);
        }


        [WinformMethod]
        public void ClearAllCache()
        {
            foreach (var client in WinformGlobal.CSoftClientList)
            {
                if (client.SoftName == SoftName)
                {
                    client.ClearAllWinformController("ClientDebuggerController");
                }
                else
                {
                    client.ClearAllWinformController();
                }
            }

            MessageBoxShowSimple("操作成功！");
        }
    }
}
