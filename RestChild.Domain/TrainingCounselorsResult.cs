// File:    TrainingCounselorsResult.cs
// Purpose: Definition of Class TrainingCounselorsResult

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Результат обучения вожатого
   /// </summary>
   [Serializable]
   [DataContract(Name = "trainingCounselorsResult")]
   public partial class TrainingCounselorsResult : IEntityBase
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
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      /// <summary>
      /// Оценка финального теста
      /// </summary>
      [Display(Description = "Оценка финального теста")]
      [DataMember(Name = "rank", EmitDefaultValue = false)]
      public virtual int? Rank { get; set; }
      
      /// <summary>
      /// Дата прохождения финального теста
      /// </summary>
      [Display(Description = "Дата прохождения финального теста")]
      [DataMember(Name = "dateOfFinalTest", EmitDefaultValue = false)]
      public virtual DateTime? DateOfFinalTest { get; set; }
      
      /// <summary>
      /// Дата зачисления
      /// </summary>
      [Display(Description = "Дата зачисления")]
      [DataMember(Name = "dateInclude", EmitDefaultValue = false)]
      public virtual DateTime? DateInclude { get; set; }
      
      /// <summary>
      /// Дата отчисления
      /// </summary>
      [Display(Description = "Дата отчисления")]
      [DataMember(Name = "dateExclude", EmitDefaultValue = false)]
      public virtual DateTime? DateExclude { get; set; }
      
      /// <summary>
      /// Обучение пройдено успешно
      /// </summary>
      [Display(Description = "Обучение пройдено успешно")]
      [Required(ErrorMessage = "\"Обучение пройдено успешно\" должно быть заполнено")]
      [DataMember(Name = "isSuccess", EmitDefaultValue = false)]
      public virtual bool IsSuccess { get; set; }
      
      
      /// <summary>
      /// Ответы на вопросы
      /// </summary>
      [InverseProperty("TrainingCounselor")]
      [DataMember(Name = "answers", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestAnswer> Answers { get; set; }
      
      /// <summary>
      /// Тесты
      /// </summary>
      [InverseProperty("TrainingCounselorsResult")]
      [DataMember(Name = "tests", EmitDefaultValue = false)]
      public virtual ICollection<TrainingCounselorsTest> Tests { get; set; }
      
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
      [InverseProperty("Results")]
      [Display(Description = "Вожатый")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }
      
      /// <summary>
      /// Результаты
      /// </summary>
      [ForeignKey("TrainingCounselors")]
      [DataMember(Name = "trainingCounselorsId")]
      [Display(Description = "Результаты")]
      public virtual long? TrainingCounselorsId { get; set; }
      
      
      /// <summary>
      /// Результаты
      /// </summary>
      [InverseProperty("Results")]
      [Display(Description = "Результаты")]
      [DataMember(Name = "trainingCounselors", EmitDefaultValue = false)]
      public virtual TrainingCounselors TrainingCounselors { get; set; }
      
      /// <summary>
      /// Статус обучения
      /// </summary>
      [ForeignKey("Status")]
      [DataMember(Name = "statusId")]
      [Display(Description = "Статус обучения")]
      public virtual long? StatusId { get; set; }
      
      
      /// <summary>
      /// Статус обучения
      /// </summary>
      [Display(Description = "Статус обучения")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual TrainingCounselorsResultStatus Status { get; set; }
      
      /// <summary>
      /// Обучение администоров
      /// </summary>
      [ForeignKey("AdministratorTour")]
      [DataMember(Name = "administratorTourId")]
      [Display(Description = "Обучение администоров")]
      public virtual long? AdministratorTourId { get; set; }
      
      
      /// <summary>
      /// Обучение администоров
      /// </summary>
      [InverseProperty("Results")]
      [Display(Description = "Обучение администоров")]
      [DataMember(Name = "administratorTour", EmitDefaultValue = false)]
      public virtual AdministratorTour AdministratorTour { get; set; }

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