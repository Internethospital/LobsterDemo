using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Debugger.Winform.FormDesign
{
    /// <summary>
    /// 加载XML代码
    /// </summary>
    public class DesignerLoaderEx : BasicDesignerLoader
    {
        private XmlDocument xmlDocument;
        private IComponent root;//根节点控件
        private DesignSurfaceEx designSurface;

        protected override void PerformFlush(IDesignerSerializationManager serializationManager)
        {
            //将设计器生成XML代码
            XmlDocument document = new XmlDocument();
            XmlDeclaration xmldecl=document.CreateXmlDeclaration("1.0", "utf-8",null);
            
            document.InsertBefore(xmldecl, document.DocumentElement);
            XmlElement viewform = document.CreateElement("viewform");
            document.AppendChild(viewform);

            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            root = idh.RootComponent;
            XmlNode node = WriteObject(document, root);

            if (node != null)
                document.DocumentElement.AppendChild(node);

            xmlDocument = document;
        }

        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            //将XML代码生成设计器界面
            string baseClassName = "Form1";
            bool successful = true;
            ArrayList errors = new ArrayList();

            if (xmlDocument == null)
            {
                this.LoaderHost.CreateComponent(typeof(Form));
            }
            else
            {
                XmlNode node = xmlDocument.DocumentElement.FirstChild;
                string nodename = node.Name;
                baseClassName = node.Attributes["name"].Value;
                object instance;
                if (nodename == "winform")
                {
                    instance= this.LoaderHost.CreateComponent(typeof(Form), baseClassName);
                }
                else
                {
                    instance= this.LoaderHost.CreateComponent(typeof(UserControl), baseClassName);
                }
                ReadProperty(node, instance);//加载属性
                ReadObject(node,instance);//加载子控件
            }

            IComponentChangeService cs = this.LoaderHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

            if (cs != null)
            {
                cs.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
                cs.ComponentAdded += new ComponentEventHandler(OnComponentAddedRemoved);
                cs.ComponentRemoved += new ComponentEventHandler(OnComponentAddedRemoved);

                OnComponentAddedRemoved(null, null);
            }

            this.LoaderHost.EndLoad(baseClassName, successful, errors);
        }

        public override void Dispose()
        {
            // Always remove attached event handlers in Dispose.
            IComponentChangeService cs = this.LoaderHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

            if (cs != null)
            {
                cs.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                cs.ComponentAdded -= new ComponentEventHandler(OnComponentAddedRemoved);
                cs.ComponentRemoved -= new ComponentEventHandler(OnComponentAddedRemoved);
            }
        }
        private void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
        {
            designSurface.ActionAddedRemovedComponent?.Invoke(designSurface.GetAllComponents());
        }

        private void OnComponentAddedRemoved(object sender, ComponentEventArgs ce)
        {
            designSurface.ActionAddedRemovedComponent?.Invoke(designSurface.GetAllComponents());
        }

        public DesignerLoaderEx(string xml,DesignSurfaceEx ds)
        {
            designSurface = ds;
            if (string.IsNullOrEmpty(xml) == false)
            {
                byte[] array = Encoding.UTF8.GetBytes(xml);
                MemoryStream _stream = new MemoryStream(array);
                StreamReader _reader = new StreamReader(_stream);

                XmlDocument document = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create(_reader, settings);
                document.Load(reader);

                xmlDocument = document;
            }
        }

        /// <summary>
        /// 切换为XML源代码的时候，根据设计器内容获取最新XML代码
        /// 保存的时候也是先调它获取XML代码
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            Flush();//更新XML

            if (xmlDocument == null) return "";

            StringWriter sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);

            xtw.Formatting = Formatting.Indented;
            xmlDocument.WriteTo(xtw);

            sw.Close();
            return sw.ToString();
        }

        public void LoadCode()
        {
            //this.Reload(ReloadOptions.NoFlush);
        }

        #region 生成XML代码
        private XmlNode WriteObject(XmlDocument document, object value)
        {
            if (value == null) return null;

            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            string nodeName = GetNodeName(value);
            if (nodeName == null) return null;

            XmlNode node = document.CreateElement(nodeName);
            //IComponent component = value as IComponent;

            //某些节点添加type属性
            string attrName = GetAttrTypeName(value);
            if (attrName != null)
            {
                XmlAttribute attr = document.CreateAttribute("type");
                attr.Value = attrName;
                node.Attributes.Append(attr);
            }

            if (nodeName == "tool")
            {
                if (value is ToolStrip)
                {
                    foreach (ToolStripItem child in ((ToolStrip)value).Items)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                else if (value is DevComponents.DotNetBar.Bar)
                {
                    foreach (DevComponents.DotNetBar.BaseItem child in ((DevComponents.DotNetBar.Bar)value).Items)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
            }
            else if (nodeName == "datagrid")
            {
                foreach (DataGridViewColumn child in ((DataGridView)value).Columns)
                {
                    XmlNode childnode = WriteObject(document, child);
                    if (childnode != null)
                        node.AppendChild(childnode);
                }
            }

            else if (nodeName == "tree")
            {
                #region tree
                if (value is TreeView)
                {
                    foreach (TreeNode child in ((TreeView)value).Nodes)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                else if (value is DevComponents.AdvTree.AdvTree)
                {
                    foreach (DevComponents.AdvTree.Node child in ((DevComponents.AdvTree.AdvTree)value).Nodes)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                #endregion
            }
            else if (nodeName == "node")
            {
                #region node
                if (value is TreeNode)
                {
                    foreach (TreeNode child in ((TreeNode)value).Nodes)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                else if (value is DevComponents.AdvTree.Node)
                {
                    foreach (DevComponents.AdvTree.Node child in ((DevComponents.AdvTree.Node)value).Nodes)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                #endregion
            }
            else if (nodeName == "tabcontrol")
            {
                #region tabcontrol
                if (value is DevComponents.DotNetBar.TabControl)
                {
                    foreach (DevComponents.DotNetBar.TabItem child in ((DevComponents.DotNetBar.TabControl)value).Tabs)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                else if (value is DevComponents.DotNetBar.SuperTabControl)
                {
                    foreach (DevComponents.DotNetBar.SuperTabItem child in ((DevComponents.DotNetBar.SuperTabControl)value).Tabs)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                else
                {
                    foreach (Control child in ((Control)value).Controls)
                    {
                        XmlNode childnode = WriteObject(document, child);
                        if (childnode != null)
                            node.AppendChild(childnode);
                    }
                }
                #endregion
            }
            else if (nodeName == "contextmenu")
            {
                foreach (ToolStripItem child in ((ContextMenuStrip)value).Items)
                {
                    XmlNode childnode = WriteObject(document, child);
                    if (childnode != null)
                        node.AppendChild(childnode);
                }
            }
            else if (nodeName == "menuitem")
            {
                foreach (ToolStripItem child in ((ToolStripMenuItem)value).DropDownItems)
                {
                    XmlNode childnode = WriteObject(document, child);
                    if (childnode != null)
                        node.AppendChild(childnode);
                }
            }

            else if (value is Control) // if is Control
            {
                #region Control
                foreach (Control child in ((Control)value).Controls)
                {
                    XmlNode childnode = WriteObject(document, child);
                    if (childnode != null)
                        node.AppendChild(childnode);
                }
                #endregion
            }
            //?右键菜单
            else if (value is MenuStrip)
            {
                foreach (ContextMenuStrip child in ((MenuStrip)value).Items)
                {

                }
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value, new Attribute[] { DesignOnlyAttribute.No });
            WriteProperties(document, properties, value, node);
            return node;
        }

        private void WriteProperties(XmlDocument document, PropertyDescriptorCollection properties, object value, XmlNode parent)
        {
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.ShouldSerializeValue(value))
                {
                    string attrname;
                    string attrvalue;

                    if (ExistAttribute(value, prop, out attrname, out attrvalue))
                    {
                        XmlAttribute attr = document.CreateAttribute(attrname);
                        attr.Value = attrvalue;
                        parent.Attributes.Append(attr);
                    }
                }
            }
        }

        private string GetNodeName(object value)
        {
            Type type = value.GetType();
            if (type == typeof(Form))
            {
                return "winform";
            }
            else if (type == typeof(UserControl))
            {
                return "panel";
            }

            ControlTypeObj ctobj = ControlTypeData.CTObjList.Find(x => x.ControlType == type);
            if (ctobj != null)
            {
                return ctobj.LabelName;
            }

            return null;
        }

        private string GetAttrTypeName(object value)
        {
            Type type = value.GetType();
            ControlTypeObj ctobj = ControlTypeData.CTObjList.Find(x => x.ControlType == type);
            if (ctobj != null)
            {
                return ctobj.TypeName;
            }

            return null;
        }

        private bool ExistAttribute(object value, PropertyDescriptor pro, out string attrname, out string attrvalue)
        {
            attrname = ControlTypeData.GetAttributeName(pro.Name);
            attrvalue = "";
            TypeConverter converter = TypeDescriptor.GetConverter(value);
            string labelName = GetNodeName(value);
            string typeName = GetAttrTypeName(value);
            switch (pro.Name)
            {
                case "Name":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "Text":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "TextAlign":
                    #region TextAlign
                    if (typeName == "textbox")
                    {
                        if ((int)pro.GetValue(value) == 0)
                            attrvalue = "left";
                        else if ((int)pro.GetValue(value) == 1)
                            attrvalue = "right";
                        else if ((int)pro.GetValue(value) == 2)
                            attrvalue = "center";
                    }
                    else
                    {
                        if ((int)pro.GetValue(value) == 1)
                            attrvalue = "topleft";
                        else if ((int)pro.GetValue(value) == 2)
                            attrvalue = "topcenter";
                        else if ((int)pro.GetValue(value) == 4)
                            attrvalue = "topright";
                        else if ((int)pro.GetValue(value) == 16)
                            attrvalue = "middleleft";
                        else if ((int)pro.GetValue(value) == 32)
                            attrvalue = "middlecenter";
                        else if ((int)pro.GetValue(value) == 64)
                            attrvalue = "middleright";
                        else if ((int)pro.GetValue(value) == 256)
                            attrvalue = "bottomleft";
                        else if ((int)pro.GetValue(value) == 512)
                            attrvalue = "bottomcenter";
                        else if ((int)pro.GetValue(value) == 1024)
                            attrvalue = "bottomright";
                    }
                    #endregion
                    return true;
                case "WatermarkText":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "Value":
                    attrvalue = Convert.ToDateTime(pro.GetValue(value)).ToString("yyyy-MM-dd HH:mm:ss");
                    return true;
                case "CustomFormat":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "Enabled":
                    attrvalue = pro.GetValue(value).ToString().ToLower();
                    return true;
                case "Location":
                    System.Drawing.Point pt = (System.Drawing.Point)pro.GetValue(value);
                    attrvalue = pt.X + "," + pt.Y;
                    return true;
                case "Size":
                    System.Drawing.Size _size = (System.Drawing.Size)pro.GetValue(value);
                    attrvalue = _size.Width + "," + _size.Height;
                    return true;
                case "Dock":
                    if ((int)pro.GetValue(value) == 0)
                        attrvalue = "none";
                    else if ((int)pro.GetValue(value) == 1)
                        attrvalue = "top";
                    else if ((int)pro.GetValue(value) == 2)
                        attrvalue = "bottom";
                    else if ((int)pro.GetValue(value) == 3)
                        attrvalue = "left";
                    else if ((int)pro.GetValue(value) == 4)
                        attrvalue = "right";
                    else if ((int)pro.GetValue(value) == 5)
                        attrvalue = "fill";
                    return true;
                case "Anchor":
                    attrvalue = (string)converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(string));
                    attrvalue = attrvalue.ToLower();
                    return true;
                case "Visible":
                    attrvalue = pro.GetValue(value).ToString().ToLower();
                    return true;
                case "Font":
                    System.Drawing.Font _font = (System.Drawing.Font)pro.GetValue(value);
                    attrvalue = _font.FontFamily.Name + "," + _font.Size;
                    return true;
                case "ForeColor":
                    System.Drawing.Color _color = (System.Drawing.Color)pro.GetValue(value);
                    attrvalue = System.Drawing.ColorTranslator.ToHtml(_color);
                    return true;
                case "ReadOnly":
                    attrvalue = pro.GetValue(value).ToString().ToLower();
                    return true;
                case "TabIndex":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "Multiline":
                    attrvalue = pro.GetValue(value).ToString().ToLower();
                    return true;
                case "SizeMode":
                    if ((int)pro.GetValue(value) == 0)
                        attrvalue = "normal";
                    else if ((int)pro.GetValue(value) == 1)
                        attrvalue = "stretchimage";
                    else if ((int)pro.GetValue(value) == 2)
                        attrvalue = "autosize";
                    else if ((int)pro.GetValue(value) == 3)
                        attrvalue = "centerimage";
                    else if ((int)pro.GetValue(value) == 4)
                        attrvalue = "zoom";
                    return true;
                case "HeaderText":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "DataPropertyName":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "MinimumWidth":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "Width":
                    attrvalue = pro.GetValue(value).ToString();
                    return true;
                case "AutoSizeMode":
                    if ((int)pro.GetValue(value) == 16)
                        attrvalue = "fill";
                    else
                        attrvalue = "notset";
                    return true;
                case "DisplayStyle":
                    if (typeName == "toolStripButton")
                    {
                        if ((int)pro.GetValue(value) == 2)
                            attrvalue = "image";
                        else if ((int)pro.GetValue(value) == 1)
                            attrvalue = "text";
                        else if ((int)pro.GetValue(value) == 3)
                            attrvalue = "imageandtext";
                    }
                    else if (typeName == "buttonItem")
                    {
                        if ((int)pro.GetValue(value) == 0)
                            attrvalue = "image";
                        else if ((int)pro.GetValue(value) == 1)
                            attrvalue = "text";
                        else if ((int)pro.GetValue(value) == 2)
                            attrvalue = "imageandtext";
                    }
                    return true;
                case "StartPosition":
                    if ((int)pro.GetValue(value) == 0)
                        attrvalue = "manual";
                    else if ((int)pro.GetValue(value) == 1)
                        attrvalue = "centerscreen";
                    else if ((int)pro.GetValue(value) == 2)
                        attrvalue = "windowsdefaultlocation";
                    else if ((int)pro.GetValue(value) == 3)
                        attrvalue = "windowsdefaultbounds";
                    else if ((int)pro.GetValue(value) == 4)
                        attrvalue = "centerparent";
                    return true;
                case "WindowState":
                    if ((int)pro.GetValue(value) == 0)
                        attrvalue = "normal";
                    else if ((int)pro.GetValue(value) == 1)
                        attrvalue = "minimized";
                    else if ((int)pro.GetValue(value) == 2)
                        attrvalue = "maximized";
                    return true;
                case "MaximizeBox":
                    attrvalue = pro.GetValue(value).ToString().ToLower();
                    return true;
                case "MinimizeBox":
                    attrvalue = pro.GetValue(value).ToString().ToLower();
                    return true;
                default:
                    attrvalue = (string)converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(string));
                    return false;
            }

            return false;
        }
        #endregion

        #region XML代码生成界面
        private Type GetTypeByNode(XmlNode node)
        {
            XmlAttribute typeAttr = node.Attributes["type"];
            if (typeAttr != null)
            {
                return ControlTypeData.CTObjList.Find(x => x.TypeName == typeAttr.Value).ControlType;
            }

            return ControlTypeData.CTObjList.Find(x => x.LabelName == node.Name).ControlType;
        }

        private void ReadObject(XmlNode node,object parent)
        {
            IList childList = null;
            if (node.Name == "tool")//工具栏
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Items"];
                childList = childProp.GetValue(parent) as IList;
                if (parent is DevComponents.DotNetBar.Bar)
                {
                    (parent as DevComponents.DotNetBar.Bar).Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                }
            }
            else if (node.Name == "datagrid")
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Columns"];
                childList = childProp.GetValue(parent) as IList;
            }

            else if (node.Name == "tabcontrol")
            {
                if (parent is TabControl)
                {
                    PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Controls"];
                    childList = childProp.GetValue(parent) as IList;
                }
                else if (parent is DevComponents.DotNetBar.TabControl)
                {
                    (parent as DevComponents.DotNetBar.TabControl).CanReorderTabs = true;
                    (parent as DevComponents.DotNetBar.TabControl).Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
                    (parent as DevComponents.DotNetBar.TabControl).TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;

                    PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Tabs"];
                    childList = childProp.GetValue(parent) as IList;
                }
                else
                {
                    PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Tabs"];
                    childList = childProp.GetValue(parent) as IList;
                }
            }
            else if (node.Name == "tree")
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Nodes"];
                childList = childProp.GetValue(parent) as IList;
            }
            else if (node.Name == "node")
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Nodes"];
                childList = childProp.GetValue(parent) as IList;
            }
            else if (node.Name == "contextmenu")
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Items"];
                childList = childProp.GetValue(parent) as IList;
            }
            else if (node.Name == "menuitem")
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["DropDownItems"];
                childList = childProp.GetValue(parent) as IList;
            }
            else if (parent is Control)
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(parent)["Controls"];
                childList = childProp.GetValue(parent) as IList;
            }

            foreach (XmlNode item in node.ChildNodes)
            {
                Type type = GetTypeByNode(item);
                object instance;
                //创建控件
                if (typeof(IComponent).IsAssignableFrom(type))
                {
                    XmlAttribute nameAttr = item.Attributes["name"];
                    if (nameAttr == null)
                    {
                        instance = this.LoaderHost.CreateComponent(type);
                    }
                    else
                    {
                        instance = this.LoaderHost.CreateComponent(type, nameAttr.Value);
                    }
                }
                else
                {
                    instance = Activator.CreateInstance(type);
                }

                childList.Add(instance);
                ReadProperty(item, instance);
                //递归创建控件
                ReadObject(item,instance);
            }
        }

        private void ReadProperty(XmlNode node, object instance)
        {
            //Dictionary<string, string> _attributeDic = new Dictionary<string, string>();
            foreach (XmlAttribute item in node.Attributes)
            {
                if (item.Name == "type" || item.Name == "name")
                    continue;
                //_attributeDic.Add(item.Name, item.Value);
                string proName = ControlTypeData.GetPropertyName(item.Name);
                PropertyDescriptor prop = TypeDescriptor.GetProperties(instance)[proName];
                if (prop != null)
                {
                    object value=null;
                    value = ReadValue(item.Name, item.Value,instance);
                    try
                    {
                        prop.SetValue(instance, Convert.ChangeType(value, prop.PropertyType));
                    }
                    catch (Exception e)
                    {
                        prop.SetValue(instance, value);
                    }
                }
            }


        }

        private object ReadValue(string attrname,string value, object instance)
        {
            object propertyValue = value;
            switch (attrname)
            {
                case "name":
                case "text":
                case "headertext":
                case "datapropertyname":
                case "miniwidth":
                case "width":
                    propertyValue = value.ToString();
                    break;
                case "visible":
                case "enabled":
                case "_readonly":
                case "maximizebox":
                case "minimizebox":
                case "multiline"://文本行支持多行
                case "multiselect":
                    propertyValue = value.ToString() == "true" ? true : false;
                    break;
                case "textalign"://有问题，button 和textbox 不一样
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
                case "location":
                    System.Drawing.Point _location = new System.Drawing.Point();
                    _location.X = Convert.ToInt32(value.ToString().Split(',')[0]);
                    _location.Y = Convert.ToInt32(value.ToString().Split(',')[1]);
                    propertyValue = _location;
                    break;
                case "size":
                    System.Drawing.Size _size = new System.Drawing.Size();
                    _size.Width = Convert.ToInt32(value.ToString().Split(',')[0]);
                    _size.Height = Convert.ToInt32(value.ToString().Split(',')[1]);
                    propertyValue = _size;
                    break;
                case "dock":
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
                case "anchor":
                    string[] str = value.ToString().Split(',');
                    AnchorStyles left = str.ToList().FindIndex(x => x.ToLower().Trim() == "left") != -1 ? AnchorStyles.Left : AnchorStyles.None;
                    AnchorStyles right = str.ToList().FindIndex(x => x.ToLower().Trim() == "right") != -1 ? AnchorStyles.Right : AnchorStyles.None;
                    AnchorStyles top = str.ToList().FindIndex(x => x.ToLower().Trim() == "top") != -1 ? AnchorStyles.Top : AnchorStyles.None;
                    AnchorStyles bottom = str.ToList().FindIndex(x => x.ToLower().Trim() == "bottom") != -1 ? AnchorStyles.Bottom : AnchorStyles.None;
                    propertyValue = (AnchorStyles)(((left | right) | top) | bottom);
                    break;

                case "font":
                    string familyName = value.ToString().Split(',')[0];
                    float emSize = Convert.ToSingle(value.ToString().Split(',')[1]);
                    propertyValue = new System.Drawing.Font(familyName, emSize);
                    break;
                case "fontcolor":
                    ColorConverter converter = new ColorConverter();
                    propertyValue = (Color)converter.ConvertFromString(value.ToString());
                    break;
                case "customformat":
                    propertyValue = value.ToString();
                    break;
                case "value":
                    propertyValue = Convert.ToDateTime(value);
                    break;
                case "startposition":
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
                case "windowstate":
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
                case "imageno":
                    propertyValue = (value);//通过图标编码获取Image对象
                    break;
                case "sizemode":
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
                case "autosizemode":
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
                case "displaystyle":
                    if (instance is ToolStripButton)//Winform默认工具栏按钮
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
                    else if (instance is DevComponents.DotNetBar.ButtonItem)//Donetbar控件的工具栏按钮
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
                case "contextmenu"://通过右键菜单的名称绑定菜单控件

                    break;
            }

            return propertyValue;
        }

        #endregion
    }
}
