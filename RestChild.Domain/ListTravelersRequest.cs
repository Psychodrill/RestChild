// File:    ListTravelersRequest.cs
// Purpose: Definition of Class ListTravelersRequest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Список для включения. Включенные заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "listTravelersRequest")]
   public partial class ListTravelersRequest : IEntityBase
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
      /// Включен в список
      /// </summary>
      [Display(Description = "Включен в список")]
      [Required(ErrorMessage = "\"Включен в список\" должно быть заполнено")]
      [DataMember(Name = "isIncluded", EmitDefaultValue = false)]
      public virtual bool IsIncluded { get; set; }
      
      /// <summary>
      /// Дата включения
      /// </summary>
      [Display(Description = "Дата включения")]
      [DataMember(Name = "dateInclude", EmitDefaultValue = false)]
      public virtual DateTime? DateInclude { get; set; }
      
      /// <summary>
      /// Оценка
      /// </summary>
      [Display(Description = "Оценка")]
      [DataMember(Name = "rank", EmitDefaultValue = false)]
      public virtual int? Rank { get; set; }
      
      /// <summary>
      /// Дата заявления
      /// </summary>
      [Display(Description = "Дата заявления")]
      [DataMember(Name = "dateRequest", EmitDefaultValue = false)]
      public virtual DateTime? DateRequest { get; set; }
      
      
      /// <summary>
      /// Детализация по рангу заявлений
      /// </summary>
      [InverseProperty("ListTravelersRequest")]
      [DataMember(Name = "details", EmitDefaultValue = false)]
      public virtual ICollection<ListTravelersRequestDetail> Details { get; set; }
      
      /// <summary>
      /// Заявление
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Заявление")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Заявление
      /// </summary>
      [Display(Description = "Заявление")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Список заявлений
      /// </summary>
      [ForeignKey("ListTravelers")]
      [DataMember(Name = "listTravelersId")]
      [Display(Description = "Список заявлений")]
      public virtual long? ListTravelersId { get; set; }
      
      
      /// <summary>
      /// Список заявлений
      /// </summary>
      [InverseProperty("Requests")]
      [Display(Description = "Список заявлений")]
      [DataMember(Name = "listTravelers", EmitDefaultValue = false)]
      public virtual ListTravelers ListTravelers { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("StateMachineState")]
      [DataMember(Name = "stateMachineStateId")]
      [Display(Description = "Статус")]
      public virtual long? StateMachineStateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "stateMachineState", EmitDefaultValue = false)]
      public virtual StateMachineState StateMachineState { get; set; }

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