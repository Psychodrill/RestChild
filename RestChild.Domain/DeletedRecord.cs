// File:    DeletedRecord.cs
// Purpose: Definition of Class DeletedRecord

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Удаленные записи
   /// </summary>
   [Serializable]
   [DataContract(Name = "deletedRecord")]
   public partial class DeletedRecord : IEntityBase
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
      /// Класс
      /// </summary>
      [Display(Description = "Класс")]
      [MaxLength(1000, ErrorMessage = "\"Класс\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Класс\" не может быть пустым")]
      [DataMember(Name = "className", EmitDefaultValue = false)]
      public virtual string ClassName { get; set; }
      
      /// <summary>
      /// Идентификатор
      /// </summary>
      [Display(Description = "Идентификатор")]
      [Required(ErrorMessage = "\"Идентификатор\" должно быть заполнено")]
      [DataMember(Name = "uid", EmitDefaultValue = false)]
      public virtual long Uid { get; set; }

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