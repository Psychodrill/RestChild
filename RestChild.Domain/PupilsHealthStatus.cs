// File:    PupilsHealthStatus.cs
// Purpose: Definition of Class PupilsHealthStatus

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Состояние здоровья воспитанников
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilsHealthStatus")]
   public partial class PupilsHealthStatus : IEntityBase
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
      /// Кол-во воспитанников с указанными ограничениями
      /// </summary>
      [Display(Description = "Кол-во воспитанников с указанными ограничениями")]
      [DataMember(Name = "pupilsCount", EmitDefaultValue = false)]
      public virtual int? PupilsCount { get; set; }
      
      
      /// <summary>
      /// Вид ограничения
      /// </summary>
      [ForeignKey("TypeOfRestriction")]
      [DataMember(Name = "typeOfRestrictionId")][Display(Description = "Вид ограничения")]
      public virtual long? TypeOfRestrictionId { get; set; }
      /// <summary>
      /// Вид ограничения
      /// </summary>
      [Display(Description = "Вид ограничения")]
      [DataMember(Name = "typeOfRestriction", EmitDefaultValue = false)]
      public virtual TypeOfRestriction TypeOfRestriction { get; set; }
      
      /// <summary>
      /// Подвид ограничения
      /// </summary>
      [ForeignKey("TypeOfSubRestriction")]
      [DataMember(Name = "typeOfSubRestrictionId")]
      [Display(Description = "Подвид ограничения")]
      public virtual long? TypeOfSubRestrictionId { get; set; }
      
      
      /// <summary>
      /// Подвид ограничения
      /// </summary>
      [Display(Description = "Подвид ограничения")]
      [DataMember(Name = "typeOfSubRestriction", EmitDefaultValue = false)]
      public virtual TypeOfSubRestriction TypeOfSubRestriction { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Состояние здоровья воспитанников
      /// </summary>
      [ForeignKey("PupilGroup")]
      [DataMember(Name = "pupilGroupId")]
      [Display(Description = "Группа (потребность) -> Состояние здоровья воспитанников")]
      public virtual long? PupilGroupId { get; set; }
      
      
      /// <summary>
      /// Группа (потребность) -> Состояние здоровья воспитанников
      /// </summary>
      [InverseProperty("PupilsHealthStatuses")]
      [Display(Description = "Группа (потребность) -> Состояние здоровья воспитанников")]
      [DataMember(Name = "pupilGroup", EmitDefaultValue = false)]
      public virtual PupilGroup PupilGroup { get; set; }

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