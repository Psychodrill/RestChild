// File:    BtiDistrict.cs
// Purpose: Definition of Class BtiDistrict

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Округ
   /// </summary>
   [Serializable]
   [DataContract(Name = "btiDistrict")]
   public partial class BtiDistrict : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Код Гивз
      /// </summary>
      [Display(Description = "Код Гивз")]
      [Required(ErrorMessage = "\"Код Гивз\" должно быть заполнено")]
      [DataMember(Name = "givz", EmitDefaultValue = false)]
      public virtual long Givz { get; set; }
      
      /// <summary>
      /// Видимые
      /// </summary>
      [Display(Description = "Видимые")]
      [Required(ErrorMessage = "\"Видимые\" должно быть заполнено")]
      [DataMember(Name = "isVisible", EmitDefaultValue = false)]
      public virtual bool IsVisible { get; set; }
      
      /// <summary>
      /// Код ОКАТО
      /// </summary>
      [Display(Description = "Код ОКАТО")]
      [DataMember(Name = "okato", EmitDefaultValue = false)]
      public virtual int? Okato { get; set; }

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