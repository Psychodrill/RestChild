using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Addressing
{
    [DataContract]
    [Serializable]
    public class AddressBtiDTO : BaseBtiDTO
    {
        /// <summary>
        ///     Наименование адреса.
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Номер дома.
        /// </summary>
        [DataMember]
        public virtual string DmtKrtLit { get; set; }

        /// <summary>
        ///     ИД улицы.
        /// </summary>
        [DataMember]
        public virtual StreetBtiDTO Ul { get; set; }

        /// <summary>
        ///     UNOM.
        /// </summary>
        [DataMember]
        public virtual long? UNOM { get; set; }

        /// <summary>
        ///     UNAD.
        /// </summary>
        [DataMember]
        public virtual int? UNAD { get; set; }

        /// <summary>
        ///     UNAD.
        /// </summary>
        [DataMember]
        public virtual int? Status { get; set; }

        /// <summary>
        ///     Административный округ по БТИ.
        /// </summary>
        [DataMember]
        public virtual DistrictBtiDTO District { get; set; }

        /// <summary>
        ///     Район по БТИ.
        /// </summary>
        [DataMember]
        public virtual RegionBtiDTO Region { get; set; }

        /// <summary>
        ///     Дом номер (текстовое).
        /// </summary>
        [DataMember]
        public virtual string Dmt { get; set; }

        /// <summary>
        ///     Корпус номер (текстовое).
        /// </summary>
        [DataMember]
        public virtual string Krt { get; set; }

        /// <summary>
        ///     Строение номер (текстовое).
        /// </summary>
        [DataMember]
        public virtual string Strt { get; set; }

        /// <summary>
        ///     Признак сооружения(kls-123)
        /// </summary>
        [DataMember]
        public virtual int? Soor { get; set; }

        /// <summary>
        ///     Тип документа для регистрации адреса (Kls-107)
        /// </summary>
        [DataMember]
        public virtual int? Tdoc { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        [DataMember]
        public virtual string Ndoc { get; set; }

        /// <summary>
        ///     Дата документа
        /// </summary>
        [DataMember]
        public virtual DateTime? Ddoc { get; set; }

        /// <summary>
        ///     Содержание документа (Kls-117)
        /// </summary>
        [DataMember]
        public virtual int? Sdoc { get; set; }

        /// <summary>
        ///     Номер регистрации
        /// </summary>
        [DataMember]
        public virtual int? Nreg { get; set; }

        /// <summary>
        ///     Дата регистрации
        /// </summary>
        [DataMember]
        public virtual DateTime? Dreg { get; set; }

        /// <summary>
        ///     Признак наличия дополнительного адреса
        /// </summary>
        [DataMember]
        public virtual int? DopAddr { get; set; }

        /// <summary>
        ///     Признак владения (kls-122)
        /// </summary>
        [DataMember]
        public virtual int? Vld { get; set; }

        /// <summary>
        ///     Признак литеры
        /// </summary>
        [DataMember]
        public virtual int? Lit { get; set; }
    }
}
