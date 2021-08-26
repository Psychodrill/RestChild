// File:    MonitoringSmallLeisureInfo.cs
// Purpose: Definition of Class MonitoringSmallLeisureInfo

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Форма сведения о малых формах досуга
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringSmallLeisureInfo")]
   public partial class MonitoringSmallLeisureInfo : IEntityBase
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
      /// Месяц
      /// </summary>
      [Display(Description = "Месяц")]
      [Required(ErrorMessage = "\"Месяц\" должно быть заполнено")]
      [DataMember(Name = "month", EmitDefaultValue = false)]
      public virtual int Month { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Сведения о малых формах досуга (Год компании)
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")][Display(Description = "Мониторинг. Сведения о малых формах досуга (Год компании)")]
      public virtual long? YearOfRestId { get; set; }
      /// <summary>
      /// Мониторинг. Сведения о малых формах досуга (Год компании)
      /// </summary>
      [Display(Description = "Мониторинг. Сведения о малых формах досуга (Год компании)")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Мониторинг. Форма сведений о малых формах досуга (Статус)
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Мониторинг. Форма сведений о малых формах досуга (Статус)")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Форма сведений о малых формах досуга (Статус)
      /// </summary>
      [Display(Description = "Мониторинг. Форма сведений о малых формах досуга (Статус)")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Мониторинг. Форма сведений о малых формах досуга (связь с ГБУ)
      /// </summary>
      [InverseProperty("MonitoringSmallLeisureInfo")]
      [DataMember(Name = "smallLeisureInfoGBUs", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringSmallLeisureInfoGBU> SmallLeisureInfoGBUs { get; set; }
      
      /// <summary>
      /// Мониторинг. Форма сведения о малых формах досуга (связь с организацией)
      /// </summary>
      [ForeignKey("Organisation")]
      [DataMember(Name = "organisationId")]
      [Display(Description = "Мониторинг. Форма сведения о малых формах досуга (связь с организацией)")]
      public virtual long? OrganisationId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Форма сведения о малых формах досуга (связь с организацией)
      /// </summary>
      [Display(Description = "Мониторинг. Форма сведения о малых формах досуга (связь с организацией)")]
      [DataMember(Name = "organisation", EmitDefaultValue = false)]
      public virtual Organization Organisation { get; set; }
      
      /// <summary>
      /// Документы сведений о малых формах
      /// </summary>
      [ForeignKey("LinkToFiles")]
      [DataMember(Name = "linkToFilesId")]
      [Display(Description = "Документы сведений о малых формах")]
      public virtual long? LinkToFilesId { get; set; }
      
      
      /// <summary>
      /// Документы сведений о малых формах
      /// </summary>
      [Display(Description = "Документы сведений о малых формах")]
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