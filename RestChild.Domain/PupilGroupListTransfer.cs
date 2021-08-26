// File:    PupilGroupListTransfer.cs
// Purpose: Definition of Class PupilGroupListTransfer

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Трансфер (список по группе отправки (потребности))
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupilGroupListTransfer")]
   public partial class PupilGroupListTransfer : IEntityBase
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
      /// При отсутствии возможности разворота ТС, где можно осуществить парковку
      /// </summary>
      [Display(Description = "При отсутствии возможности разворота ТС, где можно осуществить парковку")]
      [MaxLength(1000, ErrorMessage = "\"При отсутствии возможности разворота ТС, где можно осуществить парковку\" не может быть больше 1000 символов")]
      [DataMember(Name = "largeParkingReAddress", EmitDefaultValue = false)]
      public virtual string LargeParkingReAddress { get; set; }
      
      /// <summary>
      /// Примечание
      /// </summary>
      [Display(Description = "Примечание")]
      [MaxLength(1000, ErrorMessage = "\"Примечание\" не может быть больше 1000 символов")]
      [DataMember(Name = "note", EmitDefaultValue = false)]
      public virtual string Note { get; set; }
      
      /// <summary>
      /// Кол-во человек
      /// </summary>
      [Display(Description = "Кол-во человек")]
      [DataMember(Name = "countPeople", EmitDefaultValue = false)]
      public virtual int? CountPeople { get; set; }
      
      /// <summary>
      /// Помощь при погрузке
      /// </summary>
      [Display(Description = "Помощь при погрузке")]
      [Required(ErrorMessage = "\"Помощь при погрузке\" должно быть заполнено")]
      [DataMember(Name = "boardingHelp", EmitDefaultValue = false)]
      public virtual bool BoardingHelp { get; set; }
      
      
      /// <summary>
      /// Заявка на период отдыха -> Трансферы
      /// </summary>
      [ForeignKey("GroupRequestList")]
      [DataMember(Name = "groupRequestListId")]
      [Display(Description = "Заявка на период отдыха -> Трансферы")]
      public virtual long? GroupRequestListId { get; set; }
      
      
      /// <summary>
      /// Заявка на период отдыха -> Трансферы
      /// </summary>
      [InverseProperty("GroupTransfers")]
      [Display(Description = "Заявка на период отдыха -> Трансферы")]
      [DataMember(Name = "groupRequestList", EmitDefaultValue = false)]
      public virtual ListOfChilds GroupRequestList { get; set; }
      
      /// <summary>
      /// Трансфер (Заявка на период отдыха) -> Адрес приюта
      /// </summary>
      [ForeignKey("Address")]
      [DataMember(Name = "addressId")]
      [Display(Description = "Трансфер (Заявка на период отдыха) -> Адрес приюта")]
      public virtual long? AddressId { get; set; }
      
      
      /// <summary>
      /// Трансфер (Заявка на период отдыха) -> Адрес приюта
      /// </summary>
      [Display(Description = "Трансфер (Заявка на период отдыха) -> Адрес приюта")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual OrphanageAddress Address { get; set; }

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