// File:    PupilGroupListMember.cs
// Purpose: Definition of Class PupilGroupListMember

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Воспитанник (список по группе отправки (потребности))
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilGroupListMember")]
   public partial class PupilGroupListMember : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентифкатор
      /// </summary>
      [Display(Description = "Уникальный идентифкатор")]
      [Required(ErrorMessage = "\"Уникальный идентифкатор\" должно быть заполнено")]
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
      /// Доза воспитанника в потребности на группу отправки
      /// </summary>
      [InverseProperty("GroupPupil")]
      [DataMember(Name = "groupPupilDoses", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListMemberDrugDose> GroupPupilDoses { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Воспитанники
      /// </summary>
      [ForeignKey("GroupRequestList")]
      [DataMember(Name = "groupRequestListId")]
      [Display(Description = "Заявка на период отдыха -> Воспитанники")]
      public virtual long? GroupRequestListId { get; set; }
      
      
      /// <summary>
      /// Заявка на период отдыха -> Воспитанники
      /// </summary>
      [InverseProperty("GroupPupils")]
      [Display(Description = "Заявка на период отдыха -> Воспитанники")]
      [DataMember(Name = "groupRequestList", EmitDefaultValue = false)]
      public virtual ListOfChilds GroupRequestList { get; set; }
      
      /// <summary>
      /// Воспитанник (Заявка на период отдыха) -> Воспитанники
      /// </summary>
      [ForeignKey("Pupil")]
      [DataMember(Name = "pupilId")]
      [Display(Description = "Воспитанник (Заявка на период отдыха) -> Воспитанники")]
      public virtual long? PupilId { get; set; }
      
      
      /// <summary>
      /// Воспитанник (Заявка на период отдыха) -> Воспитанники
      /// </summary>
      [Display(Description = "Воспитанник (Заявка на период отдыха) -> Воспитанники")]
      [DataMember(Name = "pupil", EmitDefaultValue = false)]
      public virtual Pupil Pupil { get; set; }
      
      /// <summary>
      /// Связь воспитаннника с местом трансфера (список по группе отправки (потребности))
      /// </summary>
      [ForeignKey("OrganisatonAddres")]
      [DataMember(Name = "organisatonAddresId")]
      [Display(Description = "Связь воспитаннника с местом трансфера (список по группе отправки (потребности))")]
      public virtual long? OrganisatonAddresId { get; set; }
      
      
      /// <summary>
      /// Связь воспитаннника с местом трансфера (список по группе отправки (потребности))
      /// </summary>
      [InverseProperty("PupilGroupListPupils")]
      [Display(Description = "Связь воспитаннника с местом трансфера (список по группе отправки (потребности))")]
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