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
    /// 跟踪调试
    /// </summary>
    [WinformController(DefaultViewName = "frmTraceDebugger", Memo = "跟踪调试")]//在菜单上显示
    [WinformView(Name = "frmTraceDebugger", ViewTypeName = "Debugger.Winform.ViewForm.frmTraceDebugger")]
    public class TraceDebuggerController: WinformController
    {
        IfrmTraceDebugger _ifrmTraceDebugger;
        public override void Init()
        {
            _ifrmTraceDebugger = (IfrmTraceDebugger)DefaultView;

            efwplusWinform.ViewRender.ScriptTrace.handler = new efwplusWinform.ViewRender.ScriptTraceHandler(_ifrmTraceDebugger.ShowMsg);
            efwplusWinform.Common.Log.handler=new efwplusWinform.Common.LogHandler(_ifrmTraceDebugger.ShowMsg);
        }

        /// <summary>
        /// 显示调试日志界面，提供给外部调用
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public object ShowTraceView()
        {
            return _ifrmTraceDebugger;
        }
    }
}
