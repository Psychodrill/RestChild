// File:    OfferInRequest.cs
// Purpose: Definition of Class OfferInRequest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Размещения в заявлении
   /// </summary>
   [Serializable]
   [DataContract(Name = "offerInRequest")]
   public partial class OfferInRequest : IEntityBase
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
      /// Дата начала
      /// </summary>
      [Display(Description = "Дата начала")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата окончания
      /// </summary>
      [Display(Description = "Дата окончания")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Цена
      /// </summary>
      [Display(Description = "Цена")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Цена внутренняя
      /// </summary>
      [Display(Description = "Цена внутренняя")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal? PriceInternal { get; set; }
      
      
      /// <summary>
      /// Размещение
      /// </summary>
      [InverseProperty("OfferInRequest")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<Child> Children { get; set; }
      
      /// <summary>
      /// Размещение
      /// </summary>
      [InverseProperty("OfferInRequest")]
      [DataMember(Name = "attendants", EmitDefaultValue = false)]
      public virtual ICollection<Applicant> Attendants { get; set; }
      
      /// <summary>
      /// Использованиые варианты размещений
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")][Display(Description = "Использованиые варианты размещений")]
      public virtual long? RequestId { get; set; }
      /// <summary>
      /// Использованиые варианты размещений
      /// </summary>
      [InverseProperty("OfferInRequest")]
      [Display(Description = "Использованиые варианты размещений")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Стоимость номера
      /// </summary>
      [ForeignKey("RoomRates")]
      [DataMember(Name = "roomRatesId")]
      [Display(Description = "Стоимость номера")]
      public virtual long? RoomRatesId { get; set; }
      
      
      /// <summary>
      /// Стоимость номера
      /// </summary>
      [Display(Description = "Стоимость номера")]
      [DataMember(Name = "roomRates", EmitDefaultValue = false)]
      public virtual RoomRates RoomRates { get; set; }

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