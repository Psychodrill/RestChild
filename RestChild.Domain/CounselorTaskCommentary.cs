// File:    CounselorTaskCommentary.cs
// Purpose: Definition of Class CounselorTaskCommentary

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Комменатрии к поручениям
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorTaskCommentary")]
   public partial class CounselorTaskCommentary : IEntityBase
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
      /// Дата комментария
      /// </summary>
      [Display(Description = "Дата комментария")]
      [Required(ErrorMessage = "\"Дата комментария\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Комментарий пользователя
      /// </summary>
      [Display(Description = "Комментарий пользователя")]
      [DataMember(Name = "commentary", EmitDefaultValue = false)]
      public virtual String Commentary { get; set; }
      
      /// <summary>
      /// Автор комментария
      /// </summary>
      [Display(Description = "Автор комментария")]
      [MaxLength(1000, ErrorMessage = "\"Автор комментария\" не может быть больше 1000 символов")]
      [DataMember(Name = "author", EmitDefaultValue = false)]
      public virtual string Author { get; set; }
      
      
      /// <summary>
      /// Поручение
      /// </summary>
      [ForeignKey("CounselorTask")]
      [DataMember(Name = "counselorTaskId")]
      [Display(Description = "Поручение")]
      public virtual long? CounselorTaskId { get; set; }
      
      
      /// <summary>
      /// Поручение
      /// </summary>
      [InverseProperty("Comments")]
      [Display(Description = "Поручение")]
      [DataMember(Name = "counselorTask", EmitDefaultValue = false)]
      public virtual CounselorTask CounselorTask { get; set; }
      
      /// <summary>
      /// Ответсвенный за поручение
      /// </summary>
      [ForeignKey("ResponsibilityForTask")]
      [DataMember(Name = "responsibilityForTaskId")]
      [Display(Description = "Ответсвенный за поручение")]
      public virtual long? ResponsibilityForTaskId { get; set; }
      
      
      /// <summary>
      /// Ответсвенный за поручение
      /// </summary>
      [Display(Description = "Ответсвенный за поручение")]
      [DataMember(Name = "responsibilityForTask", EmitDefaultValue = false)]
      public virtual ResponsibilityForTask ResponsibilityForTask { get; set; }

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