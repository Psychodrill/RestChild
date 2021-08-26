using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using RestChild.Web.Models.FIASInegration;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    /// Взаимодействие с ФИАС
    /// </summary>
    [Authorize]
    public class WebFIASController : BaseController
    {
        private static readonly string BasicUrl = System.Configuration.ConfigurationManager.AppSettings["FIASBasicUrl"] ?? "https://efp6.sm-soft.ru:8243/address/1.0";

        /// <summary>
        /// поиск адреса в ФИАС (прокси класс)
        /// </summary>
        /// <param name="Query"></param>
        [HttpGet]
        public async Task<IHttpActionResult> SearchHome(string Query)
        {
            var req = new FIASRequest()
            {
                Query = Query,
                Count = 20,
                LocationsBoost = new FIASRequestLocation[1] { new FIASRequestLocation { Region = "Москва" } }
            };
            using (var httpClient = new HttpClient())
            {
                var jsonRequest = JsonConvert.SerializeObject(req);
                using (var requestMessage = CreateHttpRequestMessage(HttpMethod.Post, new Uri($"{BasicUrl}/searchHome"), jsonRequest))
                {
                    using (var response = await httpClient.SendAsync(requestMessage))
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(responseData);
                        return Json(json);
                    }
                }
            }
        }

        /// <summary>
        ///     Формирование запроса
        /// </summary>
        /// <returns></returns>
        private static HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, Uri url, string requestContent = null)
        {
            var requestMessage = new HttpRequestMessage(method, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ETPToken.Token);
            if (!string.IsNullOrWhiteSpace(requestContent))
            {
                requestMessage.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }

        /// <summary>
        ///     Достать адрес по идентификатору ФИАС
        /// </summary>
        public static async Task<JObject> GetAddressByFiasId(string FiasId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var requestMessage = CreateHttpRequestMessage(HttpMethod.Get, new Uri($"{BasicUrl}/getAddress?fiasId={FiasId}")))
                {
                    using (var response = await httpClient.SendAsync(requestMessage))
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(responseData);
                        return json;
                    }
                }
            }
        }
    }
}
