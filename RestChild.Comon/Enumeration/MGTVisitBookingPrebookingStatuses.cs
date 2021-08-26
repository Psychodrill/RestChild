namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Статусы преброней
    /// </summary>
    public enum MGTVisitBookingPrebookingStatuses : long
    {
        //Пребронь на данного человека уже существует
        PrebookingExists = 300,

        //Пребронь произведена успешно
        PrebookingSucsess = 200,

        //Нет свободных окон
        NoVacantSlots = 400,

        //Техническая ошибка
        TechError = 500
    }
}
