using DevComponents.DotNetBar.Controls;
using EfwControls.CustomControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染Input输入控件
    /// </summary>
    public class RenderInput : RenderObject
    {
        public RenderInput(object control, XmlNode node, RenderMode mode) : base(control, node, mode)
        {
        }

        public override Object CreateControl()
        {
            Control _control = null;
            string type = AttributeCollection.ContainsKey(XMLLabelAttribute.type) ? AttributeCollection[XMLLabelAttribute.type] : null;
            switch (type)
            {
                case null:
                case RenderInputType.textbox:
                    _control = new TextBox();
                    break;
                case RenderInputType.combobox:
                    _control = new ComboBox();
                    break;
                case RenderInputType.checkbox:
                    _control = new CheckBox();
                    break;
                case RenderInputType.radiobutton:
                    _control = new RadioButton();
                    break;
                case RenderInputType.dateTimePicker:
                    _control = new DateTimePicker();
                    break;
                case RenderInputType.richTextBox:
                    _control = new RichTextBox();
                    break;
                case RenderInputType.comboBoxEx:
                    _control = new ComboBoxEx();
                    (_control as ComboBoxEx).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    break;
                case RenderInputType.richTextBoxEx:
                    _control = new RichTextBoxEx();
                    (_control as RichTextBoxEx).BackgroundStyle.Class = "RichTextBoxBorder";
                    (_control as RichTextBoxEx).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    break;
                case RenderInputType.textBoxX:
                    _control = new TextBoxX();
                    (_control as TextBoxX).Border.Class = "TextBoxBorder";
                    (_control as TextBoxX).Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    break;
                case RenderInputType.checkBoxX:
                    _control = new CheckBoxX();
                    (_control as CheckBoxX).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as CheckBoxX).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    break;
                case RenderInputType.integerInput:
                    _control = new DevComponents.Editors.IntegerInput();
                    (_control as DevComponents.Editors.IntegerInput).BackgroundStyle.Class = "DateTimeInputBackground";
                    (_control as DevComponents.Editors.IntegerInput).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as DevComponents.Editors.IntegerInput).ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
                    (_control as DevComponents.Editors.IntegerInput).ShowUpDown = true;
                    break;
                case RenderInputType.dateTimeInput:
                    _control = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).BackgroundStyle.Class = "DateTimeInputBackground";
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).ButtonDropDown.Visible = true;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).IsPopupCalendarOpen = false;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).CustomFormat = "yyyy-MM-dd";
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
                    // 
                    // 
                    // 
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.ClearButtonVisible = true;
                    // 
                    // 
                    // 
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.DisplayMonth = new System.DateTime(2018, 4, 1, 0, 0, 0, 0);
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.MarkedDates = new System.DateTime[0];
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
                    // 
                    // 
                    // 
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.TodayButtonVisible = true;
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
                    (_control as DevComponents.Editors.DateTimeAdv.DateTimeInput).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    break;
                case RenderInputType.comboTree:
                    _control = new ComboTree();
                    (_control as ComboTree).BackgroundStyle.Class = "TextBoxBorder";
                    (_control as ComboTree).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as ComboTree).ButtonDropDown.Visible = true;
                    (_control as ComboTree).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    break;
                case RenderInputType.doubleInput:
                    _control = new DevComponents.Editors.DoubleInput();
                    (_control as DevComponents.Editors.DoubleInput).BackgroundStyle.Class = "DateTimeInputBackground";
                    (_control as DevComponents.Editors.DoubleInput).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as DevComponents.Editors.DoubleInput).ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
                    (_control as DevComponents.Editors.DoubleInput).Increment = 1D;
                    (_control as DevComponents.Editors.DoubleInput).ShowUpDown = true;
                    break;
                case RenderInputType.switchButton:
                    _control = new SwitchButton();
                    (_control as SwitchButton).BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    (_control as SwitchButton).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    break;
                case RenderInputType.textBoxCard:
                    _control = new TextBoxCard();
                    (_control as TextBoxCard).Border.Class = "TextBoxBorder";
                    (_control as TextBoxCard).Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    //this.textBoxCard1.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("textBoxCard1.ButtonCustom.Image")));
                    //this.textBoxCard1.ButtonCustom.Visible = true;
                    break;
                case RenderInputType.multiSelectText:
                    _control = new MultiSelectText();
                    break;
                case RenderInputType.statDateTime:
                    _control = new StatDateTime();
                    break;
                default:
                    throw new Exception("标签属性type设置的值找不到对应对象");
            }

            _control.Name = Guid.NewGuid().ToString();
            return _control;
        }
    }

    public class RenderInputType
    {
        //winform默认控件
        public const string textbox = "textbox";
        public const string combobox = "combobox";
        public const string checkbox = "checkbox";
        public const string radiobutton = "radiobutton";
        public const string dateTimePicker = "dateTimePicker";
        public const string richTextBox = "richTextBox";
        //donetbar控件
        public const string comboBoxEx = "comboBoxEx";
        public const string richTextBoxEx = "richTextBoxEx";
        public const string textBoxX = "textBoxX";
        public const string checkBoxX = "checkBoxX";
        public const string integerInput = "integerInput";
        public const string dateTimeInput = "dateTimeInput";
        public const string comboTree = "comboTree";
        public const string doubleInput = "doubleInput";
        //public const string colorPickerButton = "colorPickerButton";
        public const string switchButton = "switchButton";
        //efw控件
        public const string textBoxCard = "textBoxCard";
        public const string multiSelectText = "multiSelectText";
        public const string statDateTime = "statDateTime";
    }
}
