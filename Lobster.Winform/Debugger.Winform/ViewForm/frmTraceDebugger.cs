using Debugger.Winform.IView;
using efwplusWinform.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.ViewForm
{
    public partial class frmTraceDebugger : BaseFormBusiness, IfrmTraceDebugger
    {
        public frmTraceDebugger()
        {
            InitializeComponent();
        }

        public void ShowMsg(DateTime date, object text)
        {
            txtMsg.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] : " + text.ToString());
            txtMsg.AppendText("\r\n");
            //txtMsg.SelectionStart = txtMsg.TextLength - 1;
        }

        private void btnClearInfo_Click(object sender, EventArgs e)
        {
            txtMsg.Clear();
        }

        private void txtMsg_TextChanged(object sender, EventArgs e)
        {
            txtMsg.ScrollToCaret();
        }
    }
}
