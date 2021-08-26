// File:    CounselorHighSchool.cs
// Purpose: Definition of Class CounselorHighSchool

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Высшее образование вожатого
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorHighSchool")]
   public partial class CounselorHighSchool : IEntityBase
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
      [DataMember(Name = "educationInstitutionName", EmitDefaultValue = false)]
      public virtual string EducationInstitutionName { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "department", EmitDefaultValue = false)]
      public virtual string Department { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "speciality", EmitDefaultValue = false)]
      public virtual string Speciality { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "course", EmitDefaultValue = false)]
      public virtual string Course { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "graduationYear", EmitDefaultValue = false)]
      public virtual string GraduationYear { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("HighSchoolGraduations")]
      [Display(Description = "")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }

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