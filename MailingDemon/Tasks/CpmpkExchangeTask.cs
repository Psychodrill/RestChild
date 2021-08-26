using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange.Cpmpk;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Booking.Logic.Extensions;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     обмен с ЦПМПК
    /// </summary>
    [Task]
    public class CpmpkExchangeTask : BaseTask
    {
        /// <summary>
        ///     Настройки
        /// </summary>
        [XmlElement("config")]
        public CpmpkExchangeTaskConfig Config { get; set; }

        /// <summary>
        ///     Сбросить значения для проверки
        /// </summary>
        private void ResetCheckChildInBaseRegistry(UnitOfWork uw, long childId, ExchangeBaseRegistryTypeEnum type)
        {
            var exchangeBaseRegistries =
                uw.GetSet<RestChild.Domain.ExchangeBaseRegistry>()
                    .Where(e => !e.NotActual && e.ChildId == childId &&
                                (e.ExchangeBaseRegistryTypeId == (long) type ||
                                 !e.ExchangeBaseRegistryTypeId.HasValue))
                    .ToList();
            foreach (var exchangeBaseRegistry in exchangeBaseRegistries)
            {
                exchangeBaseRegistry.NotActual = true;
            }

            uw.SaveChanges();
        }

        /// <summary>
        ///     обмен с ЦПМПК
        /// </summary>
        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("CpmpkExchangeTask started");

                var exec = unitOfWork.GetSet<ExchangeBaseRegistryType>().Any(ss =>
                    ss.Id == (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange && ss.SendMessage);

                if (!exec)
                {
                    Logger.Info("CpmpkExchangeTask disabled");
                    Logger.Info("CpmpkExchangeTask finish");
                    return;
                }


                var requests = unitOfWork.GetSet<Request>().Where(r =>
                        r.NeedSendForCPMPK && !r.IsDeleted && !r.NeedSendForBenefit && !r.NeedSendToRelative &&
                        !r.NeedSendForSnils && !r.NeedSendForPassport).Include(r => r.Child)
                    .Take(1000).ToList();

                if (string.IsNullOrWhiteSpace(Config.Url))
                {
                    Logger.Info("CpmpkExchangeTask not have url");
                    foreach (var request in requests)
                    {
                        request.NeedSendForCPMPK = false;
                    }

                    unitOfWork.SaveChanges();
                    Logger.Info("CpmpkExchangeTask finish");
                    return;
                }

                var accessToken = string.Empty;

                using (var httpClient = new HttpClient())
                {
                    using (var requestMessage =
                        new HttpRequestMessage(HttpMethod.Post, new Uri(Config.Url + "/app/auth")))
                    {
                        var jsonRequest = "{"
                                          + $"\"name\": \"{Config.Service}\", \"secret\": \"{Config.Secret}\""
                                          + "}";
                        requestMessage.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                        requestMessage.Headers.Add("X-API-KEY", Config.ApiKey);

                        using (var response = httpClient.SendAsync(requestMessage).Result)
                        {
                            var responseData = response.Content.ReadAsStringAsync().Result;
                            var json = JObject.Parse(responseData);
                            if (json.TryGetValue("accessToken", out var value))
                            {
                                accessToken = value.ToString();
                            }
                        }
                    }

                    foreach (var request in requests)
                    {
                        var children = request.Child.Where(c => c.TypeOfRestrictionId == Config.TypeOfRestrictionId).ToList();

                        if (children.Any())
                        {
                            unitOfWork.SendChangeStatusByEvent(request, RequestEventEnum.SendRequestInCPMPK);

                            foreach (var child in children)
                            {
                                ResetCheckChildInBaseRegistry(unitOfWork, child.Id,
                                    ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck);


                                var requestText =
                                    $"/app/document/do?fullName={HttpUtility.UrlEncode($"{child.LastName} {child.FirstName} {child.MiddleName}")}&dob={child.DateOfBirth.XmlToString()}";
                                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(Config.Url + requestText)))
                                {
                                    requestMessage.Headers.Authorization =
                                        new AuthenticationHeaderValue("Bearer", accessToken);
                                    requestMessage.Headers.Add("X-API-KEY", Config.ApiKey);
                                    using (var response = httpClient.SendAsync(requestMessage).Result)
                                    {
                                        var responseData = response.Content.ReadAsStringAsync().Result;
                                        var dto = JsonConvert.DeserializeObject<CpmpkResponseDto>(responseData);

                                        foreach (var bi in child.BaseRegistryInfo.Where(b =>
                                            !b.NotActual && b.ExchangeBaseRegistryTypeId ==
                                            (long)ExchangeBaseRegistryTypeEnum.CpmpkExchange).ToList())
                                        {
                                            bi.NotActual = true;
                                        }

                                        unitOfWork.AddEntity(new RestChild.Domain.ExchangeBaseRegistry
                                        {
                                            RequestGuid = Guid.NewGuid().ToString(),
                                            ChildId = child.Id,
                                            RequestText = requestText,
                                            ResponseText = responseData,
                                            SendDate = DateTime.Now,
                                            ResponseDate = DateTime.Now,
                                            IsProcessed = false,
                                            IsIncoming = false,
                                            OperationType = "cpmpkrequest",
                                            Success = dto.Available && (dto.Aoop ?? false),
                                            ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.CpmpkExchange,
                                            ServiceNumber = "б/н",
                                            ResponseGuid = "б/н"
                                        });
                                    }
                                }
                            }

                            unitOfWork.SendChangeStatusByEvent(request, RequestEventEnum.GetResponseForCPMPK);
                        }

                        request.NeedSendForCPMPK = false;
                        unitOfWork.SaveChanges();
                    }
                }

                Logger.Info("CpmpkExchangeTask finished");
            }
        }
    }
}
