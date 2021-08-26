// File:    CouncelorComment.cs
// Purpose: Definition of Class CouncelorComment

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отзыв о вожатом
   /// </summary>
   [Serializable]
   [DataContract(Name = "councelorComment")]
   public partial class CouncelorComment : IEntityBase
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
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" не может быть пустым")]
      [DataMember(Name = "author", EmitDefaultValue = false)]
      public virtual string Author { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Системная информация
      /// </summary>
      [Display(Description = "Системная информация")]
      [DataMember(Name = "systemInfo", EmitDefaultValue = false)]
      public virtual String SystemInfo { get; set; }
      
      /// <summary>
      /// Отображать на сайте
      /// </summary>
      [Display(Description = "Отображать на сайте")]
      [Required(ErrorMessage = "\"Отображать на сайте\" должно быть заполнено")]
      [DataMember(Name = "visibleOnSite", EmitDefaultValue = false)]
      public virtual bool VisibleOnSite { get; set; }
      
      /// <summary>
      /// Идентификатор пользователя сайта
      /// </summary>
      [Display(Description = "Идентификатор пользователя сайта")]
      [MaxLength(1000, ErrorMessage = "\"Идентификатор пользователя сайта\" не может быть больше 1000 символов")]
      [DataMember(Name = "siteUserUid", EmitDefaultValue = false)]
      public virtual string SiteUserUid { get; set; }
      
      /// <summary>
      /// Оценка
      /// </summary>
      [Display(Description = "Оценка")]
      [DataMember(Name = "rank", EmitDefaultValue = false)]
      public virtual int? Rank { get; set; }
      
      /// <summary>
      /// Ответ на отзыв
      /// </summary>
      [Display(Description = "Ответ на отзыв")]
      [DataMember(Name = "answer", EmitDefaultValue = false)]
      public virtual String Answer { get; set; }
      
      /// <summary>
      /// Дата ответа
      /// </summary>
      [Display(Description = "Дата ответа")]
      [DataMember(Name = "answerDate", EmitDefaultValue = false)]
      public virtual DateTime? AnswerDate { get; set; }
      
      /// <summary>
      /// Проверен
      /// </summary>
      [Display(Description = "Проверен")]
      [Required(ErrorMessage = "\"Проверен\" должно быть заполнено")]
      [DataMember(Name = "checked", EmitDefaultValue = false)]
      public virtual bool Checked { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "Вожатый")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Comments")]
      [Display(Description = "Вожатый")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }
      
      /// <summary>
      /// Заявитель/сопровождающий
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Заявитель/сопровождающий")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Заявитель/сопровождающий
      /// </summary>
      [Display(Description = "Заявитель/сопровождающий")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Ребёнок")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [Display(Description = "Ребёнок")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [Display(Description = "Пользователь")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Пользователь отвечающий на отзыв
      /// </summary>
      [ForeignKey("AnswerAccount")]
      [DataMember(Name = "answerAccountId")]
      [Display(Description = "Пользователь отвечающий на отзыв")]
      public virtual long? AnswerAccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь отвечающий на отзыв
      /// </summary>
      [Display(Description = "Пользователь отвечающий на отзыв")]
      [DataMember(Name = "answerAccount", EmitDefaultValue = false)]
      public virtual Account AnswerAccount { get; set; }

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