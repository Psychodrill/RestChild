// File:    Booking.cs
// Purpose: Definition of Class Booking

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Бронирование
   /// </summary>
   [Serializable]
   [DataContract(Name = "booking")]
   public partial class Booking : IEntityBase
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
      /// ИД бронирования
      /// </summary>
      [Display(Description = "ИД бронирования")]
      [Required(ErrorMessage = "\"ИД бронирования\" должно быть заполнено")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual Guid Code { get; set; }
      
      /// <summary>
      /// Дата бронирования
      /// </summary>
      [Display(Description = "Дата бронирования")]
      [Required(ErrorMessage = "\"Дата бронирования\" должно быть заполнено")]
      [DataMember(Name = "bookingDate", EmitDefaultValue = false)]
      public virtual DateTime BookingDate { get; set; }
      
      /// <summary>
      /// Количество номеров
      /// </summary>
      [Display(Description = "Количество номеров")]
      [DataMember(Name = "countRooms", EmitDefaultValue = false)]
      public virtual int? CountRooms { get; set; }
      
      /// <summary>
      /// Количество мест
      /// </summary>
      [Display(Description = "Количество мест")]
      [DataMember(Name = "countPlace", EmitDefaultValue = false)]
      public virtual int? CountPlace { get; set; }
      
      /// <summary>
      /// Количество мест сопровождающих
      /// </summary>
      [Display(Description = "Количество мест сопровождающих")]
      [DataMember(Name = "countAttendants", EmitDefaultValue = false)]
      public virtual int? CountAttendants { get; set; }
      
      /// <summary>
      /// Отмена бронирования
      /// </summary>
      [Display(Description = "Отмена бронирования")]
      [Required(ErrorMessage = "\"Отмена бронирования\" должно быть заполнено")]
      [DataMember(Name = "canceled", EmitDefaultValue = false)]
      public virtual bool Canceled { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("TourVolume")]
      [DataMember(Name = "tourVolumeId")]
      [Display(Description = "")]
      public virtual long? TourVolumeId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "tourVolume", EmitDefaultValue = false)]
      public virtual TourVolume TourVolume { get; set; }
      
      /// <summary>
      /// Связь бронирования и заявления
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Связь бронирования и заявления")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Связь бронирования и заявления
      /// </summary>
      [Display(Description = "Связь бронирования и заявления")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Ссылка на вид отдыха
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Ссылка на вид отдыха")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Ссылка на вид отдыха
      /// </summary>
      [Display(Description = "Ссылка на вид отдыха")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }

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