// File:    AccessRight.cs
// Purpose: Definition of Class AccessRight

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Право
   /// </summary>
   [Serializable]
   [DataContract(Name = "accessRight")]
   public partial class AccessRight : IEntityBase
   {
      
      /// <summary>
      /// Униакльный идентификатор
      /// </summary>
      [Display(Description = "Униакльный идентификатор")]
      [Required(ErrorMessage = "\"Униакльный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Код права
      /// </summary>
      [Display(Description = "Код права")]
      [MaxLength(1000, ErrorMessage = "\"Код права\" не может быть больше 1000 символов")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Наименование права
      /// </summary>
      [Display(Description = "Наименование права")]
      [MaxLength(1000, ErrorMessage = "\"Наименование права\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Применяется к организации
      /// </summary>
      [Display(Description = "Применяется к организации")]
      [Required(ErrorMessage = "\"Применяется к организации\" должно быть заполнено")]
      [DataMember(Name = "forOrganization", EmitDefaultValue = false)]
      public virtual bool ForOrganization { get; set; }
      
      /// <summary>
      /// Код группы права
      /// </summary>
      [Display(Description = "Код группы права")]
      [MaxLength(1000, ErrorMessage = "\"Код группы права\" не может быть больше 1000 символов")]
      [DataMember(Name = "groupCode", EmitDefaultValue = false)]
      public virtual string GroupCode { get; set; }
      
      
      /// <summary>
      /// Права роли
      /// </summary>
      [InverseProperty("AccessRights")]
      [DataMember(Name = "roles", EmitDefaultValue = false)]
      public virtual ICollection<Role> Roles { get; set; }
      
      /// <summary>
      /// Связь с правом
      /// </summary>
      [InverseProperty("AccessRight")]
      [DataMember(Name = "rights", EmitDefaultValue = false)]
      public virtual ICollection<AccountRights> Rights { get; set; }

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