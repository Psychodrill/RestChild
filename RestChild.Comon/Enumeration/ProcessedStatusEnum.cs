namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     статус обработки сообщения обмена
    /// </summary>
    public enum ProcessedStatusEnum
    {
        /// <summary>
        ///     не обработано
        /// </summary>
        Unprocessed = 1,

        /// <summary>
        ///     считаны данные из ЕКИС
        /// </summary>
        EkisDataReaded = 2,

        /// <summary>
        ///     Данные обработаны из ЕКИС
        /// </summary>
        EkisDataProcessed = 3,

        /// <summary>
        ///     Отправлены данные в ЕКИС
        /// </summary>
        EkisSend = 4,

        /// <summary>
        ///     Обработан ответ из ЕКИС
        /// </summary>
        EkisProcessed = 5
    }
}
