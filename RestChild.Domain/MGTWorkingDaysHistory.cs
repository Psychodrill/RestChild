// File:    MGTWorkingDaysHistory.cs
// Purpose: Definition of Class MGTWorkingDaysHistory

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Связь истории рабочего дня с пользователем
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTWorkingDaysHistory")]
   public partial class MGTWorkingDaysHistory : IEntityBase
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
      /// Событие
      /// </summary>
      [Display(Description = "Событие")]
      [MaxLength(1000, ErrorMessage = "\"Событие\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Событие\" не может быть пустым")]
      [DataMember(Name = "eventName", EmitDefaultValue = false)]
      public virtual string EventName { get; set; }
      
      /// <summary>
      /// Описание события
      /// </summary>
      [Display(Description = "Описание события")]
      [DataMember(Name = "eventDescription", EmitDefaultValue = false)]
      public virtual String EventDescription { get; set; }
      
      /// <summary>
      /// Дата события
      /// </summary>
      [Display(Description = "Дата события")]
      [Required(ErrorMessage = "\"Дата события\" должно быть заполнено")]
      [DataMember(Name = "eventDate", EmitDefaultValue = false)]
      public virtual DateTime EventDate { get; set; }
      
      
      /// <summary>
      /// Связь рабочего дня с его историей
      /// </summary>
      [ForeignKey("WorkingDay")]
      [DataMember(Name = "workingDayId")][Display(Description = "Связь рабочего дня с его историей")]
      public virtual long? WorkingDayId { get; set; }
      /// <summary>
      /// Связь рабочего дня с его историей
      /// </summary>
      [InverseProperty("WorkingDayHistory")]
      [Display(Description = "Связь рабочего дня с его историей")]
      [DataMember(Name = "workingDay", EmitDefaultValue = false)]
      public virtual MGTWorkingDay WorkingDay { get; set; }
      
      /// <summary>
      /// МГТ Рабочие дни. Автор события
      /// </summary>
      [ForeignKey("Author")]
      [DataMember(Name = "authorId")]
      [Display(Description = "МГТ Рабочие дни. Автор события")]
      public virtual long? AuthorId { get; set; }
      
      
      /// <summary>
      /// МГТ Рабочие дни. Автор события
      /// </summary>
      [InverseProperty("MGTWorkingDaysHistory")]
      [Display(Description = "МГТ Рабочие дни. Автор события")]
      [DataMember(Name = "author", EmitDefaultValue = false)]
      public virtual Account Author { get; set; }

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