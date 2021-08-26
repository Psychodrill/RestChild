// File:    ERLBenefitStatus.cs
// Purpose: Definition of Class ERLBenefitStatus

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// ЕРЛ статус загрузки льготы
   /// </summary>
   [Serializable]
   [DataContract(Name = "eRLBenefitStatus")]
   public partial class ERLBenefitStatus : IEntityBase
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
      /// Идентификатор льготы в ЕРЛ
      /// </summary>
      [Display(Description = "Идентификатор льготы в ЕРЛ")]
      [DataMember(Name = "benefitUid", EmitDefaultValue = false)]
      public virtual Guid? BenefitUid { get; set; }
      
      /// <summary>
      /// Идентификатор сообщения в ЕРЛ
      /// </summary>
      [Display(Description = "Идентификатор сообщения в ЕРЛ")]
      [DataMember(Name = "eRLMessageId", EmitDefaultValue = false)]
      public virtual Guid? ERLMessageId { get; set; }
      
      /// <summary>
      /// Принято в ЕРЛ без ошибок
      /// </summary>
      [Display(Description = "Принято в ЕРЛ без ошибок")]
      [Required(ErrorMessage = "\"Принято в ЕРЛ без ошибок\" должно быть заполнено")]
      [DataMember(Name = "eRLCommited", EmitDefaultValue = false)]
      public virtual bool ERLCommited { get; set; }
      
      /// <summary>
      /// Отправлено в потоке 2.4
      /// </summary>
      [Display(Description = "Отправлено в потоке 2.4")]
      [Required(ErrorMessage = "\"Отправлено в потоке 2.4\" должно быть заполнено")]
      [DataMember(Name = "queue24Sended", EmitDefaultValue = false)]
      public virtual bool Queue24Sended { get; set; }
      
      
      /// <summary>
      /// Связь статуса персоны в ЕРЛ с его льготами
      /// </summary>
      [ForeignKey("Person")]
      [DataMember(Name = "personId")][Display(Description = "Связь статуса персоны в ЕРЛ с его льготами")]
      public virtual long? PersonId { get; set; }
      /// <summary>
      /// Связь статуса персоны в ЕРЛ с его льготами
      /// </summary>
      [InverseProperty("Benefits")]
      [Display(Description = "Связь статуса персоны в ЕРЛ с его льготами")]
      [DataMember(Name = "person", EmitDefaultValue = false)]
      public virtual ERLPersonStatus Person { get; set; }
      
      /// <summary>
      /// Связь заявляени яо статусом в ЕРЛ
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Связь заявляени яо статусом в ЕРЛ")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Связь заявляени яо статусом в ЕРЛ
      /// </summary>
      [Display(Description = "Связь заявляени яо статусом в ЕРЛ")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }

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