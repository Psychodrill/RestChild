// File:    LimitOnVedomstvo.cs
// Purpose: Definition of Class LimitOnVedomstvo

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Лимиты ведомствам
   /// </summary>
   [Serializable]
   [DataContract(Name = "limitOnVedomstvo")]
   public partial class LimitOnVedomstvo : IEntityBase
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
      /// Год лимита
      /// </summary>
      [Display(Description = "Год лимита")]
      [Required(ErrorMessage = "\"Год лимита\" должно быть заполнено")]
      [DataMember(Name = "limitYear", EmitDefaultValue = false)]
      public virtual int LimitYear { get; set; }
      
      /// <summary>
      /// Утверждены
      /// </summary>
      [Display(Description = "Утверждены")]
      [Required(ErrorMessage = "\"Утверждены\" должно быть заполнено")]
      [DataMember(Name = "approved", EmitDefaultValue = false)]
      public virtual bool Approved { get; set; }
      
      /// <summary>
      /// Объем
      /// </summary>
      [Display(Description = "Объем")]
      [Required(ErrorMessage = "\"Объем\" должно быть заполнено")]
      [DataMember(Name = "volume", EmitDefaultValue = false)]
      public virtual int Volume { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("LimitOnVedomstvo")]
      [DataMember(Name = "vedomstvoLimit", EmitDefaultValue = false)]
      public virtual ICollection<LimitOnOrganization> VedomstvoLimit { get; set; }
      
      /// <summary>
      /// Связь квоты и заявки
      /// </summary>
      [InverseProperty("LimitOnVedomstvo")]
      [DataMember(Name = "limitOnOrganizationRequests", EmitDefaultValue = false)]
      public virtual ICollection<LimitOnOrganizationRequest> LimitOnOrganizationRequests { get; set; }
      
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
      [InverseProperty("Vedomstvo")]
      [Display(Description = "")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Статус квоты по ведомству
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус квоты по ведомству")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус квоты по ведомству
      /// </summary>
      [Display(Description = "Статус квоты по ведомству")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
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