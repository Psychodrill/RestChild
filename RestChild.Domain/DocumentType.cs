// File:    DocumentType.cs
// Purpose: Definition of Class DocumentType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вид документа
   /// </summary>
   [Serializable]
   [DataContract(Name = "documentType")]
   public partial class DocumentType : IEntityBase
   {
      
      /// <summary>
      /// Код документа
      /// </summary>
      [Display(Description = "Код документа")]
      [Required(ErrorMessage = "\"Код документа\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Наименование документа
      /// </summary>
      [Display(Description = "Наименование документа")]
      [MaxLength(1000, ErrorMessage = "\"Наименование документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Владелец
      /// </summary>
      [Display(Description = "Владелец")]
      [MaxLength(1000, ErrorMessage = "\"Владелец\" не может быть больше 1000 символов")]
      [DataMember(Name = "owner", EmitDefaultValue = false)]
      public virtual string Owner { get; set; }
      
      /// <summary>
      /// Глобальный идентификатор
      /// </summary>
      [Display(Description = "Глобальный идентификатор")]
      [MaxLength(1000, ErrorMessage = "\"Глобальный идентификатор\" не может быть больше 1000 символов")]
      [DataMember(Name = "globalUid", EmitDefaultValue = false)]
      public virtual string GlobalUid { get; set; }
      
      /// <summary>
      /// Для ребенка
      /// </summary>
      [Display(Description = "Для ребенка")]
      [Required(ErrorMessage = "\"Для ребенка\" должно быть заполнено")]
      [DataMember(Name = "forChild", EmitDefaultValue = false)]
      public virtual bool ForChild { get; set; }
      
      /// <summary>
      /// Для загран документов
      /// </summary>
      [Display(Description = "Для загран документов")]
      [Required(ErrorMessage = "\"Для загран документов\" должно быть заполнено")]
      [DataMember(Name = "forForeign", EmitDefaultValue = false)]
      public virtual bool ForForeign { get; set; }
      
      /// <summary>
      /// Остальные
      /// </summary>
      [Display(Description = "Остальные")]
      [Required(ErrorMessage = "\"Остальные\" должно быть заполнено")]
      [DataMember(Name = "forOther", EmitDefaultValue = false)]
      public virtual bool ForOther { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "baseRegistryUid", EmitDefaultValue = false)]
      public virtual string BaseRegistryUid { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "forApplicant", EmitDefaultValue = false)]
      public virtual bool ForApplicant { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "forAgent", EmitDefaultValue = false)]
      public virtual bool ForAgent { get; set; }
      
      
      /// <summary>
      /// Связь типа документа с целью обращения
      /// </summary>
      [InverseProperty("DocumentTypes")]
      [DataMember(Name = "typesOfRest", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRest> TypesOfRest { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }

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