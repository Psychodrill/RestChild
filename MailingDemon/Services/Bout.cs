// File:    Bout.cs
// Purpose: Definition of Class Bout

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MailingDemon.Services
{
   /// <summary>
   ///    Заезд
   /// </summary>
   [Serializable]
   [DataContract(Name = "bout")]
   public class BoutEx
   {
      /// <summary>
      ///    Дата заезда
      /// </summary>
      [Display(Description = "Дата заезда")]
      [Required(ErrorMessage = "\"Дата заезда\" должно быть заполнено")]
      [DataMember(Name = "dateIncome", EmitDefaultValue = false)]
      public virtual DateTime DateIncome { get; set; }

      /// <summary>
      ///    Дата отъезда
      /// </summary>
      [Display(Description = "Дата отъезда")]
      [Required(ErrorMessage = "\"Дата отъезда\" должно быть заполнено")]
      [DataMember(Name = "dateOutcome", EmitDefaultValue = false)]
      public virtual DateTime DateOutcome { get; set; }

      /// <summary>
      ///    Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }

      /// <summary>
      ///    Год кампании
      /// </summary>
      [Display(Description = "Год кампании")]
      [Required(ErrorMessage = "\"Год кампании\" должно быть заполнено")]
      [DataMember(Name = "yearOfCompany", EmitDefaultValue = false)]
      public virtual int YearOfCompany { get; set; }

      /// <summary>
      ///    Смена
      /// </summary>
      [Display(Description = "Смена")]
      [MaxLength(1000, ErrorMessage = "\"Смена\" не может быть больше 1000 символов")]
      [DataMember(Name = "change", EmitDefaultValue = false)]
      public virtual string Change { get; set; }

      /// <summary>
      ///    Заезды
      /// </summary>
      [DataMember(Name = "campId")]
      [Display(Description = "Заезды")]
      public virtual long? CampId { get; set; }

      /// <summary>
      ///    Уникальный идентификатор (не генерируемый)
      /// </summary>
      [Display(Description = "Уникальный идентификатор (не генерируемый)")]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }

      /// <summary>
      ///     Сотрудники
      /// </summary>
      [DataMember(Name = "personals", EmitDefaultValue = false)]
      public virtual BoutPersonalEx[] Personals { get; set; }

      /// <summary>
      ///     Статус
      /// </summary>
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }

      /// <summary>
      ///     Смена
      /// </summary>
      [DataMember(Name = "groupedTimeId")]
      [Display(Description = "Смена")]
      public virtual long? GroupedTimeId { get; set; }

      /// <summary>
      ///    Последнее сохранение
      /// </summary>
      [Display(Description = "Последнее сохранение")]
      [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
      public virtual long LastUpdateTick { get; set; }
   }
}
