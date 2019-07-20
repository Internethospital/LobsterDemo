using Debugger.Winform.IView;
using EFWCoreLib.CoreFrame.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame;

namespace Debugger.Winform.ViewForm
{
    public partial class frmServiceDebugger : BaseFormBusiness, IfrmServiceDebugger
    {
        public frmServiceDebugger()
        {
            InitializeComponent();
        }

        private List<dwPlugin> PList;
        public void LoadPlugin(List<dwPlugin> plist)
        {
            PList = plist;

            cbplugin.ValueMember = "pluginname";
            cbplugin.DisplayMember = "pluginname";
            cbplugin.DataSource = plist;

            LoadTree(plist);
        }

        private void LoadTree(List<dwPlugin> plist)
        {
            treePlugin.Nodes.Clear();
            
            foreach (var p in plist)
            {
                TreeNode root = new TreeNode(p.pluginname);
                root.ImageIndex = 0;
                root.SelectedImageIndex = 0;
                foreach (var c in p.controllerlist)
                {
                    TreeNode ctn = new TreeNode(c.controllername);
                    ctn.ImageIndex = 1;
                    ctn.SelectedImageIndex = 1;
                    foreach (var m in c.methodlist)
                    {
                        TreeNode mtn = new TreeNode(m);
                        mtn.ImageIndex = 2;
                        mtn.SelectedImageIndex = 2;
                        mtn.Tag = p.pluginname + "#" + c.controllername + "#" + m;
                        ctn.Nodes.Add(mtn);
                    }
                    root.Nodes.Add(ctn);
                }
                treePlugin.Nodes.Add(root);
            }
        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            InvokeController("GetAllService");
        }

        private void frmServiceDebugger_Load(object sender, EventArgs e)
        {
            InvokeController("GetAllService");
        }

        private void cbplugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pname = cbplugin.Text;
            dwPlugin pl = PList.Find(x => x.pluginname == pname);

            cbcontroller.ValueMember = "controllername";
            cbcontroller.DisplayMember = "controllername";
            cbcontroller.DataSource = pl.controllerlist;
        }

        private void cbcontroller_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pname = cbplugin.Text;
            dwPlugin pl = PList.Find(x => x.pluginname == pname);
            string cname = cbcontroller.Text;
            dwController co = pl.controllerlist.Find(x => x.controllername == cname);
            cbmothed.DataSource = co.methodlist;

        }
        private void treePlugin_DoubleClick(object sender, EventArgs e)
        {
            if (treePlugin.SelectedNode != null && treePlugin.SelectedNode.Tag != null)
            {
                string pname = treePlugin.SelectedNode.Tag.ToString().Split(new char[] { '#' })[0];
                string cname = treePlugin.SelectedNode.Tag.ToString().Split(new char[] { '#' })[1];
                string mname = treePlugin.SelectedNode.Tag.ToString().Split(new char[] { '#' })[2];

                cbplugin.Text = pname;
                cbcontroller.Text = cname;
                cbmothed.Text = mname;
                txtparams.Text = "";
                txtResult.Text = "";
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                btnRequest.Enabled = false;
                ServiceResponseData retjson = ClientLinkManage.CreateConnection(cbplugin.Text).Request(string.Format("{0}", cbcontroller.Text), cbmothed.Text,
                    (ClientRequestData request) =>
                    {
                        request.SetJsonData(txtparams.Text.Trim());
                    });
                txtResult.Text = retjson.GetJsonData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRequest.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Exit");
        }
    }
}
