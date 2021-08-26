using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestChild.Mobile.Domain;

namespace MailingDemon.Services
{
    /// <summary>
    /// реализация сервиса
    /// </summary>
    public class ExchangeServiceExImplementation : IExchangeServiceEx
    {
        private string BaseUrl;

        public ExchangeServiceExImplementation(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// реализация сервиса
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private async Task<T> CallService<T>(string url, ExchangeRequest param)
        {
            if (param.LastUpdateTick == 0)
            {
                param.LastUpdateTick = -1;
            }

            using (var request = new HttpClient(new HttpClientHandler {UseProxy = false})
                {Timeout = new TimeSpan(0, 10, 0)})
            {
                using (var response = await request.PostAsync(BaseUrl + url,
                    new StringContent(await JsonConvert.SerializeObjectAsync(param), Encoding.UTF8, "application/json")))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
            }
        }

        public long[] GetBoutToRemove(ExchangeRequest package)
        {
            return CallService<long[]>("/boutr", package).Result;
        }

        public BoutEx[] GetBouts(ExchangeRequest package)
        {
            return CallService<BoutEx[]>("/bouts", package).Result;
        }

        public long[] GetCampToRemove(ExchangeRequest package)
        {
            return CallService<long[]>("/campr", package).Result;
        }

        public Camp[] GetCamps(ExchangeRequest package)
        {
            return CallService<Camp[]>("/camps", package).Result;
        }

        public long[] GetPartyToRemove(ExchangeRequest package)
        {
            return CallService<long[]>("/partyr", package).Result;
        }

        public Party[] GetParty(ExchangeRequest package)
        {
            return CallService<Party[]>("/party", package).Result;
        }

        public long[] GetPersonalToRemove(ExchangeRequest package)
        {
            return CallService<long[]>("/personalr", package).Result;
        }

        public Personal[] GetPersonal(ExchangeRequest package)
        {
            return CallService<Personal[]>("/personals", package).Result;
        }

        public long[] GetCamperToRemove(ExchangeRequest package)
        {
            return CallService<long[]>("/camperr", package).Result;
        }

        public Camper[] GetCampers(ExchangeRequest package)
        {
            return CallService<Camper[]>("/campers", package).Result;
        }
    }
}
