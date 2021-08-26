// File:    MGTBookingVisit.cs
// Purpose: Definition of Class MGTBookingVisit

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Запись на визит в МГТ
   /// </summary>
   [Serializable]
   [DataContract(Name = "mGTBookingVisit")]
   public partial class MGTBookingVisit : IEntityBase
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
      /// Дата время визита (ячейка)
      /// </summary>
      [Display(Description = "Дата время визита (ячейка)")]
      [Required(ErrorMessage = "\"Дата время визита (ячейка)\" должно быть заполнено")]
      [DataMember(Name = "visitCell", EmitDefaultValue = false)]
      public virtual DateTime VisitCell { get; set; }
      
      /// <summary>
      /// Код бронироавния
      /// </summary>
      [Display(Description = "Код бронироавния")]
      [MaxLength(1000, ErrorMessage = "\"Код бронироавния\" не может быть больше 1000 символов")]
      [DataMember(Name = "pINCode", EmitDefaultValue = false)]
      public virtual string PINCode { get; set; }
      
      /// <summary>
      /// Идентификатор в МПГУ
      /// </summary>
      [Display(Description = "Идентификатор в МПГУ")]
      [MaxLength(1000, ErrorMessage = "\"Идентификатор в МПГУ\" не может быть больше 1000 символов")]
      [DataMember(Name = "mPGURegNum", EmitDefaultValue = false)]
      public virtual string MPGURegNum { get; set; }
      
      /// <summary>
      /// Номер заявления
      /// </summary>
      [Display(Description = "Номер заявления")]
      [MaxLength(1000, ErrorMessage = "\"Номер заявления\" не может быть больше 1000 символов")]
      [DataMember(Name = "serviceNumber", EmitDefaultValue = false)]
      public virtual string ServiceNumber { get; set; }
      
      /// <summary>
      /// Дата регистрации в МПГУ
      /// </summary>
      [Display(Description = "Дата регистрации в МПГУ")]
      [DataMember(Name = "mPGURegDate", EmitDefaultValue = false)]
      public virtual DateTime? MPGURegDate { get; set; }
      
      
      /// <summary>
      ///  Статус записи на визит в МГТ
      /// </summary>
      [ForeignKey("Status")]
      [DataMember(Name = "statusId")][Display(Description = " Статус записи на визит в МГТ")]
      public virtual long? StatusId { get; set; }
      /// <summary>
      ///  Статус записи на визит в МГТ
      /// </summary>
      [Display(Description = " Статус записи на визит в МГТ")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual MGTVisitBookingStatus Status { get; set; }
      
      /// <summary>
      /// Цепочка бронирований в случае когда детей более 3х (и другие подобные случаи)
      /// </summary>
      [InverseProperty("Parrent")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<MGTBookingVisit> Children { get; set; }
      
      /// <summary>
      /// Связь рабочих дней с запясями на приём
      /// </summary>
      [ForeignKey("WorkingDay")]
      [DataMember(Name = "workingDayId")]
      [Display(Description = "Связь рабочих дней с запясями на приём")]
      public virtual long? WorkingDayId { get; set; }
      
      
      /// <summary>
      /// Связь рабочих дней с запясями на приём
      /// </summary>
      [InverseProperty("VisitBookings")]
      [Display(Description = "Связь рабочих дней с запясями на приём")]
      [DataMember(Name = "workingDay", EmitDefaultValue = false)]
      public virtual MGTWorkingDay WorkingDay { get; set; }
      
      /// <summary>
      /// Связь записи на приём с посетителями
      /// </summary>
      [InverseProperty("VisitBooking")]
      [DataMember(Name = "persons", EmitDefaultValue = false)]
      public virtual ICollection<MGTVisitBookingPerson> Persons { get; set; }
      
      /// <summary>
      /// Связь визита и цели визита
      /// </summary>
      [ForeignKey("Target")]
      [DataMember(Name = "targetId")]
      [Display(Description = "Связь визита и цели визита")]
      public virtual long? TargetId { get; set; }
      
      
      /// <summary>
      /// Связь визита и цели визита
      /// </summary>
      [Display(Description = "Связь визита и цели визита")]
      [DataMember(Name = "target", EmitDefaultValue = false)]
      public virtual MGTVisitTarget Target { get; set; }
      
      /// <summary>
      /// Цепочка бронирований в случае когда детей более 3х (и другие подобные случаи)
      /// </summary>
      [ForeignKey("Parrent")]
      [DataMember(Name = "parrentId")]
      [Display(Description = "Цепочка бронирований в случае когда детей более 3х (и другие подобные случаи)")]
      public virtual long? ParrentId { get; set; }
      
      
      /// <summary>
      /// Цепочка бронирований в случае когда детей более 3х (и другие подобные случаи)
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Цепочка бронирований в случае когда детей более 3х (и другие подобные случаи)")]
      [DataMember(Name = "parrent", EmitDefaultValue = false)]
      public virtual MGTBookingVisit Parrent { get; set; }
      
      /// <summary>
      /// История изменений визита
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История изменений визита")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История изменений визита
      /// </summary>
      [Display(Description = "История изменений визита")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }

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