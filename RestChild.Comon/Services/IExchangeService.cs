using System.ServiceModel;
using System.ServiceModel.Web;
using RestChild.Comon.Exchange;

namespace RestChild.Comon.Services
{
    [ServiceContract]
    public interface IExchangeService
    {
        /// <summary>
        ///     Пакеты
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "pkts?pkId={id}&pkname={name}&k={key}")]
        ExchangePacket GetPacket(long id, string name, string key);

        /// <summary>
        ///     изменения
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "chgs?pkname={name}&k={key}")]
        long[] GetChangedIds(string name, string key);


        /// <summary>
        ///     список для удаления
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "rms?pkname={name}&k={key}")]
        ExchangePacket[] GetPacketsToRemove(string key, string name);

        /// <summary>
        ///     обработка
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "prcs?pkId={id}&pkname={name}&k={key}&lut={lastUpdateTick}")]
        void SetNotChanged(long id, string name, string key, long lastUpdateTick);

        /// <summary>
        ///     обработка
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "upkt")]
        ExchangePacket UpdatePacket(ExchangePacket packet);

        /// <summary>
        ///     удаление пакета
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "rm")]
        bool RemoveEntity(ExchangePacket packet);
    }
}
