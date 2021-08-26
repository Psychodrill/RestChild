// File:    School.cs
// Purpose: Definition of Class School

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Образовательное учреждение
   /// </summary>
   [Serializable]
   [DataContract(Name = "school")]
   public partial class School : IEntityBase
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
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Внешний ключ
      /// </summary>
      [Display(Description = "Внешний ключ")]
      [DataMember(Name = "sourcePk", EmitDefaultValue = false)]
      public virtual long? SourcePk { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual long? Status { get; set; }
      
      /// <summary>
      /// Дата закрытия
      /// </summary>
      [Display(Description = "Дата закрытия")]
      [DataMember(Name = "closeDate", EmitDefaultValue = false)]
      public virtual DateTime? CloseDate { get; set; }
      
      /// <summary>
      /// Куда ушло
      /// </summary>
      [Display(Description = "Куда ушло")]
      [DataMember(Name = "toOrganizationId", EmitDefaultValue = false)]
      public virtual long? ToOrganizationId { get; set; }
      
      /// <summary>
      /// Глобальный идентификатор
      /// </summary>
      [Display(Description = "Глобальный идентификатор")]
      [DataMember(Name = "organizationGuid", EmitDefaultValue = false)]
      public virtual Guid? OrganizationGuid { get; set; }
      
      /// <summary>
      /// Дата изменения
      /// </summary>
      [Display(Description = "Дата изменения")]
      [Required(ErrorMessage = "\"Дата изменения\" должно быть заполнено")]
      [DataMember(Name = "dateChange", EmitDefaultValue = false)]
      public virtual DateTime DateChange { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "externalId", EmitDefaultValue = false)]
      public virtual long? ExternalId { get; set; }
      
      
      /// <summary>
      /// Воспитанник -> Образовательное учреждение
      /// </summary>
      [InverseProperty("School")]
      [DataMember(Name = "pupil", EmitDefaultValue = false)]
      public virtual ICollection<Pupil> Pupil { get; set; }

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