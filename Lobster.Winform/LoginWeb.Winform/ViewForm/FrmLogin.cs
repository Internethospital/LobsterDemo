using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.WinformFrame.Controller;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using LoginWeb.Winform.IView;

namespace LoginWeb.Winform.ViewForm
{
    public partial class FrmLogin : BaseFormBusiness, IfrmLogin
    {

        public FrmLogin()
        {
            InitializeComponent();

            
            this.frmForm1.AddItem(txtUser, null, "请输入用户名！");
            this.frmForm1.AddItem(txtPassWord, null, "请输入密码！");
        }

        public void btLogin_Click(object sender, EventArgs e)
        {
            M_SetButtonOkImage("button_chick.png");
            if (this.frmForm1.Validate())
            {
                try
                {
                    InvokeController("UserLogin");
                    isReLogin = true;//登录成功后，以后的登录都是重新登录
                    this.Close();
                }
                catch (Exception err)
                {
                    if (err.InnerException != null)
                        MessageBoxEx.Show(err.InnerException.Message, "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBoxEx.Show(err.Message, "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            M_SetButtonCancelImage("button_chick.png");
            this.Close();
        }


        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btLogin_Click(null, null);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.txtUser.Focus();
            //this.labsystemName.Text = InvokeController("GetSysName").ToString();
            this.pcb_background.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "images/login.png";
        }

        #region IfrmLogin 成员

        public string usercode
        {
            get
            {
                return this.txtUser.Text;
            }
            set
            {
                this.txtUser.Text = value;
            }
        }

        public string password
        {
            get
            {
                return this.txtPassWord.Text;
            }
            set
            {
                this.txtPassWord.Text = value;
            }
        }

        private bool _isReLogin = false;

        public bool isReLogin
        {
            get
            {
                return _isReLogin;
            }
            set
            {
                _isReLogin = value;
            }
        }

        #endregion


        #region 确定和退出按钮的图片变换
        private void M_SetButtonOkImage(string str_FileName)
        {
            string str_ImageFile = Application.StartupPath.ToString() + "\\images\\main\\" + str_FileName;
            if (System.IO.File.Exists(str_ImageFile))
            {
                this.pb_Ok.Image = Image.FromFile(str_ImageFile);
                lab_OK.Image = Image.FromFile(str_ImageFile);
            }
        }

        private void M_SetButtonCancelImage(string str_FileName)
        {
            string str_ImageFile = Application.StartupPath.ToString() + "\\images\\main\\" + str_FileName;
            if (System.IO.File.Exists(str_ImageFile))
            {
                this.pb_Cancel.Image = Image.FromFile(str_ImageFile);
                lab_Cancel.Image = Image.FromFile(str_ImageFile);
            }
        }
 
        private void pcb_OK_MouseHover(object sender, EventArgs e)
        {
            M_SetButtonOkImage("button_houver.png");
        }

        private void pcb_OK_MouseLeave(object sender, EventArgs e)
        {
            M_SetButtonOkImage("button.png");
        }
  
        private void pcb_Cancel_MouseHover(object sender, EventArgs e)
        {
            M_SetButtonCancelImage("button_houver.png");
        }

        private void pcb_Cancel_MouseLeave(object sender, EventArgs e)
        {
            M_SetButtonCancelImage("button.png");
        }
        #endregion

        private void pb_Ok_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btLogin_Click(null, null);
            }
        }

        private void btnconn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EFWCoreLib.WinformFrame.WinformGlobal.AppConfig();
        }
    }
}
