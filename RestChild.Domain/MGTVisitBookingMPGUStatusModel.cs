// File:    MGTVisitBookingMPGUStatusModel.cs
// Purpose: Definition of Class MGTVisitBookingMPGUStatusModel

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Запись на визит в МГТ Статусная модель МПГУ
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTVisitBookingMPGUStatusModel")]
   public partial class MGTVisitBookingMPGUStatusModel : IEntityBase
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
      /// Порядок отправки статусов
      /// </summary>
      [Display(Description = "Порядок отправки статусов")]
      [Required(ErrorMessage = "\"Порядок отправки статусов\" должно быть заполнено")]
      [DataMember(Name = "order", EmitDefaultValue = false)]
      public virtual int Order { get; set; }
      
      
      /// <summary>
      /// Статус (от) записи на визит в МГТ
      /// </summary>
      [ForeignKey("StatusFrom")]
      [DataMember(Name = "statusFromId")][Display(Description = "Статус (от) записи на визит в МГТ")]
      public virtual long? StatusFromId { get; set; }
      /// <summary>
      /// Статус (от) записи на визит в МГТ
      /// </summary>
      [Display(Description = "Статус (от) записи на визит в МГТ")]
      [DataMember(Name = "statusFrom", EmitDefaultValue = false)]
      public virtual MGTVisitBookingStatus StatusFrom { get; set; }
      
      /// <summary>
      /// Статус (в) записи на визит в МГТ
      /// </summary>
      [ForeignKey("StatusTo")]
      [DataMember(Name = "statusToId")][Display(Description = "Статус (в) записи на визит в МГТ")]
      public virtual long? StatusToId { get; set; }
      /// <summary>
      /// Статус (в) записи на визит в МГТ
      /// </summary>
      [Display(Description = "Статус (в) записи на визит в МГТ")]
      [DataMember(Name = "statusTo", EmitDefaultValue = false)]
      public virtual MGTVisitBookingStatus StatusTo { get; set; }
      
      /// <summary>
      /// Переход статуса от
      /// </summary>
      [ForeignKey("MPGUStatus")]
      [DataMember(Name = "mPGUStatusId")]
      [Display(Description = "Переход статуса от")]
      public virtual long? MPGUStatusId { get; set; }
      
      
      /// <summary>
      /// Переход статуса от
      /// </summary>
      [Display(Description = "Переход статуса от")]
      [DataMember(Name = "mPGUStatus", EmitDefaultValue = false)]
      public virtual MGTVisitBookingMPGUStatus MPGUStatus { get; set; }

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