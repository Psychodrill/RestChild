using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Addressing
{
    [DataContract]
    [Serializable]
    public class DistrictBtiDTO : BaseBtiDTO
    {
        /// <summary>
        ///     ИД округа по GIVZ.
        /// </summary>
        [DataMember]
        public virtual int? Givz { get; set; }

        /// <summary>
        ///     Наименование округа.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }
    }
}
