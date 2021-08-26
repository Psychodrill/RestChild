// File:    TradeUnionPersonCheck.cs
// Purpose: Definition of Class TradeUnionPersonCheck

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Проверка персоны
   /// </summary>
   [Serializable]
   [DataContract(Name = "tradeUnionPersonCheck")]
   public partial class TradeUnionPersonCheck : IEntityBase
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
      /// Тип проверки
      /// </summary>
      [Display(Description = "Тип проверки")]
      [Required(ErrorMessage = "\"Тип проверки\" должно быть заполнено")]
      [DataMember(Name = "personCheckType", EmitDefaultValue = false)]
      public virtual long PersonCheckType { get; set; }
      
      /// <summary>
      /// Обработано
      /// </summary>
      [Display(Description = "Обработано")]
      [Required(ErrorMessage = "\"Обработано\" должно быть заполнено")]
      [DataMember(Name = "isProcessed", EmitDefaultValue = false)]
      public virtual bool IsProcessed { get; set; }
      
      /// <summary>
      /// Не акутально
      /// </summary>
      [Display(Description = "Не акутально")]
      [Required(ErrorMessage = "\"Не акутально\" должно быть заполнено")]
      [DataMember(Name = "notActual", EmitDefaultValue = false)]
      public virtual bool NotActual { get; set; }
      
      
      /// <summary>
      /// Связь проверки с персоной
      /// </summary>
      [ForeignKey("Person")]
      [DataMember(Name = "personId")][Display(Description = "Связь проверки с персоной")]
      public virtual long? PersonId { get; set; }
      /// <summary>
      /// Связь проверки с персоной
      /// </summary>
      [InverseProperty("PersonCheck")]
      [Display(Description = "Связь проверки с персоной")]
      [DataMember(Name = "person", EmitDefaultValue = false)]
      public virtual Person Person { get; set; }
      
      /// <summary>
      /// Результат проверки персоны
      /// </summary>
      [InverseProperty("PersonCheckResults")]
      [DataMember(Name = "personCheckResults", EmitDefaultValue = false)]
      public virtual ICollection<Person> PersonCheckResults { get; set; }

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