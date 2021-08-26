// File:    MonitoringFinancialData.cs
// Purpose: Definition of Class MonitoringFinancialData

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Данные о финансировании
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringFinancialData")]
   public partial class MonitoringFinancialData : IEntityBase
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
      /// План
      /// </summary>
      [Display(Description = "План")]
      [DataMember(Name = "plan", EmitDefaultValue = false)]
      public virtual decimal? Plan { get; set; }
      
      /// <summary>
      /// Январь
      /// </summary>
      [Display(Description = "Январь")]
      [DataMember(Name = "jan", EmitDefaultValue = false)]
      public virtual decimal? Jan { get; set; }
      
      /// <summary>
      /// Февраль
      /// </summary>
      [Display(Description = "Февраль")]
      [DataMember(Name = "feb", EmitDefaultValue = false)]
      public virtual decimal? Feb { get; set; }
      
      /// <summary>
      /// Март
      /// </summary>
      [Display(Description = "Март")]
      [DataMember(Name = "mar", EmitDefaultValue = false)]
      public virtual decimal? Mar { get; set; }
      
      /// <summary>
      /// Апрель
      /// </summary>
      [Display(Description = "Апрель")]
      [DataMember(Name = "apr", EmitDefaultValue = false)]
      public virtual decimal? Apr { get; set; }
      
      /// <summary>
      /// Май
      /// </summary>
      [Display(Description = "Май")]
      [DataMember(Name = "may", EmitDefaultValue = false)]
      public virtual decimal? May { get; set; }
      
      /// <summary>
      /// Июнь
      /// </summary>
      [Display(Description = "Июнь")]
      [DataMember(Name = "jun", EmitDefaultValue = false)]
      public virtual decimal? Jun { get; set; }
      
      /// <summary>
      /// Июль
      /// </summary>
      [Display(Description = "Июль")]
      [DataMember(Name = "jul", EmitDefaultValue = false)]
      public virtual decimal? Jul { get; set; }
      
      /// <summary>
      /// Август
      /// </summary>
      [Display(Description = "Август")]
      [DataMember(Name = "aug", EmitDefaultValue = false)]
      public virtual decimal? Aug { get; set; }
      
      /// <summary>
      /// Сентябрь
      /// </summary>
      [Display(Description = "Сентябрь")]
      [DataMember(Name = "sep", EmitDefaultValue = false)]
      public virtual decimal? Sep { get; set; }
      
      /// <summary>
      /// Октябрь
      /// </summary>
      [Display(Description = "Октябрь")]
      [DataMember(Name = "oct", EmitDefaultValue = false)]
      public virtual decimal? Oct { get; set; }
      
      /// <summary>
      /// Ноябрь
      /// </summary>
      [Display(Description = "Ноябрь")]
      [DataMember(Name = "nov", EmitDefaultValue = false)]
      public virtual decimal? Nov { get; set; }
      
      /// <summary>
      /// Декабрь
      /// </summary>
      [Display(Description = "Декабрь")]
      [DataMember(Name = "dec", EmitDefaultValue = false)]
      public virtual decimal? Dec { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      /// <summary>
      /// Формула
      /// </summary>
      [Display(Description = "Формула")]
      [MaxLength(1000, ErrorMessage = "\"Формула\" не может быть больше 1000 символов")]
      [DataMember(Name = "formula", EmitDefaultValue = false)]
      public virtual string Formula { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Связь информации о мониторинге с данными
      /// </summary>
      [ForeignKey("FinanceInformation")]
      [DataMember(Name = "financeInformationId")][Display(Description = "Мониторинг. Связь информации о мониторинге с данными")]
      public virtual long? FinanceInformationId { get; set; }
      /// <summary>
      /// Мониторинг. Связь информации о мониторинге с данными
      /// </summary>
      [InverseProperty("FinantialDatas")]
      [Display(Description = "Мониторинг. Связь информации о мониторинге с данными")]
      [DataMember(Name = "financeInformation", EmitDefaultValue = false)]
      public virtual MonitoringFinancialInformation FinanceInformation { get; set; }
      
      /// <summary>
      /// Мониторинг. Связь данных о финансировании с источником финансирования
      /// </summary>
      [ForeignKey("MonitoringFinancialSource")]
      [DataMember(Name = "monitoringFinancialSourceId")]
      [Display(Description = "Мониторинг. Связь данных о финансировании с источником финансирования")]
      public virtual long? MonitoringFinancialSourceId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Связь данных о финансировании с источником финансирования
      /// </summary>
      [InverseProperty("MonitoringFinancialDatas")]
      [Display(Description = "Мониторинг. Связь данных о финансировании с источником финансирования")]
      [DataMember(Name = "monitoringFinancialSource", EmitDefaultValue = false)]
      public virtual MonitoringFinancialSource MonitoringFinancialSource { get; set; }

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