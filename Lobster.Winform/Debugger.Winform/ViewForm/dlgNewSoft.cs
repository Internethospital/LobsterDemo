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
    public partial class dlgNewSoft : BaseFormBusiness, IdlgNewSoft
    {
        public int SoftType
        {
            get;
            set;
        }

        public dlgNewSoft()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBoxShowSimple("云软件名称不能为空");
                txtName.Focus();
                return;
            }
            if (txtTitle.Text.Trim() == "")
            {
                MessageBoxShowSimple("云软件标题不能为空");
                txtTitle.Focus();
                return;
            }
            if (txtVersion.Text.Trim() == "")
            {
                MessageBoxShowSimple("云软件版本不能为空");
                txtVersion.Focus();
                return;
            }
            if (txtAuthor.Text.Trim() == "")
            {
                MessageBoxShowSimple("云软件作者不能为空");
                txtAuthor.Focus();
                return;
            }
            if (SoftType == 0)
            {
                bool succeed = (bool)InvokeController("NewSoftClient", txtName.Text, txtTitle.Text, txtVersion.Text, txtAuthor.Text);
                if (succeed)
                {
                    this.Close();
                }
            }
            else if (SoftType == 1)
            {
                bool succeed = (bool)InvokeController("NewSoftServer", txtName.Text, txtTitle.Text, txtVersion.Text, txtAuthor.Text);
                if (succeed)
                {
                    this.Close();
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dlgNewSoft_Shown(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtTitle.Text = "";
            txtName.Focus();
        }
    }
}
