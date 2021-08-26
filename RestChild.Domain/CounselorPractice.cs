// File:    CounselorPractice.cs
// Purpose: Definition of Class CounselorPractice

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Опыт работы вожатого
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorPractice")]
   public partial class CounselorPractice : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентикатор
      /// </summary>
      [Display(Description = "Уникальный идентикатор")]
      [Required(ErrorMessage = "\"Уникальный идентикатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Название лагеря
      /// </summary>
      [Display(Description = "Название лагеря")]
      [MaxLength(1000, ErrorMessage = "\"Название лагеря\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название лагеря\" не может быть пустым")]
      [DataMember(Name = "camp", EmitDefaultValue = false)]
      public virtual string Camp { get; set; }
      
      /// <summary>
      /// Год работы
      /// </summary>
      [Display(Description = "Год работы")]
      [MaxLength(1000, ErrorMessage = "\"Год работы\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Год работы\" не может быть пустым")]
      [DataMember(Name = "year", EmitDefaultValue = false)]
      public virtual string Year { get; set; }
      
      /// <summary>
      /// Смена
      /// </summary>
      [Display(Description = "Смена")]
      [MaxLength(1000, ErrorMessage = "\"Смена\" не может быть больше 1000 символов")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual string Tour { get; set; }
      
      /// <summary>
      /// Отряд
      /// </summary>
      [Display(Description = "Отряд")]
      [MaxLength(1000, ErrorMessage = "\"Отряд\" не может быть больше 1000 символов")]
      [DataMember(Name = "party", EmitDefaultValue = false)]
      public virtual string Party { get; set; }
      
      
      /// <summary>
      /// Опыт работы вожатого
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")][Display(Description = "Опыт работы вожатого")]
      public virtual long? CounselorsId { get; set; }
      /// <summary>
      /// Опыт работы вожатого
      /// </summary>
      [InverseProperty("CounselorPractices")]
      [Display(Description = "Опыт работы вожатого")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }

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