using efwplusWinform.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 渲染引擎
    /// </summary>
    public class RenderEngine
    {
        /// <summary>
        /// 渲染后界面的所有控件
        /// </summary>
        public List<RenderObject> AllRenderObjList { get; set; }
        
        /// <summary>
        /// 根控件
        /// </summary>
        public Control RootControl { get; set; }
        /// <summary>
        /// 根控件对应的XML文档
        /// </summary>
        public XmlDocument RootControlXmlDoc { get; set; }
        /// <summary>
        /// 子控件和XML文档
        /// </summary>
        public Dictionary<Control, XmlDocument> ControlXmlDic { get; set; }

        /// <summary>
        /// 渲染模式
        /// </summary>
        public RenderMode RMode { get; set; }
        /// <summary>
        /// 只有两种情况，渲染整个界面或者渲染界面中的某几个子控件容器
        /// </summary>
        /// <param name="rootcontrol"></param>
        /// <param name="xmldoc"></param>
        /// <param name="controlXmlDic"></param>
        /// <param name="mode"></param>
        public RenderEngine(Control rootcontrol, XmlDocument xmldoc, Dictionary<Control, XmlDocument> controlXmlDic, RenderMode mode)
        {
            
            RootControl = rootcontrol;
            RootControlXmlDoc = xmldoc;
            ControlXmlDic = controlXmlDic;
            RMode = mode;
        }

        /// <summary>
        /// 执行渲染
        /// </summary>
        public void ExecuteRender()
        {
            AllRenderObjList = new List<RenderObject>();
            LoadRootControlAllFieldInfo();

            if (RootControlXmlDoc != null)//渲染整个界面
            {
                List<RenderObject> list = new List<RenderObject>();
                XmlNode node = RootControlXmlDoc.DocumentElement.FirstChild;
                RenderObject robj = FactoryCreateRenderObject(RootControl, node);
                list.Add(robj);//创建根容器
                if (RootControl == null)
                {
                    RootControl = robj.Control as Control;
                }
                GetXmlNode(node, robj, list);
                //
                InitializeComponent(list);

                
            }
            else if(RootControl!=null && ControlXmlDic != null)//渲染界面中的某几个子控件容器
            {
                //循环ControlXmlDic渲染局部界面
                foreach (var item in ControlXmlDic)
                {
                    List<RenderObject> list = new List<RenderObject>();

                    XmlNode node = item.Value.DocumentElement.FirstChild;
                    RenderObject robj = FactoryCreateRenderObject(item.Key, node);
                    list.Add(robj);
                    GetXmlNode(node, robj,list);
                    //
                    InitializeComponent(list);

                }

            }
            else
            {
                throw new Exception("构造函数参数有误");
            }

            //将RootControl添加到AllRenderObjList中
            AllRenderObjList.Add(new RenderObject(RootControl));
        }

        private void LoadRootControlAllFieldInfo()
        {
            //先把RootControl中的所有控件都添加到AllrenderObjList中
            //这种方式不行，动态创建的控件获取不到
            List<System.Reflection.FieldInfo> fieldInfo = RootControl.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).ToList();
            foreach (var field in fieldInfo)
            {
                Object ctrl = field.GetValue(RootControl);
                if(ctrl!=null && (ctrl is System.ComponentModel.IComponent))
                AllRenderObjList.Add(new RenderObject(ctrl));
            }
        }

        private object GetRenderControl(XmlNode node)
        {
            if (node.Attributes["name"] == null) return null;
            string ctrlname = node.Attributes["name"].Value;

            RenderObject robj= AllRenderObjList.Find(x => x.ControlName == ctrlname);
            if (robj != null)
            {
                return robj.Control;
            }
            //反射  
            //List<System.Reflection.FieldInfo> fieldInfo = RootControl.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).ToList();
            //System.Reflection.FieldInfo field = fieldInfo.Find(x => x.Name == ctrlname);
            //if (field != null)
            //{
            //    return field.GetValue(RootControl);
            //}
            return null;
        }
        private void GetXmlNode(XmlNode node, RenderObject robj, List<RenderObject> list)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                object renderCtrl = GetRenderControl(item);//控件是否已存在
                RenderObject _robj = FactoryCreateRenderObject(renderCtrl, item);
                list.Add(_robj);
                robj.AddChildControlName(_robj.ControlName);//增加控件对象的子控件名称
                if (renderCtrl == null)
                {
                    //新创建的控件添加到集合
                    AllRenderObjList.Add(_robj);
                }

                GetXmlNode(item, _robj, list);
            }
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitializeComponent(List<RenderObject> list)
        {
            //Dispose(list);
            //RootControl.Controls.Clear();
            if (list.Count > 0)
            {
                (list[0].Control as Control).Controls.Clear();
            }

            //list.Reverse();//倒序
            //挂起布局逻辑，由里至外
            foreach (RenderObject ro in list)
            {
                if (ro.IsSuspendLayout)
                {
                    (ro.Control as Control).SuspendLayout();
                }
            }
            //list.Reverse();//还原为正序
            //设置控件属性
            foreach (RenderObject ro in list)
            {
                ro.InitControlAttribute(list);
            }
            //list.Reverse();//再倒序
            //恢复布局逻辑
            foreach (RenderObject ro in list)
            {
                if (ro.IsSuspendLayout)
                {
                    (ro.Control as Control).ResumeLayout(false);
                    (ro.Control as Control).PerformLayout();
                }
            }
        }

        /// <summary>
        /// 清空根控件内的所有控件
        /// </summary>
        private void Dispose(List<RenderObject> list)
        {
            foreach (RenderObject ro in list)
            {
                ro.Dispose();
            }
        }
        /// <summary>
        /// 创建渲染控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private RenderObject FactoryCreateRenderObject(object control, XmlNode node)
        {
            switch (node.Name)
            {
                case XMLLabel.winform:
                    return new RenderWinform(control, node, RMode);
                case XMLLabel.panel:
                    return new RenderPanel(control, node, RMode);
                case XMLLabel.tree:
                    return new RenderTree(control, node, RMode);
                case XMLLabel.node:
                    return new RenderNode(control, node, RMode);
                case XMLLabel.tabcontrol:
                    return new RenderTabControl(control, node, RMode);
                case XMLLabel.tabitem:
                    return new RenderTabItem(control, node, RMode);
                case XMLLabel.datagrid:
                    return new RenderDataGrid(control, node, RMode);
                case XMLLabel.column:
                    return new RenderColumn(control, node, RMode);
                case XMLLabel.contextmenu:
                    return new RenderContextMenu(control, node, RMode);
                case XMLLabel.menuitem:
                    return new RenderMenuItem(control, node, RMode);
                case XMLLabel.tool:
                    return new RenderTool(control, node, RMode);
                case XMLLabel.toolitem:
                    return new RenderToolItem(control, node, RMode);
                case XMLLabel.label:
                    return new RenderLabel(control, node, RMode);
                case XMLLabel.input:
                    return new RenderInput(control, node, RMode);
                case XMLLabel.button:
                    return new RenderButton(control, node, RMode);
                case XMLLabel.picturebox:
                    return new RenderPictureBox(control, node, RMode);

            }
            return null;
        }

        #region 提供给Python调用的方法

        

        /// <summary>
        /// 根据控件名称获取界面控件对象
        /// </summary>
        /// <param name="controlname"></param>
        /// <returns></returns>
        public Object getbyname(string controlname)
        {
            return AllRenderObjList.Find(x => x.ControlName == controlname);
        }

        public void setattr(string controlname,string attrname,object value)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == controlname);
            if (ro != null)
            {
                ro.SetControlAttributeValue(attrname, value);
            }
        }

        public object getattr(string controlname, string attrname)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == controlname);
            if (ro != null)
            {
                return ro.GetControlAttributeValue(attrname);
            }

            return null;
        }

        public void bind(string controlname,string eventname,Action<object, object> eventfun)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == controlname);
            ro.AddControlEvent(eventname, eventfun);
        }

        public void unbind(string controlname, string eventname)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == controlname);
            ro.RemoveControlEvent(eventname);
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons">yesno/ok</param>
        /// <param name="icon">info/error/question</param>
        /// <returns>OK = 1/Yes = 6/No = 7</returns>
        public int msgbox(string text, string caption, string buttons, string icon)
        {
            MessageBoxButtons btn = (buttons == "yesno" ? MessageBoxButtons.YesNo : MessageBoxButtons.OK);
            MessageBoxIcon bi = MessageBoxIcon.Information;
            if (icon.Equals("error"))
                bi = MessageBoxIcon.Error;
            else if (icon.Equals("question"))
                bi = MessageBoxIcon.Question;
            return (int)MessageBox.Show(text, caption, btn, bi);
        }
        
        public void msgboxsimple(string text)
        {
            if(RootControl is BaseFormBusiness)
            {
                (RootControl as BaseFormBusiness).MessageBoxShowSimple(text);
            }
        }

        public void bindgridselectedindex(string gridname)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == gridname);
            if (ro != null)
            {
                if (RootControl is BaseFormBusiness && ro.Control is DataGridView)
                {
                    (RootControl as BaseFormBusiness).bindGridSelectIndex((ro.Control as DataGridView));
                }
            }
        }

        public void setgridselectedindex(string gridname)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == gridname);
            if (ro != null)
            {
                if (RootControl is BaseFormBusiness && ro.Control is DataGridView)
                {
                    (RootControl as BaseFormBusiness).setGridSelectIndex((ro.Control as DataGridView));
                }
            }
        }

        public void setgridselectedindex(string gridname, int index)
        {
            RenderObject ro = AllRenderObjList.Find(x => x.ControlName == gridname);
            if (ro != null)
            {
                if (RootControl is BaseFormBusiness && ro.Control is DataGridView)
                {
                    (RootControl as BaseFormBusiness).setGridSelectIndex((ro.Control as DataGridView),index);
                }
            }
        }
        /// <summary>
        /// 异步执行函数
        /// </summary>
        /// <param name="beginInvoke"></param>
        /// <param name="endInvoke"></param>
        public void asyninvokedfun(Func<Object> beginInvoke, Action<Object> endInvoke)
        {
            if (RootControl is BaseFormBusiness)
            {
                (RootControl as BaseFormBusiness).AsynInvoked(beginInvoke, endInvoke);
            }
        }

        /// <summary>
        /// 界面初始化加载
        /// </summary>
        /// <param name="fun"></param>
        public void initload(Action fun)
        {
            if (RootControl is BaseFormBusiness && fun != null)
            {
                BaseFormBusiness baseform = (RootControl as BaseFormBusiness);
                baseform.Load += new EventHandler((object sender, EventArgs e) =>
                {

                    fun();
                });

                baseform.OpenWindowBefore += new EventHandler((object sender, EventArgs e) =>
                 {
                     fun();
                 });
            }
        }
        #endregion
    }
    /// <summary>
    /// 渲染模式
    /// </summary>
    public enum RenderMode
    {
        /// <summary>
        /// 设计模式
        /// </summary>
        design = 0,
        /// <summary>
        /// 运行模式
        /// </summary>
        run = 1
    }
}
