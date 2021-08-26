// File:    CounselorTask.cs
// Purpose: Definition of Class CounselorTask

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Поручения вожатым
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTask")]
   public partial class CounselorTask : IEntityBase
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
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime? DateCreate { get; set; }
      
      /// <summary>
      /// Срок исполнения
      /// </summary>
      [Display(Description = "Срок исполнения")]
      [DataMember(Name = "datePlanFinish", EmitDefaultValue = false)]
      public virtual DateTime? DatePlanFinish { get; set; }
      
      /// <summary>
      /// Фактический срок исполнения
      /// </summary>
      [Display(Description = "Фактический срок исполнения")]
      [DataMember(Name = "dateFactFinish", EmitDefaultValue = false)]
      public virtual DateTime? DateFactFinish { get; set; }
      
      /// <summary>
      /// Тема
      /// </summary>
      [Display(Description = "Тема")]
      [DataMember(Name = "subject", EmitDefaultValue = false)]
      public virtual String Subject { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "body", EmitDefaultValue = false)]
      public virtual String Body { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "report", EmitDefaultValue = false)]
      public virtual String Report { get; set; }
      
      /// <summary>
      /// Базовое поручение
      /// </summary>
      [Display(Description = "Базовое поручение")]
      [Required(ErrorMessage = "\"Базовое поручение\" должно быть заполнено")]
      [DataMember(Name = "baseTask", EmitDefaultValue = false)]
      public virtual bool BaseTask { get; set; }
      
      /// <summary>
      /// Не обязательно для исполнения
      /// </summary>
      [Display(Description = "Не обязательно для исполнения")]
      [Required(ErrorMessage = "\"Не обязательно для исполнения\" должно быть заполнено")]
      [DataMember(Name = "notNecessary", EmitDefaultValue = false)]
      public virtual bool NotNecessary { get; set; }
      
      /// <summary>
      /// Дата изменения
      /// </summary>
      [Display(Description = "Дата изменения")]
      [DataMember(Name = "dateUpdate", EmitDefaultValue = false)]
      public virtual DateTime? DateUpdate { get; set; }
      
      
      /// <summary>
      /// Файлы отчета по поруению
      /// </summary>
      [InverseProperty("CounselorTask")]
      [DataMember(Name = "reportFiles", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTaskReportFile> ReportFiles { get; set; }
      
      /// <summary>
      /// Файлы поручений
      /// </summary>
      [InverseProperty("CounselorTask")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTaskFile> Files { get; set; }
      
      /// <summary>
      /// Поручения соисполнителям
      /// </summary>
      [InverseProperty("Parent")]
      [DataMember(Name = "cotasks", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTask> Cotasks { get; set; }
      
      /// <summary>
      /// Поручение
      /// </summary>
      [InverseProperty("CounselorTask")]
      [DataMember(Name = "comments", EmitDefaultValue = false)]
      public virtual ICollection<CounselorTaskCommentary> Comments { get; set; }
      
      /// <summary>
      /// Поручения соисполнителям
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Поручения соисполнителям")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Поручения соисполнителям
      /// </summary>
      [InverseProperty("Cotasks")]
      [Display(Description = "Поручения соисполнителям")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual CounselorTask Parent { get; set; }
      
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
      /// Автор поручения
      /// </summary>
      [ForeignKey("Author")]
      [DataMember(Name = "authorId")]
      [Display(Description = "Автор поручения")]
      public virtual long? AuthorId { get; set; }
      
      
      /// <summary>
      /// Автор поручения
      /// </summary>
      [Display(Description = "Автор поручения")]
      [DataMember(Name = "author", EmitDefaultValue = false)]
      public virtual ResponsibilityForTask Author { get; set; }
      
      /// <summary>
      /// Исполнитель
      /// </summary>
      [ForeignKey("Executor")]
      [DataMember(Name = "executorId")]
      [Display(Description = "Исполнитель")]
      public virtual long? ExecutorId { get; set; }
      
      
      /// <summary>
      /// Исполнитель
      /// </summary>
      [Display(Description = "Исполнитель")]
      [DataMember(Name = "executor", EmitDefaultValue = false)]
      public virtual ResponsibilityForTask Executor { get; set; }
      
      /// <summary>
      /// Заезд
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Заезд")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Заезд
      /// </summary>
      [Display(Description = "Заезд")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }

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