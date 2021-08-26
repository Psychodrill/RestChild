// File:    Person.cs
// Purpose: Definition of Class Person

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Персона
   /// </summary>
   [Serializable]
   [DataContract(Name = "person")]
   public partial class Person : IEntityBase
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
      /// Место рождения
      /// </summary>
      [Display(Description = "Место рождения")]
      [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
      [DataMember(Name = "placeOfBirth", EmitDefaultValue = false)]
      public virtual string PlaceOfBirth { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
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
      /// Идентификатор контингента
      /// </summary>
      [Display(Description = "Идентификатор контингента")]
      [MaxLength(1000, ErrorMessage = "\"Идентификатор контингента\" не может быть больше 1000 символов")]
      [DataMember(Name = "contingentUid", EmitDefaultValue = false)]
      public virtual string ContingentUid { get; set; }
      
      /// <summary>
      /// Признак что ребёнок
      /// </summary>
      [Display(Description = "Признак что ребёнок")]
      [Required(ErrorMessage = "\"Признак что ребёнок\" должно быть заполнено")]
      [DataMember(Name = "isChild", EmitDefaultValue = false)]
      public virtual bool IsChild { get; set; }
      
      /// <summary>
      /// Ключевое поле
      /// </summary>
      [Display(Description = "Ключевое поле")]
      [MaxLength(1000, ErrorMessage = "\"Ключевое поле\" не может быть больше 1000 символов")]
      [DataMember(Name = "keyField", EmitDefaultValue = false)]
      public virtual string KeyField { get; set; }
      
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [InverseProperty("Child")]
      [DataMember(Name = "tradeUnionCamper", EmitDefaultValue = false)]
      public virtual ICollection<TradeUnionCamper> TradeUnionCamper { get; set; }
      
      /// <summary>
      /// Вид документа
      /// </summary>
      [ForeignKey("DocumentType")]
      [DataMember(Name = "documentTypeId")]
      [Display(Description = "Вид документа")]
      public virtual long? DocumentTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документа
      /// </summary>
      [Display(Description = "Вид документа")]
      [DataMember(Name = "documentType", EmitDefaultValue = false)]
      public virtual DocumentType DocumentType { get; set; }
      
      /// <summary>
      /// Адрес регистрации
      /// </summary>
      [ForeignKey("Address")]
      [DataMember(Name = "addressId")]
      [Display(Description = "Адрес регистрации")]
      public virtual long? AddressId { get; set; }
      
      
      /// <summary>
      /// Адрес регистрации
      /// </summary>
      [Display(Description = "Адрес регистрации")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual Address Address { get; set; }
      
      /// <summary>
      /// Связь проверки с персоной
      /// </summary>
      [InverseProperty("Person")]
      [DataMember(Name = "personCheck", EmitDefaultValue = false)]
      public virtual ICollection<TradeUnionPersonCheck> PersonCheck { get; set; }
      
      /// <summary>
      /// Результат проверки персоны
      /// </summary>
      [InverseProperty("PersonCheckResults")]
      [DataMember(Name = "personCheckResults", EmitDefaultValue = false)]
      public virtual ICollection<TradeUnionPersonCheck> PersonCheckResults { get; set; }

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