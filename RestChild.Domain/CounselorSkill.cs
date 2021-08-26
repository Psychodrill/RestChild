// File:    CounselorSkill.cs
// Purpose: Definition of Class CounselorSkill

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Навык вожатого
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorSkill")]
   public partial class CounselorSkill : IEntityBase
   {
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Текст
      /// </summary>
      [Display(Description = "Текст")]
      [DataMember(Name = "otherText", EmitDefaultValue = false)]
      public virtual String OtherText { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "Вожатый")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Skill")]
      [Display(Description = "Вожатый")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }
      
      /// <summary>
      /// Навык
      /// </summary>
      [ForeignKey("Skill")]
      [DataMember(Name = "skillId")]
      [Display(Description = "Навык")]
      public virtual long? SkillId { get; set; }
      
      
      /// <summary>
      /// Навык
      /// </summary>
      [Display(Description = "Навык")]
      [DataMember(Name = "skill", EmitDefaultValue = false)]
      public virtual Skill Skill { get; set; }
      
      /// <summary>
      /// Справочник
      /// </summary>
      [ForeignKey("SkillVocabulary")]
      [DataMember(Name = "skillVocabularyId")]
      [Display(Description = "Справочник")]
      public virtual long? SkillVocabularyId { get; set; }
      
      
      /// <summary>
      /// Справочник
      /// </summary>
      [Display(Description = "Справочник")]
      [DataMember(Name = "skillVocabulary", EmitDefaultValue = false)]
      public virtual SkillVocabulary SkillVocabulary { get; set; }

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