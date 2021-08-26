// File:    ListOfChilds.cs
// Purpose: Definition of Class ListOfChilds

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Список детей
   /// </summary>
   [Serializable]
   [DataContract(Name = "listOfChilds")]
   public partial class ListOfChilds : IEntityBase
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
      /// Признак последней версии
      /// </summary>
      [Display(Description = "Признак последней версии")]
      [Required(ErrorMessage = "\"Признак последней версии\" должно быть заполнено")]
      [DataMember(Name = "isLast", EmitDefaultValue = false)]
      public virtual bool IsLast { get; set; }
      
      /// <summary>
      /// Признак что удалено
      /// </summary>
      [Display(Description = "Признак что удалено")]
      [Required(ErrorMessage = "\"Признак что удалено\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Дата изменения
      /// </summary>
      [Display(Description = "Дата изменения")]
      [Required(ErrorMessage = "\"Дата изменения\" должно быть заполнено")]
      [DataMember(Name = "dateChange", EmitDefaultValue = false)]
      public virtual DateTime DateChange { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Количество детей
      /// </summary>
      [Display(Description = "Количество детей")]
      [Required(ErrorMessage = "\"Количество детей\" должно быть заполнено")]
      [DataMember(Name = "countChild", EmitDefaultValue = false)]
      public virtual int CountChild { get; set; }
      
      /// <summary>
      /// Количество сопровождающих
      /// </summary>
      [Display(Description = "Количество сопровождающих")]
      [Required(ErrorMessage = "\"Количество сопровождающих\" должно быть заполнено")]
      [DataMember(Name = "countAttendants", EmitDefaultValue = false)]
      public virtual int CountAttendants { get; set; }
      
      /// <summary>
      /// Номер сертификата
      /// </summary>
      [Display(Description = "Номер сертификата")]
      [MaxLength(1000, ErrorMessage = "\"Номер сертификата\" не может быть больше 1000 символов")]
      [DataMember(Name = "certificateNumber", EmitDefaultValue = false)]
      public virtual string CertificateNumber { get; set; }
      
      /// <summary>
      /// ФИО ответственного за выезд
      /// </summary>
      [Display(Description = "ФИО ответственного за выезд")]
      [MaxLength(1000, ErrorMessage = "\"ФИО ответственного за выезд\" не может быть больше 1000 символов")]
      [DataMember(Name = "responsible", EmitDefaultValue = false)]
      public virtual string Responsible { get; set; }
      
      /// <summary>
      /// Телефон ответственного за выезд
      /// </summary>
      [Display(Description = "Телефон ответственного за выезд")]
      [MaxLength(1000, ErrorMessage = "\"Телефон ответственного за выезд\" не может быть больше 1000 символов")]
      [DataMember(Name = "responsiblePhone", EmitDefaultValue = false)]
      public virtual string ResponsiblePhone { get; set; }
      
      /// <summary>
      /// Для индексации
      /// </summary>
      [Display(Description = "Для индексации")]
      [Required(ErrorMessage = "\"Для индексации\" должно быть заполнено")]
      [DataMember(Name = "forIndex", EmitDefaultValue = false)]
      public virtual bool ForIndex { get; set; }
      
      /// <summary>
      /// Ознакомлен и согласен с Правилами
      /// </summary>
      [Display(Description = "Ознакомлен и согласен с Правилами")]
      [Required(ErrorMessage = "\"Ознакомлен и согласен с Правилами\" должно быть заполнено")]
      [DataMember(Name = "rulesAgreement", EmitDefaultValue = false)]
      public virtual bool RulesAgreement { get; set; }
      
      /// <summary>
      /// Воспитанники с Правилами ознакомлены
      /// </summary>
      [Display(Description = "Воспитанники с Правилами ознакомлены")]
      [Required(ErrorMessage = "\"Воспитанники с Правилами ознакомлены\" должно быть заполнено")]
      [DataMember(Name = "pupilsRulesAgreement", EmitDefaultValue = false)]
      public virtual bool PupilsRulesAgreement { get; set; }
      
      
      /// <summary>
      /// Связь списка и детей
      /// </summary>
      [InverseProperty("ChildList")]
      [DataMember(Name = "childs", EmitDefaultValue = false)]
      public virtual ICollection<Child> Childs { get; set; }
      
      /// <summary>
      /// Связь список детей и сопровождающий
      /// </summary>
      [InverseProperty("ChildList")]
      [DataMember(Name = "attendants", EmitDefaultValue = false)]
      public virtual ICollection<Applicant> Attendants { get; set; }
      
      /// <summary>
      /// Список детей на который коммерческая заявка
      /// </summary>
      [InverseProperty("ParentListOfChild")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<Request> Requests { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Список
      /// </summary>
      [InverseProperty("Lists")]
      [DataMember(Name = "pupilGroupRequest", EmitDefaultValue = false)]
      public virtual ICollection<RequestForPeriodOfRest> PupilGroupRequest { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Воспитанники
      /// </summary>
      [InverseProperty("GroupRequestList")]
      [DataMember(Name = "groupPupils", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListMember> GroupPupils { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Сопровождающие
      /// </summary>
      [InverseProperty("GroupRequestList")]
      [DataMember(Name = "groupCollaborators", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListCollaborator> GroupCollaborators { get; set; }
      
      /// <summary>
      /// Заявка на период отдыха -> Трансферы
      /// </summary>
      [InverseProperty("GroupRequestList")]
      [DataMember(Name = "groupTransfers", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroupListTransfer> GroupTransfers { get; set; }
      
      /// <summary>
      /// Ссылка на историю изменений
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "Ссылка на историю изменений")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// Ссылка на историю изменений
      /// </summary>
      [Display(Description = "Ссылка на историю изменений")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Связь со списками детей
      /// </summary>
      [ForeignKey("LimitOnOrganization")]
      [DataMember(Name = "limitOnOrganizationId")]
      [Display(Description = "Связь со списками детей")]
      public virtual long? LimitOnOrganizationId { get; set; }
      
      
      /// <summary>
      /// Связь со списками детей
      /// </summary>
      [Display(Description = "Связь со списками детей")]
      [DataMember(Name = "limitOnOrganization", EmitDefaultValue = false)]
      public virtual LimitOnOrganization LimitOnOrganization { get; set; }
      
      /// <summary>
      /// Связь время отдыха и список детей
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")]
      [Display(Description = "Связь время отдыха и список детей")]
      public virtual long? TimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь время отдыха и список детей
      /// </summary>
      [Display(Description = "Связь время отдыха и список детей")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
      /// <summary>
      /// Место отдыха и список детей
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")]
      [Display(Description = "Место отдыха и список детей")]
      public virtual long? PlaceOfRestId { get; set; }
      
      
      /// <summary>
      /// Место отдыха и список детей
      /// </summary>
      [Display(Description = "Место отдыха и список детей")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }
      
      /// <summary>
      /// Статус списка детей
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус списка детей")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус списка детей
      /// </summary>
      [Display(Description = "Статус списка детей")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Связь заездов и спика детей
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Связь заездов и спика детей")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Связь заездов и спика детей
      /// </summary>
      [InverseProperty("ListOfChilds")]
      [Display(Description = "Связь заездов и спика детей")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Связь категории и списка
      /// </summary>
      [ForeignKey("ListOfChildsCategory")]
      [DataMember(Name = "listOfChildsCategoryId")]
      [Display(Description = "Связь категории и списка")]
      public virtual long? ListOfChildsCategoryId { get; set; }
      
      
      /// <summary>
      /// Связь категории и списка
      /// </summary>
      [Display(Description = "Связь категории и списка")]
      [DataMember(Name = "listOfChildsCategory", EmitDefaultValue = false)]
      public virtual ListOfChildsCategory ListOfChildsCategory { get; set; }
      
      /// <summary>
      /// Тип списков квот
      /// </summary>
      [ForeignKey("TypeOfLimitList")]
      [DataMember(Name = "typeOfLimitListId")]
      [Display(Description = "Тип списков квот")]
      public virtual long? TypeOfLimitListId { get; set; }
      
      
      /// <summary>
      /// Тип списков квот
      /// </summary>
      [Display(Description = "Тип списков квот")]
      [DataMember(Name = "typeOfLimitList", EmitDefaultValue = false)]
      public virtual TypeOfLimitList TypeOfLimitList { get; set; }

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