// File:    BenefitTypeERL.cs
// Purpose: Definition of Class BenefitTypeERL

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Наименование ЛК в ИС Социум
   /// </summary>
   [Serializable]
   [DataContract(Name = "benefitTypeERL")]
   public partial class BenefitTypeERL : IEntityBase
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
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активно
      /// </summary>
      [Display(Description = "Активно")]
      [Required(ErrorMessage = "\"Активно\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Код ЛК
      /// </summary>
      [Display(Description = "Код ЛК ")]
      [MaxLength(1000, ErrorMessage = "\"Код ЛК \" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Код ЛК \" не может быть пустым")]
      [DataMember(Name = "lCCode", EmitDefaultValue = false)]
      public virtual string LCCode { get; set; }
      
      
      /// <summary>
      /// Вид льготы АИС ДО -> ЛК в ИС Социум
      /// </summary>
      [InverseProperty("BenefitTypeERL")]
      [DataMember(Name = "benefitTypes", EmitDefaultValue = false)]
      public virtual ICollection<BenefitType> BenefitTypes { get; set; }
      
      /// <summary>
      /// ЛК -> Цель обращения
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")][Display(Description = "ЛК -> Цель обращения")]
      public virtual long? TypeOfRestId { get; set; }
      /// <summary>
      /// ЛК -> Цель обращения
      /// </summary>
      [InverseProperty("BenefitTypesERL")]
      [Display(Description = "ЛК -> Цель обращения")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }
      
      /// <summary>
      /// МСП -> ЛК ИС Социум
      /// </summary>
      [InverseProperty("BenefitTypesERL")]
      [DataMember(Name = "typesOfRestERL", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRestERL> TypesOfRestERL { get; set; }

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