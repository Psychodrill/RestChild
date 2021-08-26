// File:    BoutJournalFile.cs
// Purpose: Definition of Class BoutJournalFile

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Фотографии события из журнала смены
   /// </summary>
   [Serializable]
   [DataContract(Name = "boutJournalFile")]
   public partial class BoutJournalFile : IEntityBase
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
      /// Имя файла
      /// </summary>
      [Display(Description = "Имя файла")]
      [MaxLength(1000, ErrorMessage = "\"Имя файла\" не может быть больше 1000 символов")]
      [DataMember(Name = "fileName", EmitDefaultValue = false)]
      public virtual string FileName { get; set; }
      
      /// <summary>
      /// Ссылка
      /// </summary>
      [Display(Description = "Ссылка")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка\" не может быть больше 1000 символов")]
      [DataMember(Name = "fileLink", EmitDefaultValue = false)]
      public virtual string FileLink { get; set; }
      
      /// <summary>
      /// Признак что фотография
      /// </summary>
      [Display(Description = "Признак что фотография")]
      [Required(ErrorMessage = "\"Признак что фотография\" должно быть заполнено")]
      [DataMember(Name = "isPhoto", EmitDefaultValue = false)]
      public virtual bool IsPhoto { get; set; }
      
      /// <summary>
      /// Дата добавления
      /// </summary>
      [Display(Description = "Дата добавления")]
      [Required(ErrorMessage = "\"Дата добавления\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      
      /// <summary>
      /// Файлы из дневника
      /// </summary>
      [ForeignKey("BoutJournal")]
      [DataMember(Name = "boutJournalId")]
      [Display(Description = "Файлы из дневника")]
      public virtual long? BoutJournalId { get; set; }
      
      
      /// <summary>
      /// Файлы из дневника
      /// </summary>
      [InverseProperty("Files")]
      [Display(Description = "Файлы из дневника")]
      [DataMember(Name = "boutJournal", EmitDefaultValue = false)]
      public virtual BoutJournal BoutJournal { get; set; }

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