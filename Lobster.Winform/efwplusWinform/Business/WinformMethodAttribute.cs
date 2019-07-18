using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efwplusWinform.Business.AttributeInfo
{
    [AttributeUsageAttribute(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class WinformMethodAttribute : Attribute
    {
        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }
}
