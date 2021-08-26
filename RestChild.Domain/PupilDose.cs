// File:    PupilDose.cs
// Purpose: Definition of Class PupilDose

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Доза воспитанника
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilDose")]
   public partial class PupilDose : IEntityBase
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
      /// Дозировка
      /// </summary>
      [Display(Description = "Дозировка")]
      [MaxLength(1000, ErrorMessage = "\"Дозировка\" не может быть больше 1000 символов")]
      [DataMember(Name = "dose", EmitDefaultValue = false)]
      public virtual string Dose { get; set; }
      
      /// <summary>
      /// Удалена
      /// </summary>
      [Display(Description = "Удалена")]
      [Required(ErrorMessage = "\"Удалена\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      
      /// <summary>
      /// Воспитанник -> Наркотики
      /// </summary>
      [ForeignKey("Pupil")]
      [DataMember(Name = "pupilId")]
      [Display(Description = "Воспитанник -> Наркотики")]
      public virtual long? PupilId { get; set; }
      
      
      /// <summary>
      /// Воспитанник -> Наркотики
      /// </summary>
      [InverseProperty("Drugs")]
      [Display(Description = "Воспитанник -> Наркотики")]
      [DataMember(Name = "pupil", EmitDefaultValue = false)]
      public virtual Pupil Pupil { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("Drug")]
      [DataMember(Name = "drugId")]
      [Display(Description = "")]
      public virtual long? DrugId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("Doses")]
      [Display(Description = "")]
      [DataMember(Name = "drug", EmitDefaultValue = false)]
      public virtual Drug Drug { get; set; }
      
      /// <summary>
      /// Временная доза -> Доза
      /// </summary>
      [InverseProperty("Dose")]
      [DataMember(Name = "pupilGroupListMemberDrugDoses", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListMemberDrugDose> PupilGroupListMemberDrugDoses { get; set; }

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