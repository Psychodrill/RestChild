// File:    RequestForPeriodOfRest.cs
// Purpose: Definition of Class RequestForPeriodOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявка на период отдыха
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestForPeriodOfRest")]
   public partial class RequestForPeriodOfRest : IEntityBase
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
      /// Кол-во воспитанников
      /// </summary>
      [Display(Description = "Кол-во воспитанников")]
      [Required(ErrorMessage = "\"Кол-во воспитанников\" должно быть заполнено")]
      [DataMember(Name = "pupilsCount", EmitDefaultValue = false)]
      public virtual int PupilsCount { get; set; }
      
      /// <summary>
      /// Кол-во сопровождающих от учреждения
      /// </summary>
      [Display(Description = "Кол-во сопровождающих от учреждения")]
      [Required(ErrorMessage = "\"Кол-во сопровождающих от учреждения\" должно быть заполнено")]
      [DataMember(Name = "collaboratorsCount", EmitDefaultValue = false)]
      public virtual int CollaboratorsCount { get; set; }
      
      /// <summary>
      /// Кол-во сопровождающих от МГТ
      /// </summary>
      [Display(Description = "Кол-во сопровождающих от МГТ")]
      [Required(ErrorMessage = "\"Кол-во сопровождающих от МГТ\" должно быть заполнено")]
      [DataMember(Name = "mGTCollaboratorsCount", EmitDefaultValue = false)]
      public virtual int MGTCollaboratorsCount { get; set; }
      
      /// <summary>
      /// Каникулы с
      /// </summary>
      [Display(Description = "Каникулы с")]
      [DataMember(Name = "vacationFrom", EmitDefaultValue = false)]
      public virtual DateTime? VacationFrom { get; set; }
      
      /// <summary>
      /// Каникулы по
      /// </summary>
      [Display(Description = "Каникулы по")]
      [DataMember(Name = "vacationTo", EmitDefaultValue = false)]
      public virtual DateTime? VacationTo { get; set; }
      
      
      /// <summary>
      /// Заявка на период отдыха -> Период отдыха
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")][Display(Description = "Заявка на период отдыха -> Период отдыха")]
      public virtual long? TimeOfRestId { get; set; }
      /// <summary>
      /// Заявка на период отдыха -> Период отдыха
      /// </summary>
      [Display(Description = "Заявка на период отдыха -> Период отдыха")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Регион отдыха
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")][Display(Description = "Заявка на период отдыха -> Регион отдыха")]
      public virtual long? PlaceOfRestId { get; set; }
      /// <summary>
      /// Заявка на период отдыха -> Регион отдыха
      /// </summary>
      [Display(Description = "Заявка на период отдыха -> Регион отдыха")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Размещение
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")][Display(Description = "Заявка на период отдыха -> Размещение")]
      public virtual long? TourId { get; set; }
      /// <summary>
      /// Заявка на период отдыха -> Размещение
      /// </summary>
      [Display(Description = "Заявка на период отдыха -> Размещение")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Заявка на период отдыха
      /// </summary>
      [ForeignKey("PupilGroup")]
      [DataMember(Name = "pupilGroupId")]
      [Display(Description = "Группа (потребность) -> Заявка на период отдыха")]
      public virtual long? PupilGroupId { get; set; }
      
      
      /// <summary>
      /// Группа (потребность) -> Заявка на период отдыха
      /// </summary>
      [InverseProperty("Requests")]
      [Display(Description = "Группа (потребность) -> Заявка на период отдыха")]
      [DataMember(Name = "pupilGroup", EmitDefaultValue = false)]
      public virtual PupilGroup PupilGroup { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Список
      /// </summary>
      [ForeignKey("Lists")]
      [DataMember(Name = "listsId")]
      [Display(Description = "Заявка на период отдыха -> Список")]
      public virtual long? ListsId { get; set; }
      
      
      /// <summary>
      /// Заявка на период отдыха -> Список
      /// </summary>
      [InverseProperty("PupilGroupRequest")]
      [Display(Description = "Заявка на период отдыха -> Список")]
      [DataMember(Name = "lists", EmitDefaultValue = false)]
      public virtual ListOfChilds Lists { get; set; }

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