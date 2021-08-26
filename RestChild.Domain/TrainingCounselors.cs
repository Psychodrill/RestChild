// File:    TrainingCounselors.cs
// Purpose: Definition of Class TrainingCounselors

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Группы обучения вожатых
   /// </summary>
   [Serializable]
   [DataContract(Name = "trainingCounselors")]
   public partial class TrainingCounselors : IEntityBase
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
      /// Наименование программы обучения
      /// </summary>
      [Display(Description = "Наименование программы обучения")]
      [MaxLength(1000, ErrorMessage = "\"Наименование программы обучения\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование программы обучения\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание программы обучения
      /// </summary>
      [Display(Description = "Описание программы обучения")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Плановое количество мест
      /// </summary>
      [Display(Description = "Плановое количество мест")]
      [DataMember(Name = "value", EmitDefaultValue = false)]
      public virtual int? Value { get; set; }
      
      /// <summary>
      /// Расписание обучения
      /// </summary>
      [Display(Description = "Расписание обучения")]
      [DataMember(Name = "timetable", EmitDefaultValue = false)]
      public virtual String Timetable { get; set; }
      
      /// <summary>
      /// Дата начала обучения
      /// </summary>
      [Display(Description = "Дата начала обучения")]
      [DataMember(Name = "startTraining", EmitDefaultValue = false)]
      public virtual DateTime? StartTraining { get; set; }
      
      /// <summary>
      /// Дата окончания обучения
      /// </summary>
      [Display(Description = "Дата окончания обучения")]
      [Required(ErrorMessage = "\"Дата окончания обучения\" должно быть заполнено")]
      [DataMember(Name = "endTraining", EmitDefaultValue = false)]
      public virtual DateTime EndTraining { get; set; }
      
      /// <summary>
      /// Количество часов
      /// </summary>
      [Display(Description = "Количество часов")]
      [Required(ErrorMessage = "\"Количество часов\" должно быть заполнено")]
      [DataMember(Name = "duration", EmitDefaultValue = false)]
      public virtual int Duration { get; set; }
      
      /// <summary>
      /// Для сайта
      /// </summary>
      [Display(Description = "Для сайта")]
      [Required(ErrorMessage = "\"Для сайта\" должно быть заполнено")]
      [DataMember(Name = "forSite", EmitDefaultValue = false)]
      public virtual bool ForSite { get; set; }
      
      
      /// <summary>
      /// Результаты
      /// </summary>
      [InverseProperty("TrainingCounselors")]
      [DataMember(Name = "results", EmitDefaultValue = false)]
      public virtual ICollection<TrainingCounselorsResult> Results { get; set; }
      
      /// <summary>
      /// Тесты группы
      /// </summary>
      [InverseProperty("TrainingCounselors")]
      [DataMember(Name = "tests", EmitDefaultValue = false)]
      public virtual ICollection<TrainingCounselorsGroupTest> Tests { get; set; }
      
      /// <summary>
      /// Документы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")]
      [Display(Description = "Документы")]
      public virtual long? LinkToFileId { get; set; }
      
      
      /// <summary>
      /// Документы
      /// </summary>
      [Display(Description = "Документы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Место проведения обучения
      /// </summary>
      [ForeignKey("TrainingCounselorsPlace")]
      [DataMember(Name = "trainingCounselorsPlaceId")]
      [Display(Description = "Место проведения обучения")]
      public virtual long? TrainingCounselorsPlaceId { get; set; }
      
      
      /// <summary>
      /// Место проведения обучения
      /// </summary>
      [Display(Description = "Место проведения обучения")]
      [DataMember(Name = "trainingCounselorsPlace", EmitDefaultValue = false)]
      public virtual TrainingCounselorsPlace TrainingCounselorsPlace { get; set; }
      
      /// <summary>
      /// Вид обучения
      /// </summary>
      [ForeignKey("TrainingCounselorsType")]
      [DataMember(Name = "trainingCounselorsTypeId")]
      [Display(Description = "Вид обучения")]
      public virtual long? TrainingCounselorsTypeId { get; set; }
      
      
      /// <summary>
      /// Вид обучения
      /// </summary>
      [Display(Description = "Вид обучения")]
      [DataMember(Name = "trainingCounselorsType", EmitDefaultValue = false)]
      public virtual TrainingCounselorsType TrainingCounselorsType { get; set; }
      
      /// <summary>
      /// Время обучения
      /// </summary>
      [ForeignKey("TrainingCounselorsTime")]
      [DataMember(Name = "trainingCounselorsTimeId")]
      [Display(Description = "Время обучения")]
      public virtual long? TrainingCounselorsTimeId { get; set; }
      
      
      /// <summary>
      /// Время обучения
      /// </summary>
      [Display(Description = "Время обучения")]
      [DataMember(Name = "trainingCounselorsTime", EmitDefaultValue = false)]
      public virtual TrainingCounselorsTime TrainingCounselorsTime { get; set; }
      
      /// <summary>
      /// Статус группы
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус группы")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус группы
      /// </summary>
      [Display(Description = "Статус группы")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }

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