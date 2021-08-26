// File:    TrainingCounselorsGroupTest.cs
// Purpose: Definition of Class TrainingCounselorsGroupTest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тестирование группы
   /// </summary>
   [Serializable]
   [DataContract(Name = "trainingCounselorsGroupTest")]
   public partial class TrainingCounselorsGroupTest : IEntityBase
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
      /// Дата начала тестирования
      /// </summary>
      [Display(Description = "Дата начала тестирования")]
      [Required(ErrorMessage = "\"Дата начала тестирования\" должно быть заполнено")]
      [DataMember(Name = "dateStart", EmitDefaultValue = false)]
      public virtual DateTime DateStart { get; set; }
      
      /// <summary>
      /// Крайняя дата тестирования
      /// </summary>
      [Display(Description = "Крайняя дата тестирования")]
      [Required(ErrorMessage = "\"Крайняя дата тестирования\" должно быть заполнено")]
      [DataMember(Name = "dateEnd", EmitDefaultValue = false)]
      public virtual DateTime DateEnd { get; set; }
      
      /// <summary>
      /// Удален
      /// </summary>
      [Display(Description = "Удален")]
      [Required(ErrorMessage = "\"Удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Количество прохождений тестов
      /// </summary>
      [Display(Description = "Количество прохождений тестов")]
      [DataMember(Name = "countAttempts", EmitDefaultValue = false)]
      public virtual int? CountAttempts { get; set; }
      
      /// <summary>
      /// Ограничение на комчество попыток
      /// </summary>
      [Display(Description = "Ограничение на комчество попыток")]
      [Required(ErrorMessage = "\"Ограничение на комчество попыток\" должно быть заполнено")]
      [DataMember(Name = "isCountLimited", EmitDefaultValue = false)]
      public virtual bool IsCountLimited { get; set; }
      
      
      /// <summary>
      /// Связь с обучаемыми для тестирования
      /// </summary>
      [InverseProperty("GroupTest")]
      [DataMember(Name = "students", EmitDefaultValue = false)]
      public virtual ICollection<TrainingCounselorsTest> Students { get; set; }
      
      /// <summary>
      /// Тесты группы
      /// </summary>
      [ForeignKey("TrainingCounselors")]
      [DataMember(Name = "trainingCounselorsId")]
      [Display(Description = "Тесты группы")]
      public virtual long? TrainingCounselorsId { get; set; }
      
      
      /// <summary>
      /// Тесты группы
      /// </summary>
      [InverseProperty("Tests")]
      [Display(Description = "Тесты группы")]
      [DataMember(Name = "trainingCounselors", EmitDefaultValue = false)]
      public virtual TrainingCounselors TrainingCounselors { get; set; }
      
      /// <summary>
      /// Тест
      /// </summary>
      [ForeignKey("CounselorTest")]
      [DataMember(Name = "counselorTestId")]
      [Display(Description = "Тест")]
      public virtual long? CounselorTestId { get; set; }
      
      
      /// <summary>
      /// Тест
      /// </summary>
      [Display(Description = "Тест")]
      [DataMember(Name = "counselorTest", EmitDefaultValue = false)]
      public virtual CounselorTest CounselorTest { get; set; }

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