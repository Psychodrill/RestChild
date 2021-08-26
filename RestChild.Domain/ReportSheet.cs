// File:    ReportSheet.cs
// Purpose: Definition of Class ReportSheet

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тип отчета
   /// </summary>
   [Serializable]
   [DataContract(Name = "reportSheet")]
   public partial class ReportSheet : IEntityBase
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
      /// Код права
      /// </summary>
      [Display(Description = "Код права")]
      [Required(ErrorMessage = "\"Код права\" должно быть заполнено")]
      [DataMember(Name = "codeAccess", EmitDefaultValue = false)]
      public virtual Guid CodeAccess { get; set; }
      
      /// <summary>
      /// Наименование отчета
      /// </summary>
      [Display(Description = "Наименование отчета")]
      [MaxLength(1000, ErrorMessage = "\"Наименование отчета\" не может быть больше 1000 символов")]
      [DataMember(Name = "reportName", EmitDefaultValue = false)]
      public virtual string ReportName { get; set; }
      
      /// <summary>
      /// Порядок сортировки
      /// </summary>
      [Display(Description = "Порядок сортировки")]
      [Required(ErrorMessage = "\"Порядок сортировки\" должно быть заполнено")]
      [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
      public virtual long SortOrder { get; set; }
      
      
      /// <summary>
      /// Отчеты на странице
      /// </summary>
      [InverseProperty("ReportSheet")]
      [DataMember(Name = "reportTables", EmitDefaultValue = false)]
      public virtual ICollection<ReportTable> ReportTables { get; set; }

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