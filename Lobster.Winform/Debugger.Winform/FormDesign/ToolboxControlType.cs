using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.FormDesign
{
    public class ControlTypeData
    {
        public static List<ControlTypeObj> CTObjList
        {
            get
            {
                List<ControlTypeObj> _list = new List<ControlTypeObj>();
                _list.Add(new ControlTypeObj("windowsForm", "panel", "panel", typeof(System.Windows.Forms.Panel)));
                _list.Add(new ControlTypeObj("windowsForm", "panel", "groupBox", typeof(System.Windows.Forms.GroupBox)));
                _list.Add(new ControlTypeObj("windowsForm", "label", "label", typeof(System.Windows.Forms.Label)));
                _list.Add(new ControlTypeObj("windowsForm", "label", "linkLabel", typeof(System.Windows.Forms.LinkLabel)));
                _list.Add(new ControlTypeObj("windowsForm", "button", "button", typeof(System.Windows.Forms.Button)));
                _list.Add(new ControlTypeObj("windowsForm", "input", "textbox", typeof(System.Windows.Forms.TextBox)));
                _list.Add(new ControlTypeObj("windowsForm", "input", "checkbox", typeof(System.Windows.Forms.CheckBox)));
                _list.Add(new ControlTypeObj("windowsForm", "input", "radiobutton", typeof(System.Windows.Forms.RadioButton)));
                _list.Add(new ControlTypeObj("windowsForm", "input", "combobox", typeof(System.Windows.Forms.ComboBox)));
                _list.Add(new ControlTypeObj("windowsForm", "input", "richTextBox", typeof(System.Windows.Forms.RichTextBox)));
                _list.Add(new ControlTypeObj("windowsForm", "input", "dateTimePicker", typeof(System.Windows.Forms.DateTimePicker)));
                _list.Add(new ControlTypeObj("windowsForm", "datagrid", "datagridview", typeof(System.Windows.Forms.DataGridView)));
                _list.Add(new ControlTypeObj("windowsForm", "picturebox", "", typeof(System.Windows.Forms.PictureBox)));
                _list.Add(new ControlTypeObj("windowsForm", "tabcontrol", "tabcontrol", typeof(System.Windows.Forms.TabControl)));
                
                _list.Add(new ControlTypeObj("windowsForm", "tree", "treeview", typeof(System.Windows.Forms.TreeView)));
                _list.Add(new ControlTypeObj("windowsForm", "tool", "toolstrip", typeof(System.Windows.Forms.ToolStrip)));
                _list.Add(new ControlTypeObj("windowsForm", "contextmenu", "", typeof(System.Windows.Forms.ContextMenuStrip)));

                _list.Add(new ControlTypeObj("DotNetBar", "panel", "panelEx", typeof(DevComponents.DotNetBar.PanelEx)));
                _list.Add(new ControlTypeObj("DotNetBar", "panel", "groupPanel", typeof(DevComponents.DotNetBar.Controls.GroupPanel)));
                _list.Add(new ControlTypeObj("DotNetBar", "panel", "expandablePanel", typeof(DevComponents.DotNetBar.ExpandablePanel)));
                _list.Add(new ControlTypeObj("DotNetBar", "panel", "expandableSplitter", typeof(DevComponents.DotNetBar.ExpandableSplitter)));
                _list.Add(new ControlTypeObj("DotNetBar", "label", "labelX", typeof(DevComponents.DotNetBar.LabelX)));
                _list.Add(new ControlTypeObj("DotNetBar", "button", "buttonX", typeof(DevComponents.DotNetBar.ButtonX)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "textBoxX", typeof(DevComponents.DotNetBar.Controls.TextBoxX)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "checkBoxX", typeof(DevComponents.DotNetBar.Controls.CheckBoxX)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "integerInput", typeof(DevComponents.Editors.IntegerInput)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "comboBoxEx", typeof(DevComponents.DotNetBar.Controls.ComboBoxEx)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "richTextBoxEx", typeof(DevComponents.DotNetBar.Controls.RichTextBoxEx)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "dateTimeInput", typeof(DevComponents.Editors.DateTimeAdv.DateTimeInput)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "comboTree", typeof(DevComponents.DotNetBar.Controls.ComboTree)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "switchButton", typeof(DevComponents.DotNetBar.Controls.SwitchButton)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "textBoxCard", typeof(EfwControls.CustomControl.TextBoxCard)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "multiSelectText", typeof(EfwControls.CustomControl.MultiSelectText)));
                _list.Add(new ControlTypeObj("DotNetBar", "input", "statDateTime", typeof(EfwControls.CustomControl.StatDateTime)));
                _list.Add(new ControlTypeObj("DotNetBar", "datagrid", "datagridviewX", typeof(DevComponents.DotNetBar.Controls.DataGridViewX)));
                _list.Add(new ControlTypeObj("DotNetBar", "datagrid", "efwdatagrid", typeof(EfwControls.CustomControl.DataGrid)));
                _list.Add(new ControlTypeObj("DotNetBar", "datagrid", "efwgridboxcard", typeof(EfwControls.CustomControl.GridBoxCard)));
                _list.Add(new ControlTypeObj("DotNetBar", "tabcontrol", "tabcontrolX", typeof(DevComponents.DotNetBar.TabControl)));
                _list.Add(new ControlTypeObj("DotNetBar", "tabcontrol", "superTabControl", typeof(DevComponents.DotNetBar.SuperTabControl)));
                
                _list.Add(new ControlTypeObj("DotNetBar", "tree", "advTree", typeof(DevComponents.AdvTree.AdvTree)));
                _list.Add(new ControlTypeObj("DotNetBar", "tool", "toolbar", typeof(DevComponents.DotNetBar.Bar)));

                _list.Add(new ControlTypeObj("windowsForm", "tabitem", "tabpage", typeof(System.Windows.Forms.TabPage), false));            
                _list.Add(new ControlTypeObj("DotNetBar", "tabitem", "tabItem", typeof(DevComponents.DotNetBar.TabItem), false));
                _list.Add(new ControlTypeObj("DotNetBar", "tabitem", "superTabItem", typeof(DevComponents.DotNetBar.SuperTabItem), false));

                _list.Add(new ControlTypeObj("windowsForm", "column", "datagridtextbox", typeof(System.Windows.Forms.DataGridViewTextBoxColumn), false));
                _list.Add(new ControlTypeObj("windowsForm", "column", "datagridbutton", typeof(System.Windows.Forms.DataGridViewButtonColumn), false));
                _list.Add(new ControlTypeObj("windowsForm", "column", "datagridcheckbox", typeof(System.Windows.Forms.DataGridViewCheckBoxColumn), false));
                _list.Add(new ControlTypeObj("windowsForm", "column", "datagridcombobox", typeof(System.Windows.Forms.DataGridViewComboBoxColumn), false));
                _list.Add(new ControlTypeObj("windowsForm", "column", "datagridimage", typeof(System.Windows.Forms.DataGridViewImageColumn), false));
                _list.Add(new ControlTypeObj("DotNetBar", "column", "datagriddatetimeX", typeof(DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn), false));
                _list.Add(new ControlTypeObj("DotNetBar", "column", "datagridlabelX", typeof(DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn), false));
                _list.Add(new ControlTypeObj("DotNetBar", "column", "datagridcomboboxEx", typeof(DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn), false));
                _list.Add(new ControlTypeObj("DotNetBar", "column", "datagridbuttonX", typeof(DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn), false));

                _list.Add(new ControlTypeObj("windowsForm", "node", "treenode", typeof(System.Windows.Forms.TreeNode), false));
                _list.Add(new ControlTypeObj("DotNetBar", "node", "advnode", typeof(DevComponents.AdvTree.Node), false));

                _list.Add(new ControlTypeObj("windowsForm", "toolitem", "toolStripButton", typeof(System.Windows.Forms.ToolStripButton), false));
                _list.Add(new ControlTypeObj("windowsForm", "toolitem", "toolStripLabel", typeof(System.Windows.Forms.ToolStripLabel), false));
                _list.Add(new ControlTypeObj("windowsForm", "toolitem", "toolStripComboBox", typeof(System.Windows.Forms.ToolStripComboBox), false));
                _list.Add(new ControlTypeObj("windowsForm", "toolitem", "toolStripTextBox", typeof(System.Windows.Forms.ToolStripTextBox), false));
                _list.Add(new ControlTypeObj("DotNetBar", "toolitem", "buttonItem", typeof(DevComponents.DotNetBar.ButtonItem), false));
                _list.Add(new ControlTypeObj("DotNetBar", "toolitem", "textBoxItem", typeof(DevComponents.DotNetBar.TextBoxItem), false));
                _list.Add(new ControlTypeObj("DotNetBar", "toolitem", "comboBoxItem", typeof(DevComponents.DotNetBar.ComboBoxItem), false));
                _list.Add(new ControlTypeObj("DotNetBar", "toolitem", "labelItem", typeof(DevComponents.DotNetBar.LabelItem), false));

                _list.Add(new ControlTypeObj("windowsForm", "menuitem", "", typeof(System.Windows.Forms.ToolStripMenuItem), false));

                return _list;
            }
        }

        public static Type[] windowsFormsToolTypes
        {
            get
            {
                return CTObjList.FindAll(x => x.CategoryName == "windowsForm" && x.IsToolBoxShow==true).Select(s => s.ControlType).ToArray();
            }
        }

        public static Type[] dotNetBarToolTypes
        {
            get
            {
                return CTObjList.FindAll(x => x.CategoryName == "DotNetBar" && x.IsToolBoxShow==true).Select(s => s.ControlType).ToArray();
            }
        }

        public static string GetAttributeName(string proName)
        {
            string attrname = proName;
            switch (proName)
            {
                case "Name":
                    attrname = "name";
                    break;
                case "Text":
                    attrname = "text";
                    break;
                case "TextAlign":
                    attrname = "textalign";
                    break;
                case "WatermarkText":
                    attrname = "marktext";
                    break;
                case "Value":
                    attrname = "value";
                    break;
                case "CustomFormat":
                    attrname = "customformat";
                    break;
                case "Enabled":
                    attrname = "enabled";
                    break;
                case "Location":
                    attrname = "location";
                    break;
                case "Size":
                    attrname = "size";
                    break;
                case "Dock":
                    attrname = "dock";
                    break;
                case "Anchor":
                    attrname = "anchor";
                    break;
                case "Visible":
                    attrname = "visible";
                    break;
                case "Font":
                    attrname = "font";
                    break;
                case "ForeColor":
                    attrname = "fontcolor";
                    break;
                case "ReadOnly":
                    attrname = "_readonly";
                    break;
                case "TabIndex":
                    attrname = "tabindex";
                    break;
                case "Multiline":
                    attrname = "multiline";
                    break;
                case "SizeMode":
                    attrname = "sizemode";
                    break;
                case "HeaderText":
                    attrname = "headertext";
                    break;
                case "DataPropertyName":
                    attrname = "datapropertyname";
                    break;
                case "MinimumWidth":
                    attrname = "miniwidth";
                    break;
                case "Width":
                    attrname = "width";
                    break;
                case "AutoSizeMode":
                    attrname = "autosizemode";
                    break;
                case "DisplayStyle":
                    attrname = "displaystyle";
                    break;
                case "StartPosition":
                    attrname = "startposition";
                    break;
                case "WindowState":
                    attrname = "windowstate";
                    break;
                case "MaximizeBox":
                    attrname = "maximizebox";
                    break;
                case "MinimizeBox":
                    attrname = "minimizebox";
                    break;
            }
            return attrname;
        }

        public static string GetPropertyName(string attrName)
        {
            switch (attrName)
            {
                case "name":
                    return "Name";
                case "text":
                    return "Text";
                case "textalign":
                    return "TextAlign";
                case "marktext":
                    return "WatermarkText";
                case "value":
                    return "Value";
                case "customformat":
                    return "CustomFormat";
                case "enabled":
                    return "Enabled";
                case "location":
                    return "Location";
                case "size":
                    return "Size";
                case "dock":
                    return "Dock";
                case "anchor":
                    return "Anchor";
                case "visible":
                    return "Visible";
                case "font":
                    return "Font";
                case "fontcolor":
                    return "ForeColor";
                case "_readonly":
                    return "ReadOnly";
                case "tabindex":
                    return "TabIndex";
                case "multiline":
                    return "Multiline";
                case "imageno":
                    return "Image";
                case "headertext":
                    return "HeaderText";
                case "datapropertyname":
                    return "DataPropertyName";
                case "miniwidth":
                    return "MinimumWidth";
                case "width":
                    return "Width";
                case "autosizemode":
                    return "AutoSizeMode";
                case "multiselect":
                    return "MultiSelect";
                case "displaystyle":
                    return "DisplayStyle";
                case "contextmenu":
                    return "ContextMenuStrip";
                case "startposition":
                    return "StartPosition";
                case "windowstate":
                    return "WindowState";
                case "maximizebox":
                    return "MaximizeBox";
                case "minimizebox":
                    return "MinimizeBox";
                default:
                    return attrName;
            }
        }
    }

    public class ControlTypeObj
    {
        public ControlTypeObj(string _CategoryName,string _LabelName,string _TypeName,Type _ControlType)
        {
            CategoryName = _CategoryName;
            LabelName = _LabelName;
            TypeName = _TypeName;
            ControlType = _ControlType;
        }

        public ControlTypeObj(string _CategoryName, string _LabelName, string _TypeName, Type _ControlType,bool _IsShow)
        {
            _IsToolBoxShow = _IsShow;
            CategoryName = _CategoryName;
            LabelName = _LabelName;
            TypeName = _TypeName;
            ControlType = _ControlType;
        }

        private bool _IsToolBoxShow = true;
        /// <summary>
        /// 是否显示在工具箱中
        /// </summary>
        public bool IsToolBoxShow { get
            {
                return _IsToolBoxShow;
            }
            set
            {
                _IsToolBoxShow = value;
            }
        }
        public string CategoryName { get; set; }
        public string LabelName { get; set; }
        public string TypeName { get; set; }
        public Type ControlType { get; set; }
    }
}
