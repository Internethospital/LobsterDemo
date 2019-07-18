using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    public class RenderWinform : RenderObject
    {
        public RenderWinform(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
            base.IsSuspendLayout = true;
        }

        public override void InitControlAttribute(List<RenderObject> renderObjList)
        {
            base.InitControlAttribute(renderObjList);
            //增加子控件
            if (ChildControlName.Length > 0)
            {
                //要从最大的开始循环，因为最小的是置底
                for (int i = 0; i < ChildControlName.Length; i++)
                {
                    string s = ChildControlName[i];
                    RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                    (Control as Control).Controls.Add(sonCtrl.Control as Control);
                }
            }
        }

        public override Object CreateControl()
        {
            Form _control = new Form();
            _control.Name = Guid.NewGuid().ToString();//指定控件名称
            return _control;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
