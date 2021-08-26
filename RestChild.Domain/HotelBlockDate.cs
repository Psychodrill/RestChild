// File:    HotelBlockDate.cs
// Purpose: Definition of Class HotelBlockDate

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Блок мест по датам
   /// </summary>
   [Serializable]
   [DataContract(Name = "hotelBlockDate")]
   public partial class HotelBlockDate : IEntityBase
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
      /// Дата
      /// </summary>
      [Display(Description = "Дата")]
      [Required(ErrorMessage = "\"Дата\" должно быть заполнено")]
      [DataMember(Name = "date", EmitDefaultValue = false)]
      public virtual DateTime Date { get; set; }
      
      /// <summary>
      /// Колечество мест
      /// </summary>
      [Display(Description = "Колечество мест")]
      [Required(ErrorMessage = "\"Колечество мест\" должно быть заполнено")]
      [DataMember(Name = "count", EmitDefaultValue = false)]
      public virtual int Count { get; set; }
      
      /// <summary>
      /// Свободно мест
      /// </summary>
      [Display(Description = "Свободно мест")]
      [Required(ErrorMessage = "\"Свободно мест\" должно быть заполнено")]
      [DataMember(Name = "free", EmitDefaultValue = false)]
      public virtual int Free { get; set; }
      
      /// <summary>
      /// Версия записи
      /// </summary>
      [Display(Description = "Версия записи")]
      [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
      public virtual byte[] RowVersion { get; set; }
      
      
      /// <summary>
      /// Места в блоке мест
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")]
      [Display(Description = "Места в блоке мест")]
      public virtual long? HotelId { get; set; }
      
      
      /// <summary>
      /// Места в блоке мест
      /// </summary>
      [Display(Description = "Места в блоке мест")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual Hotels Hotel { get; set; }
      
      /// <summary>
      /// Места в блоке мест по датам
      /// </summary>
      [ForeignKey("Block")]
      [DataMember(Name = "blockId")]
      [Display(Description = "Места в блоке мест по датам")]
      public virtual long? BlockId { get; set; }
      
      
      /// <summary>
      /// Места в блоке мест по датам
      /// </summary>
      [InverseProperty("Dates")]
      [Display(Description = "Места в блоке мест по датам")]
      [DataMember(Name = "block", EmitDefaultValue = false)]
      public virtual HotelBlock Block { get; set; }
      
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