using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace RestChild.Web.Models.FIASInegration
{
    /// <summary>
    /// получение Bearer Token
    /// </summary>
    internal static class ETPToken
    {
        private static readonly object syncroObject = new object();

        private static string token = null;
        private static DateTime? tokenUpdate = null;

        private static readonly string ConsumerKey = System.Configuration.ConfigurationManager.AppSettings["FIASConsumerKey"] ?? "JhiiLzyxCqJyvO1yXkQaasD35fYa";
        private static readonly string ConsumerSecret = System.Configuration.ConfigurationManager.AppSettings["FIASConsumerSecret"] ?? "_IdQge73vNm1uyo9DNU7xcngmG4a";
        private static readonly string AuthorizationBasicUrl = System.Configuration.ConfigurationManager.AppSettings["FIASAuthorizationBasicUrl"] ?? "https://efp6.sm-soft.ru:8243/token";

        /// <summary>
        /// Bearer Token
        /// </summary>
        public static string Token
        {
            get
            {
                if (string.IsNullOrWhiteSpace(token) || !tokenUpdate.HasValue || tokenUpdate < DateTime.Now)
                {
                    UpdateToken();
                }
                return token;
            }
        }

        private static void UpdateToken()
        {
            lock (syncroObject)
            {
                if (string.IsNullOrWhiteSpace(token) || !tokenUpdate.HasValue || tokenUpdate.Value < DateTime.Now)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), new Uri(AuthorizationBasicUrl)))
                        {
                            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {Base64Encode()}");

                            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                            var response = httpClient.SendAsync(request).Result;
                            var contents = response.Content.ReadAsStringAsync().Result;

                            JObject json = JObject.Parse(contents);

                            token = (string)json["access_token"];
                            tokenUpdate = new DateTime(Convert.ToInt64(json["expires_in"]));
                            if (tokenUpdate <= DateTime.Now)
                                tokenUpdate = DateTime.Now.AddMinutes(5);
                        }
                    }
                }
            }
        }

        private static string Base64Encode()
        {
            var plainTextBytes = Encoding.UTF8.GetBytes($"{ConsumerKey}:{ConsumerSecret}");
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
