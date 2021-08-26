// File:    ServiceBlock.cs
// Purpose: Definition of Class ServiceBlock

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Блок мест по услуге
   /// </summary>
   [Serializable]
   [DataContract(Name = "serviceBlock")]
   public partial class ServiceBlock : IEntityBase
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
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [Required(ErrorMessage = "\"Дата с\" должно быть заполнено")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [Required(ErrorMessage = "\"Дата по\" должно быть заполнено")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime DateTo { get; set; }
      
      /// <summary>
      /// Колечество мест
      /// </summary>
      [Display(Description = "Колечество мест")]
      [Required(ErrorMessage = "\"Колечество мест\" должно быть заполнено")]
      [DataMember(Name = "count", EmitDefaultValue = false)]
      public virtual int Count { get; set; }
      
      /// <summary>
      /// Версия записи
      /// </summary>
      [Display(Description = "Версия записи")]
      [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
      public virtual byte[] RowVersion { get; set; }
      
      
      /// <summary>
      /// Места в блоке мест по датам
      /// </summary>
      [InverseProperty("Block")]
      [DataMember(Name = "dates", EmitDefaultValue = false)]
      public virtual ICollection<ServiceBlockDate> Dates { get; set; }
      
      /// <summary>
      /// Блоки мест
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "Блоки мест")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// Блоки мест
      /// </summary>
      [InverseProperty("ServiceBlocks")]
      [Display(Description = "Блоки мест")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }

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