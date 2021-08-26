// File:    DiscountCard.cs
// Purpose: Definition of Class DiscountCard

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Дисконтная карта
   /// </summary>
   [Serializable]
   [DataContract(Name = "discountCard")]
   public partial class DiscountCard : IEntityBase
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
      /// Номер
      /// </summary>
      [Display(Description = "Номер")]
      [MaxLength(1000, ErrorMessage = "\"Номер\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Номер\" не может быть пустым")]
      [DataMember(Name = "cardNumber", EmitDefaultValue = false)]
      public virtual string CardNumber { get; set; }
      
      /// <summary>
      /// Кому выдана
      /// </summary>
      [Display(Description = "Кому выдана")]
      [MaxLength(1000, ErrorMessage = "\"Кому выдана\" не может быть больше 1000 символов")]
      [DataMember(Name = "cardHolder", EmitDefaultValue = false)]
      public virtual string CardHolder { get; set; }
      
      /// <summary>
      /// Дата начала действия
      /// </summary>
      [Display(Description = "Дата начала действия")]
      [Required(ErrorMessage = "\"Дата начала действия\" должно быть заполнено")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime DateFrom { get; set; }
      
      /// <summary>
      /// Дата окончания действия
      /// </summary>
      [Display(Description = "Дата окончания действия")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Процент скидки
      /// </summary>
      [Display(Description = "Процент скидки")]
      [DataMember(Name = "procent", EmitDefaultValue = false)]
      public virtual decimal? Procent { get; set; }
      
      /// <summary>
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      
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