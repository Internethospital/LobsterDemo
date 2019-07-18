using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efwplusWinform.ViewRender
{
    /// <summary>
    /// 脚本跟踪调试
    /// </summary>
    public class ScriptTrace
    {
        public static ScriptTraceHandler handler;

        private string controller;
        private string scriptfile;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_controller">控制器</param>
        /// <param name="_scriptfile">脚本文件</param>
        public ScriptTrace(string _controller,string _scriptfile)
        {
            controller = _controller;
            scriptfile = _scriptfile;
        }
        public ScriptTrace(ScriptTraceHandler _handler)
        {
            handler = _handler;
        }
        public void printlog(object msg)
        {
            printlog(msg, false);
        }
        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="_switch">开启位置显示</param>
        public void printlog(object msg,bool _switch)
        {
            if (_switch)
            {
                Common.Log.Info("控制器:" + controller);
                Common.Log.Info("脚本文件路径:" + scriptfile);
                if (handler != null)
                {
                    handler(DateTime.Now, "控制器:" + controller);
                    handler(DateTime.Now, "脚本文件路径:" + scriptfile);
                }
            }

            Common.Log.Info(msg);
            if (handler != null)
            {
                handler(DateTime.Now, msg);
            }
        }
    }

    public delegate void ScriptTraceHandler(DateTime time, object msg);
}
