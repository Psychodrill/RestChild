// File:    MGTVisitBookingStatus.cs
// Purpose: Definition of Class MGTVisitBookingStatus

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// МГТСтатусы
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTVisitBookingStatus")]
   public partial class MGTVisitBookingStatus : IEntityBase
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
      /// МГТ Код
      /// </summary>
      [Display(Description = "МГТ Код")]
      [MaxLength(1000, ErrorMessage = "\"МГТ Код\" не может быть больше 1000 символов")]
      [DataMember(Name = "mGTCode", EmitDefaultValue = false)]
      public virtual string MGTCode { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [MaxLength(1000, ErrorMessage = "\"Описание\" не может быть больше 1000 символов")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual string Description { get; set; }
      
      
      /// <summary>
      /// Связь статуса с предком
      /// </summary>
      [ForeignKey("Parrent")]
      [DataMember(Name = "parrentId")]
      [Display(Description = "Связь статуса с предком")]
      public virtual long? ParrentId { get; set; }
      
      
      /// <summary>
      /// Связь статуса с предком
      /// </summary>
      [Display(Description = "Связь статуса с предком")]
      [DataMember(Name = "parrent", EmitDefaultValue = false)]
      public virtual MGTVisitBookingStatus Parrent { get; set; }

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