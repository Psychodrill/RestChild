// File:    MonitoringSmallLeisureInfoData.cs
// Purpose: Definition of Class MonitoringSmallLeisureInfoData

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Данные формы о малых формах досуга
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringSmallLeisureInfoData")]
   public partial class MonitoringSmallLeisureInfoData : IEntityBase
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
      /// Количество проведенных малых формах досуга (занятости) детей
      /// </summary>
      [Display(Description = "Количество проведенных малых формах досуга (занятости) детей")]
      [DataMember(Name = "childrenCountPost", EmitDefaultValue = false)]
      public virtual int? ChildrenCountPost { get; set; }
      
      /// <summary>
      /// Численность детей, охваченных малыми формами досуга (занятости)
      /// </summary>
      [Display(Description = "Численность детей, охваченных малыми формами досуга (занятости)")]
      [DataMember(Name = "childernCountCovered", EmitDefaultValue = false)]
      public virtual int? ChildernCountCovered { get; set; }
      
      /// <summary>
      /// Объем финансовых средств, затраченных на организацию малых форм досуга (занятости) (тыс.руб.)
      /// </summary>
      [Display(Description = "Объем финансовых средств, затраченных на организацию малых форм досуга (занятости) (тыс.руб.)")]
      [DataMember(Name = "moneyOutcome", EmitDefaultValue = false)]
      public virtual decimal? MoneyOutcome { get; set; }
      
      /// <summary>
      /// Примечание1
      /// </summary>
      [Display(Description = "Примечание1")]
      [MaxLength(1000, ErrorMessage = "\"Примечание1\" не может быть больше 1000 символов")]
      [DataMember(Name = "noteOne", EmitDefaultValue = false)]
      public virtual string NoteOne { get; set; }
      
      /// <summary>
      /// Примечание2
      /// </summary>
      [Display(Description = "Примечание2")]
      [MaxLength(1000, ErrorMessage = "\"Примечание2\" не может быть больше 1000 символов")]
      [DataMember(Name = "noteTwo", EmitDefaultValue = false)]
      public virtual string NoteTwo { get; set; }
      
      /// <summary>
      /// Примечание3
      /// </summary>
      [Display(Description = "Примечание3")]
      [MaxLength(1000, ErrorMessage = "\"Примечание3\" не может быть больше 1000 символов")]
      [DataMember(Name = "noteThree", EmitDefaultValue = false)]
      public virtual string NoteThree { get; set; }
      
      
      /// <summary>
      /// Связь ГБУ формы данных о малых формах досуга с данными о малыйх формах досуга
      /// </summary>
      [ForeignKey("MonitoringSmallLeisureInfoGBU")]
      [DataMember(Name = "monitoringSmallLeisureInfoGBUId")][Display(Description = "Связь ГБУ формы данных о малых формах досуга с данными о малыйх формах досуга")]
      public virtual long? MonitoringSmallLeisureInfoGBUId { get; set; }
      /// <summary>
      /// Связь ГБУ формы данных о малых формах досуга с данными о малыйх формах досуга
      /// </summary>
      [InverseProperty("MonitoringSmallLeisureInfoDatas")]
      [Display(Description = "Связь ГБУ формы данных о малых формах досуга с данными о малыйх формах досуга")]
      [DataMember(Name = "monitoringSmallLeisureInfoGBU", EmitDefaultValue = false)]
      public virtual MonitoringSmallLeisureInfoGBU MonitoringSmallLeisureInfoGBU { get; set; }
      
      /// <summary>
      /// Данные формы о малых формах досуга (связь с типом малых форм досуга)
      /// </summary>
      [ForeignKey("SmallLeisureType")]
      [DataMember(Name = "smallLeisureTypeId")]
      [Display(Description = "Данные формы о малых формах досуга (связь с типом малых форм досуга)")]
      public virtual long? SmallLeisureTypeId { get; set; }
      
      
      /// <summary>
      /// Данные формы о малых формах досуга (связь с типом малых форм досуга)
      /// </summary>
      [Display(Description = "Данные формы о малых формах досуга (связь с типом малых форм досуга)")]
      [DataMember(Name = "smallLeisureType", EmitDefaultValue = false)]
      public virtual SmallLeisureType SmallLeisureType { get; set; }
      
      /// <summary>
      /// Данные формы о малых формах досуга (связь с подтипом малых форм досуга)
      /// </summary>
      [ForeignKey("SmallLeisureSubtype")]
      [DataMember(Name = "smallLeisureSubtypeId")]
      [Display(Description = "Данные формы о малых формах досуга (связь с подтипом малых форм досуга)")]
      public virtual long? SmallLeisureSubtypeId { get; set; }
      
      
      /// <summary>
      /// Данные формы о малых формах досуга (связь с подтипом малых форм досуга)
      /// </summary>
      [Display(Description = "Данные формы о малых формах досуга (связь с подтипом малых форм досуга)")]
      [DataMember(Name = "smallLeisureSubtype", EmitDefaultValue = false)]
      public virtual SmallLeisureSubtype SmallLeisureSubtype { get; set; }

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