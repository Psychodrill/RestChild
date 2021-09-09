using System;
using System.Collections.Generic;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange.Passport;
using RestChild.Comon.Exchange.PassportRegistration;
using RestChild.Comon.Exchange.Snils;
using RestChild.Comon.Exchange.Zagz;

namespace RestChild.Comon.Dto
{
    /// <summary>
    ///     результат проверки в базовом регистре
    /// </summary>
    public class BaseRegistryCheckResult
    {
        /// <summary>
        ///     ребёнок
        /// </summary>
        public IEntityBase Child { get; set; }

        /// <summary>
        ///     сопровождающий
        /// </summary>
        public IEntityBase Applicant { get; set; }


        /// <summary>
        ///     дата запроса
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        ///     номер запроса
        /// </summary>
        public string RequestNumber { get; set; }

        /// <summary>
        ///     тип запроса
        /// </summary>
        public ExchangeBaseRegistryTypeEnum Type { get; set; }

        /// <summary>
        ///     ответа нет.
        /// </summary>
        public bool ResultAbsent { get; set; }

        /// <summary>
        ///     подтверждено или нет.
        /// </summary>
        public bool? Approved { get; set; }

        /// <summary>
        ///     подтверждена мало обеспеченность
        /// </summary>
        public bool? ApprovedLowIncome { get; set; }

        /// <summary>
        ///     заметка
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     проверка льготы запрос
        /// </summary>
        public BenefitCheckRequest BenefitCheckRequest { get; set; }

        /// <summary>
        ///     результат проверки по льготе
        /// </summary>
        public List<ResidentPreferentialCategories> BenefitCheckResult { get; set; }

        /// <summary>
        ///     проверка по родственным связям
        /// </summary>
        public List<RelationshipCheckResult> RelationshipCheckResults { get; set; }

        /// <summary>
        ///     проверка выплат
        /// </summary>
        public List<PaymentCheckResult> PaymentCheckResults { get; set; }

        /// <summary>
        ///     проверка СНИЛС
        /// </summary>
        public SnilsCheckResult SnilsCheckResult { get; set; }

        /// <summary>
        ///     ответ по ЗАГС от СМЭВ
        /// </summary>
        public informResponse SmevZagzResponse { get; set; }

        /// <summary>
        ///     ответ по паспортам
        /// </summary>
        public OutPassportType Passport { get; set; }

        /// <summary>
        ///     данные по СНИЛС
        /// </summary>
        public SnilsByAdditionalDataResponse SnilsInfo { get; set; }

        /// <summary>
        ///     ответ от АС УР по регистрации по паспортным данным
        /// </summary>
        public Registration PassportRegistrationResponse { get; set; }

        /// <summary>
        ///     Результат проверки в АИС ДО
        /// </summary>
        public string AisoLegalRepresentationCheck { get; set; }

        /// <summary>
        /// статус разбора сообщения
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     Результат проверки в ФРИ
        /// </summary>
        public DisabilityExtractResponse FRIResponse { get; set; }
    }
}
