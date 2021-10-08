// File:    MGTVisitTarget.cs
// Purpose: Definition of Class MGTVisitTarget

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// МГТ Цель обращения
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTVisitTarget")]
   public partial class MGTVisitTarget : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идетификатор
      /// </summary>
      [Display(Description = "Уникальный идетификатор")]
      [Required(ErrorMessage = "\"Уникальный идетификатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [MaxLength(1000, ErrorMessage = "\"Название\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Используется
      /// </summary>
      [Display(Description = "Используется")]
      [Required(ErrorMessage = "\"Используется\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Для МПГУ
      /// </summary>
      [Display(Description = "Для МПГУ")]
      [Required(ErrorMessage = "\"Для МПГУ\" должно быть заполнено")]
      [DataMember(Name = "isForMPGU", EmitDefaultValue = false)]
      public virtual bool IsForMPGU { get; set; }
      
      
      /// <summary>
      /// Связь приёмного окна с целями обращения
      /// </summary>
      [InverseProperty("Targets")]
      [DataMember(Name = "window", EmitDefaultValue = false)]
      public virtual ICollection<MGTWorkingDayWindow> Window { get; set; }

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
