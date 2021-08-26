// File:    LinkToPeople.cs
// Purpose: Definition of Class LinkToPeople

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Связь с людьми
   /// </summary>
   [Serializable]
   [DataContract(Name = "linkToPeople")]
   public partial class LinkToPeople : IEntityBase
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
      /// Номер вагона
      /// </summary>
      [Display(Description = "Номер вагона")]
      [MaxLength(1000, ErrorMessage = "\"Номер вагона\" не может быть больше 1000 символов")]
      [DataMember(Name = "wagon", EmitDefaultValue = false)]
      public virtual string Wagon { get; set; }
      
      /// <summary>
      /// Номер места
      /// </summary>
      [Display(Description = "Номер места")]
      [MaxLength(1000, ErrorMessage = "\"Номер места\" не может быть больше 1000 символов")]
      [DataMember(Name = "placeNumber", EmitDefaultValue = false)]
      public virtual string PlaceNumber { get; set; }
      
      /// <summary>
      /// Нужен билет
      /// </summary>
      [Display(Description = "Нужен билет")]
      [Required(ErrorMessage = "\"Нужен билет\" должно быть заполнено")]
      [DataMember(Name = "needTicket", EmitDefaultValue = false)]
      public virtual bool NeedTicket { get; set; }
      
      /// <summary>
      /// Не явился в место отдыха
      /// </summary>
      [Display(Description = "Не явился в место отдыха")]
      [Required(ErrorMessage = "\"Не явился в место отдыха\" должно быть заполнено")]
      [DataMember(Name = "notComeInPlaceOfRest", EmitDefaultValue = false)]
      public virtual bool NotComeInPlaceOfRest { get; set; }
      
      /// <summary>
      /// Дата отъезда
      /// </summary>
      [Display(Description = "Дата отъезда")]
      [DataMember(Name = "dateDeparture", EmitDefaultValue = false)]
      public virtual DateTime? DateDeparture { get; set; }
      
      /// <summary>
      /// Сдан родителям
      /// </summary>
      [Display(Description = "Сдан родителям")]
      [Required(ErrorMessage = "\"Сдан родителям\" должно быть заполнено")]
      [DataMember(Name = "deliveredParents", EmitDefaultValue = false)]
      public virtual bool DeliveredParents { get; set; }
      
      
      /// <summary>
      /// Траснпорт
      /// </summary>
      [ForeignKey("Transport")]
      [DataMember(Name = "transportId")]
      [Display(Description = "Траснпорт")]
      public virtual long? TransportId { get; set; }
      
      
      /// <summary>
      /// Траснпорт
      /// </summary>
      [InverseProperty("People")]
      [Display(Description = "Траснпорт")]
      [DataMember(Name = "transport", EmitDefaultValue = false)]
      public virtual TransportInfo Transport { get; set; }
      
      /// <summary>
      /// Рейс
      /// </summary>
      [ForeignKey("DirectoryFlights")]
      [DataMember(Name = "directoryFlightsId")]
      [Display(Description = "Рейс")]
      public virtual long? DirectoryFlightsId { get; set; }
      
      
      /// <summary>
      /// Рейс
      /// </summary>
      [InverseProperty("LinkToPeoples")]
      [Display(Description = "Рейс")]
      [DataMember(Name = "directoryFlights", EmitDefaultValue = false)]
      public virtual DirectoryFlights DirectoryFlights { get; set; }
      
      /// <summary>
      /// Отряд
      /// </summary>
      [ForeignKey("Party")]
      [DataMember(Name = "partyId")]
      [Display(Description = "Отряд")]
      public virtual long? PartyId { get; set; }
      
      
      /// <summary>
      /// Отряд
      /// </summary>
      [Display(Description = "Отряд")]
      [DataMember(Name = "party", EmitDefaultValue = false)]
      public virtual Party Party { get; set; }
      
      /// <summary>
      /// Список детей
      /// </summary>
      [ForeignKey("ListOfChilds")]
      [DataMember(Name = "listOfChildsId")]
      [Display(Description = "Список детей")]
      public virtual long? ListOfChildsId { get; set; }
      
      
      /// <summary>
      /// Список детей
      /// </summary>
      [Display(Description = "Список детей")]
      [DataMember(Name = "listOfChilds", EmitDefaultValue = false)]
      public virtual ListOfChilds ListOfChilds { get; set; }
      
      /// <summary>
      /// Администратор смены
      /// </summary>
      [ForeignKey("AdministratorTour")]
      [DataMember(Name = "administratorTourId")]
      [Display(Description = "Администратор смены")]
      public virtual long? AdministratorTourId { get; set; }
      
      
      /// <summary>
      /// Администратор смены
      /// </summary>
      [Display(Description = "Администратор смены")]
      [DataMember(Name = "administratorTour", EmitDefaultValue = false)]
      public virtual AdministratorTour AdministratorTour { get; set; }
      
      /// <summary>
      /// Вожатые
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "Вожатые")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// Вожатые
      /// </summary>
      [Display(Description = "Вожатые")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Ребёнок")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [InverseProperty("LinkToPeoples")]
      [Display(Description = "Ребёнок")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Заявление
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Заявление")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Заявление
      /// </summary>
      [Display(Description = "Заявление")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Вид связи
      /// </summary>
      [ForeignKey("TypeOfLinkPeople")]
      [DataMember(Name = "typeOfLinkPeopleId")]
      [Display(Description = "Вид связи")]
      public virtual long? TypeOfLinkPeopleId { get; set; }
      
      
      /// <summary>
      /// Вид связи
      /// </summary>
      [Display(Description = "Вид связи")]
      [DataMember(Name = "typeOfLinkPeople", EmitDefaultValue = false)]
      public virtual TypeOfLinkPeople TypeOfLinkPeople { get; set; }
      
      /// <summary>
      /// Заявитель/сопровождающий
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Заявитель/сопровождающий")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Заявитель/сопровождающий
      /// </summary>
      [InverseProperty("LinkToPeoples")]
      [Display(Description = "Заявитель/сопровождающий")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Заезд
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Заезд")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Заезд
      /// </summary>
      [Display(Description = "Заезд")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }
      
      /// <summary>
      /// Причина по которой не нужен билет
      /// </summary>
      [ForeignKey("NotNeedTicketReason")]
      [DataMember(Name = "notNeedTicketReasonId")]
      [Display(Description = "Причина по которой не нужен билет")]
      public virtual long? NotNeedTicketReasonId { get; set; }
      
      
      /// <summary>
      /// Причина по которой не нужен билет
      /// </summary>
      [Display(Description = "Причина по которой не нужен билет")]
      [DataMember(Name = "notNeedTicketReason", EmitDefaultValue = false)]
      public virtual NotNeedTicketReason NotNeedTicketReason { get; set; }

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