using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Addressing
{
    [DataContract]
    [Serializable]
    public class RegionBtiDTO : BaseBtiDTO
    {
        /// <summary>
        ///     ИД района по GIVZ.
        /// </summary>
        [DataMember]
        public virtual int? Givz { get; set; }

        /// <summary>
        ///     Наименование района.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }
    }
}
