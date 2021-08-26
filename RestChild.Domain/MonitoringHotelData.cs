// File:    MonitoringHotelData.cs
// Purpose: Definition of Class MonitoringHotelData

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Сведения об организации отдыха и оздоравления
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringHotelData")]
   public partial class MonitoringHotelData : IEntityBase
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
      /// Мониторинг. Связь организации отдыха с объектом мониторинга
      /// </summary>
      [ForeignKey("ChildrenNumberInformation")]
      [DataMember(Name = "childrenNumberInformationId")][Display(Description = "Мониторинг. Связь организации отдыха с объектом мониторинга")]
      public virtual long? ChildrenNumberInformationId { get; set; }
      /// <summary>
      /// Мониторинг. Связь организации отдыха с объектом мониторинга
      /// </summary>
      [InverseProperty("HotelDatas")]
      [Display(Description = "Мониторинг. Связь организации отдыха с объектом мониторинга")]
      [DataMember(Name = "childrenNumberInformation", EmitDefaultValue = false)]
      public virtual MonitoringChildrenNumberInformation ChildrenNumberInformation { get; set; }
      
      /// <summary>
      /// Мониторинг. Отель формы сведений о численности детей
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")][Display(Description = "Мониторинг. Отель формы сведений о численности детей")]
      public virtual long? HotelId { get; set; }
      /// <summary>
      /// Мониторинг. Отель формы сведений о численности детей
      /// </summary>
      [InverseProperty("HotelDatas")]
      [Display(Description = "Мониторинг. Отель формы сведений о численности детей")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual MonitoringHotel Hotel { get; set; }
      
      /// <summary>
      /// Мониторинг. Связь тура с организацией отдыха
      /// </summary>
      [InverseProperty("HotelData")]
      [DataMember(Name = "tourDatas", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringTourData> TourDatas { get; set; }

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