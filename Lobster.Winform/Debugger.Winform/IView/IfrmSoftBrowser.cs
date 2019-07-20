using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.IView
{
    public interface IfrmSoftBrowser : IBaseView
    {
        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="view"></param>
        void OpenDocumentView(Control view);

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="softnav"></param>
        /// <param name="script"></param>
        /// <param name="trace"></param>
        void InitView(Control softnav, Control script, Control trace);

        /// <summary>
        /// 关闭所有界面
        /// </summary>
        void ClearBarDocument();
    }
}
