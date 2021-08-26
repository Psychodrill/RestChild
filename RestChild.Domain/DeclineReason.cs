// File:    DeclineReason.cs
// Purpose: Definition of Class DeclineReason

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Причины отказа заявлению
   /// </summary>
   [Serializable]
   [DataContract(Name = "declineReason")]
   public partial class DeclineReason : IEntityBase
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
      /// Наименование причины
      /// </summary>
      [Display(Description = "Наименование причины")]
      [MaxLength(1000, ErrorMessage = "\"Наименование причины\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование причины\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Причина активна
      /// </summary>
      [Display(Description = "Причина активна")]
      [Required(ErrorMessage = "\"Причина активна\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Причина первого этапа
      /// </summary>
      [Display(Description = "Причина первого этапа")]
      [Required(ErrorMessage = "\"Причина первого этапа\" должно быть заполнено")]
      [DataMember(Name = "firstStage", EmitDefaultValue = false)]
      public virtual bool FirstStage { get; set; }
      
      /// <summary>
      /// Причина второго этапа
      /// </summary>
      [Display(Description = "Причина второго этапа")]
      [Required(ErrorMessage = "\"Причина второго этапа\" должно быть заполнено")]
      [DataMember(Name = "secondStage", EmitDefaultValue = false)]
      public virtual bool SecondStage { get; set; }
      
      /// <summary>
      /// Доступна для ручного указания
      /// </summary>
      [Display(Description = "Доступна для ручного указания")]
      [Required(ErrorMessage = "\"Доступна для ручного указания\" должно быть заполнено")]
      [DataMember(Name = "isManual", EmitDefaultValue = false)]
      public virtual bool IsManual { get; set; }
      
      /// <summary>
      /// Для льготных путевок
      /// </summary>
      [Display(Description = "Для льготных путевок")]
      [Required(ErrorMessage = "\"Для льготных путевок\" должно быть заполнено")]
      [DataMember(Name = "forPreferential", EmitDefaultValue = false)]
      public virtual bool ForPreferential { get; set; }
      
      /// <summary>
      /// Для коммерческих путевок
      /// </summary>
      [Display(Description = "Для коммерческих путевок")]
      [Required(ErrorMessage = "\"Для коммерческих путевок\" должно быть заполнено")]
      [DataMember(Name = "forCommerce", EmitDefaultValue = false)]
      public virtual bool ForCommerce { get; set; }
      
      /// <summary>
      /// Уважительная причина
      /// </summary>
      [Display(Description = "Уважительная причина")]
      [Required(ErrorMessage = "\"Уважительная причина\" должно быть заполнено")]
      [DataMember(Name = "validReasons", EmitDefaultValue = false)]
      public virtual bool ValidReasons { get; set; }
      
      
      /// <summary>
      /// Связь причин отказа с видом отдыха
      /// </summary>
      [InverseProperty("DeclineReasons")]
      [DataMember(Name = "typeOfRests", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRest> TypeOfRests { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("Status")]
      [DataMember(Name = "statusId")]
      [Display(Description = "Статус")]
      public virtual long? StatusId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual Status Status { get; set; }

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