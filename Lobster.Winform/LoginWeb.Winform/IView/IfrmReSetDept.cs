using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WinformFrame.Controller;
using LoginWeb.Winform.Model;

namespace LoginWeb.Winform.IView
{
    public interface IfrmReSetDept:IBaseView
    {
        string UserName { set; }
        string WorkName { set; }
        void loadDepts(List<BaseDept> list, int selectDeptId);
        BaseDept getDept();
    }
}
