using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     заявление для бронирования
    /// </summary>
    [DataContract(Name = "request")]
    public class RequestDto
    {
        [IgnoreDataMember] public long Id { get; set; }

        /// <summary>
        ///     вид отдыха
        /// </summary>
        [IgnoreDataMember]
        public long TypeOfRest { get; set; }

        /// <summary>
        ///     номер заявления
        /// </summary>
        [IgnoreDataMember]
        public string RequestNumber { get; set; }

        /// <summary>
        ///     количество детей
        /// </summary>
        [DataMember(Name = "places", EmitDefaultValue = false)]
        public int Places { get; set; }

        /// <summary>
        ///     Признак дополнительной кампании 2020
        /// </summary>
        [DataMember(Name = "covid", EmitDefaultValue = false)]
        public bool Covid19 { get; set; }

        /// <summary>
        ///     количество сопровождающих
        /// </summary>
        [DataMember(Name = "attendants", EmitDefaultValue = false)]
        public int Attendants { get; set; }

        /// <summary>
        ///     время отдыха
        /// </summary>
        [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
        public long?[] TimeOfRest { get; set; }

        /// <summary>
        ///     направления отдыха
        /// </summary>
        [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
        public long?[] PlaceOfRest { get; set; }

        /// <summary>
        ///     даты рождения
        /// </summary>
        [DataMember(Name = "ds", EmitDefaultValue = false)]
        public Tuple<DateTime, DateTime>[] Dates { get; set; }

        /// <summary>
        ///     забронировано
        /// </summary>
        [IgnoreDataMember]
        public bool Booked { get; set; }

        /// <summary>
        ///     группа
        /// </summary>
        [IgnoreDataMember]
        public long Group { get; set; }

        /// <summary>
        ///     могут быть деньги
        /// </summary>
        [DataMember(Name = "mayBeMoney")]
        public bool MayBeMoney { get; set; }
    }
}
