using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efwplusWinform.Business.AttributeInfo
{
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class WinformControllerAttribute:Attribute
    {
        

        private string _defaultViewName;
        /// <summary>
        /// 菜单对应打开界面
        /// </summary>
        public string DefaultViewName
        {
            get { return _defaultViewName; }
            set { _defaultViewName = value; }
        }

        private string _scriptFile;
        /// <summary>
        /// 脚本文件,允许指定多个脚本文件，采用;号隔开
        /// </summary>
        public string ScriptFile
        {
            get { return _scriptFile; }
            set { _scriptFile = value; }
        }

        private string _memo;
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }
}
