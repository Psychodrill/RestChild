// File:    Skill.cs
// Purpose: Definition of Class Skill

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Навык
   /// </summary>
   [Serializable]
   [DataContract(Name = "skill")]
   public partial class Skill : IEntityBase
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
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [MaxLength(1000, ErrorMessage = "\"Название\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активность
      /// </summary>
      [Display(Description = "Активность")]
      [Required(ErrorMessage = "\"Активность\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Порядок сортировки
      /// </summary>
      [Display(Description = "Порядок сортировки")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual long? SortOrder { get; set; }
      
      /// <summary>
      /// Необходимость заполнения текста
      /// </summary>
      [Display(Description = "Необходимость заполнения текста")]
      [Required(ErrorMessage = "\"Необходимость заполнения текста\" должно быть заполнено")]
      [DataMember(Name = "needText", EmitDefaultValue = false)]
      public virtual bool NeedText { get; set; }
      
      /// <summary>
      /// Необходимость справочника
      /// </summary>
      [Display(Description = "Необходимость справочника")]
      [DataMember(Name = "needVocabulary", EmitDefaultValue = false)]
      public virtual bool? NeedVocabulary { get; set; }
      
      
      /// <summary>
      /// Навык
      /// </summary>
      [InverseProperty("Skill")]
      [DataMember(Name = "values", EmitDefaultValue = false)]
      public virtual ICollection<SkillVocabulary> Values { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("SkillsGroup")]
      [DataMember(Name = "skillsGroupId")]
      [Display(Description = "")]
      public virtual long? SkillsGroupId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "skillsGroup", EmitDefaultValue = false)]
      public virtual SkillsGroup SkillsGroup { get; set; }

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