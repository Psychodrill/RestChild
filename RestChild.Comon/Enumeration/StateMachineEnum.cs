namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     список машин состояний.
    /// </summary>
    public enum StateMachineEnum
    {
        #region квоты по одаренным детям

        /// <summary>
        ///     статусы для списка организации
        /// </summary>
        LimitListState = 1,

        /// <summary>
        ///     квота по организации
        /// </summary>
        LimitOrganizationState = 2,

        /// <summary>
        ///     квота по ОИВ
        /// </summary>
        LimitOivState = 3,

        /// <summary>
        ///     общегородская квота.
        /// </summary>
        LimitGeneralState = 4,

        #endregion


        /// <summary>
        ///     Статус блока мест
        /// </summary>
        TourState = 5,

        /// <summary>
        ///     Статус места отдыха
        /// </summary>
        HotelState = 6,

        /// <summary>
        ///     Статус вожатого
        /// </summary>
        CounselorState = 7,

        /// <summary>
        ///     Статус контракта
        /// </summary>
        ContractState = 8,

        /// <summary>
        ///     Статус заезда
        /// </summary>
        BoutState = 9,

        /// <summary>
        ///     Статус отряда
        /// </summary>
        PartyState = 10,

        /// <summary>
        ///     Статус рейса
        /// </summary>
        DirectoryFlightsState = 11,

        /// <summary>
        ///     Транспорт
        /// </summary>
        TransportState = 12,

        /// <summary>
        ///     Дополнительные услуги
        /// </summary>
        AddonServiceState = 13,

        /// <summary>
        ///     Начисление
        /// </summary>
        CalculationState = 14,

        /// <summary>
        ///     Связь с услугой
        /// </summary>
        AddonServiceLinkState = 15,

        /// <summary>
        ///     задания вожатым
        /// </summary>
        CounselorTask = 16,

        /// <summary>
        ///     Администратор заезда
        /// </summary>
        AdministratorTour = 17,

        /// <summary>
        ///     тестирование вожатых
        /// </summary>
        CounselorTest = 18,

        /// <summary>
        ///     группы обчения вожатых
        /// </summary>
        TrainingCounselors = 19,

        /// <summary>
        ///     Педотряд
        /// </summary>
        PedParty = 20,

        /// <summary>
        ///     Профильники. Заявки на квоту
        /// </summary>
        LimitRequest = 21,

        /// <summary>
        ///     платежи
        /// </summary>
        Payments = 22,

        /// <summary>
        ///     Заявки
        /// </summary>
        Request = 23,

        /// <summary>
        ///     Список профсоюза
        /// </summary>
        TradeUnionList = 24,

        /// <summary>
        ///     Группы (потребности) приютов
        /// </summary>
        PupilGroup = 25,

        /// <summary>
        ///     Списки (группы отправки) приютов
        /// </summary>
        //PupilGroupList = 26,

        /// <summary>
        ///     Сертификаты
        /// </summary>
        Certificate = 27,

        /// <summary>
        ///     Мониторинг. Cведения о численности детей
        /// </summary>
        MonitoringMonitoringChildrenNumberInformation = 28,

        /// <summary>
        ///     Мониторинг. Cведения о финансировании оздоровительной кампании
        /// </summary>
        MonitoringFinanceInformation = 29,


        /// <summary>
        ///     Мониторинг. Сведения о малых формах занятости детей
        /// </summary>
        MonitoringSmallLeisureInfoData = 30,
    }
}
