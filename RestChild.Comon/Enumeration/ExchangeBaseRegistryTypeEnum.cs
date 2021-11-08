namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     тип запроса в БР
    /// </summary>
    public enum ExchangeBaseRegistryTypeEnum
    {
        /// <summary>
        ///     Наличие льготной категории
        /// </summary>
        Benefit = 260,

        /// <summary>
        ///     Проверка родства
        /// </summary>
        Relationship = 22,

        /// <summary>
        ///     Предоставление из ЕГР ЗАГС сведений об актах гражданского состояния(не используется)
        /// </summary>
        RelationshipSmev = 3091,

        /// <summary>
        ///     Проверка СНИЛС
        /// </summary>
        Snils = 10244,

        /// <summary>
        ///     Проверка СНИЛС(2040)
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
        FRIExchange = 2043,

        /// <summary>
        ///     Получение данных из ЕГР ЗАГС
        /// </summary>
        GetEGRZAGS = 11827,

        /// <summary>
        ///     Выписка сведений об инвалиде FGIS FRI
        /// </summary>
        GetFGISFRI = 12150
    }
}
