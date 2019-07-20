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
    public partial class dlgNewFile : BaseFormBusiness, IdlgNewFile
    {
        public dlgNewFile()
        {
            InitializeComponent();
        }

        public int fileType
        {
            get;
            set;
        }
        public string path { get; set; }
        public string filename { get; set; }
        private bool _isOk = false;
        public bool isOk
        {
            get
            {
                return _isOk;
            }
            set
            {
                _isOk = value;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBoxShowSimple("文件名称不能为空");
                txtName.Focus();
                return;
            }
            filename = "";
            bool succeed = (bool)InvokeController("NewFile", txtName.Text);
            if (succeed)
            {
                filename = txtName.Text;
                _isOk = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dlgNewFile_Shown(object sender, EventArgs e)
        {
            filename = "";
            txtName.Text = "";
            txtName.Focus();
        }
    }
}
