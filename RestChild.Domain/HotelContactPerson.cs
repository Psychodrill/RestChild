// File:    HotelContactPerson.cs
// Purpose: Definition of Class HotelContactPerson

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Контактные лица
   /// </summary>
   [Serializable]
   [DataContract(Name = "hotelContactPerson")]
   public partial class HotelContactPerson : IEntityBase
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
      /// Фамилия
      /// </summary>
      [Display(Description = "Фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Фамилия\" не может быть пустым")]
      [DataMember(Name = "lastName", EmitDefaultValue = false)]
      public virtual string LastName { get; set; }
      
      /// <summary>
      /// Имя
      /// </summary>
      [Display(Description = "Имя")]
      [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Имя\" не может быть пустым")]
      [DataMember(Name = "firstName", EmitDefaultValue = false)]
      public virtual string FirstName { get; set; }
      
      /// <summary>
      /// Отчество
      /// </summary>
      [Display(Description = "Отчество")]
      [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Отчество\" не может быть пустым")]
      [DataMember(Name = "middleName", EmitDefaultValue = false)]
      public virtual string MiddleName { get; set; }
      
      /// <summary>
      /// Должность
      /// </summary>
      [Display(Description = "Должность")]
      [MaxLength(1000, ErrorMessage = "\"Должность\" не может быть больше 1000 символов")]
      [DataMember(Name = "position", EmitDefaultValue = false)]
      public virtual string Position { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Телефон\" не может быть пустым")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      
      /// <summary>
      /// Контактные лица
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")]
      [Display(Description = "Контактные лица")]
      public virtual long? HotelId { get; set; }
      
      
      /// <summary>
      /// Контактные лица
      /// </summary>
      [InverseProperty("Contacts")]
      [Display(Description = "Контактные лица")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual Hotels Hotel { get; set; }

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