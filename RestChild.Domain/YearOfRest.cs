// File:    YearOfRest.cs
// Purpose: Definition of Class YearOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Год  кампании
   /// </summary>
   [Serializable]
   [DataContract(Name = "yearOfRest")]
   public partial class YearOfRest : IEntityBase
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
      /// Наименование года отдыха
      /// </summary>
      [Display(Description = "Наименование года отдыха")]
      [MaxLength(1000, ErrorMessage = "\"Наименование года отдыха\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование года отдыха\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Год
      /// </summary>
      [Display(Description = "Год")]
      [Required(ErrorMessage = "\"Год\" должно быть заполнено")]
      [DataMember(Name = "year", EmitDefaultValue = false)]
      public virtual int Year { get; set; }
      
      /// <summary>
      /// Признак что завершена
      /// </summary>
      [Display(Description = "Признак что завершена")]
      [Required(ErrorMessage = "\"Признак что завершена\" должно быть заполнено")]
      [DataMember(Name = "isClosed", EmitDefaultValue = false)]
      public virtual bool IsClosed { get; set; }
      
      /// <summary>
      /// Дата открытия первого этапа
      /// </summary>
      [Display(Description = "Дата открытия первого этапа")]
      [DataMember(Name = "dateFirstStage", EmitDefaultValue = false)]
      public virtual DateTime? DateFirstStage { get; set; }
      
      /// <summary>
      /// Дата закрытия первого этапа
      /// </summary>
      [Display(Description = "Дата закрытия первого этапа")]
      [DataMember(Name = "dateFirstStageClose", EmitDefaultValue = false)]
      public virtual DateTime? DateFirstStageClose { get; set; }
      
      /// <summary>
      /// Дата открытия второго этапа
      /// </summary>
      [Display(Description = "Дата открытия второго этапа")]
      [DataMember(Name = "dateSecondStage", EmitDefaultValue = false)]
      public virtual DateTime? DateSecondStage { get; set; }
      
      /// <summary>
      /// Дата закрытия второго этапа
      /// </summary>
      [Display(Description = "Дата закрытия второго этапа")]
      [DataMember(Name = "dateSecondStageClose", EmitDefaultValue = false)]
      public virtual DateTime? DateSecondStageClose { get; set; }
      
      /// <summary>
      /// Завершен прием заявок
      /// </summary>
      [Display(Description = "Завершен прием заявок")]
      [Required(ErrorMessage = "\"Завершен прием заявок\" должно быть заполнено")]
      [DataMember(Name = "receptionOfApplicationsCompleted", EmitDefaultValue = false)]
      public virtual bool ReceptionOfApplicationsCompleted { get; set; }
      
      /// <summary>
      /// Список сформирован
      /// </summary>
      [Display(Description = "Список сформирован")]
      [Required(ErrorMessage = "\"Список сформирован\" должно быть заполнено")]
      [DataMember(Name = "listComplited", EmitDefaultValue = false)]
      public virtual bool ListComplited { get; set; }
      
      /// <summary>
      /// Размещения открыты
      /// </summary>
      [Display(Description = "Размещения открыты")]
      [Required(ErrorMessage = "\"Размещения открыты\" должно быть заполнено")]
      [DataMember(Name = "tourOpened", EmitDefaultValue = false)]
      public virtual bool TourOpened { get; set; }
      
      
      /// <summary>
      /// Стаус списка
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Стаус списка")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Стаус списка
      /// </summary>
      [Display(Description = "Стаус списка")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("SignInfo")]
      [DataMember(Name = "signInfoId")]
      [Display(Description = "")]
      public virtual long? SignInfoId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "signInfo", EmitDefaultValue = false)]
      public virtual SignInfo SignInfo { get; set; }
      
      /// <summary>
      /// Год компании -> цена 1 путёвки
      /// </summary>
      [InverseProperty("YearOfRest")]
      [DataMember(Name = "prices", EmitDefaultValue = false)]
      public virtual ICollection<AverageRestPrice> Prices { get; set; }

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