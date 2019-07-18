using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using LoginWeb.Winform.IView;
using LoginWeb.Winform.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginWeb.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmPassWord", Memo = "Web浏览器")]//与系统菜单对应
    [WinformView(Name = "FrmPassWord", ViewTypeName = "LoginWeb.Winform.ViewForm.FrmPassWord")]
    [WinformView(Name = "FrmReportConfigure", ViewTypeName = "LoginWeb.Winform.ViewForm.FrmReportConfigure")]
    [WinformView(Name = "FrmSetting", ViewTypeName = "LoginWeb.Winform.ViewForm.FrmSetting")]
    [WinformView(Name = "ReDept", ViewTypeName = "LoginWeb.Winform.ViewForm.ReDept")]
    public class WebLoginOtherController: WcfClientController
    {
        IfrmPassWord _ifrmPassWord;
        IFrmReportConfigure _ifrmReportConfigure;
        IfrmReSetDept _ifrmReSetDept;
        IfrmSetting _ifrmSetting;
        public override void Init()
        {
            _ifrmPassWord = (IfrmPassWord)iBaseView["FrmPassWord"];
            _ifrmReportConfigure=(IFrmReportConfigure)iBaseView["FrmReportConfigure"];
            _ifrmReSetDept = (IfrmReSetDept)iBaseView["ReDept"];
            _ifrmSetting= (IfrmSetting)iBaseView["FrmSetting"];
        }

        #region 设置
        [WinformMethod]
        public void OpenSetting()
        {
            List<InputLanguage> list = new List<InputLanguage>();
            foreach (InputLanguage val in InputLanguage.InstalledInputLanguages)
            {
                list.Add(val);
            }
            _ifrmSetting.languageList = list;
            _ifrmSetting.inputMethod_CH = CustomConfigManager.GetInputMethod(EN_CH.CH);
            _ifrmSetting.inputMethod_EN = CustomConfigManager.GetInputMethod(EN_CH.EN);

            //打印机
            //ManagementObjectSearcher query;
            //ManagementObjectCollection queryCollection;
            //string _classname = "SELECT * FROM Win32_Printer";

            //query = new ManagementObjectSearcher(_classname);
            //queryCollection = query.Get();
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0)
            {
                List<string> Printers = new List<string>();
                foreach (var p in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    Printers.Add(p.ToString());
                }
                _ifrmSetting.loadPrinter(
                    Printers.ToArray(), 
                    CustomConfigManager.GetPrinter(0), 
                    CustomConfigManager.GetPrinter(1), 
                    CustomConfigManager.GetPrinter(2));
            }
            //消息
            _ifrmSetting.runacceptMessage = CustomConfigManager.GetrunacceptMessage() == 1 ? true : false;
            _ifrmSetting.displayWay = CustomConfigManager.GetDisplayWay() == 1 ? true : false;
            _ifrmSetting.setbackgroundImage = CustomConfigManager.GetBackgroundImage();
            _ifrmSetting.mainStyle = CustomConfigManager.GetMainStyle();
            ((Form)_ifrmSetting).ShowDialog();
        }
        [WinformMethod]
        public void SaveSetting()
        {
            ((Form)_ifrmSetting).Close();
            CustomConfigManager.SaveConfig(
                _ifrmSetting.inputMethod_EN,
                _ifrmSetting.inputMethod_CH,
                _ifrmSetting.printfirst,
                _ifrmSetting.printsecond,
                _ifrmSetting.printthree,
                _ifrmSetting.runacceptMessage ? 1 : 0,
                _ifrmSetting.displayWay ? 1 : 0,
                _ifrmSetting.setbackgroundImage,
                _ifrmSetting.mainStyle);
        }
        #endregion

        #region 切换科室
        [WinformMethod]
        public void OpenReDept(List<BaseDept> depts)
        {
            _ifrmReSetDept.UserName = base.LoginUserInfo.EmpName;
            _ifrmReSetDept.WorkName = base.LoginUserInfo.WorkName;
            _ifrmReSetDept.loadDepts(depts, LoginUserInfo.DeptId);
            ((Form)_ifrmReSetDept).ShowDialog();
        }
        [WinformMethod]
        public void SaveReDept()
        {
            BaseDept dept = ((IfrmReSetDept)iBaseView["ReDept"]).getDept();
            LoginUserInfo.DeptId = dept.DeptId;
            LoginUserInfo.DeptName = dept.Name;

            InvokeController("WebLoginController", "ChangedDeptName", dept.Name);

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(dept.DeptId);
                request.AddData(dept.Name);
            });
            InvokeWcfService("MainFrame.Service", "LoginController", "SaveReDept", requestAction);
        }
        #endregion

        #region 修改密码
        [WinformMethod]
        public void OpenPass()
        {
            _ifrmPassWord.clearPass();
            ((Form)_ifrmPassWord).ShowDialog();
        }

        [WinformMethod]
        public void AlterPass()
        {
            string oldpass = _ifrmPassWord.oldpass;
            string newpass = _ifrmPassWord.newpass;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.UserId);
                request.AddData(oldpass);
                request.AddData(newpass);
            });
            ServiceResponseData retdata = InvokeWcfService("MainFrame.Service", "LoginController", "AlterPass", requestAction);
            if (retdata.GetData<bool>(0) == false)
                throw new Exception("您输入的原始密码不正确！");
        }
        #endregion

        #region 报表设置打印机

        /// <summary>
        /// 展示报表弹窗
        /// </summary>
        [WinformMethod]
        public void ShowConfigure()
        {
            (_ifrmReportConfigure as Form).ShowDialog();
        }

        /// <summary>
        /// 读取报表数据
        /// </summary>
        [WinformMethod]
        public void GetReportData()
        {
            var retdata = InvokeWcfService(
            "MainFrame.Service",
            "ReportController",
            "GetReportBasicData");
            DataTable dt = retdata.GetData<DataTable>(0);
            _ifrmReportConfigure.LoadReportGrid(dt);
            _ifrmReportConfigure.BindPrintInfoCard();
        }
        #endregion
    }
}
