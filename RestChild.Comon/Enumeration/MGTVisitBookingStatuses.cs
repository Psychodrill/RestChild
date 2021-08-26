namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Статусы броней на визит в МГТ
    /// </summary>
    public enum MGTVisitBookingStatuses : long
    {
        //Предбронь зарегистрирована
        PrebookingRegistered = 1,

        //Предбронь аннулирована
        PrebookingCanceled = 2,

        //Заявление зарегистрировано
        BookingRegistered = 3,

        //Заявление отозвано заявителем
        BookingCanceled = 4,

        //Заявление отозвано по инициативе ГАУК Мосгортур
        BookingMGTCanceled = 5,

        //Прием осуществлен
        BookingVisited = 6,

        //Заявитель не явился на прием
        BookingUnvisited = 7
    }
}
