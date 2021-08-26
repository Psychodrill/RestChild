namespace RestChild.Comon.Enumeration
{
    public enum StatusEnum
    {
        /// <summary>
        ///     черновик
        /// </summary>
        Draft = -1,

        /// <summary>
        ///     В работе
        /// </summary>
        OnWorking = -3,

        /// <summary>
        ///     Готово к полной оплате
        /// </summary>
        ReadyToPayFull = -4,

        /// <summary>
        ///     Запрошено
        /// </summary>
        OnApprove = -5,

        /// <summary>
        ///     Оплачен аванс
        /// </summary>
        Payed = -6,

        /// <summary>
        ///     Отказ
        /// </summary>
        Denial = -7,

        /// <summary>
        ///     Готово к оплате аванса
        /// </summary>
        ReadyToPay = -2,

        /// <summary>
        ///     ошибочное сообщение
        /// </summary>
        ErrorRequest = -100,

        /// <summary>
        ///     Заявление зарегистрировано
        /// </summary>
        Send = 1050,

        /// <summary>
        ///     Ожидание прихода заявителя
        /// </summary>
        WaitApplicant = 1055,

        /// <summary>
        ///     На исполнении. Формирование результата предоставления услуги.
        /// </summary>
        ApplicantCome = 1052,

        /// <summary>
        ///     Услуга оказана.
        /// </summary>
        CertificateIssued = 1075,

        /// <summary>
        ///     Услуга оказана. Отказ в предоставлении услуги / Аннулировано
        /// </summary>
        Reject = 1080,

        /// <summary>
        ///     Отозвано по инициативе заявителя
        /// </summary>
        CancelByApplicant = 1090,

        /// <summary>
        ///     проверка оператором.
        /// </summary>
        OperatorCheck = 7704,

        /// <summary>
        ///     Отказ в регистрации
        /// </summary>
        RegistrationDecline = 1030,

        /// <summary>
        ///     Ранжирование
        /// </summary>
        Ranging = 1051,

        /// <summary>
        ///     Включено в список
        /// </summary>
        IncludedInList = 1054,

        /// <summary>
        ///     Принятие решения
        /// </summary>
        DecisionMaking = 8021,

        /// <summary>
        ///     Принятие решения (в связи с COVID-19)
        /// </summary>
        DecisionMakingCovid = 80211,

        /// <summary>
        ///     Решение принято
        /// </summary>
        DecisionIsMade = 8011,

        /// <summary>
        ///     Вызов для подтверждения банковских реквизитов
        /// </summary>
        WaitApplicantMoney = 1046,

        /// <summary>
        ///     Окончание первого этапа компании
        /// </summary>
        EndOfCompanyFirstStep = 1148
    }
}
