using System;
using System.Runtime.Serialization;
using RestChild.Comon.Enumeration;

namespace RestChild.Comon.Dto.SearchRestChild
{
    /// <summary>
    ///     фильтр для поиска по заявлениям
    /// </summary>
    [DataContract]
    public class RestChildFilterDto
    {
        [DataMember] public string RequestNumber { get; set; }

        [DataMember] public string ApplicantFIO { get; set; }

        [DataMember] public string ChildFIO { get; set; }

        [DataMember] public long YearOfRestId { get; set; }

        [DataMember] public long[] YearOfRests { get; set; }

        [DataMember] public long TypeOfRest { get; set; }

        [DataMember] public long TimeOfRestId { get; set; }

        [DataMember] public long PlaceOfRest { get; set; }

        [DataMember] public string Hotel { get; set; }

        [DataMember] public long? HotelId { get; set; }

        [DataMember] public long DistrictId { get; set; }

        [DataMember] public long RegionId { get; set; }
        [DataMember] public long SourceId { get; set; }
        [DataMember] public DateTime? RequestDateSupplyStart { get; set; }

        [DataMember] public DateTime? RequestDateSupplyEnd { get; set; }

        [DataMember] public long OperatorId { get; set; }
        [DataMember] public bool? BenefitApprove { get; set; }

        [DataMember] public bool? IsApprovedInInteragency { get; set; }

        [DataMember] public int? AgeStart { get; set; }
        [DataMember] public int? AgeEnd { get; set; }

        /// <summary>
        ///     вид льготы
        /// </summary>
        [DataMember]
        public long? BenefitTypeId { get; set; }

        /// <summary>
        ///     вид ограничения
        /// </summary>
        [DataMember]
        public string TypeOfRestriction { get; set; }

        /// <summary>
        ///     номер страницы
        /// </summary>
        [DataMember]
        public int PageNumber { get; set; }

        /// <summary>
        ///     размер страницы
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        ///     подразделение
        /// </summary>
        [DataMember]
        public long OrganizationId { get; set; }

        /// <summary>
        ///     ведомство
        /// </summary>
        [DataMember]
        public long VedomstvoId { get; set; }

        /// <summary>
        ///     заявление на выплату
        /// </summary>
        [DataMember]
        public long TypeOfDecision { get; set; }

        /// <summary>
        ///     котегория отдыхающих
        /// </summary>
        [DataMember]
        public RestCategoryEnum? RestCategory { get; set; }

        /// <summary>
        ///     статус оплаты
        /// </summary>
        [DataMember]
        public bool? PaymentStatus { get; set; }
    }
}
