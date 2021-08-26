// File:    ReportTableRow.cs
// Purpose: Definition of Class ReportTableRow

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Строки
   /// </summary>
   [Serializable]
   [DataContract(Name = "reportTableRow")]
   public partial class ReportTableRow : IEntityBase
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
      /// Наименование строки
      /// </summary>
      [Display(Description = "Наименование строки")]
      [MaxLength(1000, ErrorMessage = "\"Наименование строки\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Класс строки
      /// </summary>
      [Display(Description = "Класс строки")]
      [MaxLength(1000, ErrorMessage = "\"Класс строки\" не может быть больше 1000 символов")]
      [DataMember(Name = "cssClass", EmitDefaultValue = false)]
      public virtual string CssClass { get; set; }
      
      /// <summary>
      /// Стиль строки
      /// </summary>
      [Display(Description = "Стиль строки")]
      [MaxLength(1000, ErrorMessage = "\"Стиль строки\" не может быть больше 1000 символов")]
      [DataMember(Name = "style", EmitDefaultValue = false)]
      public virtual string Style { get; set; }
      
      /// <summary>
      /// Порядок
      /// </summary>
      [Display(Description = "Порядок")]
      [Required(ErrorMessage = "\"Порядок\" должно быть заполнено")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual int SortOrder { get; set; }
      
      
      /// <summary>
      /// Данные строки
      /// </summary>
      [InverseProperty("Row")]
      [DataMember(Name = "rowData", EmitDefaultValue = false)]
      public virtual ICollection<ReportRowData> RowData { get; set; }
      
      /// <summary>
      /// Строки
      /// </summary>
      [ForeignKey("Table")]
      [DataMember(Name = "tableId")]
      [Display(Description = "Строки")]
      public virtual long? TableId { get; set; }
      
      
      /// <summary>
      /// Строки
      /// </summary>
      [InverseProperty("Rows")]
      [Display(Description = "Строки")]
      [DataMember(Name = "table", EmitDefaultValue = false)]
      public virtual ReportTable Table { get; set; }

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