// File:    BookingCommercial.cs
// Purpose: Definition of Class BookingCommercial

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Комереческое бронирование
   /// </summary>
   [Serializable]
   [DataContract(Name = "bookingCommercial")]
   public partial class BookingCommercial : IEntityBase
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
      /// Дата бронирования
      /// </summary>
      [Display(Description = "Дата бронирования")]
      [Required(ErrorMessage = "\"Дата бронирования\" должно быть заполнено")]
      [DataMember(Name = "dateBooking", EmitDefaultValue = false)]
      public virtual DateTime DateBooking { get; set; }
      
      /// <summary>
      /// Идентификатор брони
      /// </summary>
      [Display(Description = "Идентификатор брони")]
      [Required(ErrorMessage = "\"Идентификатор брони\" должно быть заполнено")]
      [DataMember(Name = "uid", EmitDefaultValue = false)]
      public virtual Guid Uid { get; set; }
      
      /// <summary>
      /// Бронирование отменено
      /// </summary>
      [Display(Description = "Бронирование отменено")]
      [Required(ErrorMessage = "\"Бронирование отменено\" должно быть заполнено")]
      [DataMember(Name = "isCancel", EmitDefaultValue = false)]
      public virtual bool IsCancel { get; set; }
      
      /// <summary>
      /// Версия записи
      /// </summary>
      [Display(Description = "Версия записи")]
      [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
      public virtual byte[] RowVersion { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "count", EmitDefaultValue = false)]
      public virtual int Count { get; set; }
      
      
      /// <summary>
      /// Ссылка на количество
      /// </summary>
      [ForeignKey("TourVolume")]
      [DataMember(Name = "tourVolumeId")]
      [Display(Description = "Ссылка на количество")]
      public virtual long? TourVolumeId { get; set; }
      
      
      /// <summary>
      /// Ссылка на количество
      /// </summary>
      [Display(Description = "Ссылка на количество")]
      [DataMember(Name = "tourVolume", EmitDefaultValue = false)]
      public virtual TourVolume TourVolume { get; set; }
      
      /// <summary>
      /// Бронирования
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Бронирования")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Бронирования
      /// </summary>
      [InverseProperty("BookingsCom")]
      [Display(Description = "Бронирования")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Ссылка на продукт
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Ссылка на продукт")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Ссылка на продукт
      /// </summary>
      [Display(Description = "Ссылка на продукт")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }

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