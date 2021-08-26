// File:    PupilGroupListCollaborator.cs
// Purpose: Definition of Class PupilGroupListCollaborator

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Сопровождающие (список по группе отправки (потребности))
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilGroupListCollaborator")]
   public partial class PupilGroupListCollaborator : IEntityBase
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
      /// Билет туда
      /// </summary>
      [Display(Description = "Билет туда")]
      [Required(ErrorMessage = "\"Билет туда\" должно быть заполнено")]
      [DataMember(Name = "ticketTo", EmitDefaultValue = false)]
      public virtual bool TicketTo { get; set; }
      
      /// <summary>
      /// Билет обратно
      /// </summary>
      [Display(Description = "Билет обратно")]
      [Required(ErrorMessage = "\"Билет обратно\" должно быть заполнено")]
      [DataMember(Name = "ticketFrom", EmitDefaultValue = false)]
      public virtual bool TicketFrom { get; set; }
      
      
      /// <summary>
      /// Сопровождающие (Заявка на период отдыха) -> Сотрудник приюта
      /// </summary>
      [ForeignKey("OrganisatonCollaborator")]
      [DataMember(Name = "organisatonCollaboratorId")][Display(Description = "Сопровождающие (Заявка на период отдыха) -> Сотрудник приюта")]
      public virtual long? OrganisatonCollaboratorId { get; set; }
      /// <summary>
      /// Сопровождающие (Заявка на период отдыха) -> Сотрудник приюта
      /// </summary>
      [Display(Description = "Сопровождающие (Заявка на период отдыха) -> Сотрудник приюта")]
      [DataMember(Name = "organisatonCollaborator", EmitDefaultValue = false)]
      public virtual OrganisatorCollaborator OrganisatonCollaborator { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Сопровождающие
      /// </summary>
      [ForeignKey("GroupRequestList")]
      [DataMember(Name = "groupRequestListId")]
      [Display(Description = "Заявка на период отдыха -> Сопровождающие")]
      public virtual long? GroupRequestListId { get; set; }
      
      
      /// <summary>
      /// Заявка на период отдыха -> Сопровождающие
      /// </summary>
      [InverseProperty("GroupCollaborators")]
      [Display(Description = "Заявка на период отдыха -> Сопровождающие")]
      [DataMember(Name = "groupRequestList", EmitDefaultValue = false)]
      public virtual ListOfChilds GroupRequestList { get; set; }
      
      /// <summary>
      /// Связь сопровождающего с местом трансфера (список по группе отправки (потребности))
      /// </summary>
      [ForeignKey("OrganisatonAddres")]
      [DataMember(Name = "organisatonAddresId")]
      [Display(Description = "Связь сопровождающего с местом трансфера (список по группе отправки (потребности))")]
      public virtual long? OrganisatonAddresId { get; set; }
      
      
      /// <summary>
      /// Связь сопровождающего с местом трансфера (список по группе отправки (потребности))
      /// </summary>
      [InverseProperty("PupilGroupListCollaborators")]
      [Display(Description = "Связь сопровождающего с местом трансфера (список по группе отправки (потребности))")]
      [DataMember(Name = "organisatonAddres", EmitDefaultValue = false)]
      public virtual OrphanageAddress OrganisatonAddres { get; set; }

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