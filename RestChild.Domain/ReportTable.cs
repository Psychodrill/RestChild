// File:    ReportTable.cs
// Purpose: Definition of Class ReportTable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Таблица отчета
   /// </summary>
   [Serializable]
   [DataContract(Name = "reportTable")]
   public partial class ReportTable : IEntityBase
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
      /// Наименование таблиыцы
      /// </summary>
      [Display(Description = "Наименование таблиыцы")]
      [MaxLength(1000, ErrorMessage = "\"Наименование таблиыцы\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Класс таблицы
      /// </summary>
      [Display(Description = "Класс таблицы")]
      [MaxLength(1000, ErrorMessage = "\"Класс таблицы\" не может быть больше 1000 символов")]
      [DataMember(Name = "cssClass", EmitDefaultValue = false)]
      public virtual string CssClass { get; set; }
      
      /// <summary>
      /// Порядок в отчете
      /// </summary>
      [Display(Description = "Порядок в отчете")]
      [Required(ErrorMessage = "\"Порядок в отчете\" должно быть заполнено")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual long SortOrder { get; set; }
      
      
      /// <summary>
      /// Столбцы
      /// </summary>
      [InverseProperty("ReportTable")]
      [DataMember(Name = "reportTableHeads", EmitDefaultValue = false)]
      public virtual ICollection<ReportTableHead> ReportTableHeads { get; set; }
      
      /// <summary>
      /// Строки
      /// </summary>
      [InverseProperty("Table")]
      [DataMember(Name = "rows", EmitDefaultValue = false)]
      public virtual ICollection<ReportTableRow> Rows { get; set; }
      
      /// <summary>
      /// Отчеты на странице
      /// </summary>
      [ForeignKey("ReportSheet")]
      [DataMember(Name = "reportSheetId")]
      [Display(Description = "Отчеты на странице")]
      public virtual long? ReportSheetId { get; set; }
      
      
      /// <summary>
      /// Отчеты на странице
      /// </summary>
      [InverseProperty("ReportTables")]
      [Display(Description = "Отчеты на странице")]
      [DataMember(Name = "reportSheet", EmitDefaultValue = false)]
      public virtual ReportSheet ReportSheet { get; set; }

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