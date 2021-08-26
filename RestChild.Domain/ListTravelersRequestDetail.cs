// File:    ListTravelersRequestDetail.cs
// Purpose: Definition of Class ListTravelersRequestDetail

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Детализация ранга заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "listTravelersRequestDetail")]
   public partial class ListTravelersRequestDetail : IEntityBase
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
      /// Детализация
      /// </summary>
      [Display(Description = "Детализация")]
      [DataMember(Name = "detail", EmitDefaultValue = false)]
      public virtual String Detail { get; set; }
      
      
      /// <summary>
      /// Детализация по рангу заявлений
      /// </summary>
      [ForeignKey("ListTravelersRequest")]
      [DataMember(Name = "listTravelersRequestId")]
      [Display(Description = "Детализация по рангу заявлений")]
      public virtual long? ListTravelersRequestId { get; set; }
      
      
      /// <summary>
      /// Детализация по рангу заявлений
      /// </summary>
      [InverseProperty("Details")]
      [Display(Description = "Детализация по рангу заявлений")]
      [DataMember(Name = "listTravelersRequest", EmitDefaultValue = false)]
      public virtual ListTravelersRequest ListTravelersRequest { get; set; }
      
      /// <summary>
      /// Отдыхающий
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Отдыхающий")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Отдыхающий
      /// </summary>
      [Display(Description = "Отдыхающий")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Отдыхающий
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Отдыхающий")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Отдыхающий
      /// </summary>
      [Display(Description = "Отдыхающий")]
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