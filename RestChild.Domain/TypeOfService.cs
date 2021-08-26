// File:    TypeOfService.cs
// Purpose: Definition of Class TypeOfService

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вид услуги
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfService")]
   public partial class TypeOfService : IEntityBase
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
      /// Наименование вида услуги
      /// </summary>
      [Display(Description = "Наименование вида услуги")]
      [MaxLength(1000, ErrorMessage = "\"Наименование вида услуги\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование вида услуги\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активность записи
      /// </summary>
      [Display(Description = "Активность записи")]
      [Required(ErrorMessage = "\"Активность записи\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Разовая
      /// </summary>
      [Display(Description = "Разовая")]
      [Required(ErrorMessage = "\"Разовая\" должно быть заполнено")]
      [DataMember(Name = "single", EmitDefaultValue = false)]
      public virtual bool Single { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needSize", EmitDefaultValue = false)]
      public virtual bool NeedSize { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needConditions", EmitDefaultValue = false)]
      public virtual bool NeedConditions { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needAnnouncement", EmitDefaultValue = false)]
      public virtual bool NeedAnnouncement { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needName", EmitDefaultValue = false)]
      public virtual bool NeedName { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needDescription", EmitDefaultValue = false)]
      public virtual bool NeedDescription { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needDurationHour", EmitDefaultValue = false)]
      public virtual bool NeedDurationHour { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needDurationDay", EmitDefaultValue = false)]
      public virtual bool NeedDurationDay { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needDurationMonth", EmitDefaultValue = false)]
      public virtual bool NeedDurationMonth { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "needDurationYear", EmitDefaultValue = false)]
      public virtual bool NeedDurationYear { get; set; }
      
      /// <summary>
      /// Можно по умолчанию
      /// </summary>
      [Display(Description = "Можно по умолчанию")]
      [Required(ErrorMessage = "\"Можно по умолчанию\" должно быть заполнено")]
      [DataMember(Name = "mayByDefault", EmitDefaultValue = false)]
      public virtual bool MayByDefault { get; set; }
      
      /// <summary>
      /// Можно обязательность
      /// </summary>
      [Display(Description = "Можно обязательность")]
      [Required(ErrorMessage = "\"Можно обязательность\" должно быть заполнено")]
      [DataMember(Name = "mayRequared", EmitDefaultValue = false)]
      public virtual bool MayRequared { get; set; }
      
      /// <summary>
      /// Можно только с проживанием
      /// </summary>
      [Display(Description = "Можно только с проживанием")]
      [Required(ErrorMessage = "\"Можно только с проживанием\" должно быть заполнено")]
      [DataMember(Name = "mayWithAccomodation", EmitDefaultValue = false)]
      public virtual bool MayWithAccomodation { get; set; }
      
      /// <summary>
      /// Можно требует подтверждение
      /// </summary>
      [Display(Description = "Можно требует подтверждение")]
      [Required(ErrorMessage = "\"Можно требует подтверждение\" должно быть заполнено")]
      [DataMember(Name = "mayMustApprove", EmitDefaultValue = false)]
      public virtual bool MayMustApprove { get; set; }
      
      /// <summary>
      /// Нужно выбрать транспорт
      /// </summary>
      [Display(Description = "Нужно выбрать транспорт")]
      [Required(ErrorMessage = "\"Нужно выбрать транспорт\" должно быть заполнено")]
      [DataMember(Name = "needTransport", EmitDefaultValue = false)]
      public virtual bool NeedTransport { get; set; }
      
      
      /// <summary>
      /// Ответсвенный пользователь
      /// </summary>
      [ForeignKey("Curator")]
      [DataMember(Name = "curatorId")]
      [Display(Description = "Ответсвенный пользователь")]
      public virtual long? CuratorId { get; set; }
      
      
      /// <summary>
      /// Ответсвенный пользователь
      /// </summary>
      [Display(Description = "Ответсвенный пользователь")]
      [DataMember(Name = "curator", EmitDefaultValue = false)]
      public virtual Account Curator { get; set; }

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