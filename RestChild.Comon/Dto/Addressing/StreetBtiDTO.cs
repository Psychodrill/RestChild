using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Addressing
{
    /// <summary>
    ///     улица.
    /// </summary>
    [DataContract]
    [Serializable]
    public class StreetBtiDTO : BaseBtiDTO
    {
        /// <summary>
        ///     Наименование улицы.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }
    }
}
