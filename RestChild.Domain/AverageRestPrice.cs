// File:    AverageRestPrice.cs
// Purpose: Definition of Class AverageRestPrice

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Усреднённая цена за отдых
   /// </summary>
   [Serializable]
   [DataContract(Name = "averageRestPrice")]
   public partial class AverageRestPrice : IEntityBase
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
      /// Цена за отдых 1 человека
      /// </summary>
      [Display(Description = "Цена за отдых 1 человека")]
      [Required(ErrorMessage = "\"Цена за отдых 1 человека\" должно быть заполнено")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal Price { get; set; }
      
      
      /// <summary>
      /// Год компании -> цена 1 путёвки
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")][Display(Description = "Год компании -> цена 1 путёвки")]
      public virtual long? YearOfRestId { get; set; }
      /// <summary>
      /// Год компании -> цена 1 путёвки
      /// </summary>
      [InverseProperty("Prices")]
      [Display(Description = "Год компании -> цена 1 путёвки")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Цель обращения -> цена путёвки
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Цель обращения -> цена путёвки")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Цель обращения -> цена путёвки
      /// </summary>
      [InverseProperty("Prices")]
      [Display(Description = "Цель обращения -> цена путёвки")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }

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