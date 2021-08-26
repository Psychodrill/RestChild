// File:    TypeOfSubRestriction.cs
// Purpose: Definition of Class TypeOfSubRestriction

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Под вид ограничения
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfSubRestriction")]
   public partial class TypeOfSubRestriction : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Удален
      /// </summary>
      [Display(Description = "Удален")]
      [Required(ErrorMessage = "\"Удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      
      /// <summary>
      /// Ограничения
      /// </summary>
      [ForeignKey("TypeOfRestriction")]
      [DataMember(Name = "typeOfRestrictionId")]
      [Display(Description = "Ограничения")]
      public virtual long? TypeOfRestrictionId { get; set; }
      
      
      /// <summary>
      /// Ограничения
      /// </summary>
      [InverseProperty("Subs")]
      [Display(Description = "Ограничения")]
      [DataMember(Name = "typeOfRestriction", EmitDefaultValue = false)]
      public virtual TypeOfRestriction TypeOfRestriction { get; set; }
      
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