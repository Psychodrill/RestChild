// File:    CounselorCource.cs
// Purpose: Definition of Class CounselorCource

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вожатский курс
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselorCource")]
   public partial class CounselorCource : IEntityBase
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
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [MaxLength(1000, ErrorMessage = "\"Название\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Год
      /// </summary>
      [Display(Description = "Год")]
      [Required(ErrorMessage = "\"Год\" должно быть заполнено")]
      [DataMember(Name = "year", EmitDefaultValue = false)]
      public virtual int Year { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")][Display(Description = "")]
      public virtual long? CounselorsId { get; set; }
      /// <summary>
      /// </summary>
      [InverseProperty("CounselorCources")]
      [Display(Description = "")]
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