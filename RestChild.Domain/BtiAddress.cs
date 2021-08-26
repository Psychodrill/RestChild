// File:    BtiAddress.cs
// Purpose: Definition of Class BtiAddress

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Адрес по БТИ
   /// </summary>
   [Serializable]
   [DataContract(Name = "btiAddress")]
   public partial class BtiAddress : IEntityBase
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
      /// Наименование адреса
      /// </summary>
      [Display(Description = "Наименование адреса")]
      [MaxLength(1000, ErrorMessage = "\"Наименование адреса\" не может быть больше 1000 символов")]
      [DataMember(Name = "fullAddress", EmitDefaultValue = false)]
      public virtual string FullAddress { get; set; }
      
      /// <summary>
      /// Уном
      /// </summary>
      [Display(Description = "Уном")]
      [Required(ErrorMessage = "\"Уном\" должно быть заполнено")]
      [DataMember(Name = "unom", EmitDefaultValue = false)]
      public virtual long Unom { get; set; }
      
      /// <summary>
      /// Краткое имя адреса (дом корпус)
      /// </summary>
      [Display(Description = "Краткое имя адреса (дом корпус)")]
      [MaxLength(1000, ErrorMessage = "\"Краткое имя адреса (дом корпус)\" не может быть больше 1000 символов")]
      [DataMember(Name = "shortAddress", EmitDefaultValue = false)]
      public virtual string ShortAddress { get; set; }
      
      /// <summary>
      /// Уникальны номер адреса в статкарте
      /// </summary>
      [Display(Description = "Уникальны номер адреса в статкарте")]
      [Required(ErrorMessage = "\"Уникальны номер адреса в статкарте\" должно быть заполнено")]
      [DataMember(Name = "unod", EmitDefaultValue = false)]
      public virtual long Unod { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [Required(ErrorMessage = "\"Статус\" должно быть заполнено")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual long Status { get; set; }
      
      
      /// <summary>
      /// Округ
      /// </summary>
      [ForeignKey("BtiDistrict")]
      [DataMember(Name = "btiDistrictId")]
      [Display(Description = "Округ")]
      public virtual long? BtiDistrictId { get; set; }
      
      
      /// <summary>
      /// Округ
      /// </summary>
      [Display(Description = "Округ")]
      [DataMember(Name = "btiDistrict", EmitDefaultValue = false)]
      public virtual BtiDistrict BtiDistrict { get; set; }
      
      /// <summary>
      /// Район
      /// </summary>
      [ForeignKey("BtiRegion")]
      [DataMember(Name = "btiRegionId")]
      [Display(Description = "Район")]
      public virtual long? BtiRegionId { get; set; }
      
      
      /// <summary>
      /// Район
      /// </summary>
      [Display(Description = "Район")]
      [DataMember(Name = "btiRegion", EmitDefaultValue = false)]
      public virtual BtiRegion BtiRegion { get; set; }
      
      /// <summary>
      /// Улица
      /// </summary>
      [ForeignKey("BtiStreet")]
      [DataMember(Name = "btiStreetId")]
      [Display(Description = "Улица")]
      public virtual long? BtiStreetId { get; set; }
      
      
      /// <summary>
      /// Улица
      /// </summary>
      [Display(Description = "Улица")]
      [DataMember(Name = "btiStreet", EmitDefaultValue = false)]
      public virtual BtiStreet BtiStreet { get; set; }

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