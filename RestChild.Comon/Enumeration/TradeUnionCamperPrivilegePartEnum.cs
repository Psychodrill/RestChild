namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Кэшбэк признак льготы
    /// </summary>
    public enum TradeUnionCamperPrivilegePartEnum : long
    {
        /// <summary>
        ///     100% льгота
        /// </summary>
        FullPrivilege = 1,

        /// <summary>
        ///     Не льготная категория
        /// </summary>
        NoPrivilege = 2,

        /// <summary>
        ///     Льготы менее 100% стоимости
        /// </summary>
        PartlyPrivilege = 3,
    }
}
