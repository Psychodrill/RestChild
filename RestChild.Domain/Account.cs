// File:    Account.cs
// Purpose: Definition of Class Account

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Пользователь
   /// </summary>
   [Serializable]
   [DataContract(Name = "account")]
   public partial class Account : IEntityBase
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
      /// Имя пользователя
      /// </summary>
      [Display(Description = "Имя пользователя")]
      [MaxLength(1000, ErrorMessage = "\"Имя пользователя\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Имя пользователя\" не может быть пустым")]
      [DataMember(Name = "login", EmitDefaultValue = false)]
      public virtual string Login { get; set; }
      
      /// <summary>
      /// ФИО пользователя
      /// </summary>
      [Display(Description = "ФИО пользователя")]
      [MaxLength(1000, ErrorMessage = "\"ФИО пользователя\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"ФИО пользователя\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Пароль
      /// </summary>
      [Display(Description = "Пароль")]
      [MaxLength(1000, ErrorMessage = "\"Пароль\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Пароль\" не может быть пустым")]
      [DataMember(Name = "password", EmitDefaultValue = false)]
      public virtual string Password { get; set; }
      
      /// <summary>
      /// Соль
      /// </summary>
      [Display(Description = "Соль")]
      [MaxLength(1000, ErrorMessage = "\"Соль\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Соль\" не может быть пустым")]
      [DataMember(Name = "salt", EmitDefaultValue = false)]
      public virtual string Salt { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime? DateCreate { get; set; }
      
      /// <summary>
      /// Активен
      /// </summary>
      [Display(Description = "Активен")]
      [Required(ErrorMessage = "\"Активен\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Дата обновления
      /// </summary>
      [Display(Description = "Дата обновления")]
      [DataMember(Name = "dateUpdate", EmitDefaultValue = false)]
      public virtual DateTime? DateUpdate { get; set; }
      
      /// <summary>
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Должность
      /// </summary>
      [Display(Description = "Должность")]
      [MaxLength(1000, ErrorMessage = "\"Должность\" не может быть больше 1000 символов")]
      [DataMember(Name = "position", EmitDefaultValue = false)]
      public virtual string Position { get; set; }
      
      /// <summary>
      /// Дата удаления
      /// </summary>
      [Display(Description = "Дата удаления")]
      [DataMember(Name = "dateDelete", EmitDefaultValue = false)]
      public virtual DateTime? DateDelete { get; set; }
      
      /// <summary>
      /// Удален
      /// </summary>
      [Display(Description = "Удален")]
      [Required(ErrorMessage = "\"Удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Временный пароль
      /// </summary>
      [Display(Description = "Временный пароль")]
      [Required(ErrorMessage = "\"Временный пароль\" должно быть заполнено")]
      [DataMember(Name = "isTemporyPassword", EmitDefaultValue = false)]
      public virtual bool IsTemporyPassword { get; set; }
      
      /// <summary>
      /// Дата последнего изменения пароля
      /// </summary>
      [Display(Description = "Дата последнего изменения пароля")]
      [DataMember(Name = "dateLastChangePassword", EmitDefaultValue = false)]
      public virtual DateTime? DateLastChangePassword { get; set; }
      
      /// <summary>
      /// Кол-во неудачных попыток входа
      /// </summary>
      [Display(Description = "Кол-во неудачных попыток входа")]
      [DataMember(Name = "countUnsuccess", EmitDefaultValue = false)]
      public virtual int? CountUnsuccess { get; set; }
      
      /// <summary>
      /// Дата последней неудачной попытки
      /// </summary>
      [Display(Description = "Дата последней неудачной попытки")]
      [DataMember(Name = "dateLastUnsuccess", EmitDefaultValue = false)]
      public virtual DateTime? DateLastUnsuccess { get; set; }
      
      
      /// <summary>
      /// Связь пользователя и роли
      /// </summary>
      [InverseProperty("Account")]
      [DataMember(Name = "roles", EmitDefaultValue = false)]
      public virtual ICollection<AccountRoles> Roles { get; set; }
      
      /// <summary>
      /// МГТ Рабочие дни. Автор события
      /// </summary>
      [InverseProperty("Author")]
      [DataMember(Name = "mGTWorkingDaysHistory", EmitDefaultValue = false)]
      public virtual ICollection<MGTWorkingDaysHistory> MGTWorkingDaysHistory { get; set; }
      
      /// <summary>
      /// Связь пользователей с правами
      /// </summary>
      [InverseProperty("Account")]
      [DataMember(Name = "rights", EmitDefaultValue = false)]
      public virtual ICollection<AccountRights> Rights { get; set; }
      
      /// <summary>
      /// История пользователя
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История пользователя")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История пользователя
      /// </summary>
      [Display(Description = "История пользователя")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }

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