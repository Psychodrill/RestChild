// File:    OrganisatorCollaborator.cs
// Purpose: Definition of Class OrganisatorCollaborator

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Cотрудник учреждения социальной защиты
   /// </summary>
   [Serializable]
   [DataContract(Name = "organisatorCollaborator")]
   public partial class OrganisatorCollaborator : IEntityBase
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
      /// Дополнительный телефон
      /// </summary>
      [Display(Description = "Дополнительный телефон")]
      [MaxLength(1000, ErrorMessage = "\"Дополнительный телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "aditionalPhone", EmitDefaultValue = false)]
      public virtual string AditionalPhone { get; set; }
      
      /// <summary>
      /// Должность в учреждении
      /// </summary>
      [Display(Description = "Должность в учреждении")]
      [MaxLength(1000, ErrorMessage = "\"Должность в учреждении\" не может быть больше 1000 символов")]
      [DataMember(Name = "organisationPosition", EmitDefaultValue = false)]
      public virtual string OrganisationPosition { get; set; }
      
      /// <summary>
      /// Все необходимые данные заполнены
      /// </summary>
      [Display(Description = "Все необходимые данные заполнены")]
      [Required(ErrorMessage = "\"Все необходимые данные заполнены\" должно быть заполнено")]
      [DataMember(Name = "filled", EmitDefaultValue = false)]
      public virtual bool Filled { get; set; }
      
      
      /// <summary>
      /// Должность в оздоровительной организации
      /// </summary>
      [ForeignKey("WellnessOrganisationPosition")]
      [DataMember(Name = "wellnessOrganisationPositionId")][Display(Description = "Должность в оздоровительной организации")]
      public virtual long? WellnessOrganisationPositionId { get; set; }
      /// <summary>
      /// Должность в оздоровительной организации
      /// </summary>
      [InverseProperty("OrganisatonCollaborator")]
      [Display(Description = "Должность в оздоровительной организации")]
      [DataMember(Name = "wellnessOrganisationPosition", EmitDefaultValue = false)]
      public virtual TypeOfLinkPeople WellnessOrganisationPosition { get; set; }
      
      /// <summary>
      /// Связь сотрудника социальной защитой с адресом орагнизации социальной защиты
      /// </summary>
      [ForeignKey("OrganisatonAddress")]
      [DataMember(Name = "organisatonAddressId")][Display(Description = "Связь сотрудника социальной защитой с адресом орагнизации социальной защиты")]
      public virtual long? OrganisatonAddressId { get; set; }
      /// <summary>
      /// Связь сотрудника социальной защитой с адресом орагнизации социальной защиты
      /// </summary>
      [InverseProperty("OrganisatonCollaborators")]
      [Display(Description = "Связь сотрудника социальной защитой с адресом орагнизации социальной защиты")]
      [DataMember(Name = "organisatonAddress", EmitDefaultValue = false)]
      public virtual OrphanageAddress OrganisatonAddress { get; set; }
      
      /// <summary>
      /// Связь сотрудника социальной защитой с (сопровождающим)
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Связь сотрудника социальной защитой с (сопровождающим)")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Связь сотрудника социальной защитой с (сопровождающим)
      /// </summary>
      [Display(Description = "Связь сотрудника социальной защитой с (сопровождающим)")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Тип работника
      /// </summary>
      [ForeignKey("Position")]
      [DataMember(Name = "positionId")]
      [Display(Description = "Тип работника")]
      public virtual long? PositionId { get; set; }
      
      
      /// <summary>
      /// Тип работника
      /// </summary>
      [InverseProperty("OrganisatonCollaborator")]
      [Display(Description = "Тип работника")]
      [DataMember(Name = "position", EmitDefaultValue = false)]
      public virtual OrganizationCollaboratorPostType Position { get; set; }
      
      /// <summary>
      /// История изменений сотрудника учреждения социальной защиты
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История изменений сотрудника учреждения социальной защиты")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История изменений сотрудника учреждения социальной защиты
      /// </summary>
      [Display(Description = "История изменений сотрудника учреждения социальной защиты")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Связь сотрудника социальной защитой с орагнизацией социальной защиты
      /// </summary>
      [ForeignKey("Organisaton")]
      [DataMember(Name = "organisatonId")]
      [Display(Description = "Связь сотрудника социальной защитой с орагнизацией социальной защиты")]
      public virtual long? OrganisatonId { get; set; }
      
      
      /// <summary>
      /// Связь сотрудника социальной защитой с орагнизацией социальной защиты
      /// </summary>
      [InverseProperty("OrganisatonCollaborators")]
      [Display(Description = "Связь сотрудника социальной защитой с орагнизацией социальной защиты")]
      [DataMember(Name = "organisaton", EmitDefaultValue = false)]
      public virtual Organization Organisaton { get; set; }
      
      /// <summary>
      /// Связь версий
      /// </summary>
      [ForeignKey("Entity")]
      [DataMember(Name = "entityId")]
      [Display(Description = "Связь версий")]
      public virtual long? EntityId { get; set; }
      
      
      /// <summary>
      /// Связь версий
      /// </summary>
      [Display(Description = "Связь версий")]
      [DataMember(Name = "entity", EmitDefaultValue = false)]
      public virtual OrganisatorCollaborator Entity { get; set; }

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