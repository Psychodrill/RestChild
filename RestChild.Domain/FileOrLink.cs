// File:    FileOrLink.cs
// Purpose: Definition of Class FileOrLink

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Файл
   /// </summary>
   [Serializable]
   [DataContract(Name = "fileOrLink")]
   public partial class FileOrLink : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Имя файла
      /// </summary>
      [Display(Description = "Имя файла")]
      [MaxLength(1000, ErrorMessage = "\"Имя файла\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Имя файла\" не может быть пустым")]
      [DataMember(Name = "fileName", EmitDefaultValue = false)]
      public virtual string FileName { get; set; }
      
      /// <summary>
      /// Ссылка на файл подписи
      /// </summary>
      [Display(Description = "Ссылка на файл подписи")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на файл подписи\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Ссылка на файл подписи\" не может быть пустым")]
      [DataMember(Name = "fileUrl", EmitDefaultValue = false)]
      public virtual string FileUrl { get; set; }
      
      /// <summary>
      /// Признак что фотография
      /// </summary>
      [Display(Description = "Признак что фотография")]
      [Required(ErrorMessage = "\"Признак что фотография\" должно быть заполнено")]
      [DataMember(Name = "isPhoto", EmitDefaultValue = false)]
      public virtual bool IsPhoto { get; set; }
      
      /// <summary>
      /// Признак что видео
      /// </summary>
      [Display(Description = "Признак что видео")]
      [Required(ErrorMessage = "\"Признак что видео\" должно быть заполнено")]
      [DataMember(Name = "isVideo", EmitDefaultValue = false)]
      public virtual bool IsVideo { get; set; }
      
      /// <summary>
      /// Главный
      /// </summary>
      [Display(Description = "Главный")]
      [Required(ErrorMessage = "\"Главный\" должно быть заполнено")]
      [DataMember(Name = "isMain", EmitDefaultValue = false)]
      public virtual bool IsMain { get; set; }
      
      
      /// <summary>
      /// Сылка на файлы
      /// </summary>
      [ForeignKey("Link")]
      [DataMember(Name = "linkId")]
      [Display(Description = "Сылка на файлы")]
      public virtual long? LinkId { get; set; }
      
      
      /// <summary>
      /// Сылка на файлы
      /// </summary>
      [InverseProperty("Files")]
      [Display(Description = "Сылка на файлы")]
      [DataMember(Name = "link", EmitDefaultValue = false)]
      public virtual LinkToFile Link { get; set; }

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