using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染Label控件
    /// </summary>
    public class RenderLabel : RenderObject
    {
        public RenderLabel(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderLabelType.label:
                    _control = new Label();
                    break;
                case RenderLabelType.linkLabel:
                    _control = new LinkLabel();
                    break;
                case RenderLabelType.labelX:
                    _control = new LabelX();
                    (_control as LabelX).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    break;
                default:
                    throw new Exception("标签属性type设置的值找不到对应对象");
            }

            _control.Name = Guid.NewGuid().ToString();
            return _control;
        }
    }

    public class RenderLabelType
    {
        public const string label = "label";
        public const string labelX = "labelX";
        public const string linkLabel = "linkLabel";
    }
}
