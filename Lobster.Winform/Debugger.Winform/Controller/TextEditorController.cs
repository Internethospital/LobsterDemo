using Debugger.Winform.IView;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.Controller
{
    [WinformController(DefaultViewName = "frmPythonEditor", Memo = "文件编辑器")]//在菜单上显示
    [WinformView(Name = "frmPythonEditor", ViewTypeName = "Debugger.Winform.ViewForm.frmPythonEditor")]//控制器关联的界面
    [WinformView(Name = "frmXMLEditor", ViewTypeName = "Debugger.Winform.ViewForm.frmXMLEditor")]//控制器关联的界面
    public class TextEditorController : WinformController
    {
        IfrmPythonEditor _ifrmPythonEditor;
        IfrmXMLEditor _ifrmXMLEditor;
        public override void Init()
        {
            _ifrmPythonEditor = (IfrmPythonEditor)DefaultView;
            _ifrmXMLEditor = (IfrmXMLEditor)iBaseView["frmXMLEditor"];
        }

        //显示代码文件内容
        [WinformMethod]
        public IBaseView ShowCodeFile(string file)
        {
            _ifrmPythonEditor.LoadScriptFile(file);
            return _ifrmPythonEditor;
        }

        [WinformMethod]
        public IBaseView ShowViewFile(string file)
        {
            _ifrmXMLEditor.LoadViewFile(file);
            return _ifrmXMLEditor;
        }

        [WinformMethod]
        public IBaseView ShowText(string text)
        {
            _ifrmPythonEditor.VisibleTool = false;
            _ifrmPythonEditor.LoadText(text);
            return _ifrmPythonEditor;
        }
    }
}
