// File:    InteragencyRequestResult.cs
// Purpose: Definition of Class InteragencyRequestResult

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Результат проверки ребенка
   /// </summary>
   [Serializable]
   [DataContract(Name = "interagencyRequestResult")]
   public partial class InteragencyRequestResult : IEntityBase
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
      /// Межведомственный запрос
      /// </summary>
      [ForeignKey("InteragencyRequest")]
      [DataMember(Name = "interagencyRequestId")]
      [Display(Description = "Межведомственный запрос")]
      public virtual long? InteragencyRequestId { get; set; }
      
      
      /// <summary>
      /// Межведомственный запрос
      /// </summary>
      [Display(Description = "Межведомственный запрос")]
      [DataMember(Name = "interagencyRequest", EmitDefaultValue = false)]
      public virtual InteragencyRequest InteragencyRequest { get; set; }
      
      /// <summary>
      /// Список детей
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Список детей")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Список детей
      /// </summary>
      [Display(Description = "Список детей")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Статус результата проверки в ОИВ
      /// </summary>
      [ForeignKey("StatusResult")]
      [DataMember(Name = "statusResultId")]
      [Display(Description = "Статус результата проверки в ОИВ")]
      public virtual long? StatusResultId { get; set; }
      
      
      /// <summary>
      /// Статус результата проверки в ОИВ
      /// </summary>
      [Display(Description = "Статус результата проверки в ОИВ")]
      [DataMember(Name = "statusResult", EmitDefaultValue = false)]
      public virtual StatusResult StatusResult { get; set; }
      
      /// <summary>
      /// Заявитель
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Заявитель")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Заявитель
      /// </summary>
      [Display(Description = "Заявитель")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }

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