// File:    PupilGroupListMemberDrugDose.cs
// Purpose: Definition of Class PupilGroupListMemberDrugDose

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Воспитанник (список по группе отправки (потребности)) -> дозировка припарата на период отдыха
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilGroupListMemberDrugDose")]
   public partial class PupilGroupListMemberDrugDose : IEntityBase
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
      /// Дозировка (Кол-во лекарственного препарата)
      /// </summary>
      [Display(Description = "Дозировка (Кол-во лекарственного препарата)")]
      [MaxLength(1000, ErrorMessage = "\"Дозировка (Кол-во лекарственного препарата)\" не может быть больше 1000 символов")]
      [DataMember(Name = "drugQuantity", EmitDefaultValue = false)]
      public virtual string DrugQuantity { get; set; }
      
      
      /// <summary>
      /// Временная доза -> Доза
      /// </summary>
      [ForeignKey("Dose")]
      [DataMember(Name = "doseId")][Display(Description = "Временная доза -> Доза")]
      public virtual long? DoseId { get; set; }
      /// <summary>
      /// Временная доза -> Доза
      /// </summary>
      [InverseProperty("PupilGroupListMemberDrugDoses")]
      [Display(Description = "Временная доза -> Доза")]
      [DataMember(Name = "dose", EmitDefaultValue = false)]
      public virtual PupilDose Dose { get; set; }
      
      /// <summary>
      /// Доза воспитанника в потребности на группу отправки
      /// </summary>
      [ForeignKey("GroupPupil")]
      [DataMember(Name = "groupPupilId")]
      [Display(Description = "Доза воспитанника в потребности на группу отправки")]
      public virtual long? GroupPupilId { get; set; }
      
      
      /// <summary>
      /// Доза воспитанника в потребности на группу отправки
      /// </summary>
      [InverseProperty("GroupPupilDoses")]
      [Display(Description = "Доза воспитанника в потребности на группу отправки")]
      [DataMember(Name = "groupPupil", EmitDefaultValue = false)]
      public virtual PupilGroupListMember GroupPupil { get; set; }

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