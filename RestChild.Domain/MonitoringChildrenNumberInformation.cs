// File:    MonitoringChildrenNumberInformation.cs
// Purpose: Definition of Class MonitoringChildrenNumberInformation

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Cведения о численности детей
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringChildrenNumberInformation")]
   public partial class MonitoringChildrenNumberInformation : IEntityBase
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
      /// История изменений сведеней о численности детей
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")][Display(Description = "История изменений сведеней о численности детей")]
      public virtual long? HistoryLinkId { get; set; }
      /// <summary>
      /// История изменений сведеней о численности детей
      /// </summary>
      [Display(Description = "История изменений сведеней о численности детей")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Мониторинг. Cведения о численности детей (Статус)
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Мониторинг. Cведения о численности детей (Статус)")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Cведения о численности детей (Статус)
      /// </summary>
      [Display(Description = "Мониторинг. Cведения о численности детей (Статус)")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Мониторинг. Cведения о численности детей (Участник мониторинга)
      /// </summary>
      [ForeignKey("Organisation")]
      [DataMember(Name = "organisationId")]
      [Display(Description = "Мониторинг. Cведения о численности детей (Участник мониторинга)")]
      public virtual long? OrganisationId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Cведения о численности детей (Участник мониторинга)
      /// </summary>
      [InverseProperty("MonitoringChildrenNumberInformations")]
      [Display(Description = "Мониторинг. Cведения о численности детей (Участник мониторинга)")]
      [DataMember(Name = "organisation", EmitDefaultValue = false)]
      public virtual Organization Organisation { get; set; }
      
      /// <summary>
      /// Мониторинг. Год формы сведений о численности детей
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Мониторинг. Год формы сведений о численности детей")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Год формы сведений о численности детей
      /// </summary>
      [Display(Description = "Мониторинг. Год формы сведений о численности детей")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Мониторинг. Связь организации отдыха с объектом мониторинга
      /// </summary>
      [InverseProperty("ChildrenNumberInformation")]
      [DataMember(Name = "hotelDatas", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringHotelData> HotelDatas { get; set; }
      
      /// <summary>
      /// Файлы сведений о численности детей
      /// </summary>
      [ForeignKey("LinkToFiles")]
      [DataMember(Name = "linkToFilesId")]
      [Display(Description = "Файлы сведений о численности детей")]
      public virtual long? LinkToFilesId { get; set; }
      
      
      /// <summary>
      /// Файлы сведений о численности детей
      /// </summary>
      [Display(Description = "Файлы сведений о численности детей")]
      [DataMember(Name = "linkToFiles", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFiles { get; set; }

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