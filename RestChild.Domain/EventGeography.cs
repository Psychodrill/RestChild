// File:    EventGeography.cs
// Purpose: Definition of Class EventGeography

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// География мероприятия
   /// </summary>
   [Serializable]
   [DataContract(Name = "eventGeography")]
   public partial class EventGeography : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Поле по порядку
      /// </summary>
      [Display(Description = "Поле по порядку")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual int? SortOrder { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Продолжительность в днях
      /// </summary>
      [Display(Description = "Продолжительность в днях")]
      [DataMember(Name = "durationInDays", EmitDefaultValue = false)]
      public virtual int? DurationInDays { get; set; }
      
      
      /// <summary>
      /// Ссылка на самого себя
      /// </summary>
      [InverseProperty("Parent")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<EventGeography> Children { get; set; }
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "География мероприятия")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [InverseProperty("EventGeographys")]
      [Display(Description = "География мероприятия")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }
      
      /// <summary>
      /// Город
      /// </summary>
      [ForeignKey("City")]
      [DataMember(Name = "cityId")]
      [Display(Description = "Город")]
      public virtual long? CityId { get; set; }
      
      
      /// <summary>
      /// Город
      /// </summary>
      [Display(Description = "Город")]
      [DataMember(Name = "city", EmitDefaultValue = false)]
      public virtual City City { get; set; }
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "География мероприятия")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [InverseProperty("EventGeographys")]
      [Display(Description = "География мероприятия")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [ForeignKey("Product")]
      [DataMember(Name = "productId")]
      [Display(Description = "География мероприятия")]
      public virtual long? ProductId { get; set; }
      
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [InverseProperty("EventGeographys")]
      [Display(Description = "География мероприятия")]
      [DataMember(Name = "product", EmitDefaultValue = false)]
      public virtual Product Product { get; set; }
      
      /// <summary>
      /// Ссылка на самого себя
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Ссылка на самого себя")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Ссылка на самого себя
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Ссылка на самого себя")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual EventGeography Parent { get; set; }

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