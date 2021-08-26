// File:    RequestStatusCshedSendAndSignDocument.cs
// Purpose: Definition of Class RequestStatusCshedSendAndSignDocument

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявление. Отправка и подпись документов в РЦХЭД
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestStatusCshedSendAndSignDocument")]
   public partial class RequestStatusCshedSendAndSignDocument : IEntityBase
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
      /// Путь к документу
      /// </summary>
      [Display(Description = "Путь к документу")]
      [MaxLength(1000, ErrorMessage = "\"Путь к документу\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Путь к документу\" не может быть пустым")]
      [DataMember(Name = "documentPath", EmitDefaultValue = false)]
      public virtual string DocumentPath { get; set; }
      
      /// <summary>
      /// Необходимость подписания
      /// </summary>
      [Display(Description = "Необходимость подписания")]
      [Required(ErrorMessage = "\"Необходимость подписания\" должно быть заполнено")]
      [DataMember(Name = "signNeed", EmitDefaultValue = false)]
      public virtual bool SignNeed { get; set; }
      
      
      /// <summary>
      /// Связь в рамках отправки документов в РЦХЭД
      /// </summary>
      [ForeignKey("MpguStatus")]
      [DataMember(Name = "mpguStatusId")][Display(Description = "Связь в рамках отправки документов в РЦХЭД")]
      public virtual long? MpguStatusId { get; set; }
      /// <summary>
      /// Связь в рамках отправки документов в РЦХЭД
      /// </summary>
      [InverseProperty("CshedDocAndSign")]
      [Display(Description = "Связь в рамках отправки документов в РЦХЭД")]
      [DataMember(Name = "mpguStatus", EmitDefaultValue = false)]
      public virtual RequestStatusForMpgu MpguStatus { get; set; }

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