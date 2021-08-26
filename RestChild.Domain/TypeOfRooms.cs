// File:    TypeOfRooms.cs
// Purpose: Definition of Class TypeOfRooms

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вид номера
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfRooms")]
   public partial class TypeOfRooms : IEntityBase
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
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveFurniture", EmitDefaultValue = false)]
      public virtual bool HaveFurniture { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveBalcony", EmitDefaultValue = false)]
      public virtual bool HaveBalcony { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveTv", EmitDefaultValue = false)]
      public virtual bool HaveTv { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveBath", EmitDefaultValue = false)]
      public virtual bool HaveBath { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveSatelliteTv", EmitDefaultValue = false)]
      public virtual bool HaveSatelliteTv { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveShower", EmitDefaultValue = false)]
      public virtual bool HaveShower { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveLocalTv", EmitDefaultValue = false)]
      public virtual bool HaveLocalTv { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveHairDryer", EmitDefaultValue = false)]
      public virtual bool HaveHairDryer { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveRadio", EmitDefaultValue = false)]
      public virtual bool HaveRadio { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveWc", EmitDefaultValue = false)]
      public virtual bool HaveWc { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "havePhone", EmitDefaultValue = false)]
      public virtual bool HavePhone { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveBidet", EmitDefaultValue = false)]
      public virtual bool HaveBidet { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveBar", EmitDefaultValue = false)]
      public virtual bool HaveBar { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveAirConditioning", EmitDefaultValue = false)]
      public virtual bool HaveAirConditioning { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveSafe", EmitDefaultValue = false)]
      public virtual bool HaveSafe { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveKitchen", EmitDefaultValue = false)]
      public virtual bool HaveKitchen { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "haveRefrigerator", EmitDefaultValue = false)]
      public virtual bool HaveRefrigerator { get; set; }
      
      /// <summary>
      /// Количество основных мест
      /// </summary>
      [Display(Description = "Количество основных мест")]
      [Required(ErrorMessage = "\"Количество основных мест\" должно быть заполнено")]
      [DataMember(Name = "countBasePlace", EmitDefaultValue = false)]
      public virtual int CountBasePlace { get; set; }
      
      /// <summary>
      /// Количество дополнительных мест
      /// </summary>
      [Display(Description = "Количество дополнительных мест")]
      [Required(ErrorMessage = "\"Количество дополнительных мест\" должно быть заполнено")]
      [DataMember(Name = "countAddonPlace", EmitDefaultValue = false)]
      public virtual int CountAddonPlace { get; set; }
      
      /// <summary>
      /// Максимальное количество номеров
      /// </summary>
      [Display(Description = "Максимальное количество номеров")]
      [Required(ErrorMessage = "\"Максимальное количество номеров\" должно быть заполнено")]
      [DataMember(Name = "maximumCount", EmitDefaultValue = false)]
      public virtual int MaximumCount { get; set; }
      
      /// <summary>
      /// Площадь номера (кв. м.)
      /// </summary>
      [Display(Description = "Площадь номера (кв. м.)")]
      [DataMember(Name = "roomSize", EmitDefaultValue = false)]
      public virtual decimal? RoomSize { get; set; }
      
      /// <summary>
      /// Количество м2 на человека
      /// </summary>
      [Display(Description = "Количество м2 на человека")]
      [DataMember(Name = "roomSizePerPerson", EmitDefaultValue = false)]
      public virtual decimal? RoomSizePerPerson { get; set; }
      
      
      /// <summary>
      /// Ссылка на файл вида номера
      /// </summary>
      [InverseProperty("TypeOfRooms")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<FileHotel> Files { get; set; }
      
      /// <summary>
      /// Связь отелей и видов номеров
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")]
      [Display(Description = "Связь отелей и видов номеров")]
      public virtual long? HotelId { get; set; }
      
      
      /// <summary>
      /// Связь отелей и видов номеров
      /// </summary>
      [InverseProperty("TypeOfRooms")]
      [Display(Description = "Связь отелей и видов номеров")]
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