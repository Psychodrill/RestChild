// File:    TypeOfRestERL.cs
// Purpose: Definition of Class TypeOfRestERL

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Наименование МСП в ИС Социум
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfRestERL")]
   public partial class TypeOfRestERL : IEntityBase
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
      /// Использовать заявителя
      /// </summary>
      [Display(Description = "Использовать заявителя")]
      [Required(ErrorMessage = "\"Использовать заявителя\" должно быть заполнено")]
      [DataMember(Name = "useApplicant", EmitDefaultValue = false)]
      public virtual bool UseApplicant { get; set; }
      
      /// <summary>
      /// Код МСП
      /// </summary>
      [Display(Description = "Код МСП")]
      [MaxLength(1000, ErrorMessage = "\"Код МСП\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Код МСП\" не может быть пустым")]
      [DataMember(Name = "mSPCode", EmitDefaultValue = false)]
      public virtual string MSPCode { get; set; }
      
      
      /// <summary>
      /// МСП -> ЛК ИС Социум
      /// </summary>
      [InverseProperty("TypesOfRestERL")]
      [DataMember(Name = "benefitTypesERL", EmitDefaultValue = false)]
      public virtual ICollection<BenefitTypeERL> BenefitTypesERL { get; set; }
      
      /// <summary>
      /// Цели обращения в АИС ДО -> МСП в ИС Социум
      /// </summary>
      [InverseProperty("TypeOfRestERL")]
      [DataMember(Name = "typesOfRest", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRest> TypesOfRest { get; set; }

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