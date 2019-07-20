using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmXMLEditor : IfrmTextEditor
    {
        /// <summary>
        /// 导入配置文件
        /// </summary>
        /// <param name="file">文件</param>
        void LoadViewFile(string file);
    }
}
