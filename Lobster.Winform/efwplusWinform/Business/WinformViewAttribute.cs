using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efwplusWinform.Business.AttributeInfo
{
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class WinformViewAttribute : Attribute
    {
        private string _name;
        /// <summary>
        /// 界面名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _dllName;
        /// <summary>
        /// 界面存放的DLL
        /// </summary>
        public string DllName
        {
            get { return _dllName; }
            set { _dllName = value; }
        }

        private string _viewTypeName;
        /// <summary>
        /// 界面类型名称
        /// </summary>
        public string ViewTypeName
        {
            get { return _viewTypeName; }
            set { _viewTypeName = value; }
        }

        private string _viewFile;
        /// <summary>
        /// 界面文件，允许指定多个界面文件，采用;号隔开
        /// </summary>
        public string ViewFile
        {
            get { return _viewFile; }
            set { _viewFile = value; }
        }

        string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }
}
