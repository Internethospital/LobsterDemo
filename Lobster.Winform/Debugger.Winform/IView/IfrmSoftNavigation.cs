using efwplusWinform;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmSoftNavigation : IBaseView
    {
        /// <summary>
        /// 导入控制器地址
        /// </summary>
        /// <param name="calist"></param>
        void LoadControllerAddress(List<CloudSoftClient> clientlist);
    }
}
