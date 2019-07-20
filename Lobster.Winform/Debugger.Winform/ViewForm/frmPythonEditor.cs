using Debugger.Winform.IView;
using efwplusWinform.Business;
using ICSharpCode.TextEditor;
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
    public partial class frmPythonEditor : BaseFormBusiness, IfrmPythonEditor
    {
        string filename;
        TextArea textArea;
        public bool VisibleTool
        {
            set
            {
                toolStrip.Visible = value;
            }
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

        public frmPythonEditor()
        {
            InitializeComponent();
            textArea = textEditorControl.ActiveTextAreaControl.TextArea;
        }

        public void LoadScriptFile(string file)
        {
            filename = file;
            System.IO.FileInfo fileinfo = new System.IO.FileInfo(file);
            if (fileinfo.Exists)
            {
                textEditorControl.LoadFile(file,true,true);
                if (fileinfo.Extension == ".py")
                {
                    textEditorControl.SetHighlighting("Boo");
                    textEditorControl.Refresh();
                }
                this.Text = fileinfo.Name;
            }
        }

        public bool LoadText(string text)
        {
            textEditorControl.Text = text;
            //textEditorControl.SetHighlighting("Xml");
            //textEditorControl.Refresh();
            return true;
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                textEditorControl.LoadFile(filename);
                System.IO.FileInfo fileinfo = new System.IO.FileInfo(filename);
                if (fileinfo.Extension == ".py")
                {
                    textEditorControl.SetHighlighting("Boo");
                    textEditorControl.Refresh();
                }

                this.Text = filename;
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
            textArea.ClipboardHandler.Cut(sender, e);
        }

        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            textArea.AutoClearSelection = false;
            textArea.ClipboardHandler.Copy(sender, e);
        }

        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            textArea.ClipboardHandler.Paste(sender, e);
        }

        public void OpenFile()
        {
            打开OToolStripButton_Click(null, null);
        }

        public void SaveFile()
        {
            保存SToolStripButton_Click(null, null);
        }

        public void CutText()
        {
            剪切UToolStripButton_Click(null, null);
        }

        public void CopyText()
        {
            复制CToolStripButton_Click(null, null);
        }

        public void PasteText()
        {
            粘贴PToolStripButton_Click(null, null);
        }
        public void DeleteText()
        {
            删除toolStripButton_Click(null, null);
        }

        public void UndoText()
        {
            撤销toolStripButton_Click(null, null);
        }

        public void RedoText()
        {
            重做toolStripButton_Click(null, null);
        }


        private void frmPythonEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //重做toolStripButton_Click(null, null);
        }

        private void frmPythonEditor_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Control==true )
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

        private void 删除toolStripButton_Click(object sender, EventArgs e)
        {
            textArea.ClipboardHandler.Delete(sender, e);
        }

        private void 撤销toolStripButton_Click(object sender, EventArgs e)
        {
            textEditorControl.Undo();
        }

        private void 重做toolStripButton_Click(object sender, EventArgs e)
        {
            textEditorControl.Redo();
        }

        
    }
}
