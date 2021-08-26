namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Виды услуг.
    /// </summary>
    public enum ServiceEnum
    {
        /// <summary>
        ///     Транспорт в место отдыха
        /// </summary>
        TransportTo = 1,

        /// <summary>
        ///     транспорт из места отдыха
        /// </summary>
        TransportFrom = 2,

        /// <summary>
        ///     Экскурсия
        /// </summary>
        Excursion = 3,

        /// <summary>
        ///     Виза
        /// </summary>
        Visa = 4,

        /// <summary>
        ///     Трансфер авиа
        /// </summary>
        TransferAero = 5,

        /// <summary>
        ///     Трансфер поезд
        /// </summary>
        TransferTrain = 12,

        /// <summary>
        ///     Трансфер авто
        /// </summary>
        TransferAuto = 13,

        /// <summary>
        ///     Профильные лагеря дети
        /// </summary>
        SpecializedPlaceChild = 6,

        /// <summary>
        ///     Профильные лагеря дети - транспорт
        /// </summary>
        SpecializedTransportChild = 7,

        /// <summary>
        ///     Профильные лагеря педагоги/тренера
        /// </summary>
        SpecializedPlaceAttendant = 8,

        /// <summary>
        ///     Профильные лагеря педагоги/тренера - транспорт
        /// </summary>
        SpecializedTransportAttendant = 9,

        /// <summary>
        ///     Страховка
        /// </summary>
        Insurance = 10,


        /// <summary>
        ///     Дополнительное место
        /// </summary>
        AddonPlace = 11,

        /// <summary>
        ///     Прочее
        /// </summary>
        Other = 999
    }
}
