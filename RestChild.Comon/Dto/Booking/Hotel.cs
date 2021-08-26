using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     результат поиска услуги.
    /// </summary>
    [DataContract(Name = "hotel")]
    public class Hotel
    {
        private string _key;

        public Hotel()
        {
        }

        /// <summary>
        ///     копия объекта.
        /// </summary>
        public Hotel(Hotel source, long?[] timeOfRest, Tuple<DateTime, DateTime>[] dates, int main, int attendant,
            bool withBookingDate, bool findAdditional)
        {
            HotelId = source.HotelId;
            TypeOfRestId = source.TypeOfRestId;
            PlaceOfRestId = source.PlaceOfRestId;
            SubjectOfRestId = source.SubjectOfRestId;
            Name = source.Name;
            Address = source.Address;
            Description = source.Description;
            AccessibleEnvironment = source.AccessibleEnvironment;
            Photos = source.Photos.Select(p => new FileLink(p)).ToList();
            ForFirstRequest = source.ForFirstRequest;
            Group = source.Group;
            TimeOfRests =
                source.TimeOfRests
                    .Where(t => timeOfRest == null || !timeOfRest.Any() || timeOfRest.Contains(t.TimeOfRestId))
                    .Where(t => dates == null || dates.All(d => d.Item1 <= t.Start && d.Item2 > t.Start))
                    .Select(p => new TimeOfRest(p, main, attendant, withBookingDate, findAdditional))
                    .Where(p => p.LeftPlaces > 0)
                    .ToList();
        }

        /// <summary>
        ///     отель.
        /// </summary>
        [IgnoreDataMember]
        public long HotelId { get; set; }

        /// <summary>
        ///     ключ для места отдыха
        /// </summary>
        [DataMember(Name = "key")]
        public string Key
        {
            get => string.IsNullOrEmpty(_key)
                ? $"{HotelId}_{TypeOfRestId}_{SubjectOfRestId}_{ForFirstRequest}_{Group}"
                : _key;
            set => _key = value;
        }

        /// <summary>
        ///     Доступная среда
        /// </summary>
        [DataMember(Name = "accessibleEnvironment")]
        public bool AccessibleEnvironment { get; set; }

        /// <summary>
        ///     вид отдыха
        /// </summary>
        [DataMember(Name = "typeOfRestId")]
        public long TypeOfRestId { get; set; }

        /// <summary>
        ///     тематика смены
        /// </summary>
        [DataMember(Name = "subjectOfRestId")]
        public long? SubjectOfRestId { get; set; }

        /// <summary>
        ///     место отдыха
        /// </summary>
        [DataMember(Name = "placeOfRestId")]
        public long PlaceOfRestId { get; set; }

        /// <summary>
        ///     Наименование места отдыха
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     Адрес места отдыха
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        ///     описание места отдыха
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     фотографии места отдыха
        /// </summary>
        [DataMember(Name = "photos")]
        public List<FileLink> Photos { get; set; }

        /// <summary>
        ///     времена отдыха
        /// </summary>
        [DataMember(Name = "timeOfRests")]
        public List<TimeOfRest> TimeOfRests { get; set; }

        /// <summary>
        ///     для первого этапа кампании.
        /// </summary>
        [IgnoreDataMember]
        public bool ForFirstRequest { get; set; }

        /// <summary>
        ///     для колясочников.
        /// </summary>
        [IgnoreDataMember]
        public long Group { get; set; }
    }
}
