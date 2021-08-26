using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Addressing
{
    /// <summary>
    ///     характеристики зданий по БТИ.
    /// </summary>
    [DataContract]
    public class FeaturesBuildBtiDTO : BaseBtiDTO
    {
        /// <summary>
        ///     Уникальный номер статкарты
        /// </summary>
        [DataMember]
        public virtual long? UNOM { get; set; }

        /// <summary>
        ///     Класс строения (клс.49)
        /// </summary>
        [DataMember]
        public virtual long? Kl { get; set; }

        /// <summary>
        ///     Назначение строения (kls_35)
        /// </summary>
        [DataMember]
        public virtual long? Naz { get; set; }

        /// <summary>
        ///     Состояние строения или его части(клс.89)
        /// </summary>
        [DataMember]
        public virtual long? Sost { get; set; }

        /// <summary>
        ///     Дата установки состояния
        /// </summary>
        [DataMember]
        public virtual DateTime? Dtsost { get; set; }

        /// <summary>
        ///     Признак наличия техпаспорта
        /// </summary>
        [DataMember]
        public virtual long? Tehpasp { get; set; }

        /// <summary>
        ///     Признак самовол.возведен.объекта(kls_8)
        /// </summary>
        [DataMember]
        public virtual long? Samovol { get; set; }

        /// <summary>
        ///     Признак аварийности здания(kls_8)
        /// </summary>
        [DataMember]
        public virtual long? Avarzd { get; set; }

        /// <summary>
        ///     Дата установки признака
        /// </summary>
        [DataMember]
        public virtual DateTime? Dtavarzd { get; set; }

        /// <summary>
        ///     Корпус отселен,отселяется(kls-129)
        /// </summary>
        [DataMember]
        public virtual long? Otskorp { get; set; }

        /// <summary>
        ///     Код БТИ(клс.48)
        /// </summary>
        [DataMember]
        public virtual long? Bti { get; set; }

        /// <summary>
        ///     Вид фонда (клс.4)
        /// </summary>
        [DataMember]
        public virtual long? Kat { get; set; }

        /// <summary>
        ///     Материал стен(клс.3)
        /// </summary>
        [DataMember]
        public virtual long? Mst { get; set; }

        /// <summary>
        ///     Капитальность
        /// </summary>
        [DataMember]
        public virtual long? Kap { get; set; }

        /// <summary>
        ///     Этажность
        /// </summary>
        [DataMember]
        public virtual long? Et { get; set; }

        /// <summary>
        ///     Год постройки
        /// </summary>
        [DataMember]
        public virtual long? Gdpostr { get; set; }

        /// <summary>
        ///     Признак  года постройки до 1917
        /// </summary>
        [DataMember]
        public virtual long? Gddo1917 { get; set; }

        /// <summary>
        ///     Количество подъездов
        /// </summary>
        [DataMember]
        public virtual long? Pdzq { get; set; }

        /// <summary>
        ///     Общая площадь жилых помещений (кВ.м)
        /// </summary>
        [DataMember]
        public virtual decimal? OplG { get; set; }

        /// <summary>
        ///     Общая площадь нежилых помещений (кв.м)
        /// </summary>
        [DataMember]
        public virtual decimal? OplN { get; set; }

        /// <summary>
        ///     Серия проекта(клс.63)
        /// </summary>
        [DataMember]
        public virtual long? Ser { get; set; }

        /// <summary>
        ///     Признак-корпусом не считать
        /// </summary>
        [DataMember]
        public virtual long? Prkor { get; set; }

        /// <summary>
        ///     Код ОТИ по новому клс.151 В ст/к БТИ это поле = 0
        /// </summary>
        [DataMember]
        public virtual long? Oti { get; set; }

        /// <summary>
        ///     Ссылка на ст/к БТИ (UNOM). Для ст/к БТИ и ст/к ОТИ без связи это поле = 0
        /// </summary>
        [DataMember]
        public virtual long? Refunbti { get; set; }

        [DataMember] public virtual long? LiftPass { get; set; }

        [DataMember] public virtual long? LiftPassGrz { get; set; }

        [DataMember] public virtual long? LiftGrz { get; set; }
    }
}
