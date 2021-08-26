// File:    OrphanageAddress.cs
// Purpose: Definition of Class OrphanageAddress

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Адрес детского дома
   /// </summary>
   [Serializable]
   [DataContract(Name = "orphanageAddress")]
   public partial class OrphanageAddress : IEntityBase
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
      /// Наличие огороженной территории
      /// </summary>
      [Display(Description = "Наличие огороженной территории")]
      [Required(ErrorMessage = "\"Наличие огороженной территории\" должно быть заполнено")]
      [DataMember(Name = "fencedArea", EmitDefaultValue = false)]
      public virtual bool FencedArea { get; set; }
      
      /// <summary>
      /// Возможность для разворота ТС
      /// </summary>
      [Display(Description = "Возможность для разворота ТС")]
      [Required(ErrorMessage = "\"Возможность для разворота ТС\" должно быть заполнено")]
      [DataMember(Name = "largeParking", EmitDefaultValue = false)]
      public virtual bool LargeParking { get; set; }
      
      
      /// <summary>
      /// Связь адреса учереждения социальной защиты с Адресом
      /// </summary>
      [ForeignKey("Address")]
      [DataMember(Name = "addressId")][Display(Description = "Связь адреса учереждения социальной защиты с Адресом")]
      public virtual long? AddressId { get; set; }
      /// <summary>
      /// Связь адреса учереждения социальной защиты с Адресом
      /// </summary>
      [Display(Description = "Связь адреса учереждения социальной защиты с Адресом")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual Address Address { get; set; }
      
      /// <summary>
      /// Воспитанник -> Адрес учреждение социальной защиты
      /// </summary>
      [InverseProperty("OrphanageAddress")]
      [DataMember(Name = "pupils", EmitDefaultValue = false)]
      public virtual ICollection<Pupil> Pupils { get; set; }
      
      /// <summary>
      /// Связь сопровождающего с местом трансфера (список по группе отправки (потребности))
      /// </summary>
      [InverseProperty("OrganisatonAddres")]
      [DataMember(Name = "pupilGroupListCollaborators", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListCollaborator> PupilGroupListCollaborators { get; set; }
      
      /// <summary>
      /// Связь воспитаннника с местом трансфера (список по группе отправки (потребности))
      /// </summary>
      [InverseProperty("OrganisatonAddres")]
      [DataMember(Name = "pupilGroupListPupils", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListMember> PupilGroupListPupils { get; set; }
      
      /// <summary>
      /// Адреса учереждения социальной защиты
      /// </summary>
      [ForeignKey("Organisation")]
      [DataMember(Name = "organisationId")]
      [Display(Description = "Адреса учереждения социальной защиты")]
      public virtual long? OrganisationId { get; set; }
      
      
      /// <summary>
      /// Адреса учереждения социальной защиты
      /// </summary>
      [InverseProperty("OrphanageOrganizationAddresses")]
      [Display(Description = "Адреса учереждения социальной защиты")]
      [DataMember(Name = "organisation", EmitDefaultValue = false)]
      public virtual Organization Organisation { get; set; }
      
      /// <summary>
      /// Связь сотрудника социальной защитой с адресом орагнизации социальной защиты
      /// </summary>
      [InverseProperty("OrganisatonAddress")]
      [DataMember(Name = "organisatonCollaborators", EmitDefaultValue = false)]
      public virtual ICollection<OrganisatorCollaborator> OrganisatonCollaborators { get; set; }

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