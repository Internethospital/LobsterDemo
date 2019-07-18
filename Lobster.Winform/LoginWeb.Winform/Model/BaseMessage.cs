using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginWeb.Winform.Model
{
    public class BaseMessage
    {
        private int  _id;
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return  _id; }
            set {  _id = value; }
        }

        private string  _messagetype;
        /// <summary>
        /// 消息类型代码
        /// </summary>
        public string MessageType
        {
            get { return  _messagetype; }
            set {  _messagetype = value; }
        }

       

        private string  _messagetitle;
        /// <summary>
        /// 消息标题
        /// </summary>
        public string MessageTitle
        {
            get { return  _messagetitle; }
            set {  _messagetitle = value; }
        }

        private string  _messagecontent;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent
        {
            get { return  _messagecontent; }
            set {  _messagecontent = value; }
        }
        private string _sendTime;
        public string SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }


    }
}
