using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml.Serialization;
using EasyEncryption;
using WeiXinServer.Models;

namespace WeiXinServer.Controllers
{

    public class WeixinValidationController : ApiController
    {
        private const string WXToken = "RyanCloudToken20161201";

        [Route("")]
        public string Get()
        {
            return string.Empty;
        }


        [Route("")]
        public HttpResponseMessage Get(string signature, string timestamp, string nonce, string echostr)
        {
            var arr = new [] {WXToken, timestamp, nonce};
            Array.Sort(arr);
            var temp = string.Join(string.Empty, arr);
            var result = SHA.ComputeSHA1Hash(temp);

            var responseMessage = new HttpResponseMessage { Content = new StringContent(signature == result ? echostr : string.Empty, Encoding.GetEncoding("UTF-8"), "text/plain") };

            return responseMessage;

        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {

                var contextStream = HttpContext.Current.Request.InputStream;
                var contextBytes = new byte[contextStream.Length];
                contextStream.Read(contextBytes, 0, contextBytes.Length);
                var postContent = Encoding.UTF8.GetString(contextBytes);

                XmlSerializer serializer = new XmlSerializer(typeof(ContentXmlModel), new XmlRootAttribute("xml"));
                var result =
                    (ContentXmlModel)serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(postContent)));


                var sbReturn = new StringBuilder("<xml>" + "\n");
                sbReturn.AppendFormat(@"<ToUserName><![CDATA[{0}]]></ToUserName>" + "\n", result.FromUserName);
                sbReturn.AppendFormat(@"<FromUserName><![CDATA[{0}]]></FromUserName>" + "\n", result.ToUserName);
                sbReturn.AppendFormat(@"<CreateTime>{0}</CreateTime>" + "\n", CommonUtil.CommonUtil.toTime(DateTime.Now));
                sbReturn.AppendFormat(@"<MsgType><![CDATA[{0}]]></MsgType>" + "\n", result.MsgType);
                sbReturn.AppendFormat(@"<Content><![CDATA[{0}]]></Content>" + "\n", "<a href=\"http://www.baidu.com?r=" + result.FromUserName + "\">订单管理</a>");
                sbReturn.Append("</xml>");


                var responseMessage = new HttpResponseMessage
                {
                    Content = new StringContent(sbReturn.ToString(), Encoding.GetEncoding("UTF-8"), "text/plain")
                };

                return responseMessage;

            }
            catch (Exception ex)
            {
                File.AppendAllText(@"D:\WebSites\WeiXinServer\log.txt", ex.Message + "\r\n");

                var responseMessage = new HttpResponseMessage
                {
                    Content = new StringContent("success", Encoding.GetEncoding("UTF-8"), "text/plain")
                };

                return responseMessage;
            }

        }

    }
}
