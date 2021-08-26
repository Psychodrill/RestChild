// File:    ResponsibilityForTask.cs
// Purpose: Definition of Class ResponsibilityForTask

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Ответственный за поручение
   /// </summary>
   [Serializable]
   [DataContract(Name = "responsibilityForTask")]
   public partial class ResponsibilityForTask : IEntityBase
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
      /// Имя ответсвенного
      /// </summary>
      [Display(Description = "Имя ответсвенного")]
      [MaxLength(1000, ErrorMessage = "\"Имя ответсвенного\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "Вожатый")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Tasks")]
      [Display(Description = "Вожатый")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }
      
      /// <summary>
      /// Администратор смены
      /// </summary>
      [ForeignKey("AdministratorTour")]
      [DataMember(Name = "administratorTourId")]
      [Display(Description = "Администратор смены")]
      public virtual long? AdministratorTourId { get; set; }
      
      
      /// <summary>
      /// Администратор смены
      /// </summary>
      [Display(Description = "Администратор смены")]
      [DataMember(Name = "administratorTour", EmitDefaultValue = false)]
      public virtual AdministratorTour AdministratorTour { get; set; }
      
      /// <summary>
      /// Заезд
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Заезд")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Заезд
      /// </summary>
      [Display(Description = "Заезд")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }
      
      /// <summary>
      /// Отряд
      /// </summary>
      [ForeignKey("Party")]
      [DataMember(Name = "partyId")]
      [Display(Description = "Отряд")]
      public virtual long? PartyId { get; set; }
      
      
      /// <summary>
      /// Отряд
      /// </summary>
      [Display(Description = "Отряд")]
      [DataMember(Name = "party", EmitDefaultValue = false)]
      public virtual Party Party { get; set; }
      
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
      /// Тип ответсвенного за исполнение поручения
      /// </summary>
      [ForeignKey("CounselorTaskExecutorType")]
      [DataMember(Name = "counselorTaskExecutorTypeId")]
      [Display(Description = "Тип ответсвенного за исполнение поручения")]
      public virtual long? CounselorTaskExecutorTypeId { get; set; }
      
      
      /// <summary>
      /// Тип ответсвенного за исполнение поручения
      /// </summary>
      [Display(Description = "Тип ответсвенного за исполнение поручения")]
      [DataMember(Name = "counselorTaskExecutorType", EmitDefaultValue = false)]
      public virtual CounselorTaskExecutorType CounselorTaskExecutorType { get; set; }

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