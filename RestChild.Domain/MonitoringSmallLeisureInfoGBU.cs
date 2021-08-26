// File:    MonitoringSmallLeisureInfoGBU.cs
// Purpose: Definition of Class MonitoringSmallLeisureInfoGBU

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Мониторинг. Связанные ГБУ формы сведений о малых формах досуга
   /// </summary>
   [Serializable]
   [DataContract(Name = "monitoringSmallLeisureInfoGBU")]
   public partial class MonitoringSmallLeisureInfoGBU : IEntityBase
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
      /// Дата последнего обновления
      /// </summary>
      [Display(Description = "Дата последнего обновления")]
      [DataMember(Name = "lastUploadData", EmitDefaultValue = false)]
      public virtual DateTime? LastUploadData { get; set; }
      
      
      /// <summary>
      /// Мониторинг. Форма сведений о малых формах досуга (связь с ГБУ)
      /// </summary>
      [ForeignKey("MonitoringSmallLeisureInfo")]
      [DataMember(Name = "monitoringSmallLeisureInfoId")][Display(Description = "Мониторинг. Форма сведений о малых формах досуга (связь с ГБУ)")]
      public virtual long? MonitoringSmallLeisureInfoId { get; set; }
      /// <summary>
      /// Мониторинг. Форма сведений о малых формах досуга (связь с ГБУ)
      /// </summary>
      [InverseProperty("SmallLeisureInfoGBUs")]
      [Display(Description = "Мониторинг. Форма сведений о малых формах досуга (связь с ГБУ)")]
      [DataMember(Name = "monitoringSmallLeisureInfo", EmitDefaultValue = false)]
      public virtual MonitoringSmallLeisureInfo MonitoringSmallLeisureInfo { get; set; }
      
      /// <summary>
      /// Связь ГБУ формы данных о малых формах досуга с данными о малыйх формах досуга
      /// </summary>
      [InverseProperty("MonitoringSmallLeisureInfoGBU")]
      [DataMember(Name = "monitoringSmallLeisureInfoDatas", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringSmallLeisureInfoData> MonitoringSmallLeisureInfoDatas { get; set; }
      
      /// <summary>
      /// Мониторинг. ГБУ связанные с формой сведений о малых формах досуга  (связь с ГБУ)
      /// </summary>
      [ForeignKey("GBU")]
      [DataMember(Name = "gBUId")]
      [Display(Description = "Мониторинг. ГБУ связанные с формой сведений о малых формах досуга  (связь с ГБУ)")]
      public virtual long? GBUId { get; set; }
      
      
      /// <summary>
      /// Мониторинг. ГБУ связанные с формой сведений о малых формах досуга  (связь с ГБУ)
      /// </summary>
      [InverseProperty("SmallLeisureInfoConnectedGBUs")]
      [Display(Description = "Мониторинг. ГБУ связанные с формой сведений о малых формах досуга  (связь с ГБУ)")]
      [DataMember(Name = "gBU", EmitDefaultValue = false)]
      public virtual MonitoringGBU GBU { get; set; }

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