// File:    ListTravelers.cs
// Purpose: Definition of Class ListTravelers

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Список для включения
   /// </summary>
   [Serializable]
   [DataContract(Name = "listTravelers")]
   public partial class ListTravelers : IEntityBase
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
      /// Номер
      /// </summary>
      [Display(Description = "Номер")]
      [MaxLength(1000, ErrorMessage = "\"Номер\" не может быть больше 1000 символов")]
      [DataMember(Name = "signNumber", EmitDefaultValue = false)]
      public virtual string SignNumber { get; set; }
      
      /// <summary>
      /// Дата
      /// </summary>
      [Display(Description = "Дата")]
      [DataMember(Name = "signDate", EmitDefaultValue = false)]
      public virtual DateTime? SignDate { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual String Name { get; set; }
      
      /// <summary>
      /// Пункт
      /// </summary>
      [Display(Description = "Пункт")]
      [MaxLength(1000, ErrorMessage = "\"Пункт\" не может быть больше 1000 символов")]
      [DataMember(Name = "point", EmitDefaultValue = false)]
      public virtual string Point { get; set; }
      
      /// <summary>
      /// Квота
      /// </summary>
      [Display(Description = "Квота")]
      [DataMember(Name = "limit", EmitDefaultValue = false)]
      public virtual long? Limit { get; set; }
      
      
      /// <summary>
      /// Список заявлений
      /// </summary>
      [InverseProperty("ListTravelers")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<ListTravelersRequest> Requests { get; set; }
      
      /// <summary>
      /// Связь списков по иеархии
      /// </summary>
      [InverseProperty("Parent")]
      [DataMember(Name = "children", EmitDefaultValue = false)]
      public virtual ICollection<ListTravelers> Children { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("StateMachineState")]
      [DataMember(Name = "stateMachineStateId")]
      [Display(Description = "")]
      public virtual long? StateMachineStateId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "stateMachineState", EmitDefaultValue = false)]
      public virtual StateMachineState StateMachineState { get; set; }
      
      /// <summary>
      /// Цель обращения
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Цель обращения")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Цель обращения
      /// </summary>
      [Display(Description = "Цель обращения")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }
      
      /// <summary>
      /// Год кампании
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год кампании")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год кампании
      /// </summary>
      [Display(Description = "Год кампании")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
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
      /// Документы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")]
      [Display(Description = "Документы")]
      public virtual long? LinkToFileId { get; set; }
      
      
      /// <summary>
      /// Документы
      /// </summary>
      [Display(Description = "Документы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }
      
      /// <summary>
      /// Связь списков по иеархии
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Связь списков по иеархии")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Связь списков по иеархии
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Связь списков по иеархии")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual ListTravelers Parent { get; set; }

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