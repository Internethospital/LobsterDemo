using EFWCoreLib.WinformFrame.Controller;
using LoginWeb.Winform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginWeb.Winform.IView
{
    public interface IfrmBrowserMain: IBaseView
    {
        #region 操作界面
        /// <summary>
        /// UI线程打开界面
        /// </summary>
        void UiThreadOpenDocument(string url, string title);
        /// <summary>
        /// UI线程关闭界面
        /// </summary>
        /// <param name="hashcode"></param>
        void UiThreadCloseDocument(string hashcode);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        void UiThreadUserLoginPrefect(string userId);
        /// <summary>
        /// UI线程弹窗
        /// </summary>
        /// <param name="url"></param>
        void UiThreadOpenWindow(string url, string title);
        /// <summary>
        /// 打开弹窗
        /// </summary>
        /// <param name="url"></param>
        void OpenWindow(Object state);
        /// <summary>
        /// 打开文档界面
        /// </summary>
        /// <param name="url"></param>
        void OpenDocument(Object state);
        /// <summary>
        /// 关闭文档界面
        /// </summary>
        /// <param name="hashcode"></param>
        void CloseDocument(Object state);

        #endregion
        /// <summary>
        /// 加载登录数据
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="DeptName"></param>
        /// <param name="WorkName"></param>
        /// <param name="modules">系统模块</param>
        /// <param name="menus">菜单列表</param>
        /// <param name="depts">包含科室</param>
        void LoadLoginData(string UserName,string DeptName,string WorkName, List<BaseModule> modules, List<BaseMenu> menus, List<BaseDept> depts);

        /// <summary>
        /// 切换科室
        /// </summary>
        /// <param name="DeptName"></param>
        void ChangedDeptName(string DeptName);
    }
}
