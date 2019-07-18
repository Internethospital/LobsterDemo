using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染网格控件的列
    /// </summary>
    public class RenderColumn : RenderObject
    {
        public RenderColumn(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
        }

        public override Object CreateControl()
        {
            DataGridViewColumn _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderColumnType.datagridtextbox:
                    _control = new DataGridViewTextBoxColumn();
                    break;
                case RenderColumnType.datagridbutton:
                    _control = new DataGridViewButtonColumn();
                    break;
                case RenderColumnType.datagridcheckbox:
                    _control = new DataGridViewCheckBoxColumn();
                    break;
                case RenderColumnType.datagridcombobox:
                    _control = new DataGridViewComboBoxColumn();
                    break;
                case RenderColumnType.datagridimage:
                    _control = new DataGridViewImageColumn();
                    break;
                case RenderColumnType.datagriddatetimeX:
                    _control = new DataGridViewDateTimeInputColumn();
                    break;
                case RenderColumnType.datagridlabelX:
                    _control = new DataGridViewLabelXColumn();
                    break;
                case RenderColumnType.datagridcomboboxEx:
                    _control = new DataGridViewComboBoxExColumn();
                    break;
                case RenderColumnType.datagridbuttonX:
                    _control = new DataGridViewButtonXColumn();
                    break;
                default:
                    throw new Exception("标签属性type设置的值找不到对应对象");
            }

            _control.Name = Guid.NewGuid().ToString();
            return _control;
        }
    }

    public class RenderColumnType
    {
        public const string datagridtextbox = "datagridtextbox";
        public const string datagridbutton = "datagridbutton";  
        public const string datagridcheckbox = "datagridcheckbox";
        public const string datagridcombobox = "datagridcombobox";
        public const string datagridimage = "datagridimage";

        public const string datagriddatetimeX = "datagriddatetimeX";
        public const string datagridlabelX = "datagridlabelX";
        public const string datagridcomboboxEx = "datagridcomboboxEx";
        public const string datagridbuttonX = "datagridbuttonX";
    }
}
