using maps.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace maps.Instagram
{
    public class InsagramApiCaller 
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string ClientID = "f8f7037e3b53471092c3c81e0c83daaa";

        public string ClientSecret = "8ac774a96afa4690909e38d962340b6e";

        public string AuthUri = "https://api.instagram.com/oauth/authorize/?client_id={0}&redirect_uri={1}&response_type=code";

        public string AccessTokenUri = "https://api.instagram.com/oauth/access_token";

        public string SearchMediaUri = "https://api.instagram.com/v1/media/search";

        public string Code { get; set; }

        public JObject JsonAccess { get; set; }

        public static long UnixTimeNow(DateTime dateTime)
        {
            var timeSpan = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }

        public string AccessToken
        {
            get
            {
                if (JsonAccess != null)
                {
                    return JsonAccess["access_token"].Value<string>();
                }
                return null;
            }
        }
        

        public string Auth(string callbackUri)
        {
            return string.Format(AuthUri, ClientID, callbackUri);
        }

        public JObject GetAccess(string callbackUri)
        {
            try
            {
                var @params = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("client_id",ClientID),
                new KeyValuePair<string, string>("client_secret",ClientSecret),
                new KeyValuePair<string, string>("grant_type","authorization_code"),
                new KeyValuePair<string, string>("redirect_uri",callbackUri),
                new KeyValuePair<string, string>("code",Code) };
                JsonAccess = Connector.Connector.GetResource(AccessTokenUri, RequestMethodType.Post, @params);
                return JsonAccess;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return null;
        }

        public JObject SearchMedia(double lat, double lng, int distance = 1000, DateTime? dateTime = null)
        {
            logger.Debug(string.Format("Search {0} {1} {2} {3}", lat, lng, distance, dateTime));
            if (AccessToken != null)
            {
                var @params = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("lat",lat.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("lng",lng.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("distance",distance.ToString(CultureInfo.InvariantCulture)),
                };
                if (dateTime.HasValue)
                {
                    @params.Add(new KeyValuePair<string, string>("max_timestamp", UnixTimeNow(dateTime.Value).ToString()));
                }
                @params.Add(new KeyValuePair<string, string>("access_token", AccessToken));
                return Connector.Connector.GetResource(SearchMediaUri, RequestMethodType.Get, @params);
            }
            return null;
        }


        public bool IsReady
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Code) && AccessToken != null;
            }
        }
    }
}
