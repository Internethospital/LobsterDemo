/*
 *控制器的目的：
 *使界面对象与服务对象达到隔离和重用的目的 
 *所以控制器是把界面对象与服务对象组合一些业务功能、一些菜单。
 *如果一个界面有两个菜单那就分开建两个控制器对象。
 */


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar;
using Microsoft.Scripting.Hosting;
using efwplusWinform.ViewRender;
using efwplusWinform.Business;

namespace efwplusWinform.Controller
{
 
    /// <summary>
    /// Winform控制器基类
    /// </summary>
    public class WinformController 
    {
        /// <summary>
        /// 获取系统登录用户信息
        /// </summary>
        public SysLoginRight LoginUserInfo
        {
            get
            {
                if (WinformGlobal.LoginUserInfo == null)
                {
                    return new SysLoginRight(1);
                }
                return WinformGlobal.LoginUserInfo;
            }
        }
        /// <summary>
        /// 设置登录用户
        /// </summary>
        /// <param name="user"></param>
        public void SetUserInfo(SysLoginRight user)
        {
            WinformGlobal.LoginUserInfo = user;
        }

        private string _softname;
        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftName
        {
            get { return _softname; }
            set { _softname = value; }
        }

        private string _controllerId;
        /// <summary>
        /// 控制器标识
        /// </summary>
        public string ControllerId
        {
            get
            {
                return string.IsNullOrEmpty(_controllerId) ? this.GetType().Name : _controllerId;
            }
            set { _controllerId = value; }
        }

        internal IBaseView _defaultView;
        /// <summary>
        /// 默认界面
        /// </summary>
        public IBaseView DefaultView
        {
            get {
                if (_defaultView == null && _iBaseView.Count>0)
                {
                    return _iBaseView.First().Value;
                }
                return _defaultView;
            }
            set { _defaultView = value; }
        }

        private Dictionary<string, IBaseView> _iBaseView;
        /// <summary>
        /// 界面列表
        /// </summary>
        public Dictionary<string, IBaseView> iBaseView
        {
            get { return _iBaseView; }
            set
            {
                _iBaseView = value;
            }
        }
        private Dictionary<string, RenderEngine> _enginelist;
        /// <summary>
        /// 渲染对象列表，每个界面对应的渲染对象
        /// </summary>
        public Dictionary<string, RenderEngine> RenderList
        {
            get { return _enginelist; }
            set { _enginelist = value; }
        }

        private ScriptScope _ScriptScope;
        /// <summary>
        /// 脚本对象
        /// </summary>
        public ScriptScope ScriptScope
        {
            get { return _ScriptScope; }
            set { _ScriptScope = value; }
        }

