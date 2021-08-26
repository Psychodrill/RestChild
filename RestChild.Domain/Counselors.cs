// File:    Counselors.cs
// Purpose: Definition of Class Counselors

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Вожатый
   /// </summary>
   [Serializable]
   [DataContract(Name = "counselors")]
   public partial class Counselors : IEntityBase
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
      [DataMember(Name = "lastName", EmitDefaultValue = false)]
      public virtual string LastName { get; set; }
      
      /// <summary>
      /// Имя
      /// </summary>
      [Display(Description = "Имя")]
      [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "firstName", EmitDefaultValue = false)]
      public virtual string FirstName { get; set; }
      
      /// <summary>
      /// Отчество
      /// </summary>
      [Display(Description = "Отчество")]
      [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
      [DataMember(Name = "middleName", EmitDefaultValue = false)]
      public virtual string MiddleName { get; set; }
      
      /// <summary>
      /// Признак что есть отчество
      /// </summary>
      [Display(Description = "Признак что есть отчество")]
      [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
      [DataMember(Name = "haveMiddleName", EmitDefaultValue = false)]
      public virtual bool HaveMiddleName { get; set; }
      
      /// <summary>
      /// Серия документа
      /// </summary>
      [Display(Description = "Серия документа")]
      [MaxLength(1000, ErrorMessage = "\"Серия документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSeria", EmitDefaultValue = false)]
      public virtual string DocumentSeria { get; set; }
      
      /// <summary>
      /// Номер документа
      /// </summary>
      [Display(Description = "Номер документа")]
      [MaxLength(1000, ErrorMessage = "\"Номер документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentNumber", EmitDefaultValue = false)]
      public virtual string DocumentNumber { get; set; }
      
      /// <summary>
      /// Дата выдачи документа
      /// </summary>
      [Display(Description = "Дата выдачи документа")]
      [DataMember(Name = "documentDateOfIssue", EmitDefaultValue = false)]
      public virtual DateTime? DocumentDateOfIssue { get; set; }
      
      /// <summary>
      /// Кем выдан документ
      /// </summary>
      [Display(Description = "Кем выдан документ")]
      [MaxLength(1000, ErrorMessage = "\"Кем выдан документ\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSubjectIssue", EmitDefaultValue = false)]
      public virtual string DocumentSubjectIssue { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      /// <summary>
      /// Мужской пол
      /// </summary>
      [Display(Description = "Мужской пол")]
      [Required(ErrorMessage = "\"Мужской пол\" должно быть заполнено")]
      [DataMember(Name = "male", EmitDefaultValue = false)]
      public virtual bool Male { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Дата обновления
      /// </summary>
      [Display(Description = "Дата обновления")]
      [Required(ErrorMessage = "\"Дата обновления\" должно быть заполнено")]
      [DataMember(Name = "dateUpdate", EmitDefaultValue = false)]
      public virtual DateTime DateUpdate { get; set; }
      
      /// <summary>
      /// Место рождения
      /// </summary>
      [Display(Description = "Место рождения")]
      [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
      [DataMember(Name = "placeOfBirth", EmitDefaultValue = false)]
      public virtual string PlaceOfBirth { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Рейтинг
      /// </summary>
      [Display(Description = "Рейтинг")]
      [DataMember(Name = "rating", EmitDefaultValue = false)]
      public virtual decimal? Rating { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "registrationAddress", EmitDefaultValue = false)]
      public virtual string RegistrationAddress { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "inn", EmitDefaultValue = false)]
      public virtual string Inn { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "foreignPassport", EmitDefaultValue = false)]
      public virtual bool? ForeignPassport { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "militaryReserveCategory", EmitDefaultValue = false)]
      public virtual string MilitaryReserveCategory { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "militaryRank", EmitDefaultValue = false)]
      public virtual string MilitaryRank { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "militartStaff", EmitDefaultValue = false)]
      public virtual string MilitartStaff { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "vusCodeName", EmitDefaultValue = false)]
      public virtual string VusCodeName { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "mIlitaryCategory", EmitDefaultValue = false)]
      public virtual string MIlitaryCategory { get; set; }
      
      /// <summary>
      /// Золотой парус
      /// </summary>
      [Display(Description = "Золотой парус")]
      [Required(ErrorMessage = "\"Золотой парус\" должно быть заполнено")]
      [DataMember(Name = "goldenSail", EmitDefaultValue = false)]
      public virtual bool GoldenSail { get; set; }
      
      /// <summary>
      /// Количество неучтенных отработанных смен
      /// </summary>
      [Display(Description = "Количество неучтенных отработанных смен")]
      [Required(ErrorMessage = "\"Количество неучтенных отработанных смен\" должно быть заполнено")]
      [DataMember(Name = "unaccountedForWaste", EmitDefaultValue = false)]
      public virtual int UnaccountedForWaste { get; set; }
      
      /// <summary>
      /// Ссылка на фасебук
      /// </summary>
      [Display(Description = "Ссылка на фасебук")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на фасебук\" не может быть больше 1000 символов")]
      [DataMember(Name = "linkFacebook", EmitDefaultValue = false)]
      public virtual string LinkFacebook { get; set; }
      
      /// <summary>
      /// Ссылка на вконтакте
      /// </summary>
      [Display(Description = "Ссылка на вконтакте")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на вконтакте\" не может быть больше 1000 символов")]
      [DataMember(Name = "linkVk", EmitDefaultValue = false)]
      public virtual string LinkVk { get; set; }
      
      /// <summary>
      /// Ссылка на однокласники
      /// </summary>
      [Display(Description = "Ссылка на однокласники")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на однокласники\" не может быть больше 1000 символов")]
      [DataMember(Name = "linkOk", EmitDefaultValue = false)]
      public virtual string LinkOk { get; set; }
      
      /// <summary>
      /// Ссылка на инстаграмм
      /// </summary>
      [Display(Description = "Ссылка на инстаграмм")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на инстаграмм\" не может быть больше 1000 символов")]
      [DataMember(Name = "linkLinkedIn", EmitDefaultValue = false)]
      public virtual string LinkLinkedIn { get; set; }
      
      /// <summary>
      /// Уникальный идентификатор сайта
      /// </summary>
      [Display(Description = "Уникальный идентификатор сайта")]
      [MaxLength(1000, ErrorMessage = "\"Уникальный идентификатор сайта\" не может быть больше 1000 символов")]
      [DataMember(Name = "externalUid", EmitDefaultValue = false)]
      public virtual string ExternalUid { get; set; }
      
      /// <summary>
      /// Фактический адрес
      /// </summary>
      [Display(Description = "Фактический адрес")]
      [MaxLength(1000, ErrorMessage = "\"Фактический адрес\" не может быть больше 1000 символов")]
      [DataMember(Name = "factAddress", EmitDefaultValue = false)]
      public virtual string FactAddress { get; set; }
      
      /// <summary>
      /// Пароль
      /// </summary>
      [Display(Description = "Пароль")]
      [MaxLength(1000, ErrorMessage = "\"Пароль\" не может быть больше 1000 символов")]
      [DataMember(Name = "password", EmitDefaultValue = false)]
      public virtual string Password { get; set; }
      
      /// <summary>
      /// Соль
      /// </summary>
      [Display(Description = "Соль")]
      [MaxLength(1000, ErrorMessage = "\"Соль\" не может быть больше 1000 символов")]
      [DataMember(Name = "salt", EmitDefaultValue = false)]
      public virtual string Salt { get; set; }
      
      /// <summary>
      /// Причина вноса в стоп лист текстом
      /// </summary>
      [Display(Description = "Причина вноса в стоп лист текстом")]
      [DataMember(Name = "stopListReasonText", EmitDefaultValue = false)]
      public virtual String StopListReasonText { get; set; }
      
      /// <summary>
      /// Представитель профильной организации
      /// </summary>
      [Display(Description = "Представитель профильной организации")]
      [Required(ErrorMessage = "\"Представитель профильной организации\" должно быть заполнено")]
      [DataMember(Name = "representativesOrganizations", EmitDefaultValue = false)]
      public virtual bool RepresentativesOrganizations { get; set; }
      
      
      /// <summary>
      /// Вожатые отряд
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "partys", EmitDefaultValue = false)]
      public virtual ICollection<Party> Partys { get; set; }
      
      /// <summary>
      /// Старший вожатый
      /// </summary>
      [InverseProperty("SeniorCounselors")]
      [DataMember(Name = "bouts", EmitDefaultValue = false)]
      public virtual ICollection<Bout> Bouts { get; set; }
      
      /// <summary>
      /// Подменные вожатые
      /// </summary>
      [InverseProperty("SwingCounselors")]
      [DataMember(Name = "swingBoats", EmitDefaultValue = false)]
      public virtual ICollection<Bout> SwingBoats { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<CounselorFile> Files { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "highSchoolGraduations", EmitDefaultValue = false)]
      public virtual ICollection<CounselorHighSchool> HighSchoolGraduations { get; set; }
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "skill", EmitDefaultValue = false)]
      public virtual ICollection<CounselorSkill> Skill { get; set; }
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "comments", EmitDefaultValue = false)]
      public virtual ICollection<CouncelorComment> Comments { get; set; }
      
      /// <summary>
      /// Загран паспорта
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "foreginPassports", EmitDefaultValue = false)]
      public virtual ICollection<ForeginPassport> ForeginPassports { get; set; }
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "tasks", EmitDefaultValue = false)]
      public virtual ICollection<ResponsibilityForTask> Tasks { get; set; }
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "results", EmitDefaultValue = false)]
      public virtual ICollection<TrainingCounselorsResult> Results { get; set; }
      
      /// <summary>
      /// История изменения записей
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История изменения записей")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История изменения записей
      /// </summary>
      [Display(Description = "История изменения записей")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Пользователь создавший запись
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь создавший запись")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь создавший запись
      /// </summary>
      [Display(Description = "Пользователь создавший запись")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Вид документа удостоверяющего личность
      /// </summary>
      [ForeignKey("DocumentType")]
      [DataMember(Name = "documentTypeId")]
      [Display(Description = "Вид документа удостоверяющего личность")]
      public virtual long? DocumentTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документа удостоверяющего личность
      /// </summary>
      [Display(Description = "Вид документа удостоверяющего личность")]
      [DataMember(Name = "documentType", EmitDefaultValue = false)]
      public virtual DocumentType DocumentType { get; set; }
      
      /// <summary>
      /// Опыт работы вожатого
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "counselorPractices", EmitDefaultValue = false)]
      public virtual ICollection<CounselorPractice> CounselorPractices { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Counselors")]
      [DataMember(Name = "counselorCources", EmitDefaultValue = false)]
      public virtual ICollection<CounselorCource> CounselorCources { get; set; }
      
      /// <summary>
      /// Цвет галстука
      /// </summary>
      [ForeignKey("TieColor")]
      [DataMember(Name = "tieColorId")]
      [Display(Description = "Цвет галстука")]
      public virtual long? TieColorId { get; set; }
      
      
      /// <summary>
      /// Цвет галстука
      /// </summary>
      [Display(Description = "Цвет галстука")]
      [DataMember(Name = "tieColor", EmitDefaultValue = false)]
      public virtual TieColor TieColor { get; set; }
      
      /// <summary>
      /// Регион
      /// </summary>
      [ForeignKey("StateDistrict")]
      [DataMember(Name = "stateDistrictId")]
      [Display(Description = "Регион")]
      public virtual long? StateDistrictId { get; set; }
      
      
      /// <summary>
      /// Регион
      /// </summary>
      [Display(Description = "Регион")]
      [DataMember(Name = "stateDistrict", EmitDefaultValue = false)]
      public virtual StateDistrict StateDistrict { get; set; }
      
      /// <summary>
      /// Семейное положение
      /// </summary>
      [ForeignKey("MatrialStatus")]
      [DataMember(Name = "matrialStatusId")]
      [Display(Description = "Семейное положение")]
      public virtual long? MatrialStatusId { get; set; }
      
      
      /// <summary>
      /// Семейное положение
      /// </summary>
      [Display(Description = "Семейное положение")]
      [DataMember(Name = "matrialStatus", EmitDefaultValue = false)]
      public virtual MatrialStatus MatrialStatus { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("MilitaryDuty")]
      [DataMember(Name = "militaryDutyId")]
      [Display(Description = "")]
      public virtual long? MilitaryDutyId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "militaryDuty", EmitDefaultValue = false)]
      public virtual MilitaryDuty MilitaryDuty { get; set; }
      
      /// <summary>
      /// Вид образования
      /// </summary>
      [ForeignKey("TypeOfEducation")]
      [DataMember(Name = "typeOfEducationId")]
      [Display(Description = "Вид образования")]
      public virtual long? TypeOfEducationId { get; set; }
      
      
      /// <summary>
      /// Вид образования
      /// </summary>
      [Display(Description = "Вид образования")]
      [DataMember(Name = "typeOfEducation", EmitDefaultValue = false)]
      public virtual TypeOfEducation TypeOfEducation { get; set; }
      
      /// <summary>
      /// Связанный пользователь
      /// </summary>
      [ForeignKey("LinkedAccount")]
      [DataMember(Name = "linkedAccountId")]
      [Display(Description = "Связанный пользователь")]
      public virtual long? LinkedAccountId { get; set; }
      
      
      /// <summary>
      /// Связанный пользователь
      /// </summary>
      [Display(Description = "Связанный пользователь")]
      [DataMember(Name = "linkedAccount", EmitDefaultValue = false)]
      public virtual Account LinkedAccount { get; set; }
      
      /// <summary>
      /// Размер одежды (футболки)
      /// </summary>
      [ForeignKey("ClothingSize")]
      [DataMember(Name = "clothingSizeId")]
      [Display(Description = "Размер одежды (футболки)")]
      public virtual long? ClothingSizeId { get; set; }
      
      
      /// <summary>
      /// Размер одежды (футболки)
      /// </summary>
      [Display(Description = "Размер одежды (футболки)")]
      [DataMember(Name = "clothingSize", EmitDefaultValue = false)]
      public virtual ClothingSize ClothingSize { get; set; }
      
      /// <summary>
      /// Пед отряд
      /// </summary>
      [ForeignKey("PedParty")]
      [DataMember(Name = "pedPartyId")]
      [Display(Description = "Пед отряд")]
      public virtual long? PedPartyId { get; set; }
      
      
      /// <summary>
      /// Пед отряд
      /// </summary>
      [Display(Description = "Пед отряд")]
      [DataMember(Name = "pedParty", EmitDefaultValue = false)]
      public virtual PedParty PedParty { get; set; }
      
      /// <summary>
      /// Причина вноса в стоп лист
      /// </summary>
      [ForeignKey("StopListReason")]
      [DataMember(Name = "stopListReasonId")]
      [Display(Description = "Причина вноса в стоп лист")]
      public virtual long? StopListReasonId { get; set; }
      
      
      /// <summary>
      /// Причина вноса в стоп лист
      /// </summary>
      [Display(Description = "Причина вноса в стоп лист")]
      [DataMember(Name = "stopListReason", EmitDefaultValue = false)]
      public virtual CounselorsStopListReason StopListReason { get; set; }

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