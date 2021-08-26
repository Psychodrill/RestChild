// File:    FileItemUserCommentary.cs
// Purpose: Definition of Class FileItemUserCommentary

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Файл-комментарий
    /// </summary>
    [Serializable]
    [DataContract(Name = "fileItemUserCommentary")]
    public class FileItemUserCommentary : IEntityBase
    {
        /// <summary>
        ///     Комментарий
        /// </summary>
        [ForeignKey("Commentary")]
        [DataMember(Name = "commentaryId")]
        [Display(Description = "Комментарий")]
        public virtual long? CommentaryId { get; set; }


        /// <summary>
        ///     Комментарий
        /// </summary>
        [InverseProperty("Files")]
        [Display(Description = "Комментарий")]
        [DataMember(Name = "commentary", EmitDefaultValue = false)]
        public virtual UserCommentary Commentary { get; set; }

        /// <summary>
        ///     Файл
        /// </summary>
        [ForeignKey("File")]
        [DataMember(Name = "fileId")]
        [Display(Description = "Файл")]
        public virtual long? FileId { get; set; }


        /// <summary>
        ///     Файл
        /// </summary>
        [InverseProperty("Commentarys")]
        [Display(Description = "Файл")]
        [DataMember(Name = "file", EmitDefaultValue = false)]
        public virtual FileItem File { get; set; }

        /// <summary>
        ///     Файлы в комментарии
        /// </summary>
        [InverseProperty("FileItemUserCommentary")]
        [DataMember(Name = "files", EmitDefaultValue = false)]
        public virtual ICollection<FileItem> Files { get; set; }

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
