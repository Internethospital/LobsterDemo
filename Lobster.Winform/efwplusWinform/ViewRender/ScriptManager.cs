using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 界面渲染管理
    /// </summary>
    public class ScriptManager
    {
        #region 界面渲染
        /// <summary>
        /// 创建界面通过配置文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static RenderEngine CreateViewFromFile(string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(path, settings);
            xmldoc.Load(reader);
            reader.Close();
            return RenderView(null, xmldoc);
        }
        /// <summary>
        /// 创建界面通过配置文件
        /// </summary>
        /// <param name="targetform"></param>
        /// <param name="path"></param>
        public static RenderEngine RenderViewFromFile(Form targetform, string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(path, settings);
            xmldoc.Load(reader);
            reader.Close();
            return RenderView(targetform, xmldoc);
        }
        /// <summary>
        /// 创建界面通过XML字符串
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static RenderEngine CreateViewFromXML(string xml)
        {
            byte[] array = Encoding.UTF8.GetBytes(xml);
            MemoryStream _stream = new MemoryStream(array);
            StreamReader _reader = new StreamReader(_stream);

            XmlDocument xmldoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(_reader, settings);
            xmldoc.Load(reader);
            return RenderView(null, xmldoc);
        }
        /// <summary>
        /// 创建界面通过XML字符串
        /// </summary>
        /// <param name="targetform"></param>
        /// <param name="xml"></param>
        public static RenderEngine RenderViewFromXML(Form targetform, string xml)
        {
            byte[] array = Encoding.UTF8.GetBytes(xml);
            MemoryStream _stream = new MemoryStream(array);
            StreamReader _reader = new StreamReader(_stream);

            XmlDocument xmldoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(_reader, settings);
            xmldoc.Load(reader);
            return RenderView(targetform, xmldoc);
        }

        /// <summary>
        /// 渲染界面的局部容器
        /// </summary>
        /// <param name="targetform"></param>
        /// <param name="paths">xml界面配置文件</param>
        public static RenderEngine RenderPartViewFromFile(Form targetform, params string[] paths)
        {
            Dictionary<Control, XmlDocument> controlXmlDic = new Dictionary<Control, XmlDocument>();

            foreach (string path in paths)
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create(path, settings);
                xmldoc.Load(reader);
                reader.Close();

                Control partControl = GetRenderControl(targetform, xmldoc);
                if (controlXmlDic.ContainsKey(partControl) == false)
                    controlXmlDic.Add(partControl, xmldoc);
            }

            if (controlXmlDic.Count > 0)
                return RenderPartView(targetform, controlXmlDic);

            return null;
        }

        /// <summary>
        /// 渲染界面的局部容器
        /// </summary>
        /// <param name="targetform"></param>
        /// <param name="paths">xml界面配置文件</param>
        public static RenderEngine RenderPartViewFromXML(Form targetform, params string[] xmls)
        {
            Dictionary<Control, XmlDocument> controlXmlDic = new Dictionary<Control, XmlDocument>();
            foreach (string xml in xmls)
            {
                byte[] array = Encoding.UTF8.GetBytes(xml);
                MemoryStream _stream = new MemoryStream(array);
                StreamReader _reader = new StreamReader(_stream);

                XmlDocument xmldoc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create(_reader, settings);
                xmldoc.Load(reader);

                Control partControl = GetRenderControl(targetform, xmldoc);
                if (controlXmlDic.ContainsKey(partControl) == false)
                    controlXmlDic.Add(partControl, xmldoc);
            }

            if (controlXmlDic.Count > 0)
                return RenderPartView(targetform, controlXmlDic);

            return null;
        }

        private static Control GetRenderControl(Form targetform, XmlDocument xmldoc)
        {
            XmlNode node = xmldoc.DocumentElement.FirstChild;
            string ctrlname = node.Attributes["name"].Value;

            if(targetform.Name==ctrlname)
            {
                return targetform;
            }
            //反射  
            List<System.Reflection.FieldInfo> fieldInfo = targetform.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).ToList();
            System.Reflection.FieldInfo field = fieldInfo.Find(x => x.Name == ctrlname);
            if (field != null)
            {
                return field.GetValue(targetform) as Control;
            }

            throw new Exception("找不到控件：" + ctrlname);
        }

        /// <summary>
        /// 渲染整个界面
        /// </summary>
        /// <param name="rootcontainer">目标容器</param>
        /// <param name="xmldoc">界面配置</param>
        public static RenderEngine RenderView(Control rootControl, XmlDocument xmlDoc)
        {
            RenderEngine rengine = new RenderEngine(rootControl, xmlDoc, null, RenderMode.run);
            rengine.ExecuteRender();
            return rengine;
        }

        /// <summary>
        /// 渲染界面局部容器
        /// </summary>
        /// <param name="rootControl"></param>
        /// <param name="controlXmlDic"></param>
        /// <returns></returns>
        public static RenderEngine RenderPartView(Control rootControl, Dictionary<Control, XmlDocument> controlXmlDic)
        {
            RenderEngine rengine = new RenderEngine(rootControl, null, controlXmlDic, RenderMode.run);
            rengine.ExecuteRender();
            return rengine;
        }

        /// <summary>
        /// 设计界面根据配置文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static RenderEngine DesignViewFromFile(Control targetform, string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(path, settings);
            xmldoc.Load(reader);
            reader.Close();

            return RenderDesign(targetform, xmldoc);
        }

        /// <summary>
        /// 设计界面
        /// </summary>
        /// <param name="rootcontainer">目标容器</param>
        /// <param name="xmldoc">界面配置</param>
        public static RenderEngine RenderDesign(Control rootControl, XmlDocument xmlDoc)
        {
            RenderEngine rengine = new RenderEngine(rootControl, xmlDoc, null, RenderMode.design);
            rengine.ExecuteRender();
            return rengine;
        }

        #endregion

        #region python脚本
        /// <summary>
        /// 创建脚本对象通过文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="actionScript"></param>
        /// <returns></returns>
        public static ScriptScope CreateScriptFromFile(string path,Action<ScriptEngine, ScriptScope, ScriptSource> actionScript)
        {
            ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            engine.SetSearchPaths(new string[] { AppDomain.CurrentDomain.BaseDirectory+ "PythonLib"});
            ScriptScope scope = engine.CreateScope();
            ScriptSource script = engine.CreateScriptSourceFromFile(path, Encoding.UTF8);
            if (actionScript != null)
            {
                actionScript(engine, scope, script);
            }
            script.Execute(scope);

            return scope;
        }
        /// <summary>
        /// 创建脚本对象通过文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="actionScript"></param>
        /// <returns></returns>
        public static ScriptScope CreateScriptFromText(string text, Action<ScriptEngine, ScriptScope, ScriptSource> actionScript)
        {
            ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            engine.SetSearchPaths(new string[] { AppDomain.CurrentDomain.BaseDirectory + "PythonLib" });
            ScriptScope scope = engine.CreateScope();
            ScriptSource script = engine.CreateScriptSourceFromString(text);
            if (actionScript != null)
            {
                actionScript(engine, scope, script);
            }
            script.Execute(scope);

            return scope;
        }
        #endregion

    }
}
