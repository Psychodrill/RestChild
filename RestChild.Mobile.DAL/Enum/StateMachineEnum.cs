namespace RestChild.Mobile.DAL.Enum
{
    /// <summary>
    ///     машина статусов
    /// </summary>
    public static class StateMachineEnum
    {
        /// <summary>
        ///     статус задания
        /// </summary>
        public const long CamperTask = 1;

        /// <summary>
        ///     статус задания
        /// </summary>
        public const long BoutTask = 2;

        /// <summary>
        ///     уведомления
        /// </summary>
        public const long Notification = 3;

        /// <summary>
        ///     Заезд
        /// </summary>
        public const long Bout = 4;

        /// <summary>
        ///     Отряд
        /// </summary>
        public const long Party = 5;

        /// <summary>
        ///     Объект отдыха
        /// </summary>
        public const long Camp = 6;

        /// <summary>
        ///     Персонал
        /// </summary>
        public const long Personal = 7;

        /// <summary>
        ///     Администратор смены
        /// </summary>
        public const long AdministratorTour = 8;

        /// <summary>
        ///     Задание
        /// </summary>
        public const long Task = 9;

        /// <summary>
        ///     Подарок
        /// </summary>
        public const long Gift = 10;

        /// <summary>
        ///     Резервирование подарка
        /// </summary>
        public const long GiftReserved = 11;

        /// <summary>
        ///     Задание на лагерь
        /// </summary>
        public const long CampTask = 12;
    }
}
