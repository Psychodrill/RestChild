// File:    Product.cs
// Purpose: Definition of Class Product

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Продукт
   /// </summary>
   [Serializable]
   [DataContract(Name = "product")]
   public partial class Product : IEntityBase
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
      /// Список продуктов
      /// </summary>
      [InverseProperty("Product")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tours { get; set; }
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [InverseProperty("Product")]
      [DataMember(Name = "eventGeographys", EmitDefaultValue = false)]
      public virtual ICollection<EventGeography> EventGeographys { get; set; }
      
      /// <summary>
      /// Основное место отдыха
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "Основное место отдыха")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// Основное место отдыха
      /// </summary>
      [Display(Description = "Основное место отдыха")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
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
      /// Файлы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")]
      [Display(Description = "Файлы")]
      public virtual long? LinkToFileId { get; set; }
      
      
      /// <summary>
      /// Файлы
      /// </summary>
      [Display(Description = "Файлы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }

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