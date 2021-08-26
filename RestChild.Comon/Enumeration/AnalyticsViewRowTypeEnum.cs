namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Формат вывода аналитики
    /// </summary>
    public enum AnalyticsViewRowTypeEnum
    {
        /// <summary>
        ///     Четыре столбца за час, за сутки, за неделю, всего
        /// </summary>
        FourColumns = 1,

        /// <summary>
        ///     Четыре пары столбцов  за час, за сутки, за неделю, всего
        /// </summary>
        EightColumns = 2,

        /// <summary>
        ///     Один столбец
        /// </summary>
        OneColumn = 3,

        /// <summary>
        ///     Один столбец две цифры
        /// </summary>
        TwoColumn = 4,

        /// <summary>
        ///     по датам.
        /// </summary>
        ByDays = 5
    }
}
