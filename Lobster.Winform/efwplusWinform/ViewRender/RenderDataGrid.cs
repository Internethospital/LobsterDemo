using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染DataGrid控件
    /// </summary>
    public class RenderDataGrid : RenderObject
    {
        public RenderDataGrid(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {

        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderDataGridType.datagridview:
                    _control = new DataGridView();
                    break;
                case RenderDataGridType.datagridviewX:
                    _control = new DataGridViewX();
                    break;
                case RenderDataGridType.efwdatagrid:
                    _control = new EfwControls.CustomControl.DataGrid();
                    break;
                case RenderDataGridType.efwgridboxcard:
                    _control = new EfwControls.CustomControl.GridBoxCard();
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
                    case RenderDataGridType.datagridview:
                    case RenderDataGridType.datagridviewX:
                    case RenderDataGridType.efwdatagrid:
                    case RenderDataGridType.efwgridboxcard:
                        foreach (string s in ChildControlName)
                        {
                            RenderObject sonCtrl = renderObjList.Find(x => x.ControlName == s);
                            (Control as DataGridView).Columns.Add(sonCtrl.Control as DataGridViewColumn);
                        }
                        break;
                }
            }
        }
    }

    public class RenderDataGridType
    {
        public const string datagridview = "datagridview";
        public const string datagridviewX = "datagridviewX";
        public const string efwdatagrid = "efwdatagrid";
        public const string efwgridboxcard = "efwgridboxcard";
    }
}
