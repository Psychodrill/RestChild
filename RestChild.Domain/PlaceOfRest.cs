// File:    PlaceOfRest.cs
// Purpose: Definition of Class PlaceOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Регион отдыха
   /// </summary>
   [Serializable]
   [DataContract(Name = "placeOfRest")]
   public partial class PlaceOfRest : IEntityBase
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
      /// Наименование места отдыха
      /// </summary>
      [Display(Description = "Наименование места отдыха")]
      [MaxLength(1000, ErrorMessage = "\"Наименование места отдыха\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование места отдыха\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание места
      /// </summary>
      [Display(Description = "Описание места")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Ссылка на фотографию
      /// </summary>
      [Display(Description = "Ссылка на фотографию")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на фотографию\" не может быть больше 1000 символов")]
      [DataMember(Name = "photoUrl", EmitDefaultValue = false)]
      public virtual string PhotoUrl { get; set; }
      
      /// <summary>
      /// Признак необходимсоти загран паспорта
      /// </summary>
      [Display(Description = "Признак необходимсоти загран паспорта")]
      [Required(ErrorMessage = "\"Признак необходимсоти загран паспорта\" должно быть заполнено")]
      [DataMember(Name = "isForegin", EmitDefaultValue = false)]
      public virtual bool IsForegin { get; set; }
      
      /// <summary>
      /// Активность
      /// </summary>
      [Display(Description = "Активность")]
      [Required(ErrorMessage = "\"Активность\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Стоимость основного места
      /// </summary>
      [Display(Description = "Стоимость основного места")]
      [DataMember(Name = "priceBasePlace", EmitDefaultValue = false)]
      public virtual decimal? PriceBasePlace { get; set; }
      
      /// <summary>
      /// Стоимость дополнительного места
      /// </summary>
      [Display(Description = "Стоимость дополнительного места")]
      [DataMember(Name = "priceAddonPlace", EmitDefaultValue = false)]
      public virtual decimal? PriceAddonPlace { get; set; }
      
      /// <summary>
      /// Зона с морем
      /// </summary>
      [Display(Description = "Зона с морем")]
      [Required(ErrorMessage = "\"Зона с морем\" должно быть заполнено")]
      [DataMember(Name = "zoneOfSea", EmitDefaultValue = false)]
      public virtual bool ZoneOfSea { get; set; }
      
      /// <summary>
      /// Для МПГУ
      /// </summary>
      [Display(Description = "Для МПГУ")]
      [Required(ErrorMessage = "\"Для МПГУ\" должно быть заполнено")]
      [DataMember(Name = "forMpgu", EmitDefaultValue = false)]
      public virtual bool ForMpgu { get; set; }
      
      /// <summary>
      /// Для Сайта
      /// </summary>
      [Display(Description = "Для Сайта")]
      [Required(ErrorMessage = "\"Для Сайта\" должно быть заполнено")]
      [DataMember(Name = "forSite", EmitDefaultValue = false)]
      public virtual bool ForSite { get; set; }
      
      /// <summary>
      /// Не для выбора
      /// </summary>
      [Display(Description = "Не для выбора")]
      [Required(ErrorMessage = "\"Не для выбора\" должно быть заполнено")]
      [DataMember(Name = "notForSelect", EmitDefaultValue = false)]
      public virtual bool NotForSelect { get; set; }
      
      
      /// <summary>
      /// Связь места отдыха с заявлением
      /// </summary>
      [InverseProperty("PlaceOfRest")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<RequestPlaceOfRest> Requests { get; set; }
      
      /// <summary>
      /// Цели обращения в регионах отдыха
      /// </summary>
      [InverseProperty("PlaceOfRest")]
      [DataMember(Name = "typeOfRests", EmitDefaultValue = false)]
      public virtual ICollection<PlaceOfRestTypeOfRest> TypeOfRests { get; set; }
      
      /// <summary>
      /// Регионы отдыха
      /// </summary>
      [InverseProperty("PlaceOfRests")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual ICollection<AddonServices> AddonServices { get; set; }
      
      /// <summary>
      /// Иеархия регионов отдыха
      /// </summary>
      [ForeignKey("Group")]
      [DataMember(Name = "groupId")]
      [Display(Description = "Иеархия регионов отдыха")]
      public virtual long? GroupId { get; set; }
      
      
      /// <summary>
      /// Иеархия регионов отдыха
      /// </summary>
      [Display(Description = "Иеархия регионов отдыха")]
      [DataMember(Name = "group", EmitDefaultValue = false)]
      public virtual PlaceOfRest Group { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Страна
      /// </summary>
      [ForeignKey("Country")]
      [DataMember(Name = "countryId")]
      [Display(Description = "Страна")]
      public virtual long? CountryId { get; set; }
      
      
      /// <summary>
      /// Страна
      /// </summary>
      [Display(Description = "Страна")]
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