using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IdlgNewFile: IBaseView
    {
        /// <summary>
        /// 文件类型 0：Controller文件 1：Model文件 2：View文件
        /// </summary>
        int fileType { get; set; }
        /// <summary>
        /// 文件夹路径
        /// </summary>
        string path { get; set; }

        //文件名
        string filename { get; set; }

        bool isOk { get; set; }
    }
}
