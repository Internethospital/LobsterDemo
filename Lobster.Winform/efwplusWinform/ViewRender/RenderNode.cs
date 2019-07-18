using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染树节点控件
    /// </summary>
    public class RenderNode : RenderObject
    {
        public RenderNode(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
        }

        public override Object CreateControl()
        {
            Object _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderNodeType.treenode:
                    _control = new TreeNode();
                    ((TreeNode)_control).Name = Guid.NewGuid().ToString();
                    break;
                case RenderNodeType.advnode:
                    _control = new DevComponents.AdvTree.Node();
                    ((DevComponents.AdvTree.Node)_control).Name = Guid.NewGuid().ToString();
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
                    case RenderNodeType.treenode:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as TreeNode).Nodes.Add(sonCtrl.Control as TreeNode);
                        }
                        break;
                    case RenderNodeType.advnode:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as DevComponents.AdvTree.Node).Nodes.Add(sonCtrl.Control as DevComponents.AdvTree.Node);
                        }
                        break;
                }
            }
        }
    }

    public class RenderNodeType
    {
        public const string treenode = "treenode";
        public const string advnode = "advnode";
    }
}
