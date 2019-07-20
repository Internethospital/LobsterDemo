
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmScriptNavigation : IBaseView
    {
        /// <summary>
        /// 导入路径内所有文件
        /// </summary>
        /// <param name="path"></param>
        void loadTree(string path);
        /// <summary>
        /// 选定路径
        /// </summary>
        string selectedPath { get; }
        /// <summary>
        /// 选定文本
        /// </summary>
        string selectedText { get; }
    }
}
