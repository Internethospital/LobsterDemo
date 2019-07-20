using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmTraceDebugger : IBaseView
    {
        void ShowMsg(DateTime date, object text);
    }
}
