using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinServer.Models
{
    public class ContentXmlModel
    {
        public string Url;
        public string ToUserName;
        public string FromUserName;
        public string CreateTime;
        public string MsgType;
        public string Content;
        public string MsgId;
    }
}