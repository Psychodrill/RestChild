// File:    RequestService.cs
// Purpose: Definition of Class RequestService

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Выбранная услуга
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestService")]
   public partial class RequestService : IEntityBase
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
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [InverseProperty("Service")]
      [DataMember(Name = "peoples", EmitDefaultValue = false)]
      public virtual ICollection<AddonServicesLink> Peoples { get; set; }
      
      /// <summary>
      /// Связь в услугах по иеархии
      /// </summary>
      [InverseProperty("Parent")]
      [DataMember(Name = "childs", EmitDefaultValue = false)]
      public virtual ICollection<RequestService> Childs { get; set; }
      
      /// <summary>
      /// Выбранные услуги в заявке
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Выбранные услуги в заявке")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Выбранные услуги в заявке
      /// </summary>
      [InverseProperty("RequestServices")]
      [Display(Description = "Выбранные услуги в заявке")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Услуга
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "Услуга")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// Услуга
      /// </summary>
      [Display(Description = "Услуга")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }
      
      /// <summary>
      /// Рейс
      /// </summary>
      [ForeignKey("DirectoryFlights")]
      [DataMember(Name = "directoryFlightsId")]
      [Display(Description = "Рейс")]
      public virtual long? DirectoryFlightsId { get; set; }
      
      
      /// <summary>
      /// Рейс
      /// </summary>
      [Display(Description = "Рейс")]
      [DataMember(Name = "directoryFlights", EmitDefaultValue = false)]
      public virtual DirectoryFlights DirectoryFlights { get; set; }
      
      /// <summary>
      /// Связь в услугах по иеархии
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Связь в услугах по иеархии")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Связь в услугах по иеархии
      /// </summary>
      [InverseProperty("Childs")]
      [Display(Description = "Связь в услугах по иеархии")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual RequestService Parent { get; set; }

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