using System.ServiceModel;
using System.ServiceModel.Web;
using RestChild.Comon.Services;
using RestChild.Mobile.Domain;

namespace MailingDemon.Services
{
    [ServiceContract(Name = "IExchangeService")]
    public interface IExchangeServiceEx
    {
        /// <summary>
        ///     список для удаления
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetBoutToRemove")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "boutr")]
        long[] GetBoutToRemove(ExchangeRequest package);

        /// <summary>
        ///     заезды
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetBouts")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "bouts")]
        BoutEx[] GetBouts(ExchangeRequest package);

        /// <summary>
        ///     лагеря
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetCampToRemove")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "campr")]
        long[] GetCampToRemove(ExchangeRequest package);

        /// <summary>
        ///     лагеря
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetCamps")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "camps")]
        Camp[] GetCamps(ExchangeRequest package);

        /// <summary>
        ///     отряды
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetPartyToRemove")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "partyr")]
        long[] GetPartyToRemove(ExchangeRequest package);

        /// <summary>
        ///     отряды
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetParty")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "party")]
        Party[] GetParty(ExchangeRequest package);

        /// <summary>
        ///     персонал
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetPersonalToRemove")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "personalr")]
        long[] GetPersonalToRemove(ExchangeRequest package);

        /// <summary>
        ///     персонал
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetPersonal")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "personals")]
        Personal[] GetPersonal(ExchangeRequest package);

        /// <summary>
        ///     отдыхающие
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetCamperToRemove")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "camperr")]
        long[] GetCamperToRemove(ExchangeRequest package);

        /// <summary>
        ///     отдыхающие
        /// </summary>
        [OperationContract(Action = "http://tempuri.org/IExchangeService/GetCampers")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "campers")]
        Camper[] GetCampers(ExchangeRequest package);
    }
}
