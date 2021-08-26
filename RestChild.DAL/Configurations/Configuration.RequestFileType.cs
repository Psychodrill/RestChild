using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы файлов по виду заявления
        /// </summary>
        private static void RequestFileType(Context context)
        {
            var childRest = new long[]
            {
                (int) TypeOfRestEnum.ChildRestOrphanCamps, (int) TypeOfRestEnum.ChildRestCamps,
                (int) TypeOfRestEnum.TentChildrenCampOrphan, (int) TypeOfRestEnum.TentChildrenCamp,
                (int) TypeOfRestEnum.ChildRestFederalCamps
            };

            var jointRest = new long[]
            {
                (int) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex, (int) TypeOfRestEnum.RestWithParentsInvalid,
                (int) TypeOfRestEnum.RestWithParentsOrphan, (int) TypeOfRestEnum.RestWithParentsComplex,
                (int) TypeOfRestEnum.RestWithParentsPoor, (int) TypeOfRestEnum.RestWithParentsOther
            };

            var certRest = new long[]
            {
                (int) TypeOfRestEnum.MoneyOn3To7, (int) TypeOfRestEnum.MoneyOn7To15,
                (int) TypeOfRestEnum.MoneyOnInvalidOn4To17
            };

            var youthRest = new long[]
            {
                (int) TypeOfRestEnum.YouthRestOrphanCamps
            };

            context.RequestFileType.AddOrUpdate(r => r.Id,
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.Request,
                    ForMpgu = false,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Заявление",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.Applicant,
                    ForMpgu = true,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документ, удостоверяющий личность заявителя",
                    CodeChed = "DocumentConfirmingIdentityOfApplicant",
                    CodeAsGuf = "11269"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.RightApplicant,
                    ForMpgu = true,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документ, подтверждающий полномочия законного представителя",
                    CodeChed = "DocumentConfirmingAuthorityOfLegalRepresentative",
                    CodeAsGuf = "11270"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.RightAgent,
                    ForMpgu = true,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документ, подтверждающий представление интересов законного представителя",
                    CodeChed = "RepresentingInterestsOfLegalRepresentative",
                    CodeAsGuf = "11271"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.Child,
                    ForMpgu = true,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документ, удостоверяющий личность ребёнка",
                    CodeChed = "DocumentConfirmingIdentityOfChild",
                    CodeAsGuf = "11272"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.ChildRegistration,
                    ForMpgu = true,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документ, подтверждающий место жительства ребёнка в городе Москве",
                    CodeChed = "DocumentConfirmingPlaceOfResidenceInMoscow",
                    CodeAsGuf = "11274"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.ChildBenefit,
                    ForMpgu = true,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документ, подтверждающий льготную категорию ребёнка",
                    CodeChed = "DocumentConfirmingPreferentialCategoryChild",
                    CodeAsGuf = "11273"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.ChildReason,
                    ForMpgu = false,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Подтверждающие уважительные причины неиспользования ранее выданной путевки",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.RestChildApprove,
                    ForMpgu = false,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Документы, подтверждающие отдых",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.CertificateOnPayment,
                    ForMpgu = false,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Сертификат на субсидию",
                    CodeChed = "CertificateOnPayment",
                    CodeAsGuf = "14582"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.CertificateOnRest,
                    ForMpgu = false,
                    ForOperator = true,
                    IsActive = true,
                    Name = "Сертификат на отдых",
                    CodeChed = "CertificateOnRest",
                    CodeAsGuf = "14581"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.NotificationRefuse,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = false,
                    Name = "Уведомление об отказе в предоставлении услуги",
                    CodeChed = "CopyOfLetterOfRefusal",
                    CodeAsGuf = "10604"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.Notifications,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = false,
                    Name = "Уведомления о предоставлении услуги",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.ConfirmRepresentationApplicantsInterests,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "Подтверждающие представление интересов заявителя",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.BankCredentials,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "Банковские реквизиты",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.ApplicantSnils,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "СНИЛС заявителя",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.PersonsConfirmingPrivilegedCategoryOrphans,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name =
                        "Подтверждающие льготную категорию лица из числа детей-сирот и детей, оставшихся без попечения родителей",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.PersonsConfirmingAddressOrphans,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name =
                        "Подтверждающие место жительства лица из числа детей-сирот и детей, оставшихся без попечения родителей",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.AttendantIdentity,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "Удостоверяющие личность сопровождающего лица",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.AttendantSnils,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "СНИЛС сопровождающего лица",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.AttendantConfirmRepresentationInterests,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "Подтверждающие полномочия доверенного лица на сопровождение",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                },
                new RequestFileType
                {
                    Id = (long) RequestFileTypeEnum.ChildSnils,
                    ForMpgu = false,
                    ForOperator = false,
                    IsActive = true,
                    Name = "СНИЛС ребёнка",
                    CodeChed = "OtherConfirmingDocuments",
                    CodeAsGuf = "11275"
                });

            context.SaveChanges();

            var rfts = context.RequestFileType.ToList();
            foreach (var rft in rfts)
            {
                rft.TypeOfRests.Clear();
                rft.Eid = rft.Id;
                if (rft.Id == (long) RequestFileTypeEnum.RightApplicant)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id) ||
                        childRest.Contains(t.Id) || certRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.RightAgent)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id) ||
                        childRest.Contains(t.Id) || certRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.Child)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id) ||
                        childRest.Contains(t.Id) || certRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.ChildRegistration)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id) ||
                        childRest.Contains(t.Id) || certRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.ChildBenefit)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id) ||
                        childRest.Contains(t.Id) || certRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.ChildReason)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        jointRest.Contains(t.Id) || childRest.Contains(t.Id) || youthRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.RestChildApprove)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                            t.Id == (int) TypeOfRestEnum.Compensation ||
                            t.Id == (int) TypeOfRestEnum.CompensationYouthRest)
                        .ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.ConfirmRepresentationApplicantsInterests)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest
                        .Where(t => t.Id == (int) TypeOfRestEnum.CompensationYouthRest)
                        .ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.BankCredentials)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                            t.Id == (int) TypeOfRestEnum.Compensation ||
                            t.Id == (int) TypeOfRestEnum.CompensationYouthRest)
                        .ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.PersonsConfirmingPrivilegedCategoryOrphans)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.CompensationYouthRest || youthRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.PersonsConfirmingAddressOrphans)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.CompensationYouthRest || youthRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.AttendantIdentity)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest
                        .Where(t => t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.AttendantSnils)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest
                        .Where(t => t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.AttendantConfirmRepresentationInterests)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t => jointRest.Contains(t.Id)).ToList());
                }
                else if (rft.Id == (long) RequestFileTypeEnum.ChildSnils)
                {
                    rft.TypeOfRests.AddRange(context.TypeOfRest.Where(t =>
                        t.Id == (int) TypeOfRestEnum.Compensation || jointRest.Contains(t.Id) ||
                        certRest.Contains(t.Id) || childRest.Contains(t.Id)).ToList());
                }

                context.SaveChanges();
            }
        }
    }
}
