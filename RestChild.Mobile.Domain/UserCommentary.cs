// File:    UserCommentary.cs
// Purpose: Definition of Class UserCommentary

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Комментарий
    /// </summary>
    [Serializable]
    [DataContract(Name = "userCommentary")]
    public class UserCommentary : IEntityBase
    {
        /// <summary>
        ///     Примечание
        /// </summary>
        [Display(Description = "Примечание")]
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public virtual string Comment { get; set; }

        /// <summary>
        ///     Строка с автором изменений
        /// </summary>
        [Display(Description = "Строка с автором изменений")]
        [MaxLength(1000, ErrorMessage = "\"Строка с автором изменений\" не может быть больше 1000 символов")]
        [DataMember(Name = "authorString", EmitDefaultValue = false)]
        public virtual string AuthorString { get; set; }

        /// <summary>
        ///     Дата действия
        /// </summary>
        [Display(Description = "Дата действия")]
        [Required(ErrorMessage = "\"Дата действия\" должно быть заполнено")]
        [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
        public virtual DateTime DateCreate { get; set; }


        /// <summary>
        ///     Иеархия комментариев
        /// </summary>
        [InverseProperty("Parent")]
        [DataMember(Name = "children", EmitDefaultValue = false)]
        public virtual ICollection<UserCommentary> Children { get; set; }

        /// <summary>
        ///     Комментарий
        /// </summary>
        [InverseProperty("Commentary")]
        [DataMember(Name = "files", EmitDefaultValue = false)]
        public virtual ICollection<FileItemUserCommentary> Files { get; set; }

        /// <summary>
        ///     Иеархия комментариев
        /// </summary>
        [ForeignKey("Parent")]
        [DataMember(Name = "parentId")]
        [Display(Description = "Иеархия комментариев")]
        public virtual long? ParentId { get; set; }


        /// <summary>
        ///     Иеархия комментариев
        /// </summary>
        [InverseProperty("Children")]
        [Display(Description = "Иеархия комментариев")]
        [DataMember(Name = "parent", EmitDefaultValue = false)]
        public virtual UserCommentary Parent { get; set; }

        /// <summary>
        ///     Комментарии
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "Комментарии")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     Комментарии
        /// </summary>
        [InverseProperty("Commentarys")]
        [Display(Description = "Комментарии")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

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
