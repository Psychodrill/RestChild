using System;
using System.Collections.Generic;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class RequestSearchFilterModel
    {
        public string RequestNumber { get; set; }
        public long YearOfRestId { get; set; }

        /// <summary>
        ///     больше чем текущий год
        /// </summary>
        public bool MoreThenSelectedYear { get; set; }

        public string ApplicantFio { get; set; }
        public string ChildFio { get; set; }
        public long TypeOfRestId { get; set; }
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

        public DateTime? RestDateFromFrom { get; set; }

        public DateTime? RestDateFromTo { get; set; }

        public DateTime? RestDateToFrom { get; set; }

        public DateTime? RestDateToTo { get; set; }

        public DateTime? StartRequestDate { get; set; }

        public DateTime? EndRequestDate { get; set; }

        public DateTime? StartChangeStatusDate { get; set; }

        public DateTime? EndChangeStatustDate { get; set; }

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

        public long TypeOfDecision { get; set; }

        public bool SourceSetted { get; set; }
        public int TotalRecordsCount { get; set; }

        /// <summary>
        ///     Подтверждение в базовом регистре льготной категории
        /// </summary>
        public bool? BaseRegistryPreferentialCategoryCheck { get; set; }

        /// <summary>
        ///     Подтверждение в базовом регистре родства
        /// </summary>
        public bool? BaseRegistryPassportCheck { get; set; }

        /// <summary>
        ///     Подтверждение в базовом регистре паспорта
        /// </summary>
        public bool? BaseRegistryRelationshipCheck { get; set; }

        /// <summary>
        ///     Подтверждение в базовом регистре СНИЛС
        /// </summary>
        public bool? BaseRegistrySNILSCheck { get; set; }

        /// <summary>
        ///     СНИЛС
        /// </summary>
        public string SNILS { get; set; }

        /// <summary>
        ///     Способ доставки в организацию отдыха и оздоровления
        /// </summary>
        public long? TransferToId { get; set; }

        /// <summary>
        ///     Способ доставки из организации отдыха и оздоровления
        /// </summary>
        public long? TransferFromId { get; set; }

        /// <summary>
        ///     Сертификат погашен
        /// </summary>
        public bool CertificateRepaid { get; set; }

        /// <summary>
        ///     Причина отказа
        /// </summary>
        public long? DeclineReasonId { get; set; }

        /// <summary>
        ///     Комментарий сотрудника
        /// </summary>
        public bool? WithKC { get; set; }
    }
}
