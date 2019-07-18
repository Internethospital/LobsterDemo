using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.WcfFrame;

namespace LoginWeb.Winform.ViewForm
{
    internal partial class MessageTimer : System.Timers.Timer
    {
        private Form _frmMain;

        public Form FrmMain
        {
            get { return _frmMain; }
            set
            {
                _frmMain = value;
                Messages.InvokeController = (_frmMain as IBaseViewBusiness).InvokeController;
            }
        }

        public MessageTimer()
        {
            InitializeComponent();
            //manager = new MessageManager();
            this.Interval = ClientLinkManage.MessageTime * 2000;
            this.Enabled = true;
            this.Elapsed += new System.Timers.ElapsedEventHandler(MessageTimer_Tick);
        }

        void MessageTimer_Tick(object sender, EventArgs e)
        {
            if (ClientLinkManage.IsMessage == true)
            {
                this.Enabled = false;
                try
                {
                    GetMessages();
                }
                catch { }
                finally
                {
                    this.Enabled = true;
                }
            }
        }

        private bool GetMessages()
        {
            System.Windows.Forms.Application.DoEvents();
            List<Messages> showMs = Messages.GetMessages();

            if (showMs.Count > 0)
            {
                TaskbarForm.ShowForm(FrmMain, showMs);
                return true;
            }

            return false;
        }
    }
}
