// File:    Agent.cs
// Purpose: Definition of Class Agent

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Представитель
   /// </summary>
   [Serializable]
   [DataContract(Name = "agent")]
   public partial class Agent : IEntityBase
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
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
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
      /// Дата выдачи доверенности
      /// </summary>
      [Display(Description = "Дата выдачи доверенности")]
      [DataMember(Name = "proxyDateOfIssure", EmitDefaultValue = false)]
      public virtual DateTime? ProxyDateOfIssure { get; set; }
      
      /// <summary>
      /// Имя натариуса
      /// </summary>
      [Display(Description = "Имя натариуса")]
      [MaxLength(1000, ErrorMessage = "\"Имя натариуса\" не может быть больше 1000 символов")]
      [DataMember(Name = "notaryName", EmitDefaultValue = false)]
      public virtual string NotaryName { get; set; }
      
      /// <summary>
      /// Дата окончания действия доверенности
      /// </summary>
      [Display(Description = "Дата окончания действия доверенности")]
      [DataMember(Name = "proxyEndDate", EmitDefaultValue = false)]
      public virtual DateTime? ProxyEndDate { get; set; }
      
      /// <summary>
      /// Номер доверенности
      /// </summary>
      [Display(Description = "Номер доверенности")]
      [MaxLength(1000, ErrorMessage = "\"Номер доверенности\" не может быть больше 1000 символов")]
      [DataMember(Name = "proxyNumber", EmitDefaultValue = false)]
      public virtual string ProxyNumber { get; set; }
      
      /// <summary>
      /// Снилс
      /// </summary>
      [Display(Description = "Снилс")]
      [MaxLength(1000, ErrorMessage = "\"Снилс\" не может быть больше 1000 символов")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
      /// <summary>
      /// Признак последней версии
      /// </summary>
      [Display(Description = "Признак последней версии")]
      [Required(ErrorMessage = "\"Признак последней версии\" должно быть заполнено")]
      [DataMember(Name = "isLast", EmitDefaultValue = false)]
      public virtual bool IsLast { get; set; }
      
      /// <summary>
      /// Признак что есть отчество
      /// </summary>
      [Display(Description = "Признак что есть отчество")]
      [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
      [DataMember(Name = "haveMiddleName", EmitDefaultValue = false)]
      public virtual bool HaveMiddleName { get; set; }
      
      /// <summary>
      /// Мужской пол
      /// </summary>
      [Display(Description = "Мужской пол")]
      [DataMember(Name = "male", EmitDefaultValue = false)]
      public virtual bool? Male { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      /// <summary>
      /// Документ код подразделения
      /// </summary>
      [Display(Description = "Документ код подразделения")]
      [MaxLength(1000, ErrorMessage = "\"Документ код подразделения\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentCode", EmitDefaultValue = false)]
      public virtual string DocumentCode { get; set; }
      
      /// <summary>
      /// Место рождения
      /// </summary>
      [Display(Description = "Место рождения")]
      [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
      [DataMember(Name = "placeOfBirth", EmitDefaultValue = false)]
      public virtual string PlaceOfBirth { get; set; }
      
      
      /// <summary>
      /// Вид документа представителя
      /// </summary>
      [ForeignKey("DocumentType")]
      [DataMember(Name = "documentTypeId")]
      [Display(Description = "Вид документа представителя")]
      public virtual long? DocumentTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документа представителя
      /// </summary>
      [Display(Description = "Вид документа представителя")]
      [DataMember(Name = "documentType", EmitDefaultValue = false)]
      public virtual DocumentType DocumentType { get; set; }
      
      /// <summary>
      /// Статус по отношению к ребенку
      /// </summary>
      [ForeignKey("StatusByChild")]
      [DataMember(Name = "statusByChildId")]
      [Display(Description = "Статус по отношению к ребенку")]
      public virtual long? StatusByChildId { get; set; }
      
      
      /// <summary>
      /// Статус по отношению к ребенку
      /// </summary>
      [Display(Description = "Статус по отношению к ребенку")]
      [DataMember(Name = "statusByChild", EmitDefaultValue = false)]
      public virtual StatusByChild StatusByChild { get; set; }

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