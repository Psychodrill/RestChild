using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Заполнение каникулярных периодов
        /// </summary>
        private static void PupilGroupVacationPeriod(Context context)
        {
            context.PupilGroupVacationPeriod.AddOrUpdate(r => r.Id, new PupilGroupVacationPeriod
                {
                    Id = (long)OrphanagePupilGroupVacationPeriodEnum.SpringVacation,
                    Name = "Весенние каникулы",
                    IsDeleted = false,
                    LastUpdateTick = DateTime.Now.Ticks
                },
                new PupilGroupVacationPeriod
                {
                    Id = (long)OrphanagePupilGroupVacationPeriodEnum.SummerVacation,
                    Name = "Летние каникулы",
                    IsDeleted = false,
                    LastUpdateTick = DateTime.Now.Ticks
                },
                new PupilGroupVacationPeriod
                {
                    Id = (long)OrphanagePupilGroupVacationPeriodEnum.AutumnVacation,
                    Name = "Осенние каникулы",
                    IsDeleted = false,
                    LastUpdateTick = DateTime.Now.Ticks
                },
                new PupilGroupVacationPeriod
                {
                    Id = (long)OrphanagePupilGroupVacationPeriodEnum.WinterVacation,
                    Name = "Зимние каникулы",
                    IsDeleted = false,
                    LastUpdateTick = DateTime.Now.Ticks
                });
        }

    }
}
