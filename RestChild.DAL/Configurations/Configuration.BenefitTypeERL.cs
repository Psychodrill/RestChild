using System.Collections.Generic;
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
        ///     Заполнение наименований ЛК в ИС Социум
        /// </summary>
        private static void BenefitTypeERL(Context context)
        {
            //Бесплатная путевка для отдыха и оздоровления (индивидуальный выездной отдых)
            context.BenefitTypeERL.AddOrUpdate(r => r.Id, new BenefitTypeERL
                {
                    Id = 1,
                    Name = "Ребёнок-сирота или ребёнок, оставшийся без попечения родителей",
                    LCCode = "903001",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 2,
                    Name = "Ребёнок-инвалид",
                    LCCode = "1015",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 3,
                    Name = "Ребёнок с ограниченными возможностями здоровья",
                    LCCode = "903002",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 4,
                    Name = "Ребёнок из малообеспеченной семьи",
                    LCCode = "903003",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 5,
                    Name = "Ребёнок из малообеспеченной семьи",
                    LCCode = "903003",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 6,
                    Name =
                        "Ребёнок-жертва вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий",
                    LCCode = "903004",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 7,
                    Name = "Ребёнок из семей беженцев и вынужденных переселенцев",
                    LCCode = "903005",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 8,
                    Name = "Ребёнок, оказавшийся в экстремальных условиях",
                    LCCode = "903006",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 9,
                    Name = "Ребёнок-жертва насилия",
                    LCCode = "903007",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 10,
                    Name =
                        "Ребёнок, жизнедеятельность которого объективно нарушена в результате сложившихся обстоятельств, и который не может преодолеть данные обстоятельства самостоятельно или с помощью семьи",
                    LCCode = "903008",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 11,
                    Name = "Ребёнок, пострадавший в результате террористических актов",
                    LCCode = "903009",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 12,
                    Name =
                        "Ребёнок военнослужащего или сотрудника некоторых федеральных органов исполнительной власти, погибших (умерших), пропавших без вести при исполнении обязанностей военной службы (служебных обязанностей)",
                    LCCode = "903010",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 13,
                    Name = "Ребёнок из семьи, в которой оба или единственный родитель являются инвалидами",
                    LCCode = "903011",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 14,
                    Name = "Ребёнок с отклонениями в поведении",
                    LCCode = "903012",
                    IsActive = true,
                },
                new BenefitTypeERL
                {
                    Id = 15,
                    Name = "Лицо из числа детей-сирот и детей, оставшихся без попечения родителей",
                    LCCode = "903013",
                    IsActive = true,
                });

            context.SaveChanges();
            foreach (var bft in context.BenefitTypeERL.ToList())
            {
                if (bft.TypesOfRestERL == null)
                {
                    bft.TypesOfRestERL = new List<TypeOfRestERL>();
                }
                else
                {
                    bft.TypesOfRestERL.Clear();
                }

                if (bft.BenefitTypes == null)
                {
                    bft.BenefitTypes = new List<BenefitType>();
                }
                else
                {
                    bft.BenefitTypes.Clear();
                }

                if (bft.Id == 1)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestGroup)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 40
                            || ss.Id == 38
                            || ss.Id == 39
                            || ss.Id == 18)
                        .ToList());
                }
                else if (bft.Id == 2)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestGroup
                            || ss.Id == (long) TypeOfRestERLEnum.Compensation
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChildAndApplicant)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 41
                            || ss.Id == 42
                            || ss.Id == 43)
                        .ToList());
                }
                else if (bft.Id == 3)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestGroup
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChildAndApplicant)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 41)
                        .ToList());
                }
                else if (bft.Id == 4)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestGroup
                            || ss.Id == (long) TypeOfRestERLEnum.Compensation
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChildAndApplicant)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 44
                            || ss.Id == 45
                            || ss.Id == 46
                            || ss.Id == 17)
                        .ToList());
                }
                else if (bft.Id == 5)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestGroup
                            || ss.Id == (long) TypeOfRestERLEnum.Compensation
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 44
                            || ss.Id == 45
                            || ss.Id == 46
                            || ss.Id == 17
                            || ss.Id == 56
                            || ss.Id == 57)
                        .ToList());
                }
                else if (bft.Id == 6)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 5
                            || ss.Id == 66)
                        .ToList());
                }
                else if (bft.Id == 7)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 4
                            || ss.Id == 58)
                        .ToList());
                }
                else if (bft.Id == 8)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 36
                            || ss.Id == 63)
                        .ToList());
                }
                else if (bft.Id == 9)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 37
                            || ss.Id == 62)
                        .ToList());
                }
                else if (bft.Id == 10)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 47
                            || ss.Id == 64)
                        .ToList());
                }
                else if (bft.Id == 11)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 3
                            || ss.Id == 61)
                        .ToList());
                }
                else if (bft.Id == 12)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 6
                            || ss.Id == 65)
                        .ToList());
                }
                else if (bft.Id == 13)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 49
                            || ss.Id == 59)
                        .ToList());
                }
                else if (bft.Id == 14)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestIndividual
                            || ss.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 50
                            || ss.Id == 60)
                        .ToList());
                }
                else if (bft.Id == 15)
                {
                    bft.TypesOfRestERL.AddRange(context.TypeOfRestERL.Where(ss =>
                            ss.Id == (long) TypeOfRestERLEnum.FreeRestCertYouth
                            || ss.Id == (long) TypeOfRestERLEnum.CompensationYouthRest)
                        .ToList());
                    bft.BenefitTypes.AddRange(context.BenefitType.Where(ss =>
                            ss.Id == 48
                            || ss.Id == 67)
                        .ToList());
                }
            }
        }
    }
}
