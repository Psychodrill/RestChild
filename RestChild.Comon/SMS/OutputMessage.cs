using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.SMS
{
    /// <summary>
    ///     СМС сообщение которое будет отправлено
    /// </summary>
    [Serializable]
    [DataContract]
    public struct OutputMessage
    {
        /// <summary>
        ///     Наименование маски
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        ///     Номер получателя, Указывается в международном формате без знака «+»
        /// </summary>
        [DataMember(Name = "destination")]
        public string Destination { get; set; }

        /// <summary>
        ///     Текст сообщения
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        ///     Идентификатор сообщения системы провайдера
        /// </summary>
        [DataMember(Name = "ext_message_id")]
        public string ExtMessageId { get; set; }
    }
}
