// File:    SmallLeisureType.cs
// Purpose: Definition of Class SmallLeisureType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Типы малых фом досуга
   /// </summary>
   [Serializable]
   [DataContract(Name = "smallLeisureType")]
   public partial class SmallLeisureType : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активно
      /// </summary>
      [Display(Description = "Активно")]
      [Required(ErrorMessage = "\"Активно\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Склеивать подтипы
      /// </summary>
      [Display(Description = "Склеивать подтипы")]
      [Required(ErrorMessage = "\"Склеивать подтипы\" должно быть заполнено")]
      [DataMember(Name = "mergeSubtypes", EmitDefaultValue = false)]
      public virtual bool MergeSubtypes { get; set; }
      
      /// <summary>
      /// Является текстовым полем
      /// </summary>
      [Display(Description = "Является текстовым полем")]
      [Required(ErrorMessage = "\"Является текстовым полем\" должно быть заполнено")]
      [DataMember(Name = "isTextData", EmitDefaultValue = false)]
      public virtual bool IsTextData { get; set; }
      
      /// <summary>
      /// Формула
      /// </summary>
      [Display(Description = "Формула")]
      [MaxLength(1000, ErrorMessage = "\"Формула\" не может быть больше 1000 символов")]
      [DataMember(Name = "formula", EmitDefaultValue = false)]
      public virtual string Formula { get; set; }
      
      
      /// <summary>
      /// Связь малых форм отдыха с их подтипами
      /// </summary>
      [InverseProperty("SmallLeisureType")]
      [DataMember(Name = "smallLeisureSubtypes", EmitDefaultValue = false)]
      public virtual ICollection<SmallLeisureSubtype> SmallLeisureSubtypes { get; set; }

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