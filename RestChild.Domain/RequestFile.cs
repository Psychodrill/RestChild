// File:    RequestFile.cs
// Purpose: Definition of Class RequestFile

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Файлы заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestFile")]
   public partial class RequestFile : IEntityBase
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
      [Required(ErrorMessage = "\"Имя файла\" не может быть пустым")]
      [DataMember(Name = "fileName", EmitDefaultValue = false)]
      public virtual string FileName { get; set; }
      
      /// <summary>
      /// Ссылка на файл
      /// </summary>
      [Display(Description = "Ссылка на файл")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на файл\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Ссылка на файл\" не может быть пустым")]
      [DataMember(Name = "fileTitle", EmitDefaultValue = false)]
      public virtual string FileTitle { get; set; }
      
      /// <summary>
      /// Дата добавления
      /// </summary>
      [Display(Description = "Дата добавления")]
      [Required(ErrorMessage = "\"Дата добавления\" должно быть заполнено")]
      [DataMember(Name = "dataCreate", EmitDefaultValue = false)]
      public virtual DateTime DataCreate { get; set; }
      
      /// <summary>
      /// Файл сохранен удаленно
      /// </summary>
      [Display(Description = "Файл сохранен удаленно")]
      [Required(ErrorMessage = "\"Файл сохранен удаленно\" должно быть заполнено")]
      [DataMember(Name = "remoteSave", EmitDefaultValue = false)]
      public virtual bool RemoteSave { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [MaxLength(1000, ErrorMessage = "\"Описание\" не может быть больше 1000 символов")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual string Description { get; set; }
      
      
      /// <summary>
      /// Файлы
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Файлы")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Файлы
      /// </summary>
      [InverseProperty("Files")]
      [Display(Description = "Файлы")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Тип файла
      /// </summary>
      [ForeignKey("RequestFileType")]
      [DataMember(Name = "requestFileTypeId")]
      [Display(Description = "Тип файла")]
      public virtual long? RequestFileTypeId { get; set; }
      
      
      /// <summary>
      /// Тип файла
      /// </summary>
      [Display(Description = "Тип файла")]
      [DataMember(Name = "requestFileType", EmitDefaultValue = false)]
      public virtual RequestFileType RequestFileType { get; set; }
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Пользователь")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [Display(Description = "Пользователь")]
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