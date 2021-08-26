namespace RestChild.Comon.Enumeration
{
    public enum NotNeedTicketReasonEnum
    {
        /// <summary>
        ///     Прибыл в место отдыха
        /// </summary>
        ArrivedAtPlaceOfRest = 0,

        /// <summary>
        ///     Добирается самостоятельно
        /// </summary>
        ComeSingly = 1,

        /// <summary>
        ///     Госпитализирован
        /// </summary>
        Hospitalized = 2,

        /// <summary>
        ///     Не прибыл в место отдыха
        /// </summary>
        NotCome = 3,

        /// <summary>
        ///     Выехал досрочно
        /// </summary>
        LeftEarly = 4
    }
}
