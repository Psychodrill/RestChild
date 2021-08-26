// File:    ForeginPassport.cs
// Purpose: Definition of Class ForeginPassport

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заграничный паспорт
   /// </summary>
   [Serializable]
   [DataContract(Name = "foreginPassport")]
   public partial class ForeginPassport : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификтор
      /// </summary>
      [Display(Description = "Уникальный идентификтор")]
      [Required(ErrorMessage = "\"Уникальный идентификтор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "passportNumber", EmitDefaultValue = false)]
      public virtual string PassportNumber { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "passportIssueDate", EmitDefaultValue = false)]
      public virtual DateTime? PassportIssueDate { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "passportValidityEndDate", EmitDefaultValue = false)]
      public virtual DateTime? PassportValidityEndDate { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "passportIssue", EmitDefaultValue = false)]
      public virtual string PassportIssue { get; set; }
      
      /// <summary>
      /// Загран паспорт имя
      /// </summary>
      [Display(Description = "Загран паспорт имя")]
      [MaxLength(1000, ErrorMessage = "\"Загран паспорт имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "foreginName", EmitDefaultValue = false)]
      public virtual string ForeginName { get; set; }
      
      /// <summary>
      /// Загран паспрт фамилия
      /// </summary>
      [Display(Description = "Загран паспрт фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Загран паспрт фамилия\" не может быть больше 1000 символов")]
      [DataMember(Name = "foreginLastName", EmitDefaultValue = false)]
      public virtual string ForeginLastName { get; set; }
      
      /// <summary>
      /// Имя
      /// </summary>
      [Display(Description = "Имя")]
      [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "firstName", EmitDefaultValue = false)]
      public virtual string FirstName { get; set; }
      
      /// <summary>
      /// Фамилия
      /// </summary>
      [Display(Description = "Фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
      [DataMember(Name = "lastName", EmitDefaultValue = false)]
      public virtual string LastName { get; set; }
      
      
      /// <summary>
      /// Заграничный паспорт
      /// </summary>
      [ForeignKey("AdministratorTour")]
      [DataMember(Name = "administratorTourId")][Display(Description = "Заграничный паспорт")]
      public virtual long? AdministratorTourId { get; set; }
      /// <summary>
      /// Заграничный паспорт
      /// </summary>
      [InverseProperty("ForeginPassports")]
      [Display(Description = "Заграничный паспорт")]
      [DataMember(Name = "administratorTour", EmitDefaultValue = false)]
      public virtual AdministratorTour AdministratorTour { get; set; }
      
      /// <summary>
      /// Загран паспорта
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "Загран паспорта")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// Загран паспорта
      /// </summary>
      [InverseProperty("ForeginPassports")]
      [Display(Description = "Загран паспорта")]
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