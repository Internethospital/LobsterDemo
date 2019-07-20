using Debugger.Winform.Common;
using Debugger.Winform.IView;
using efwplusWinform;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Debugger.Winform.Controller
{
    [WinformController(DefaultViewName = "frmScriptNavigation", Memo = "云软件脚本管理")]//在菜单上显示
    [WinformView(Name = "frmScriptNavigation", ViewTypeName = "Debugger.Winform.ViewForm.frmScriptNavigation")]//控制器关联的界面
    [WinformView(Name = "dlgNewSoft", ViewTypeName = "Debugger.Winform.ViewForm.dlgNewSoft")]//控制器关联的界面
    [WinformView(Name = "dlgNewFile", ViewTypeName = "Debugger.Winform.ViewForm.dlgNewFile")]//控制器关联的界面
    public class ScriptManageController : WinformController
    {
        /// <summary>
        /// 嵌入主界面显示
        /// </summary>
        private bool Embedded = false;
        IfrmScriptNavigation _ifrmScriptNavigation;
        IdlgNewSoft _idlgNewSoft;//新增云软件
        IdlgNewFile _idlgNewFile;//新增文件
        private string _efwplusRuntimePath = string.Empty;
        public override void Init()
        {
            _efwplusRuntimePath = AppDomain.CurrentDomain.BaseDirectory;
            _ifrmScriptNavigation = (IfrmScriptNavigation)DefaultView;
            _idlgNewSoft = (IdlgNewSoft)iBaseView["dlgNewSoft"];
            _idlgNewFile = (IdlgNewFile)iBaseView["dlgNewFile"];
            LoadScript();
        }
        /// <summary>
        /// 加载脚本资源文件
        /// </summary>
        [WinformMethod]
        public void LoadScript()
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + "WinScript";
            _ifrmScriptNavigation.loadTree(_efwplusRuntimePath);
        }
        /// <summary>
        /// 打开脚本代码文件
        /// </summary>
        /// <param name="file"></param>
        [WinformMethod]
        public void OpenScript(string file)
        {
            Form view = (Form)InvokeControllerEx(file, "TextEditorController", "ShowCodeFile", new object[] { file });
            if (Embedded)
            {
                InvokeController("SoftBrowserController", "OpenView", view);
            }
            else//独立窗口显示
            {
                view.Show();
            }
        }

        /// <summary>
        /// 打开云软件界面
        /// </summary>
        /// <param name="file"></param>
        [WinformMethod]
        public void OpenView(string file)
        {
            Form view = (Form)InvokeControllerEx(file, "TextEditorController", "ShowViewFile", new object[] { file });
            if (Embedded)
            {
                InvokeController("SoftBrowserController", "OpenView", view);
            }
            else//独立窗口显示
            {
                view.Show();
            }
        }

        /// <summary>
        /// 显示脚本资源界面，提供给外部调用
        /// </summary>
        /// <returns></returns>
        [WinformMethod]
        public object ShowScriptView()
        {
            Embedded = true;
            return _ifrmScriptNavigation;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        [WinformMethod]
        public void DeleteFile(string file)
        {
            FileAttributes attr = File.GetAttributes(file);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(file, true);
            }
            else
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// 删除软件项目
        /// </summary>
        /// <param name="path"></param>
        [WinformMethod]
        public void DeleteSoft(string path)
        {
            Directory.Delete(path, true);
            DeleteCloudSoftConfig(new DirectoryInfo(path).Name);
            //刷新树
            //LoadScript();
            //重新加载云软件
            InvokeController("SoftBrowserController", "ReLoadSoft");
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        [WinformMethod]
        public object RenameFile(object Name, string newName)
        {
            string newFullName = "";
            if (Name is DirectoryInfo)
            {
                DirectoryInfo dir = (Name as DirectoryInfo);
                newFullName = dir.Parent.FullName + "/" + newName;
                Directory.Move(dir.FullName, newFullName);
                return new DirectoryInfo(newFullName);
            }
            else if (Name is FileInfo)
            {
                FileInfo file = (Name as FileInfo);
                newFullName = file.DirectoryName + "/" + newName;
                File.Move(file.FullName, newFullName);
                return new FileInfo(newFullName);
            }
            return null;
        }

        [WinformMethod]
        public void DialogNewSoft()
        {
            if (_ifrmScriptNavigation.selectedText == "WinScript")
            {
                _idlgNewSoft.SoftType = 0;
            }
            else if (_ifrmScriptNavigation.selectedText == "ModulePlugin")
            {
                _idlgNewSoft.SoftType = 1;
            }
            (_idlgNewSoft as Form).ShowDialog();
        }

        [WinformMethod]
        public string DialogNewFile(int filetype, string path)
        {
            _idlgNewFile.fileType = filetype;
            _idlgNewFile.path = path;
            if (filetype == 0)
            {
                (_idlgNewFile as Form).Text = "添加Controller文件";
            }
            else if (filetype == 1)
            {
                (_idlgNewFile as Form).Text = "添加Model文件";
            }
            else if (filetype == 2)
            {
                (_idlgNewFile as Form).Text = "添加View文件";
            }
            (_idlgNewFile as Form).ShowDialog();
            return _idlgNewFile.filename;
        }

        /// <summary>
        /// 新增云软件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="version"></param>
        /// <param name="author"></param>
        [WinformMethod]
        public bool NewSoftClient(string name, string title, string version, string author)
        {
            //string winscriptpath = _efwplusRuntimePath + "efwplusClient\\WinScript\\";
            string scripttemplatepath = _efwplusRuntimePath + "ScriptTemplate\\";
            string softpath = _efwplusRuntimePath + "WinScript\\" + name;
            //不能新建重复名称的云软件
            if (Directory.Exists(softpath))
            {
                throw new Exception("该软件名称已经存在，请输入一个新的软件名称！");
            }
            //创建云软件文件夹
            Directory.CreateDirectory(softpath);

            //拷贝模板项目
            CopyEntireDir(scripttemplatepath + "Hello", softpath);

            string configpath = softpath + "/config.xml";
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(configpath);

            //修改配置文件config.xml
            System.Xml.XmlNode softnode = xmlDoc.DocumentElement.SelectSingleNode("cloudsoft");
            if (softnode != null)
            {
                softnode.Attributes["name"].Value = name;
                softnode.Attributes["title"].Value = title;
                softnode.Attributes["version"].Value = version;
                softnode.Attributes["author"].Value = author;
            }
            System.Xml.XmlNode wcnode = xmlDoc.DocumentElement.SelectSingleNode("cloudsoft/controllerList/WinformController");
            if (wcnode != null)
            {
                wcnode.Attributes["ScriptFile"].Value = wcnode.Attributes["ScriptFile"].Value.Replace("TemplateScript", name);
                wcnode.FirstChild.Attributes["ViewFile"].Value = wcnode.FirstChild.Attributes["ViewFile"].Value.Replace("TemplateScript", name);
            }
            xmlDoc.Save(configpath);

            //替换文件内容，将name、title、version、author更新到文件中
            DirectoryInfo softdir = new DirectoryInfo(softpath);
            FileInfo[] allfile = softdir.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo f in allfile)
            {
                if (f.Name == "Controller01.py")
                {
                    FileStream fs = new FileStream(f.FullName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    string con = sr.ReadToEnd();
                    con = con.Replace("WinScript/Hello", "WinScript/" + name);
                    sr.Close();
                    fs.Close();
                    FileStream fs2 = new FileStream(f.FullName, FileMode.Open, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs2);
                    sw.WriteLine(con);
                    sw.Close();
                    fs2.Close();
                }
            }

            //云软件添加到CloudSoftConfig.xml中
            string path = "WinScript/" + name + "/config.xml";
            ToCloudSoftConfig(name, title, version, path);
            //刷新树
            LoadScript();
            //重新加载云软件
            InvokeController("SoftBrowserController", "ReLoadSoft");
            return true;
        }

        private string getSoftName(string path)
        {
            string efwplusclientpath = AppDomain.CurrentDomain.BaseDirectory + "WinScript\\";
            if (path.IndexOf(efwplusclientpath) > -1)
            {
                return path.Replace(efwplusclientpath, "").Split('\\')[0];
            }
            return null;
        }

        [WinformMethod]
        public bool NewFile(string name)
        {
            string scripttemplatepath = _efwplusRuntimePath + "ScriptTemplate\\";
            string createpath = _idlgNewFile.path;

            if (_idlgNewFile.fileType == 0)
            {
                //不能新建重复名称的文件
                if (File.Exists(createpath + "\\" + name + ".py"))
                {
                    throw new Exception("该文件名称已经存在，请输入一个新的文件名称！");
                }

                //拷贝文件模板
                File.Copy(scripttemplatepath + "Controller01.py", createpath + "\\" + name + ".py", true);
                //替换文件内容
                FileStream fs = new FileStream(createpath + "\\" + name + ".py", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string con = sr.ReadToEnd();
                con = con.Replace("WinScript/Hello", "WinScript/" + getSoftName(createpath));
                con = con.Replace("Controller01.py", name + ".py");
                sr.Close();
                fs.Close();
                FileStream fs2 = new FileStream(createpath + "\\" + name + ".py", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(con);
                sw.Close();
                fs2.Close();
                //修改树节点

            }
            else if (_idlgNewFile.fileType == 1)
            {
                //不能新建重复名称的文件
                if (File.Exists(createpath + "\\" + name + ".py"))
                {
                    throw new Exception("该文件名称已经存在，请输入一个新的文件名称！");
                }

                //拷贝文件模板
                File.Copy(scripttemplatepath + "Model01.py", createpath + "\\" + name + ".py", true);
                //替换文件内容
                FileStream fs = new FileStream(createpath + "\\" + name + ".py", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string con = sr.ReadToEnd();
                con = con.Replace("Model01.py", name + ".py");
                sr.Close();
                fs.Close();
                FileStream fs2 = new FileStream(createpath + "\\" + name + ".py", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(con);
                sw.Close();
                fs2.Close();
                //修改树节点
            }
            else if (_idlgNewFile.fileType == 2)
            {
                //不能新建重复名称的文件
                if (File.Exists(createpath + "\\" + name + ".py"))
                {
                    throw new Exception("该文件名称已经存在，请输入一个新的文件名称！");
                }

                //拷贝文件模板
                File.Copy(scripttemplatepath + "View01.py", createpath + "\\" + name + ".py", true);
                File.Copy(scripttemplatepath + "View01.xml", createpath + "\\" + name + ".xml", true);
                //替换文件内容
                FileStream fs = new FileStream(createpath + "\\" + name + ".py", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string con = sr.ReadToEnd();
                con = con.Replace("View01.py", name + ".py");
                sr.Close();
                fs.Close();
                FileStream fs2 = new FileStream(createpath + "\\" + name + ".py", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(con);
                sw.Close();
                fs2.Close();
                //修改树节点
            }
            return true;
        }
        //拷贝文件夹
        private void CopyEntireDir(string sourcePath, string destPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
               SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
               SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
        }

        [WinformMethod]
        public bool NewSoftServer(string name, string title, string version, string author)
        {
            return true;
        }

        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="filepath">文件存放地址，包含文件名</param>
        /// <returns></returns>
        private bool HttpDownload(string url, string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);    //存在则删除
            }
            try
            {
                Directory.CreateDirectory(new FileInfo(filepath).DirectoryName);//创建目录
                FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                //StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("utf-8"));
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                fs.Close();
                responseStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 将软件加入到配置文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="version"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool ToCloudSoftConfig(string name, string title, string version, string path)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(CloudSoftClientManager.csoftconfigFile);
            System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("cloudsoftClient[@name='" + name + "']");
            if (node != null)
            {
                node.Attributes["path"].Value = path;
                node.Attributes["title"].Value = title;
                node.Attributes["version"].Value = version;
            }
            else
            {
                System.Xml.XmlElement el = xmlDoc.CreateElement("cloudsoftClient");
                el.SetAttribute("name", name);
                el.SetAttribute("path", path);
                el.SetAttribute("title", title);
                el.SetAttribute("version", version);
                xmlDoc.DocumentElement.AppendChild(el);
            }
            xmlDoc.Save(CloudSoftClientManager.csoftconfigFile);
            return true;
        }

        private bool DeleteCloudSoftConfig(string name)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(CloudSoftClientManager.csoftconfigFile);
            System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("cloudsoftClient[@name='" + name + "']");
            if (node != null)
            {
                xmlDoc.DocumentElement.RemoveChild(node);
                xmlDoc.Save(CloudSoftClientManager.csoftconfigFile);
            }
            return true;
        }


        [WinformMethod]
        public void SearchPublishSoft()
        {
            //ServiceResponseData retdata = InvokeWcfService("System.Service", "PackServerController", "GetCloudSoftConfig");
            //string filedata = retdata.GetData<string>(0);
            //Form view = (Form)InvokeControllerEx(Guid.NewGuid().ToString(), "TextEditorController", "ShowText", new object[] { filedata });
            //view.ShowDialog();

        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name=""></param>
        [WinformMethod]
        public bool PublishSoft(string path)
        {
            return true;
        }
        /// <summary>
        /// 下载
        /// </summary>
        [WinformMethod]
        public bool UploadSoft(string path)
        {
            return true;
        }
    }
}
