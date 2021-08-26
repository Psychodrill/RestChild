// File:    TimeOfRest.cs
// Purpose: Definition of Class TimeOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Время отдыха
   /// </summary>
   [Serializable]
   [DataContract(Name = "timeOfRest")]
   public partial class TimeOfRest : IEntityBase
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
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Коэффициент учитывающий зависимость стоимости от времени отдыха
      /// </summary>
      [Display(Description = "Коэффициент учитывающий зависимость стоимости от времени отдыха")]
      [DataMember(Name = "factorDependence", EmitDefaultValue = false)]
      public virtual decimal? FactorDependence { get; set; }
      
      /// <summary>
      /// День месяца начала отдыха
      /// </summary>
      [Display(Description = "День месяца начала отдыха")]
      [Required(ErrorMessage = "\"День месяца начала отдыха\" должно быть заполнено")]
      [DataMember(Name = "dayOfMonth", EmitDefaultValue = false)]
      public virtual int DayOfMonth { get; set; }
      
      /// <summary>
      /// Месяц начала отдыха
      /// </summary>
      [Display(Description = "Месяц начала отдыха")]
      [Required(ErrorMessage = "\"Месяц начала отдыха\" должно быть заполнено")]
      [DataMember(Name = "month", EmitDefaultValue = false)]
      public virtual int Month { get; set; }
      
      /// <summary>
      /// Продолжительность периода дней
      /// </summary>
      [Display(Description = "Продолжительность периода дней")]
      [Required(ErrorMessage = "\"Продолжительность периода дней\" должно быть заполнено")]
      [DataMember(Name = "periodLength", EmitDefaultValue = false)]
      public virtual int PeriodLength { get; set; }
      
      /// <summary>
      /// Год начала отдыха
      /// </summary>
      [Display(Description = "Год начала отдыха")]
      [Required(ErrorMessage = "\"Год начала отдыха\" должно быть заполнено")]
      [DataMember(Name = "year", EmitDefaultValue = false)]
      public virtual int Year { get; set; }
      
      /// <summary>
      /// Активный элемент
      /// </summary>
      [Display(Description = "Активный элемент")]
      [Required(ErrorMessage = "\"Активный элемент\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      
      /// <summary>
      /// Связь с видом отдыха
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Связь с видом отдыха")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь с видом отдыха
      /// </summary>
      [Display(Description = "Связь с видом отдыха")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }
      
      /// <summary>
      /// Связь времени отдыха с заявлением
      /// </summary>
      [InverseProperty("TimeOfRest")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<RequestsTimeOfRest> Requests { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }
      
      /// <summary>
      /// Связь времени отдыха и годов
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Связь времени отдыха и годов")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь времени отдыха и годов
      /// </summary>
      [Display(Description = "Связь времени отдыха и годов")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Связь смены
      /// </summary>
      [ForeignKey("GroupedTimeOfRest")]
      [DataMember(Name = "groupedTimeOfRestId")]
      [Display(Description = "Связь смены")]
      public virtual long? GroupedTimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь смены
      /// </summary>
      [Display(Description = "Связь смены")]
      [DataMember(Name = "groupedTimeOfRest", EmitDefaultValue = false)]
      public virtual GroupedTimeOfRest GroupedTimeOfRest { get; set; }

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