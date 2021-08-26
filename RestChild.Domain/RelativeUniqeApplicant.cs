// File:    RelativeUniqeApplicant.cs
// Purpose: Definition of Class RelativeUniqeApplicant

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Уникальный родственник. Заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "relativeUniqeApplicant")]
   public partial class RelativeUniqeApplicant : IEntityBase
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
      /// Сопровождающий
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Сопровождающий")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Сопровождающий
      /// </summary>
      [Display(Description = "Сопровождающий")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Заявление
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Заявление")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Заявление
      /// </summary>
      [Display(Description = "Заявление")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Примеры заявлений
      /// </summary>
      [ForeignKey("RelativeUniqe")]
      [DataMember(Name = "relativeUniqeId")]
      [Display(Description = "Примеры заявлений")]
      public virtual long? RelativeUniqeId { get; set; }
      
      
      /// <summary>
      /// Примеры заявлений
      /// </summary>
      [InverseProperty("RelativeRequests")]
      [Display(Description = "Примеры заявлений")]
      [DataMember(Name = "relativeUniqe", EmitDefaultValue = false)]
      public virtual RelativeUniqe RelativeUniqe { get; set; }

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