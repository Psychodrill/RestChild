// File:    Country.cs
// Purpose: Definition of Class Country

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Страна
   /// </summary>
   [Serializable]
   [DataContract(Name = "country")]
   public partial class Country : IEntityBase
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
      /// Код страны
      /// </summary>
      [Display(Description = "Код страны")]
      [MaxLength(1000, ErrorMessage = "\"Код страны\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Код страны\" не может быть пустым")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Полное наименование
      /// </summary>
      [Display(Description = "Полное наименование")]
      [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Полное наименование\" не может быть пустым")]
      [DataMember(Name = "fullName", EmitDefaultValue = false)]
      public virtual string FullName { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" не может быть пустым")]
      [DataMember(Name = "symbolCode2", EmitDefaultValue = false)]
      public virtual string SymbolCode2 { get; set; }
      
      /// <summary>
      /// Код 3 буквы
      /// </summary>
      [Display(Description = "Код 3 буквы")]
      [MaxLength(1000, ErrorMessage = "\"Код 3 буквы\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Код 3 буквы\" не может быть пустым")]
      [DataMember(Name = "symbolCode3", EmitDefaultValue = false)]
      public virtual string SymbolCode3 { get; set; }
      
      
      /// <summary>
      /// Связь питания и стран
      /// </summary>
      [InverseProperty("Countrys")]
      [DataMember(Name = "diningOptions", EmitDefaultValue = false)]
      public virtual ICollection<DiningOptions> DiningOptions { get; set; }
      
      /// <summary>
      /// Связь размещения и страны
      /// </summary>
      [InverseProperty("Country")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<TourCountry> Tours { get; set; }

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