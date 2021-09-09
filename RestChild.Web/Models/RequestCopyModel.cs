using System.Collections;
using System.Collections.Generic;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     Класс для копирования заявления
    /// </summary>
    public class RequestCopyModel
    {
        /// <summary>
        ///     Идентификатор заявления из которого осуществляется копирование
        /// </summary>
        public long RequestId { get; set; }

        /// <summary>
        ///     Передавать данные раздела "Общие сведения о заявлении"
        /// </summary>
        public bool TransferGeneralData{ get; set; }

        /// <summary>
        ///     Передавать данные раздела "Тип транспорта"
        /// </summary>
        public bool TransferTypeOfTransportData{ get; set; }

        /// <summary>
        ///     Передавать данные раздела "Тип лагеря"
        /// </summary>
        public bool TransferTypeOfCampData{ get; set; }

        /// <summary>
        ///     Передавать данные раздела "Цель обращения и время отдыха"
        /// </summary>
        public bool TransferTargetAndTimeOfRestData{ get; set; }

        /// <summary>
        ///     Передавать данные раздела "Сведения о заявителе"
        /// </summary>
        public bool TransferApplicantData { get; set; }

        /// <summary>
        ///     Передавать данные раздела "Сведения о представителе заявителе"
        /// </summary>
        public bool TransferAgentData { get; set; }

        /// <summary>
        ///     Передавать данные раздела "Банковские реквизиты"
        /// </summary>
        public bool TransferBankData { get; set; }

        /// <summary>
        ///     Передавать данные раздела "Документы"
        /// </summary>
        public bool TransferFilesData { get; set; }

        /// <summary>
        ///     Коллекция идентификаторов сопровождающих, которые будут скопированы
        /// </summary>
        public List<long> AttendantsIds { get; set; }

        /// <summary>
        ///     Коллекция идентификаторов детей, которые будут скопированы
        /// </summary>
        public List<long> ChildrenIds { get; set; }

    }
}
