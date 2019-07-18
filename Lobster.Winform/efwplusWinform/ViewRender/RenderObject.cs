using efwplusWinform.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    public class RenderObject
    {
        #region 属性
        private Object _control;
        /// <summary>
        /// 显示控件
        /// </summary>
        public Object Control
        {
            get { return _control; }
        }

        private XmlNode _xmlnode;
        /// <summary>
        /// 控件对应的XML配置节点
        /// </summary>
        public XmlNode XNode
        {
            get { return _xmlnode; }
        }

        private RenderMode _mode;
        /// <summary>
        /// 控件渲染模式
        /// </summary>
        public RenderMode RMode
        {
            get { return _mode; }
        }

        protected string _controlname;
        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName
        {
            get
            {
                if (_controlname == null)
                {
                    _controlname = GetControlAttributeValue("name").ToString();
                }
                return _controlname;
            }
        }

        private List<string> _childcontrolname;
        /// <summary>
        /// 包含子控件名称
        /// </summary>
        public string[] ChildControlName
        {
            get
            {
                return _childcontrolname.ToArray();
            }
        }

        private Dictionary<string, string> _attributecollection;
        /// <summary>
        /// 属性集合
        /// </summary>
        public Dictionary<string, string> AttributeCollection
        {
            get { return _attributecollection; }
        }

        private bool _isSuspendLayout = false;
        /// <summary>
        /// 是否需要挂起布局
        /// </summary>
        public bool IsSuspendLayout
        {
            get { return _isSuspendLayout; }
            set { _isSuspendLayout = value; }
        }

        #endregion

        public RenderObject(Object control)
        {
            _control = control;
            _mode = RenderMode.run;
            _childcontrolname = new List<string>();
            _attributecollection = new Dictionary<string, string>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="control"></param>
        /// <param name="node"></param>
        /// <param name="mode"></param>
        public RenderObject(Object control, XmlNode node, RenderMode mode)
        {
            _control = control;
            _xmlnode = node;
            _mode = mode;

            _childcontrolname = new List<string>();
            _attributecollection = new Dictionary<string, string>();
            foreach (XmlAttribute item in XNode.Attributes)
            {
                _attributecollection.Add(item.Name, item.Value);
            }

            if (_control == null)
            {
                _control = CreateControl();
            }

            if (XNode.Attributes[XMLLabelAttribute.name] != null)
            {
                _controlname = XNode.Attributes[XMLLabelAttribute.name].Value;
            }
        }

        /// <summary>
        /// 增加子控件名称
        /// </summary>
        /// <param name="controlname"></param>
        public void AddChildControlName(string controlname)
        {
            if (_childcontrolname.FindIndex(x => x == controlname) == -1)
            {
                _childcontrolname.Add(controlname);
            }
        }

        /// <summary>
        /// 如果_control为空的时候，调用此函数创建控件
        /// </summary>
        public virtual Object CreateControl()
        {
            return null;
        }

        /// <summary>
        /// 设置控件属性
        /// </summary>
        public virtual void InitControlAttribute(List<RenderObject> renderObjList)
        {

            if (_mode == RenderMode.design)
            {
                string[] desAttr = XMLLabelAttribute.GetControlAttributeNames(_mode);

                foreach (var item in AttributeCollection)
                {
                    if (desAttr.ToList().FindIndex(x => x == item.Key) > -1)
                    {
                        SetControlAttributeValue(item.Key, item.Value);
                    }
                }
            }
            else
            {

                foreach (var item in AttributeCollection)
                {
                    if (item.Key == XMLLabelAttribute.contextmenu || item.Key == XMLLabelAttribute.type)
                        continue;
                    SetControlAttributeValue(item.Key, item.Value);
                }

                //contextmenu 根据名称找到控件对象赋值
                if (AttributeCollection.ContainsKey(XMLLabelAttribute.contextmenu))
                {
                    string menuname = AttributeCollection[XMLLabelAttribute.contextmenu];
                    SetControlAttributeValue(XMLLabelAttribute.contextmenu, renderObjList.Find(x => x.ControlName == menuname), null);
                }
            }
        }
        /// <summary>
        /// 清空子控件
        /// </summary>
        public virtual void Dispose()
        {
            if (Control is IDisposable)
            {
                (Control as IDisposable).Dispose();
            }
        }
        /// <summary>
        /// 获取控件的属性值
        /// </summary>
        /// <param name="attrname"></param>
        /// <returns></returns>
        public virtual object GetControlAttributeValue(string attrname)
        {
            return GetControlAttributeValue(attrname, FormattingGetPropertyValue);
        }

        /// <summary>
        /// 获取控件的属性值
        /// </summary>
        /// <param name="attrname"></param>
        /// <returns></returns>
        public virtual object GetControlAttributeValue(string attrname, Func<string, object, object> formatting)
        {
            string propertyName = XMLLabelAttribute.GetPropertyName(attrname, AttributeCollection.ContainsKey(XMLLabelAttribute.type) == false ? null : AttributeCollection[XMLLabelAttribute.type]);
            object propertyValue = null;

            //List<PropertyInfo> proList = Control.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();
            //PropertyInfo pro = proList.Find(x => x.Name.ToLower() == propertyName.ToLower());
            PropertyInfo pro = Control.GetType().GetProperty(propertyName);
            if (pro != null)
            {
                propertyValue = pro.GetValue(Control, null);
            }

            if (formatting != null)
            {
                propertyValue = formatting(attrname, propertyValue);
            }
            return propertyValue;
        }
        /// <summary>
        /// 设置控件的属性值
        /// </summary>
        /// <param name="attrname"></param>
        /// <param name="value"></param>
        public virtual void SetControlAttributeValue(string attrname, object value)
        {
            SetControlAttributeValue(attrname, value, FormattingSetPropertyValue);
        }

        /// <summary>
        /// 设置控件的属性值
        /// </summary>
        /// <param name="attrname"></param>
        /// <param name="value"></param>
        /// <param name="formatting">格式化value为属性需要的数据</param>
        public virtual void SetControlAttributeValue(string attrname, object value, Func<string, object, object> formatting)
        {

            string propertyName = XMLLabelAttribute.GetPropertyName(attrname, AttributeCollection.ContainsKey(XMLLabelAttribute.type) == false ? null : AttributeCollection[XMLLabelAttribute.type]);
            object propertyValue = value;

            //List<PropertyInfo> proList = Control.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).ToList();
            //PropertyInfo pro = proList.Find(x => x.Name.ToLower() == propertyName.ToLower());
            PropertyInfo pro = Control.GetType().GetProperty(propertyName);
            if (pro != null)
            {
                #region 常用属性值处理
                switch (attrname)
                {
                    case XMLLabelAttribute.name:
                    case XMLLabelAttribute.text:
                    case XMLLabelAttribute.headertext:
                    case XMLLabelAttribute.datapropertyname:
                    case XMLLabelAttribute.miniwidth:
                    case XMLLabelAttribute.width:
                        propertyValue = value.ToString();
                        break;
                    case XMLLabelAttribute.visible:
                    case XMLLabelAttribute.enabled:
                    case XMLLabelAttribute._readonly:
                    case XMLLabelAttribute.maximizebox:
                    case XMLLabelAttribute.minimizebox:
                    case XMLLabelAttribute.multiline://文本行支持多行
                    case XMLLabelAttribute.multiselect:
                        propertyValue = value.ToString() == "true" ? true : false;
                        break;
                    case XMLLabelAttribute.textalign://有问题，button 和textbox 不一样
                        switch (value.ToString().ToLower())
                        {
                            case "middlecenter":
                                propertyValue = ContentAlignment.MiddleCenter;
                                break;
                            case "middleleft":
                                propertyValue = ContentAlignment.MiddleLeft;
                                break;
                            case "middleright":
                                propertyValue = ContentAlignment.MiddleRight;
                                break;
                            case "topcenter":
                                propertyValue = ContentAlignment.TopCenter;
                                break;
                            case "topleft":
                                propertyValue = ContentAlignment.TopLeft;
                                break;
                            case "topright":
                                propertyValue = ContentAlignment.TopRight;
                                break;
                            case "bottomcenter":
                                propertyValue = ContentAlignment.BottomCenter;
                                break;
                            case "bottomleft":
                                propertyValue = ContentAlignment.BottomLeft;
                                break;
                            case "bottomright":
                                propertyValue = ContentAlignment.BottomRight;
                                break;
                            //下面是textbox的textalign
                            case "left":
                                propertyValue = HorizontalAlignment.Left;
                                break;
                            case "right":
                                propertyValue = HorizontalAlignment.Right;
                                break;
                            case "center":
                                propertyValue = HorizontalAlignment.Center;
                                break;
                        }
                        break;
                    case XMLLabelAttribute.location:
                        System.Drawing.Point _location = new System.Drawing.Point();
                        _location.X = Convert.ToInt32(value.ToString().Split(',')[0]);
                        _location.Y = Convert.ToInt32(value.ToString().Split(',')[1]);
                        propertyValue = _location;
                        break;
                    case XMLLabelAttribute.size:
                        System.Drawing.Size _size = new System.Drawing.Size();
                        _size.Width = Convert.ToInt32(value.ToString().Split(',')[0]);
                        _size.Height = Convert.ToInt32(value.ToString().Split(',')[1]);
                        propertyValue = _size;
                        break;
                    case XMLLabelAttribute.dock:
                        switch (value.ToString().ToLower())
                        {
                            case "top":
                                propertyValue = DockStyle.Top;
                                break;
                            case "left":
                                propertyValue = DockStyle.Left;
                                break;
                            case "right":
                                propertyValue = DockStyle.Right;
                                break;
                            case "bottom":
                                propertyValue = DockStyle.Bottom;
                                break;
                            case "fill":
                                propertyValue = DockStyle.Fill;
                                break;
                            default:
                                propertyValue = DockStyle.None;
                                break;
                        }
                        break;
                    case XMLLabelAttribute.anchor:
                        string[] str = value.ToString().Split(',');
                        AnchorStyles left = str.ToList().FindIndex(x => x.ToLower().Trim() == "left") != -1 ? AnchorStyles.Left : AnchorStyles.None;
                        AnchorStyles right = str.ToList().FindIndex(x => x.ToLower().Trim() == "right") != -1 ? AnchorStyles.Right : AnchorStyles.None;
                        AnchorStyles top = str.ToList().FindIndex(x => x.ToLower().Trim() == "top") != -1 ? AnchorStyles.Top : AnchorStyles.None;
                        AnchorStyles bottom = str.ToList().FindIndex(x => x.ToLower().Trim() == "bottom") != -1 ? AnchorStyles.Bottom : AnchorStyles.None;
                        propertyValue = (AnchorStyles)(((left | right) | top) | bottom);
                        break;

                    case XMLLabelAttribute.font:
                        string familyName = value.ToString().Split(',')[0];
                        float emSize = Convert.ToSingle(value.ToString().Split(',')[1]);
                        propertyValue = new System.Drawing.Font(familyName, emSize);
                        break;
                    case XMLLabelAttribute.fontcolor:
                        ColorConverter converter = new ColorConverter();
                        propertyValue = (Color)converter.ConvertFromString(value.ToString());
                        break;
                    case XMLLabelAttribute.customformat:
                        pro.SetValue(Control, Convert.ChangeType(0, pro.PropertyType), null);
                        propertyValue = value.ToString();
                        break;
                    case XMLLabelAttribute.value:
                        propertyValue = Convert.ToDateTime(value);
                        break;
                    case XMLLabelAttribute.startposition:
                        switch (value.ToString().ToLower())
                        {
                            case "manual":
                                propertyValue = FormStartPosition.Manual;
                                break;
                            case "centerscreen":
                                propertyValue = FormStartPosition.CenterScreen;
                                break;
                            case "windowsdefaultlocation":
                                propertyValue = FormStartPosition.WindowsDefaultLocation;
                                break;
                            case "windowsdefaultbounds":
                                propertyValue = FormStartPosition.WindowsDefaultBounds;
                                break;
                            case "centerparent":
                                propertyValue = FormStartPosition.CenterParent;
                                break;
                        }
                        break;
                    case XMLLabelAttribute.windowstate:
                        switch (value.ToString().ToLower())
                        {
                            case "normal":
                                propertyValue = FormWindowState.Normal;
                                break;
                            case "minimized":
                                propertyValue = FormWindowState.Minimized;
                                break;
                            case "maximized":
                                propertyValue = FormWindowState.Maximized;
                                break;
                        }
                        break;
                    case XMLLabelAttribute.imageno:
                        propertyValue = (value);//通过图标编码获取Image对象
                        break;
                    case XMLLabelAttribute.sizemode:
                        switch (value.ToString().ToLower())
                        {
                            case "zoom":
                                propertyValue = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                                break;
                            case "autosize":
                                propertyValue = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                                break;
                            case "centerimage":
                                propertyValue = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                                break;
                            case "normal":
                                propertyValue = System.Windows.Forms.PictureBoxSizeMode.Normal;
                                break;
                            case "stretchimage":
                                propertyValue = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                                break;
                        }
                        break;
                    case XMLLabelAttribute.autosizemode:
                        switch (value.ToString().ToLower())
                        {
                            case "fill":
                                propertyValue = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                                break;
                            default:
                                propertyValue = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
                                break;
                        }
                        break;
                    case XMLLabelAttribute.displaystyle:
                        if (Control is ToolStripButton)//Winform默认工具栏按钮
                        {
                            switch (value.ToString().ToLower())
                            {
                                case "image":
                                    propertyValue = 2;
                                    break;
                                case "text":
                                    propertyValue = 1;
                                    break;
                                case "imageandtext":
                                    propertyValue = 3;
                                    break;
                            }
                        }
                        else if (Control is DevComponents.DotNetBar.ButtonItem)//Donetbar控件的工具栏按钮
                        {
                            switch (value.ToString().ToLower())
                            {
                                case "image":
                                    propertyValue = 0;
                                    break;
                                case "text":
                                    propertyValue = 1;
                                    break;
                                case "imageandtext":
                                    propertyValue = 2;
                                    break;
                            }
                        }
                        break;
                    case XMLLabelAttribute.contextmenu://通过右键菜单的名称绑定菜单控件

                        break;
                }
                #endregion

                if (formatting != null)//
                {
                    try
                    {
                        pro.SetValue(Control, Convert.ChangeType(formatting(propertyName, propertyValue), pro.PropertyType), null);
                    }
                    catch (Exception e)
                    {
                        pro.SetValue(Control, formatting(propertyName, propertyValue), null);
                        Log.Info(e);
                    }
                }
                else
                {
                    try
                    {
                        pro.SetValue(Control, Convert.ChangeType(propertyValue, pro.PropertyType), null);
                    }
                    catch (Exception e)
                    {
                        pro.SetValue(Control, propertyValue, null);
                        Log.Info(e);
                    }
                }
            }

        }
        /// <summary>
        /// 格式化获取的属性值，比如将DataGrid的DataSource格式化为Json字符串
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns>格式化后的数据</returns>
        public virtual object FormattingGetPropertyValue(string propertyName, object propertyValue)
        {
            return propertyValue;
        }
        /// <summary>
        /// 格式化数据赋值给属性值，比如将Json字符串格式化为DataTable对象赋值给DataGrid的DataSource
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns>格式化后的数据</returns>
        public virtual object FormattingSetPropertyValue(string propertyName, object propertyValue)
        {
            return propertyValue;
        }

        private Dictionary<string, Object> eventDic = new Dictionary<string, object>();//存储事件
        public virtual void AddControlEvent(string eventname, Action<object, object> eventfun)
        {
            if (eventDic.ContainsKey(eventname)) return;

            string EventName = XMLLabelEvent.GetEventName(eventname);
            EventInfo evt = Control.GetType().GetEvent(EventName);

            switch (eventname)
            {
                case XMLLabelEvent.enter:
                case XMLLabelEvent.leave:
                case XMLLabelEvent.click:
                case XMLLabelEvent.doubleclick:
                case XMLLabelEvent.textchanged:
                case XMLLabelEvent.checkedchanged:
                case XMLLabelEvent.selectedindexchanged:
                case XMLLabelEvent.currentcellchanged:
                    EventHandler handler = (sender, args) =>
                    {
                        eventfun(sender, args);
                    };
                    evt.AddEventHandler(Control, handler);
                    eventDic.Add(eventname, handler);
                    break;
                case XMLLabelEvent.keypress:
                    KeyPressEventHandler handler_key = (sender, args) =>
                    {
                        eventfun(sender, args);
                    };
                    evt.AddEventHandler(Control, handler_key);
                    eventDic.Add(eventname, handler_key);
                    break;
                case XMLLabelEvent.afterselect:
                    TreeViewEventHandler handler_tree = (sender, args) =>
                    {
                        eventfun(sender, args);
                    };
                    evt.AddEventHandler(Control, handler_tree);
                    eventDic.Add(eventname, handler_tree);
                    break;
                case XMLLabelEvent.cellvaluechanged:
                    DataGridViewCellEventHandler handler_cell = (sender, args) =>
                    {
                        eventfun(sender, args);
                    };
                    evt.AddEventHandler(Control, handler_cell);
                    eventDic.Add(eventname, handler_cell);
                    break;
                case XMLLabelEvent.CellFormatting:
                    DataGridViewCellFormattingEventHandler handler_cellf = (sender, args) =>
                    {
                        eventfun(sender, args);
                    };
                    evt.AddEventHandler(Control, handler_cellf);
                    eventDic.Add(eventname, handler_cellf);
                    break;
            }
        }

        public virtual void RemoveControlEvent(string eventname)
        {
            if (eventDic.ContainsKey(eventname) == false) return;

            string EventName = XMLLabelEvent.GetEventName(eventname);
            EventInfo evt = Control.GetType().GetEvent(EventName);
            switch (eventname)
            {
                case XMLLabelEvent.enter:
                case XMLLabelEvent.leave:
                case XMLLabelEvent.click:
                case XMLLabelEvent.doubleclick:
                case XMLLabelEvent.textchanged:
                case XMLLabelEvent.checkedchanged:
                case XMLLabelEvent.selectedindexchanged:
                case XMLLabelEvent.currentcellchanged:
                    evt.RemoveEventHandler(Control, (EventHandler)eventDic[eventname]);
                    eventDic.Remove(eventname);
                    break;
                case XMLLabelEvent.keypress:
                    evt.RemoveEventHandler(Control, (KeyPressEventHandler)eventDic[eventname]);
                    eventDic.Remove(eventname);
                    break;
                case XMLLabelEvent.afterselect:
                    evt.RemoveEventHandler(Control, (TreeViewEventHandler)eventDic[eventname]);
                    eventDic.Remove(eventname);
                    break;
                case XMLLabelEvent.cellvaluechanged:
                    evt.RemoveEventHandler(Control, (DataGridViewCellEventHandler)eventDic[eventname]);
                    eventDic.Remove(eventname);
                    break;
                case XMLLabelEvent.CellFormatting:
                    evt.RemoveEventHandler(Control, (DataGridViewCellFormattingEventHandler)eventDic[eventname]);
                    eventDic.Remove(eventname);
                    break;
            }
        }

        /// <summary>
        /// 将AttributeCollection属性写回XmlNode，一般是在设计模式时调用
        /// </summary>
        public virtual void WriteBackXmlNode()
        {

        }
    }

    public static class XMLLabel
    {
        public const string winform = "winform";
        public const string panel = "panel";
        public const string tabcontrol = "tabcontrol";
        public const string tabitem = "tabitem";
        public const string input = "input";
        public const string button = "button";
        public const string label = "label";
        public const string picturebox = "picturebox";
        public const string tree = "tree";
        public const string node = "node";
        public const string datagrid = "datagrid";
        public const string column = "column";
        public const string tool = "tool";
        public const string toolitem = "toolitem";
        public const string contextmenu = "contextmenu";
        public const string menuitem = "menuitem";

        
    }

    public static class XMLLabelAttribute
    {
        /// <summary>
        /// 指定标签控件的类型
        /// </summary>
        public const string type = "type";

        public const string name = "name";//RenderObject
        public const string text = "text";//RenderObject
        public const string textalign = "textalign";
        public const string marktext = "marktext";
        /// <summary>
        /// Date控件的值
        /// </summary>
        public const string value = "value";
        /// <summary>
        /// Date控件的显示格式
        /// </summary>
        public const string customformat = "customformat";
        public const string enabled = "enabled";//RenderObject
        public const string location = "location";//RenderObject
        public const string size = "size";//RenderObject

        public const string dock = "dock";//RenderObject
        public const string anchor = "anchor";//RenderObject
        public const string visible = "visible";//RenderObject
        public const string font = "font";//RenderObject
        public const string fontcolor = "fontcolor";//RenderObject
        public const string _readonly = "readonly";//RenderObject
        public const string tabindex = "tabindex";//RenderObject
        public const string multiline = "multiline";
        /// <summary>
        /// 图标编码
        /// </summary>
        public const string imageno = "imageno";//
        /// <summary>
        /// 图片填充模式，zoom
        /// </summary>
        public const string sizemode = "sizemode";
        /// <summary>
        /// 网格控件设置是否多选
        /// </summary>
        public const string multiselect = "multiselect";
        /// <summary>
        /// 网格列头标题
        /// </summary>
        public const string headertext = "headertext";
        /// <summary>
        /// 网格列数据映射名称
        /// </summary>
        public const string datapropertyname = "datapropertyname";
        /// <summary>
        /// 网格列最小宽度
        /// </summary>
        public const string miniwidth = "miniwidth";
        /// <summary>
        /// 网格列宽度
        /// </summary>
        public const string width = "width";
        /// <summary>
        /// 网格列的尺寸模式，fill
        /// </summary>
        public const string autosizemode = "autosizemode";
        /// <summary>
        /// 工具栏按钮的显示模式，默认imageandtext
        /// </summary>
        public const string displaystyle = "displaystyle";
        /// <summary>
        /// 右键菜单名称
        /// </summary>
        public const string contextmenu = "contextmenu";
        /// <summary>
        /// 窗体属性
        /// </summary>
        public const string startposition = "startposition";
        public const string windowstate = "windowstate";
        public const string maximizebox = "maximizebox";
        public const string minimizebox = "minimizebox";

        public static string GetPropertyName(string attrname, string typeval)
        {
            switch (attrname)
            {
                case name:
                    return "Name";
                case text:
                    return "Text";
                case textalign:
                    return "TextAlign";
                case marktext:
                    return "WatermarkText";
                case value:
                    return "Value";
                case customformat:
                    return "CustomFormat";
                case enabled:
                    return "Enabled";
                case location:
                    return "Location";
                case size:
                    return "Size";
                case dock:
                    return "Dock";
                case anchor:
                    return "Anchor";
                case visible:
                    return "Visible";
                case font:
                    return "Font";
                case fontcolor:
                    return "ForeColor";
                case _readonly:
                    return "ReadOnly";
                case tabindex:
                    return "TabIndex";
                case multiline:
                    return "Multiline";
                case imageno:
                    return "Image";
                case headertext:
                    return "HeaderText";
                case datapropertyname:
                    return "DataPropertyName";
                case miniwidth:
                    return "MinimumWidth";
                case width:
                    return "Width";
                case autosizemode:
                    return "AutoSizeMode";
                case multiselect:
                    return "MultiSelect";
                case displaystyle:
                    if (typeval == "toolstrip")
                    {
                        return "DisplayStyle";
                    }
                    else if (typeval == "toolbar")
                    {
                        return "ButtonStyle";
                    }
                    return attrname;
                case contextmenu:
                    return "ContextMenuStrip";
                case startposition:
                    return "StartPosition";
                case windowstate:
                    return "WindowState";
                case maximizebox:
                    return "MaximizeBox";
                case minimizebox:
                    return "MinimizeBox";
                default:
                    return attrname;
            }
        }

        /// <summary>
        /// 获取控件的属性名称
        /// </summary>
        /// <param name="labelname"></param>
        /// <returns></returns>
        public static string[] GetControlAttributeNames(string labelname)
        {
            switch (labelname)
            {
                case XMLLabel.winform:
                    return new string[] { name, text, enabled, visible, size, startposition, windowstate, maximizebox, minimizebox };
                case XMLLabel.panel:
                    return new string[] { type, name, dock, location, size, enabled, visible };
                case XMLLabel.tabcontrol:
                    return new string[] { type, name, dock, location, size, enabled, visible };
                case XMLLabel.tabitem:
                    return new string[] { type, name, text, visible };
                case XMLLabel.input:
                    return new string[] { type, name, text, multiline, textalign, marktext, anchor, location, size, enabled, visible, _readonly, font, fontcolor, tabindex, value, customformat, imageno };
                case XMLLabel.button:
                    return new string[] { type, name, text, textalign, font, fontcolor, anchor, location, size, enabled, visible, tabindex, imageno };
                case XMLLabel.label:
                    return new string[] { type, name, text, textalign, font, fontcolor, anchor, location, size, enabled, visible };
                case XMLLabel.picturebox:
                    return new string[] { name, anchor, location, size, enabled, visible, sizemode };
                case XMLLabel.tree:
                    return new string[] { type, name, anchor, location, size, enabled, visible, dock, font, fontcolor, tabindex, contextmenu };
                case XMLLabel.node:
                    return new string[] { type, name, text };
                case XMLLabel.datagrid:
                    return new string[] { type, name, anchor, location, size, enabled, visible, dock, font, fontcolor, tabindex, _readonly, multiselect, contextmenu };
                case XMLLabel.column:
                    return new string[] { type, name, headertext, datapropertyname, miniwidth, width, _readonly, visible, autosizemode };
                case XMLLabel.tool:
                    return new string[] { type, name, dock, visible, enabled };
                case XMLLabel.toolitem:
                    return new string[] { type, name, displaystyle, imageno, enabled, visible };
                case XMLLabel.contextmenu:
                    return new string[] { name };
                case XMLLabel.menuitem:
                    return new string[] { name, enabled, visible };
                default:
                    return new string[] { };
            }
        }

        public static string[] GetControlAttributeNames(RenderMode mode)
        {
            if (mode == RenderMode.design)
            {
                return new string[] { name, text, textalign, location, size, dock, font, fontcolor, imageno };
            }

            return null;
        }
    }

    public static class XMLLabelEvent
    {
        public const string enter = "enter";
        public const string leave = "leave";
        public const string click = "click";
        public const string doubleclick = "doubleclick";
        /// <summary>
        /// 界面打开的事件
        /// </summary>
        //public const string initload = "initload";
        public const string keypress = "keypress";
        public const string textchanged = "textchanged";
        public const string checkedchanged = "checkedchanged";
        /// <summary>
        /// 选择事件，Tab控件、Combox控件
        /// </summary>
        //public const string selectedchanged = "selectedchanged";
        public const string selectedindexchanged = "selectedindexchanged";
        /// <summary>
        /// 树节点选中后事件
        /// </summary>
        public const string afterselect = "afterselect";
        /// <summary>
        /// 网格当前单元格改变事件
        /// </summary>
        public const string currentcellchanged = "currentcellchanged";
        /// <summary>
        /// 网格单元格值改变事件
        /// </summary>
        public const string cellvaluechanged = "cellvaluechanged";
        public const string CellFormatting = "CellFormatting";

        public static string GetEventName(string eventname)
        {
            switch (eventname)
            {
                case XMLLabelEvent.enter:
                    return "Enter";
                case XMLLabelEvent.leave:
                    return "Leave";
                case XMLLabelEvent.click:
                    return "Click";
                case XMLLabelEvent.doubleclick:
                    return "DoubleClick";
                case XMLLabelEvent.keypress:
                    return "KeyPress";
                case XMLLabelEvent.textchanged:
                    return "TextChanged";
                case XMLLabelEvent.checkedchanged:
                    return "CheckedChanged";
                case XMLLabelEvent.selectedindexchanged:
                    return "SelectedIndexChanged";
                case XMLLabelEvent.afterselect:
                    return "AfterSelect";
                case XMLLabelEvent.currentcellchanged:
                    return "CurrentCellChanged";
                case XMLLabelEvent.cellvaluechanged:
                    return "CellValueChanged";
                default:
                    return eventname;
            }
        }
    }
}
