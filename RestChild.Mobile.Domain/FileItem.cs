// File:    FileItem.cs
// Purpose: Definition of Class FileItem

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Файл
    /// </summary>
    [Serializable]
    [DataContract(Name = "fileItem")]
    public class FileItem : IEntityBase
    {
        /// <summary>
        ///     Номер
        /// </summary>
        [Display(Description = "Номер")]
        [MaxLength(1000, ErrorMessage = "\"Номер\" не может быть больше 1000 символов")]
        [DataMember(Name = "fileNumber", EmitDefaultValue = false)]
        public virtual string FileNumber { get; set; }

        /// <summary>
        ///     Дата
        /// </summary>
        [Display(Description = "Дата")]
        [DataMember(Name = "fileDate", EmitDefaultValue = false)]
        public virtual DateTime? FileDate { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "fileTitle", EmitDefaultValue = false)]
        public virtual string FileTitle { get; set; }

        /// <summary>
        ///     Имя файла
        /// </summary>
        [Display(Description = "Имя файла")]
        [MaxLength(1000, ErrorMessage = "\"Имя файла\" не может быть больше 1000 символов")]
        [Required(ErrorMessage = "\"Имя файла\" не может быть пустым")]
        [DataMember(Name = "fileName", EmitDefaultValue = false)]
        public virtual string FileName { get; set; }

        /// <summary>
        ///     Ссылка на файл
        /// </summary>
        [Display(Description = "Ссылка на файл")]
        [MaxLength(1000, ErrorMessage = "\"Ссылка на файл\" не может быть больше 1000 символов")]
        [Required(ErrorMessage = "\"Ссылка на файл\" не может быть пустым")]
        [DataMember(Name = "fileUrl", EmitDefaultValue = false)]
        public virtual string FileUrl { get; set; }

        /// <summary>
        ///     Дата действия
        /// </summary>
        [Display(Description = "Дата действия")]
        [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
        public virtual DateTime? DateCreate { get; set; }

        /// <summary>
        ///     Главный
        /// </summary>
        [Display(Description = "Главный")]
        [Required(ErrorMessage = "\"Главный\" должно быть заполнено")]
        [DataMember(Name = "isMain", EmitDefaultValue = false)]
        public virtual bool IsMain { get; set; }

        /// <summary>
        ///     Описание файла
        /// </summary>
        [Display(Description = "Описание файла")]
        [DataMember(Name = "fileDescription", EmitDefaultValue = false)]
        public virtual string FileDescription { get; set; }

        /// <summary>
        ///     Размер файла
        /// </summary>
        [Display(Description = "Размер файла")]
        [Required(ErrorMessage = "\"Размер файла\" должно быть заполнено")]
        [DataMember(Name = "fileSize", EmitDefaultValue = false)]
        public virtual int FileSize { get; set; }


        /// <summary>
        ///     Файл
        /// </summary>
        [InverseProperty("File")]
        [DataMember(Name = "commentarys", EmitDefaultValue = false)]
        public virtual ICollection<FileItemUserCommentary> Commentarys { get; set; }

        /// <summary>
        ///     Файлы в комментарии
        /// </summary>
        [ForeignKey("FileItemUserCommentary")]
        [DataMember(Name = "fileItemUserCommentaryId")]
        [Display(Description = "Файлы в комментарии")]
        public virtual long? FileItemUserCommentaryId { get; set; }

        /// <summary>
        ///     Файлы в комментарии
        /// </summary>
        [InverseProperty("Files")]
        [Display(Description = "Файлы в комментарии")]
        [DataMember(Name = "fileItemUserCommentary", EmitDefaultValue = false)]
        public virtual FileItemUserCommentary FileItemUserCommentary { get; set; }

        /// <summary>
        ///     Пользователь
        /// </summary>
        [ForeignKey("Account")]
        [DataMember(Name = "accountId")]
        [Display(Description = "Пользователь")]
        public virtual long? AccountId { get; set; }


        /// <summary>
        ///     Пользователь
        /// </summary>
        [Display(Description = "Пользователь")]
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public virtual Account Account { get; set; }

        /// <summary>
        ///     Тип файла
        /// </summary>
        [ForeignKey("FileItemType")]
        [DataMember(Name = "fileItemTypeId")]
        [Display(Description = "Тип файла")]
        public virtual long? FileItemTypeId { get; set; }


        /// <summary>
        ///     Тип файла
        /// </summary>
        [Display(Description = "Тип файла")]
        [DataMember(Name = "fileItemType", EmitDefaultValue = false)]
        public virtual FileItemType FileItemType { get; set; }

        /// <summary>
        ///     Файлы
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "Файлы")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     Файлы
        /// </summary>
        [InverseProperty("Files")]
        [Display(Description = "Файлы")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public virtual long Id { get; set; }

        /// <summary>
        ///     Последнее сохранение
        /// </summary>
        [Display(Description = "Последнее сохранение")]
        [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
        public virtual long LastUpdateTick { get; set; }

        /// <summary>
        ///     Внешний ключ
        /// </summary>
        [Display(Description = "Внешний ключ")]
        [DataMember(Name = "eid", EmitDefaultValue = false)]
        [Index]
        public virtual long? Eid { get; set; }

        /// <summary>
        ///     Статус обмена по сущности
        /// </summary>
        [Display(Description = "Статус обмена по сущности")]
        [DataMember(Name = "eidSendStatus", EmitDefaultValue = false)]
        [Index]
        public virtual long? EidSendStatus { get; set; }

        /// <summary>
        ///     Дата синхронизации
        /// </summary>
        [Display(Description = "Дата синхронизации")]
        [DataMember(Name = "eidSyncDate", EmitDefaultValue = false)]
        public virtual DateTime? EidSyncDate { get; set; }
    }
}
