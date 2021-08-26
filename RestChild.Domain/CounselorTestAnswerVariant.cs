// File:    CounselorTestAnswerVariant.cs
// Purpose: Definition of Class CounselorTestAnswerVariant

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Варианты ответа
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTestAnswerVariant")]
   public partial class CounselorTestAnswerVariant : IEntityBase
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
      /// Текст ответа
      /// </summary>
      [Display(Description = "Текст ответа")]
      [DataMember(Name = "text", EmitDefaultValue = false)]
      public virtual String Text { get; set; }
      
      /// <summary>
      /// Правильность
      /// </summary>
      [Display(Description = "Правильность")]
      [Required(ErrorMessage = "\"Правильность\" должно быть заполнено")]
      [DataMember(Name = "isTrue", EmitDefaultValue = false)]
      public virtual bool IsTrue { get; set; }
      
      /// <summary>
      /// Удален
      /// </summary>
      [Display(Description = "Удален")]
      [Required(ErrorMessage = "\"Удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Порядок ответов
      /// </summary>
      [Display(Description = "Порядок ответов")]
      [Required(ErrorMessage = "\"Порядок ответов\" должно быть заполнено")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual int SortOrder { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("Variant")]
      [DataMember(Name = "answer", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestAnswer> Answer { get; set; }
      
      /// <summary>
      /// Варианты ответа
      /// </summary>
      [ForeignKey("Question")]
      [DataMember(Name = "questionId")]
      [Display(Description = "Варианты ответа")]
      public virtual long? QuestionId { get; set; }
      
      
      /// <summary>
      /// Варианты ответа
      /// </summary>
      [InverseProperty("Variants")]
      [Display(Description = "Варианты ответа")]
      [DataMember(Name = "question", EmitDefaultValue = false)]
      public virtual CounselorTestQuestion Question { get; set; }
      
      /// <summary>
      /// Ссылка на файл
      /// </summary>
      [ForeignKey("FileOrLink")]
      [DataMember(Name = "fileOrLinkId")]
      [Display(Description = "Ссылка на файл")]
      public virtual long? FileOrLinkId { get; set; }
      
      
      /// <summary>
      /// Ссылка на файл
      /// </summary>
      [Display(Description = "Ссылка на файл")]
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