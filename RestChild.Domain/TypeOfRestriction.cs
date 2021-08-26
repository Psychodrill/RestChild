// File:    TypeOfRestriction.cs
// Purpose: Definition of Class TypeOfRestriction

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вид ограничения
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfRestriction")]
   public partial class TypeOfRestriction : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентфикатор
      /// </summary>
      [Display(Description = "Уникальный идентфикатор")]
      [Required(ErrorMessage = "\"Уникальный идентфикатор\" должно быть заполнено")]
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
      /// Активная запись
      /// </summary>
      [Display(Description = "Активная запись")]
      [Required(ErrorMessage = "\"Активная запись\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      
      /// <summary>
      /// Ограничения
      /// </summary>
      [InverseProperty("TypeOfRestriction")]
      [DataMember(Name = "subs", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfSubRestriction> Subs { get; set; }
      
      /// <summary>
      /// Группа ограничений
      /// </summary>
      [ForeignKey("RestrictionGroup")]
      [DataMember(Name = "restrictionGroupId")]
      [Display(Description = "Группа ограничений")]
      public virtual long? RestrictionGroupId { get; set; }
      
      
      /// <summary>
      /// Группа ограничений
      /// </summary>
      [Display(Description = "Группа ограничений")]
      [DataMember(Name = "restrictionGroup", EmitDefaultValue = false)]
      public virtual RestrictionGroup RestrictionGroup { get; set; }

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