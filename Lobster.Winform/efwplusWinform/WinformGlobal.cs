using efwplusWinform.Business;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace efwplusWinform
{
    /// <summary>
    /// Winform程序
    /// </summary>
    public class WinformGlobal
    {
        /// <summary>
        /// 入口软件名称
        /// </summary>
        private static string _entrysoftname = CloudSoftClientManager.defaultSoftName;
        /// <summary>
        /// 入口控制器，默认登陆控制器
        /// </summary>
        public static string _entrycontroller = "BasicLoginController";

        /// <summary>
        /// 入口界面
        /// </summary>
        private static string _entryview = null;

        /// <summary>
        /// 加载程序
        /// </summary>
        public static ILoading frmLoading;
        /// <summary>
        /// 登陆用户信息
        /// </summary>
        public static SysLoginRight LoginUserInfo;
        /// <summary>
        /// 升级地址
        /// </summary>
        public static string UpdaterUrl;
        /// <summary>
        /// 云软件集合
        /// </summary>
        public static List<CloudSoftClient> CSoftClientList
        {
            get { return CloudSoftClientManager.CSoftClientList; }
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="entryAddress">入口地址，如default#SoftBrowserController</param>
        public static void Main(string entryAddress)
        {
            if (!string.IsNullOrEmpty(entryAddress))
            {
                string[] args = entryAddress.Split('#');
                if (args.Length == 1)
                {
                    //_entrysoftname = _entrysoftname;
                    _entrycontroller = args[0];
                }
                else if (args.Length == 2)
                {
                    _entrysoftname = args[0];
                    _entrycontroller = args[1];
                }
                else if (args.Length == 3)
                {
                    _entrysoftname = args[0];
                    _entrycontroller = args[1];
                    _entryview = args[2];
                }
            }

            if (_entrycontroller == "BasicLoginController")
            {
                FrmSplash frmSplash = new FrmSplash(Init);
                frmLoading = frmSplash as ILoading;
                Application.Run(frmSplash);
            }
            else
            {
                WinformGlobal.LoadController();
                Form frmmain = (Form)WinformGlobal.RunView(entryAddress);
                Application.Run(frmmain);
            }
        }

        #region 启动登录程序
        
        
        private static bool Init()
        {
            try
            {
                CloudSoftClientManager.InitCloudSoft();
                IBaseView view = ControllerHelper.ShowView(_entrysoftname, _entrycontroller, _entrycontroller, _entryview);
                frmLoading.MainForm = ((System.Windows.Forms.Form)view);
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
        }
        /// <summary>
        /// 退出程序
        /// </summary>
        public static void Exit()
        {
            try
            {
                //WcfFrame.ClientLinkManage.UnAllConnection();//关闭所有连接

                if (frmLoading != null)
                {
                    (frmLoading as FrmSplash).Dispose();
                }
                //Application.Exit();
                //Application.ExitThread();
                //System.Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch { }
        }

        public static void AppConfig()
        {
            FrmConfig config = new FrmConfig();
            config.ShowDialog();
        }
        #endregion

        #region 打开指定控制器
        /// <summary>
        /// 第一步：加载控制器对象
        /// </summary>
        public static void LoadController()
        {
            CloudSoftClientManager.InitCloudSoft();
        }
        /// <summary>
        /// 运行控制器
        /// </summary>
        /// <param name="entrycontroller"></param>
        public static WinformController RunController(string entryAddress)
        {
            string[] args = entryAddress.Split('#');
            if (args.Length == 1)
            {
                //_entrysoftname = _entrysoftname;
                _entrycontroller = args[0];
            }
            else if (args.Length == 2)
            {
                _entrysoftname = args[0];
                _entrycontroller = args[1];
            }
            WinformController controller =ControllerHelper.CreateController(_entrysoftname,_entrycontroller,_entrycontroller);
            return controller;
        }

        /// <summary>
        /// 运行界面
        /// </summary>
        /// <param name="entrycontroller"></param>
        /// <returns></returns>
        public static IBaseView RunView(string entryAddress)
        {
            string[] args = entryAddress.Split('#');
            if (args.Length == 1)
            {
                //_entrysoftname = _entrysoftname;
                _entrycontroller = args[0];
            }
            else if (args.Length == 2)
            {
                _entrysoftname = args[0];
                _entrycontroller = args[1];
            }
            else if (args.Length == 3)
            {
                _entrysoftname = args[0];
                _entrycontroller = args[1];
                _entryview = args[2];
            }
            return ControllerHelper.ShowView(_entrysoftname, _entrycontroller, _entrycontroller, _entryview, false);
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="EmpId"></param>
        /// <param name="EmpName"></param>
        /// <param name="workId"></param>
        /// <param name="WorkName"></param>
        public static void SetUserInfo(int EmpId,string EmpName,int workId,string WorkName)
        {
            SysLoginRight login = new SysLoginRight();
            login.EmpId = EmpId;
            login.EmpName = EmpName;
            login.WorkId = workId;
            login.WorkName = WorkName;

            LoginUserInfo = login;
        }
        #endregion
    }
}
