using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染工具栏
    /// </summary>
    public class RenderTool : RenderObject
    {
        public RenderTool(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
            //bar不支持
            //base.IsSuspendLayout = true;
        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderToolType.toolstrip:
                    _control = new ToolStrip();
                    _control.Dock = DockStyle.Top;
                    break;
                case RenderToolType.toolbar:
                    _control=new DevComponents.DotNetBar.Bar();
                    (_control as DevComponents.DotNetBar.Bar).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
            if (ChildControlName.Length > 0)
            {
                string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
                switch (type)
                {
                    case null:
                    case RenderToolType.toolstrip:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as ToolStrip).Items.Add(sonCtrl.Control as ToolStripItem);
                        }
                        break;
                    case RenderToolType.toolbar:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as DevComponents.DotNetBar.Bar).Items.Add(sonCtrl.Control as DevComponents.DotNetBar.BaseItem);
                        }
                        break;
                }
            }
        }
    }

    public class RenderToolType
    {
        public const string toolstrip = "toolstrip";
        public const string toolbar = "toolbar";
    }
}
