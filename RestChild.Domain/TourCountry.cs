// File:    TourCountry.cs
// Purpose: Definition of Class TourCountry

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Страна в продукте
   /// </summary>
   [Serializable]
   [DataContract(Name = "tourCountry")]
   public partial class TourCountry : IEntityBase
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
      /// Главная страна
      /// </summary>
      [Display(Description = "Главная страна")]
      [Required(ErrorMessage = "\"Главная страна\" должно быть заполнено")]
      [DataMember(Name = "isMain", EmitDefaultValue = false)]
      public virtual bool IsMain { get; set; }
      
      
      /// <summary>
      /// Связь страны и размещения
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Связь страны и размещения")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Связь страны и размещения
      /// </summary>
      [InverseProperty("Countrys")]
      [Display(Description = "Связь страны и размещения")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Связь размещения и страны
      /// </summary>
      [ForeignKey("Country")]
      [DataMember(Name = "countryId")]
      [Display(Description = "Связь размещения и страны")]
      public virtual long? CountryId { get; set; }
      
      
      /// <summary>
      /// Связь размещения и страны
      /// </summary>
      [InverseProperty("Tours")]
      [Display(Description = "Связь размещения и страны")]
      [DataMember(Name = "country", EmitDefaultValue = false)]
      public virtual Country Country { get; set; }

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