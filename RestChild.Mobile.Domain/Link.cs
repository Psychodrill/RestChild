// File:    Link.cs
// Purpose: Definition of Class Link

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Общая история, файлы, комментарии
    /// </summary>
    [Serializable]
    [DataContract(Name = "link")]
    public class Link : IEntityBase
    {
        /// <summary>
        ///     История
        /// </summary>
        [InverseProperty("Link")]
        [DataMember(Name = "historys", EmitDefaultValue = false)]
        public virtual ICollection<History> Historys { get; set; }

        /// <summary>
        ///     Комментарии
        /// </summary>
        [InverseProperty("Link")]
        [DataMember(Name = "commentarys", EmitDefaultValue = false)]
        public virtual ICollection<UserCommentary> Commentarys { get; set; }

        /// <summary>
        ///     Файлы
        /// </summary>
        [InverseProperty("Link")]
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
