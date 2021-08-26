// File:    Accommodation.cs
// Purpose: Definition of Class Accommodation

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Варианты размещения
   /// </summary>
   [Serializable]
   [DataContract(Name = "accommodation")]
   public partial class Accommodation : IEntityBase
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
      /// Количество взрослых
      /// </summary>
      [Display(Description = "Количество взрослых")]
      [Required(ErrorMessage = "\"Количество взрослых\" должно быть заполнено")]
      [DataMember(Name = "adult", EmitDefaultValue = false)]
      public virtual int Adult { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      
      /// <summary>
      /// Размещение детей
      /// </summary>
      [InverseProperty("Accommodation")]
      [DataMember(Name = "accommodationChildren", EmitDefaultValue = false)]
      public virtual ICollection<AccommodationChildren> AccommodationChildren { get; set; }
      
      /// <summary>
      /// Отель
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")]
      [Display(Description = "Отель")]
      public virtual long? HotelId { get; set; }
      
      
      /// <summary>
      /// Отель
      /// </summary>
      [InverseProperty("Accommodation")]
      [Display(Description = "Отель")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual Hotels Hotel { get; set; }

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