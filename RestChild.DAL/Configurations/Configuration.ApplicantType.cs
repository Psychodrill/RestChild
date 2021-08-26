using System.Data.Entity.Migrations;
using System.Linq;
using System;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды представительства
        /// </summary>
        private static void ApplicantType(Context context)
        {
            var maxId = context.ApplicantType.Where(s => s.Id <= 5).Select(s => s.Id).DefaultIfEmpty().Max();

            context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (ApplicantType, RESEED, {maxId})");

            context.ApplicantType.AddOrUpdate(s => s.Id,
                new ApplicantType
                {
                    Id = 1,
                    Name = "Родитель",
                    IsDeleted = true,
                    LastUpdateTick = DateTime.Now.Ticks,
                    Eid = 1
                },
                new ApplicantType
                {
                    Id = 2,
                    Name = "Законный представитель",
                    LastUpdateTick = DateTime.Now.Ticks,
                    Eid = 2
                },
                new ApplicantType
                {
                    Id = 3,
                    Name = "Доверенное лицо",
                    LastUpdateTick = DateTime.Now.Ticks,
                    Eid = 3
                },
                new ApplicantType
                {
                    Id = 4,
                    Name = "Отец",
                    LastUpdateTick = DateTime.Now.Ticks,
                    Eid = 4
                },
                new ApplicantType
                {
                    Id = 5,
                    Name = "Мать",
                    LastUpdateTick = DateTime.Now.Ticks,
                    Eid = 5
                }
            );
            context.SaveChanges();

            context.Database.ExecuteSqlCommand("Update [dbo].[Applicant] Set [ApplicantTypeId] = (Select Id From [dbo].[ApplicantType] at Where at.[Eid] = [dbo].[Applicant].[ApplicantTypeId] And at.[Id] <= 5) Where [ApplicantTypeId] > 5");
            context.Database.ExecuteSqlCommand("Delete From [dbo].[ApplicantType] Where Id > 5");
        }
    }
}
