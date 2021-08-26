namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     статусы организации
    /// </summary>
    public enum OrganizationStatusEnum
    {
        /// <summary> Новая 2</summary>
        StatusActual = 2,

        /// <summary> Удаленный 4</summary>
        StatusDeleted = 4,

        /// <summary> В процессе закрытия 3</summary>
        StatusClosing = 3,

        /// <summary> В процессе открытия 1</summary>
        StatusOpening = 1
    }
}
