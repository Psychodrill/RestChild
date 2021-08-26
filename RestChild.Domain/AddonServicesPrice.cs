// File:    AddonServicesPrice.cs
// Purpose: Definition of Class AddonServicesPrice

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Услуга. Стоимость
   /// </summary>
   [Serializable]
   [DataContract(Name = "addonServicesPrice")]
   public partial class AddonServicesPrice : IEntityBase
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
      [DataMember(Name = "ageTo", EmitDefaultValue = false)]
      public virtual int? AgeTo { get; set; }
      
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
      /// Период действия цены с
      /// </summary>
      [Display(Description = "Период действия цены с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Период действия цены по
      /// </summary>
      [Display(Description = "Период действия цены по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      
      /// <summary>
      /// Стоимость услуг
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "Стоимость услуг")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// Стоимость услуг
      /// </summary>
      [InverseProperty("Prices")]
      [Display(Description = "Стоимость услуг")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }

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