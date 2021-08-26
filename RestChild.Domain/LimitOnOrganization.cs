// File:    LimitOnOrganization.cs
// Purpose: Definition of Class LimitOnOrganization

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Лимиты от на подведомственные
   /// </summary>
   [Serializable]
   [DataContract(Name = "limitOnOrganization")]
   public partial class LimitOnOrganization : IEntityBase
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
      /// Объем
      /// </summary>
      [Display(Description = "Объем")]
      [Required(ErrorMessage = "\"Объем\" должно быть заполнено")]
      [DataMember(Name = "volume", EmitDefaultValue = false)]
      public virtual int Volume { get; set; }
      
      /// <summary>
      /// Утверждено
      /// </summary>
      [Display(Description = "Утверждено")]
      [Required(ErrorMessage = "\"Утверждено\" должно быть заполнено")]
      [DataMember(Name = "approved", EmitDefaultValue = false)]
      public virtual bool Approved { get; set; }
      
      
      /// <summary>
      /// Ссылка на историю изменений
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "Ссылка на историю изменений")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// Ссылка на историю изменений
      /// </summary>
      [Display(Description = "Ссылка на историю изменений")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("LimitOrganization")]
      [Display(Description = "")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("LimitOnVedomstvo")]
      [DataMember(Name = "limitOnVedomstvoId")]
      [Display(Description = "")]
      public virtual long? LimitOnVedomstvoId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("VedomstvoLimit")]
      [Display(Description = "")]
      [DataMember(Name = "limitOnVedomstvo", EmitDefaultValue = false)]
      public virtual LimitOnVedomstvo LimitOnVedomstvo { get; set; }
      
      /// <summary>
      /// Статус квоты по подведомственным
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус квоты по подведомственным")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус квоты по подведомственным
      /// </summary>
      [Display(Description = "Статус квоты по подведомственным")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Ссылка на квоту
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Ссылка на квоту")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Ссылка на квоту
      /// </summary>
      [InverseProperty("LimitOnOrganizations")]
      [Display(Description = "Ссылка на квоту")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
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
      /// Место отдыха
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")]
      [Display(Description = "Место отдыха")]
      public virtual long? PlaceOfRestId { get; set; }
      
      
      /// <summary>
      /// Место отдыха
      /// </summary>
      [Display(Description = "Место отдыха")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }
      
      /// <summary>
      /// Тип списков квот
      /// </summary>
      [ForeignKey("TypeOfLimitList")]
      [DataMember(Name = "typeOfLimitListId")]
      [Display(Description = "Тип списков квот")]
      public virtual long? TypeOfLimitListId { get; set; }
      
      
      /// <summary>
      /// Тип списков квот
      /// </summary>
      [Display(Description = "Тип списков квот")]
      [DataMember(Name = "typeOfLimitList", EmitDefaultValue = false)]
      public virtual TypeOfLimitList TypeOfLimitList { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("SignInfo")]
      [DataMember(Name = "signInfoId")]
      [Display(Description = "")]
      public virtual long? SignInfoId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "signInfo", EmitDefaultValue = false)]
      public virtual SignInfo SignInfo { get; set; }

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