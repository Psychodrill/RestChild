namespace RestChild.Comon.Enumeration
{
    public enum EidSendStatusEnum
    {
        /// <summary>
        ///     не изменена
        /// </summary>
        NotChanged = 0,

        /// <summary>
        ///     изменена
        /// </summary>
        Changed = 1,

        /// <summary>
        ///     обновлена без ключей
        /// </summary>
        UpdateedWithNoFk = 2,

        /// <summary>
        ///     обновлена
        /// </summary>
        Updateed = 3
    }
}
