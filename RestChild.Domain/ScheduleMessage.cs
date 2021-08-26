// File:    ScheduleMessage.cs
// Purpose: Definition of Class ScheduleMessage

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Сообщение о записи
   /// </summary>
   [Serializable]
   [DataContract(Name = "scheduleMessage")]
   public partial class ScheduleMessage : IEntityBase
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
      /// Сообщение
      /// </summary>
      [Display(Description = "Сообщение")]
      [DataMember(Name = "message", EmitDefaultValue = false)]
      public virtual String Message { get; set; }
      
      /// <summary>
      /// Дата получения
      /// </summary>
      [Display(Description = "Дата получения")]
      [DataMember(Name = "dateMessage", EmitDefaultValue = false)]
      public virtual DateTime? DateMessage { get; set; }
      
      /// <summary>
      /// Обработано
      /// </summary>
      [Display(Description = "Обработано")]
      [Required(ErrorMessage = "\"Обработано\" должно быть заполнено")]
      [DataMember(Name = "processed", EmitDefaultValue = false)]
      public virtual bool Processed { get; set; }
      
      /// <summary>
      /// Есть ошибки
      /// </summary>
      [Display(Description = "Есть ошибки")]
      [Required(ErrorMessage = "\"Есть ошибки\" должно быть заполнено")]
      [DataMember(Name = "hasError", EmitDefaultValue = false)]
      public virtual bool HasError { get; set; }
      
      /// <summary>
      /// Текст ошибки
      /// </summary>
      [Display(Description = "Текст ошибки")]
      [DataMember(Name = "errorMessage", EmitDefaultValue = false)]
      public virtual String ErrorMessage { get; set; }

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