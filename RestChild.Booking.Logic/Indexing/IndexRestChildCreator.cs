using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Booking.Logic.Indexing
{
    public static class IndexRestChildCreator
    {
        private static IndexRestChildDto CreateIndexRestManDto(Applicant attendant)
        {
            var to = attendant.LinkToPeoples.FirstOrDefault(l => l.TransportId == attendant.Bout?.TransportInfoToId);
            var from = attendant.LinkToPeoples.FirstOrDefault(l =>
                l.TransportId == attendant.Bout?.TransportInfoFromId);

            return new IndexRestChildDto
            {
                //export
                Key = $"a{attendant.Id}",
                AttendantId = attendant.Id,
                PaymentStatus = attendant.Payed,

                //RestMan
                LastName = attendant.LastName ?? string.Empty,
                MiddleName = attendant.MiddleName ?? string.Empty,
                FirstName = attendant.FirstName ?? string.Empty,
                BirthDate = DateTime.SpecifyKind(attendant.DateOfBirth ?? default, DateTimeKind.Utc),
                Male = attendant.Male ?? false,
                Age = attendant.GetAgeInYears() ?? 0,
                PlaceOfBirth = attendant.PlaceOfBirth ?? string.Empty,
                DocumentType = attendant.DocumentType?.Name ?? string.Empty,
                DocumentSeria = attendant.DocumentSeria ?? string.Empty,
                DocumentNumber = attendant.DocumentNumber ?? string.Empty,
                DocumentIssueDate = DateTime.SpecifyKind(attendant.DocumentDateOfIssue ?? default, DateTimeKind.Utc),
                DocumentSubjectIssue = attendant.DocumentSubjectIssue ?? string.Empty,
                FromNotNeedTicketReasonId = from?.NotNeedTicketReasonId ?? 0,
                FromNotNeedTicketReason = from?.NotNeedTicketReason?.Name,
                ToNotNeedTicketReasonId = to?.NotNeedTicketReasonId ?? 0,
                ToNotNeedTicketReason = to?.NotNeedTicketReason?.Name,
                TypeOfDecision = 1
            };
        }

        private static IndexRestChildDto CreateIndexRestManDto(Child child)
        {
            var to = child.LinkToPeoples.FirstOrDefault(l => l.TransportId == child.Bout?.TransportInfoToId);
            var from = child.LinkToPeoples.FirstOrDefault(l => l.TransportId == child.Bout?.TransportInfoFromId);
            return new IndexRestChildDto
            {
                ListOfChildrenId = child.ChildListId ?? 0,

                RestCategory = RestCategoryEnum.Child,
                //filter
                RegionId = child.Address?.BtiRegionId ?? 0,
                DistrictId = child.Address?.BtiDistrictId ?? 0,
                BenefitApprove = child.BenefitApprove,
                IsApprovedInInteragency = child.IsApprovedInInteragency ?? false,

                //export
                Key = $"c{child.Id}",
                ChildId = child.Id,
                CertificateNumber = child.DocumentSeriaCertOfBirth ?? string.Empty,
                PaymentStatus = child.Payed,

                //child
                LastName = child.LastName ?? string.Empty,
                MiddleName = child.MiddleName ?? string.Empty,
                FirstName = child.FirstName ?? string.Empty,
                BirthDate = DateTime.SpecifyKind(child.DateOfBirth ?? default, DateTimeKind.Utc),
                Male = child.Male,
                Age = child.GetAgeInYears() ?? 0,
                PlaceOfBirth = child.PlaceOfBirth ?? string.Empty,
                DocumentType = child.DocumentType?.Name ?? string.Empty,
                DocumentSeria = child.DocumentSeria ?? string.Empty,
                DocumentNumber = child.DocumentNumber ?? string.Empty,
                DocumentIssueDate = DateTime.SpecifyKind(child.DocumentDateOfIssue ?? default, DateTimeKind.Utc),
                DocumentSubjectIssue = child.DocumentSubjectIssue ?? string.Empty,
                TypeOfRestriction = child.TypeOfRestriction?.Name ?? string.Empty,
                BenefitTypeId = child.BenefitTypeId ?? 0,
                Address = child.Address?.Name ?? string.Empty,
                FromNotNeedTicketReasonId = from?.NotNeedTicketReasonId ?? 0,
                FromNotNeedTicketReason = from?.NotNeedTicketReason?.Name,
                ToNotNeedTicketReasonId = to?.NotNeedTicketReasonId ?? 0,
                ToNotNeedTicketReason = to?.NotNeedTicketReason?.Name,
                TypeOfDecision = child?.Request?.RequestOnMoney == true ? 2 : 1
            };
        }

        private static void AddListOfChildrenInfo(IndexRestChildDto indexRestChildDto, ListOfChilds listOfChilds,
            IDictionary<long, Organization> vedomstva)
        {
            //filter
            indexRestChildDto.YearOfRest = listOfChilds.LimitOnOrganization?.LimitOnVedomstvo?.YearOfRestId ?? 0;

            //export
            indexRestChildDto.TypeOfRestId = (long) TypeOfRestEnum.SpecializedСamp;
            indexRestChildDto.ListOfChildrenName = listOfChilds.Name;

            var organization = listOfChilds.LimitOnOrganization?.Organization;
            var tour = listOfChilds.LimitOnOrganization?.Tour;

            if (organization != null)
            {
                indexRestChildDto.Organization = organization.Id;

                if (!organization.ParentId.HasValue)
                {
                    indexRestChildDto.VedomstvoShortName = organization.ShortName ?? string.Empty;
                    indexRestChildDto.VedomstvoName = organization.Name ?? string.Empty;
                    indexRestChildDto.VedomstvoId = organization.Id;
                }
                else
                {
                    indexRestChildDto.OrganizationShortName = organization.ShortName ?? string.Empty;
                    indexRestChildDto.OrganizationName = organization.Name ?? string.Empty;
                    indexRestChildDto.VedomstvoId = vedomstva[organization.ParentId.Value].Id;
                    indexRestChildDto.VedomstvoShortName =
                        vedomstva[organization.ParentId.Value].ShortName ?? string.Empty;
                    indexRestChildDto.VedomstvoName = vedomstva[organization.ParentId.Value].Name ?? string.Empty;
                }
            }

            indexRestChildDto.PlaceOfRestId = tour?.Hotels?.PlaceOfRestId ?? 0;
            indexRestChildDto.TimeOfRestId = tour?.TimeOfRestId ?? 0;
            indexRestChildDto.HotelName = tour?.Hotels?.Name ?? string.Empty;
            indexRestChildDto.HotelId = tour?.HotelsId ?? 0;
            indexRestChildDto.HotelAddress = tour?.Hotels?.Address ?? string.Empty;
            indexRestChildDto.DateIncome = DateTime.SpecifyKind(tour?.DateIncome ?? default, DateTimeKind.Utc);
            indexRestChildDto.DateOutcome = DateTime.SpecifyKind(tour?.DateOutcome ?? default, DateTimeKind.Utc);
            indexRestChildDto.SubjectOfRestId = tour?.SubjectOfRestId ?? 0;
            indexRestChildDto.CertificateNumber = listOfChilds.CertificateNumber ?? string.Empty;
        }

        public static IndexRestChildDto CreateIndexRestManDto(Child child, Request request)
        {
            var indexRestChildDto = CreateIndexRestManDto(child);

            indexRestChildDto.RequestNumber = request.RequestNumber ?? string.Empty;
            indexRestChildDto.RequestId = request.Id;
            //filter
            indexRestChildDto.YearOfRest = request.YearOfRestId ?? 0;

            //export
            indexRestChildDto.RequestNumberFromMpgu = request.RequestNumberMpgu ?? string.Empty;
            indexRestChildDto.RequestSupplyDate =
                DateTime.SpecifyKind(request.DateRequest ?? default, DateTimeKind.Utc);
            indexRestChildDto.Status = request.Status?.Name ?? string.Empty;
            indexRestChildDto.TypeOfRestId = request.TypeOfRest?.Id ?? 0;
            indexRestChildDto.SubjectOfRestId = request.SubjectOfRestId ?? 0;

            indexRestChildDto.PlaceOfRestId = request.Hotels?.PlaceOfRestId ?? request.Tour?.Hotels?.PlaceOfRestId ?? 0;
            indexRestChildDto.TimeOfRestId = request.TimeOfRestId ?? 0;
            indexRestChildDto.HotelId = request.Tour?.HotelsId ?? request.HotelsId ?? 0;
            indexRestChildDto.HotelName = request.Tour?.Hotels?.Name ?? request.Hotels?.Name ?? string.Empty;
            indexRestChildDto.HotelAddress = request.Tour?.Hotels?.Address ?? request.Hotels?.Address;
            indexRestChildDto.DateIncome = DateTime.SpecifyKind(request.Tour?.DateIncome ?? default, DateTimeKind.Utc);
            indexRestChildDto.DateOutcome =
                DateTime.SpecifyKind(request.Tour?.DateOutcome ?? default, DateTimeKind.Utc);
            indexRestChildDto.SubjectOfRestId = request.Tour?.SubjectOfRestId ?? 0;
            indexRestChildDto.CertificateNumber = request.CertificateNumber ?? string.Empty;

            //Applicant
            indexRestChildDto.ApplicantId = request.ApplicantId ?? 0;
            indexRestChildDto.ApplicantLastName = request.Applicant?.LastName ?? child.ContactLastName ?? string.Empty;
            indexRestChildDto.ApplicantFirstName =
                request.Applicant?.FirstName ?? child.ContactFirstName ?? string.Empty;
            indexRestChildDto.ApplicantMiddleName =
                request.Applicant?.MiddleName ?? child.ContactMiddleName ?? string.Empty;
            indexRestChildDto.ApplicantDocumentType = request.Applicant?.DocumentType?.Name ?? string.Empty;
            indexRestChildDto.ApplicantDocumentSeria = request.Applicant?.DocumentSeria ?? string.Empty;
            indexRestChildDto.ApplicantDocumentNumber = request.Applicant?.DocumentNumber ?? string.Empty;
            indexRestChildDto.ApplicantBirthDate = DateTime.SpecifyKind(request.Applicant?.DateOfBirth ?? default,
                DateTimeKind.Utc);
            indexRestChildDto.ApplicantDocumentTypeId = request.Applicant?.DocumentTypeId ?? 0;
            indexRestChildDto.ApplicantPhone = request.Applicant?.Phone ?? child.ContactPhone ?? string.Empty;
            indexRestChildDto.ApplicantEmail = request.Applicant?.Email ?? string.Empty;

            return indexRestChildDto;
        }

        public static IndexRestChildDto CreateIndexRestManDto(Applicant attendant, Request request)
        {
            var indexRestChildDto = CreateIndexRestManDto(attendant);

            indexRestChildDto.RestCategory = RestCategoryEnum.Attendant;
            indexRestChildDto.TypeOfDecision = request?.RequestOnMoney == true ? 2 : 1;

            indexRestChildDto.RequestNumber = request?.RequestNumber ?? string.Empty;

            //filter
            indexRestChildDto.YearOfRest = request?.YearOfRestId ?? 0;

            indexRestChildDto.RequestNumberFromMpgu = request?.RequestNumberMpgu ?? string.Empty;
            indexRestChildDto.CertificateNumber = request?.CertificateNumber ?? string.Empty;
            indexRestChildDto.RequestSupplyDate = DateTime.SpecifyKind(request?.DateRequest ?? default,
                DateTimeKind.Utc);

            indexRestChildDto.Status = request?.Status?.Name ?? string.Empty;
            indexRestChildDto.TypeOfRestId = request?.TypeOfRest?.Id ?? 0;
            indexRestChildDto.SubjectOfRestId = request?.SubjectOfRestId ?? 0;

            //Applicant
            indexRestChildDto.ApplicantId = request?.ApplicantId ?? 0;
            indexRestChildDto.ApplicantLastName = request?.Applicant?.LastName ?? string.Empty;
            indexRestChildDto.ApplicantFirstName = request?.Applicant?.FirstName ?? string.Empty;
            indexRestChildDto.ApplicantMiddleName = request?.Applicant?.MiddleName ?? string.Empty;
            indexRestChildDto.ApplicantDocumentType = request?.Applicant?.DocumentType?.Name ?? string.Empty;
            indexRestChildDto.ApplicantDocumentSeria = request?.Applicant?.DocumentSeria ?? string.Empty;
            indexRestChildDto.ApplicantDocumentNumber = request?.Applicant?.DocumentNumber ?? string.Empty;
            indexRestChildDto.ApplicantBirthDate = DateTime.SpecifyKind(request?.Applicant?.DateOfBirth ?? default,
                DateTimeKind.Utc);
            indexRestChildDto.ApplicantDocumentTypeId = request?.Applicant?.DocumentTypeId ?? 0;
            indexRestChildDto.ApplicantPhone = request?.Applicant?.Phone ?? string.Empty;
            indexRestChildDto.ApplicantEmail = request?.Applicant?.Email ?? string.Empty;

            if (request != null)
            {
                indexRestChildDto.PlaceOfRestId =
                    request.Hotels?.PlaceOfRestId ?? request.Tour?.Hotels?.PlaceOfRestId ?? 0;
                indexRestChildDto.TimeOfRestId = request.TimeOfRestId ?? 0;
                indexRestChildDto.HotelId = request.Tour?.HotelsId ?? request.HotelsId ?? 0;
                indexRestChildDto.HotelName = request.Tour?.Hotels?.Name ?? request.Hotels?.Name ?? string.Empty;
                indexRestChildDto.HotelAddress = request.Tour?.Hotels?.Address ?? request.Hotels?.Address;
                indexRestChildDto.DateIncome =
                    DateTime.SpecifyKind(request.Tour?.DateIncome ?? default, DateTimeKind.Utc);
                indexRestChildDto.DateOutcome = DateTime.SpecifyKind(request.Tour?.DateOutcome ?? default,
                    DateTimeKind.Utc);
                indexRestChildDto.SubjectOfRestId = request.Tour?.SubjectOfRestId ?? 0;
                indexRestChildDto.CertificateNumber = request.CertificateNumber ?? string.Empty;
            }

            return indexRestChildDto;
        }

        public static IndexRestChildDto CreateIndexRestManDto(Child child, ListOfChilds listOfChilds,
            IDictionary<long, Organization> vedomstva)
        {
            var indexRestChildDto = CreateIndexRestManDto(child);

            indexRestChildDto.RestCategory = RestCategoryEnum.Child;
            indexRestChildDto.ApplicantFirstName = child.ContactFirstName;
            indexRestChildDto.ApplicantLastName = child.ContactLastName;
            indexRestChildDto.ApplicantMiddleName = child.ContactMiddleName;
            indexRestChildDto.ApplicantPhone = child.ContactPhone;
            AddListOfChildrenInfo(indexRestChildDto, listOfChilds, vedomstva);

            return indexRestChildDto;
        }

        public static IndexRestChildDto CreateIndexRestManDto(Applicant teacher, ListOfChilds listOfChilds,
            IDictionary<long, Organization> vedomstva)
        {
            var to = teacher.LinkToPeoples.FirstOrDefault(l => l.TransportId == teacher.Bout?.TransportInfoToId);
            var from = teacher.LinkToPeoples.FirstOrDefault(l => l.TransportId == teacher.Bout?.TransportInfoFromId);

            var indexRestChildDto = new IndexRestChildDto
            {
                //export
                Key = $"a{teacher.Id}",
                ApplicantId = teacher.Id,

                //RestMan
                LastName = teacher.LastName ?? string.Empty,
                MiddleName = teacher.MiddleName ?? string.Empty,
                FirstName = teacher.FirstName ?? string.Empty,
                BirthDate = DateTime.SpecifyKind(teacher.DateOfBirth ?? default, DateTimeKind.Utc),
                Male = teacher.Male ?? false,
                Age = teacher.GetAgeInYears() ?? 0,
                PlaceOfBirth = teacher.PlaceOfBirth ?? string.Empty,
                DocumentType = teacher.DocumentType?.Name ?? string.Empty,
                DocumentSeria = teacher.DocumentSeria ?? string.Empty,
                DocumentNumber = teacher.DocumentNumber ?? string.Empty,
                DocumentIssueDate = DateTime.SpecifyKind(teacher.DocumentDateOfIssue ?? default, DateTimeKind.Utc),
                DocumentSubjectIssue = teacher.DocumentSubjectIssue ?? string.Empty,
                FromNotNeedTicketReasonId = from?.NotNeedTicketReasonId ?? 0,
                FromNotNeedTicketReason = from?.NotNeedTicketReason?.Name,
                ToNotNeedTicketReasonId = to?.NotNeedTicketReasonId ?? 0,
                ToNotNeedTicketReason = to?.NotNeedTicketReason?.Name
            };

            if (teacher.ChildListId.HasValue)
            {
                indexRestChildDto.RestCategory = RestCategoryEnum.Teacher;
            }

            AddListOfChildrenInfo(indexRestChildDto, listOfChilds, vedomstva);

            return indexRestChildDto;
        }
    }
}
