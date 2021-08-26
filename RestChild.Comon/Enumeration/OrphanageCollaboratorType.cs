namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Типы сотрудников детского дома
    /// </summary>
    public enum OrphanageCollaboratorType : long
    {
        //Директор
        Director = 1,

        //Ответственный за отдых
        ResponsibleForRest = 2,

        //Дополнительное контактное лицо
        AdditionalContactPerson = 3,

        //Сопровождающий
        Attendant = 4
    }
}
