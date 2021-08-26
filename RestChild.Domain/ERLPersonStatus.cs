// File:    ERLPersonStatus.cs
// Purpose: Definition of Class ERLPersonStatus

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// ЕРЛ статус загрузки льготника
   /// </summary>
   [Serializable]
   [DataContract(Name = "eRLPersonStatus")]
   public partial class ERLPersonStatus : IEntityBase
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
      /// Идентификатор в ЕРЛ
      /// </summary>
      [Display(Description = "Идентификатор в ЕРЛ")]
      [DataMember(Name = "personUid", EmitDefaultValue = false)]
      public virtual Guid? PersonUid { get; set; }
      
      /// <summary>
      /// Идентификатор сообщения в ЕРЛ
      /// </summary>
      [Display(Description = "Идентификатор сообщения в ЕРЛ")]
      [DataMember(Name = "eRLMessageId", EmitDefaultValue = false)]
      public virtual Guid? ERLMessageId { get; set; }
      
      /// <summary>
      /// Принято в ЕРЛ без ошибок
      /// </summary>
      [Display(Description = "Принято в ЕРЛ без ошибок")]
      [Required(ErrorMessage = "\"Принято в ЕРЛ без ошибок\" должно быть заполнено")]
      [DataMember(Name = "eRLCommited", EmitDefaultValue = false)]
      public virtual bool ERLCommited { get; set; }
      
      
      /// <summary>
      /// Связь заявителя со статусом в ЕРЛ
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Связь заявителя со статусом в ЕРЛ")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Связь заявителя со статусом в ЕРЛ
      /// </summary>
      [InverseProperty("ERLPersons")]
      [Display(Description = "Связь заявителя со статусом в ЕРЛ")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Связь ребенка со статусом ЕРЛ
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Связь ребенка со статусом ЕРЛ")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Связь ребенка со статусом ЕРЛ
      /// </summary>
      [InverseProperty("ERLPersons")]
      [Display(Description = "Связь ребенка со статусом ЕРЛ")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Связь статуса персоны в ЕРЛ с его льготами
      /// </summary>
      [InverseProperty("Person")]
      [DataMember(Name = "benefits", EmitDefaultValue = false)]
      public virtual ICollection<ERLBenefitStatus> Benefits { get; set; }

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