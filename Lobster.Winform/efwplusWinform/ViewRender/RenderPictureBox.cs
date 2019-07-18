using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染PictureBox控件
    /// </summary>
    public class RenderPictureBox : RenderObject
    {
        public RenderPictureBox(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
        }

        public override Object CreateControl()
        {
            PictureBox _control = new PictureBox();
            _control.Name = Guid.NewGuid().ToString();
            return _control;
        }

    }
}
