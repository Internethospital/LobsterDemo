using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{

    

    public interface IfrmPythonEditor : IfrmTextEditor
    {
        /// <summary>
        /// 导入脚本文件
        /// </summary>
        /// <param name="file">文件</param>
        void LoadScriptFile(string file);
    }
}
