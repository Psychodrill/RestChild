// File:    MonitoringHotel.cs
// Purpose: Definition of Class MonitoringHotel

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Оздаровительные организации
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringHotel")]
   public partial class MonitoringHotel : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентифкатор
      /// </summary>
      [Display(Description = "Уникальный идентифкатор")]
      [Required(ErrorMessage = "\"Уникальный идентифкатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [MaxLength(1000, ErrorMessage = "\"Название\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название\" не может быть пустым")]
      [DataMember(Name = "shortName", EmitDefaultValue = false)]
      public virtual string ShortName { get; set; }
      
      /// <summary>
      /// Полное наименование
      /// </summary>
      [Display(Description = "Полное наименование")]
      [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "fullName", EmitDefaultValue = false)]
      public virtual string FullName { get; set; }
      
      /// <summary>
      /// Фактический адрес
      /// </summary>
      [Display(Description = "Фактический адрес")]
      [MaxLength(1000, ErrorMessage = "\"Фактический адрес\" не может быть больше 1000 символов")]
      [DataMember(Name = "factAddress", EmitDefaultValue = false)]
      public virtual string FactAddress { get; set; }
      
      /// <summary>
      /// ИНН
      /// </summary>
      [Display(Description = "ИНН")]
      [MaxLength(1000, ErrorMessage = "\"ИНН\" не может быть больше 1000 символов")]
      [DataMember(Name = "inn", EmitDefaultValue = false)]
      public virtual string Inn { get; set; }
      
      /// <summary>
      /// Удалено
      /// </summary>
      [Display(Description = "Удалено")]
      [Required(ErrorMessage = "\"Удалено\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      
      /// <summary>
      /// Адрес лагеря мониторинга
      /// </summary>
      [ForeignKey("Address")]
      [DataMember(Name = "addressId")][Display(Description = "Адрес лагеря мониторинга")]
      public virtual long? AddressId { get; set; }
      /// <summary>
      /// Адрес лагеря мониторинга
      /// </summary>
      [Display(Description = "Адрес лагеря мониторинга")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual Address Address { get; set; }
      
      /// <summary>
      /// Мониторинг. Связь информации об оздаровительной организации с регионом отдыха
      /// </summary>
      [ForeignKey("Region")]
      [DataMember(Name = "regionId")]
      [Display(Description = "Мониторинг. Связь информации об оздаровительной организации с регионом отдыха")]
      public virtual long? RegionId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Связь информации об оздаровительной организации с регионом отдыха
      /// </summary>
      [Display(Description = "Мониторинг. Связь информации об оздаровительной организации с регионом отдыха")]
      [DataMember(Name = "region", EmitDefaultValue = false)]
      public virtual StateDistrict Region { get; set; }
      
      /// <summary>
      /// Мониторинг. Отель формы сведений о численности детей
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "hotelDatas", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringHotelData> HotelDatas { get; set; }
      
      /// <summary>
      /// Мониторинг. Оздоровительные организации. Связь с историей.
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "Мониторинг. Оздоровительные организации. Связь с историей.")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Оздоровительные организации. Связь с историей.
      /// </summary>
      [Display(Description = "Мониторинг. Оздоровительные организации. Связь с историей.")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }

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