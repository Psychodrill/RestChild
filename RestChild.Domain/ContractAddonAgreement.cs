// File:    ContractAddonAgreement.cs
// Purpose: Definition of Class ContractAddonAgreement

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Договор. Дополнительное соглашение
   /// </summary>
   [Serializable]
   [DataContract(Name = "contractAddonAgreement")]
   public partial class ContractAddonAgreement : IEntityBase
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
      /// Номер
      /// </summary>
      [Display(Description = "Номер")]
      [MaxLength(1000, ErrorMessage = "\"Номер\" не может быть больше 1000 символов")]
      [DataMember(Name = "signNumber", EmitDefaultValue = false)]
      public virtual string SignNumber { get; set; }
      
      /// <summary>
      /// Дата
      /// </summary>
      [Display(Description = "Дата")]
      [DataMember(Name = "signDate", EmitDefaultValue = false)]
      public virtual DateTime? SignDate { get; set; }
      
      /// <summary>
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      
      /// <summary>
      /// Доп соглашения
      /// </summary>
      [ForeignKey("Contract")]
      [DataMember(Name = "contractId")]
      [Display(Description = "Доп соглашения")]
      public virtual long? ContractId { get; set; }
      
      
      /// <summary>
      /// Доп соглашения
      /// </summary>
      [InverseProperty("AddonAgreements")]
      [Display(Description = "Доп соглашения")]
      [DataMember(Name = "contract", EmitDefaultValue = false)]
      public virtual Contract Contract { get; set; }

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