// File:    BoutPersonal.cs
// Purpose: Definition of Class BoutPersonal

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MailingDemon.Services
{
   /// <summary>
   ///    Сотрудник. В заезде
   /// </summary>
   [Serializable]
   [DataContract(Name = "boutPersonal")]
   public class BoutPersonalEx
   {
      /// <summary>
      ///    Отряд
      /// </summary>
      [DataMember(Name = "partyId")]
      [Display(Description = "Отряд")]
      public virtual long? PartyId { get; set; }

      /// <summary>
      ///    Персонал
      /// </summary>
      [DataMember(Name = "personalId")]
      [Display(Description = "Персонал")]
      public virtual long? PersonalId { get; set; }

      /// <summary>
      ///    Вид персонала
      /// </summary>
      [DataMember(Name = "personalTypeId")]
      [Display(Description = "Вид персонала")]
      public virtual long? PersonalTypeId { get; set; }

      /// <summary>
      ///    Сотрудники
      /// </summary>
      [DataMember(Name = "boutId")]
      [Display(Description = "Сотрудники")]
      public virtual long? BoutId { get; set; }

      /// <summary>
      ///    Уникальный идентификатор (не генерируемый)
      /// </summary>
      [Display(Description = "Уникальный идентификатор (не генерируемый)")]
      [Required(ErrorMessage = "\"Уникальный идентификатор (не генерируемый)\" должно быть заполнено")]
      public virtual long Id { get; set; }

      /// <summary>
      ///    Последнее сохранение
      /// </summary>
      [Display(Description = "Последнее сохранение")]
      [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
      public virtual long LastUpdateTick { get; set; }
   }
}