        /// <summary>
        /// 创建WinformController的实例
        /// </summary>
        public WinformController()
        {
            
        }
        /// <summary>
        /// 界面控制事件
        /// </summary>
        /// <param name="eventname">事件名称</param>
        /// <param name="objs">参数数组</param>
        /// <returns></returns>
        public virtual object UI_ControllerEvent(string eventname, params object[] objs)
        {
            try
            {
                object[] _objs;
                switch (eventname)
                {
                    case "Show"://显示界面在主界面
                        if (objs.Length > 0)
                        {
                            if (WinformGlobal._entrycontroller == "BasicLoginController")
                            {
                                Form form = null;
                                if (objs[0] is String)//根据名称取控制器中的界面对象
                                {
                                    form = iBaseView[objs[0].ToString()] as Form;
                                }
                                else//直接显示界面对象
                                {
                                    form = objs[0] as Form;
                                }

                                if (objs.Length == 1)//取界面标题为Tab标题
                                {
                                    string tabName = form.Text;
                                    string tabId = "view" + form.GetHashCode();
                                    _objs = new object[] { form, tabName, tabId };
                                    InvokeControllerEx("Login", "BasicLoginController", "BasicLoginController", "ShowForm", _objs);
                                }
                                else if (objs.Length == 2)//自定义Tab标题
                                {
                                    string tabName = objs[1].ToString();
                                    string tabId = "view" + form.GetHashCode();
                                    _objs = new object[] { form, tabName, tabId };
                                    InvokeControllerEx("Login", "BasicLoginController", "BasicLoginController", "ShowForm", _objs);
                                }
                            }
                            else if (WinformGlobal._entrycontroller == "WebLoginController")
                            {
                                string viewname = objs[0].ToString();
                                string url = "efwplus://" + SoftName + "/" + ControllerId + "/" + viewname;
                                string title = (iBaseView[viewname] as Form).Text;
                                _objs = new object[] { url, title };
                                InvokeControllerEx("Login", "WebLoginController", "WebLoginController", "StartNewTab", _objs);
                            }
                        }
                        return true;
                    case "ShowDialog"://弹窗的方式显示界面
                        if (objs.Length > 0)
                        {
                            Form form = null;
                            if (objs[0] is String)//根据名称取控制器中的界面对象
                            {
                                form = iBaseView[objs[0].ToString()] as Form;
                            }
                            else//直接显示界面对象
                            {
                                form = objs[0] as Form;
                            }
                            return form.ShowDialog();
                        }
                        return false;
                    case "Close"://关闭主界面上的Tab
                        
                        if (objs[0] is Form)
                        {
                            string tabId = "view" + objs[0].GetHashCode();
                            _objs = new object[] { tabId };
                            //(objs[0] as Form).Hide();
                        }
                        else
                        {
                            string tabId = objs[0].ToString();
                            _objs = new object[] {  tabId };
                        }
                        if(WinformGlobal._entrycontroller== "BasicLoginController")
                        {
                            InvokeControllerEx("Login", "BasicLoginController", "BasicLoginController", "CloseForm", _objs);//关闭Tab
                        }
                        else if(WinformGlobal._entrycontroller == "WebLoginController")
                        {
                            InvokeControllerEx("Login", "WebLoginController", "WebLoginController", "CloseTab", _objs);//关闭Tab
                        }
                        return true;
                    case "Exit"://退出程序
                        WinformGlobal.Exit();
                        return null;
                    case "this"://返回控制器对象
                        return this;
                    case "LoginUserInfo"://返回登录用户
                        return LoginUserInfo;
                    case "AsynInitCompleted"://异步初始化完成
                        if (AsynInitCompletedFinish == false)
                        {
                            AsynInitCompletedFinish = true;
                            AsynInitCompleted();
                        }
                        return true;
                    case "AsynInitCompletedForm":
                        if (objs.Length > 0)
                        {
                            AsynInitCompletedForm(objs[0].ToString());
                        }
                        return true;
                    //case "MessageBoxShowSimple"://右下角弹出提示框
                    //    if (objs.Length > 0)
                    //    {
                    //        MessageBoxShowSimple(objs[0].ToString()); 
                    //    }
                    //    return true;
                }
                //InvokeController("FrmInStoreDS@SetSelectedDept", Convert.ToInt32(cmbDept.SelectedValue));
                if (eventname.IndexOf('@') > -1)//重新设置界面 
                {
                    string frmName = eventname.Split('@')[0];
                    SetView(frmName);
                    eventname= eventname.Split('@')[1];
                }
                return InvokeControllerEx(this.ControllerId,this.GetType().Name, eventname, objs);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //public bool InitFinish = false;//是否完成初始化
        public bool AsynInitCompletedFinish = false;//是否完成异步初始化
        /// <summary>
        /// 控制器实例化后初始化加载
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// 异步初始化
        /// </summary>
        public virtual void AsynInit()
        {

        }
        

        /// <summary>
        /// 异步完成后给所有窗口显示数据
        /// </summary>
        public virtual void AsynInitCompleted()
        {

        }
        /// <summary>
        /// 异步完成后给指定窗口显示数据
        /// </summary>
        /// <param name="frmName"></param>
        public virtual void AsynInitCompletedForm(string frmName)
        {

        }
        /// <summary>
        /// 设置界面
        /// </summary>
        /// <param name="frmName"></param>
        public virtual void SetView(string frmName)
        {

        }
        /// <summary>
        /// 获取界面
        /// </summary>
        /// <param name="frmName">根据界面名称</param>
        /// <returns></returns>
        public virtual IBaseView GetView(string frmName)
        {
            return iBaseView[frmName];
        }

        /// <summary>
        /// 显示默认窗口
        /// </summary>
        public void ShowDefaultForm()
        {
            (this.DefaultView as Form).Show();
        }

        /// <summary>
        /// 显示指定窗口
        /// </summary>
        /// <param name="frmName">窗口名</param>
        public void ShowForm(string frmName)
        {
            if (iBaseView.ContainsKey(frmName))
            {
                (iBaseView[frmName] as Form).Show();
            }
        }

        /// <summary>
        /// 显示指定窗口
        /// </summary>
        /// <param name="frmName">窗口名</param>
        public void ShowDialog(string frmName)
        {
            if (iBaseView.ContainsKey(frmName))
            {
                (iBaseView[frmName] as Form).ShowDialog();
            }
        }
        /// <summary>
        /// 执行控制器
        /// </summary>
        /// <returns></returns>
        public Object InvokeController(string controllerName, string methodName, params object[] objs)
        {
            return ControllerHelper.Execute(SoftName, controllerName, controllerName, methodName, objs);
        }

        /// <summary>
        /// 执行控制器
        /// </summary>
        /// <returns></returns>
        public Object InvokeControllerEx(string controllerId, string controllerName, string methodName, object[] objs)
        {
            return ControllerHelper.Execute(SoftName,controllerId, controllerName, methodName, objs);
        }

        /// <summary>
        /// 执行控制器
        /// </summary>
        /// <returns></returns>
        public Object InvokeControllerEx(string softName, string controllerId, string controllerName, string methodName, object[] objs)
        {
            return ControllerHelper.Execute(softName, controllerId, controllerName, methodName, objs);
        }
        /// <summary>
        /// 右下角弹出提示框
        /// </summary>
        /// <param name="text">提示内容</param>
        public void MessageBoxShowSimple(string text)
        {
            //InvokeController("wcfclientLoginController", "ShowBalloonMessage", new object[] { "", text });
            //MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            string CaptionText = "";//默认空
            //InvokeController("MessageBoxShowSimple", text);
            DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
            Rectangle r = Screen.GetWorkingArea(DefaultView as Control);
            b.Size = new Size(280, 120);
            b.Location = new Point(r.Right - b.Width, r.Bottom - b.Height);
            b.AlertAnimation = eAlertAnimation.BottomToTop;
            b.Style = eBallonStyle.Office2007Alert;
            b.CaptionText = CaptionText;
            b.Font = new Font("宋体", 11f);
            //b.Padding = new System.Windows.Forms.Padding(5);
            b.Text = text;
            //b.AlertAnimation=eAlertAnimation.TopToBottom;
            //b.AutoResize();
            b.AutoClose = true;
            b.AutoCloseTimeOut = 5;
            //b.Owner=this;

            b.Show(false);
        }
        /// <summary>
        /// 询问框，是否
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShowYesNo(string text)
        {
            return MessageBox.Show(text, "询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
        }
        /// <summary>
        /// 错误消息框
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShowError(string text)
        {
            return MessageBox.Show(text, "询问", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
        /// <summary>
        /// 消息框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="msgBoxBtn"></param>
        /// <param name="msgBoxIcon"></param>
        /// <param name="defaultBtn"></param>
        /// <returns></returns>
        public DialogResult MessageBoxShow(string text, MessageBoxButtons msgBoxBtn, MessageBoxIcon msgBoxIcon, MessageBoxDefaultButton defaultBtn)
        {
            return MessageBox.Show(text, "提示", msgBoxBtn, msgBoxIcon, defaultBtn);
        }

        
    }
}