// File:    CounselorTestAnswer.cs
// Purpose: Definition of Class CounselorTestAnswer

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Ответ на вопросы
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTestAnswer")]
   public partial class CounselorTestAnswer : IEntityBase
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
      [DataMember(Name = "answer", EmitDefaultValue = false)]
      public virtual String Answer { get; set; }
      
      /// <summary>
      /// Оценка
      /// </summary>
      [Display(Description = "Оценка")]
      [DataMember(Name = "raiting", EmitDefaultValue = false)]
      public virtual decimal? Raiting { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [ForeignKey("CounselorTest")]
      [DataMember(Name = "counselorTestId")]
      [Display(Description = "Ответы на вопросы")]
      public virtual long? CounselorTestId { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [Display(Description = "Ответы на вопросы")]
      [DataMember(Name = "counselorTest", EmitDefaultValue = false)]
      public virtual CounselorTest CounselorTest { get; set; }
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [ForeignKey("Question")]
      [DataMember(Name = "questionId")]
      [Display(Description = "Ответы на вопросы")]
      public virtual long? QuestionId { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("Answer")]
      [Display(Description = "Ответы на вопросы")]
      [DataMember(Name = "question", EmitDefaultValue = false)]
      public virtual CounselorTestQuestion Question { get; set; }
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [ForeignKey("Variant")]
      [DataMember(Name = "variantId")]
      [Display(Description = "Ответы на вопросы")]
      public virtual long? VariantId { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("Answer")]
      [Display(Description = "Ответы на вопросы")]
      [DataMember(Name = "variant", EmitDefaultValue = false)]
      public virtual CounselorTestAnswerVariant Variant { get; set; }
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [ForeignKey("TrainingCounselor")]
      [DataMember(Name = "trainingCounselorId")]
      [Display(Description = "Ответы на вопросы")]
      public virtual long? TrainingCounselorId { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("Answers")]
      [Display(Description = "Ответы на вопросы")]
      [DataMember(Name = "trainingCounselor", EmitDefaultValue = false)]
      public virtual TrainingCounselorsResult TrainingCounselor { get; set; }
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [ForeignKey("TrainingCounselorsTest")]
      [DataMember(Name = "trainingCounselorsTestId")]
      [Display(Description = "Ответы на вопросы")]
      public virtual long? TrainingCounselorsTestId { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("Answers")]
      [Display(Description = "Ответы на вопросы")]
      [DataMember(Name = "trainingCounselorsTest", EmitDefaultValue = false)]
      public virtual TrainingCounselorsTest TrainingCounselorsTest { get; set; }

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