// File:    MonitoringFinancialInformation.cs
// Purpose: Definition of Class MonitoringFinancialInformation

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Cведения о финансировании
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringFinancialInformation")]
   public partial class MonitoringFinancialInformation : IEntityBase
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
      /// Мониторинг. Cведения о финансировании (Статус)
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Мониторинг. Cведения о финансировании (Статус)")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Cведения о финансировании (Статус)
      /// </summary>
      [Display(Description = "Мониторинг. Cведения о финансировании (Статус)")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Мониторинг. Cведения о финансировании (Участник мониторинга)
      /// </summary>
      [ForeignKey("Organisation")]
      [DataMember(Name = "organisationId")]
      [Display(Description = "Мониторинг. Cведения о финансировании (Участник мониторинга)")]
      public virtual long? OrganisationId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Cведения о финансировании (Участник мониторинга)
      /// </summary>
      [InverseProperty("MonitoringFinanceInformations")]
      [Display(Description = "Мониторинг. Cведения о финансировании (Участник мониторинга)")]
      [DataMember(Name = "organisation", EmitDefaultValue = false)]
      public virtual Organization Organisation { get; set; }
      
      /// <summary>
      /// Мониторинг. Связь информации о мониторинге с данными
      /// </summary>
      [InverseProperty("FinanceInformation")]
      [DataMember(Name = "finantialDatas", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringFinancialData> FinantialDatas { get; set; }
      
      /// <summary>
      /// История изменений сведений о финансировании
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История изменений сведений о финансировании")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История изменений сведений о финансировании
      /// </summary>
      [Display(Description = "История изменений сведений о финансировании")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Мониторинг. Сведения о финансировании (Год компании)
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Мониторинг. Сведения о финансировании (Год компании)")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Сведения о финансировании (Год компании)
      /// </summary>
      [Display(Description = "Мониторинг. Сведения о финансировании (Год компании)")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Документы сведений о финансировании
      /// </summary>
      [ForeignKey("LinkToFiles")]
      [DataMember(Name = "linkToFilesId")]
      [Display(Description = "Документы сведений о финансировании")]
      public virtual long? LinkToFilesId { get; set; }
      
      
      /// <summary>
      /// Документы сведений о финансировании
      /// </summary>
      [Display(Description = "Документы сведений о финансировании")]
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
