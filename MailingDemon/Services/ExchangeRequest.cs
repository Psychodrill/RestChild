using System.Runtime.Serialization;

namespace MailingDemon.Services
{
    /// <summary>
    ///     пакет для запроса
    /// </summary>
    [DataContract]
    public class ExchangeRequest
    {
        /// <summary>
        ///     ключ
        /// </summary>
        [DataMember(Name = "key")]
        public string Key { get; set; }

        /// <summary>
        ///     тип персонала
        /// </summary>
        [DataMember(Name = "personalTypeId", EmitDefaultValue = false)]
        public long PersonalTypeId { get; set; }

        /// <summary>
        ///     последнее обновление
        /// </summary>
        [DataMember(Name = "lut")]
        public long LastUpdateTick { get; set; }

        /// <summary>
        ///     ИД
        /// </summary>
        [DataMember(Name = "id")]
        public long Id { get; set; }
    }
}
