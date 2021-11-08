using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;


namespace RestChild.Domain
{

    /// <summary>
    /// Объект отдыха
    /// </summary>
    [Serializable]
    [DataContract(Name = "leisureFacilities")]
    public partial class LeisureFacilities  : IEntityBase
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public virtual long Id { get; set; }

        /// <summary>
        /// Наименование места отдыха
        /// </summary>
        [Display(Description = "Сокращённое название")]
        [DataMember(Name = "abbreviated", EmitDefaultValue = false)]
        public virtual string Abbreviated { get; set; }

        /// <summary>
        /// Полное название
        /// </summary>
        [Display(Description = "Полное название")]
        [DataMember(Name = "fullname", EmitDefaultValue = false)]
        public virtual string Fullname { get; set; }

        /// <summary>
        /// Фактический адрес
        /// </summary>
        [Display(Description = "Фактический адрес")]
        [DataMember(Name = "actualAdress", EmitDefaultValue = false)]
        public virtual string ActualAdress { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [Display(Description = "ИНН")]
        [MaxLength(1000, ErrorMessage = "\"ИНН\" не может быть больше 1000 символов")]
        [DataMember(Name = "inn", EmitDefaultValue = false)]
        public virtual string Inn { get; set; }
        /// <summary>
        /// Последнее сохранение
        /// </summary>
        [Display(Description = "Последнее сохранение")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
        public virtual long LastUpdateTick { get; set; }
        /// <summary>
        /// Внешний ключ
        /// </summary>
        [Display(Description = "Внешний ключ")]
        [DataMember(Name = "eid", EmitDefaultValue = false)]
        [Index]
        public virtual long? Eid { get; set; }
        /// <summary>
        /// Статус обмена по сущности
        /// </summary>
        [Display(Description = "Статус обмена по сущности")]
        [DataMember(Name = "eidSendStatus", EmitDefaultValue = false)]
        [Index]
        public virtual long? EidSendStatus { get; set; }
        /// <summary>
        /// Дата синхронизации
        /// </summary>
        [Display(Description = "Дата синхронизации")]
        [DataMember(Name = "eidSyncDate", EmitDefaultValue = false)]
        public virtual DateTime? EidSyncDate { get; set; }
        /// <summary>
        /// Регион
        /// </summary>
        [ForeignKey("StateDistrict")]
        [DataMember(Name = "stateDistrictId")]
        [Display(Description = "Регион")]
        public virtual long? StateDistrictId { get; set; }
        /// <summary>
        /// Регион
        /// </summary>
        [Display(Description = "Регион")]
        [DataMember(Name = "stateDistrict", EmitDefaultValue = false)]
        public virtual StateDistrict StateDistrict { get; set; }
        /// <summary>
        /// История
        /// </summary>
        [ForeignKey("HistoryLink")]
        [DataMember(Name = "historyLinkId")]
        [Display(Description = "История")]
        public virtual long? HistoryLinkId { get; set; }
        /// <summary>
        /// История
        /// </summary>
        [Display(Description = "История")]
        [DataMember(Name = "historyLink", EmitDefaultValue = false)]
        public virtual HistoryLink HistoryLink { get; set; }


    }
}
