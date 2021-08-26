using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.SMS
{
    /// <summary>
    ///     Результат запроса
    /// </summary>
    [DataContract]
    [Serializable]
    public struct ResponseMessageResult
    {
        /// <summary>
        ///     Идентификатор сообщения системы провайдера.
        ///     Указывается, если был передан в запросе.
        /// </summary>
        [DataMember(Name = "ext_message_id ")]
        public string ExtMessageId  { get; set; }

        /// <summary>
        ///     Идентификатор сообщения системы ЕМП.
        /// </summary>
        [DataMember(Name = "emp_message_id")]
        public string EmpMessageId { get; set; }
    }
}
