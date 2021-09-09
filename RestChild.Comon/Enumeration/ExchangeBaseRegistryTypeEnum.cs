namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     тип запроса в БР
    /// </summary>
    public enum ExchangeBaseRegistryTypeEnum
    {
        /// <summary>
        ///     льгота
        /// </summary>
        Benefit = 260,

        /// <summary>
        ///     родство
        /// </summary>
        Relationship = 22,

        /// <summary>
        ///     Предоставление из ЕГР ЗАГС сведений об актах гражданского состояния
        /// </summary>
        RelationshipSmev = 3091,

        /// <summary>
        ///     СНИЛС
        /// </summary>
        Snils = 10244,

        /// <summary>
        ///     СНИЛС
        /// </summary>
        Snils2040 = 2040,

        /// <summary>
        ///     Выплаты
        /// </summary>
        Payments = 10062,

        /// <summary>
        ///     Запроса паспортного досье по СНИЛС
        /// </summary>
        PassportDataBySNILS = 10209,

        /// <summary>
        ///     Запрос снилс по фио
        /// </summary>
        SNILSByFio = 8255,

        /// <summary>
        ///     обмен с ЦПМПК
        /// </summary>
        CpmpkExchange = -1,

        /// <summary>
        ///     Проверка действительности регистрации по месту пребывания
        /// </summary>
        PassportRegistration = 10214,

        /// <summary>
        ///     Получение регистрации по месту жительства
        /// </summary>
        GetPassportRegistration = 10211,

        /// <summary>
        ///     Проверка законного представительства внутри АИС ДО
        /// </summary>
        AisoLegalRepresentationCheck = -2,

        /// <summary>
        ///     Проверка в ФРИ сведений об инвалидности
        /// </summary>
        FRIExchange = 2043
    }
}
