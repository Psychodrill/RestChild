// File:    SubjectOfRest.cs
// Purpose: Definition of Class SubjectOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тематика смены
   /// </summary>
   [Serializable]
   [DataContract(Name = "subjectOfRest")]
   public partial class SubjectOfRest : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание места
      /// </summary>
      [Display(Description = "Описание места")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Ссылка на фотографию
      /// </summary>
      [Display(Description = "Ссылка на фотографию")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на фотографию\" не может быть больше 1000 символов")]
      [DataMember(Name = "photoUrl", EmitDefaultValue = false)]
      public virtual string PhotoUrl { get; set; }
      
      /// <summary>
      /// Описание смены в HTML
      /// </summary>
      [Display(Description = "Описание смены в HTML")]
      [DataMember(Name = "descriptionHtml", EmitDefaultValue = false)]
      public virtual String DescriptionHtml { get; set; }
      
      /// <summary>
      /// Активность
      /// </summary>
      [Display(Description = "Активность")]
      [Required(ErrorMessage = "\"Активность\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Показывать на сайте
      /// </summary>
      [Display(Description = "Показывать на сайте")]
      [Required(ErrorMessage = "\"Показывать на сайте\" должно быть заполнено")]
      [DataMember(Name = "viewOnSite", EmitDefaultValue = false)]
      public virtual bool ViewOnSite { get; set; }
      
      /// <summary>
      /// Показывать на МПГУ
      /// </summary>
      [Display(Description = "Показывать на МПГУ")]
      [Required(ErrorMessage = "\"Показывать на МПГУ\" должно быть заполнено")]
      [DataMember(Name = "viewOnMpgu", EmitDefaultValue = false)]
      public virtual bool ViewOnMpgu { get; set; }
      
      
      /// <summary>
      /// Ссылка на документы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")][Display(Description = "Ссылка на документы")]
      public virtual long? LinkToFileId { get; set; }
      /// <summary>
      /// Ссылка на документы
      /// </summary>
      [Display(Description = "Ссылка на документы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }
      
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
      /// Классификация тематик смены
      /// </summary>
      [ForeignKey("SubjectOfRestClassification")]
      [DataMember(Name = "subjectOfRestClassificationId")]
      [Display(Description = "Классификация тематик смены")]
      public virtual long? SubjectOfRestClassificationId { get; set; }
      
      
      /// <summary>
      /// Классификация тематик смены
      /// </summary>
      [Display(Description = "Классификация тематик смены")]
      [DataMember(Name = "subjectOfRestClassification", EmitDefaultValue = false)]
      public virtual SubjectOfRestClassification SubjectOfRestClassification { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }

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