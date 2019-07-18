using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染工具栏的子控件
    /// </summary>
    public class RenderToolItem : RenderObject
    {
        public RenderToolItem(Object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {

        }

        public override Object CreateControl()
        {
            Object _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderToolItemType.toolStripButton:
                    _control = new ToolStripButton();
                    (_control as ToolStripButton).Name = Guid.NewGuid().ToString();
                    break;
                case RenderToolItemType.toolStripLabel:
                    _control = new ToolStripLabel();
                    (_control as ToolStripLabel).Name = Guid.NewGuid().ToString();
                    break;
                case RenderToolItemType.toolStripComboBox:
                    _control = new ToolStripComboBox();
                    (_control as ToolStripComboBox).Name = Guid.NewGuid().ToString();
                    break;
                case RenderToolItemType.toolStripTextBox:
                    _control = new ToolStripTextBox();
                    (_control as ToolStripTextBox).Name = Guid.NewGuid().ToString();
                    break;

                case RenderToolItemType.buttonItem:
                    _control = new DevComponents.DotNetBar.ButtonItem();
                    (_control as DevComponents.DotNetBar.ButtonItem).Name = Guid.NewGuid().ToString();
                    break;
                case RenderToolItemType.textBoxItem:
                    _control = new DevComponents.DotNetBar.TextBoxItem();
                    (_control as DevComponents.DotNetBar.TextBoxItem).Name = Guid.NewGuid().ToString();
                    break;
                case RenderToolItemType.comboBoxItem:
                    _control = new DevComponents.DotNetBar.ComboBoxItem();
                    (_control as DevComponents.DotNetBar.ComboBoxItem).Name = Guid.NewGuid().ToString();
                    break;
                case RenderToolItemType.labelItem:
                    _control = new DevComponents.DotNetBar.LabelItem();
                    (_control as DevComponents.DotNetBar.LabelItem).Name = Guid.NewGuid().ToString();
                    break;
                default:
                    throw new Exception("标签属性type设置的值找不到对应对象");
            }

            return _control;

        }
    }

    public class RenderToolItemType
    {
        //toolStripSeparator控件变为BeginGroup属性
        public const string toolStripButton = "toolStripButton";
        public const string toolStripLabel = "toolStripLabel";
        public const string toolStripComboBox = "toolStripComboBox";
        public const string toolStripTextBox = "toolStripTextBox";

        public const string buttonItem = "buttonItem";
        public const string textBoxItem = "textBoxItem";
        public const string comboBoxItem = "comboBoxItem";
        public const string labelItem = "labelItem";
    }
}
