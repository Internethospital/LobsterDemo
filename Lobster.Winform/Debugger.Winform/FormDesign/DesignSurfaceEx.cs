using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.FormDesign
{
    public class DesignSurfaceEx: DesignSurface
    {
        ISelectionService _selectionService;
        IDesignerHost _designerHost;

        public Action<List<object>> ActionSelectedComponents { get; set; }
        public Action<List<IComponent>> ActionAddedRemovedComponent { get; set; }
        public DesignerLoaderEx Loader { get; set; }

        public DesignSurfaceEx():base()
        {
            //cut copy paste 三个命令无法执行
            this.ServiceContainer.AddService(typeof(IMenuCommandService), new MenuCommandService(this));
        }

        /// <summary>
        /// 创建窗体设计器
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="type"></param>
        /// <param name="xml">界面XML</param>
        public void CreateDesign(Control parent, IToolboxService toolbox, string xml)
        {
            //解决新增的控件Name不为空
            this.ServiceContainer.AddService(typeof(INameCreationService), new NameCreationService());
            //解决设计器支持Toolbox控件的拖拽新增控件
            this.ServiceContainer.AddService(typeof(IToolboxService), toolbox);

            //先清空
            parent.Controls.Clear();

            DesignerLoaderEx loader = new DesignerLoaderEx(xml,this);
            this.BeginLoad(loader);
            Loader = loader;
            // Get the View of the DesignSurface, host it in a form, and show it
            Control cDesign = this.View as Control;

            cDesign.Parent = parent;
            cDesign.Dock = DockStyle.Fill;

            _designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
            _selectionService = (ISelectionService)(this.GetService(typeof(ISelectionService)));
            _selectionService.SelectionChanged += selectionService_SelectionChanged;

            _designerHost.Activate();
            (toolbox as Toolbox).DesignerHost = _designerHost;

            List<object> comps = new List<object>();
            comps.Add(_designerHost.RootComponent);
            ActionSelectedComponents(comps);
        }

        private void selectionService_SelectionChanged(object sender, EventArgs e)
        {
            if (_selectionService != null)
            {
                ICollection selectedComponents = _selectionService.GetSelectedComponents();

                List<object> comps = new List<object>();

                foreach (Object o in selectedComponents)
                {
                    comps.Add(o);
                }

                if (ActionSelectedComponents != null && comps.Count>0)
                {
                    ActionSelectedComponents(comps);
                }

                //propertyGrid1.SelectedObjects = comps.ToArray();
                
            }
        }

        /// <summary>
        /// 获取所有组件
        /// </summary>
        /// <returns></returns>
        public List<IComponent> GetAllComponents()
        {
            List<IComponent> clist = new List<IComponent>();
            foreach (IComponent comp in this.ComponentContainer.Components)
            {
                clist.Add(comp);
            }
            return clist;
        }

        /// <summary>
        /// 设置选定组件
        /// </summary>
        /// <param name="components"></param>
        public void SetSelectedComponents(ICollection components)
        {

        }

        /// <summary>
        /// 添加控件到设计器
        /// </summary>
        /// <param name="control"></param>
        public void AddComponent(Type control)
        {
            IToolboxUser tbu = _designerHost.GetDesigner(_designerHost.RootComponent as IComponent) as IToolboxUser;
            if (tbu != null)
            {
                System.Drawing.Design.ToolboxItem tbi = new System.Drawing.Design.ToolboxItem(control);
                tbu.ToolPicked(tbi);
            }
        }

        public IDesignerHost GetDesignerHost()
        {
            return _designerHost;
        }
        /// <summary>
        /// 执行菜单命令
        /// </summary>
        /// <param name="commandID"></param>
        public void GlobalInvoke(CommandID commandID)
        {
            IMenuCommandService ims = this.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
            ims.GlobalInvoke(commandID);
        }
    }

    public enum DesignType
    {
        Form, UserControl
    }
}
