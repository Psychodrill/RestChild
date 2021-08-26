namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     коды ответа БР
    /// </summary>
    public static class ExchangeBaseRegistryResponseStatusEnum
    {
        /// <summary>
        ///     Получен запрос в ФОИВ/ОИВ на получение документа (промежуточный)
        /// </summary>
        public static int GetRequest = 1000;

        /// <summary>
        ///     Сервис получения документа не реализован
        /// </summary>
        public static int NotImplemented = 1001;

        /// <summary>
        ///     Ошибка при обработке запроса в ФОИВ/ОИВ на получение документа
        /// </summary>
        public static int ErrorInProcess = 1002;

        /// <summary>
        ///     Запрос принят ФОИВ/ОИВ Статус факта отправки в СМЭВ, в ответе возвращаем MessageID СМЭВ для разбора инцидента
        /// </summary>
        public static int RequestAccepted = 1003;

        /// <summary>
        ///     Получен ответ по запросу документа Возвращается в случае получения ответа от поставщика
        /// </summary>
        public static int ReceivedResponse = 1004;

        /// <summary>
        ///     Запрашиваемые сведения не найдены NO_DATA
        /// </summary>
        public static int NoData = 1005;

        /// <summary>
        ///     Неверные/неполные параметры для запроса документа UNKNOWN_REQUEST_DESCRIPTION Возвращается в случае ошибки ФЛК
        ///     любого уровня:
        /// </summary>
        public static int InvalidParameters = 1006;

        /// <summary>
        ///     Сервис предоставления сведений в данный момент не доступен. Выполнено
        ///     n попыток подключения. Попытки подключения будут продолжены (промежуточный)
        /// </summary>
        public static int ServiceUnavailable = 1007;

        /// <summary>
        ///     Истек регламентный срок предоставления документа. Запрос результатов будет продолжен в установленном порядке
        ///     (конечный)
        /// </summary>
        public static int DeadlineExpired = 1008;

        /// <summary>
        ///     Недостаточно прав доступа для запроса документа
        /// </summary>
        public static int NotEnoughRights = 1009;
    }
}
