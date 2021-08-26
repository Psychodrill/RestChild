using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды льготной категории
        /// </summary>
        private static void BenefitType(Context context)
        {
            var id = context.BenefitType.Select(b => b.Id).Max();
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (BenefitType, RESEED, " + id + ")");

            context.BenefitType.AddOrUpdate(s => s.Id, new BenefitType
                {
                    Id = 1,
                    Name =
                        @"Дети-сироты",
                    ExnternalUid = "52,69",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = false
                }, new BenefitType
                {
                    Id = 2,
                    Name = @"Дети, оставшиеся без попечения родителей",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = false
                }, new BenefitType
                {
                    Id = 3,
                    Name = @"Дети, пострадавшие в результате террористических актов",
                    ExnternalUid = "57",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 4,
                    Name =
                        @"Дети из семей беженцев и вынужденных переселенцев",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 5,
                    Name =
                        @"Дети-жертвы вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий",
                    ExnternalUid = "57,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 6,
                    Name =
                        @"Дети из семей военнослужащих и приравненных к ним лиц, погибших или получивших увечья (ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей.",
                    ExnternalUid = "58,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 7,
                    Name =
                        @"Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств в семье, вызванных утратой имущества вследствие ограбления, пожара, затопления, разрушения или утраты жилища",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = false,
                    SameBenefitId = 47
                }, new BenefitType
                {
                    Id = 8,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = false,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 9,
                    Name =
                        @"Дети из семей, в которых оба или один из родителей являются инвалидами",
                    ExnternalUid = "56",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = false
                }, new BenefitType
                {
                    Id = 10,
                    Name =
                        @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsPoor,
                    IsActive = false,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 11,
                    Name =
                        @"Дети - инвалиды ",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsInvalid,
                    IsActive = false
                }, new BenefitType
                {
                    Id = 12,
                    Name = @"Дети-сироты",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsOrphan,
                    IsActive = false,
                    SameBenefitId = 1,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForTwoYears
                }, new BenefitType
                {
                    Id = 13,
                    Name = @"Дети, оставшиеся без попечения родителей",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsOrphan,
                    IsActive = false,
                    SameBenefitId = 2,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForTwoYears
                }, new BenefitType
                {
                    Id = 14,
                    Name = @"Дети, состоящие на учете в комиссиях по делам несовершеннолетних и защите их прав",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = false
                }, new BenefitType
                {
                    Id = 15,
                    Name =
                        @"Дети-инвалиды",
                    ExnternalUid = "24",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    NeedTypeOfRestriction = true,
                    IsActive = false,
                    SameBenefitId = 11
                }, new BenefitType
                {
                    Id = 16,
                    Name =
                        @"Дети из семей, в которых 10 и более детей",
                    ExnternalUid = "",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsOther,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 17,
                    Name =
                        @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.Compensation,
                    IsActive = true,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 18,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.Compensation,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 19,
                    Name =
                        @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    IsActive = false,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 20,
                    Name =
                        @"Дети-инвалиды",
                    ExnternalUid = "24",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    NeedTypeOfRestriction = true,
                    IsActive = false,
                    SameBenefitId = 11
                }, new BenefitType
                {
                    Id = 21,
                    Name = @"Дети-сироты",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    IsActive = false,
                    SameBenefitId = 1,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForTwoYears
                }, new BenefitType
                {
                    Id = 22,
                    Name = @"Дети, оставшиеся без попечения родителей",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    IsActive = false,
                    SameBenefitId = 2,
                    TypeOfGroupCheckId = (long) TypeOfGroupCheckEnum.ForTwoYears
                },
                new BenefitType
                {
                    Id = 23,
                    Name =
                        @"Дети-сироты",
                    ExnternalUid = "52,69",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = false,
                    SameBenefitId = 1
                }, new BenefitType
                {
                    Id = 24,
                    Name = @"Дети, оставшиеся без попечения родителей",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = false,
                    SameBenefitId = 2
                }, new BenefitType
                {
                    Id = 25,
                    Name = @"Дети, пострадавшие в результате террористических актов",
                    ExnternalUid = "57",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 3
                }, new BenefitType
                {
                    Id = 26,
                    Name =
                        @"Дети из семей беженцев и вынужденных переселенцев",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 4
                }, new BenefitType
                {
                    Id = 27,
                    Name =
                        @"Дети-жертвы вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий",
                    ExnternalUid = "57,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 5
                }, new BenefitType
                {
                    Id = 28,
                    Name =
                        @"Дети из семей лиц, погибших или получивших увечья (ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей",
                    ExnternalUid = "58,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = false,
                    SameBenefitId = 6
                }, new BenefitType
                {
                    Id = 29,
                    Name =
                        @"Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств в семье, вызванных утратой имущества вследствие ограбления, пожара, затопления, разрушения или утраты жилища",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = false,
                    SameBenefitId = 47
                }, new BenefitType
                {
                    Id = 30,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 31,
                    Name =
                        @"Дети из семей, в которых оба или один из родителей являются инвалидами",
                    ExnternalUid = "56",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 9
                }, new BenefitType
                {
                    Id = 32,
                    Name = @"Дети, состоящие на учете в комиссиях по делам несовершеннолетних и защите их прав",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 14
                }, new BenefitType
                {
                    Id = 33,
                    Name =
                        @"Дети-инвалиды",
                    ExnternalUid = "24",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    NeedTypeOfRestriction = true,
                    IsActive = true,
                    SameBenefitId = 11
                },
                new BenefitType
                {
                    Id = 34,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestOrphanCamps,
                    IsActive = false
                }, new BenefitType
                {
                    Id = 35,
                    Name = @"Дети-инвалиды и иные дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.YouthRestOrphanCamps,
                    IsActive = false,
                    SameBenefitId = null
                },
                new BenefitType
                {
                    Id = 36,
                    Name =
                        @"Дети, оказавшиеся в экстремальных условиях",
                    ExnternalUid = "",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 37,
                    Name = @"Дети-жертвы насилия",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = (long) BenefitTypeEnum.Orphans,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsOrphan,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 39,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    IsActive = true,
                    SameBenefitId = (long) BenefitTypeEnum.Orphans
                }, new BenefitType
                {
                    Id = 40,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestOrphanCamps,
                    IsActive = true,
                    SameBenefitId = (long) BenefitTypeEnum.Orphans
                }, new BenefitType
                {
                    Id = 41,
                    Name = @"Дети-инвалиды, дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 42,
                    Name = @"Дети-инвалиды, дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsInvalid,
                    IsActive = true,
                    SameBenefitId = 41
                }, new BenefitType
                {
                    Id = 43,
                    Name = @"Дети-инвалиды, дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    IsActive = true,
                    SameBenefitId = 41
                }, new BenefitType
                {
                    Id = 44,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 45,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsPoor,
                    IsActive = true,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 46,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex,
                    IsActive = true,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 47,
                    Name =
                        @"Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств, и которые не могут преодолеть данные обстоятельства самостоятельно или с помощью семьи",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 48,
                    Name =
                        @"Лица, из числа детей-сирот и детей, оставшихся без попечения родителей, в возрасте от 18 до 23 лет (включительно), обучающиеся по образовательным программам среднего профессионального образования или образовательным программам высшего образования по очной форме обучения",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.YouthRestOrphanCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 49,
                    Name = @"Дети, из семей, в которых оба или один родитель являются инвалидами",
                    ExnternalUid = "56",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 50,
                    Name = @"Дети с отклонениями в поведении",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 51,
                    Name =
                        @"Дети из семей военнослужащих и приравненных к ним лиц, погибших или получивших увечья (ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей.",
                    ExnternalUid = "58,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 6
                }, new BenefitType
                {
                    Id = 52,
                    Name =
                        @"Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств, и которые не могут преодолеть данные обстоятельства самостоятельно или с помощью семьи",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 47
                }, new BenefitType
                {
                    Id = 53,
                    Name = @"Дети-жертвы насилия",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 37
                },
                new BenefitType
                {
                    Id = 54,
                    Name =
                        @"Дети, оказавшиеся в экстремальных условиях",
                    ExnternalUid = "",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = 36
                },
                new BenefitType
                {
                    Id = 55,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestFederalCamps,
                    IsActive = true,
                    SameBenefitId = (long) BenefitTypeEnum.Orphans,
                    Eid = 55
                }, new BenefitType
                {
                    Id = 56,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn3To7,
                    IsActive = true,
                    SameBenefitId = 44,
                    Eid = 56
                }, new BenefitType
                {
                    Id = 57,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 44,
                    Eid = 57
                }, new BenefitType
                {
                    Id = 58,
                    Name =
                        @"Дети из семей беженцев и вынужденных переселенцев",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 4,
                    Eid = 58
                }, new BenefitType
                {
                    Id = 59,
                    Name = @"Дети, из семей, в которых оба или один родитель являются инвалидами",
                    ExnternalUid = "56",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 49,
                    Eid = 59
                }, new BenefitType
                {
                    Id = 60,
                    Name = @"Дети с отклонениями в поведении",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 50,
                    Eid = 60
                }, new BenefitType
                {
                    Id = 61,
                    Name = @"Дети, пострадавшие в результате террористических актов",
                    ExnternalUid = "57",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 3,
                    Eid = 61
                }, new BenefitType
                {
                    Id = 62,
                    Name = @"Дети-жертвы насилия",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 37,
                    Eid = 62
                },
                new BenefitType
                {
                    Id = 63,
                    Name =
                        @"Дети, оказавшиеся в экстремальных условиях",
                    ExnternalUid = "",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 36,
                    Eid = 63
                }, new BenefitType
                {
                    Id = 64,
                    Name =
                        @"Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств, и которые не могут преодолеть данные обстоятельства самостоятельно или с помощью семьи",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 47,
                    Eid = 64
                }, new BenefitType
                {
                    Id = 65,
                    Name =
                        @"Дети из семей военнослужащих и приравненных к ним лиц, погибших или получивших увечья (ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей.",
                    ExnternalUid = "58,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 6,
                    Eid = 65
                }, new BenefitType
                {
                    Id = 66,
                    Name =
                        @"Дети-жертвы вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий",
                    ExnternalUid = "57,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 5,
                    Eid = 66
                }, new BenefitType
                {
                    Id = 67,
                    Name =
                        @"Лица, из числа детей-сирот и детей, оставшихся без попечения родителей, в возрасте от 18 до 23 лет (включительно), обучающиеся по образовательным программам среднего профессионального образования или образовательным программам высшего образования по очной форме обучения",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.CompensationYouthRest,
                    IsActive = true
                }, new BenefitType
                {
                    Id = 68,
                    Name = @"Дети, пострадавшие в результате террористических актов",
                    ExnternalUid = "57",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 3
                }, new BenefitType
                {
                    Id = 69,
                    Name =
                        @"Дети из семей беженцев и вынужденных переселенцев",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 4
                }, new BenefitType
                {
                    Id = 70,
                    Name =
                        @"Дети-жертвы вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий",
                    ExnternalUid = "57,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 5
                }, new BenefitType
                {
                    Id = 71,
                    Name =
                        @"Дети из семей военнослужащих и приравненных к ним лиц, погибших или получивших увечья (ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей.",
                    ExnternalUid = "58,71,72",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 6
                },
                new BenefitType
                {
                    Id = 72,
                    Name =
                        @"Дети, оказавшиеся в экстремальных условиях",
                    ExnternalUid = "",
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 36
                }, new BenefitType
                {
                    Id = 73,
                    Name = @"Дети-жертвы насилия",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 37
                }, new BenefitType
                {
                    Id = 74,
                    Name =
                        @"Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье",
                    ExnternalUid = "52,69",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCampOrphan,
                    IsActive = true,
                    SameBenefitId = (long) BenefitTypeEnum.Orphans
                }, new BenefitType
                {
                    Id = 75,
                    Name = @"Дети-инвалиды, дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 41
                }, new BenefitType
                {
                    Id = 76,
                    Name = @"Дети из малообеспеченных семей",
                    ExnternalUid = "48",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 44
                }, new BenefitType
                {
                    Id = 77,
                    Name =
                        @"Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств, и которые не могут преодолеть данные обстоятельства самостоятельно или с помощью семьи",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 47
                }, new BenefitType
                {
                    Id = 78,
                    Name = @"Дети, из семей, в которых оба или один родитель являются инвалидами",
                    ExnternalUid = "56",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 49
                }, new BenefitType
                {
                    Id = 79,
                    Name = @"Дети с отклонениями в поведении",
                    ExnternalUid = "",
                    NeedTypeOfRestriction = false,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.TentChildrenCamp,
                    IsActive = true,
                    SameBenefitId = 50
                }, new BenefitType
                {
                    Id = 80,
                    Name = @"Дети-инвалиды, дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOnInvalidOn4To17,
                    IsActive = true,
                    SameBenefitId = 41
                }, new BenefitType
                {
                    Id = 81,
                    Name = @"Дети-инвалиды, дети с ограниченными возможностями здоровья",
                    ExnternalUid = "24",
                    NeedTypeOfRestriction = true,
                    NeedApproveDocument = false,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15,
                    IsActive = true,
                    SameBenefitId = 41
                }
            );

            context.SaveChanges();

            SetEidAndLastUpdateTicks(context.BenefitType.ToList());
            context.SaveChanges();
        }
    }
}
