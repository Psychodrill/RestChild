// File:    ReportTableHead.cs
// Purpose: Definition of Class ReportTableHead

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Настройки столбцов
   /// </summary>
   [Serializable]
   [DataContract(Name = "reportTableHead")]
   public partial class ReportTableHead : IEntityBase
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
      /// Класс заголовка
      /// </summary>
      [Display(Description = "Класс заголовка")]
      [MaxLength(1000, ErrorMessage = "\"Класс заголовка\" не может быть больше 1000 символов")]
      [DataMember(Name = "cssClass", EmitDefaultValue = false)]
      public virtual string CssClass { get; set; }
      
      /// <summary>
      /// Порядок
      /// </summary>
      [Display(Description = "Порядок")]
      [Required(ErrorMessage = "\"Порядок\" должно быть заполнено")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual int SortOrder { get; set; }
      
      /// <summary>
      /// Стиль
      /// </summary>
      [Display(Description = "Стиль")]
      [MaxLength(1000, ErrorMessage = "\"Стиль\" не может быть больше 1000 символов")]
      [DataMember(Name = "style", EmitDefaultValue = false)]
      public virtual string Style { get; set; }
      
      /// <summary>
      /// RowSpan
      /// </summary>
      [Display(Description = "RowSpan")]
      [DataMember(Name = "rowSpan", EmitDefaultValue = false)]
      public virtual int? RowSpan { get; set; }
      
      /// <summary>
      /// ColSpan
      /// </summary>
      [Display(Description = "ColSpan")]
      [DataMember(Name = "colSpan", EmitDefaultValue = false)]
      public virtual int? ColSpan { get; set; }
      
      /// <summary>
      /// RowIndex
      /// </summary>
      [Display(Description = "RowIndex")]
      [DataMember(Name = "rowIndex", EmitDefaultValue = false)]
      public virtual int? RowIndex { get; set; }
      
      
      /// <summary>
      /// Столбцы
      /// </summary>
      [ForeignKey("ReportTable")]
      [DataMember(Name = "reportTableId")]
      [Display(Description = "Столбцы")]
      public virtual long? ReportTableId { get; set; }
      
      
      /// <summary>
      /// Столбцы
      /// </summary>
      [InverseProperty("ReportTableHeads")]
      [Display(Description = "Столбцы")]
      [DataMember(Name = "reportTable", EmitDefaultValue = false)]
      public virtual ReportTable ReportTable { get; set; }

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