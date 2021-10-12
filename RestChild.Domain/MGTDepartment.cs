// File:    MGTDepartment.cs
// Purpose: Definition of Class MGTDepartment

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// МГТ Отдел
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTDepartment")]
   public partial class MGTDepartment : IEntityBase
   {
      
      /// <summary>
      /// Идентификатор
      /// </summary>
      [Display(Description = "Идентификатор")]
      [Required(ErrorMessage = "\"Идентификатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "Id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [Required(ErrorMessage = "\"Название\" должно быть заполнено")]
      [DataMember(Name = "Name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "Description", EmitDefaultValue = false)]
      public virtual string Description { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
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
