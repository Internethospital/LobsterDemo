using Debugger.Winform.IView;
using efwplusWinform.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.ViewForm
{
    public partial class frmScriptNavigation : BaseFormBusiness, IfrmScriptNavigation
    {
        public string selectedPath
        {
            get
            {
                //if (this.treeScript.SelectedNode == null) return null;
                if (this.treeScript.SelectedNode.Tag is DirectoryInfo)
                {
                    return (this.treeScript.SelectedNode.Tag as DirectoryInfo).FullName;
                }
                else if (this.treeScript.SelectedNode.Tag is FileInfo)
                {
                    return (this.treeScript.SelectedNode.Tag as FileInfo).FullName;
                }

                return null;
            }
        }

        public string selectedText
        {
            get
            {
                if (this.treeScript.SelectedNode != null)
                {
                    return treeScript.SelectedNode.Text;
                }
                return null;
            }
        }

        public frmScriptNavigation()
        {
            InitializeComponent();
        }

        public void loadTree(string path)
        {
            treeScript.Nodes.Clear();

            string clientPath = path;
            if (!Directory.Exists(clientPath)) return;
            //add efwplusClient
            DirectoryInfo clientdir = new DirectoryInfo(clientPath);
            TreeNode clientroot = new TreeNode(clientdir.Name);
            clientroot.ImageIndex = 8;
            clientroot.SelectedImageIndex = 8;
            clientroot.Tag = clientdir;
            clientroot.Expand();
            treeScript.Nodes.Add(clientroot);
            // add efwplusClient/Config
            TreeNode configNode = new TreeNode("Config");
            configNode.ImageIndex = 4;
            configNode.SelectedImageIndex = 4;
            configNode.Tag = new DirectoryInfo(clientPath + "/Config");
            clientroot.Nodes.Add(configNode);
            // add efwplusClient/Config/CloudSoftConfig.xml
            TreeNode cscNode = new TreeNode("CloudSoftConfig.xml");
            cscNode.ImageIndex = 2;
            cscNode.SelectedImageIndex = 2;
            cscNode.Tag = new FileInfo(clientPath + "/Config/CloudSoftConfig.xml");
            configNode.Nodes.Add(cscNode);
            // add efwplusClient/WinAssembly
            TreeNode winANode = new TreeNode("WinAssembly");
            winANode.ImageIndex = 5;
            winANode.SelectedImageIndex = 5;
            winANode.Tag = new DirectoryInfo(clientPath + "/WinAssembly");
            clientroot.Nodes.Add(winANode);
            // add efwplusClient/WinAssembly/*.dll
            FileInfo[] dlls = new DirectoryInfo(clientPath + "/WinAssembly").GetFiles("*.dll");
            foreach (FileInfo f in dlls)
            {
                TreeNode dllNode = new TreeNode(f.Name);
                dllNode.ImageIndex = 3;
                dllNode.SelectedImageIndex = 3;
                dllNode.Tag = f;
                winANode.Nodes.Add(dllNode);
            }
            // add efwplusClient/WinScript
            TreeNode winSNode = new TreeNode("WinScript");
            winSNode.ImageIndex = 6;
            winSNode.SelectedImageIndex = 6;
            winSNode.Tag = new DirectoryInfo(clientPath + "/WinScript");
            clientroot.Nodes.Add(winSNode);
            // add efwplusClient/WinScript/*
            loadChildNode(winSNode, winSNode.Tag as DirectoryInfo);

            /*
            string serverPath = path + "efwplusServer";
            if (!Directory.Exists(serverPath)) return;
            // add efwplusServer
            DirectoryInfo serverdir = new DirectoryInfo(serverPath);
            TreeNode serverroot = new TreeNode(serverdir.Name);
            serverroot.ImageIndex = 0;
            serverroot.SelectedImageIndex = 0;
            serverroot.Tag = serverdir;
            serverroot.Expand();
            treeScript.Nodes.Add(serverroot);
            // add efwplusServer/Config
            TreeNode configSNode = new TreeNode("Config");
            configSNode.ImageIndex = 0;
            configSNode.SelectedImageIndex = 0;
            configSNode.Tag = new DirectoryInfo(serverPath + "/Config");
            serverroot.Nodes.Add(configSNode);
            // add efwplusServer/Config/pluginsys.xml
            TreeNode psysNode = new TreeNode("pluginsys.xml");
            psysNode.ImageIndex = 2;
            psysNode.SelectedImageIndex = 2;
            psysNode.Tag = new FileInfo(serverPath + "/Config/pluginsys.xml");
            configSNode.Nodes.Add(psysNode);
            // add efwplusServer/Config/EntLib.config
            TreeNode entlibNode = new TreeNode("EntLib.config");
            entlibNode.ImageIndex = 2;
            entlibNode.SelectedImageIndex = 2;
            entlibNode.Tag = new FileInfo(serverPath + "/Config/EntLib.config");
            configSNode.Nodes.Add(entlibNode);
            // add efwplusServer/ModulePlugin
            TreeNode mpNode = new TreeNode("ModulePlugin");
            mpNode.ImageIndex = 0;
            mpNode.SelectedImageIndex = 0;
            mpNode.Tag = new DirectoryInfo(serverPath + "/ModulePlugin");
            serverroot.Nodes.Add(mpNode);
            // add efwplusServer/ModulePlugin/*
            loadChildNode(mpNode, mpNode.Tag as DirectoryInfo);
            */
        }

        private void loadChildNode(TreeNode node, DirectoryInfo dir)
        {
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo d in dis)
            {
                TreeNode ctn = new TreeNode(d.Name);
                ctn.ImageIndex = 0;
                ctn.SelectedImageIndex = 0;
                ctn.Tag = d;
                node.Nodes.Add(ctn);
                loadChildNode(ctn, d);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo f in files)
            {
                if (f.Name == "__init__.py") continue;
                TreeNode ctn = new TreeNode(f.Name);
                if (f.Extension == ".py")
                {
                    ctn.ImageIndex = 1;
                    ctn.SelectedImageIndex = 1;
                }
                else if (f.Extension == ".xml")
                {
                    ctn.ImageIndex = 2;
                    ctn.SelectedImageIndex = 2;
                }
                else
                {
                    ctn.ImageIndex = 3;
                    ctn.SelectedImageIndex = 3;
                }
                ctn.Tag = f;
                node.Nodes.Add(ctn);
            }
        }
        //折叠
        private void btnCollapse_Click(object sender, EventArgs e)
        {
            treeScript.CollapseAll();
        }
        //展开
        private void btnExpand_Click(object sender, EventArgs e)
        {
            treeScript.ExpandAll();
        }
        //双击打开
        private void treeScript_DoubleClick(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.FileInfo)
            {
                InvokeController("OpenScript", (treeScript.SelectedNode.Tag as System.IO.FileInfo).FullName);
            }
        }
        //右键选定节点
        private void treeScript_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //确定右键的位置  
                Point clickPoint = new Point(e.X, e.Y);
                //在确定后的位置上面定义一个节点  
                TreeNode treeNode = treeScript.GetNodeAt(clickPoint);
                if (treeNode != null)
                {
                    treeScript.SelectedNode = treeNode;
                }
            }
        }

        private void 查看代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.FileInfo)
            {
                InvokeController("OpenScript", (treeScript.SelectedNode.Tag as System.IO.FileInfo).FullName);
            }
        }

        private void 查看设计器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.FileInfo)
            {
                InvokeController("OpenView", (treeScript.SelectedNode.Tag as System.IO.FileInfo).FullName);
            }
        }
        //刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InvokeController("LoadScript");
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.FileInfo)
            {
                if (MessageBox.Show("确定删除此文件?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    InvokeController("DeleteFile", (treeScript.SelectedNode.Tag as System.IO.FileInfo).FullName);
                    treeScript.SelectedNode.Remove();
                    //删除之后，如果文件已经打开则需要关闭打开的文档
                }
            }
            else if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.DirectoryInfo)
            {
                if (treeScript.SelectedNode.Parent != null && treeScript.SelectedNode.Parent.Text == "WinScript")
                {
                    if (MessageBox.Show("确定删除此软件项目?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        InvokeController("DeleteSoft", (treeScript.SelectedNode.Tag as System.IO.DirectoryInfo).FullName);
                        //删除之后，如果文件已经打开则需要关闭打开的文档
                        treeScript.SelectedNode.Remove();
                    }
                }
            }
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeScript.LabelEdit = true;
            treeScript.SelectedNode.BeginEdit();
        }
        private void treeScript_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Object newName = InvokeController("RenameFile", treeScript.SelectedNode.Tag, e.Label);
            treeScript.SelectedNode.Tag = newName;
            //重命名文件夹后，下面的子节点的Tag属性都要修改
        }

        private void 打开所在文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "WinScript";
                if (treeScript.SelectedNode.Tag is DirectoryInfo)
                {
                    path = (treeScript.SelectedNode.Tag as DirectoryInfo).FullName;
                }
                else if (treeScript.SelectedNode.Tag is FileInfo)
                {
                    path = (treeScript.SelectedNode.Tag as FileInfo).DirectoryName;
                }
                System.Diagnostics.Process.Start("Explorer.exe", path);
            }
        }

        private void 重新生成configxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null && treeScript.SelectedNode.Text == "config.xml")
            {
                DirectoryInfo projectDir = (treeScript.SelectedNode.Parent.Tag as DirectoryInfo);
                FileInfo file = (treeScript.SelectedNode.Tag as FileInfo);

                FileInfo[] filelist = file.Directory.GetFiles("*.*", SearchOption.AllDirectories);

                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(file.FullName);
                System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("cloudsoft/fileList");
                if (node != null)
                {
                    node.RemoveAll();
                    foreach (var f in filelist)
                    {
                        System.Xml.XmlElement el = xmlDoc.CreateElement("file");
                        el.SetAttribute("path", f.FullName.Remove(0, projectDir.FullName.Length + 1));
                        node.AppendChild(el);
                    }
                    xmlDoc.Save(file.FullName);
                    MessageBoxShowSimple("生成完成");
                }
            }
        }

        private void 新建云软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新加客户端软件
            if (treeScript.SelectedNode.Text == "WinScript")
            {
                InvokeController("DialogNewSoft");
            }
            //新加服务端软件
            else if (treeScript.SelectedNode.Text == "ModulePlugin")
            {

            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (treeScript.SelectedNode != null)
            {
                if (treeScript.SelectedNode == treeScript.TopNode)
                {
                    menuState = MenuState.efwplusClient;
                }
                else if (treeScript.SelectedNode.Parent == treeScript.TopNode && treeScript.SelectedNode.Text == "Config")
                {
                    menuState = MenuState.Config目录;
                }
                else if (treeScript.SelectedNode.Parent == treeScript.TopNode && treeScript.SelectedNode.Text == "WinAssembly")
                {
                    menuState = MenuState.WinAssembly目录;
                }
                else if (treeScript.SelectedNode.Parent == treeScript.TopNode && treeScript.SelectedNode.Text == "WinScript")
                {
                    menuState = MenuState.方案;
                }
                else if (treeScript.SelectedNode.Tag is DirectoryInfo)
                {
                    if (treeScript.SelectedNode.Parent != null && treeScript.SelectedNode.Parent.Text == "WinScript")
                    {
                        menuState = MenuState.项目;
                    }
                    else
                    {
                        menuState = MenuState.目录;
                    }
                }
                else if (treeScript.SelectedNode.Tag is FileInfo)
                {
                    if ((treeScript.SelectedNode.Tag as FileInfo).Extension == ".py")
                    {
                        menuState = MenuState.py文件;
                    }
                    else if ((treeScript.SelectedNode.Tag as FileInfo).Name == "config.xml")
                    {
                        menuState = MenuState.config;
                    }
                    else if ((treeScript.SelectedNode.Tag as FileInfo).Extension == ".xml")
                    {
                        menuState = MenuState.xml文件;
                    }
                    else
                    {
                        menuState = MenuState.其他文件;
                    }
                }
            }
        }

        /// <summary>
        /// 菜单状态
        /// </summary>
        private MenuState menuState
        {
            set
            {
                获取云软件从服务器ToolStripMenuItem.Enabled = false;
                发布云软件到服务器ToolStripMenuItem.Enabled = false;
                switch (value)
                {
                    case MenuState.efwplusClient:
                    case MenuState.Config目录:
                    case MenuState.WinAssembly目录:
                        查看代码ToolStripMenuItem.Enabled = false;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = false;
                        重命名ToolStripMenuItem.Enabled = false;
                        获取云软件从服务器ToolStripMenuItem.Enabled = false;
                        打开所在文件夹ToolStripMenuItem.Enabled = true;
                        重新生成configxmlToolStripMenuItem.Enabled = false;
                        break;
                    case MenuState.方案:
                        查看代码ToolStripMenuItem.Enabled = false;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = true;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = false;
                        重命名ToolStripMenuItem.Enabled = false;
                        //从云同步代码ToolStripMenuItem.Enabled = true;
                        打开所在文件夹ToolStripMenuItem.Enabled = true;
                        重新生成configxmlToolStripMenuItem.Enabled = false;
                        break;
                    case MenuState.项目:
                        查看代码ToolStripMenuItem.Enabled = false;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = true;
                        重命名ToolStripMenuItem.Enabled = true;
                        打开所在文件夹ToolStripMenuItem.Enabled = true;
                        重新生成configxmlToolStripMenuItem.Enabled = false;

                        获取云软件从服务器ToolStripMenuItem.Enabled = true;
                        发布云软件到服务器ToolStripMenuItem.Enabled = true;
                        break;
                    case MenuState.目录:
                        查看代码ToolStripMenuItem.Enabled = false;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = true;
                        删除ToolStripMenuItem.Enabled = true;
                        重命名ToolStripMenuItem.Enabled = true;
                        获取云软件从服务器ToolStripMenuItem.Enabled = false;
                        打开所在文件夹ToolStripMenuItem.Enabled = true;
                        重新生成configxmlToolStripMenuItem.Enabled = false;
                        break;
                    case MenuState.py文件:
                        查看代码ToolStripMenuItem.Enabled = true;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = true;
                        重命名ToolStripMenuItem.Enabled = true;
                        获取云软件从服务器ToolStripMenuItem.Enabled = false;
                        打开所在文件夹ToolStripMenuItem.Enabled = false;
                        重新生成configxmlToolStripMenuItem.Enabled = false;
                        break;
                    case MenuState.xml文件:
                        查看代码ToolStripMenuItem.Enabled = true;
                        查看设计器ToolStripMenuItem.Enabled = true;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = true;
                        重命名ToolStripMenuItem.Enabled = true;
                        获取云软件从服务器ToolStripMenuItem.Enabled = false;
                        打开所在文件夹ToolStripMenuItem.Enabled = false;
                        重新生成configxmlToolStripMenuItem.Enabled = false;
                        break;
                    case MenuState.config:
                        查看代码ToolStripMenuItem.Enabled = true;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = false;
                        重命名ToolStripMenuItem.Enabled = false;
                        获取云软件从服务器ToolStripMenuItem.Enabled = false;
                        打开所在文件夹ToolStripMenuItem.Enabled = false;
                        重新生成configxmlToolStripMenuItem.Enabled = true;
                        break;
                    case MenuState.其他文件:
                        查看代码ToolStripMenuItem.Enabled = true;
                        查看设计器ToolStripMenuItem.Enabled = false;
                        新建云软件ToolStripMenuItem.Enabled = false;
                        添加文件ToolStripMenuItem.Enabled = false;
                        删除ToolStripMenuItem.Enabled = true;
                        重命名ToolStripMenuItem.Enabled = true;
                        获取云软件从服务器ToolStripMenuItem.Enabled = false;
                        打开所在文件夹ToolStripMenuItem.Enabled = false;
                        重新生成configxmlToolStripMenuItem.Enabled = false;
                        break;
                }
            }
        }
        /// <summary>
        /// 配置CloudSoftConfig.xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSoftConfig_Click(object sender, EventArgs e)
        {
            string file = efwplusWinform.CloudSoftClientManager.csoftconfigFile;
            InvokeController("OpenScript", file);
        }
        //添加Controller
        private void controllerpyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeScript.SelectedNode.Expand();
            string path = (treeScript.SelectedNode.Tag as DirectoryInfo).FullName;
            string filename = Convert.ToString(InvokeController("DialogNewFile", 0, path));
            if (!string.IsNullOrEmpty(filename))
            {
                TreeNode ctn = new TreeNode(filename + ".py");
                ctn.ImageIndex = 1;
                ctn.SelectedImageIndex = 1;
                ctn.Tag = new FileInfo(path + "\\" + filename + ".py");
                treeScript.SelectedNode.Nodes.Add(ctn);
            }
        }
        //添加Model
        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeScript.SelectedNode.Expand();
            string path = (treeScript.SelectedNode.Tag as DirectoryInfo).FullName;
            string filename = Convert.ToString(InvokeController("DialogNewFile", 1, path));
            if (!string.IsNullOrEmpty(filename))
            {
                TreeNode ctn = new TreeNode(filename + ".py");
                ctn.ImageIndex = 1;
                ctn.SelectedImageIndex = 1;
                ctn.Tag = new FileInfo(path + "\\" + filename + ".py");
                treeScript.SelectedNode.Nodes.Add(ctn);
            }
        }
        //添加View
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeScript.SelectedNode.Expand();
            string path = (treeScript.SelectedNode.Tag as DirectoryInfo).FullName;
            string filename = Convert.ToString(InvokeController("DialogNewFile", 2, path));
            if (!string.IsNullOrEmpty(filename))
            {
                TreeNode ctn = new TreeNode(filename + ".py");
                ctn.ImageIndex = 1;
                ctn.SelectedImageIndex = 1;
                ctn.Tag = new FileInfo(path + "\\" + filename + ".py");
                treeScript.SelectedNode.Nodes.Add(ctn);

                ctn = new TreeNode(filename + ".xml");
                ctn.ImageIndex = 2;
                ctn.SelectedImageIndex = 2;
                ctn.Tag = new FileInfo(path + "\\" + filename + ".xml");
                treeScript.SelectedNode.Nodes.Add(ctn);
            }
        }

        private void 发布云软件到服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.DirectoryInfo)
                {
                    InvokeController("PublishSoft", (treeScript.SelectedNode.Tag as System.IO.DirectoryInfo).FullName);

                    MessageBox.Show("发布成功！");
                }
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    MessageBox.Show(err.InnerException.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void 获取云软件从服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeScript.SelectedNode != null && treeScript.SelectedNode.Tag is System.IO.DirectoryInfo)
                {
                    InvokeController("UploadSoft", (treeScript.SelectedNode.Tag as System.IO.DirectoryInfo).FullName);
                    MessageBox.Show("获取成功！");
                }
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    MessageBox.Show(err.InnerException.Message,"错误提示",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show(err.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void toolServer_Click(object sender, EventArgs e)
        {
            InvokeController("SearchPublishSoft");
        }
    }

    public enum MenuState
    {
        efwplusClient,Config目录,WinAssembly目录,方案,项目,目录,py文件,xml文件,config,其他文件
    }
}
