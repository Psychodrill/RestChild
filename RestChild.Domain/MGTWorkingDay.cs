// File:    MGTWorkingDay.cs
// Purpose: Definition of Class MGTWorkingDay

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// МГТ Рабочий день
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTWorkingDay")]
   public partial class MGTWorkingDay : IEntityBase
   {
      
      /// <summary>
      /// Идентификатор
      /// </summary>
      [Display(Description = "Идентификатор")]
      [Required(ErrorMessage = "\"Идентификатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Дата
      /// </summary>
      [Display(Description = "Дата")]
      [Required(ErrorMessage = "\"Дата\" должно быть заполнено")]
      [DataMember(Name = "date", EmitDefaultValue = false)]
      public virtual DateTime Date { get; set; }
      
      /// <summary>
      /// Интрвал приёма
      /// </summary>
      [Display(Description = "Интрвал приёма")]
      [Required(ErrorMessage = "\"Интрвал приёма\" должно быть заполнено")]
      [DataMember(Name = "workingInterval", EmitDefaultValue = false)]
      public virtual short WorkingInterval { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      
      /// <summary>
      /// Связь рабочего дня с приёмными окнами
      /// </summary>
      [InverseProperty("WorkingDay")]
      [DataMember(Name = "windows", EmitDefaultValue = false)]
      public virtual ICollection<MGTWorkingDayWindow> Windows { get; set; }
      
      /// <summary>
      /// Связь рабочих дней с запясями на приём
      /// </summary>
      [InverseProperty("WorkingDay")]
      [DataMember(Name = "visitBookings", EmitDefaultValue = false)]
      public virtual ICollection<MGTBookingVisit> VisitBookings { get; set; }
      
      /// <summary>
      /// Связь рабочего дня с его историей
      /// </summary>
      [InverseProperty("WorkingDay")]
      [DataMember(Name = "workingDayHistory", EmitDefaultValue = false)]
      public virtual ICollection<MGTWorkingDaysHistory> WorkingDayHistory { get; set; }

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