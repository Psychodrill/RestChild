// File:    StatusAction.cs
// Purpose: Definition of Class StatusAction

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Действия в статусах
   /// </summary>
   [Serializable]
   [DataContract(Name = "statusAction")]
   public partial class StatusAction : IEntityBase
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
      /// Код действия
      /// </summary>
      [Display(Description = "Код действия")]
      [MaxLength(1000, ErrorMessage = "\"Код действия\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Код действия\" не может быть пустым")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Наименование операции
      /// </summary>
      [Display(Description = "Наименование операции")]
      [MaxLength(1000, ErrorMessage = "\"Наименование операции\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование операции\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Заявление в первую заявочную
      /// </summary>
      [Display(Description = "Заявление в первую заявочную")]
      [DataMember(Name = "isFirstCompany", EmitDefaultValue = false)]
      public virtual bool? IsFirstCompany { get; set; }
      
      /// <summary>
      /// Заявление на деньги
      /// </summary>
      [Display(Description = "Заявление на деньги")]
      [DataMember(Name = "requestOnMoney", EmitDefaultValue = false)]
      public virtual bool? RequestOnMoney { get; set; }
      
      
      /// <summary>
      /// Из какого статуса
      /// </summary>
      [InverseProperty("Action")]
      [DataMember(Name = "fromStatus", EmitDefaultValue = false)]
      public virtual ICollection<Status> FromStatus { get; set; }
      
      /// <summary>
      /// В какой статус
      /// </summary>
      [ForeignKey("ToStatus")]
      [DataMember(Name = "toStatusId")]
      [Display(Description = "В какой статус")]
      public virtual long? ToStatusId { get; set; }
      
      
      /// <summary>
      /// В какой статус
      /// </summary>
      [Display(Description = "В какой статус")]
      [DataMember(Name = "toStatus", EmitDefaultValue = false)]
      public virtual Status ToStatus { get; set; }

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