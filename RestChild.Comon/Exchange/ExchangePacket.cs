using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Exchange
{
    [Serializable]
    [DataContract(Name = "ep")]
    public class ExchangePacket
    {
        /// <summary>
        ///     Результат
        /// </summary>
        [DataMember(Name = "r", EmitDefaultValue = false)]
        public bool? Result { get; set; }

        [DataMember(Name = "ef", EmitDefaultValue = false)]
        public string ExtractFiled { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        [DataMember(Name = "n", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     ключ
        /// </summary>
        [DataMember(Name = "k", EmitDefaultValue = false)]
        public string Key { get; set; }

        /// <summary>
        ///     Идентификатор пакета
        /// </summary>
        [DataMember(Name = "pid", EmitDefaultValue = false)]
        public long? PacketId { get; set; }

        /// <summary>
        ///     Идентификатор
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        ///     внешний идентификатор
        /// </summary>
        [DataMember(Name = "eid", EmitDefaultValue = false)]
        public long? Eid { get; set; }

        /// <summary>
        ///     последнее измененние
        /// </summary>
        [DataMember(Name = "lut", EmitDefaultValue = false)]
        public long? LastUpdateTick { get; set; }

        /// <summary>
        ///     пакет
        /// </summary>
        [DataMember(Name = "p", EmitDefaultValue = false)]
        public string Packet { get; set; }
    }
}
