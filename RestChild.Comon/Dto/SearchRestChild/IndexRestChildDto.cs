using System;
using System.Runtime.Serialization;
using RestChild.Comon.Enumeration;

namespace RestChild.Comon.Dto.SearchRestChild
{
    [DataContract(Name = "IndexRestChild", Namespace = "http://schemas.datacontract.org/2004/07/RestChild.Comon.Dto")]
    public class IndexRestChildDto
    {
        [DataMember] public string Key { get; set; }

        [DataMember] public long AttendantId { get; set; }

        [DataMember] public long ChildId { get; set; }

        [DataMember] public long PlaceOfRestId { get; set; }

        [DataMember] public string RequestNumber { get; set; }

        [DataMember] public long RequestId { get; set; }

        [DataMember] public long YearOfRest { get; set; }

        [DataMember] public long TypeOfDecision { get; set; }

        [DataMember] public string RequestNumberFromMpgu { get; set; }

        [DataMember] public string CertificateNumber { get; set; }

        [DataMember] public DateTime RequestSupplyDate { get; set; }

        [DataMember] public string Status { get; set; }

        [DataMember] public long TypeOfRestId { get; set; }

        [DataMember] public long HotelId { get; set; }

        [DataMember] public string HotelName { get; set; }

        [DataMember] public string HotelAddress { get; set; }

        [DataMember] public long TimeOfRestId { get; set; }

        [DataMember] public long SubjectOfRestId { get; set; }

        [DataMember] public string LastName { get; set; }

        [DataMember] public string FirstName { get; set; }

        [DataMember] public string MiddleName { get; set; }

        [DataMember] public DateTime BirthDate { get; set; }

        [DataMember] public bool Male { get; set; }

        [DataMember] public long Age { get; set; }

        [DataMember] public string PlaceOfBirth { get; set; }

        [DataMember] public string DocumentType { get; set; }

        [DataMember] public string DocumentSeria { get; set; }

        [DataMember] public string DocumentNumber { get; set; }

        [DataMember] public DateTime DocumentIssueDate { get; set; }

        [DataMember] public string DocumentSubjectIssue { get; set; }

        [DataMember] public string TypeOfRestriction { get; set; }

        [DataMember] public long BenefitTypeId { get; set; }

        [DataMember] public string Address { get; set; }

        [DataMember] public long ApplicantId { get; set; }

        [DataMember] public string ApplicantLastName { get; set; }

        [DataMember] public string ApplicantFirstName { get; set; }

        [DataMember] public string ApplicantMiddleName { get; set; }

        [DataMember] public string ApplicantDocumentType { get; set; }

        [DataMember] public long ApplicantDocumentTypeId { get; set; }

        [DataMember] public string ApplicantDocumentSeria { get; set; }

        [DataMember] public string ApplicantDocumentNumber { get; set; }

        [DataMember] public DateTime ApplicantBirthDate { get; set; }

        [DataMember] public string ApplicantPhone { get; set; }

        [DataMember] public string ApplicantEmail { get; set; }

        [DataMember] public string OrganizationShortName { get; set; }

        [DataMember] public string OrganizationName { get; set; }

        [DataMember] public long Organization { get; set; }

        [DataMember] public string VedomstvoName { get; set; }

        [DataMember] public string VedomstvoShortName { get; set; }

        [DataMember] public DateTime DateIncome { get; set; }

        [DataMember] public DateTime DateOutcome { get; set; }

        [DataMember] public RestCategoryEnum RestCategory { get; set; }

        [DataMember] public bool PaymentStatus { get; set; }

        [DataMember] public long ListOfChildrenId { get; set; }

        [DataMember] public string ListOfChildrenName { get; set; }

        [DataMember] public long VedomstvoId { get; set; }

        [DataMember] public long DistrictId { get; set; }

        [DataMember] public long RegionId { get; set; }

        [DataMember] public long SourceId { get; set; }

        [DataMember] public long OperatorId { get; set; }

        [DataMember] public bool BenefitApprove { get; set; }

        [DataMember] public bool IsApprovedInInteragency { get; set; }

        [DataMember] public long FromNotNeedTicketReasonId { get; set; }

        [DataMember] public string FromNotNeedTicketReason { get; set; }

        [DataMember] public long ToNotNeedTicketReasonId { get; set; }

        [DataMember] public string ToNotNeedTicketReason { get; set; }
    }

    public class FullIndexRestChildDto
    {
        public IndexRestChildDto IndexRestChildDto { get; set; }
        public string BenefitType { get; set; }
        public string TimeOfRest { get; set; }
        public string PlaceOfRest { get; set; }
        public string TypeOfRest { get; set; }
        public string ParentTypeOfRest { get; set; }
    }
}
