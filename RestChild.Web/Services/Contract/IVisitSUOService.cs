using System.ServiceModel;

namespace RestChild.Web.Services.Contract
{
    /// <summary>
    ///     Сервис работы с СУО
    /// </summary>
    [ServiceContract]
    public interface IVisitSUOService
    {
        /// <summary>
        ///     Получить данные по ПИН коду
        /// </summary>
        [OperationContract]
        Models.VisitQueue.VisitSUOResult GetSUOVisitData(int PINCode);
    }
}
