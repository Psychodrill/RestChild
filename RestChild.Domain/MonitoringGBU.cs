// File:    MonitoringGBU.cs
// Purpose: Definition of Class MonitoringGBU

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. ГБУ
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringGBU")]
   public partial class MonitoringGBU : IEntityBase
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
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [MaxLength(1000, ErrorMessage = "\"Название\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название\" не может быть пустым")]
      [DataMember(Name = "shortName", EmitDefaultValue = false)]
      public virtual string ShortName { get; set; }
      
      /// <summary>
      /// Полное наименование
      /// </summary>
      [Display(Description = "Полное наименование")]
      [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "fullName", EmitDefaultValue = false)]
      public virtual string FullName { get; set; }
      
      /// <summary>
      /// Фактический адрес
      /// </summary>
      [Display(Description = "Фактический адрес")]
      [MaxLength(1000, ErrorMessage = "\"Фактический адрес\" не может быть больше 1000 символов")]
      [DataMember(Name = "factAddress", EmitDefaultValue = false)]
      public virtual string FactAddress { get; set; }
      
      
      /// <summary>
      /// Мониторинг. ГБУ связанные с формой сведений о малых формах досуга  (связь с ГБУ)
      /// </summary>
      [InverseProperty("GBU")]
      [DataMember(Name = "smallLeisureInfoConnectedGBUs", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringSmallLeisureInfoGBU> SmallLeisureInfoConnectedGBUs { get; set; }
      
      /// <summary>
      /// Адрес ГБУ
      /// </summary>
      [ForeignKey("Address")]
      [DataMember(Name = "addressId")]
      [Display(Description = "Адрес ГБУ")]
      public virtual long? AddressId { get; set; }
      
      
      /// <summary>
      /// Адрес ГБУ
      /// </summary>
      [Display(Description = "Адрес ГБУ")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual Address Address { get; set; }
      
      /// <summary>
      /// Связь ГБУ с объектами мониторинга (префектурами и т.д.)
      /// </summary>
      [ForeignKey("Organisation")]
      [DataMember(Name = "organisationId")]
      [Display(Description = "Связь ГБУ с объектами мониторинга (префектурами и т.д.)")]
      public virtual long? OrganisationId { get; set; }
      
      
      /// <summary>
      /// Связь ГБУ с объектами мониторинга (префектурами и т.д.)
      /// </summary>
      [InverseProperty("GBUs")]
      [Display(Description = "Связь ГБУ с объектами мониторинга (префектурами и т.д.)")]
      [DataMember(Name = "organisation", EmitDefaultValue = false)]
      public virtual Organization Organisation { get; set; }

      /// <summary>
      /// Последнее сохранение
      /// </summary>
      [Display(Description = "Последнее сохранение")]
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
   }
}