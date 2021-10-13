using System;
using RestChild.Comon.Extensions;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Отделы МГТ
        /// </summary>
        private static void MgtDepartment(Context context)
        {
            context.MGTDepartment.AddIfNotExists(new MGTDepartment
            {
                Id = 1,
                Name = "Все отделы",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 1,
                IsDeleted = false
            }, r => r.Id == 1);
            context.MGTDepartment.AddIfNotExists(new MGTDepartment
            {
                Id = 2,
                Name = "Отдел по приему документов",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 2,
                IsDeleted = false
            }, r => r.Id == 2); ;

            context.MGTDepartment.AddIfNotExists(new MGTDepartment
            {
                Id = 3,
                Name = "Отдел по работе с клиентами",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 3,
                IsDeleted = false
            }, r => r.Id == 3);

            context.MGTDepartment.AddIfNotExists(new MGTDepartment
            {
                Id = 4,
                Name = "Отдел бронирования",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,                
                Eid = 4,
                IsDeleted = false
            }, r => r.Id == 4);
           

            context.SaveChanges();
        }
    }
}
