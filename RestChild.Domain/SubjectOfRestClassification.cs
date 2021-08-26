// File:    SubjectOfRestClassification.cs
// Purpose: Definition of Class SubjectOfRestClassification

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тематика смены (классификатор)
   /// </summary>
   [Serializable]
   [DataContract(Name = "subjectOfRestClassification")]
   public partial class SubjectOfRestClassification : IEntityBase
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
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Показывать на сайте
      /// </summary>
      [Display(Description = "Показывать на сайте")]
      [Required(ErrorMessage = "\"Показывать на сайте\" должно быть заполнено")]
      [DataMember(Name = "viewOnSite", EmitDefaultValue = false)]
      public virtual bool ViewOnSite { get; set; }
      
      /// <summary>
      /// Архивный
      /// </summary>
      [Display(Description = "Архивный")]
      [Required(ErrorMessage = "\"Архивный\" должно быть заполнено")]
      [DataMember(Name = "isArchive", EmitDefaultValue = false)]
      public virtual bool IsArchive { get; set; }
      
      
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
      /// Файл
      /// </summary>
      [ForeignKey("FileOrLink")]
      [DataMember(Name = "fileOrLinkId")]
      [Display(Description = "Файл")]
      public virtual long? FileOrLinkId { get; set; }
      
      
      /// <summary>
      /// Файл
      /// </summary>
      [Display(Description = "Файл")]
      [DataMember(Name = "fileOrLink", EmitDefaultValue = false)]
      public virtual FileOrLink FileOrLink { get; set; }

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