using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.SMS
{
    /// <summary>
    ///     Результат отправки СМС сообщения
    /// </summary>
    [Serializable]
    [DataContract]
    public struct ResponseMessage
    {
        /// <summary>
        ///     Код ошибки
        /// </summary>
        [DataMember(Name = "errorCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        ///     Описание ошибки
        /// </summary>
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Результат запроса
        /// </summary>
        [DataMember(Name = "result")]
        public ICollection<ResponseMessageResult> Result { get; set; }
    }
}
