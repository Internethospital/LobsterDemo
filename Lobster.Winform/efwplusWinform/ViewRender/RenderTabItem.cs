using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染Tab的子控件
    /// </summary>
    public class RenderTabItem : RenderObject
    {
        public RenderTabItem(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
            //IsSuspendLayout = true;
        }

        public override Object CreateControl()
        {

            object  _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderTabItemType.tabpage:
                    _control = new TabPage();
                    (_control as TabPage).Name = Guid.NewGuid().ToString();
                    break;
                case RenderTabItemType.tabItem:
                    _control = new DevComponents.DotNetBar.TabItem();
                    (_control as DevComponents.DotNetBar.TabItem).Name = Guid.NewGuid().ToString();
                    break;
                case RenderTabItemType.superTabItem:
                    _control = new DevComponents.DotNetBar.SuperTabItem();
                    (_control as DevComponents.DotNetBar.SuperTabItem).Name = Guid.NewGuid().ToString();
                    break;
                default:
                    throw new Exception("标签属性type设置的值找不到对应对象");
            }

            
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
                    case RenderTabItemType.tabpage:
                        //要从最大的开始循环，因为最小的是置底
                        for (int i = 0; i < ChildControlName.Length; i++)
                        {
                            string s = ChildControlName[i];
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as TabPage).Controls.Add(sonCtrl.Control as Control);
                        }
                        break;
                    case RenderTabItemType.tabItem:
                        //要从最大的开始循环，因为最小的是置底
                        for (int i = 0; i < ChildControlName.Length; i++)
                        {
                            string s = ChildControlName[i];
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as DevComponents.DotNetBar.TabItem).AttachedControl.Controls.Add(sonCtrl.Control as Control);
                        }
                        break;
                    case RenderTabItemType.superTabItem:
                        //要从最大的开始循环，因为最小的是置底
                        for (int i = 0; i < ChildControlName.Length; i++)
                        {
                            string s = ChildControlName[i];
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as DevComponents.DotNetBar.SuperTabItem).AttachedControl.Controls.Add(sonCtrl.Control as Control);
                        }
                        break;
                }
            }
        }
    }

    public class RenderTabItemType
    {
        public const string tabpage = "tabpage";
        public const string tabItem = "tabItem";
        public const string superTabItem = "superTabItem";
    }
}
