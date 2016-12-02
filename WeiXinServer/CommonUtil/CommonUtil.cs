using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace WeiXinServer.CommonUtil
{
    public class CommonUtil
    {
        private const string APPID = "wx539747950e016d65";
        private const string APPSECRET = "51f74d50835ce998c1c5f37b9c32a6eb";
        private static DateTime LastTokenTime = default(DateTime);
        private static double ExpireIn = 0;
        public static string CurrentToken = string.Empty;


        public static string GetToken()
        {
            if (CurrentToken == string.Empty || (DateTime.Now - LastTokenTime).TotalSeconds >= ExpireIn)
            {
                lock (CurrentToken)
                {
                    var url = string.Format(@"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", APPID, APPSECRET);
                    var responseStream = HttpWebRequest.Create(url).GetResponse().GetResponseStream();
                    var bytesRead = new byte[responseStream.Length];
                    responseStream.Read(bytesRead, 0, (int)responseStream.Length);
                    File.AppendAllText(@"D:\WebSites\WeiXinServer\log.txt", Encoding.UTF8.GetString(bytesRead));
                }

            }

            return CurrentToken;
        }



        public string HttpGet()
        {
            var client = new HttpClient();   
            return string.Empty;


        }


        public static string formatTime(long createTime)
        {
            long time_JAVA_Long = createTime * 1000L;//java长整型日期，毫秒为单位                
            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度                         
            long time_tricks = tricks_1970 + time_JAVA_Long * 10000;//日志日期刻度                         
            DateTime dt = new DateTime(time_tricks).AddHours(8);//转化为DateTime
            return dt.ToString();
        }


        public static string toTime(DateTime createTime)
        {
            var dt = createTime.AddHours(-8);
            var span = (dt.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks)/10000/1000L;
            return span.ToString();
        }



    }
}