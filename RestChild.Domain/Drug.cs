﻿// File:    Drug.cs
// Purpose: Definition of Class Drug

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Наркотик
   /// </summary>
   [Serializable]
   [DataContract(Name = "drug")]
   public partial class Drug : IEntityBase
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
      /// Условия хранения
      /// </summary>
      [Display(Description = "Условия хранения")]
      [MaxLength(1000, ErrorMessage = "\"Условия хранения\" не может быть больше 1000 символов")]
      [DataMember(Name = "storage", EmitDefaultValue = false)]
      public virtual string Storage { get; set; }
      
      
      /// <summary>
      /// Наркотик -> Тип
      /// </summary>
      [ForeignKey("DrugType")]
      [DataMember(Name = "drugTypeId")][Display(Description = "Наркотик -> Тип")]
      public virtual long? DrugTypeId { get; set; }
      /// <summary>
      /// Наркотик -> Тип
      /// </summary>
      [InverseProperty("Drugs")]
      [Display(Description = "Наркотик -> Тип")]
      [DataMember(Name = "drugType", EmitDefaultValue = false)]
      public virtual TypeOfDrug DrugType { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Drug")]
      [DataMember(Name = "doses", EmitDefaultValue = false)]
      public virtual ICollection<PupilDose> Doses { get; set; }

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