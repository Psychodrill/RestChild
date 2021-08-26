// File:    Discount.cs
// Purpose: Definition of Class Discount

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Акция
   /// </summary>
   [Serializable]
   [DataContract(Name = "discount")]
   public partial class Discount : IEntityBase
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
      /// Процент скидки
      /// </summary>
      [Display(Description = "Процент скидки")]
      [DataMember(Name = "procent", EmitDefaultValue = false)]
      public virtual decimal? Procent { get; set; }
      
      /// <summary>
      /// Наименование скидки
      /// </summary>
      [Display(Description = "Наименование скидки")]
      [MaxLength(1000, ErrorMessage = "\"Наименование скидки\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование скидки\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание скидки
      /// </summary>
      [Display(Description = "Описание скидки")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Дата начала
      /// </summary>
      [Display(Description = "Дата начала")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата окончания
      /// </summary>
      [Display(Description = "Дата окончания")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Бессрочно
      /// </summary>
      [Display(Description = "Бессрочно")]
      [Required(ErrorMessage = "\"Бессрочно\" должно быть заполнено")]
      [DataMember(Name = "unlimited", EmitDefaultValue = false)]
      public virtual bool Unlimited { get; set; }
      
      
      /// <summary>
      /// Акции <-> Реестр размещения
      /// </summary>
      [InverseProperty("Discounts")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tours { get; set; }
      
      /// <summary>
      /// Акция <-> Заявки
      /// </summary>
      [InverseProperty("Discounts")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<Request> Requests { get; set; }
      
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
      /// Файл
      /// </summary>
      [ForeignKey("FileOrLink")]
      [DataMember(Name = "fileOrLinkId")]
      [Display(Description = "Файл")]
      public virtual long? FileOrLinkId { get; set; }
      
      
      /// <summary>
      /// Файл
      /// </summary>
      [Display(Description = "Файл")]
      [DataMember(Name = "fileOrLink", EmitDefaultValue = false)]
      public virtual FileOrLink FileOrLink { get; set; }

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