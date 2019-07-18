using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染右键菜单
    /// </summary>
    public class RenderContextMenu : RenderObject
    {
        public RenderContextMenu(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
            IsSuspendLayout = true;
        }

        public override Object CreateControl()
        {
            ContextMenuStrip _control = new ContextMenuStrip();
            _control.Name = Guid.NewGuid().ToString();
            return _control;
        }
        public override void InitControlAttribute(List<RenderObject> renderObjList)
        {
            base.InitControlAttribute(renderObjList);

            if (ChildControlName.Length > 0)
            {
                //要从最大的开始循环，因为最小的是置底
                for (int i = 0; i < ChildControlName.Length; i++)
                {
                    string s = ChildControlName[i];
                    RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                    (Control as ContextMenuStrip).Items.Add(sonCtrl.Control as ToolStripItem);
                }
            }
        }
       
    }
}
