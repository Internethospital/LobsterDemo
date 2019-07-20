using efwplusWinform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace efwplusStudio
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            setprivatepath();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //LocalizationKeys.LocalizeString += LocalizationKeys_LocalizeString;
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //下面这行代码必须注释掉，使用PrintPreviewDialog对象的ShowDialog()方法的时候会触发此事件而退出程序
            //Application.ThreadExit += new EventHandler(Application_ThreadExit);
            //EFWCoreLib.WinformFrame.WinformGlobal.UpdaterUrl = System.Configuration.ConfigurationSettings.AppSettings["UpdaterUrl"];
            string EntryController = System.Configuration.ConfigurationSettings.AppSettings["EntryController"];
            WinformGlobal.Main(EntryController);
        }

        static void setprivatepath()
        {
            string privatepath = @"Component;WinAssembly;CefSharp";

            AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", privatepath);
            AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", privatepath);
            var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty", BindingFlags.NonPublic | BindingFlags.Static);
            var funsion = typeof(AppDomain).GetMethod("GetFusionContext", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(null, new object[] { funsion.Invoke(AppDomain.CurrentDomain, null), "PRIVATE_BINPATH", privatepath });
        }

        //线程异常处理
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs t)
        {
            efwplusWinform.Common.Log.Error(sender, t.Exception);
            if (t.Exception.InnerException != null)
                MessageBox.Show(t.Exception.InnerException.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show(t.Exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
