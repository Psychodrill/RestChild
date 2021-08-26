// File:    MGTWorkingDayWindow.cs
// Purpose: Definition of Class MGTWorkingDayWindow

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// МГТ Приёмноё окно
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTWorkingDayWindow")]
   public partial class MGTWorkingDayWindow : IEntityBase
   {
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isCanceled", EmitDefaultValue = false)]
      public virtual bool IsCanceled { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "windowNumber", EmitDefaultValue = false)]
      public virtual int? WindowNumber { get; set; }
      
      
      /// <summary>
      /// Связь приёмного окна с временем работы
      /// </summary>
      [InverseProperty("Window")]
      [DataMember(Name = "workingPeriods", EmitDefaultValue = false)]
      public virtual ICollection<MGTWindowWorkingPeriod> WorkingPeriods { get; set; }
      
      /// <summary>
      /// Связь приёмного окна с целями обращения
      /// </summary>
      [InverseProperty("Window")]
      [DataMember(Name = "targets", EmitDefaultValue = false)]
      public virtual ICollection<MGTVisitTarget> Targets { get; set; }
      
      /// <summary>
      /// Связь рабочего дня с приёмными окнами
      /// </summary>
      [ForeignKey("WorkingDay")]
      [DataMember(Name = "workingDayId")]
      [Display(Description = "Связь рабочего дня с приёмными окнами")]
      public virtual long? WorkingDayId { get; set; }
      
      
      /// <summary>
      /// Связь рабочего дня с приёмными окнами
      /// </summary>
      [InverseProperty("Windows")]
      [Display(Description = "Связь рабочего дня с приёмными окнами")]
      [DataMember(Name = "workingDay", EmitDefaultValue = false)]
      public virtual MGTWorkingDay WorkingDay { get; set; }

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