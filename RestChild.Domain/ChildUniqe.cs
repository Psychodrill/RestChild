// File:    ChildUniqe.cs
// Purpose: Definition of Class ChildUniqe

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Уникальный ребёнок
   /// </summary>
   [Serializable]
   [DataContract(Name = "childUniqe")]
   public partial class ChildUniqe : IEntityBase
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
      /// СНИЛС
      /// </summary>
      [Display(Description = "СНИЛС")]
      [MaxLength(1000, ErrorMessage = "\"СНИЛС\" не может быть больше 1000 символов")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
      
      /// <summary>
      /// Уникальный ребёнок
      /// </summary>
      [InverseProperty("ChildUniqe")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<Child> Children { get; set; }
      
      /// <summary>
      /// Родственники - дети
      /// </summary>
      [InverseProperty("Children")]
      [DataMember(Name = "relatives", EmitDefaultValue = false)]
      public virtual ICollection<RelativeUniqe> Relatives { get; set; }
      
      /// <summary>
      /// Последняя информация
      /// </summary>
      [ForeignKey("LastInfo")]
      [DataMember(Name = "lastInfoId")]
      [Display(Description = "Последняя информация")]
      public virtual long? LastInfoId { get; set; }
      
      
      /// <summary>
      /// Последняя информация
      /// </summary>
      [Display(Description = "Последняя информация")]
      [DataMember(Name = "lastInfo", EmitDefaultValue = false)]
      public virtual Child LastInfo { get; set; }

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