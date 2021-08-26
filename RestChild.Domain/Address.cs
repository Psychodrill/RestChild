// File:    Address.cs
// Purpose: Definition of Class Address

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Адрес
   /// </summary>
   [Serializable]
   [DataContract(Name = "address")]
   public partial class Address : IEntityBase
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
      /// Полное наименование
      /// </summary>
      [Display(Description = "Полное наименование")]
      [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Номер квартиры
      /// </summary>
      [Display(Description = "Номер квартиры")]
      [MaxLength(1000, ErrorMessage = "\"Номер квартиры\" не может быть больше 1000 символов")]
      [DataMember(Name = "appartment", EmitDefaultValue = false)]
      public virtual string Appartment { get; set; }
      
      /// <summary>
      /// Улица
      /// </summary>
      [Display(Description = "Улица")]
      [MaxLength(1000, ErrorMessage = "\"Улица\" не может быть больше 1000 символов")]
      [DataMember(Name = "street", EmitDefaultValue = false)]
      public virtual string Street { get; set; }
      
      /// <summary>
      /// Дом
      /// </summary>
      [Display(Description = "Дом")]
      [MaxLength(1000, ErrorMessage = "\"Дом\" не может быть больше 1000 символов")]
      [DataMember(Name = "house", EmitDefaultValue = false)]
      public virtual string House { get; set; }
      
      /// <summary>
      /// Корпус
      /// </summary>
      [Display(Description = "Корпус")]
      [MaxLength(1000, ErrorMessage = "\"Корпус\" не может быть больше 1000 символов")]
      [DataMember(Name = "corpus", EmitDefaultValue = false)]
      public virtual string Corpus { get; set; }
      
      /// <summary>
      /// Строение
      /// </summary>
      [Display(Description = "Строение")]
      [MaxLength(1000, ErrorMessage = "\"Строение\" не может быть больше 1000 символов")]
      [DataMember(Name = "stroenie", EmitDefaultValue = false)]
      public virtual string Stroenie { get; set; }
      
      /// <summary>
      /// Широта
      /// </summary>
      [Display(Description = "Широта")]
      [DataMember(Name = "latitude", EmitDefaultValue = false)]
      public virtual decimal? Latitude { get; set; }
      
      /// <summary>
      /// Долгота
      /// </summary>
      [Display(Description = "Долгота")]
      [DataMember(Name = "longitude", EmitDefaultValue = false)]
      public virtual decimal? Longitude { get; set; }
      
      /// <summary>
      /// Владение
      /// </summary>
      [Display(Description = "Владение")]
      [MaxLength(1000, ErrorMessage = "\"Владение\" не может быть больше 1000 символов")]
      [DataMember(Name = "vladenie", EmitDefaultValue = false)]
      public virtual string Vladenie { get; set; }
      
      /// <summary>
      /// Идентификатор ФИАС
      /// </summary>
      [Display(Description = "Идентификатор ФИАС")]
      [MaxLength(1000, ErrorMessage = "\"Идентификатор ФИАС\" не может быть больше 1000 символов")]
      [DataMember(Name = "fiasId", EmitDefaultValue = false)]
      public virtual string FiasId { get; set; }
      
      
      /// <summary>
      /// Адрес БТИ
      /// </summary>
      [ForeignKey("BtiAddress")]
      [DataMember(Name = "btiAddressId")]
      [Display(Description = "Адрес БТИ")]
      public virtual long? BtiAddressId { get; set; }
      
      
      /// <summary>
      /// Адрес БТИ
      /// </summary>
      [Display(Description = "Адрес БТИ")]
      [DataMember(Name = "btiAddress", EmitDefaultValue = false)]
      public virtual BtiAddress BtiAddress { get; set; }
      
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