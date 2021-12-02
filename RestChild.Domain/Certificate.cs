// File:    Certificate.cs
// Purpose: Definition of Class Certificate

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Сертификат
   /// </summary>
   [Serializable]
   [DataContract(Name = "certificate")]
   public partial class Certificate : IEntityBase
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
      /// Номер договора с отдыхающим
      /// </summary>
      [Display(Description = "Номер договора с отдыхающим")]
      [MaxLength(1000, ErrorMessage = "\"Номер договора с отдыхающим\" не может быть больше 1000 символов")]
      [DataMember(Name = "contractNumber", EmitDefaultValue = false)]
      public virtual string ContractNumber { get; set; }
      
      /// <summary>
      /// Дата заключения договора с отдыхающим
      /// </summary>
      [Display(Description = "Дата заключения договора с отдыхающим")]
      [DataMember(Name = "contractDate", EmitDefaultValue = false)]
      public virtual DateTime? ContractDate { get; set; }
      
      /// <summary>
      /// Период отдыха c
      /// </summary>
      [Display(Description = "Период отдыха c")]
      [DataMember(Name = "restDateFrom", EmitDefaultValue = false)]
      public virtual DateTime? RestDateFrom { get; set; }
      
      /// <summary>
      /// Период отдыха по
      /// </summary>
      [Display(Description = "Период отдыха по")]
      [DataMember(Name = "restDateTo", EmitDefaultValue = false)]
      public virtual DateTime? RestDateTo { get; set; }
      
      /// <summary>
      /// Наименование организации отдыха и оздоровления
      /// </summary>
      [Display(Description = "Наименование организации отдыха и оздоровления")]
      [MaxLength(1000, ErrorMessage = "\"Наименование организации отдыха и оздоровления\" не может быть больше 1000 символов")]
      [DataMember(Name = "place", EmitDefaultValue = false)]
      public virtual string Place { get; set; }
      
      /// <summary>
      /// Стоимость договора общая
      /// </summary>
      [Display(Description = "Стоимость договора общая")]
      [DataMember(Name = "fullPrice", EmitDefaultValue = false)]
      public virtual decimal? FullPrice { get; set; }
      
      /// <summary>
      /// Стоимость договора на ребенка
      /// </summary>
      [Display(Description = "Стоимость договора на ребенка")]
      [DataMember(Name = "priceForChild", EmitDefaultValue = false)]
      public virtual decimal? PriceForChild { get; set; }
      
      /// <summary>
      /// Дата гашения сертификата
      /// </summary>
      [Display(Description = "Дата гашения сертификата")]
      [DataMember(Name = "datePaidOff", EmitDefaultValue = false)]
      public virtual DateTime? DatePaidOff { get; set; }
      
      /// <summary>
      /// Место отдыха (регион)
      /// </summary>
      [Display(Description = "Место отдыха (регион)")]
      [MaxLength(1000, ErrorMessage = "\"Место отдыха (регион)\" не может быть больше 1000 символов")]
      [DataMember(Name = "region", EmitDefaultValue = false)]
      public virtual string Region { get; set; }
      
      
      /// <summary>
      /// связь погашения сертификата и заявления
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "связь погашения сертификата и заявления")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// связь погашения сертификата и заявления
      /// </summary>
      [InverseProperty("Certificates")]
      [Display(Description = "связь погашения сертификата и заявления")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Связь сертификата с его статусом
      /// </summary>
      [ForeignKey("StateMachineState")]
      [DataMember(Name = "stateMachineStateId")]
      [Display(Description = "Связь сертификата с его статусом")]
      public virtual long? StateMachineStateId { get; set; }
      
      
      /// <summary>
      /// Связь сертификата с его статусом
      /// </summary>
      [Display(Description = "Связь сертификата с его статусом")]
      [DataMember(Name = "stateMachineState", EmitDefaultValue = false)]
      public virtual StateMachineState StateMachineState { get; set; }

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

        /// <summary>
        /// связь погашения сертификата и заявления
        /// </summary>
        [ForeignKey("Organizations")]
        [DataMember(Name = "organizationsId")]
        [Display(Description = "Связь сертификата с организацией отдыха")]
        public virtual long? OrganizationsId { get; set; }


        /// <summary>
        /// связь погашения сертификата и заявления
        /// </summary>
       // [InverseProperty("Certificates")]
        [Display(Description = "Связь сертификата с организацией отдыха")]
        [DataMember(Name = "organizations", EmitDefaultValue = false)]
        public virtual Organization Organizations { get; set; }
    }
}
