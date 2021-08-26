// File:    TicketLink.cs
// Purpose: Definition of Class TicketLink

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Билет (ссылка на отдыхающего)
   /// </summary>
   [Serializable]
   [DataContract(Name = "ticketLink")]
   public partial class TicketLink : IEntityBase
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
      /// Стоимость
      /// </summary>
      [Display(Description = "Стоимость")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Себестоимость
      /// </summary>
      [Display(Description = "Себестоимость")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal? PriceInternal { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "commentary", EmitDefaultValue = false)]
      public virtual String Commentary { get; set; }
      
      /// <summary>
      /// Подтверждено
      /// </summary>
      [Display(Description = "Подтверждено")]
      [Required(ErrorMessage = "\"Подтверждено\" должно быть заполнено")]
      [DataMember(Name = "approved", EmitDefaultValue = false)]
      public virtual bool Approved { get; set; }
      
      /// <summary>
      /// Для формирования начисления
      /// </summary>
      [Display(Description = "Для формирования начисления")]
      [Required(ErrorMessage = "\"Для формирования начисления\" должно быть заполнено")]
      [DataMember(Name = "forCalculation", EmitDefaultValue = false)]
      public virtual bool ForCalculation { get; set; }
      
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Ребёнок")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [Display(Description = "Ребёнок")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Сопровождающий
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Сопровождающий")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Сопровождающий
      /// </summary>
      [Display(Description = "Сопровождающий")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [ForeignKey("Ticket")]
      [DataMember(Name = "ticketId")]
      [Display(Description = "Отдыхающие")]
      public virtual long? TicketId { get; set; }
      
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [InverseProperty("Persons")]
      [Display(Description = "Отдыхающие")]
      [DataMember(Name = "ticket", EmitDefaultValue = false)]
      public virtual Ticket Ticket { get; set; }

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