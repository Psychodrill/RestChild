using System;
using RestChild.Comon.Extensions;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Цели визита в МГТ
        /// </summary>
        private static void MgtVisitTargets(Context context)
        {
            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 1,
                Name = "Подать заявление на выплату компенсации",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 1
            }, r => r.Id == 1);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 2,
                Name = "Получить консультацию по вопросу получения услуг отдыха и оздоровления",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 2
            }, r => r.Id == 2);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 3,
                Name = "Предоставить запрашиваемые документы",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 3
            }, r => r.Id == 3);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 4,
                Name = "Подать заявление на бумажном носителе",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsActive = true,
                Eid = 4
            }, r => r.Id == 4);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 5,
                Name = "Подать заявление на предоставление услуг отдыха и оздоровления",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 5
            }, r => r.Id == 5);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 6,
                Name = "Выбрать организацию отдыха и оздоровления",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 6
            }, r => r.Id == 6);

            context.SaveChanges();
        }
    }
}
