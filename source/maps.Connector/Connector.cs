using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace maps.Connector
{
    /// <summary>
    /// Класс который запрашивает данные у сайта (любого)
    /// </summary>
    public static class Connector
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Получаем Json какого-либо ресурса 
        /// </summary>
        /// <param name="uri">Адрес ресурса</param>
        /// <param name="requestMethod">Тип метода</param>
        /// <param name="parameters">параметры запроса</param>
        /// <returns>Json-объект</returns>
        public static JObject GetResource(string uri, RequestMethodType requestMethod, List<KeyValuePair<string, string>> parameters, int timeout = 20000)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException("Не задан адрес");
                }
                var webResponse = GetHttpRequest(uri, requestMethod, parameters, timeout);

                var responceStream = webResponse.GetResponseStream();
                var enc = System.Text.Encoding.GetEncoding(1251);
                var loResponseStream = new
                    StreamReader(webResponse.GetResponseStream(), enc);

                string response = loResponseStream.ReadToEnd();
                logger.Debug(string.Format("RESPONSE: {0}", response));
                return JObject.Parse(response);
            }
            catch (Exception ex)
            {
                logger.Error("Exception: " + ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Получаем картинку
        /// </summary>
        /// <param name="uri">Адрес ресурса</param>
        /// <param name="requestMethod">Тип метода</param>
        /// <param name="parameters">параметры запроса</param>
        /// <returns>Json-объект</returns>
        public static WebResponse GetImage(string uri, RequestMethodType requestMethod, List<KeyValuePair<string, string>> parameters, string watermark = null)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException("Не задан адрес");
            }
            var webResponse = GetHttpRequest(uri, requestMethod, parameters, 10000, watermark);
            return webResponse;
        }

        /// <summary>
        /// Получаем Json какого-либо ресурса 
        /// </summary>
        /// <param name="uri">Адрес ресурса</param>
        /// <param name="requestMethod">Тип метода</param>
        /// <param name="parameters">параметры запроса в строке</param>
        /// <returns>Json-объект</returns>
        public static JObject GetResource(string uri, RequestMethodType requestMethod, string parameters, int timeout = 10000)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentNullException("Не задан адрес");
                }
                var webResponse = GetHttpRequest(uri, requestMethod, parameters, timeout);

                var enc = Encoding.GetEncoding(1251);
                var loResponseStream = new
                        StreamReader(webResponse.GetResponseStream(), enc);

                var response = loResponseStream.ReadToEnd();
                logger.Debug(string.Format("RESPONSE: {0}", response));
                return JObject.Parse(response);
            }
            catch (Exception ex)
            {
                logger.Error("Exception: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Запрос к серверу
        /// </summary>
        /// <param name="uri">Адрес</param>
        /// <param name="requestMethod">GET\POST</param>
        /// <param name="parameters">Параметры</param>
        /// <returns>Ответ от сервера</returns>
        private static WebResponse GetHttpRequest(string uri, RequestMethodType requestMethod, List<KeyValuePair<string, string>> parameters, int timeout, string referer = null)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("Не задан Uri запроса");
            }

            Uri realUri = null;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out realUri))
            {
                throw new ArgumentException("Неверно задан адрес");
            }

            HttpWebRequest webRequest = null;
            if (requestMethod == RequestMethodType.Get)
            {
                if (parameters != null && parameters.Count > 0)
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(
                        string.Format("{0}?{1}", uri, ParamsString(parameters)));
                }
                else
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(uri);
                }
                webRequest.Method = "GET";
            }
            else if (requestMethod == RequestMethodType.Post)
            {
                webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "POST";
            }
            webRequest.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrWhiteSpace(referer))
            {
                webRequest.Referer = referer;
            }
            webRequest.KeepAlive = false;
            webRequest.PreAuthenticate = false;
            webRequest.Timeout = timeout;
            if (requestMethod == RequestMethodType.Post)
            {
                CreateParams(ParamsString(parameters), webRequest);
            }
            logger.Debug(string.Format("REQUEST: {0} params: {1}", uri, ParamsString(parameters)));
            return webRequest.GetResponse();
        }

        /// <summary>
        /// Запрос к серверу
        /// </summary>
        /// <param name="uri">Адрес</param>
        /// <param name="requestMethod">GET\POST</param>
        /// <param name="parameters">Параметры в строке</param>
        /// <returns>Ответ от сервера</returns>
        private static WebResponse GetHttpRequest(string uri, RequestMethodType requestMethod, string parameters, int timeout)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("Не задан Uri запроса");
            }

            Uri realUri = null;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out realUri))
            {
                throw new ArgumentException("Неверно задан адрес");
            }

            HttpWebRequest webRequest = null;
            if (requestMethod == RequestMethodType.Get)
            {
                if (parameters != null)
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(
                        string.Format("{0}?{1}", uri, parameters));
                }
                else
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(uri);
                }
                webRequest.Method = "GET";
            }
            else if (requestMethod == RequestMethodType.Post)
            {
                webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "POST";
            }
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = false;
            webRequest.PreAuthenticate = false;
            webRequest.Timeout = timeout;
            if (requestMethod == RequestMethodType.Post)
            {
                CreateParams(parameters, webRequest);
            }
            logger.Debug(string.Format("REQUEST: {0} params: {1}", uri, parameters));

            return webRequest.GetResponse();
        }

        /// <summary>
        /// Добавить post параметры в web-запрос
        /// </summary>
        /// <param name="parameterString">строка параметров</param>
        /// <param name="webRequest">web-запрос</param>
        private static void CreateParams(string parameterString, HttpWebRequest webRequest)
        {
            var enc = System.Text.Encoding.GetEncoding(1251);
            byte[] byteArray = enc.GetBytes(parameterString);
            webRequest.ContentLength = byteArray.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }

        /// <summary>
        /// Преобразовать список ключ-значение параметров в строку
        /// </summary>
        /// <param name="parameters">Список ключ-значение параметров</param>
        /// <returns></returns>
        private static string ParamsString(List<KeyValuePair<string, string>> parameters)
        {
            StringBuilder sb = new StringBuilder(1024);

            foreach (var keyValuePair in parameters)
            {
                sb.AppendFormat("{0}={1}&", keyValuePair.Key, keyValuePair.Value);
            }
            //удалить последний "&"
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
