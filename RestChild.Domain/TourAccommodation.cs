// File:    TourAccommodation.cs
// Purpose: Definition of Class TourAccommodation

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Проживание
   /// </summary>
   [Serializable]
   [DataContract(Name = "tourAccommodation")]
   public partial class TourAccommodation : IEntityBase
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
      /// Дата заезда
      /// </summary>
      [Display(Description = "Дата заезда")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата отъезда
      /// </summary>
      [Display(Description = "Дата отъезда")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Требуется подтверждение
      /// </summary>
      [Display(Description = "Требуется подтверждение")]
      [Required(ErrorMessage = "\"Требуется подтверждение\" должно быть заполнено")]
      [DataMember(Name = "needApprove", EmitDefaultValue = false)]
      public virtual bool NeedApprove { get; set; }
      
      
      /// <summary>
      /// Варианты размещения
      /// </summary>
      [InverseProperty("TourAccommodation")]
      [DataMember(Name = "roomRates", EmitDefaultValue = false)]
      public virtual ICollection<RoomRates> RoomRates { get; set; }
      
      /// <summary>
      /// Фонд
      /// </summary>
      [InverseProperty("TourAccommodation")]
      [DataMember(Name = "volumes", EmitDefaultValue = false)]
      public virtual ICollection<TourVolume> Volumes { get; set; }
      
      /// <summary>
      /// Тематика
      /// </summary>
      [ForeignKey("SubjectOfRest")]
      [DataMember(Name = "subjectOfRestId")]
      [Display(Description = "Тематика")]
      public virtual long? SubjectOfRestId { get; set; }
      
      
      /// <summary>
      /// Тематика
      /// </summary>
      [Display(Description = "Тематика")]
      [DataMember(Name = "subjectOfRest", EmitDefaultValue = false)]
      public virtual SubjectOfRest SubjectOfRest { get; set; }
      
      /// <summary>
      /// Год кампании
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год кампании")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год кампании
      /// </summary>
      [Display(Description = "Год кампании")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
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
      [Display(Description = "Отель")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual Hotels Hotel { get; set; }
      
      /// <summary>
      /// Проживания
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Проживания")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Проживания
      /// </summary>
      [InverseProperty("TourAccommodations")]
      [Display(Description = "Проживания")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Связь с географией мероприятия
      /// </summary>
      [ForeignKey("EventGeography")]
      [DataMember(Name = "eventGeographyId")]
      [Display(Description = "Связь с географией мероприятия")]
      public virtual long? EventGeographyId { get; set; }
      
      
      /// <summary>
      /// Связь с географией мероприятия
      /// </summary>
      [Display(Description = "Связь с географией мероприятия")]
      [DataMember(Name = "eventGeography", EmitDefaultValue = false)]
      public virtual EventGeography EventGeography { get; set; }
      
      /// <summary>
      /// Тип стоимости
      /// </summary>
      [ForeignKey("TypePriceCalculation")]
      [DataMember(Name = "typePriceCalculationId")]
      [Display(Description = "Тип стоимости")]
      public virtual long? TypePriceCalculationId { get; set; }
      
      
      /// <summary>
      /// Тип стоимости
      /// </summary>
      [Display(Description = "Тип стоимости")]
      [DataMember(Name = "typePriceCalculation", EmitDefaultValue = false)]
      public virtual TypePriceCalculation TypePriceCalculation { get; set; }

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