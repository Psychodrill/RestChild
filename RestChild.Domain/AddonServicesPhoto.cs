// File:    AddonServicesPhoto.cs
// Purpose: Definition of Class AddonServicesPhoto

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Фотографии дополнительных услуг
   /// </summary>
   [Serializable]
   [DataContract(Name = "addonServicesPhoto")]
   public partial class AddonServicesPhoto : IEntityBase
   {
      
      /// <summary>
      /// Уникальний идентификатор
      /// </summary>
      [Display(Description = "Уникальний идентификатор")]
      [Required(ErrorMessage = "\"Уникальний идентификатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Имя файла
      /// </summary>
      [Display(Description = "Имя файла")]
      [MaxLength(1000, ErrorMessage = "\"Имя файла\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Имя файла\" не может быть пустым")]
      [DataMember(Name = "fileName", EmitDefaultValue = false)]
      public virtual string FileName { get; set; }
      
      /// <summary>
      /// Ссылка на файл
      /// </summary>
      [Display(Description = "Ссылка на файл")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на файл\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Ссылка на файл\" не может быть пустым")]
      [DataMember(Name = "fileTitle", EmitDefaultValue = false)]
      public virtual string FileTitle { get; set; }
      
      /// <summary>
      /// Дата добавления
      /// </summary>
      [Display(Description = "Дата добавления")]
      [Required(ErrorMessage = "\"Дата добавления\" должно быть заполнено")]
      [DataMember(Name = "dataCreate", EmitDefaultValue = false)]
      public virtual DateTime DataCreate { get; set; }
      
      
      /// <summary>
      /// Фотографии
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "Фотографии")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// Фотографии
      /// </summary>
      [InverseProperty("Photos")]
      [Display(Description = "Фотографии")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }
      
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