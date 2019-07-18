using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    public class RenderPanel : RenderObject
    {
        public RenderPanel(object control, XmlNode node, RenderMode mode) : base(control, node,mode)
        {
            base.IsSuspendLayout = true;
        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderPanelType.panel:
                    _control = new Panel();
                    if (RMode == RenderMode.design)
                    {
                        (_control as Panel).BorderStyle = BorderStyle.FixedSingle;
                    }
                    break;
                case RenderPanelType.panelEx:
                    _control = new PanelEx();
                    (_control as PanelEx).ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    (_control as PanelEx).CanvasColor = System.Drawing.SystemColors.Control;
                    (_control as PanelEx).ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    (_control as PanelEx).Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as PanelEx).Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as PanelEx).Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                    (_control as PanelEx).Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                    (_control as PanelEx).Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                    (_control as PanelEx).Style.GradientAngle = 90;
                    break;
                case RenderPanelType.groupBox:
                    _control = new GroupBox();
                    break;
                case RenderPanelType.groupPanel:
                    _control = new GroupPanel();
                    (_control as GroupPanel).CanvasColor = System.Drawing.SystemColors.Control;
                    (_control as GroupPanel).ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
                    (_control as GroupPanel).DrawTitleBox = false;
                    (_control as GroupPanel).Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                    (_control as GroupPanel).Style.BackColorGradientAngle = 90;
                    (_control as GroupPanel).Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as GroupPanel).Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
                    (_control as GroupPanel).Style.BorderBottomWidth = 1;
                    (_control as GroupPanel).Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                    (_control as GroupPanel).Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
                    (_control as GroupPanel).Style.BorderLeftWidth = 1;
                    (_control as GroupPanel).Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
                    (_control as GroupPanel).Style.BorderRightWidth = 1;
                    (_control as GroupPanel).Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
                    (_control as GroupPanel).Style.BorderTopWidth = 1;
                    (_control as GroupPanel).Style.CornerDiameter = 4;
                    (_control as GroupPanel).Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as GroupPanel).Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
                    (_control as GroupPanel).Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                    (_control as GroupPanel).Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
                    break;
                case RenderPanelType.expandablePanel:
                    _control = new ExpandablePanel();
                    (_control as ExpandablePanel).CanvasColor = System.Drawing.SystemColors.Control;
                    (_control as ExpandablePanel).ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    (_control as ExpandablePanel).HideControlsWhenCollapsed = true;
                    (_control as ExpandablePanel).Style.Alignment = System.Drawing.StringAlignment.Center;
                    (_control as ExpandablePanel).Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as ExpandablePanel).Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as ExpandablePanel).Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
                    (_control as ExpandablePanel).Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
                    (_control as ExpandablePanel).Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
                    (_control as ExpandablePanel).Style.GradientAngle = 90;
                    (_control as ExpandablePanel).TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
                    (_control as ExpandablePanel).TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as ExpandablePanel).TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as ExpandablePanel).TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
                    (_control as ExpandablePanel).TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                    (_control as ExpandablePanel).TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                    (_control as ExpandablePanel).TitleStyle.GradientAngle = 90;
                    break;
                case RenderPanelType.expandableSplitter:
                    _control = new DevComponents.DotNetBar.ExpandableSplitter();
                    (_control as DevComponents.DotNetBar.ExpandableSplitter).Style= DevComponents.DotNetBar.eSplitterStyle.Office2007;
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
    }

    public class RenderPanelType
    {
        public const string panel = "panel";
        public const string panelEx = "panelEx";
        public const string groupBox = "groupBox";
        public const string groupPanel = "groupPanel";
        public const string expandablePanel = "expandablePanel";
        public const string expandableSplitter = "expandableSplitter";
    }
}
