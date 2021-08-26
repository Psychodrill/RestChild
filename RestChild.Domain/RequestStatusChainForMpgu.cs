// File:    RequestStatusChainForMpgu.cs
// Purpose: Definition of Class RequestStatusChainForMpgu

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявление. Цепочка статусов для ЕЛК и МПГУ
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestStatusChainForMpgu")]
   public partial class RequestStatusChainForMpgu : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Заявление на деньги
      /// </summary>
      [Display(Description = "Заявление на деньги")]
      [DataMember(Name = "requestOnMoney", EmitDefaultValue = false)]
      public virtual bool? RequestOnMoney { get; set; }
      
      /// <summary>
      /// Заявление двух этапной кампании
      /// </summary>
      [Display(Description = "Заявление двух этапной кампании")]
      [DataMember(Name = "isFirstCompany", EmitDefaultValue = false)]
      public virtual bool? IsFirstCompany { get; set; }
      
      
      /// <summary>
      /// Статусы для отправки в МПГУ
      /// </summary>
      [InverseProperty("Chain")]
      [DataMember(Name = "statuses", EmitDefaultValue = false)]
      public virtual ICollection<RequestStatusForMpgu> Statuses { get; set; }
      
      /// <summary>
      /// Статус заявления из которого
      /// </summary>
      [ForeignKey("Status")]
      [DataMember(Name = "statusId")]
      [Display(Description = "Статус заявления из которого")]
      public virtual long? StatusId { get; set; }
      
      
      /// <summary>
      /// Статус заявления из которого
      /// </summary>
      [Display(Description = "Статус заявления из которого")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual Status Status { get; set; }
      
      /// <summary>
      /// Действия в заявлении
      /// </summary>
      [ForeignKey("StatusAction")]
      [DataMember(Name = "statusActionId")]
      [Display(Description = "Действия в заявлении")]
      public virtual long? StatusActionId { get; set; }
      
      
      /// <summary>
      /// Действия в заявлении
      /// </summary>
      [Display(Description = "Действия в заявлении")]
      [DataMember(Name = "statusAction", EmitDefaultValue = false)]
      public virtual StatusAction StatusAction { get; set; }
      
      /// <summary>
      /// Причины отказа по заявлению
      /// </summary>
      [ForeignKey("DeclineReason")]
      [DataMember(Name = "declineReasonId")]
      [Display(Description = "Причины отказа по заявлению")]
      public virtual long? DeclineReasonId { get; set; }
      
      
      /// <summary>
      /// Причины отказа по заявлению
      /// </summary>
      [Display(Description = "Причины отказа по заявлению")]
      [DataMember(Name = "declineReason", EmitDefaultValue = false)]
      public virtual DeclineReason DeclineReason { get; set; }
      
      /// <summary>
      /// Кампания
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Кампания")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Кампания
      /// </summary>
      [Display(Description = "Кампания")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Событие заявления
      /// </summary>
      [ForeignKey("RequestEvent")]
      [DataMember(Name = "requestEventId")]
      [Display(Description = "Событие заявления")]
      public virtual long? RequestEventId { get; set; }
      
      
      /// <summary>
      /// Событие заявления
      /// </summary>
      [Display(Description = "Событие заявления")]
      [DataMember(Name = "requestEvent", EmitDefaultValue = false)]
      public virtual RequestEvent RequestEvent { get; set; }

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