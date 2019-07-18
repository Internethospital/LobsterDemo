﻿using System.Windows.Forms;
using System.Threading;
using System;
using System.Reflection;
using efwplusWinform;

namespace efwplusClient
{
    public class Program
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

            #region 升级代码
            WinformGlobal.UpdaterUrl = System.Configuration.ConfigurationSettings.AppSettings["UpdaterUrl"];
            var updater = FSLib.App.SimpleUpdater.Updater.Instance;
            //当检查发生错误时,这个事件会触发
            updater.Error += new EventHandler(updater_Error);
            //没有找到更新的事件
            updater.NoUpdatesFound += new EventHandler(updater_NoUpdatesFound);
            //找到更新的事件.但在此实例中,找到更新会自动进行处理,所以这里并不需要操作
            //updater.UpdatesFound += new EventHandler(updater_UpdatesFound);
            //开始检查更新-这是最简单的模式.请现在 assemblyInfo.cs 中配置更新地址,参见对应的文件.
            FSLib.App.SimpleUpdater.Updater.CheckUpdateSimple(System.Configuration.ConfigurationSettings.AppSettings["UpdaterUrl"]);
            #endregion

            string EntryController = System.Configuration.ConfigurationSettings.AppSettings["EntryController"];
            WinformGlobal.Main(EntryController);

            //Application.Run(new Form1());
        }

        static void setprivatepath()
        {
            string privatepath = @"Component;WinAssembly;CefSharp;";

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


        static void Application_ThreadExit(object sender, EventArgs e)
        {
            //EFWCoreLib.WinformFrame.WinformGlobal.Exit();
        }

        static void updater_NoUpdatesFound(object sender, EventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("没有找到更新");
        }

        static void updater_Error(object sender, EventArgs e)
        {
            var updater = sender as FSLib.App.SimpleUpdater.Updater;
            //System.Windows.Forms.MessageBox.Show("访问升级服务失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}