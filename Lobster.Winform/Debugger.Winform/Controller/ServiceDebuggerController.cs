using Debugger.Winform.IView;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.Controller
{
    /// <summary>
    /// 服务端调试
    /// </summary>
    [WinformController(DefaultViewName = "frmServiceDebugger",Memo = "服务端调试")]//在菜单上显示
    [WinformView(Name = "frmServiceDebugger", ViewTypeName = "Debugger.Winform.ViewForm.frmServiceDebugger")]
    public class ServiceDebuggerController: WinformController
    {
        IfrmServiceDebugger _ifrmServiceDebugger;
        public override void Init()
        {
            _ifrmServiceDebugger = (IfrmServiceDebugger)DefaultView;
        }

        [WinformMethod]
        public void GetAllService()
        {
            //List<dwPlugin> plist = ClientLinkManage.CreateConnection("Test").GetWcfServicesAllInfo();
            //_ifrmServiceDebugger.LoadPlugin(plist);
        }


    }
}
