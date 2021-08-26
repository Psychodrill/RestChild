using System.Runtime.Serialization;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     модель для поиска направлений отдыха
    /// </summary>
    [DataContract]
    public class PlaceOfRestsSearchModel
    {
        /// <summary>
        ///     Список ИД которые уже есть
        /// </summary>
        [DataMember(Name = "presentIds")]
        public long[] PresentIds { get; set; }

        /// <summary>
        ///     цель обращения
        /// </summary>
        [DataMember(Name = "tid")]
        public long? TypeOfRestId { get; set; }

        /// <summary>
        ///     подстрока
        /// </summary>
        [DataMember(Name = "t")]
        public string Term { get; set; }
    }
}
