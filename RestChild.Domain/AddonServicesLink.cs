// File:    AddonServicesLink.cs
// Purpose: Definition of Class AddonServicesLink

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Связь с услугами
   /// </summary>
   [Serializable]
   [DataContract(Name = "addonServicesLink")]
   public partial class AddonServicesLink : IEntityBase
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
      /// Количество услуг
      /// </summary>
      [Display(Description = "Количество услуг")]
      [DataMember(Name = "countService", EmitDefaultValue = false)]
      public virtual decimal? CountService { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime? DateCreate { get; set; }
      
      /// <summary>
      /// Дата изменения
      /// </summary>
      [Display(Description = "Дата изменения")]
      [DataMember(Name = "dateChange", EmitDefaultValue = false)]
      public virtual DateTime? DateChange { get; set; }
      
      /// <summary>
      /// Дата действия с
      /// </summary>
      [Display(Description = "Дата действия с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата действия по
      /// </summary>
      [Display(Description = "Дата действия по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Цена
      /// </summary>
      [Display(Description = "Цена")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Себестоимость
      /// </summary>
      [Display(Description = "Себестоимость")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal? PriceInternal { get; set; }
      
      /// <summary>
      /// Комментарий к услуге
      /// </summary>
      [Display(Description = "Комментарий к услуге")]
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
      /// Ручной ввод цены
      /// </summary>
      [Display(Description = "Ручной ввод цены")]
      [Required(ErrorMessage = "\"Ручной ввод цены\" должно быть заполнено")]
      [DataMember(Name = "manualPrice", EmitDefaultValue = false)]
      public virtual bool ManualPrice { get; set; }
      
      
      /// <summary>
      /// Ссылка на дополнительную услугу
      /// </summary>
      [InverseProperty("AddonServicesLinks")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
      /// <summary>
      /// Дополнительная услуга
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "Дополнительная услуга")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// Дополнительная услуга
      /// </summary>
      [Display(Description = "Дополнительная услуга")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }
      
      /// <summary>
      /// Заявление связь с услугами
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Заявление связь с услугами")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Заявление связь с услугами
      /// </summary>
      [InverseProperty("AddonServicesLinks")]
      [Display(Description = "Заявление связь с услугами")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
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
      /// Статус услуги
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус услуги")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус услуги
      /// </summary>
      [Display(Description = "Статус услуги")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [Display(Description = "Пользователь")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Билет
      /// </summary>
      [ForeignKey("Ticket")]
      [DataMember(Name = "ticketId")]
      [Display(Description = "Билет")]
      public virtual long? TicketId { get; set; }
      
      
      /// <summary>
      /// Билет
      /// </summary>
      [Display(Description = "Билет")]
      [DataMember(Name = "ticket", EmitDefaultValue = false)]
      public virtual Ticket Ticket { get; set; }
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [ForeignKey("Service")]
      [DataMember(Name = "serviceId")]
      [Display(Description = "Отдыхающие")]
      public virtual long? ServiceId { get; set; }
      
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [InverseProperty("Peoples")]
      [Display(Description = "Отдыхающие")]
      [DataMember(Name = "service", EmitDefaultValue = false)]
      public virtual RequestService Service { get; set; }

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