// File:    Contract.cs
// Purpose: Definition of Class Contract

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Договор
   /// </summary>
   [Serializable]
   [DataContract(Name = "contract")]
   public partial class Contract : IEntityBase
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
      /// Номер
      /// </summary>
      [Display(Description = "Номер")]
      [MaxLength(1000, ErrorMessage = "\"Номер\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Номер\" не может быть пустым")]
      [DataMember(Name = "signNumber", EmitDefaultValue = false)]
      public virtual string SignNumber { get; set; }
      
      /// <summary>
      /// Дата заключения
      /// </summary>
      [Display(Description = "Дата заключения")]
      [Required(ErrorMessage = "\"Дата заключения\" должно быть заполнено")]
      [DataMember(Name = "signDate", EmitDefaultValue = false)]
      public virtual DateTime SignDate { get; set; }
      
      /// <summary>
      /// Сумма договора
      /// </summary>
      [Display(Description = "Сумма договора")]
      [Required(ErrorMessage = "\"Сумма договора\" должно быть заполнено")]
      [DataMember(Name = "summa", EmitDefaultValue = false)]
      public virtual decimal Summa { get; set; }
      
      /// <summary>
      /// Срок действия с
      /// </summary>
      [Display(Description = "Срок действия с")]
      [DataMember(Name = "startDate", EmitDefaultValue = false)]
      public virtual DateTime? StartDate { get; set; }
      
      /// <summary>
      /// Срок действия по
      /// </summary>
      [Display(Description = "Срок действия по")]
      [DataMember(Name = "endDate", EmitDefaultValue = false)]
      public virtual DateTime? EndDate { get; set; }
      
      /// <summary>
      /// Описание договора
      /// </summary>
      [Display(Description = "Описание договора")]
      [DataMember(Name = "contractDescription", EmitDefaultValue = false)]
      public virtual String ContractDescription { get; set; }
      
      /// <summary>
      /// Плановое количество заявлений
      /// </summary>
      [Display(Description = "Плановое количество заявлений")]
      [DataMember(Name = "planCount", EmitDefaultValue = false)]
      public virtual int? PlanCount { get; set; }
      
      /// <summary>
      /// На отдых
      /// </summary>
      [Display(Description = "На отдых")]
      [Required(ErrorMessage = "\"На отдых\" должно быть заполнено")]
      [DataMember(Name = "onRest", EmitDefaultValue = false)]
      public virtual bool OnRest { get; set; }
      
      /// <summary>
      /// На транспортные услуги
      /// </summary>
      [Display(Description = "На транспортные услуги")]
      [Required(ErrorMessage = "\"На транспортные услуги\" должно быть заполнено")]
      [DataMember(Name = "onTransport", EmitDefaultValue = false)]
      public virtual bool OnTransport { get; set; }
      
      /// <summary>
      /// На услуги
      /// </summary>
      [Display(Description = "На услуги")]
      [Required(ErrorMessage = "\"На услуги\" должно быть заполнено")]
      [DataMember(Name = "onService", EmitDefaultValue = false)]
      public virtual bool OnService { get; set; }
      
      /// <summary>
      /// Название
      /// </summary>
      [Display(Description = "Название")]
      [MaxLength(1000, ErrorMessage = "\"Название\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Комиссия
      /// </summary>
      [Display(Description = "Комиссия")]
      [DataMember(Name = "commission", EmitDefaultValue = false)]
      public virtual decimal? Commission { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      
      /// <summary>
      /// Договор
      /// </summary>
      [InverseProperty("Contract")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tour { get; set; }
      
      /// <summary>
      /// Договор
      /// </summary>
      [InverseProperty("Contract")]
      [DataMember(Name = "directoryFlights", EmitDefaultValue = false)]
      public virtual ICollection<DirectoryFlights> DirectoryFlights { get; set; }
      
      /// <summary>
      /// Услуги
      /// </summary>
      [InverseProperty("Contract")]
      [DataMember(Name = "services", EmitDefaultValue = false)]
      public virtual ICollection<AddonServices> Services { get; set; }
      
      /// <summary>
      /// Доп соглашения
      /// </summary>
      [InverseProperty("Contract")]
      [DataMember(Name = "addonAgreements", EmitDefaultValue = false)]
      public virtual ICollection<ContractAddonAgreement> AddonAgreements { get; set; }
      
      /// <summary>
      /// Год отдыха
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год отдыха")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год отдыха
      /// </summary>
      [Display(Description = "Год отдыха")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Статус договора
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус договора")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус договора
      /// </summary>
      [Display(Description = "Статус договора")]
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
      /// ОИВ
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "ОИВ")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// ОИВ
      /// </summary>
      [Display(Description = "ОИВ")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Исполнитель
      /// </summary>
      [ForeignKey("Supplier")]
      [DataMember(Name = "supplierId")]
      [Display(Description = "Исполнитель")]
      public virtual long? SupplierId { get; set; }
      
      
      /// <summary>
      /// Исполнитель
      /// </summary>
      [Display(Description = "Исполнитель")]
      [DataMember(Name = "supplier", EmitDefaultValue = false)]
      public virtual Organization Supplier { get; set; }
      
      /// <summary>
      /// Выбранные банковские реквизиты
      /// </summary>
      [ForeignKey("OrganizationBank")]
      [DataMember(Name = "organizationBankId")]
      [Display(Description = "Выбранные банковские реквизиты")]
      public virtual long? OrganizationBankId { get; set; }
      
      
      /// <summary>
      /// Выбранные банковские реквизиты
      /// </summary>
      [Display(Description = "Выбранные банковские реквизиты")]
      [DataMember(Name = "organizationBank", EmitDefaultValue = false)]
      public virtual OrganizationBank OrganizationBank { get; set; }
      
      /// <summary>
      /// Куратор
      /// </summary>
      [ForeignKey("Curator")]
      [DataMember(Name = "curatorId")]
      [Display(Description = "Куратор")]
      public virtual long? CuratorId { get; set; }
      
      
      /// <summary>
      /// Куратор
      /// </summary>
      [Display(Description = "Куратор")]
      [DataMember(Name = "curator", EmitDefaultValue = false)]
      public virtual Account Curator { get; set; }

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