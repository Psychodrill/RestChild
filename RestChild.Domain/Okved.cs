// File:    Okved.cs
// Purpose: Definition of Class Okved

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// ОКВЭД
   /// </summary>
   [Serializable]
   [DataContract(Name = "okved")]
   public partial class Okved : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Код
      /// </summary>
      [Display(Description = "Код")]
      [MaxLength(1000, ErrorMessage = "\"Код\" не может быть больше 1000 символов")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      
      /// <summary>
      /// Связь
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Связь")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Связь
      /// </summary>
      [Display(Description = "Связь")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual Okved Parent { get; set; }
      
      /// <summary>
      /// ОКВЭД
      /// </summary>
      [InverseProperty("Okved")]
      [DataMember(Name = "organizations", EmitDefaultValue = false)]
      public virtual ICollection<Organization> Organizations { get; set; }

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