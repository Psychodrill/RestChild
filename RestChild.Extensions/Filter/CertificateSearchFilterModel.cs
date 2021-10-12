using System;
using System.Collections.Generic;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class CertificateSearchFilterModel
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// Номер договора с отдыхающим
        /// </summary>
        public virtual string ContractNumber { get; set; }

        /// <summary>
        /// Дата заключения договора с отдыхающим
        /// </summary>
        public virtual DateTime? ContractDate { get; set; }

        /// <summary>
        /// Период отдыха c
        /// </summary>
        public virtual DateTime? RestDateFrom { get; set; }

        /// <summary>
        /// Период отдыха по
        /// </summary>
        public virtual DateTime? RestDateTo { get; set; }

        /// <summary>
        /// Наименование организации отдыха и оздоровления
        /// </summary>
        public virtual string Place { get; set; }

        /// <summary>
        /// Стоимость договора общая
        /// </summary>
        public virtual decimal? FullPrice { get; set; }

        /// <summary>
        /// Стоимость договора на ребенка
        /// </summary>
        public virtual decimal? PriceForChild { get; set; }

        /// <summary>
        /// Дата гашения сертификата
        /// </summary>
        public virtual DateTime? DatePaidOff { get; set; }

        /// <summary>
        /// Место отдыха (регион)
        /// </summary>
        public virtual string Region { get; set; }


        /// <summary>
        /// связь погашения сертификата и заявления
        /// </summary>
        public virtual long? RequestId { get; set; }


        /// <summary>
        /// связь погашения сертификата и заявления
        /// </summary>
        public virtual Request Request { get; set; }

        /// <summary>
        /// Связь сертификата с его статусом
        /// </summary>
        public virtual long? StateMachineStateId { get; set; }


        /// <summary>
        /// Связь сертификата с его статусом
        /// </summary>
        public virtual StateMachineState StateMachineState { get; set; }

        /// <summary>
        /// Последнее сохранение
        /// </summary>
        public virtual long LastUpdateTick { get; set; }

        /// <summary>
        /// Внешний ключ
        /// </summary>
        public virtual long? Eid { get; set; }

        /// <summary>
        /// Статус обмена по сущности
        /// </summary>
        public virtual long? EidSendStatus { get; set; }
        public string ApplicantFio { get; set; }
        public string ChildFio { get; set; }
        public string AttendantFio { get; set; }
        public long RegionId { get; set; }
        public long DistrictId { get; set; }
        public long TimeOfRestId { get; set; }
        public long PlaceOfRestId { get; set; }
        public Hotels Hotels { get; set; }        
        public long? HotelsId { get; set; }
        public long SourceId { get; set; }

        public long CreateUserId { get; set; }

        public Account CreateUser { get; set; }
        public bool? BaseRegisterBenefitApprove { get; set; }

        public bool? InteragencyBenefitApprove { get; set; }
        public int? AgeStart { get; set; }
        public int? AgeEnd { get; set; }

        public long BenefitTypeId { get; set; }

        public IEnumerable<YearOfRest> ListOfYears { get; set; }
        public IEnumerable<PlaceOfRest> PlacesOfRest { get; set; }
        public IEnumerable<Hotels> Organizations { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
        public long? ContractId { get; set; }
        public DateTime? StartRequestDate { get; set; }

        public DateTime? EndRequestDate { get; set; }

        public DateTime? StartChangeStatusDate { get; set; }

        public DateTime? EndChangeStatustDate { get; set; }

        public DateTime? RestDateFromFrom { get; set; }

        public DateTime? RestDateFromTo { get; set; }

        public DateTime? RestDateToFrom { get; set; }

        public DateTime? RestDateToTo { get; set; }
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public long TypeOfRestrictionId { get; set; }
        public long? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public long? VedomstvoId { get; set; }
        public string VedomstvoName { get; set; }
        public RestCategoryEnum? RestCategory { get; set; }
        public int? PaymentStatus { get; set; }

        public string CertificateNumber { get; set; }
        public long AggregatedStatusOfRequest { get; set; }
        public bool SourceSetted { get; set; }
        public int TotalRecordsCount { get; set; }
        public bool? WillRest { get; set; }
        public bool? Resting { get; set; }
        public bool? Rested { get; set; }
        public bool? AttendantWith { get; set; }
        public bool? AttendantWO { get; set; }


        /// <summary>
        ///     СНИЛС
        /// </summary>
        public string SNILS { get; set; }
        /// <summary>
        ///     Сертификат погашен
        /// </summary>
        public bool CertificateRepaid { get; set; }
    }
}
