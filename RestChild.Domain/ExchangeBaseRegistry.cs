// File:    ExchangeBaseRegistry.cs
// Purpose: Definition of Class ExchangeBaseRegistry

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Обмен с Базовым регистром
   /// </summary>
   [Serializable]
   [DataContract(Name = "exchangeBaseRegistry")]
   public partial class ExchangeBaseRegistry : IEntityBase
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
      /// ИД сообщения
      /// </summary>
      [Display(Description = "ИД сообщения")]
      [MaxLength(1000, ErrorMessage = "\"ИД сообщения\" не может быть больше 1000 символов")]
      [DataMember(Name = "requestGuid", EmitDefaultValue = false)]
      public virtual string RequestGuid { get; set; }
      
      /// <summary>
      /// Номер обращения
      /// </summary>
      [Display(Description = "Номер обращения")]
      [MaxLength(1000, ErrorMessage = "\"Номер обращения\" не может быть больше 1000 символов")]
      [DataMember(Name = "serviceNumber", EmitDefaultValue = false)]
      public virtual string ServiceNumber { get; set; }
      
      /// <summary>
      /// Дата отправки сообщения
      /// </summary>
      [Display(Description = "Дата отправки сообщения")]
      [DataMember(Name = "sendDate", EmitDefaultValue = false)]
      public virtual DateTime? SendDate { get; set; }
      
      /// <summary>
      /// ИД подтверждения получения
      /// </summary>
      [Display(Description = "ИД подтверждения получения")]
      [MaxLength(1000, ErrorMessage = "\"ИД подтверждения получения\" не может быть больше 1000 символов")]
      [DataMember(Name = "acknolegmentGuid", EmitDefaultValue = false)]
      public virtual string AcknolegmentGuid { get; set; }
      
      /// <summary>
      /// ИД ответа
      /// </summary>
      [Display(Description = "ИД ответа")]
      [MaxLength(1000, ErrorMessage = "\"ИД ответа\" не может быть больше 1000 символов")]
      [DataMember(Name = "responseGuid", EmitDefaultValue = false)]
      public virtual string ResponseGuid { get; set; }
      
      /// <summary>
      /// Дата получения ответа
      /// </summary>
      [Display(Description = "Дата получения ответа")]
      [DataMember(Name = "responseDate", EmitDefaultValue = false)]
      public virtual DateTime? ResponseDate { get; set; }
      
      /// <summary>
      /// Тип операции
      /// </summary>
      [Display(Description = "Тип операции")]
      [MaxLength(1000, ErrorMessage = "\"Тип операции\" не может быть больше 1000 символов")]
      [DataMember(Name = "operationType", EmitDefaultValue = false)]
      public virtual string OperationType { get; set; }
      
      /// <summary>
      /// Признак что входящее сообщение
      /// </summary>
      [Display(Description = "Признак что входящее сообщение")]
      [Required(ErrorMessage = "\"Признак что входящее сообщение\" должно быть заполнено")]
      [DataMember(Name = "isIncoming", EmitDefaultValue = false)]
      public virtual bool IsIncoming { get; set; }
      
      /// <summary>
      /// Запрос
      /// </summary>
      [Display(Description = "Запрос")]
      [DataMember(Name = "requestText", EmitDefaultValue = false)]
      public virtual String RequestText { get; set; }
      
      /// <summary>
      /// Ответ
      /// </summary>
      [Display(Description = "Ответ")]
      [DataMember(Name = "responseText", EmitDefaultValue = false)]
      public virtual String ResponseText { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isProcessed", EmitDefaultValue = false)]
      public virtual bool IsProcessed { get; set; }
      
      /// <summary>
      /// Не акутально
      /// </summary>
      [Display(Description = "Не акутально")]
      [Required(ErrorMessage = "\"Не акутально\" должно быть заполнено")]
      [DataMember(Name = "notActual", EmitDefaultValue = false)]
      public virtual bool NotActual { get; set; }
      
      /// <summary>
      /// Успешно
      /// </summary>
      [Display(Description = "Успешно")]
      [Required(ErrorMessage = "\"Успешно\" должно быть заполнено")]
      [DataMember(Name = "success", EmitDefaultValue = false)]
      public virtual bool Success { get; set; }
      
      /// <summary>
      /// Версия записи
      /// </summary>
      [Display(Description = "Версия записи")]
      [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
      public virtual byte[] RowVersion { get; set; }
      
      /// <summary>
      /// Поиск
      /// </summary>
      [Display(Description = "Поиск")]
      [MaxLength(1000, ErrorMessage = "\"Поиск\" не может быть больше 1000 символов")]
      [DataMember(Name = "searchField", EmitDefaultValue = false)]
      public virtual string SearchField { get; set; }
      
      /// <summary>
      /// Дата рожения
      /// </summary>
      [Display(Description = "Дата рожения")]
      [DataMember(Name = "birthDate", EmitDefaultValue = false)]
      public virtual DateTime? BirthDate { get; set; }
      
      /// <summary>
      /// Дополнительный запрос
      /// </summary>
      [Display(Description = "Дополнительный запрос")]
      [Required(ErrorMessage = "\"Дополнительный запрос\" должно быть заполнено")]
      [DataMember(Name = "isAddonRequest", EmitDefaultValue = false)]
      public virtual bool IsAddonRequest { get; set; }
      
      
      /// <summary>
      /// Обмен с базовым регистром
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Обмен с базовым регистром")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Обмен с базовым регистром
      /// </summary>
      [InverseProperty("BaseRegistryInfo")]
      [Display(Description = "Обмен с базовым регистром")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Тип обмена с базовым регистром
      /// </summary>
      [ForeignKey("ExchangeBaseRegistryType")]
      [DataMember(Name = "exchangeBaseRegistryTypeId")]
      [Display(Description = "Тип обмена с базовым регистром")]
      public virtual long? ExchangeBaseRegistryTypeId { get; set; }
      
      
      /// <summary>
      /// Тип обмена с базовым регистром
      /// </summary>
      [Display(Description = "Тип обмена с базовым регистром")]
      [DataMember(Name = "exchangeBaseRegistryType", EmitDefaultValue = false)]
      public virtual ExchangeBaseRegistryType ExchangeBaseRegistryType { get; set; }
      
      /// <summary>
      /// Обмен с базовым регистром
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Обмен с базовым регистром")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Обмен с базовым регистром
      /// </summary>
      [InverseProperty("BaseRegistryInfo")]
      [Display(Description = "Обмен с базовым регистром")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }

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