// File:    FormOfRest.cs
// Purpose: Definition of Class FormOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Форма отдыха и оздоровления
   /// </summary>
   [Serializable]
   [DataContract(Name = "formOfRest")]
   public partial class FormOfRest : IEntityBase
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
      /// Удалён
      /// </summary>
      [Display(Description = "Удалён")]
      [Required(ErrorMessage = "\"Удалён\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Возраст От
      /// </summary>
      [Display(Description = "Возраст От")]
      [Required(ErrorMessage = "\"Возраст От\" должно быть заполнено")]
      [DataMember(Name = "ageFrom", EmitDefaultValue = false)]
      public virtual int AgeFrom { get; set; }
      
      /// <summary>
      /// Возраст До
      /// </summary>
      [Display(Description = "Возраст До")]
      [Required(ErrorMessage = "\"Возраст До\" должно быть заполнено")]
      [DataMember(Name = "ageTo", EmitDefaultValue = false)]
      public virtual int AgeTo { get; set; }
      
      
      /// <summary>
      /// Группа (потребность) -> Форма отдыха и оздоровления
      /// </summary>
      [InverseProperty("FormOfRest")]
      [DataMember(Name = "pupilGroups", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroup> PupilGroups { get; set; }
      
      /// <summary>
      /// Форма отдыха и оздоровления -> Цель обращения
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")][Display(Description = "Форма отдыха и оздоровления -> Цель обращения")]
      public virtual long? TypeOfRestId { get; set; }
      /// <summary>
      /// Форма отдыха и оздоровления -> Цель обращения
      /// </summary>
      [Display(Description = "Форма отдыха и оздоровления -> Цель обращения")]
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