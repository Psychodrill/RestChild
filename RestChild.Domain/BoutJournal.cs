// File:    BoutJournal.cs
// Purpose: Definition of Class BoutJournal

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Дневник смены
   /// </summary>
   [Serializable]
   [DataContract(Name = "boutJournal")]
   public partial class BoutJournal : IEntityBase
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
      /// Заголовок
      /// </summary>
      [Display(Description = "Заголовок")]
      [MaxLength(1000, ErrorMessage = "\"Заголовок\" не может быть больше 1000 символов")]
      [DataMember(Name = "title", EmitDefaultValue = false)]
      public virtual string Title { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Дата события
      /// </summary>
      [Display(Description = "Дата события")]
      [DataMember(Name = "eventDate", EmitDefaultValue = false)]
      public virtual DateTime? EventDate { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime? DateCreate { get; set; }
      
      /// <summary>
      /// Дата изменения
      /// </summary>
      [Display(Description = "Дата изменения")]
      [DataMember(Name = "dateChange", EmitDefaultValue = false)]
      public virtual DateTime? DateChange { get; set; }
      
      /// <summary>
      /// Признак архивной записи
      /// </summary>
      [Display(Description = "Признак архивной записи")]
      [Required(ErrorMessage = "\"Признак архивной записи\" должно быть заполнено")]
      [DataMember(Name = "isArchive", EmitDefaultValue = false)]
      public virtual bool IsArchive { get; set; }
      
      /// <summary>
      /// Для сайта
      /// </summary>
      [Display(Description = "Для сайта")]
      [Required(ErrorMessage = "\"Для сайта\" должно быть заполнено")]
      [DataMember(Name = "forSite", EmitDefaultValue = false)]
      public virtual bool ForSite { get; set; }
      
      
      /// <summary>
      /// Файлы из дневника
      /// </summary>
      [InverseProperty("BoutJournal")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<BoutJournalFile> Files { get; set; }
      
      /// <summary>
      /// Дневник заезда
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Дневник заезда")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Дневник заезда
      /// </summary>
      [InverseProperty("BoutJournal")]
      [Display(Description = "Дневник заезда")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [ForeignKey("Counselors")]
      [DataMember(Name = "counselorsId")]
      [Display(Description = "Вожатый")]
      public virtual long? CounselorsId { get; set; }
      
      
      /// <summary>
      /// Вожатый
      /// </summary>
      [Display(Description = "Вожатый")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual Counselors Counselors { get; set; }
      
      /// <summary>
      /// Тип вожатого
      /// </summary>
      [ForeignKey("CounselorTaskExecutorType")]
      [DataMember(Name = "counselorTaskExecutorTypeId")]
      [Display(Description = "Тип вожатого")]
      public virtual long? CounselorTaskExecutorTypeId { get; set; }
      
      
      /// <summary>
      /// Тип вожатого
      /// </summary>
      [Display(Description = "Тип вожатого")]
      [DataMember(Name = "counselorTaskExecutorType", EmitDefaultValue = false)]
      public virtual CounselorTaskExecutorType CounselorTaskExecutorType { get; set; }
      
      /// <summary>
      /// Ссылка на администратора смены
      /// </summary>
      [ForeignKey("AdministratorTour")]
      [DataMember(Name = "administratorTourId")]
      [Display(Description = "Ссылка на администратора смены")]
      public virtual long? AdministratorTourId { get; set; }
      
      
      /// <summary>
      /// Ссылка на администратора смены
      /// </summary>
      [Display(Description = "Ссылка на администратора смены")]
      [DataMember(Name = "administratorTour", EmitDefaultValue = false)]
      public virtual AdministratorTour AdministratorTour { get; set; }
      
      /// <summary>
      /// Ссылка на отряд
      /// </summary>
      [ForeignKey("Party")]
      [DataMember(Name = "partyId")]
      [Display(Description = "Ссылка на отряд")]
      public virtual long? PartyId { get; set; }
      
      
      /// <summary>
      /// Ссылка на отряд
      /// </summary>
      [Display(Description = "Ссылка на отряд")]
      [DataMember(Name = "party", EmitDefaultValue = false)]
      public virtual Party Party { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Тип записи в журнале
      /// </summary>
      [ForeignKey("BoutJournalType")]
      [DataMember(Name = "boutJournalTypeId")]
      [Display(Description = "Тип записи в журнале")]
      public virtual long? BoutJournalTypeId { get; set; }
      
      
      /// <summary>
      /// Тип записи в журнале
      /// </summary>
      [Display(Description = "Тип записи в журнале")]
      [DataMember(Name = "boutJournalType", EmitDefaultValue = false)]
      public virtual BoutJournalType BoutJournalType { get; set; }
      
      /// <summary>
      /// Категория происшествия
      /// </summary>
      [ForeignKey("CategoryIncident")]
      [DataMember(Name = "categoryIncidentId")]
      [Display(Description = "Категория происшествия")]
      public virtual long? CategoryIncidentId { get; set; }
      
      
      /// <summary>
      /// Категория происшествия
      /// </summary>
      [Display(Description = "Категория происшествия")]
      [DataMember(Name = "categoryIncident", EmitDefaultValue = false)]
      public virtual CategoryIncident CategoryIncident { get; set; }

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