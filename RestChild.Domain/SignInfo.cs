// File:    SignInfo.cs
// Purpose: Definition of Class SignInfo

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Электронная подпись
   /// </summary>
   [Serializable]
   [DataContract(Name = "signInfo")]
   public partial class SignInfo : IEntityBase
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
      /// Дата подписи
      /// </summary>
      [Display(Description = "Дата подписи")]
      [Required(ErrorMessage = "\"Дата подписи\" должно быть заполнено")]
      [DataMember(Name = "signDate", EmitDefaultValue = false)]
      public virtual DateTime SignDate { get; set; }
      
      /// <summary>
      /// Ссылка на файл подписи
      /// </summary>
      [Display(Description = "Ссылка на файл подписи")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на файл подписи\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Ссылка на файл подписи\" не может быть пустым")]
      [DataMember(Name = "fileUrl", EmitDefaultValue = false)]
      public virtual string FileUrl { get; set; }
      
      /// <summary>
      /// Наименование документа подписи
      /// </summary>
      [Display(Description = "Наименование документа подписи")]
      [MaxLength(1000, ErrorMessage = "\"Наименование документа подписи\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование документа подписи\" не может быть пустым")]
      [DataMember(Name = "title", EmitDefaultValue = false)]
      public virtual string Title { get; set; }
      
      /// <summary>
      /// Информация из подписи
      /// </summary>
      [Display(Description = "Информация из подписи")]
      [MaxLength(1000, ErrorMessage = "\"Информация из подписи\" не может быть больше 1000 символов")]
      [DataMember(Name = "information", EmitDefaultValue = false)]
      public virtual string Information { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
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