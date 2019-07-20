using Debugger.Winform.FormDesign;
using Debugger.Winform.IView;
using efwplusWinform.Business;
using ICSharpCode.TextEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.ViewForm
{
    public partial class frmXMLEditor : BaseFormBusiness, IfrmXMLEditor
    {
        private DesignSurfaceEx ds;
        private string filename;
        private TextArea textArea;
        private SelectedState _selectedState = SelectedState.设计器;
        public SelectedState selectedState
        {
            get
            {
                return _selectedState;
            }
            set
            {
                _selectedState = value;
                if (_selectedState == SelectedState.设计器)
                {
                    剪切UToolStripButton.Enabled = false;
                    复制CToolStripButton.Enabled = false;
                    粘贴PToolStripButton.Enabled = false;

                    撤销toolStripButton.Enabled = false;
                    重做toolStripButton.Enabled = false;

                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                    btnTop.Enabled = true;
                    btnBottom.Enabled = true;

                    btnFront.Enabled = true;
                    btnForward.Enabled = true;
                }
                else if (_selectedState == SelectedState.源代码)
                {
                    剪切UToolStripButton.Enabled = true;
                    复制CToolStripButton.Enabled = true;
                    粘贴PToolStripButton.Enabled = true;

                    撤销toolStripButton.Enabled = true;
                    重做toolStripButton.Enabled = true;

                    btnLeft.Enabled = false;
                    btnRight.Enabled = false;
                    btnTop.Enabled = false;
                    btnBottom.Enabled = false;

                    btnFront.Enabled = false;
                    btnForward.Enabled = false;
                }
            }
        }

        public frmXMLEditor()
        {
            InitializeComponent();
            textArea = textEditorControl.ActiveTextAreaControl.TextArea;
            selectedState = SelectedState.设计器;
        }
        private bool _EditState = false;
        public bool EditState
        {
            get { return _EditState; }
            set
            {
                _EditState = value;
                if (_EditState && Text.Length > 0)
                {
                    string c = Text.Substring(Text.Length - 1, 1);
                    if (c != "*")
                    {
                        Text = Text + "*";
                    }
                }
                else if (_EditState == false && Text.Length > 0)
                {
                    string c = Text.Substring(Text.Length - 1, 1);
                    if (c == "*")
                    {
                        Text = Text.Remove(Text.Length - 1, 1);
                    }
                }
                if (EditStateEvent != null)
                    EditStateEvent(Text);
            }
        }

        public Action<string> EditStateEvent
        {
            get;
            set;
        }

        public bool VisibleTool
        {
            set
            {
                toolStrip.Visible = value;
            }
        }

        public void LoadViewFile(string file)
        {
            filename = file;
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(file);
            if (fileinfo.Exists)
            {
                textEditorControl.LoadFile(file, true, true);
                this.Text = fileinfo.Name;

                string xml = textEditorControl.Text;
                LoadDesignerView(xml);
            }
        }
        public bool LoadText(string text)
        {
            return true;
        }
        public void CopyText()
        {
            复制CToolStripButton.PerformClick();
        }

        public void CutText()
        {
            剪切UToolStripButton.PerformClick();
        }

        public void DeleteText()
        {
            删除toolStripButton.PerformClick();
        }



        public void OpenFile()
        {
            打开OToolStripButton.PerformClick();
        }

        public void PasteText()
        {
            粘贴PToolStripButton.PerformClick();
        }

        public void RedoText()
        {
            重做toolStripButton.PerformClick();
        }

        public void SaveFile()
        {
            保存SToolStripButton.PerformClick();
        }

        public void UndoText()
        {
            撤销toolStripButton.PerformClick();
        }


        private void LoadDesignerView(string xml)
        {
            ds = new DesignSurfaceEx();
            ds.ActionSelectedComponents = (List<object> list) =>
            {
                propertyGrid.SelectedObjects = list.ToArray();
                if (list.Count > 0)
                    cbList.SelectedItem = list[0];
            };

            ds.ActionAddedRemovedComponent = (List<IComponent> list) =>
            {
                cbList.DataSource = list;
                cbList.DisplayMember = "Name";

                EditState = true;
            };


            ds.CreateDesign(panelDesign, toolbox, xml);
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = cbList.SelectedItem;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                string xml = textEditorControl.Text;
                LoadDesignerView(xml);

                selectedState = SelectedState.设计器;
            }
            else if (tabControl.SelectedIndex == 1)
            {
                textEditorControl.Text = ds.Loader.GetCode();
                selectedState = SelectedState.源代码;
            }
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                textEditorControl.LoadFile(filename);
                System.IO.FileInfo fileinfo = new System.IO.FileInfo(filename);

                this.Text = filename;

                LoadDesignerView(textEditorControl.Text);
            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            if (filename != null)
            {
                textEditorControl.Encoding = new UTF8Encoding(false);//必须加这句代码，因为python不能识别uft-8带BOM的
                textEditorControl.SaveFile(filename);
                EditState = false;
                MessageBoxShowSimple("保存成功！");
            }
        }

        private void 剪切UToolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedState == SelectedState.源代码)
                textArea.ClipboardHandler.Cut(sender, e);
            else
                ds.GlobalInvoke(StandardCommands.Cut);
        }

        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedState == SelectedState.源代码)
            {
                textArea.AutoClearSelection = false;
                textArea.ClipboardHandler.Copy(sender, e);
            }
            else
                ds.GlobalInvoke(StandardCommands.Copy);
        }

        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedState == SelectedState.源代码)
            {
                textArea.ClipboardHandler.Paste(sender, e);
            }
            else
                ds.GlobalInvoke(StandardCommands.Paste);
        }

        private void 删除toolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedState == SelectedState.源代码)
            {
                textArea.ClipboardHandler.Delete(sender, e);
            }
            else
            {
                ds.GlobalInvoke(StandardCommands.Delete);
            }
        }

        private void 撤销toolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedState == SelectedState.源代码)
            {
                textEditorControl.Undo();
            }
            else
            {
                ds.GlobalInvoke(StandardCommands.Undo);
            }
        }

        private void 重做toolStripButton_Click(object sender, EventArgs e)
        {
            if (selectedState == SelectedState.源代码)
            {
                textEditorControl.Redo();
            }
            else
            {
                ds.GlobalInvoke(StandardCommands.Redo);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            ds.GlobalInvoke(StandardCommands.AlignLeft);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            ds.GlobalInvoke(StandardCommands.AlignRight);
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            ds.GlobalInvoke(StandardCommands.AlignTop);
        }

        private void btnBottom_Click(object sender, EventArgs e)
        {
            ds.GlobalInvoke(StandardCommands.AlignBottom);
        }

        private void frmXMLEditor_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.S://Ctrl+S
                        保存SToolStripButton_Click(null, null);
                        break;
                    case Keys.O://Ctrl+O
                        打开OToolStripButton_Click(null, null);
                        break;
                    case Keys.T://Ctrl+T
                        剪切UToolStripButton_Click(null, null);
                        break;
                    case Keys.C://Ctrl+C
                        复制CToolStripButton_Click(null, null);
                        break;
                    case Keys.V://Ctrl+V
                        粘贴PToolStripButton_Click(null, null);
                        break;
                    case Keys.D://Ctrl+D
                        删除toolStripButton_Click(null, null);
                        break;
                    case Keys.U://Ctrl+U
                        撤销toolStripButton_Click(null, null);
                        break;
                    case Keys.R://Ctrl+R
                        重做toolStripButton_Click(null, null);
                        break;
                }
            }
        }

        private void textEditorControl_TextChanged(object sender, EventArgs e)
        {
            EditState = true;
        }

        private void btnFront_Click(object sender, EventArgs e)
        {
            ds.GlobalInvoke(StandardCommands.BringToFront);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            ds.GlobalInvoke(StandardCommands.BringForward);
        }
    }

    public enum SelectedState
    {
        设计器,源代码
    }
}
