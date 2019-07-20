using Debugger.Winform.IView;
using efwplusWinform;
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
    public partial class frmClientDebugger : BaseFormBusiness, IfrmClientDebugger
    {
        public frmClientDebugger()
        {
            InitializeComponent();
        }

        public void LoadControllerAttr(List<CloudSoftClient> clientlist)
        {
            treePlugin.Nodes.Clear();
            foreach (var s in clientlist)
            {
                TreeNode root = new TreeNode(s.SoftName + "[" + s.SoftTitle + "]");
                root.ImageIndex = 0;
                root.SelectedImageIndex = 0;
                root.Name = s.SoftName;

                foreach (var p in s.controllerInfoList)
                {
                    TreeNode cnode = new TreeNode(p.controllerName + (p.Memo == null ? "" : "[" + p.Memo + "]"));
                    cnode.ImageIndex = 0;
                    cnode.SelectedImageIndex = 0;
                    cnode.Name = p.controllerName;

                    TreeNode view = new TreeNode("界面");
                    view.ImageIndex = 1;
                    view.SelectedImageIndex = 1;
                    cnode.Nodes.Add(view);
                    TreeNode method = new TreeNode("方法");
                    method.ImageIndex = 1;
                    method.SelectedImageIndex = 1;
                    cnode.Nodes.Add(method);

                    foreach (var c in p.ViewList)
                    {
                        TreeNode ctn = new TreeNode(c.Name);
                        ctn.ImageIndex = 2;
                        ctn.SelectedImageIndex = 2;
                        ctn.Tag = "0#" + s.SoftName + "#" + p.controllerName + "#" + c.Name;
                        view.Nodes.Add(ctn);
                    }
                    foreach (var m in p.MethodList)
                    {
                        TreeNode ctn = new TreeNode(m.methodName);
                        ctn.ImageIndex = 2;
                        ctn.SelectedImageIndex = 2;
                        ctn.Tag = "1#" + s.SoftName + "#" + p.controllerName + "#" + m.methodName;
                        method.Nodes.Add(ctn);
                    }
                    root.Nodes.Add(cnode);
                }

                treePlugin.Nodes.Add(root);
            }
        }

        public void SetText(string softname,string controllerid, string controllername, string methodname, string parameters_json)
        {
            txtSoftName.Text = softname;
            txtcontrollerid.Text = controllerid;
            txtcontrollername.Text = controllername;
            txtmethodname.Text = methodname;
            txtparams.Text = parameters_json;
        }

        private void frmClientDebugger_Load(object sender, EventArgs e)
        {
            InvokeController("GetAllCAttrList");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Exit");
        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            InvokeController("GetAllCAttrList");
        }

        private void treePlugin_DoubleClick(object sender, EventArgs e)
        {
            if (treePlugin.SelectedNode == null || treePlugin.SelectedNode.Tag == null) return;

            InvokeController("OpenNode", treePlugin.SelectedNode.Tag.ToString());
        }
        //执行控制器方法
        private void btnRequest_Click(object sender, EventArgs e)
        {
            InvokeController("RequestController",txtSoftName.Text, txtcontrollerid.Text, txtcontrollername.Text, txtmethodname.Text, txtparams.Text);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            InvokeController("ClearAllCache");
        }
    }
}
