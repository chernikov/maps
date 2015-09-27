using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace maps.Dozor
{
    public class DataCollector
    {
        private string UrlData = "http://iv-frankivsk.dozor-gps.com.ua/get_data?type=5";

        private string UrlBuses = "http://iv-frankivsk.dozor-gps.com.ua/get_data?type=1";

        private string Url = "http://iv-frankivsk.dozor-gps.com.ua/";
        private string host = "iv-frankivsk.dozor-gps.com.ua";

        public CookieCollection GetCookies()
        {

            var webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.KeepAlive = false;
            webRequest.PreAuthenticate = false;
            webRequest.Timeout = 3000;
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            string cookiesText = (webResponse as HttpWebResponse).Headers[HttpResponseHeader.SetCookie];
            if (!String.IsNullOrEmpty(cookiesText))
            {
                return GetAllCookiesFromHeader(cookiesText, host);
            }
            return null;
        }

        public string GetData(CookieCollection cookies)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(UrlData);
            webRequest.CookieContainer = new CookieContainer();
            foreach (Cookie cookie in cookies)
            {
                webRequest.CookieContainer.Add(new Uri(UrlData), cookie);
            }
            webRequest.KeepAlive = false;
            webRequest.PreAuthenticate = false;
            webRequest.Timeout = 10000;
            var webResponse = webRequest.GetResponse();

            var enc = Encoding.UTF8;
            var loResponseStream = new
                    StreamReader(webResponse.GetResponseStream(), enc);

            return loResponseStream.ReadToEnd();
        }

        public string GetBuses(CookieCollection cookies)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(UrlBuses);
            webRequest.CookieContainer = new CookieContainer();
            foreach (Cookie cookie in cookies)
            {
                webRequest.CookieContainer.Add(new Uri(UrlData), cookie);
            }
            webRequest.KeepAlive = false;
            webRequest.PreAuthenticate = false;
            webRequest.Timeout = 1000;
            var webResponse = webRequest.GetResponse();

            var enc = Encoding.UTF8;
            var loResponseStream = new
                    StreamReader(webResponse.GetResponseStream(), enc);

            return loResponseStream.ReadToEnd();
        }


        private CookieCollection GetAllCookiesFromHeader(string strHeader, string strHost)
        {
            var al = new ArrayList();
            CookieCollection cc = new CookieCollection();
            if (strHeader != string.Empty)
            {
                al = ConvertCookieHeaderToArrayList(strHeader);
                cc = ConvertCookieArraysToCookieCollection(al, strHost);
            }
            return cc;
        }

        private ArrayList ConvertCookieHeaderToArrayList(string strCookHeader)
        {
            strCookHeader = strCookHeader.Replace("\r", "");
            strCookHeader = strCookHeader.Replace("\n", "");
            string[] strCookTemp = strCookHeader.Split(',');
            var al = new ArrayList();
            int i = 0;
            int n = strCookTemp.Length;
            while (i < n)
            {
                if (strCookTemp[i].IndexOf("expires=", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    al.Add(strCookTemp[i] + "," + strCookTemp[i + 1]);
                    i = i + 1;
                }
                else
                {
                    al.Add(strCookTemp[i]);
                }
                i = i + 1;
            }
            return al;
        }

        private CookieCollection ConvertCookieArraysToCookieCollection(ArrayList al, string strHost)
        {
            CookieCollection cc = new CookieCollection();

            int alcount = al.Count;
            string strEachCook;
            string[] strEachCookParts;
            for (int i = 0; i < alcount; i++)
            {
                strEachCook = al[i].ToString();
                strEachCookParts = strEachCook.Split(';');
                int intEachCookPartsCount = strEachCookParts.Length;
                string strCNameAndCValue = string.Empty;
                string strPNameAndPValue = string.Empty;
                string strDNameAndDValue = string.Empty;
                string[] NameValuePairTemp;
                Cookie cookTemp = new Cookie();

                for (int j = 0; j < intEachCookPartsCount; j++)
                {
                    if (j == 0)
                    {
                        strCNameAndCValue = strEachCookParts[j];
                        if (strCNameAndCValue != string.Empty)
                        {
                            int firstEqual = strCNameAndCValue.IndexOf("=");
                            string firstName = strCNameAndCValue.Substring(0, firstEqual);
                            string allValue = strCNameAndCValue.Substring(firstEqual + 1, strCNameAndCValue.Length - (firstEqual + 1));
                            cookTemp.Name = firstName;

                            Encoding iso = Encoding.GetEncoding("utf-8");//may be utf-8
                            allValue = HttpUtility.UrlEncode(allValue, iso);

                            cookTemp.Value = allValue;
                        }
                        continue;
                    }
                    if (strEachCookParts[j].IndexOf("path", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        strPNameAndPValue = strEachCookParts[j];
                        if (strPNameAndPValue != string.Empty)
                        {
                            NameValuePairTemp = strPNameAndPValue.Split('=');
                            if (NameValuePairTemp[1] != string.Empty)
                            {
                                cookTemp.Path = NameValuePairTemp[1];
                            }
                            else
                            {
                                cookTemp.Path = "/";
                            }
                        }
                        continue;
                    }

                    if (strEachCookParts[j].IndexOf("domain", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        strPNameAndPValue = strEachCookParts[j];
                        if (strPNameAndPValue != string.Empty)
                        {
                            NameValuePairTemp = strPNameAndPValue.Split('=');

                            if (NameValuePairTemp[1] != string.Empty)
                            {
                                cookTemp.Domain = NameValuePairTemp[1];
                            }
                            else
                            {
                                cookTemp.Domain = strHost;
                            }
                        }
                        continue;
                    }
                }

                if (cookTemp.Path == string.Empty)
                {
                    cookTemp.Path = "/";
                }
                if (cookTemp.Domain == string.Empty)
                {
                    cookTemp.Domain = strHost;
                }
                cc.Add(cookTemp);
            }
            return cc;
        }
    }
}