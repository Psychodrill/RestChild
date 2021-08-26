// File:    RequestInformationVoucher.cs
// Purpose: Definition of Class RequestInformationVoucher

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Сведения о путевках для компенсации
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestInformationVoucher")]
   public partial class RequestInformationVoucher : IEntityBase
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
      /// Наименование учреждение
      /// </summary>
      [Display(Description = "Наименование учреждение")]
      [MaxLength(1000, ErrorMessage = "\"Наименование учреждение\" не может быть больше 1000 символов")]
      [DataMember(Name = "organizationName", EmitDefaultValue = false)]
      public virtual string OrganizationName { get; set; }
      
      /// <summary>
      /// Дата начала путевки
      /// </summary>
      [Display(Description = "Дата начала путевки")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата окончания путевки
      /// </summary>
      [Display(Description = "Дата окончания путевки")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Стоимость путевки
      /// </summary>
      [Display(Description = "Стоимость путевки")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Стоимость дороги
      /// </summary>
      [Display(Description = "Стоимость дороги")]
      [DataMember(Name = "costOfRide", EmitDefaultValue = false)]
      public virtual decimal? CostOfRide { get; set; }
      
      /// <summary>
      /// Количество отдохнувших в путевке
      /// </summary>
      [Display(Description = "Количество отдохнувших в путевке")]
      [Required(ErrorMessage = "\"Количество отдохнувших в путевке\" должно быть заполнено")]
      [DataMember(Name = "countPeople", EmitDefaultValue = false)]
      public virtual int CountPeople { get; set; }
      
      
      /// <summary>
      /// Стоимость путевки сопровождающих
      /// </summary>
      [InverseProperty("RequestInformationVoucher")]
      [DataMember(Name = "attendantsPrice", EmitDefaultValue = false)]
      public virtual ICollection<RequestInformationVoucherAttendant> AttendantsPrice { get; set; }
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [ForeignKey("Type")]
      [DataMember(Name = "typeId")]
      [Display(Description = "Вид отдыха")]
      public virtual long? TypeId { get; set; }
      
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [Display(Description = "Вид отдыха")]
      [DataMember(Name = "type", EmitDefaultValue = false)]
      public virtual TypeRequestInformationVoucher Type { get; set; }
      
      /// <summary>
      /// Сведения о путевках для компенсации
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Сведения о путевках для компенсации")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Сведения о путевках для компенсации
      /// </summary>
      [InverseProperty("InformationVouchers")]
      [Display(Description = "Сведения о путевках для компенсации")]
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