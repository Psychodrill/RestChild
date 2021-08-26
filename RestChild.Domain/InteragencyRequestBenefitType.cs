// File:    InteragencyRequestBenefitType.cs
// Purpose: Definition of Class InteragencyRequestBenefitType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Связь льготы и запроса
   /// </summary>
   [Serializable]
   [DataContract(Name = "interagencyRequestBenefitType")]
   public partial class InteragencyRequestBenefitType : IEntityBase
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
      /// Льгота
      /// </summary>
      [ForeignKey("BenefitType")]
      [DataMember(Name = "benefitTypeId")]
      [Display(Description = "Льгота")]
      public virtual long? BenefitTypeId { get; set; }
      
      
      /// <summary>
      /// Льгота
      /// </summary>
      [Display(Description = "Льгота")]
      [DataMember(Name = "benefitType", EmitDefaultValue = false)]
      public virtual BenefitType BenefitType { get; set; }
      
      /// <summary>
      /// Запрос
      /// </summary>
      [ForeignKey("InteragencyRequest")]
      [DataMember(Name = "interagencyRequestId")]
      [Display(Description = "Запрос")]
      public virtual long? InteragencyRequestId { get; set; }
      
      
      /// <summary>
      /// Запрос
      /// </summary>
      [Display(Description = "Запрос")]
      [DataMember(Name = "interagencyRequest", EmitDefaultValue = false)]
      public virtual InteragencyRequest InteragencyRequest { get; set; }

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