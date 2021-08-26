// File:    AccountRoles.cs
// Purpose: Definition of Class AccountRoles

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Роли пользователя
   /// </summary>
   [Serializable]
   [DataContract(Name = "accountRoles")]
   public partial class AccountRoles : IEntityBase
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
      /// Связь роли и пользователя
      /// </summary>
      [ForeignKey("Role")]
      [DataMember(Name = "roleId")]
      [Display(Description = "Связь роли и пользователя")]
      public virtual long? RoleId { get; set; }
      
      
      /// <summary>
      /// Связь роли и пользователя
      /// </summary>
      [Display(Description = "Связь роли и пользователя")]
      [DataMember(Name = "role", EmitDefaultValue = false)]
      public virtual Role Role { get; set; }
      
      /// <summary>
      /// Связь пользователя и роли
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Связь пользователя и роли")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Связь пользователя и роли
      /// </summary>
      [InverseProperty("Roles")]
      [Display(Description = "Связь пользователя и роли")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Связь организации и роли
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Связь организации и роли")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Связь организации и роли
      /// </summary>
      [Display(Description = "Связь организации и роли")]
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