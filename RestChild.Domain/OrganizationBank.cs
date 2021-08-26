// File:    OrganizationBank.cs
// Purpose: Definition of Class OrganizationBank

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Организация. Банковские реквизиты
   /// </summary>
   [Serializable]
   [DataContract(Name = "organizationBank")]
   public partial class OrganizationBank : IEntityBase
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
      /// Банк
      /// </summary>
      [Display(Description = "Банк")]
      [MaxLength(1000, ErrorMessage = "\"Банк\" не может быть больше 1000 символов")]
      [DataMember(Name = "bank", EmitDefaultValue = false)]
      public virtual string Bank { get; set; }
      
      /// <summary>
      /// БИК
      /// </summary>
      [Display(Description = "БИК")]
      [MaxLength(1000, ErrorMessage = "\"БИК\" не может быть больше 1000 символов")]
      [DataMember(Name = "bik", EmitDefaultValue = false)]
      public virtual string Bik { get; set; }
      
      /// <summary>
      /// ИНН
      /// </summary>
      [Display(Description = "ИНН")]
      [MaxLength(1000, ErrorMessage = "\"ИНН\" не может быть больше 1000 символов")]
      [DataMember(Name = "inn", EmitDefaultValue = false)]
      public virtual string Inn { get; set; }
      
      /// <summary>
      /// Кор счет
      /// </summary>
      [Display(Description = "Кор счет")]
      [MaxLength(1000, ErrorMessage = "\"Кор счет\" не может быть больше 1000 символов")]
      [DataMember(Name = "correspondent", EmitDefaultValue = false)]
      public virtual string Correspondent { get; set; }
      
      /// <summary>
      /// Расчетный счет
      /// </summary>
      [Display(Description = "Расчетный счет")]
      [MaxLength(1000, ErrorMessage = "\"Расчетный счет\" не может быть больше 1000 символов")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual string Account { get; set; }
      
      /// <summary>
      /// Примечание
      /// </summary>
      [Display(Description = "Примечание")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      
      /// <summary>
      /// Банковские реквизиты
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Банковские реквизиты")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Банковские реквизиты
      /// </summary>
      [InverseProperty("Bank")]
      [Display(Description = "Банковские реквизиты")]
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