using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Extensions.Services
{
    /// <summary>
    ///     получение заявлений
    /// </summary>
    public interface IRequestSaver
    {
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        ///     сохранение заявления
        /// </summary>
        /// <param name="data"></param>
        /// <param name="needCreateVersion"></param>
        /// <param name="saveFileOnly"></param>
        Request SaveRequest(Request data, bool needCreateVersion = false, bool saveFileOnly = false);

        /// <summary>
        ///     Выставление юнитофворка
        /// </summary>
        /// <param name="unitOfWork"></param>
        void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork);

        /// <summary>
        ///     изменение статуса заявления.
        /// </summary>
        bool RequestChangeStatus(long requestId, string actionCode, long? declineReason = null);
    }
}
