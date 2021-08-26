// File:    CounselorTestQuestion.cs
// Purpose: Definition of Class CounselorTestQuestion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вопросы по тестам
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTestQuestion")]
   public partial class CounselorTestQuestion : IEntityBase
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
      /// Текст вопроса
      /// </summary>
      [Display(Description = "Текст вопроса")]
      [DataMember(Name = "question", EmitDefaultValue = false)]
      public virtual String Question { get; set; }
      
      /// <summary>
      /// Удален
      /// </summary>
      [Display(Description = "Удален")]
      [Required(ErrorMessage = "\"Удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Порядок вопроса
      /// </summary>
      [Display(Description = "Порядок вопроса")]
      [Required(ErrorMessage = "\"Порядок вопроса\" должно быть заполнено")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual int SortOrder { get; set; }
      
      
      /// <summary>
      /// Варианты ответа
      /// </summary>
      [InverseProperty("Question")]
      [DataMember(Name = "variants", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestAnswerVariant> Variants { get; set; }
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("Question")]
      [DataMember(Name = "answer", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestAnswer> Answer { get; set; }
      
      /// <summary>
      /// Вопросы
      /// </summary>
      [ForeignKey("CounselorTest")]
      [DataMember(Name = "counselorTestId")]
      [Display(Description = "Вопросы")]
      public virtual long? CounselorTestId { get; set; }
      
      
      /// <summary>
      /// Вопросы
      /// </summary>
      [InverseProperty("Questions")]
      [Display(Description = "Вопросы")]
      [DataMember(Name = "counselorTest", EmitDefaultValue = false)]
      public virtual CounselorTest CounselorTest { get; set; }
      
      /// <summary>
      /// Тип вопроса
      /// </summary>
      [ForeignKey("Type")]
      [DataMember(Name = "typeId")]
      [Display(Description = "Тип вопроса")]
      public virtual long? TypeId { get; set; }
      
      
      /// <summary>
      /// Тип вопроса
      /// </summary>
      [Display(Description = "Тип вопроса")]
      [DataMember(Name = "type", EmitDefaultValue = false)]
      public virtual CounselorTestQuestionType Type { get; set; }
      
      /// <summary>
      /// Вид тематики
      /// </summary>
      [ForeignKey("CounselorTestSubject")]
      [DataMember(Name = "counselorTestSubjectId")]
      [Display(Description = "Вид тематики")]
      public virtual long? CounselorTestSubjectId { get; set; }
      
      
      /// <summary>
      /// Вид тематики
      /// </summary>
      [Display(Description = "Вид тематики")]
      [DataMember(Name = "counselorTestSubject", EmitDefaultValue = false)]
      public virtual CounselorTestSubject CounselorTestSubject { get; set; }

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