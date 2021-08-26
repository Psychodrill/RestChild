namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     виды льгот
    /// </summary>
    public enum BenefitTypeEnum : long
    {
        /// <summary>
        ///     Дети из семей, в которых оба или один из родителей являются инвалидами
        /// </summary>
        ParentsInvalid = 9,

        /// <summary>
        ///     Дети из малообеспеченных семей
        /// </summary>
        LowIncomeFamily = 8,

        /// <summary>
        ///     Дети из малообеспеченных семей на 2017
        /// </summary>
        LowIncomeFamily2017 = 44,

        /// <summary>
        ///     Дети-сироты или дети оставшиеся без попечения родителей воспитывающихся в приемных или патронатных семьях,
        ///     принявших на воспитание трех и более детей
        /// </summary>
        CompensationOrphan = 18,

        /// <summary>
        ///     Сироты
        /// </summary>
        Orphans = 38
    }
}
