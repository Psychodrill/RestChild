namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Вид группы проверки
    /// </summary>
    public enum TypeOfGroupCheckEnum
    {
        /// <summary>
        ///     без проверок в рамках одного или нескольких лет
        /// </summary>
        NotCheck = 0,

        /// <summary>
        ///     Проверка в рамках одного года (индивидуальный отдых, малообеспеченные семьи, дети-инвалиды)
        /// </summary>
        ForOneYear = 1,

        /// <summary>
        ///     Проверка в рамках двух лет (дети оставшиеся без попечения родителей)
        /// </summary>
        ForTwoYears = 2,

        /// <summary>
        ///     Проверка в рамках одного года (профильные лагеря)
        /// </summary>
        ForSpecializedCamps = 3
    }
}
