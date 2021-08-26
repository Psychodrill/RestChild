// File:    Organization.cs
// Purpose: Definition of Class Organization

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Организация
   /// </summary>
   [Serializable]
   [DataContract(Name = "organization")]
   public partial class Organization : IEntityBase
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
      /// Наименование организации
      /// </summary>
      [Display(Description = "Наименование организации")]
      [MaxLength(1000, ErrorMessage = "\"Наименование организации\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование организации\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Краткое наименование
      /// </summary>
      [Display(Description = "Краткое наименование")]
      [MaxLength(1000, ErrorMessage = "\"Краткое наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "shortName", EmitDefaultValue = false)]
      public virtual string ShortName { get; set; }
      
      /// <summary>
      /// Внешний идентификатор
      /// </summary>
      [Display(Description = "Внешний идентификатор")]
      [MaxLength(1000, ErrorMessage = "\"Внешний идентификатор\" не может быть больше 1000 символов")]
      [DataMember(Name = "externalUid", EmitDefaultValue = false)]
      public virtual string ExternalUid { get; set; }
      
      /// <summary>
      /// ИНН
      /// </summary>
      [Display(Description = "ИНН")]
      [MaxLength(1000, ErrorMessage = "\"ИНН\" не может быть больше 1000 символов")]
      [DataMember(Name = "inn", EmitDefaultValue = false)]
      public virtual string Inn { get; set; }
      
      /// <summary>
      /// КПП
      /// </summary>
      [Display(Description = "КПП")]
      [MaxLength(1000, ErrorMessage = "\"КПП\" не может быть больше 1000 символов")]
      [DataMember(Name = "kpp", EmitDefaultValue = false)]
      public virtual string Kpp { get; set; }
      
      /// <summary>
      /// ОГРН
      /// </summary>
      [Display(Description = "ОГРН")]
      [MaxLength(1000, ErrorMessage = "\"ОГРН\" не может быть больше 1000 символов")]
      [DataMember(Name = "ogrn", EmitDefaultValue = false)]
      public virtual string Ogrn { get; set; }
      
      /// <summary>
      /// Удалена
      /// </summary>
      [Display(Description = "Удалена")]
      [Required(ErrorMessage = "\"Удалена\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Последняя версия
      /// </summary>
      [Display(Description = "Последняя версия")]
      [Required(ErrorMessage = "\"Последняя версия\" должно быть заполнено")]
      [DataMember(Name = "isLast", EmitDefaultValue = false)]
      public virtual bool IsLast { get; set; }
      
      /// <summary>
      /// Ведомство
      /// </summary>
      [Display(Description = "Ведомство")]
      [Required(ErrorMessage = "\"Ведомство\" должно быть заполнено")]
      [DataMember(Name = "isVedomstvo", EmitDefaultValue = false)]
      public virtual bool IsVedomstvo { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Первичный ключ ЕКИС
      /// </summary>
      [Display(Description = "Первичный ключ ЕКИС")]
      [DataMember(Name = "ekisSourcePk", EmitDefaultValue = false)]
      public virtual long? EkisSourcePk { get; set; }
      
      /// <summary>
      /// Первичный ключ источника ЕКИС
      /// </summary>
      [Display(Description = "Первичный ключ источника ЕКИС")]
      [DataMember(Name = "ekisExternalPk", EmitDefaultValue = false)]
      public virtual long? EkisExternalPk { get; set; }
      
      /// <summary>
      /// Статус ЕКИС
      /// </summary>
      [Display(Description = "Статус ЕКИС")]
      [DataMember(Name = "ekisStatus", EmitDefaultValue = false)]
      public virtual int? EkisStatus { get; set; }
      
      /// <summary>
      /// GUID организации
      /// </summary>
      [Display(Description = "GUID организации")]
      [DataMember(Name = "ekisGuid", EmitDefaultValue = false)]
      public virtual Guid? EkisGuid { get; set; }
      
      /// <summary>
      /// Учреждение
      /// </summary>
      [Display(Description = "Учреждение")]
      [DataMember(Name = "isVedOrganization", EmitDefaultValue = false)]
      public virtual bool? IsVedOrganization { get; set; }
      
      /// <summary>
      /// Контрагенты
      /// </summary>
      [Display(Description = "Контрагенты")]
      [DataMember(Name = "isContractor", EmitDefaultValue = false)]
      public virtual bool? IsContractor { get; set; }
      
      /// <summary>
      /// Транспортная организация
      /// </summary>
      [Display(Description = "Транспортная организация")]
      [DataMember(Name = "isTransport", EmitDefaultValue = false)]
      public virtual bool? IsTransport { get; set; }
      
      /// <summary>
      /// Целевая организация
      /// </summary>
      [Display(Description = "Целевая организация")]
      [DataMember(Name = "targetOrganizationPk", EmitDefaultValue = false)]
      public virtual long? TargetOrganizationPk { get; set; }
      
      /// <summary>
      /// Фактические адрес
      /// </summary>
      [Display(Description = "Фактические адрес")]
      [MaxLength(1000, ErrorMessage = "\"Фактические адрес\" не может быть больше 1000 символов")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual string Address { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "contactPerson", EmitDefaultValue = false)]
      public virtual string ContactPerson { get; set; }
      
      /// <summary>
      /// Примечание
      /// </summary>
      [Display(Description = "Примечание")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      /// <summary>
      /// Комиссия
      /// </summary>
      [Display(Description = "Комиссия")]
      [DataMember(Name = "commission", EmitDefaultValue = false)]
      public virtual decimal? Commission { get; set; }
      
      /// <summary>
      /// Название на латинице
      /// </summary>
      [Display(Description = "Название на латинице")]
      [MaxLength(1000, ErrorMessage = "\"Название на латинице\" не может быть больше 1000 символов")]
      [DataMember(Name = "latinName", EmitDefaultValue = false)]
      public virtual string LatinName { get; set; }
      
      /// <summary>
      /// Форма собственности
      /// </summary>
      [Display(Description = "Форма собственности")]
      [MaxLength(1000, ErrorMessage = "\"Форма собственности\" не может быть больше 1000 символов")]
      [DataMember(Name = "ownership", EmitDefaultValue = false)]
      public virtual string Ownership { get; set; }
      
      /// <summary>
      /// Почтовый адрес
      /// </summary>
      [Display(Description = "Почтовый адрес")]
      [MaxLength(1000, ErrorMessage = "\"Почтовый адрес\" не может быть больше 1000 символов")]
      [DataMember(Name = "postAdderss", EmitDefaultValue = false)]
      public virtual string PostAdderss { get; set; }
      
      /// <summary>
      /// Ген. директор
      /// </summary>
      [Display(Description = "Ген. директор")]
      [MaxLength(1000, ErrorMessage = "\"Ген. директор\" не может быть больше 1000 символов")]
      [DataMember(Name = "headPerson", EmitDefaultValue = false)]
      public virtual string HeadPerson { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isTradeUnion", EmitDefaultValue = false)]
      public virtual bool IsTradeUnion { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isHotel", EmitDefaultValue = false)]
      public virtual bool IsHotel { get; set; }
      
      /// <summary>
      /// Количество детей в смену
      /// </summary>
      [Display(Description = "Количество детей в смену")]
      [DataMember(Name = "countInTour", EmitDefaultValue = false)]
      public virtual int? CountInTour { get; set; }
      
      /// <summary>
      /// Детский дом
      /// </summary>
      [Display(Description = "Детский дом")]
      [DataMember(Name = "orphanage", EmitDefaultValue = false)]
      public virtual bool? Orphanage { get; set; }
      
      /// <summary>
      /// Участник мониторинга
      /// </summary>
      [Display(Description = "Участник мониторинга")]
      [Required(ErrorMessage = "\"Участник мониторинга\" должно быть заполнено")]
      [DataMember(Name = "isInMonitoring", EmitDefaultValue = false)]
      public virtual bool IsInMonitoring { get; set; }
      
      
      /// <summary>
      /// ОКВЭД
      /// </summary>
      [InverseProperty("Organizations")]
      [DataMember(Name = "okved", EmitDefaultValue = false)]
      public virtual ICollection<Okved> Okved { get; set; }
      
      /// <summary>
      /// Банковские реквизиты
      /// </summary>
      [InverseProperty("Organization")]
      [DataMember(Name = "bank", EmitDefaultValue = false)]
      public virtual ICollection<OrganizationBank> Bank { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Organization")]
      [DataMember(Name = "vedomstvo", EmitDefaultValue = false)]
      public virtual ICollection<LimitOnVedomstvo> Vedomstvo { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Organization")]
      [DataMember(Name = "limitOrganization", EmitDefaultValue = false)]
      public virtual ICollection<LimitOnOrganization> LimitOrganization { get; set; }
      
      /// <summary>
      /// Адреса учереждения социальной защиты
      /// </summary>
      [InverseProperty("Organisation")]
      [DataMember(Name = "orphanageOrganizationAddresses", EmitDefaultValue = false)]
      public virtual ICollection<OrphanageAddress> OrphanageOrganizationAddresses { get; set; }
      
      /// <summary>
      /// История изменений детского дома
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")][Display(Description = "История изменений детского дома")]
      public virtual long? HistoryLinkId { get; set; }
      /// <summary>
      /// История изменений детского дома
      /// </summary>
      [Display(Description = "История изменений детского дома")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Связь сотрудника социальной защитой с орагнизацией социальной защиты
      /// </summary>
      [InverseProperty("Organisaton")]
      [DataMember(Name = "organisatonCollaborators", EmitDefaultValue = false)]
      public virtual ICollection<OrganisatorCollaborator> OrganisatonCollaborators { get; set; }
      
      /// <summary>
      /// Мониторинг. Cведения о численности детей (Участник мониторинга)
      /// </summary>
      [InverseProperty("Organisation")]
      [DataMember(Name = "monitoringChildrenNumberInformations", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringChildrenNumberInformation> MonitoringChildrenNumberInformations { get; set; }
      
      /// <summary>
      /// Мониторинг. Cведения о финансировании (Участник мониторинга)
      /// </summary>
      [InverseProperty("Organisation")]
      [DataMember(Name = "monitoringFinanceInformations", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringFinancialInformation> MonitoringFinanceInformations { get; set; }
      
      /// <summary>
      /// Связь ГБУ с объектами мониторинга (префектурами и т.д.)
      /// </summary>
      [InverseProperty("Organisation")]
      [DataMember(Name = "gBUs", EmitDefaultValue = false)]
      public virtual ICollection<MonitoringGBU> GBUs { get; set; }
      
      /// <summary>
      /// Подведомственная сеть
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Подведомственная сеть")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Подведомственная сеть
      /// </summary>
      [Display(Description = "Подведомственная сеть")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual Organization Parent { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("Entity")]
      [DataMember(Name = "entityId")]
      [Display(Description = "")]
      public virtual long? EntityId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "entity", EmitDefaultValue = false)]
      public virtual Organization Entity { get; set; }
      
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
      /// Куратор
      /// </summary>
      [ForeignKey("Curator")]
      [DataMember(Name = "curatorId")]
      [Display(Description = "Куратор")]
      public virtual long? CuratorId { get; set; }
      
      
      /// <summary>
      /// Куратор
      /// </summary>
      [Display(Description = "Куратор")]
      [DataMember(Name = "curator", EmitDefaultValue = false)]
      public virtual Account Curator { get; set; }
      
      /// <summary>
      /// Вид транспортных услуг
      /// </summary>
      [InverseProperty("Organization")]
      [DataMember(Name = "typeOfTransport", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfTransport> TypeOfTransport { get; set; }

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