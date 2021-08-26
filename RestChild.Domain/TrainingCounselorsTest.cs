// File:    TrainingCounselorsTest.cs
// Purpose: Definition of Class TrainingCounselorsTest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тестирование обучаемых
   /// </summary>
   [Serializable]
   [DataContract(Name = "trainingCounselorsTest")]
   public partial class TrainingCounselorsTest : IEntityBase
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
      /// Рейтинг
      /// </summary>
      [Display(Description = "Рейтинг")]
      [DataMember(Name = "rating", EmitDefaultValue = false)]
      public virtual decimal? Rating { get; set; }
      
      /// <summary>
      /// Дата тестирования
      /// </summary>
      [Display(Description = "Дата тестирования")]
      [DataMember(Name = "dateTesting", EmitDefaultValue = false)]
      public virtual DateTime? DateTesting { get; set; }
      
      /// <summary>
      /// Идентификатор для входа на тест
      /// </summary>
      [Display(Description = "Идентификатор для входа на тест")]
      [DataMember(Name = "testGuid", EmitDefaultValue = false)]
      public virtual Guid? TestGuid { get; set; }
      
      /// <summary>
      /// Тест пройден
      /// </summary>
      [Display(Description = "Тест пройден")]
      [Required(ErrorMessage = "\"Тест пройден\" должно быть заполнено")]
      [DataMember(Name = "isComplited", EmitDefaultValue = false)]
      public virtual bool IsComplited { get; set; }
      
      /// <summary>
      /// Последняя попытка
      /// </summary>
      [Display(Description = "Последняя попытка")]
      [Required(ErrorMessage = "\"Последняя попытка\" должно быть заполнено")]
      [DataMember(Name = "isLastAttempt", EmitDefaultValue = false)]
      public virtual bool IsLastAttempt { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("TrainingCounselorsTest")]
      [DataMember(Name = "answers", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestAnswer> Answers { get; set; }
      
      /// <summary>
      /// Тесты
      /// </summary>
      [ForeignKey("TrainingCounselorsResult")]
      [DataMember(Name = "trainingCounselorsResultId")]
      [Display(Description = "Тесты")]
      public virtual long? TrainingCounselorsResultId { get; set; }
      
      
      /// <summary>
      /// Тесты
      /// </summary>
      [InverseProperty("Tests")]
      [Display(Description = "Тесты")]
      [DataMember(Name = "trainingCounselorsResult", EmitDefaultValue = false)]
      public virtual TrainingCounselorsResult TrainingCounselorsResult { get; set; }
      
      /// <summary>
      /// Связь с обучаемыми для тестирования
      /// </summary>
      [ForeignKey("GroupTest")]
      [DataMember(Name = "groupTestId")]
      [Display(Description = "Связь с обучаемыми для тестирования")]
      public virtual long? GroupTestId { get; set; }
      
      
      /// <summary>
      /// Связь с обучаемыми для тестирования
      /// </summary>
      [InverseProperty("Students")]
      [Display(Description = "Связь с обучаемыми для тестирования")]
      [DataMember(Name = "groupTest", EmitDefaultValue = false)]
      public virtual TrainingCounselorsGroupTest GroupTest { get; set; }

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