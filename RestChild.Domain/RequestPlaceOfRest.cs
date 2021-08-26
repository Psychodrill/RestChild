// File:    RequestPlaceOfRest.cs
// Purpose: Definition of Class RequestPlaceOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Место отдыха в заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestPlaceOfRest")]
   public partial class RequestPlaceOfRest : IEntityBase
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
      /// Порядок
      /// </summary>
      [Display(Description = "Порядок")]
      [Required(ErrorMessage = "\"Порядок\" должно быть заполнено")]
      [DataMember(Name = "order", EmitDefaultValue = false)]
      public virtual int Order { get; set; }
      
      
      /// <summary>
      /// Связь заявления с местом отдыха
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Связь заявления с местом отдыха")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Связь заявления с местом отдыха
      /// </summary>
      [InverseProperty("PlacesOfRest")]
      [Display(Description = "Связь заявления с местом отдыха")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Связь места отдыха с заявлением
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")]
      [Display(Description = "Связь места отдыха с заявлением")]
      public virtual long? PlaceOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь места отдыха с заявлением
      /// </summary>
      [InverseProperty("Requests")]
      [Display(Description = "Связь места отдыха с заявлением")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }

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