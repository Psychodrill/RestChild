// File:    RequestInformationVoucherAttendant.cs
// Purpose: Definition of Class RequestInformationVoucherAttendant

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Сведения о тратах сопровождающих
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestInformationVoucherAttendant")]
   public partial class RequestInformationVoucherAttendant : IEntityBase
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
      /// Стоимость путевки
      /// </summary>
      [Display(Description = "Стоимость путевки")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Стоимость проживания
      /// </summary>
      [Display(Description = "Стоимость проживания")]
      [DataMember(Name = "costOfRide", EmitDefaultValue = false)]
      public virtual decimal? CostOfRide { get; set; }
      
      /// <summary>
      /// Сумма компенсации
      /// </summary>
      [Display(Description = "Сумма компенсации")]
      [DataMember(Name = "amountOfCompensation", EmitDefaultValue = false)]
      public virtual decimal? AmountOfCompensation { get; set; }
      
      
      /// <summary>
      /// Стоимость путевки сопровождающих
      /// </summary>
      [ForeignKey("RequestInformationVoucher")]
      [DataMember(Name = "requestInformationVoucherId")]
      [Display(Description = "Стоимость путевки сопровождающих")]
      public virtual long? RequestInformationVoucherId { get; set; }
      
      
      /// <summary>
      /// Стоимость путевки сопровождающих
      /// </summary>
      [InverseProperty("AttendantsPrice")]
      [Display(Description = "Стоимость путевки сопровождающих")]
      [DataMember(Name = "requestInformationVoucher", EmitDefaultValue = false)]
      public virtual RequestInformationVoucher RequestInformationVoucher { get; set; }
      
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