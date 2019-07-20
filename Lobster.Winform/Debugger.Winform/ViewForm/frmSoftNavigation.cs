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
    public partial class frmSoftNavigation : BaseFormBusiness, IfrmSoftNavigation
    {
        public frmSoftNavigation()
        {
            InitializeComponent();
        }

        public void LoadControllerAddress(List<CloudSoftClient> calist)
        {
            treePlugin.Nodes.Clear();

            foreach (var s in calist)
            {
                TreeNode root = new TreeNode(s.SoftName + "[" + s.SoftTitle + "]");
                root.ImageIndex = 1;
                root.SelectedImageIndex = 1;
                root.Name = s.SoftName;

                foreach (var p in s.controllerInfoList)
                {
                    TreeNode cnode = new TreeNode(p.controllerName + (p.Memo == null ? "" : "[" + p.Memo + "]"));
                    cnode.ImageIndex = 1;
                    cnode.SelectedImageIndex = 1;
                    cnode.Name = p.controllerName;

                    foreach (var c in p.ViewList)
                    {
                        TreeNode ctn = new TreeNode(c.Name);
                        ctn.ImageIndex = 2;
                        ctn.SelectedImageIndex = 2;
                        ctn.Tag = s.SoftName + "#" + p.controllerName + "#" + c.Name;
                        cnode.Nodes.Add(ctn);
                    }
                    root.Nodes.Add(cnode);
                }

                treePlugin.Nodes.Add(root);
            }
        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            InvokeController("GetRefreshController");
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            InvokeController("ClearAllCache");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Exit");
        }

        private void treePlugin_DoubleClick(object sender, EventArgs e)
        {
            if (treePlugin.SelectedNode == null || treePlugin.SelectedNode.Tag == null) return;

            InvokeController("OpenNode", treePlugin.SelectedNode.Tag.ToString());
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treePlugin.SelectedNode == null || treePlugin.SelectedNode.Tag == null) return;

            InvokeController("OpenNode", treePlugin.SelectedNode.Tag.ToString());
        }

        private void 清除此控制器缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treePlugin.SelectedNode == null || treePlugin.SelectedNode.Tag == null) return;
            InvokeController("ClearCacheByName", treePlugin.SelectedNode.Parent.Parent.Name, treePlugin.SelectedNode.Parent.Name);
        }

        private void treePlugin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //确定右键的位置  
                Point clickPoint = new Point(e.X, e.Y);
                //在确定后的位置上面定义一个节点  
                TreeNode treeNode = treePlugin.GetNodeAt(clickPoint);
                if (treeNode != null)
                {
                    treePlugin.SelectedNode = treeNode;
                }
            }
        }
    }
}
