// File:    InteragencyRequest.cs
// Purpose: Definition of Class InteragencyRequest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Межведомственные запросы
   /// </summary>
   [Serializable]
   [DataContract(Name = "interagencyRequest")]
   public partial class InteragencyRequest : IEntityBase
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
      /// Номер запроса
      /// </summary>
      [Display(Description = "Номер запроса")]
      [MaxLength(1000, ErrorMessage = "\"Номер запроса\" не может быть больше 1000 символов")]
      [DataMember(Name = "requestNumber", EmitDefaultValue = false)]
      public virtual string RequestNumber { get; set; }
      
      /// <summary>
      /// Дата запроса
      /// </summary>
      [Display(Description = "Дата запроса")]
      [DataMember(Name = "requsetDate", EmitDefaultValue = false)]
      public virtual DateTime? RequsetDate { get; set; }
      
      /// <summary>
      /// Номер ответа
      /// </summary>
      [Display(Description = "Номер ответа")]
      [MaxLength(1000, ErrorMessage = "\"Номер ответа\" не может быть больше 1000 символов")]
      [DataMember(Name = "answerNumber", EmitDefaultValue = false)]
      public virtual string AnswerNumber { get; set; }
      
      /// <summary>
      /// Дата ответа
      /// </summary>
      [Display(Description = "Дата ответа")]
      [DataMember(Name = "answerDate", EmitDefaultValue = false)]
      public virtual DateTime? AnswerDate { get; set; }
      
      /// <summary>
      /// Комментарий к запросу
      /// </summary>
      [Display(Description = "Комментарий к запросу")]
      [DataMember(Name = "requestComment", EmitDefaultValue = false)]
      public virtual String RequestComment { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "answerComment", EmitDefaultValue = false)]
      public virtual String AnswerComment { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "requestFileUrl", EmitDefaultValue = false)]
      public virtual string RequestFileUrl { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "answerFileUrl", EmitDefaultValue = false)]
      public virtual string AnswerFileUrl { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "createDate", EmitDefaultValue = false)]
      public virtual DateTime CreateDate { get; set; }
      
      /// <summary>
      /// Признак что запрос повторный
      /// </summary>
      [Display(Description = "Признак что запрос повторный")]
      [Required(ErrorMessage = "\"Признак что запрос повторный\" должно быть заполнено")]
      [DataMember(Name = "isSecondaryRequest", EmitDefaultValue = false)]
      public virtual bool IsSecondaryRequest { get; set; }
      
      /// <summary>
      /// Для всех районов
      /// </summary>
      [Display(Description = "Для всех районов")]
      [Required(ErrorMessage = "\"Для всех районов\" должно быть заполнено")]
      [DataMember(Name = "forAllRegion", EmitDefaultValue = false)]
      public virtual bool ForAllRegion { get; set; }
      
      
      /// <summary>
      /// Статус запроса
      /// </summary>
      [ForeignKey("StatusInteragencyRequest")]
      [DataMember(Name = "statusInteragencyRequestId")]
      [Display(Description = "Статус запроса")]
      public virtual long? StatusInteragencyRequestId { get; set; }
      
      
      /// <summary>
      /// Статус запроса
      /// </summary>
      [Display(Description = "Статус запроса")]
      [DataMember(Name = "statusInteragencyRequest", EmitDefaultValue = false)]
      public virtual StatusInteragencyRequest StatusInteragencyRequest { get; set; }
      
      /// <summary>
      /// Пользователь создавший запрос
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь создавший запрос")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь создавший запрос
      /// </summary>
      [Display(Description = "Пользователь создавший запрос")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Ссылка на ОИВ куда направляется запрос
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Ссылка на ОИВ куда направляется запрос")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Ссылка на ОИВ куда направляется запрос
      /// </summary>
      [Display(Description = "Ссылка на ОИВ куда направляется запрос")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Округ
      /// </summary>
      [ForeignKey("BtiDistrict")]
      [DataMember(Name = "btiDistrictId")]
      [Display(Description = "Округ")]
      public virtual long? BtiDistrictId { get; set; }
      
      
      /// <summary>
      /// Округ
      /// </summary>
      [Display(Description = "Округ")]
      [DataMember(Name = "btiDistrict", EmitDefaultValue = false)]
      public virtual BtiDistrict BtiDistrict { get; set; }
      
      /// <summary>
      /// Район
      /// </summary>
      [ForeignKey("BtiRegion")]
      [DataMember(Name = "btiRegionId")]
      [Display(Description = "Район")]
      public virtual long? BtiRegionId { get; set; }
      
      
      /// <summary>
      /// Район
      /// </summary>
      [Display(Description = "Район")]
      [DataMember(Name = "btiRegion", EmitDefaultValue = false)]
      public virtual BtiRegion BtiRegion { get; set; }

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