using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染树控件
    /// </summary>
    public class RenderTree : RenderObject
    {
        public RenderTree(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {

        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderTreeType.treeview:
                    _control = new TreeView();
                    break;
                case RenderTreeType.advTree:
                    _control = new DevComponents.AdvTree.AdvTree();
                    (_control as DevComponents.AdvTree.AdvTree).BackgroundStyle.Class = "TreeBorderKey";
                    (_control as DevComponents.AdvTree.AdvTree).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
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
                    case RenderTreeType.treeview:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as TreeView).Nodes.Add(sonCtrl.Control as TreeNode);
                        }
                        break;
                    case RenderTreeType.advTree:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as DevComponents.AdvTree.AdvTree).Nodes.Add(sonCtrl.Control as DevComponents.AdvTree.Node);
                        }
                        break;
                }
            }
        }
    }

    public class RenderTreeType
    {
        public const string treeview = "treeview";
        public const string advTree = "advTree";
    }
}
