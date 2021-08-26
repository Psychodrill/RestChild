// File:    BtiRegion.cs
// Purpose: Definition of Class BtiRegion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Район
   /// </summary>
   [Serializable]
   [DataContract(Name = "btiRegion")]
   public partial class BtiRegion : IEntityBase
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
      /// Наименование документа
      /// </summary>
      [Display(Description = "Наименование документа")]
      [MaxLength(1000, ErrorMessage = "\"Наименование документа\" не может быть больше 1000 символов")]
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
      /// Округ c районом
      /// </summary>
      [ForeignKey("BtiDistrict")]
      [DataMember(Name = "btiDistrictId")][Display(Description = "Округ c районом")]
      public virtual long? BtiDistrictId { get; set; }
      /// <summary>
      /// Округ c районом
      /// </summary>
      [Display(Description = "Округ c районом")]
      [DataMember(Name = "btiDistrict", EmitDefaultValue = false)]
      public virtual BtiDistrict BtiDistrict { get; set; }

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