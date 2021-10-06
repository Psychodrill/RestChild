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
        ///     Заполнение наименований МСП в ИС Социум
        /// </summary>
        private static void TypeOfRestERL(Context context)
        {
            context.TypeOfRestERL.AddOrUpdate(r => r.Id, new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.FreeRestIndividual,
                    Name = "Бесплатная путевка для отдыха и оздоровления (индивидуальный выездной отдых)",
                    MSPCode = "1030001"
                },
                new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.FreeRestGroup,
                    Name = "Бесплатная путевка для отдыха и оздоровления (совместный выездной отдых)",
                    MSPCode = "1030002",
                },
                new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.FreeRestCertYouth,
                    Name = "Бесплатная путевка для отдыха и оздоровления (Молодежный отдых)",
                    MSPCode = "1030012",
                    UseApplicant = true,
                },
                new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.FreeRestChild,
                    Name = "Сертификат на отдых и оздоровление ребёнка",
                    MSPCode = "1030013",
                },
                new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.Compensation,
                    Name =
                        "Компенсация за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления",
                    MSPCode = "1030014",
                },
                new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.CompensationYouthRest,
                    Name =
                        "Компенсация за путевку лицу из числа детей-сирот и детей, оставшихся без попечения родителей",
                    MSPCode = "1030015",
                    UseApplicant = true,
                },
                new TypeOfRestERL
                {
                    Id = (long) TypeOfRestERLEnum.FreeRestChildAndApplicant,
                    Name =
                        "Сертификат на отдых и оздоровление ребенка и сопровождающего лица",
                    MSPCode = "1030016",
                    UseApplicant = false,
                });

            context.SaveChanges();

            foreach (var tor in context.TypeOfRestERL.ToList())
            {
                if (tor.TypesOfRest == null)
                {
                    tor.TypesOfRest = new List<TypeOfRest>();
                }
                else
                {
                    tor.TypesOfRest.Clear();
                }

                if (tor.Id == (long) TypeOfRestERLEnum.FreeRestIndividual)
                {
                    tor.TypesOfRest.AddRange(context.TypeOfRest.Where(s =>
                        s.Id == (long) TypeOfRestEnum.ChildRestCamps ||
                        s.Id == (long) TypeOfRestEnum.ChildRestOrphanCamps ||
                        s.Id == (long) TypeOfRestEnum.TentChildrenCamp ||
                        s.Id == (long) TypeOfRestEnum.TentChildrenCampOrphan
                    ).ToList());
                }
                else if (tor.Id == (long) TypeOfRestERLEnum.FreeRestGroup)
                {
                    tor.TypesOfRest.AddRange(context.TypeOfRest.Where(s =>
                        s.Id == (long) TypeOfRestEnum.RestWithParentsPoor
                        || s.Id == (long) TypeOfRestEnum.RestWithParentsInvalid
                        || s.Id == (long) TypeOfRestEnum.RestWithParentsOrphan
                        || s.Id == (long) TypeOfRestEnum.RestWithParentsComplex
                    ).ToList());
                }
                else if (tor.Id == (long) TypeOfRestERLEnum.FreeRestCertYouth)
                {
                    tor.TypesOfRest.AddRange(context.TypeOfRest.Where(s =>
                        s.Id == (long) TypeOfRestEnum.YouthRestOrphanCamps
                    ).ToList());
                }
                else if (tor.Id == (long) TypeOfRestERLEnum.FreeRestChild)
                {
                    tor.TypesOfRest.AddRange(context.TypeOfRest.Where(s =>
                        s.Id == (long) TypeOfRestEnum.MoneyOn3To7 ||
                        s.Id == (long) TypeOfRestEnum.MoneyOn7To15 ||
                        s.Id == (long) TypeOfRestEnum.MoneyOn18 ||
                        s.Id == (long) TypeOfRestEnum.MoneyOnInvalidOn4To17
                    ).ToList());
                }
                else if (tor.Id == (long) TypeOfRestERLEnum.Compensation)
                {
                    tor.TypesOfRest.AddRange(context.TypeOfRest.Where(s =>
                        s.Id == (long) TypeOfRestEnum.Compensation
                    ).ToList());
                }
                else if (tor.Id == (long) TypeOfRestERLEnum.CompensationYouthRest)
                {
                    tor.TypesOfRest.AddRange(context.TypeOfRest.Where(s =>
                        s.Id == (long) TypeOfRestEnum.CompensationYouthRest
                    ).ToList());
                }
            }
        }
    }
}
