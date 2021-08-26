// File:    TradeUnionList.cs
// Purpose: Definition of Class TradeUnionList

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Профсоюзный список
   /// </summary>
   [Serializable]
   [DataContract(Name = "tradeUnionList")]
   public partial class TradeUnionList : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      
      /// <summary>
      /// Список отдыхающих
      /// </summary>
      [InverseProperty("TradeUnion")]
      [DataMember(Name = "campers", EmitDefaultValue = false)]
      public virtual ICollection<TradeUnionCamper> Campers { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Год отдыха
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год отдыха")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год отдыха
      /// </summary>
      [Display(Description = "Год отдыха")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Лагерь
      /// </summary>
      [ForeignKey("Camp")]
      [DataMember(Name = "campId")]
      [Display(Description = "Лагерь")]
      public virtual long? CampId { get; set; }
      
      
      /// <summary>
      /// Лагерь
      /// </summary>
      [Display(Description = "Лагерь")]
      [DataMember(Name = "camp", EmitDefaultValue = false)]
      public virtual Organization Camp { get; set; }
      
      /// <summary>
      /// Профсоюз
      /// </summary>
      [ForeignKey("TradeUnion")]
      [DataMember(Name = "tradeUnionId")]
      [Display(Description = "Профсоюз")]
      public virtual long? TradeUnionId { get; set; }
      
      
      /// <summary>
      /// Профсоюз
      /// </summary>
      [Display(Description = "Профсоюз")]
      [DataMember(Name = "tradeUnion", EmitDefaultValue = false)]
      public virtual Organization TradeUnion { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Смена
      /// </summary>
      [ForeignKey("GroupedTimeOfRest")]
      [DataMember(Name = "groupedTimeOfRestId")]
      [Display(Description = "Смена")]
      public virtual long? GroupedTimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Смена
      /// </summary>
      [Display(Description = "Смена")]
      [DataMember(Name = "groupedTimeOfRest", EmitDefaultValue = false)]
      public virtual GroupedTimeOfRest GroupedTimeOfRest { get; set; }
      
      /// <summary>
      /// Ссылка на файлы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")]
      [Display(Description = "Ссылка на файлы")]
      public virtual long? LinkToFileId { get; set; }
      
      
      /// <summary>
      /// Ссылка на файлы
      /// </summary>
      [Display(Description = "Ссылка на файлы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }

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