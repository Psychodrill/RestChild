// File:    BenefitType.cs
// Purpose: Definition of Class BenefitType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вид льготы
   /// </summary>
   [Serializable]
   [DataContract(Name = "benefitType")]
   public partial class BenefitType : IEntityBase
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
      /// Внешний идентификатор льготы
      /// </summary>
      [Display(Description = "Внешний идентификатор льготы")]
      [MaxLength(1000, ErrorMessage = "\"Внешний идентификатор льготы\" не может быть больше 1000 символов")]
      [DataMember(Name = "exnternalUid", EmitDefaultValue = false)]
      public virtual string ExnternalUid { get; set; }
      
      /// <summary>
      /// Необходимо вид ограничение
      /// </summary>
      [Display(Description = "Необходимо вид ограничение")]
      [Required(ErrorMessage = "\"Необходимо вид ограничение\" должно быть заполнено")]
      [DataMember(Name = "needTypeOfRestriction", EmitDefaultValue = false)]
      public virtual bool NeedTypeOfRestriction { get; set; }
      
      /// <summary>
      /// Необходим подтверждающий документ
      /// </summary>
      [Display(Description = "Необходим подтверждающий документ")]
      [Required(ErrorMessage = "\"Необходим подтверждающий документ\" должно быть заполнено")]
      [DataMember(Name = "needApproveDocument", EmitDefaultValue = false)]
      public virtual bool NeedApproveDocument { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Не для МПГУ
      /// </summary>
      [Display(Description = "Не для МПГУ")]
      [Required(ErrorMessage = "\"Не для МПГУ\" должно быть заполнено")]
      [DataMember(Name = "forAisoOnly", EmitDefaultValue = false)]
      public virtual bool ForAisoOnly { get; set; }
      
      
      /// <summary>
      /// Связь с видом отдыха
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Связь с видом отдыха")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь с видом отдыха
      /// </summary>
      [InverseProperty("BenefitTypes")]
      [Display(Description = "Связь с видом отдыха")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }
      
      /// <summary>
      /// Такая же льгота
      /// </summary>
      [ForeignKey("SameBenefit")]
      [DataMember(Name = "sameBenefitId")]
      [Display(Description = "Такая же льгота")]
      public virtual long? SameBenefitId { get; set; }
      
      
      /// <summary>
      /// Такая же льгота
      /// </summary>
      [Display(Description = "Такая же льгота")]
      [DataMember(Name = "sameBenefit", EmitDefaultValue = false)]
      public virtual BenefitType SameBenefit { get; set; }
      
      /// <summary>
      /// Вид группы проверки
      /// </summary>
      [ForeignKey("TypeOfGroupCheck")]
      [DataMember(Name = "typeOfGroupCheckId")]
      [Display(Description = "Вид группы проверки")]
      public virtual long? TypeOfGroupCheckId { get; set; }
      
      
      /// <summary>
      /// Вид группы проверки
      /// </summary>
      [Display(Description = "Вид группы проверки")]
      [DataMember(Name = "typeOfGroupCheck", EmitDefaultValue = false)]
      public virtual TypeOfGroupCheck TypeOfGroupCheck { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }
      
      /// <summary>
      /// Вид льготы АИС ДО -> ЛК в ИС Социум
      /// </summary>
      [ForeignKey("BenefitTypeERL")]
      [DataMember(Name = "benefitTypeERLId")]
      [Display(Description = "Вид льготы АИС ДО -> ЛК в ИС Социум")]
      public virtual long? BenefitTypeERLId { get; set; }
      
      
      /// <summary>
      /// Вид льготы АИС ДО -> ЛК в ИС Социум
      /// </summary>
      [InverseProperty("BenefitTypes")]
      [Display(Description = "Вид льготы АИС ДО -> ЛК в ИС Социум")]
      [DataMember(Name = "benefitTypeERL", EmitDefaultValue = false)]
      public virtual BenefitTypeERL BenefitTypeERL { get; set; }

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