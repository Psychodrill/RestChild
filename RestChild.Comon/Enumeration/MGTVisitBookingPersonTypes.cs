namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Типы людей (заявитель(взрослый)/ребенок) в брони на визит в МГТ
    /// </summary>
    public enum MGTVisitBookingPersonTypes : long
    {
        //заявитель
        Declarant = 1,

        //ребёнок
        Child = 2
    }
}
