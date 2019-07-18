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
    /// 渲染Button控件
    /// </summary>
    public class RenderButton : RenderObject
    {
        public RenderButton(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderButtonType.button:
                    _control = new Button();
                    break;
                case RenderButtonType.buttonX:
                    _control = new ButtonX();
                    (_control as ButtonX).AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                    (_control as ButtonX).ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
                    (_control as ButtonX).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    break;
                default:
                    throw new Exception("标签属性type设置的值找不到对应对象");
            }

            _control.Name = Guid.NewGuid().ToString();
            return _control;
        }

        public override void InitControlAttribute(List<RenderObject> renderObjList)
        {
            base.InitControlAttribute(renderObjList);
        }
    }

    public class RenderButtonType
    {
        public const string button = "button";
        public const string buttonX = "buttonX";
    }
}
