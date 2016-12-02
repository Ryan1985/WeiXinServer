using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
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



    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var postContent =
                "<xml><URL><![CDATA[http://123.206.205.197]]></URL><ToUserName><![CDATA[123]]></ToUserName><FromUserName><![CDATA[321]]></FromUserName><CreateTime>1480656151</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[333333]]></Content><MsgId>123</MsgId></xml>";
            XmlSerializer serializer = new XmlSerializer(typeof(ContentXmlModel),new XmlRootAttribute("xml"));
            var result =
                (ContentXmlModel)serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(postContent)));

            Assert.AreEqual(1, 1);





        }
    }
}
