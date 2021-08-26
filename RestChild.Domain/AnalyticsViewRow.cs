// File:    AnalyticsViewRow.cs
// Purpose: Definition of Class AnalyticsViewRow

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Строка для базовой аналитики
   /// </summary>
   [Serializable]
   [DataContract(Name = "analyticsViewRow")]
   public partial class AnalyticsViewRow : IEntityBase
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
      /// В день
      /// </summary>
      [Display(Description = "В день")]
      [Required(ErrorMessage = "\"В день\" должно быть заполнено")]
      [DataMember(Name = "byDay", EmitDefaultValue = false)]
      public virtual int ByDay { get; set; }
      
      /// <summary>
      /// В час
      /// </summary>
      [Display(Description = "В час")]
      [Required(ErrorMessage = "\"В час\" должно быть заполнено")]
      [DataMember(Name = "byHour", EmitDefaultValue = false)]
      public virtual int ByHour { get; set; }
      
      /// <summary>
      /// В неделю
      /// </summary>
      [Display(Description = "В неделю")]
      [Required(ErrorMessage = "\"В неделю\" должно быть заполнено")]
      [DataMember(Name = "byWeek", EmitDefaultValue = false)]
      public virtual int ByWeek { get; set; }
      
      /// <summary>
      /// Всего
      /// </summary>
      [Display(Description = "Всего")]
      [Required(ErrorMessage = "\"Всего\" должно быть заполнено")]
      [DataMember(Name = "total", EmitDefaultValue = false)]
      public virtual int Total { get; set; }
      
      /// <summary>
      /// В день(цвет)
      /// </summary>
      [Display(Description = "В день(цвет)")]
      [MaxLength(1000, ErrorMessage = "\"В день(цвет)\" не может быть больше 1000 символов")]
      [DataMember(Name = "byDayColor", EmitDefaultValue = false)]
      public virtual string ByDayColor { get; set; }
      
      /// <summary>
      /// В час(цвет)
      /// </summary>
      [Display(Description = "В час(цвет)")]
      [MaxLength(1000, ErrorMessage = "\"В час(цвет)\" не может быть больше 1000 символов")]
      [DataMember(Name = "byHourColor", EmitDefaultValue = false)]
      public virtual string ByHourColor { get; set; }
      
      /// <summary>
      /// В неделю(цвет)
      /// </summary>
      [Display(Description = "В неделю(цвет)")]
      [MaxLength(1000, ErrorMessage = "\"В неделю(цвет)\" не может быть больше 1000 символов")]
      [DataMember(Name = "byWeekColor", EmitDefaultValue = false)]
      public virtual string ByWeekColor { get; set; }
      
      /// <summary>
      /// Всего(цвет)
      /// </summary>
      [Display(Description = "Всего(цвет)")]
      [MaxLength(1000, ErrorMessage = "\"Всего(цвет)\" не может быть больше 1000 символов")]
      [DataMember(Name = "totalColor", EmitDefaultValue = false)]
      public virtual string TotalColor { get; set; }
      
      /// <summary>
      /// В день 2 показатель в ячейке
      /// </summary>
      [Display(Description = "В день 2 показатель в ячейке")]
      [DataMember(Name = "byDay2", EmitDefaultValue = false)]
      public virtual int? ByDay2 { get; set; }
      
      /// <summary>
      /// В час 2 показатель в ячейке
      /// </summary>
      [Display(Description = "В час 2 показатель в ячейке")]
      [DataMember(Name = "byHour2", EmitDefaultValue = false)]
      public virtual int? ByHour2 { get; set; }
      
      /// <summary>
      /// В неделю 2 показатель в ячейке
      /// </summary>
      [Display(Description = "В неделю 2 показатель в ячейке")]
      [DataMember(Name = "byWeek2", EmitDefaultValue = false)]
      public virtual int? ByWeek2 { get; set; }
      
      /// <summary>
      /// Всего 2 показатель в ячейке
      /// </summary>
      [Display(Description = "Всего 2 показатель в ячейке")]
      [DataMember(Name = "total2", EmitDefaultValue = false)]
      public virtual int? Total2 { get; set; }
      
      /// <summary>
      /// День1
      /// </summary>
      [Display(Description = "День1")]
      [DataMember(Name = "day1", EmitDefaultValue = false)]
      public virtual int? Day1 { get; set; }
      
      /// <summary>
      /// День2
      /// </summary>
      [Display(Description = "День2")]
      [DataMember(Name = "day2", EmitDefaultValue = false)]
      public virtual int? Day2 { get; set; }
      
      /// <summary>
      /// День3
      /// </summary>
      [Display(Description = "День3")]
      [DataMember(Name = "day3", EmitDefaultValue = false)]
      public virtual int? Day3 { get; set; }
      
      /// <summary>
      /// День4
      /// </summary>
      [Display(Description = "День4")]
      [DataMember(Name = "day4", EmitDefaultValue = false)]
      public virtual int? Day4 { get; set; }
      
      /// <summary>
      /// День5
      /// </summary>
      [Display(Description = "День5")]
      [DataMember(Name = "day5", EmitDefaultValue = false)]
      public virtual int? Day5 { get; set; }
      
      /// <summary>
      /// День6
      /// </summary>
      [Display(Description = "День6")]
      [DataMember(Name = "day6", EmitDefaultValue = false)]
      public virtual int? Day6 { get; set; }
      
      /// <summary>
      /// Дата День1
      /// </summary>
      [Display(Description = "Дата День1")]
      [DataMember(Name = "dataDay1", EmitDefaultValue = false)]
      public virtual DateTime? DataDay1 { get; set; }
      
      /// <summary>
      /// Дата День2
      /// </summary>
      [Display(Description = "Дата День2")]
      [DataMember(Name = "dataDay2", EmitDefaultValue = false)]
      public virtual DateTime? DataDay2 { get; set; }
      
      /// <summary>
      /// Дата День3
      /// </summary>
      [Display(Description = "Дата День3")]
      [DataMember(Name = "dataDay3", EmitDefaultValue = false)]
      public virtual DateTime? DataDay3 { get; set; }
      
      /// <summary>
      /// Дата День4
      /// </summary>
      [Display(Description = "Дата День4")]
      [DataMember(Name = "dataDay4", EmitDefaultValue = false)]
      public virtual DateTime? DataDay4 { get; set; }
      
      /// <summary>
      /// Дата День5
      /// </summary>
      [Display(Description = "Дата День5")]
      [DataMember(Name = "dataDay5", EmitDefaultValue = false)]
      public virtual DateTime? DataDay5 { get; set; }
      
      /// <summary>
      /// Дата День6
      /// </summary>
      [Display(Description = "Дата День6")]
      [DataMember(Name = "dataDay6", EmitDefaultValue = false)]
      public virtual DateTime? DataDay6 { get; set; }
      
      
      /// <summary>
      /// Тип представления
      /// </summary>
      [ForeignKey("AnalyticsViewRowType")]
      [DataMember(Name = "analyticsViewRowTypeId")]
      [Display(Description = "Тип представления")]
      public virtual long? AnalyticsViewRowTypeId { get; set; }
      
      
      /// <summary>
      /// Тип представления
      /// </summary>
      [Display(Description = "Тип представления")]
      [DataMember(Name = "analyticsViewRowType", EmitDefaultValue = false)]
      public virtual AnalyticsViewRowType AnalyticsViewRowType { get; set; }

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