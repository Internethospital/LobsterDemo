using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IdlgNewSoft: IBaseView
    {
        /// <summary>
        /// 软件类型 0客户端软件 1服务端软件
        /// </summary>
        int SoftType { get; set; }
    }
}
