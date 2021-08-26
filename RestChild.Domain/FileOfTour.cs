// File:    FileOfTour.cs
// Purpose: Definition of Class FileOfTour

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Файлы смены
   /// </summary>
   [Serializable]
   [DataContract(Name = "fileOfTour")]
   public partial class FileOfTour : IEntityBase
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
      [DataMember(Name = "fileUrl", EmitDefaultValue = false)]
      public virtual string FileUrl { get; set; }
      
      
      /// <summary>
      /// Связь заезда и фотографий
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Связь заезда и фотографий")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Связь заезда и фотографий
      /// </summary>
      [InverseProperty("Files")]
      [Display(Description = "Связь заезда и фотографий")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("FileType")]
      [DataMember(Name = "fileTypeId")]
      [Display(Description = "")]
      public virtual long? FileTypeId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "fileType", EmitDefaultValue = false)]
      public virtual FileType FileType { get; set; }

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