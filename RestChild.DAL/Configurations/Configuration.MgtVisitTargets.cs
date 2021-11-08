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
                Eid = 1,
                DepartmentId = 2,
            }, r => r.Id == 1);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 2,
                Name = "Получить консультацию по вопросу получения услуг отдыха и оздоровления",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 2,
                DepartmentId = 2,
            }, r => r.Id == 2);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 3,
                Name = "Предоставить запрашиваемые документы",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 3,
                DepartmentId = 2,
            }, r => r.Id == 3);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 4,
                Name = "Подать заявление на бумажном носителе",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsActive = true,
                Eid = 4,
                DepartmentId = 2,
            }, r => r.Id == 4);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 5,
                Name = "Подать заявление на предоставление услуг отдыха и оздоровления",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 5,
                DepartmentId = 2,
            }, r => r.Id == 5);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 6,
                Name = "Выбрать организацию отдыха и оздоровления",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 6,
                DepartmentId = 2,
            }, r => r.Id == 6);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 7,
                Name = "Консультация у работника офиса",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = false,
                IsActive = true,
                Eid = 7,
                DepartmentId = 1,
            }, r => r.Id == 7);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 8,
                Name = "Приобрести дополнительное место к бесплатной путевке для отдыха и оздоровления / получить консультацию по указанному вопросу",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 8,
                DepartmentId = 3,
            }, r => r.Id == 8);

            context.MGTVisitTarget.AddIfNotExists(new MGTVisitTarget
            {
                Id = 9,
                Name = "Приобрести семейный тур или путевку в детский лагерь, в том числе с использованием сертификата на отдых и оздоровление / получить консультацию по указанному вопросу",
                Description = string.Empty,
                LastUpdateTick = DateTime.Now.Ticks,
                IsForMPGU = true,
                IsActive = true,
                Eid = 9,
                DepartmentId = 4,
            }, r => r.Id == 9);

            context.SaveChanges();
        }
    }
}
