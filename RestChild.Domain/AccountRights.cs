// File:    AccountRights.cs
// Purpose: Definition of Class AccountRights

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Права пользователей
   /// </summary>
   [Serializable]
   [DataContract(Name = "accountRights")]
   public partial class AccountRights : IEntityBase
   {
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      
      /// <summary>
      /// Связь с правом
      /// </summary>
      [ForeignKey("AccessRight")]
      [DataMember(Name = "accessRightId")][Display(Description = "Связь с правом")]
      public virtual long? AccessRightId { get; set; }
      /// <summary>
      /// Связь с правом
      /// </summary>
      [InverseProperty("Rights")]
      [Display(Description = "Связь с правом")]
      [DataMember(Name = "accessRight", EmitDefaultValue = false)]
      public virtual AccessRight AccessRight { get; set; }
      
      /// <summary>
      /// Связь пользователей с правами
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")][Display(Description = "Связь пользователей с правами")]
      public virtual long? AccountId { get; set; }
      /// <summary>
      /// Связь пользователей с правами
      /// </summary>
      [InverseProperty("Rights")]
      [Display(Description = "Связь пользователей с правами")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }

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