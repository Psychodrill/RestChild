// File:    MGTVisitBookingPerson.cs
// Purpose: Definition of Class MGTVisitBookingPerson

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Визит в МГТ - Посетитель
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTVisitBookingPerson")]
   public partial class MGTVisitBookingPerson : IEntityBase
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
      /// Тип
      /// </summary>
      [Display(Description = "Тип")]
      [MaxLength(1000, ErrorMessage = "\"Тип\" не может быть больше 1000 символов")]
      [DataMember(Name = "typePerson", EmitDefaultValue = false)]
      public virtual string TypePerson { get; set; }
      
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
      /// Пол
      /// </summary>
      [Display(Description = "Пол")]
      [Required(ErrorMessage = "\"Пол\" должно быть заполнено")]
      [DataMember(Name = "male", EmitDefaultValue = false)]
      public virtual bool Male { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Снилс
      /// </summary>
      [Display(Description = "Снилс")]
      [MaxLength(1000, ErrorMessage = "\"Снилс\" не может быть больше 1000 символов")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
      /// <summary>
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Льгота
      /// </summary>
      [Display(Description = "Льгота")]
      [MaxLength(1000, ErrorMessage = "\"Льгота\" не может быть больше 1000 символов")]
      [DataMember(Name = "benefit", EmitDefaultValue = false)]
      public virtual string Benefit { get; set; }
      
      
      /// <summary>
      /// Связь записи на приём с посетителями
      /// </summary>
      [ForeignKey("VisitBooking")]
      [DataMember(Name = "visitBookingId")][Display(Description = "Связь записи на приём с посетителями")]
      public virtual long? VisitBookingId { get; set; }
      /// <summary>
      /// Связь записи на приём с посетителями
      /// </summary>
      [InverseProperty("Persons")]
      [Display(Description = "Связь записи на приём с посетителями")]
      [DataMember(Name = "visitBooking", EmitDefaultValue = false)]
      public virtual MGTBookingVisit VisitBooking { get; set; }
      
      /// <summary>
      /// Вид персоны
      /// </summary>
      [ForeignKey("PersonType")]
      [DataMember(Name = "personTypeId")]
      [Display(Description = "Вид персоны")]
      public virtual long? PersonTypeId { get; set; }
      
      
      /// <summary>
      /// Вид персоны
      /// </summary>
      [InverseProperty("Persons")]
      [Display(Description = "Вид персоны")]
      [DataMember(Name = "personType", EmitDefaultValue = false)]
      public virtual MGTVisitBookingPersonType PersonType { get; set; }

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