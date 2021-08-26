// File:    LimitOnOrganizationRequest.cs
// Purpose: Definition of Class LimitOnOrganizationRequest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявка на квоту
   /// </summary>
   [Serializable]
   [DataContract(Name = "limitOnOrganizationRequest")]
   public partial class LimitOnOrganizationRequest : IEntityBase
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
      /// Объем заявки
      /// </summary>
      [Display(Description = "Объем заявки")]
      [Required(ErrorMessage = "\"Объем заявки\" должно быть заполнено")]
      [DataMember(Name = "volume", EmitDefaultValue = false)]
      public virtual int Volume { get; set; }
      
      /// <summary>
      /// Согласованный объем
      /// </summary>
      [Display(Description = "Согласованный объем")]
      [DataMember(Name = "approvedVolume", EmitDefaultValue = false)]
      public virtual int? ApprovedVolume { get; set; }
      
      /// <summary>
      /// Дата заезда
      /// </summary>
      [Display(Description = "Дата заезда")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата отъезда
      /// </summary>
      [Display(Description = "Дата отъезда")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      /// <summary>
      /// Наименование коллектива
      /// </summary>
      [Display(Description = "Наименование коллектива")]
      [MaxLength(1000, ErrorMessage = "\"Наименование коллектива\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Количество сопровождающих
      /// </summary>
      [Display(Description = "Количество сопровождающих")]
      [DataMember(Name = "volumeAttendant", EmitDefaultValue = false)]
      public virtual int? VolumeAttendant { get; set; }
      
      /// <summary>
      /// Количество сопровождающих в роли вожатого
      /// </summary>
      [Display(Description = "Количество сопровождающих в роли вожатого")]
      [DataMember(Name = "volumeCounselor", EmitDefaultValue = false)]
      public virtual int? VolumeCounselor { get; set; }
      
      
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
      
      /// <summary>
      /// Связь квоты и заявки
      /// </summary>
      [ForeignKey("LimitOnVedomstvo")]
      [DataMember(Name = "limitOnVedomstvoId")]
      [Display(Description = "Связь квоты и заявки")]
      public virtual long? LimitOnVedomstvoId { get; set; }
      
      
      /// <summary>
      /// Связь квоты и заявки
      /// </summary>
      [InverseProperty("LimitOnOrganizationRequests")]
      [Display(Description = "Связь квоты и заявки")]
      [DataMember(Name = "limitOnVedomstvo", EmitDefaultValue = false)]
      public virtual LimitOnVedomstvo LimitOnVedomstvo { get; set; }
      
      /// <summary>
      /// Организация
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Организация")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Организация
      /// </summary>
      [Display(Description = "Организация")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Регион отдыха
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")]
      [Display(Description = "Регион отдыха")]
      public virtual long? PlaceOfRestId { get; set; }
      
      
      /// <summary>
      /// Регион отдыха
      /// </summary>
      [Display(Description = "Регион отдыха")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }
      
      /// <summary>
      /// Смена
      /// </summary>
      [ForeignKey("GroupedTimeOfRest")]
      [DataMember(Name = "groupedTimeOfRestId")]
      [Display(Description = "Смена")]
      public virtual long? GroupedTimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Смена
      /// </summary>
      [Display(Description = "Смена")]
      [DataMember(Name = "groupedTimeOfRest", EmitDefaultValue = false)]
      public virtual GroupedTimeOfRest GroupedTimeOfRest { get; set; }
      
      /// <summary>
      /// Время отдыха
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")]
      [Display(Description = "Время отдыха")]
      public virtual long? TimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Время отдыха
      /// </summary>
      [Display(Description = "Время отдыха")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
      /// <summary>
      /// Категория заявки
      /// </summary>
      [ForeignKey("ListOfChildsCategory")]
      [DataMember(Name = "listOfChildsCategoryId")]
      [Display(Description = "Категория заявки")]
      public virtual long? ListOfChildsCategoryId { get; set; }
      
      
      /// <summary>
      /// Категория заявки
      /// </summary>
      [Display(Description = "Категория заявки")]
      [DataMember(Name = "listOfChildsCategory", EmitDefaultValue = false)]
      public virtual ListOfChildsCategory ListOfChildsCategory { get; set; }

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