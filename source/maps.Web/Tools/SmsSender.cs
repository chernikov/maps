using maps.Web.Global.Config;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Tools
{
    public static class SmsSender
    {
        public static ISmsConfig config = DependencyResolver.Current.GetService<ISmsConfig>();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string SendSms(string phone, string text)
        {
            if (!string.IsNullOrWhiteSpace(config.SmsAPIKey))
            {
                return GetRequest(phone, config.SmsSender, text);
            }
            else
            {
                logger.Debug("Sms \t Phone: {0} Body: {1}", phone, text);
                return "Success";
            }
        }

        private static string GetRequest(string phone, string sender, string text)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(config.SmsTemplateUri);
                //important, otherwise the service can't desirialse your request properly
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
                webRequest.KeepAlive = false;
                webRequest.PreAuthenticate = false;

                string postData = "format=json&api_key=" + config.SmsAPIKey + "&phone=" + phone
                    + "&sender=" + sender + "&text=" + HttpUtility.UrlEncode(text);
                var ascii = new ASCIIEncoding();
                byte[] byteArray = ascii.GetBytes(postData);
                webRequest.ContentLength = byteArray.Length;
                var dataStream = webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                var webResponse = webRequest.GetResponse();
                var loResponseStream = new
                        StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

                string Response = loResponseStream.ReadToEnd();
                return Response;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка при отправке SMS", ex);
                return "Ошибка при отправке SMS";
            }
        }
    }
}