// File:    MGTWindowWorkingPeriod.cs
// Purpose: Definition of Class MGTWindowWorkingPeriod

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// МГТ Приёмное время окна
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTWindowWorkingPeriod")]
   public partial class MGTWindowWorkingPeriod : IEntityBase
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
      [DataMember(Name = "timeFrom", EmitDefaultValue = false)]
      public virtual DateTime TimeFrom { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "timeTo", EmitDefaultValue = false)]
      public virtual DateTime TimeTo { get; set; }
      
      
      /// <summary>
      /// Связь приёмного окна с временем работы
      /// </summary>
      [ForeignKey("Window")]
      [DataMember(Name = "windowId")]
      [Display(Description = "Связь приёмного окна с временем работы")]
      public virtual long? WindowId { get; set; }
      
      
      /// <summary>
      /// Связь приёмного окна с временем работы
      /// </summary>
      [InverseProperty("WorkingPeriods")]
      [Display(Description = "Связь приёмного окна с временем работы")]
      [DataMember(Name = "window", EmitDefaultValue = false)]
      public virtual MGTWorkingDayWindow Window { get; set; }

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