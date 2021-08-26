// File:    PupilGroup.cs
// Purpose: Definition of Class PupilGroup

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Группа (потребность)
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilGroup")]
   public partial class PupilGroup : IEntityBase
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
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Общее кол-во мест для воспитанников
      /// </summary>
      [Display(Description = "Общее кол-во мест для воспитанников")]
      [DataMember(Name = "pupilsCount", EmitDefaultValue = false)]
      public virtual int? PupilsCount { get; set; }
      
      /// <summary>
      /// Общее кол-во мест для работников
      /// </summary>
      [Display(Description = "Общее кол-во мест для работников")]
      [DataMember(Name = "collaboratorsCount", EmitDefaultValue = false)]
      public virtual int? CollaboratorsCount { get; set; }
      
      /// <summary>
      /// Общее кол-во сопровождающих от МГТ
      /// </summary>
      [Display(Description = "Общее кол-во сопровождающих от МГТ")]
      [DataMember(Name = "mGTCollaboratorsCount", EmitDefaultValue = false)]
      public virtual int? MGTCollaboratorsCount { get; set; }
      
      
      /// <summary>
      /// Группа (потребность) -> Состояние здоровья воспитанников
      /// </summary>
      [InverseProperty("PupilGroup")]
      [DataMember(Name = "pupilsHealthStatuses", EmitDefaultValue = false)]
      public virtual ICollection<PupilsHealthStatus> PupilsHealthStatuses { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Заявка на период отдыха
      /// </summary>
      [InverseProperty("PupilGroup")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<RequestForPeriodOfRest> Requests { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Детский дом
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")][Display(Description = "Группа (потребность) -> Детский дом")]
      public virtual long? OrganizationId { get; set; }
      /// <summary>
      /// Группа (потребность) -> Детский дом
      /// </summary>
      [Display(Description = "Группа (потребность) -> Детский дом")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Воспитанник
      /// </summary>
      [InverseProperty("PupilGroups")]
      [DataMember(Name = "pupils", EmitDefaultValue = false)]
      public virtual ICollection<Pupil> Pupils { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")][Display(Description = "Группа (потребность) -> Статус")]
      public virtual long? StateId { get; set; }
      /// <summary>
      /// Группа (потребность) -> Статус
      /// </summary>
      [Display(Description = "Группа (потребность) -> Статус")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// История изменений Группы (потребности)
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")][Display(Description = "История изменений Группы (потребности)")]
      public virtual long? HistoryLinkId { get; set; }
      /// <summary>
      /// История изменений Группы (потребности)
      /// </summary>
      [Display(Description = "История изменений Группы (потребности)")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Каникулярный период
      /// </summary>
      [ForeignKey("VacationPeriod")]
      [DataMember(Name = "vacationPeriodId")][Display(Description = "Группа (потребность) -> Каникулярный период")]
      public virtual long? VacationPeriodId { get; set; }
      /// <summary>
      /// Группа (потребность) -> Каникулярный период
      /// </summary>
      [InverseProperty("PupilGroups")]
      [Display(Description = "Группа (потребность) -> Каникулярный период")]
      [DataMember(Name = "vacationPeriod", EmitDefaultValue = false)]
      public virtual PupilGroupVacationPeriod VacationPeriod { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Форма отдыха и оздоровления
      /// </summary>
      [ForeignKey("FormOfRest")]
      [DataMember(Name = "formOfRestId")]
      [Display(Description = "Группа (потребность) -> Форма отдыха и оздоровления")]
      public virtual long? FormOfRestId { get; set; }
      
      
      /// <summary>
      /// Группа (потребность) -> Форма отдыха и оздоровления
      /// </summary>
      [InverseProperty("PupilGroups")]
      [Display(Description = "Группа (потребность) -> Форма отдыха и оздоровления")]
      [DataMember(Name = "formOfRest", EmitDefaultValue = false)]
      public virtual FormOfRest FormOfRest { get; set; }
      
      /// <summary>
      /// Группа -> Год потребности
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Группа -> Год потребности")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Группа -> Год потребности
      /// </summary>
      [Display(Description = "Группа -> Год потребности")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }

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