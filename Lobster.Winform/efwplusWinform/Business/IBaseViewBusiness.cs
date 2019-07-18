
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace efwplusWinform.Business
{
   
    /// <summary>
    /// 控制器基础接口
    /// </summary>
    public interface IBaseViewBusiness
    {
        /// <summary>
        /// 控制器事件
        /// </summary>
        //event ControllerEventHandler ControllerEvent;
        string frmName { get; set; }
        ControllerEventHandler InvokeController { get; set; } 
    }
}
