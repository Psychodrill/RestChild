using System.Runtime.Serialization;

namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     шаги бронирования.
    /// </summary>
    [DataContract]
    public enum SelectPlaceStepEnum
    {
        /// <summary>
        ///     выбор начальнх параметров.
        /// </summary>
        [DataMember] FirstSelectTypeTimeAndPlace = 0,

        /// <summary>
        ///     выбор организации.
        /// </summary>
        [DataMember] SecondSelectOrganization = 1,

        /// <summary>
        ///     выбор размещения.
        /// </summary>
        [DataMember] ThridSelectTimeAndPlacment = 2,

        /// <summary>
        ///     Выбрали денежную компенсацию.
        /// </summary>
        [DataMember] ThridEnterMoneyAccount = 4
    }
}
