// File:    CounselorTest.cs
// Purpose: Definition of Class CounselorTest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тесты
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTest")]
   public partial class CounselorTest : IEntityBase
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
      /// Наименование теста
      /// </summary>
      [Display(Description = "Наименование теста")]
      [MaxLength(1000, ErrorMessage = "\"Наименование теста\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование теста\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание теста
      /// </summary>
      [Display(Description = "Описание теста")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Количество ошибок в тесте для прохождения
      /// </summary>
      [Display(Description = "Количество ошибок в тесте для прохождения")]
      [Required(ErrorMessage = "\"Количество ошибок в тесте для прохождения\" должно быть заполнено")]
      [DataMember(Name = "countErrorInTest", EmitDefaultValue = false)]
      public virtual int CountErrorInTest { get; set; }
      
      /// <summary>
      /// Финальный тест
      /// </summary>
      [Display(Description = "Финальный тест")]
      [Required(ErrorMessage = "\"Финальный тест\" должно быть заполнено")]
      [DataMember(Name = "isFinalTest", EmitDefaultValue = false)]
      public virtual bool IsFinalTest { get; set; }
      
      
      /// <summary>
      /// Вопросы
      /// </summary>
      [InverseProperty("CounselorTest")]
      [DataMember(Name = "questions", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestQuestion> Questions { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("CounselorTest")]
      [DataMember(Name = "counselorTestSubjects", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTestSubject> CounselorTestSubjects { get; set; }
      
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
      /// Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
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