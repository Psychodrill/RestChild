// File:    AccommodationChildren.cs
// Purpose: Definition of Class AccommodationChildren

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Размещение детей
   /// </summary>
   [Serializable]
   [DataContract(Name = "accommodationChildren")]
   public partial class AccommodationChildren : IEntityBase
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
      [Required(ErrorMessage = "\"Возраст с\" должно быть заполнено")]
      [DataMember(Name = "ageFrom", EmitDefaultValue = false)]
      public virtual int AgeFrom { get; set; }
      
      /// <summary>
      /// Возраст по
      /// </summary>
      [Display(Description = "Возраст по")]
      [Required(ErrorMessage = "\"Возраст по\" должно быть заполнено")]
      [DataMember(Name = "ageTo", EmitDefaultValue = false)]
      public virtual int AgeTo { get; set; }
      
      /// <summary>
      /// Количество детей
      /// </summary>
      [Display(Description = "Количество детей")]
      [Required(ErrorMessage = "\"Количество детей\" должно быть заполнено")]
      [DataMember(Name = "countChildren", EmitDefaultValue = false)]
      public virtual int CountChildren { get; set; }
      
      
      /// <summary>
      /// Размещение детей
      /// </summary>
      [ForeignKey("Accommodation")]
      [DataMember(Name = "accommodationId")]
      [Display(Description = "Размещение детей")]
      public virtual long? AccommodationId { get; set; }
      
      
      /// <summary>
      /// Размещение детей
      /// </summary>
      [InverseProperty("AccommodationChildren")]
      [Display(Description = "Размещение детей")]
      [DataMember(Name = "accommodation", EmitDefaultValue = false)]
      public virtual Accommodation Accommodation { get; set; }

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