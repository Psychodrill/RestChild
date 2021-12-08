using System;
using RestChild.Comon.Extensions;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы строк таблиц аналитики
        /// </summary>
        private static void AnalyticsViewRowType(Context context)
        {
            context.AnalyticsViewRowType.AddIfNotExists(new AnalyticsViewRowType
            {
                Id = 1,
                Name = "Четыре столбца за час, за сутки, за неделю, всего",
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 1,
            }, r => r.Id == 1);
            context.AnalyticsViewRowType.AddIfNotExists(new AnalyticsViewRowType
            {
                Id = 2,
                Name = "Четыре пары столбцов  за час, за сутки, за неделю, всего",
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 2,
            }, r => r.Id == 2); ;

            context.AnalyticsViewRowType.AddIfNotExists(new AnalyticsViewRowType
            {
                Id = 3,
                Name = "Один столбец",
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 3,
            }, r => r.Id == 3);

            context.AnalyticsViewRowType.AddIfNotExists(new AnalyticsViewRowType
            {
                Id = 4,
                Name = "Один столбец две цифры",
                LastUpdateTick = DateTime.Now.Ticks,                
                Eid = 4,
            }, r => r.Id == 4);

            context.AnalyticsViewRowType.AddIfNotExists(new AnalyticsViewRowType
            {
                Id = 5,
                Name = "По дням",
                LastUpdateTick = DateTime.Now.Ticks,
                Eid = 5,
            }, r => r.Id == 5);

            context.SaveChanges();
        }
    }
}
