// File:    CategoryIncident.cs
// Purpose: Definition of Class CategoryIncident

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Категория происшествия
   /// </summary>
   [Serializable]
   [DataContract(Name = "categoryIncident")]
   public partial class CategoryIncident : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Архивная
      /// </summary>
      [Display(Description = "Архивная")]
      [Required(ErrorMessage = "\"Архивная\" должно быть заполнено")]
      [DataMember(Name = "isArchive", EmitDefaultValue = false)]
      public virtual bool IsArchive { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      
      /// <summary>
      /// Иерархия
      /// </summary>
      [InverseProperty("Parent")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<CategoryIncident> Children { get; set; }
      
      /// <summary>
      /// Иерархия
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Иерархия")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Иерархия
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Иерархия")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual CategoryIncident Parent { get; set; }

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