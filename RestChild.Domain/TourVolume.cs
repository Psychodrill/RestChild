// File:    TourVolume.cs
// Purpose: Definition of Class TourVolume

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Объем фонда
   /// </summary>
   [Serializable]
   [DataContract(Name = "tourVolume")]
   public partial class TourVolume : IEntityBase
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
      /// Количество номеров в заезде
      /// </summary>
      [Display(Description = "Количество номеров в заезде")]
      [DataMember(Name = "countRooms", EmitDefaultValue = false)]
      public virtual int? CountRooms { get; set; }
      
      /// <summary>
      /// Занято номеров для лагеря
      /// </summary>
      [Display(Description = "Занято номеров для лагеря")]
      [DataMember(Name = "countBusyRooms", EmitDefaultValue = false)]
      public virtual int? CountBusyRooms { get; set; }
      
      /// <summary>
      /// Количество мест для лагеря
      /// </summary>
      [Display(Description = "Количество мест для лагеря")]
      [DataMember(Name = "countPlace", EmitDefaultValue = false)]
      public virtual int? CountPlace { get; set; }
      
      /// <summary>
      /// Занято мест для лагеря
      /// </summary>
      [Display(Description = "Занято мест для лагеря")]
      [DataMember(Name = "countBusyPlace", EmitDefaultValue = false)]
      public virtual int? CountBusyPlace { get; set; }
      
      /// <summary>
      /// Дата экскурсии
      /// </summary>
      [Display(Description = "Дата экскурсии")]
      [DataMember(Name = "eventDate", EmitDefaultValue = false)]
      public virtual DateTime? EventDate { get; set; }
      
      /// <summary>
      /// Дата окончания бронирования
      /// </summary>
      [Display(Description = "Дата окончания бронирования")]
      [DataMember(Name = "endBooking", EmitDefaultValue = false)]
      public virtual DateTime? EndBooking { get; set; }
      
      /// <summary>
      /// Версия записи
      /// </summary>
      [Display(Description = "Версия записи")]
      [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
      public virtual byte[] RowVersion { get; set; }
      
      
      /// <summary>
      /// Связь вида номера и объема
      /// </summary>
      [ForeignKey("TypeOfRooms")]
      [DataMember(Name = "typeOfRoomsId")]
      [Display(Description = "Связь вида номера и объема")]
      public virtual long? TypeOfRoomsId { get; set; }
      
      
      /// <summary>
      /// Связь вида номера и объема
      /// </summary>
      [Display(Description = "Связь вида номера и объема")]
      [DataMember(Name = "typeOfRooms", EmitDefaultValue = false)]
      public virtual TypeOfRooms TypeOfRooms { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
      /// <summary>
      /// Связь заезда и номерного фонда
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Связь заезда и номерного фонда")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Связь заезда и номерного фонда
      /// </summary>
      [InverseProperty("Volumes")]
      [Display(Description = "Связь заезда и номерного фонда")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Фонд
      /// </summary>
      [ForeignKey("TourAccommodation")]
      [DataMember(Name = "tourAccommodationId")]
      [Display(Description = "Фонд")]
      public virtual long? TourAccommodationId { get; set; }
      
      
      /// <summary>
      /// Фонд
      /// </summary>
      [InverseProperty("Volumes")]
      [Display(Description = "Фонд")]
      [DataMember(Name = "tourAccommodation", EmitDefaultValue = false)]
      public virtual TourAccommodation TourAccommodation { get; set; }

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