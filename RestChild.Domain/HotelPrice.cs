// File:    HotelPrice.cs
// Purpose: Definition of Class HotelPrice

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отели/лагеря Матрица цен
   /// </summary>
   [Serializable]
   [DataContract(Name = "hotelPrice")]
   public partial class HotelPrice : IEntityBase
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
      /// Возраст с
      /// </summary>
      [Display(Description = "Возраст с")]
      [DataMember(Name = "ageFrom", EmitDefaultValue = false)]
      public virtual int? AgeFrom { get; set; }
      
      /// <summary>
      /// Возраст по
      /// </summary>
      [Display(Description = "Возраст по")]
      [DataMember(Name = "agtTo", EmitDefaultValue = false)]
      public virtual int? AgtTo { get; set; }
      
      /// <summary>
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Стоимость
      /// </summary>
      [Display(Description = "Стоимость")]
      [Required(ErrorMessage = "\"Стоимость\" должно быть заполнено")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal Price { get; set; }
      
      /// <summary>
      /// Себестоимость
      /// </summary>
      [Display(Description = "Себестоимость")]
      [Required(ErrorMessage = "\"Себестоимость\" должно быть заполнено")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal PriceInternal { get; set; }
      
      
      /// <summary>
      /// Вариант питания
      /// </summary>
      [ForeignKey("DiningOptions")]
      [DataMember(Name = "diningOptionsId")]
      [Display(Description = "Вариант питания")]
      public virtual long? DiningOptionsId { get; set; }
      
      
      /// <summary>
      /// Вариант питания
      /// </summary>
      [Display(Description = "Вариант питания")]
      [DataMember(Name = "diningOptions", EmitDefaultValue = false)]
      public virtual DiningOptions DiningOptions { get; set; }
      
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
      /// Вид номера
      /// </summary>
      [ForeignKey("TypeOfRooms")]
      [DataMember(Name = "typeOfRoomsId")]
      [Display(Description = "Вид номера")]
      public virtual long? TypeOfRoomsId { get; set; }
      
      
      /// <summary>
      /// Вид номера
      /// </summary>
      [Display(Description = "Вид номера")]
      [DataMember(Name = "typeOfRooms", EmitDefaultValue = false)]
      public virtual TypeOfRooms TypeOfRooms { get; set; }
      
      /// <summary>
      /// Размещение
      /// </summary>
      [ForeignKey("Accommodation")]
      [DataMember(Name = "accommodationId")]
      [Display(Description = "Размещение")]
      public virtual long? AccommodationId { get; set; }
      
      
      /// <summary>
      /// Размещение
      /// </summary>
      [Display(Description = "Размещение")]
      [DataMember(Name = "accommodation", EmitDefaultValue = false)]
      public virtual Accommodation Accommodation { get; set; }
      
      /// <summary>
      /// Цены
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")]
      [Display(Description = "Цены")]
      public virtual long? HotelId { get; set; }
      
      
      /// <summary>
      /// Цены
      /// </summary>
      [InverseProperty("MatrixOfPrice")]
      [Display(Description = "Цены")]
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