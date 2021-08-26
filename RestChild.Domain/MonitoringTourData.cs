// File:    MonitoringTourData.cs
// Purpose: Definition of Class MonitoringTourData

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Сведения о туре в организации отдыха и оздоравления
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringTourData")]
   public partial class MonitoringTourData : IEntityBase
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
      /// Дата заезда
      /// </summary>
      [Display(Description = "Дата заезда")]
      [DataMember(Name = "dateIn", EmitDefaultValue = false)]
      public virtual DateTime? DateIn { get; set; }
      
      /// <summary>
      /// Дата отъезда
      /// </summary>
      [Display(Description = "Дата отъезда")]
      [DataMember(Name = "dateOut", EmitDefaultValue = false)]
      public virtual DateTime? DateOut { get; set; }
      
      /// <summary>
      /// Плановое кол-во детей
      /// </summary>
      [Display(Description = "Плановое кол-во детей")]
      [DataMember(Name = "planChildrenCount", EmitDefaultValue = false)]
      public virtual int? PlanChildrenCount { get; set; }
      
      /// <summary>
      /// Кол-во отдохнувших
      /// </summary>
      [Display(Description = "Кол-во отдохнувших")]
      [DataMember(Name = "factChildrenCount", EmitDefaultValue = false)]
      public virtual int? FactChildrenCount { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Связь тура с организацией отдыха
      /// </summary>
      [ForeignKey("HotelData")]
      [DataMember(Name = "hotelDataId")]
      [Display(Description = "Мониторинг. Связь тура с организацией отдыха")]
      public virtual long? HotelDataId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Связь тура с организацией отдыха
      /// </summary>
      [InverseProperty("TourDatas")]
      [Display(Description = "Мониторинг. Связь тура с организацией отдыха")]
      [DataMember(Name = "hotelData", EmitDefaultValue = false)]
      public virtual MonitoringHotelData HotelData { get; set; }

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