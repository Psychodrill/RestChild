// File:    AdministratorTour.cs
// Purpose: Definition of Class AdministratorTour

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Администраторы смены
   /// </summary>
   [Serializable]
   [DataContract(Name = "administratorTour")]
   public partial class AdministratorTour : IEntityBase
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
      /// Фамилия
      /// </summary>
      [Display(Description = "Фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
      [DataMember(Name = "lastName", EmitDefaultValue = false)]
      public virtual string LastName { get; set; }
      
      /// <summary>
      /// Имя
      /// </summary>
      [Display(Description = "Имя")]
      [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "firstName", EmitDefaultValue = false)]
      public virtual string FirstName { get; set; }
      
      /// <summary>
      /// Отчество
      /// </summary>
      [Display(Description = "Отчество")]
      [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
      [DataMember(Name = "middleName", EmitDefaultValue = false)]
      public virtual string MiddleName { get; set; }
      
      /// <summary>
      /// Признак что есть отчество
      /// </summary>
      [Display(Description = "Признак что есть отчество")]
      [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
      [DataMember(Name = "haveMiddleName", EmitDefaultValue = false)]
      public virtual bool HaveMiddleName { get; set; }
      
      /// <summary>
      /// Серия документа
      /// </summary>
      [Display(Description = "Серия документа")]
      [MaxLength(1000, ErrorMessage = "\"Серия документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSeria", EmitDefaultValue = false)]
      public virtual string DocumentSeria { get; set; }
      
      /// <summary>
      /// Номер документа
      /// </summary>
      [Display(Description = "Номер документа")]
      [MaxLength(1000, ErrorMessage = "\"Номер документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentNumber", EmitDefaultValue = false)]
      public virtual string DocumentNumber { get; set; }
      
      /// <summary>
      /// Дата выдачи документа
      /// </summary>
      [Display(Description = "Дата выдачи документа")]
      [DataMember(Name = "documentDateOfIssue", EmitDefaultValue = false)]
      public virtual DateTime? DocumentDateOfIssue { get; set; }
      
      /// <summary>
      /// Кем выдан документ
      /// </summary>
      [Display(Description = "Кем выдан документ")]
      [MaxLength(1000, ErrorMessage = "\"Кем выдан документ\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSubjectIssue", EmitDefaultValue = false)]
      public virtual string DocumentSubjectIssue { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      /// <summary>
      /// Мужской пол
      /// </summary>
      [Display(Description = "Мужской пол")]
      [Required(ErrorMessage = "\"Мужской пол\" должно быть заполнено")]
      [DataMember(Name = "male", EmitDefaultValue = false)]
      public virtual bool Male { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Дата обновления
      /// </summary>
      [Display(Description = "Дата обновления")]
      [Required(ErrorMessage = "\"Дата обновления\" должно быть заполнено")]
      [DataMember(Name = "dateUpdate", EmitDefaultValue = false)]
      public virtual DateTime DateUpdate { get; set; }
      
      /// <summary>
      /// Место рождения
      /// </summary>
      [Display(Description = "Место рождения")]
      [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
      [DataMember(Name = "placeOfBirth", EmitDefaultValue = false)]
      public virtual string PlaceOfBirth { get; set; }
      
      /// <summary>
      /// Контактный телефон
      /// </summary>
      [Display(Description = "Контактный телефон")]
      [MaxLength(1000, ErrorMessage = "\"Контактный телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Уникальный идентификатор сайта
      /// </summary>
      [Display(Description = "Уникальный идентификатор сайта")]
      [MaxLength(1000, ErrorMessage = "\"Уникальный идентификатор сайта\" не может быть больше 1000 символов")]
      [DataMember(Name = "externalUid", EmitDefaultValue = false)]
      public virtual string ExternalUid { get; set; }
      
      /// <summary>
      /// Пароль
      /// </summary>
      [Display(Description = "Пароль")]
      [MaxLength(1000, ErrorMessage = "\"Пароль\" не может быть больше 1000 символов")]
      [DataMember(Name = "password", EmitDefaultValue = false)]
      public virtual string Password { get; set; }
      
      /// <summary>
      /// Соль
      /// </summary>
      [Display(Description = "Соль")]
      [MaxLength(1000, ErrorMessage = "\"Соль\" не может быть больше 1000 символов")]
      [DataMember(Name = "salt", EmitDefaultValue = false)]
      public virtual string Salt { get; set; }
      
      /// <summary>
      /// Причина вноса в стоп лист текстом
      /// </summary>
      [Display(Description = "Причина вноса в стоп лист текстом")]
      [DataMember(Name = "stopListReasonText", EmitDefaultValue = false)]
      public virtual String StopListReasonText { get; set; }
      
      
      /// <summary>
      /// Администраторы смены
      /// </summary>
      [InverseProperty("AdministratorTours")]
      [DataMember(Name = "bouts", EmitDefaultValue = false)]
      public virtual ICollection<Bout> Bouts { get; set; }
      
      /// <summary>
      /// Обучение администоров
      /// </summary>
      [InverseProperty("AdministratorTour")]
      [DataMember(Name = "results", EmitDefaultValue = false)]
      public virtual ICollection<TrainingCounselorsResult> Results { get; set; }
      
      /// <summary>
      /// Документ удостовряющий личность
      /// </summary>
      [ForeignKey("DocumentType")]
      [DataMember(Name = "documentTypeId")]
      [Display(Description = "Документ удостовряющий личность")]
      public virtual long? DocumentTypeId { get; set; }
      
      
      /// <summary>
      /// Документ удостовряющий личность
      /// </summary>
      [Display(Description = "Документ удостовряющий личность")]
      [DataMember(Name = "documentType", EmitDefaultValue = false)]
      public virtual DocumentType DocumentType { get; set; }
      
      /// <summary>
      /// Связанный пользователь
      /// </summary>
      [ForeignKey("LinkedAccount")]
      [DataMember(Name = "linkedAccountId")]
      [Display(Description = "Связанный пользователь")]
      public virtual long? LinkedAccountId { get; set; }
      
      
      /// <summary>
      /// Связанный пользователь
      /// </summary>
      [Display(Description = "Связанный пользователь")]
      [DataMember(Name = "linkedAccount", EmitDefaultValue = false)]
      public virtual Account LinkedAccount { get; set; }
      
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
      /// Заграничный паспорт
      /// </summary>
      [InverseProperty("AdministratorTour")]
      [DataMember(Name = "foreginPassports", EmitDefaultValue = false)]
      public virtual ICollection<ForeginPassport> ForeginPassports { get; set; }
      
      /// <summary>
      /// Причина вноса в стоп лист
      /// </summary>
      [ForeignKey("StopListReason")]
      [DataMember(Name = "stopListReasonId")]
      [Display(Description = "Причина вноса в стоп лист")]
      public virtual long? StopListReasonId { get; set; }
      
      
      /// <summary>
      /// Причина вноса в стоп лист
      /// </summary>
      [Display(Description = "Причина вноса в стоп лист")]
      [DataMember(Name = "stopListReason", EmitDefaultValue = false)]
      public virtual CounselorsStopListReason StopListReason { get; set; }

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