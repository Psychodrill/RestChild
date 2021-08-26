// File:    HotelBlock.cs
// Purpose: Definition of Class HotelBlock

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Блок мест по отелю
   /// </summary>
   [Serializable]
   [DataContract(Name = "hotelBlock")]
   public partial class HotelBlock : IEntityBase
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
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [Required(ErrorMessage = "\"Дата с\" должно быть заполнено")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [Required(ErrorMessage = "\"Дата по\" должно быть заполнено")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime DateTo { get; set; }
      
      /// <summary>
      /// Количество номеров
      /// </summary>
      [Display(Description = "Количество номеров")]
      [Required(ErrorMessage = "\"Количество номеров\" должно быть заполнено")]
      [DataMember(Name = "count", EmitDefaultValue = false)]
      public virtual int Count { get; set; }
      
      /// <summary>
      /// Версия записи
      /// </summary>
      [Display(Description = "Версия записи")]
      [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
      public virtual byte[] RowVersion { get; set; }
      
      
      /// <summary>
      /// Места в блоке мест по датам
      /// </summary>
      [InverseProperty("Block")]
      [DataMember(Name = "dates", EmitDefaultValue = false)]
      public virtual ICollection<HotelBlockDate> Dates { get; set; }
      
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
      [InverseProperty("PlaceBloks")]
      [Display(Description = "Отель")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual Hotels Hotel { get; set; }
      
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