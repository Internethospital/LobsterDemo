using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染Tab控件
    /// </summary>
    public class RenderTabControl : RenderObject
    {
        public RenderTabControl(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
            IsSuspendLayout = true;
        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderTabControlType.tabcontrol:
                    _control = new TabControl();
                    break;
                case RenderTabControlType.tabcontrolX:
                    _control = new DevComponents.DotNetBar.TabControl();
                    (_control as DevComponents.DotNetBar.TabControl).CanReorderTabs = true;
                    (_control as DevComponents.DotNetBar.TabControl).Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
                    (_control as DevComponents.DotNetBar.TabControl).TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
                    break;
                case RenderTabControlType.superTabControl:
                    _control = new DevComponents.DotNetBar.SuperTabControl();
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
                //增加子控件
                string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
                switch (type)
                {
                    case null:
                    case RenderTabControlType.tabcontrol:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as TabControl).Controls.Add(sonCtrl.Control as TabPage);
                        }
                        break;
                    case RenderTabControlType.tabcontrolX:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            DevComponents.DotNetBar.TabControlPanel tabcp = new DevComponents.DotNetBar.TabControlPanel();
                            tabcp.Dock = DockStyle.Fill;
                            tabcp.Name= Guid.NewGuid().ToString();
                            tabcp.TabItem = sonCtrl.Control as DevComponents.DotNetBar.TabItem;
                            tabcp.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                            tabcp.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) | DevComponents.DotNetBar.eBorderSide.Bottom)));
                            tabcp.Style.GradientAngle = 90;
                            (sonCtrl.Control as DevComponents.DotNetBar.TabItem).AttachedControl = tabcp;
                            (Control as DevComponents.DotNetBar.TabControl).Controls.Add(tabcp);
                            (Control as DevComponents.DotNetBar.TabControl).Tabs.Add(sonCtrl.Control as DevComponents.DotNetBar.TabItem);
                        }
                        break;
                    case RenderTabControlType.superTabControl:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            DevComponents.DotNetBar.SuperTabControlPanel stabcp = new DevComponents.DotNetBar.SuperTabControlPanel();
                            stabcp.Dock = DockStyle.Fill;
                            stabcp.Name = Guid.NewGuid().ToString();
                            stabcp.TabItem = sonCtrl.Control as DevComponents.DotNetBar.SuperTabItem;
                            (sonCtrl.Control as DevComponents.DotNetBar.SuperTabItem).AttachedControl = stabcp;
                            (Control as DevComponents.DotNetBar.SuperTabControl).Controls.Add(stabcp);
                            (Control as DevComponents.DotNetBar.SuperTabControl).Tabs.Add(sonCtrl.Control as DevComponents.DotNetBar.SuperTabItem);
                        }
                        break;
                }
            }
        }

    }

    public class RenderTabControlType
    {
        public const string tabcontrol = "tabcontrol";
        public const string tabcontrolX = "tabcontrolX";
        public const string superTabControl = "superTabControl";
    }
}
