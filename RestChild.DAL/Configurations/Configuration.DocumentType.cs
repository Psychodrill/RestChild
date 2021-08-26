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
        ///     Типы документов удостоверяющих личность
        /// </summary>
        private static void DocumentType(Context context)
        {
            context.DocumentType.AddOrUpdate(s => s.Id, new DocumentType
            {
                Id = 20001,
                Name = "Паспорт РФ",
                ForChild = false,
                GlobalUid = "20001",
                ForOther = false,
                BaseRegistryUid = "1",
                ForApplicant = true
            }, new DocumentType
            {
                Id = 20007,
                Name = "Вид на жительство",
                ForChild = false,
                GlobalUid = "20007",
                ForOther = false,
                BaseRegistryUid = "6",
                ForApplicant = true
            }, new DocumentType
            {
                Id = 20010,
                Name = "Временное удостоверение личности гражданина РФ",
                ForChild = true,
                GlobalUid = "20010",
                ForOther = false,
                BaseRegistryUid = "9",
                ForApplicant = true
            }, new DocumentType
            {
                Id = 30001,
                Name = "Паспорт РФ",
                ForAgent = true,
                GlobalUid = "30001",
                ForOther = false,
                BaseRegistryUid = "1",
                ForApplicant = false
            }, new DocumentType
            {
                Id = 30007,
                Name = "Вид на жительство",
                ForChild = false,
                ForAgent = true,
                GlobalUid = "30007",
                ForOther = true,
                BaseRegistryUid = "6",
                ForApplicant = false
            }, new DocumentType
            {
                Id = 30010,
                Name = "Временное удостоверение личности гражданина РФ",
                ForChild = false,
                ForAgent = true,
                GlobalUid = "30010",
                ForOther = true,
                BaseRegistryUid = "9",
                ForApplicant = false
            }, new DocumentType
            {
                Id = 50001,
                Name = "Паспорт РФ",
                ForChild = true,
                GlobalUid = "50001",
                ForOther = false,
                BaseRegistryUid = "1",
                ForApplicant = false
            }, new DocumentType
            {
                Id = 60001,
                Name = "Паспорт РФ",
                ForChild = false,
                GlobalUid = "60001",
                ForOther = true,
                BaseRegistryUid = "1",
                ForApplicant = false
            }, new DocumentType
            {
                Id = 22,
                Name = "Свидетельство о рождении",
                ForChild = true,
                GlobalUid = "22",
                BaseRegistryUid = "10",
                ForOther = false
            }, new DocumentType
            {
                Id = 23,
                Name = "Свидетельство о рождении иностранного образца",
                ForChild = true,
                GlobalUid = "23",
                BaseRegistryUid = "11",
                ForOther = false
            }, new DocumentType
            {
                Id = 50002,
                Name = "Заграничный паспорт",
                ForChild = true,
                GlobalUid = "50002",
                ForForeign = true
            }, new DocumentType
            {
                Id = 50003,
                Name = "Заграничный паспорт",
                ForOther = true,
                GlobalUid = "50003",
                ForForeign = true
            }, new DocumentType
            {
                Id = 50004,
                Name = "Заграничный паспорт",
                ForApplicant = true,
                GlobalUid = "50004",
                ForForeign = true
            }, new DocumentType
            {
                Id = 20005,
                Name = "Паспорт иностранного образца",
                ForApplicant = true,
                GlobalUid = "",
                BaseRegistryUid = "5",
                ForForeign = false,
                ForAgent = false,
                ForChild = false,
                ForOther = false
            }, new DocumentType
            {
                Id = 30005,
                Name = "Паспорт иностранного образца",
                ForApplicant = false,
                GlobalUid = "",
                BaseRegistryUid = "5",
                ForForeign = false,
                ForAgent = true,
                ForChild = false,
                ForOther = false
            }, new DocumentType
            {
                Id = 50005,
                Name = "Паспорт иностранного образца",
                ForApplicant = false,
                GlobalUid = "",
                BaseRegistryUid = "5",
                ForForeign = false,
                ForAgent = false,
                ForChild = true,
                ForOther = false
            }, new DocumentType
            {
                Id = 20008,
                Name = "Удостоверение беженца",
                ForApplicant = true,
                GlobalUid = "20008",
                BaseRegistryUid = "7",
                ForForeign = false,
                ForAgent = true,
                ForChild = true,
                ForOther = true
            }, new DocumentType
            {
                Id = 20007,
                Name = "Вид на жительство",
                ForApplicant = true,
                GlobalUid = "20007",
                BaseRegistryUid = "6",
                ForForeign = false,
                ForAgent = false,
                ForChild = true,
                ForOther = false
            }, new DocumentType
            {
                Id = 20009,
                Name = "Временное удостоверение личности гражданина РФ",
                ForApplicant = false,
                GlobalUid = "20009",
                BaseRegistryUid = "9",
                ForForeign = false,
                ForAgent = false,
                ForChild = false,
                ForOther = false
            }, new DocumentType
            {
                Id = 20011,
                Name = "Удостоверение беженца",
                ForApplicant = true,
                GlobalUid = "20011",
                BaseRegistryUid = "7",
                ForForeign = true,
                ForAgent = false,
                ForChild = true,
                ForOther = true
            });
            context.SaveChanges();

            var cets = context.DocumentType.ToList();
            foreach (var cet in cets)
            {
                cet.Eid = cet.Id;
                if (cet.Id == 20009)
                {
                    if (cet.TypesOfRest == null)
                    {
                        cet.TypesOfRest = new List<TypeOfRest>();
                    }
                    else
                    {
                        cet.TypesOfRest.Clear();
                    }

                    cet.TypesOfRest.AddRange(context.TypeOfRest.Where(ss =>
                        ss.Id != (long) TypeOfRestEnum.RestWithParentsPoor &&
                        ss.Id != (long) TypeOfRestEnum.MoneyOn3To7).ToList());
                    context.SaveChanges();
                }
            }

            context.SaveChanges();
        }
    }
}
