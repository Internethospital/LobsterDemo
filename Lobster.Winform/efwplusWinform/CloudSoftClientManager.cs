
using efwplusWinform.Business;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Common;
using efwplusWinform.Controller;
using efwplusWinform.ViewRender;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform
{
    /// <summary>
    /// 云软件管理者
    /// </summary>
    public static class CloudSoftClientManager
    {
        /// <summary>
        /// 云软件集合
        /// </summary>
        public static List<CloudSoftClient> CSoftClientList;
        /// <summary>
        /// 默认云软件名称
        /// </summary>
        public static readonly string defaultSoftName = "default";
        public static readonly string csoftconfigFile = AppDomain.CurrentDomain.BaseDirectory+ "Config/CloudSoftConfig.xml";

        /// <summary>
        /// 初始化加载云软件
        /// </summary>
        public static void InitCloudSoft()
        {
            CSoftClientList = new List<CloudSoftClient>();
            //1.加载默认云软件
            CloudSoftClient defaultClient = LoadDefaultCSoftConfig();
            CSoftClientList.Add(defaultClient);

            //2.加载配置文件中的云软件
            List<CloudSoftClient> _configList = LoadCSoftConfig();
            CSoftClientList.AddRange(_configList);
            //3.移除调用默认云软件中已经放入配置云软件中的内容，也就是AssemblyList和controllerInfoList
            foreach(CloudSoftClient c in _configList)
            {
                foreach(var cinfo in c.controllerInfoList)
                {
                    string dllname = cinfo.DllName;
                    string typename = cinfo.TypeName;
                    string cname = cinfo.controllerName;
                    int index = defaultClient.controllerInfoList.FindIndex(x => x.DllName == dllname && x.TypeName == typename && x.controllerName == cname);
                    if ( index> -1)
                    {
                        defaultClient.controllerInfoList.RemoveAt(index);
                    }
                }
            }
        }

        /// <summary>
        /// 重新加载云软件，提供给重新登录后调用
        /// </summary>
        /// <param name="reservedcontroller">需要保留的控制器对象</param>
        public static void ReLoadCloudSoft(params WinformController[] reservedcontroller)
        {
            //1.从升级服务器下载最新版本的云软件，只包含脚本文件

            //2.初始化加载云软件
            InitCloudSoft();
            //3.将需要保留的控制器对象重新加入
            foreach(var c in reservedcontroller)
            {
                CloudSoftClient client= CSoftClientList.Find(x => x.SoftName == c.SoftName);
                if (client != null)
                {
                    client.WinformControllerObjDic.Add(c.ControllerId, c);
                }
            }
            //foreach (var c in CSoftClientList)
            //{
            //    foreach (var k in c.WinformControllerObjDic.Keys.ToList())
            //    {
            //        if (c.WinformControllerObjDic[k] != controller)
            //        {
            //            c.WinformControllerObjDic.Remove(k);
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 加载默认云软件
        /// </summary>
        private static CloudSoftClient LoadDefaultCSoftConfig()
        {
            CloudSoftClient csClient = new CloudSoftClient();
            csClient.SoftName = defaultSoftName;
            csClient.SoftTitle = "默认软件模块";
            csClient.Version = "1.0";
            csClient.Author = "kakake";
            csClient.Path = "";
            csClient.BaseInfo = new Dictionary<string, string>();
            csClient.AssemblyList = new List<string[]>();
            csClient.controllerInfoList = new List<WinformControllerInfo>();
            csClient.fileList = new List<string[]>();
            csClient.WinformControllerObjDic = new Dictionary<string, WinformController>();

            //1.装载WinAssembly中的所有程序集
            string WinAssembly_path = @"WinAssembly";
            DirectoryInfo Dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + WinAssembly_path);
            if (Dir.Exists)
            {
                FileInfo[] dlls = Dir.GetFiles("*.dll", SearchOption.AllDirectories);
                foreach (var i in dlls)
                {
                    csClient.AssemblyList.Add(new string[] { i.Name.Replace(".dll", ""), WinAssembly_path + "/" + i.Name });
                }
            }
            #region //2.装载controllerInfoList
            for (int k = 0; k < csClient.AssemblyList.Count; k++)
            {
                System.Reflection.Assembly assembly = Assembly.Load(csClient.AssemblyList[k][0]);
                Type[] types = assembly.GetTypes();
                for (int i = 0; i < types.Length; i++)
                {
                    WinformControllerInfo wcattr = GetWinformControllerInfo(types[i], assembly);
                    if (wcattr != null)
                    {
                        csClient.controllerInfoList.Add(wcattr);
                    }
                }
            }
            #endregion

            //3.无

            return csClient;
        }
        /// <summary>
        /// 加载CloudSoftConfig.xml
        /// </summary>
        /// <returns></returns>
        private static List<CloudSoftClient> LoadCSoftConfig()
        {
            List<CloudSoftClient> csClientList = new List<CloudSoftClient>();

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(csoftconfigFile);

            List<string> configList = new List<string>();
            XmlNodeList xnList= xmlDoc.DocumentElement.SelectNodes("cloudsoftClient");
            foreach (XmlNode n in xnList)
            {
                configList.Add(n.Attributes["path"].Value);
            }

            foreach (string path in configList)
            {
                string configxml = AppDomain.CurrentDomain.BaseDirectory + path;
                if (File.Exists(configxml) == false)
                {
                    //路径不存在则执行下一个
                    continue;
                }

                System.Xml.XmlDocument xd_client = new System.Xml.XmlDocument();
                xd_client.Load(configxml);
                if (xd_client == null)
                {
                    //配置文件有误则执行下一个
                    continue;
                }


                XmlNode xn_cloudsoft = xd_client.DocumentElement.SelectSingleNode("cloudsoft");
                if (xn_cloudsoft == null)
                {
                    //找不到节点则执行下一个
                    continue;
                }

                CloudSoftClient client = new CloudSoftClient();
                if (xn_cloudsoft.Attributes["name"] != null)
                    client.SoftName = xn_cloudsoft.Attributes["name"].Value;
                if (xn_cloudsoft.Attributes["title"] != null)
                    client.SoftTitle = xn_cloudsoft.Attributes["title"].Value;
                if (xn_cloudsoft.Attributes["version"] != null)
                    client.Version = xn_cloudsoft.Attributes["version"].Value;
                if (xn_cloudsoft.Attributes["author"] != null)
                    client.Author = xn_cloudsoft.Attributes["author"].Value;
                client.Path = path;
                client.BaseInfo = new Dictionary<string, string>();
                client.AssemblyList = new List<string[]>();
                client.fileList = new List<string[]>();
                client.controllerInfoList = new List<WinformControllerInfo>();
                client.WinformControllerObjDic = new Dictionary<string, WinformController>();

                //加载节点baseinfo
                XmlNode xn_baseinfo = xd_client.DocumentElement.SelectSingleNode("cloudsoft/baseinfo");
                if (xn_baseinfo != null)
                {
                    foreach(XmlNode n in xn_baseinfo.ChildNodes)
                    {
                        if(n.Attributes["key"]!=null && n.Attributes["value"] != null)
                        {
                            client.BaseInfo.Add(n.Attributes["key"].Value, n.Attributes["value"].Value);
                        }
                    }
                }
                //加载节点AssemblyList
                XmlNode xn_AssemblyList = xd_client.DocumentElement.SelectSingleNode("cloudsoft/AssemblyList");
                if (xn_AssemblyList != null)
                {
                    foreach (XmlNode n in xn_AssemblyList.ChildNodes)
                    {
                        if (n.Attributes["name"] != null && n.Attributes["path"] != null)
                        {
                            client.AssemblyList.Add(new string[] { n.Attributes["name"].Value, n.Attributes["path"].Value });
                        }
                    }
                }
                //加载节点fileList
                
                XmlNode xn_fileList = xd_client.DocumentElement.SelectSingleNode("cloudsoft/fileList");
                if (xn_fileList != null)
                {
                    foreach (XmlNode n in xn_fileList.ChildNodes)
                    {
                        if (n.Attributes["path"] != null)
                        {
                            client.fileList.Add(new string[] {n.Attributes["path"].Value });
                        }
                    }
                }
                //加载节点controllerList
               
                //1.先把AssemblyList程序集中的控制器加载，因为云软件中的控制器包括纯C#代码控制器、脚本代码控制器和混合控制器
                for (int k = 0; k < client.AssemblyList.Count; k++)
                {
                    System.Reflection.Assembly assembly = Assembly.Load(client.AssemblyList[k][0]);
                    Type[] types = assembly.GetTypes();
                    for (int i = 0; i < types.Length; i++)
                    {
                        WinformControllerInfo wcattr = GetWinformControllerInfo(types[i], assembly);
                        if (wcattr != null)
                        {
                            client.controllerInfoList.Add(wcattr);
                        }
                    }
                }
                //2.加载config.xml文件中的controllerList
                XmlNode xn_controllerList = xd_client.DocumentElement.SelectSingleNode("cloudsoft/controllerList");
                if (xn_controllerList != null)
                {
                    foreach (XmlNode wc in xn_controllerList.ChildNodes)
                    {
                        if (wc.Attributes["ControllerName"] == null)
                            continue;

                        WinformControllerInfo cmdC= client.controllerInfoList.Find(x => x.controllerName == wc.Attributes["ControllerName"].Value);
                        if (cmdC == null)
                        {
                            cmdC = new WinformControllerInfo();
                            cmdC.controllerName = wc.Attributes["ControllerName"].Value;
                            if (wc.Attributes["DefaultViewName"] != null)
                                cmdC.defaultViewName = wc.Attributes["DefaultViewName"].Value;
                            cmdC.MethodList = new List<WinformMethodInfo>();
                            cmdC.ViewList = new List<WinformViewInfo>();
                            if (wc.Attributes["Memo"] != null)
                                cmdC.Memo = wc.Attributes["Memo"].Value;
                            if (wc.Attributes["ScriptFile"] != null)
                                cmdC.ScriptFile = wc.Attributes["ScriptFile"].Value;
                            if (wc.Attributes["DllName"] != null)
                                cmdC.DllName = wc.Attributes["DllName"].Value;
                            if (wc.Attributes["TypeName"] != null)
                                cmdC.TypeName = wc.Attributes["TypeName"].Value;
                            if (cmdC.DllName != null && cmdC.TypeName != null)
                                cmdC.winformController = Assembly.Load(cmdC.DllName.Replace(".dll", "")).GetType(cmdC.TypeName, false, true);

                            client.controllerInfoList.Add(cmdC);
                        }
                        else
                        {
                            if (wc.Attributes["DefaultViewName"] != null)
                                cmdC.defaultViewName = wc.Attributes["DefaultViewName"].Value;
                            if (wc.Attributes["Memo"] != null)
                                cmdC.Memo = wc.Attributes["Memo"].Value;
                            if (wc.Attributes["ScriptFile"] != null)
                                cmdC.ScriptFile = wc.Attributes["ScriptFile"].Value;
                        }

                        //处理MethodList
                        if (cmdC.winformController != null)
                        {
                            MethodInfo[] property = cmdC.winformController.GetMethods();
                            for (int n = 0; n < property.Length; n++)
                            {
                                WinformMethodAttribute[] WinM = (WinformMethodAttribute[])property[n].GetCustomAttributes(typeof(WinformMethodAttribute), true);
                                if (WinM.Length > 0)
                                {
                                    WinformMethodInfo cmdM = new WinformMethodInfo();
                                    cmdM.methodName = property[n].Name;
                                    cmdM.methodInfo = property[n];
                                    cmdC.MethodList.Add(cmdM);
                                }
                            }
                        }



                        //处理ViewList
                        foreach (XmlNode wv in wc.ChildNodes)
                        {
                            if (wv.Attributes["Name"] == null)
                                continue;

                            WinformViewInfo winView = cmdC.ViewList.Find(x => x.Name == wv.Attributes["Name"].Value);
                            if (winView == null)
                            {
                                winView = new WinformViewInfo();
                                winView.Name = wv.Attributes["Name"].Value;
                                if (wv.Attributes["DllName"] != null)
                                    winView.DllName = wv.Attributes["DllName"].Value;
                                if (wv.Attributes["ViewTypeName"] != null)
                                    winView.ViewTypeName = wv.Attributes["ViewTypeName"].Value;
                                winView.IsDefaultView = winView.Name == cmdC.defaultViewName ? true : false;
                                if (winView.DllName != null && winView.ViewTypeName != null)
                                    winView.ViewType = Assembly.Load(winView.DllName.Replace(".dll", "")).GetType(winView.ViewTypeName, false, true);
                                if (wv.Attributes["Memo"] != null)
                                    winView.Memo = wv.Attributes["Memo"].Value;
                                if (wv.Attributes["ViewFile"] != null)
                                    winView.ViewFile = wv.Attributes["ViewFile"].Value;
                                cmdC.ViewList.Add(winView);
                            }
                            else
                            {
                                if (wv.Attributes["Memo"] != null)
                                    winView.Memo = wv.Attributes["Memo"].Value;
                                if (wv.Attributes["ViewFile"] != null)
                                    winView.ViewFile = wv.Attributes["ViewFile"].Value;
                            }
                        }
                    }
                }

                csClientList.Add(client);
            }

            return csClientList;
        }

        /// <summary>
        /// 获取控制器标签配置信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static WinformControllerInfo GetWinformControllerInfo(Type type,Assembly assembly)
        {
            WinformControllerAttribute[] winC = ((WinformControllerAttribute[])type.GetCustomAttributes(typeof(WinformControllerAttribute), true));

            if (winC.Length > 0)
            {
                WinformControllerInfo cmdC = new WinformControllerInfo();
                cmdC.controllerName = type.Name;
                cmdC.defaultViewName = winC[0].DefaultViewName;
                cmdC.winformController = type;
                cmdC.MethodList = new List<WinformMethodInfo>();
                cmdC.ViewList = new List<WinformViewInfo>();
                cmdC.Memo = winC[0].Memo;
                cmdC.ScriptFile = winC[0].ScriptFile;
                cmdC.DllName = assembly.GetName().Name + ".dll";
                cmdC.TypeName = type.FullName;

                MethodInfo[] property = type.GetMethods();
                for (int n = 0; n < property.Length; n++)
                {
                    WinformMethodAttribute[] WinM = (WinformMethodAttribute[])property[n].GetCustomAttributes(typeof(WinformMethodAttribute), true);
                    if (WinM.Length > 0)
                    {
                        WinformMethodInfo cmdM = new WinformMethodInfo();
                        cmdM.methodName = property[n].Name;
                        cmdM.methodInfo = property[n];
                        cmdC.MethodList.Add(cmdM);
                    }
                }

                WinformViewAttribute[] viewAttribute = (WinformViewAttribute[])type.GetCustomAttributes(typeof(WinformViewAttribute), true);
                for (int n = 0; n < viewAttribute.Length; n++)
                {
                    WinformViewInfo winView = new WinformViewInfo();
                    winView.Name = viewAttribute[n].Name;
                    winView.DllName = viewAttribute[n].DllName;
                    winView.ViewTypeName = viewAttribute[n].ViewTypeName;
                    winView.IsDefaultView = winView.Name == cmdC.defaultViewName ? true : false;

                    Assembly _assembly = assembly;
                    //如果配置的DllName指定了，就加载此dll创建界面类型
                    if (string.IsNullOrEmpty(winView.DllName) == false)
                    {
                        _assembly = Assembly.Load(winView.DllName.Replace(".dll", ""));
                    }
                    winView.ViewType = _assembly.GetType(winView.ViewTypeName, false, true);
                    winView.Memo = viewAttribute[n].Memo;
                    winView.ViewFile = viewAttribute[n].ViewFile;
                    cmdC.ViewList.Add(winView);
                }
                return cmdC;
            }

            return null;
        } 
        
    }

    public class WinformControllerInfo
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string DllName { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string controllerName { get; set; }
        /// <summary>
        /// 默认界面名称
        /// </summary>
        public string defaultViewName { get; set; }
        /// <summary>
        /// Winform控制器类型
        /// </summary>
        public Type winformController { get; set; }
        /// <summary>
        /// 脚本文件
        /// </summary>
        public string ScriptFile
        {
            get; set;
        }
        /// <summary>
        /// 控制器暴露的方法
        /// </summary>
        public List<WinformMethodInfo> MethodList { get; set; }
        /// <summary>
        /// 控制器关联的界面
        /// </summary>
        public List<WinformViewInfo> ViewList { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }

    public class WinformViewInfo
    {
        public string Name { get; set; }
        public string DllName { get; set; }
        public string ViewTypeName { get; set; }
        public bool IsDefaultView { get; set; }
        public Type ViewType { get; set; }
        public string ViewFile { get; set; }
        public string Memo { get; set; }
    }

    public class WinformMethodInfo
    {
        public string methodName { get; set; }
        public System.Reflection.MethodInfo methodInfo { get; set; }
        public List<string> dbkeys { get; set; }
    }

    /// <summary>
    /// 云软件
    /// </summary>
    public class CloudSoftClient
    {
        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftName { get; set; }
        /// <summary>
        /// 软件标题
        /// </summary>
        public string SoftTitle { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 云软件的路径，除了default为空，其他云软件必须有路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 控制器配置信息
        /// </summary>
        public List<WinformControllerInfo> controllerInfoList { get; set; }

        /// <summary>
        /// 控制器对象
        /// </summary>
        public Dictionary<String, WinformController> WinformControllerObjDic { get; set; }

        /// <summary>
        /// 基础信息，包括软件介绍、更新记录等
        /// </summary>
        public Dictionary<string, string> BaseInfo { get; set; }

        /// <summary>
        /// 依赖的程序集，第一个参数Name，第二个参数Path
        /// </summary>
        public List<string[]> AssemblyList { get; set; }

        /// <summary>
        /// 依赖的脚本文件，文件的路径
        /// </summary>
        public List<string[]> fileList { get; set; }

        /// <summary>
        /// 清空所有控制器集合
        /// </summary>
        public void ClearAllWinformController()
        {
            List<string> keys = WinformControllerObjDic.Keys.ToList();
            foreach (string s in keys)
            {
                WinformControllerObjDic.Remove(s);
            }
        }
        /// <summary>
        /// 清空所有控制器集合，除了参数指定
        /// </summary>
        /// <param name="exceptcontroller">排除指定控制器</param>
        public void ClearAllWinformController(params string[] exceptcontroller)
        {
            List<string> exceptlist = new List<string>();
            exceptlist = (exceptcontroller == null ? exceptlist : exceptcontroller.ToList());
            List<string> keys = WinformControllerObjDic.Keys.ToList();
            foreach (string s in keys)
            {
                if (exceptlist.FindIndex(x => x == s) > -1)
                    continue;
                WinformControllerObjDic.Remove(s);
            }
        }
        /// <summary>
        /// 清除指定控制器对象
        /// </summary>
        /// <param name="controller">指定控制器名称</param>
        public void ClearWinformController(string controller)
        {
            if (WinformControllerObjDic.ContainsKey(controller))
                WinformControllerObjDic.Remove(controller);
        }

        /// <summary>
        /// 获取控制器信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public WinformControllerInfo GetWinformControllerInfo(string controller)
        {
            List<WinformControllerInfo> list = controllerInfoList;
            if (list != null && list.FindIndex(x => x.controllerName == controller) > -1)
            {
                return list.Find(x => x.controllerName == controller);
            }
            return null;
        }

        /// <summary>
        /// 获取控制器对象
        /// </summary>
        /// <param name="controllername"></param>
        /// <param name="controllerid"></param>
        /// <returns></returns>
        public WinformController GetWinformControllerObj(string controllername, string controllerid)
        {
            //查询云软件是否有最新版本，有新版本就行升级，并清空此软件的控制器对象
            //if (UploadNewSoft())
            //{
            //    //清空控制器对象
            //    ClearAllWinformController();
            //}

            lock (WinformControllerObjDic)
            {
                if (WinformControllerObjDic.ContainsKey(controllerid))
                {
                    return WinformControllerObjDic[controllerid];
                }
                else
                {
                    WinformControllerInfo wattr = GetWinformControllerInfo(controllername);
                    if (wattr != null)
                    {
                        WinformController controllerObj = (WinformController)Activator.CreateInstance(wattr.winformController, null);
                        controllerObj.SoftName = SoftName;
                        controllerObj.ControllerId = controllerid;
                        Dictionary<string, IBaseView> viewDic = new Dictionary<string, IBaseView>();
                        for (int i = 0; i < wattr.ViewList.Count; i++)
                        {

                            if (wattr.ViewList[i].ViewType == null)
                                throw new Exception("控制器上自定义标签属性ViewTypeName配置不对！");
                            Form form = (Form)System.Activator.CreateInstance(wattr.ViewList[i].ViewType);
                            form.Name = wattr.ViewList[i].Name;
                            IBaseView view = form as IBaseView;
                            //IBaseViewBusiness view = (IBaseViewBusiness)(CreateInstance(wattr.ViewList[i].ViewType)());
                            view.frmName = wattr.ViewList[i].Name;
                            viewDic.Add(wattr.ViewList[i].Name, view);

                            if (wattr.ViewList[i].IsDefaultView)
                                controllerObj._defaultView = view;
                        }
                        controllerObj.iBaseView = viewDic;
                        //实例化InvokeController
                        foreach (KeyValuePair<string, IBaseView> val in viewDic)
                        {
                            val.Value.InvokeController = new ControllerEventHandler(controllerObj.UI_ControllerEvent);
                        }

                        WinformControllerObjDic.Add(controllerid, controllerObj);
                        //Log.Info(controllerObj);

                        //调用界面配置文件重新渲染界面
                        ViewRender(wattr, controllerObj);
                        //加载脚本文件并执行
                        ViewScript(wattr, controllerObj);

                        controllerObj.Init();//初始化必须放在WinformControllerObjDic.Add(controllerid, controllerObj);之后，不然就会出现死循环。

                        #region 解决异步加载,Init之后执行
                        List<IntPtr> ptrlist = new List<IntPtr>();
                        foreach (var frm in controllerObj.iBaseView)
                        {
                            ptrlist.Add((frm.Value as Form).Handle);
                        }
                        //异步执行数据初始化
                        var asyn = new Func<IntPtr[]>(delegate ()
                        {
                            controllerObj.AsynInit();
                            return ptrlist.ToArray();
                        });
                        IAsyncResult asynresult = asyn.BeginInvoke(new System.AsyncCallback(CallbackHandler), null);
                        #endregion
                        return controllerObj;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// 加载界面渲染
        /// </summary>
        /// <param name="wattr"></param>
        /// <param name="controller"></param>
        private void ViewRender(WinformControllerInfo wattr, WinformController controller)
        {
            try
            {
                controller.RenderList = new Dictionary<string, RenderEngine>();
                foreach (var item in wattr.ViewList)
                {
                    if (string.IsNullOrEmpty(item.ViewFile) == false)//存在界面配置文件
                    {
                        string[] arr = item.ViewFile.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        List<string> filelist = new List<string>();
                        foreach (string s in arr)
                        {
                            FileInfo fileinfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Path);
                            string viewfile = fileinfo.DirectoryName + "\\" + s;
                            if (File.Exists(viewfile))
                            {
                                filelist.Add(viewfile);
                            }
                        }

                        Form targetform = controller.iBaseView[item.Name] as Form;
                        if (targetform != null && filelist.Count > 0)
                        {
                            RenderEngine engine = ScriptManager.RenderPartViewFromFile(targetform, filelist.ToArray());
                            controller.RenderList.Add(item.Name, engine);
                        }
                        targetform.Refresh();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Info("渲染配置界面出错", e);
            }
        }

        /// <summary>
        /// 加载界面脚本
        /// </summary>
        /// <param name="wattr"></param>
        /// <param name="controller"></param>
        private void ViewScript(WinformControllerInfo wattr, WinformController controller)
        {
            try
            {
                if (string.IsNullOrEmpty(wattr.ScriptFile) == false)//配置了脚本文件
                {
                    FileInfo fileinfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Path);
                    string scriptfile = fileinfo.DirectoryName + "\\" + wattr.ScriptFile;
                    if (File.Exists(scriptfile))
                    {
                        ScriptTrace trace = new ScriptTrace(controller.ControllerId, scriptfile);
                        Action<ScriptEngine, ScriptScope, ScriptSource> actionScript = ((ScriptEngine engine, ScriptScope scope, ScriptSource source) =>
                        {
                            //engine.SetSearchPaths(new string[] { AppDomain.CurrentDomain.BaseDirectory + @"PythonLib" });
                            scope.SetVariable("trace", trace);//设置trace对象
                        });
                        Microsoft.Scripting.Hosting.ScriptScope scriptscope = ScriptManager.CreateScriptFromFile(scriptfile, actionScript);
                        controller.ScriptScope = scriptscope;
                        var main = scriptscope.GetVariable<Func<object, object>>("main");
                        object funResult = main(controller);
                        if (funResult.Equals(0) == false)
                        {
                            throw new Exception("Python脚本执行错误:" + funResult.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Info("执行脚本文件出错:"+e.Message, e);
            }
        }

        private void CallbackHandler(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult)iar;
            // 获取原委托对象。
            Func<IntPtr[]> operation = (Func<IntPtr[]>)ar.AsyncDelegate;
            // 结束委托调用。
            IntPtr[] intptrs = operation.EndInvoke(iar);
            //iController.AsynInitCompleted();
            foreach (var intp in intptrs)
            {
                WindowsAPI.SendMessage(intp, WindowsAPI.WM_ASYN_INPUT, 0, 0);
            }
        }
    }
}
