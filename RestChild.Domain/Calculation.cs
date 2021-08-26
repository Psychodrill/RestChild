// File:    Calculation.cs
// Purpose: Definition of Class Calculation

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Начисление
   /// </summary>
   [Serializable]
   [DataContract(Name = "calculation")]
   public partial class Calculation : IEntityBase
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
      /// Сумма
      /// </summary>
      [Display(Description = "Сумма")]
      [DataMember(Name = "summa", EmitDefaultValue = false)]
      public virtual decimal? Summa { get; set; }
      
      /// <summary>
      /// Дата начисления
      /// </summary>
      [Display(Description = "Дата начисления")]
      [DataMember(Name = "dateCalculation", EmitDefaultValue = false)]
      public virtual DateTime? DateCalculation { get; set; }
      
      /// <summary>
      /// Конечный срок оплаты
      /// </summary>
      [Display(Description = "Конечный срок оплаты ")]
      [DataMember(Name = "lastPaymentDate", EmitDefaultValue = false)]
      public virtual DateTime? LastPaymentDate { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Номер начисления
      /// </summary>
      [Display(Description = "Номер начисления")]
      [MaxLength(1000, ErrorMessage = "\"Номер начисления\" не может быть больше 1000 символов")]
      [DataMember(Name = "number", EmitDefaultValue = false)]
      public virtual string Number { get; set; }
      
      /// <summary>
      /// Назначение оплаты
      /// </summary>
      [Display(Description = "Назначение оплаты")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("History")]
      [DataMember(Name = "historyId")]
      [Display(Description = "История")]
      public virtual long? HistoryId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "history", EmitDefaultValue = false)]
      public virtual HistoryLink History { get; set; }
      
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
      /// Платеж - начисление
      /// </summary>
      [InverseProperty("Calculations")]
      [DataMember(Name = "payments", EmitDefaultValue = false)]
      public virtual ICollection<Payment> Payments { get; set; }
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [InverseProperty("Calculations")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<Child> Children { get; set; }
      
      /// <summary>
      /// Соповождающий
      /// </summary>
      [InverseProperty("Calculations")]
      [DataMember(Name = "attendants", EmitDefaultValue = false)]
      public virtual ICollection<Applicant> Attendants { get; set; }
      
      /// <summary>
      /// Заявление
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Заявление")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Заявление
      /// </summary>
      [InverseProperty("Calculations")]
      [Display(Description = "Заявление")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Ссылка на дополнительную услугу
      /// </summary>
      [InverseProperty("Calculations")]
      [DataMember(Name = "addonServicesLinks", EmitDefaultValue = false)]
      public virtual ICollection<AddonServicesLink> AddonServicesLinks { get; set; }
      
      /// <summary>
      /// Вид начисления
      /// </summary>
      [ForeignKey("TypeOfCalculation")]
      [DataMember(Name = "typeOfCalculationId")]
      [Display(Description = "Вид начисления")]
      public virtual long? TypeOfCalculationId { get; set; }
      
      
      /// <summary>
      /// Вид начисления
      /// </summary>
      [Display(Description = "Вид начисления")]
      [DataMember(Name = "typeOfCalculation", EmitDefaultValue = false)]
      public virtual TypeOfCalculation TypeOfCalculation { get; set; }
      
      /// <summary>
      /// Билет
      /// </summary>
      [InverseProperty("Calculations")]
      [DataMember(Name = "tickets", EmitDefaultValue = false)]
      public virtual ICollection<Ticket> Tickets { get; set; }
      
      /// <summary>
      /// Проживание в заявлении - начисление
      /// </summary>
      [InverseProperty("Calculations")]
      [DataMember(Name = "requestAccommodations", EmitDefaultValue = false)]
      public virtual ICollection<RequestAccommodation> RequestAccommodations { get; set; }

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