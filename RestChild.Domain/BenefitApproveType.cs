// File:    BenefitApproveType.cs
// Purpose: Definition of Class BenefitApproveType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тип подтверждения льготы
   /// </summary>
   [Serializable]
   [DataContract(Name = "benefitApproveType")]
   public partial class BenefitApproveType : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификтор
      /// </summary>
      [Display(Description = "Уникальный идентификтор")]
      [Required(ErrorMessage = "\"Уникальный идентификтор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }

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