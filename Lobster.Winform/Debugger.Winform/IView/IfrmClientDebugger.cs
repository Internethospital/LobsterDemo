using efwplusWinform;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmClientDebugger : IBaseView
    {
        void LoadControllerAttr(List<CloudSoftClient> clientlist);
        void SetText(string softname,string controllerid, string controllername, string methodname, string parameters_json);
    }
}
