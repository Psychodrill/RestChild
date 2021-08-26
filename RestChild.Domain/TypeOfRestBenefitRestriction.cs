// File:    TypeOfRestBenefitRestriction.cs
// Purpose: Definition of Class TypeOfRestBenefitRestriction

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Ограничение по льготе, виду отдыха
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfRestBenefitRestriction")]
   public partial class TypeOfRestBenefitRestriction : IEntityBase
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
      /// Нижняя граница возраста
      /// </summary>
      [Display(Description = "Нижняя граница возраста")]
      [Required(ErrorMessage = "\"Нижняя граница возраста\" должно быть заполнено")]
      [DataMember(Name = "minAge", EmitDefaultValue = false)]
      public virtual int MinAge { get; set; }
      
      /// <summary>
      /// Верхняя граница возраста
      /// </summary>
      [Display(Description = "Верхняя граница возраста")]
      [Required(ErrorMessage = "\"Верхняя граница возраста\" должно быть заполнено")]
      [DataMember(Name = "maxAge", EmitDefaultValue = false)]
      public virtual int MaxAge { get; set; }
      
      
      /// <summary>
      /// Вид льготы
      /// </summary>
      [ForeignKey("BenefitType")]
      [DataMember(Name = "benefitTypeId")]
      [Display(Description = "Вид льготы")]
      public virtual long? BenefitTypeId { get; set; }
      
      
      /// <summary>
      /// Вид льготы
      /// </summary>
      [Display(Description = "Вид льготы")]
      [DataMember(Name = "benefitType", EmitDefaultValue = false)]
      public virtual BenefitType BenefitType { get; set; }
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Вид отдыха")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [InverseProperty("TypeOfRestBenefitRestrictions")]
      [Display(Description = "Вид отдыха")]
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