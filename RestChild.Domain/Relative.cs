// File:    Relative.cs
// Purpose: Definition of Class Relative

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Родители
   /// </summary>
   [Serializable]
   [DataContract(Name = "relative")]
   public partial class Relative : IEntityBase
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
      /// Статус по отношению к ребёнку
      /// </summary>
      [ForeignKey("StatusByChild")]
      [DataMember(Name = "statusByChildId")]
      [Display(Description = "Статус по отношению к ребёнку")]
      public virtual long? StatusByChildId { get; set; }
      
      
      /// <summary>
      /// Статус по отношению к ребёнку
      /// </summary>
      [Display(Description = "Статус по отношению к ребёнку")]
      [DataMember(Name = "statusByChild", EmitDefaultValue = false)]
      public virtual StatusByChild StatusByChild { get; set; }
      
      /// <summary>
      /// Ребёнок <-> Родитель
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Ребёнок <-> Родитель")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Ребёнок <-> Родитель
      /// </summary>
      [InverseProperty("Relatives")]
      [Display(Description = "Ребёнок <-> Родитель")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Родственник
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Родственник")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Родственник
      /// </summary>
      [Display(Description = "Родственник")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }

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