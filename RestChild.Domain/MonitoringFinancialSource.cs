// File:    MonitoringFinancialSource.cs
// Purpose: Definition of Class MonitoringFinancialSource

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Источник финансирования
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringFinancialSource")]
   public partial class MonitoringFinancialSource : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активно
      /// </summary>
      [Display(Description = "Активно")]
      [Required(ErrorMessage = "\"Активно\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Код
      /// </summary>
      [Display(Description = "Код")]
      [MaxLength(1000, ErrorMessage = "\"Код\" не может быть больше 1000 символов")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Показывать в форме
      /// </summary>
      [Display(Description = "Показывать в форме")]
      [Required(ErrorMessage = "\"Показывать в форме\" должно быть заполнено")]
      [DataMember(Name = "showInForm", EmitDefaultValue = false)]
      public virtual bool ShowInForm { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Связь данных о финансировании с источником финансирования
      /// </summary>
      [InverseProperty("MonitoringFinancialSource")]
      [DataMember(Name = "monitoringFinancialDatas", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringFinancialData> MonitoringFinancialDatas { get; set; }
      
      /// <summary>
      /// Мониторинг. Источник финансирования (древовидная связь)
      /// </summary>
      [InverseProperty("Parrent")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringFinancialSource> Children { get; set; }
      
      /// <summary>
      /// Мониторинг. Источник финансирования (древовидная связь)
      /// </summary>
      [ForeignKey("Parrent")]
      [DataMember(Name = "parrentId")]
      [Display(Description = "Мониторинг. Источник финансирования (древовидная связь)")]
      public virtual long? ParrentId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Источник финансирования (древовидная связь)
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Мониторинг. Источник финансирования (древовидная связь)")]
      [DataMember(Name = "parrent", EmitDefaultValue = false)]
      public virtual MonitoringFinancialSource Parrent { get; set; }

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