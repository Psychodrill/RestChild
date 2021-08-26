// File:    CounselorTestSubject.cs
// Purpose: Definition of Class CounselorTestSubject

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тематика теста
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTestSubject")]
   public partial class CounselorTestSubject : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Количество вопросов по теме в тесте
      /// </summary>
      [Display(Description = "Количество вопросов по теме в тесте")]
      [DataMember(Name = "questionCount", EmitDefaultValue = false)]
      public virtual int? QuestionCount { get; set; }
      
      /// <summary>
      /// Признак что удалено
      /// </summary>
      [Display(Description = "Признак что удалено")]
      [Required(ErrorMessage = "\"Признак что удалено\" должно быть заполнено")]
      [DataMember(Name = "isArchive", EmitDefaultValue = false)]
      public virtual bool IsArchive { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("CounselorTest")]
      [DataMember(Name = "counselorTestId")]
      [Display(Description = "")]
      public virtual long? CounselorTestId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("CounselorTestSubjects")]
      [Display(Description = "")]
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