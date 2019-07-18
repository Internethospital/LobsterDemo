using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace efwplusWinform.Controller
{
    /// <summary>
    /// 控制器操作类
    /// </summary>
    public class ControllerHelper
    {
        /// <summary>
        /// 创建控制器
        /// </summary>
        /// <param name="controllerid"></param>
        /// <param name="controllername"></param>
        /// <returns></returns>
        public static WinformController CreateController(string softname, string controllerid, string controllername)
        {
            try
            {
                CloudSoftClient client = CloudSoftClientManager.CSoftClientList.Find(x => x.SoftName == softname);
                if (client == null)
                    throw new Exception("找不到指定的云软件[" + softname + "]");

                WinformController iController = client.GetWinformControllerObj(controllername, controllerid);
                if (iController == null) throw new Exception("找不到[" + controllername + "]控制器");
                return iController;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 执行控制器方法，参数是对象数组
        /// </summary>
        /// <param name="controllerid">控制器标识</param>
        /// <param name="controllername">控制器名称</param>
        /// <param name="methodname">方法名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static object Execute(string softname, string controllerid, string controllername, string methodname, object[] parameters)
        {
            CloudSoftClient client = CloudSoftClientManager.CSoftClientList.Find(x => x.SoftName == softname);
            if (client == null)
                throw new Exception("找不到指定的云软件[" + softname + "]");

            WinformController iController = client.GetWinformControllerObj(controllername, controllerid);
            if (iController == null) throw new Exception("找不到[" + controllername + "]控制器");

            WinformControllerInfo cattr = client.GetWinformControllerInfo(controllername);
            WinformMethodInfo methodattr = cattr.MethodList.Find(x => x.methodName == methodname);
            if (methodattr == null) throw new Exception("控制器中没有[" + methodname + "]方法名");

            return methodattr.methodInfo.Invoke(iController, parameters);
        }


        /// <summary>
        /// 执行控制器方法，参数是Json数组
        /// </summary>
        /// <param name="controllerid"></param>
        /// <param name="controllername"></param>
        /// <param name="methodname"></param>
        /// <param name="parametersJson"></param>
        /// <returns></returns>
        public static object Execute(string softname, string controllerid, string controllername, string methodname, string[] parametersJson)
        {
            CloudSoftClient client = CloudSoftClientManager.CSoftClientList.Find(x => x.SoftName == softname);
            if (client == null)
                throw new Exception("找不到指定的云软件[" + softname + "]");

            List<object> objParamList = new List<object>();//参数对象

            WinformController iController = client.GetWinformControllerObj(controllername, controllerid);
            if (iController == null) throw new Exception("找不到[" + controllername + "]控制器");

            WinformControllerInfo cattr = client.GetWinformControllerInfo(controllername);
            WinformMethodInfo methodattr = cattr.MethodList.Find(x => x.methodName == methodname);
            if (methodattr == null) throw new Exception("控制器中没有[" + methodname + "]方法名");


            ParameterInfo[] paraInfo = methodattr.methodInfo.GetParameters();
            if (parametersJson.Length != paraInfo.Length) throw new Exception("参数个数不一致");
            for (int i = 0; i < paraInfo.Length; i++)
            {
                objParamList.Add(JsonConvert.DeserializeObject(parametersJson[i], paraInfo[i].ParameterType));
            }
            return methodattr.methodInfo.Invoke(iController, objParamList.ToArray());
        }


        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="softname">云软件名称</param>
        /// <param name="controllerid">控制器标识</param>
        /// <param name="controllername">控制器名称</param>
        /// <param name="frmname">界面名称</param>
        /// <param name="IsPopupView">界面是否直接显示</param>
        public static IBaseView ShowView(string softname,string controllerid, string controllername, string frmname, bool IsPopupView = true)
        {
            CloudSoftClient client= CloudSoftClientManager.CSoftClientList.Find(x => x.SoftName == softname);
            if (client == null)
                throw new Exception("找不到指定的云软件["+ softname + "]");

            WinformController iController = client.GetWinformControllerObj(controllername, controllerid);
            if (iController == null)
                throw new Exception("找不到[" + controllername + "]控制器");

            if (string.IsNullOrEmpty(frmname))
            {
                if (IsPopupView)
                    iController.ShowDefaultForm();
                return iController.DefaultView;
            }
            else
            {
                if (IsPopupView)
                    iController.ShowForm(frmname);
                return iController.iBaseView[frmname];
            }
        }
    }
}
