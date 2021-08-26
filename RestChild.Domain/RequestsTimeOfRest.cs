// File:    RequestsTimeOfRest.cs
// Purpose: Definition of Class RequestsTimeOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Время отдыха в заявлении
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestsTimeOfRest")]
   public partial class RequestsTimeOfRest : IEntityBase
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
      /// Связь времени отдыха с заявлением
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")][Display(Description = "Связь времени отдыха с заявлением")]
      public virtual long? TimeOfRestId { get; set; }
      /// <summary>
      /// Связь времени отдыха с заявлением
      /// </summary>
      [InverseProperty("Requests")]
      [Display(Description = "Связь времени отдыха с заявлением")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
      /// <summary>
      /// Связь заявления с временем отдыха
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Связь заявления с временем отдыха")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Связь заявления с временем отдыха
      /// </summary>
      [InverseProperty("TimesOfRest")]
      [Display(Description = "Связь заявления с временем отдыха")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }

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