﻿// File:    ListOfChildsCategory.cs
// Purpose: Definition of Class ListOfChildsCategory

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Категория списка
   /// </summary>
   [Serializable]
   [DataContract(Name = "listOfChildsCategory")]
   public partial class ListOfChildsCategory : IEntityBase
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
      /// Активность
      /// </summary>
      [Display(Description = "Активность")]
      [Required(ErrorMessage = "\"Активность\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      
      /// <summary>
      /// Приминимость категории списка
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Приминимость категории списка")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Приминимость категории списка
      /// </summary>
      [Display(Description = "Приминимость категории списка")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }

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