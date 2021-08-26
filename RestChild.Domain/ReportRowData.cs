// File:    ReportRowData.cs
// Purpose: Definition of Class ReportRowData

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Данные строки
   /// </summary>
   [Serializable]
   [DataContract(Name = "reportRowData")]
   public partial class ReportRowData : IEntityBase
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
      /// Класс
      /// </summary>
      [Display(Description = "Класс")]
      [MaxLength(1000, ErrorMessage = "\"Класс\" не может быть больше 1000 символов")]
      [DataMember(Name = "cssClass", EmitDefaultValue = false)]
      public virtual string CssClass { get; set; }
      
      /// <summary>
      /// Значение
      /// </summary>
      [Display(Description = "Значение")]
      [MaxLength(1000, ErrorMessage = "\"Значение\" не может быть больше 1000 символов")]
      [DataMember(Name = "value", EmitDefaultValue = false)]
      public virtual string Value { get; set; }
      
      /// <summary>
      /// Гиперссылка
      /// </summary>
      [Display(Description = "Гиперссылка")]
      [MaxLength(1000, ErrorMessage = "\"Гиперссылка\" не может быть больше 1000 символов")]
      [DataMember(Name = "url", EmitDefaultValue = false)]
      public virtual string Url { get; set; }
      
      
      /// <summary>
      /// Данные строки
      /// </summary>
      [ForeignKey("Row")]
      [DataMember(Name = "rowId")]
      [Display(Description = "Данные строки")]
      public virtual long? RowId { get; set; }
      
      
      /// <summary>
      /// Данные строки
      /// </summary>
      [InverseProperty("RowData")]
      [Display(Description = "Данные строки")]
      [DataMember(Name = "row", EmitDefaultValue = false)]
      public virtual ReportTableRow Row { get; set; }
      
      /// <summary>
      /// Тип столбца
      /// </summary>
      [ForeignKey("ReportTableHead")]
      [DataMember(Name = "reportTableHeadId")]
      [Display(Description = "Тип столбца")]
      public virtual long? ReportTableHeadId { get; set; }
      
      
      /// <summary>
      /// Тип столбца
      /// </summary>
      [Display(Description = "Тип столбца")]
      [DataMember(Name = "reportTableHead", EmitDefaultValue = false)]
      public virtual ReportTableHead ReportTableHead { get; set; }

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