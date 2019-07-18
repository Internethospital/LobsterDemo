﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.WinformFrame.Controller;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using DevComponents.DotNetBar;
using LoginWeb.Winform.IView;
using LoginWeb.Winform.Model;

namespace LoginWeb.Winform.ViewForm
{
    public partial class ReDept : BaseFormBusiness, IfrmReSetDept
    {


        public ReDept()
        {
            InitializeComponent();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            InvokeController("SaveReDept");
            this.Close();
        }

        private void ReDept_Load(object sender, EventArgs e)
        {

        }

        #region IfrmReSetDept 成员

        public string UserName
        {
            set { this.labelUserName.Text = value; }
        }

        public string WorkName
        {
            set { this.labelWorkName.Text = value; }
        }

        public void loadDepts(List<BaseDept> list,int selectDeptId)
        {
            this.comboBoxDepts.DataSource = list;
            this.comboBoxDepts.SelectedValue = selectDeptId;
        }

        public BaseDept getDept()
        {
            if ((BaseDept)this.comboBoxDepts.SelectedItem != null)
            {
                return (BaseDept)this.comboBoxDepts.SelectedItem;
            }
            else
            {
                MessageBoxEx.Show("请先配置科室");
                return new BaseDept();
            }
        }

        #endregion
    }
}
