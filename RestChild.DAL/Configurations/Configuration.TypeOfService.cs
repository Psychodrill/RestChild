using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды услуги
        /// </summary>
        private static void TypeOfService(Context context)
        {
            context.TypeOfService.AddOrUpdate(t => t.Id,
                new TypeOfService
                {
                    Id = (long) ServiceEnum.TransportTo,
                    Name = "Транспорт в место отдыха",
                    IsActive = false
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.TransportFrom,
                    Name = "Транспорт из места отдыха",
                    IsActive = false
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.Excursion,
                    Name = "Экскурсия",
                    IsActive = true,
                    NeedDescription = true,
                    NeedDurationHour = true,
                    NeedSize = true,
                    NeedAnnouncement = true,
                    NeedConditions = true,
                    NeedName = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.Visa,
                    Name = "Виза",
                    IsActive = true,
                    NeedName = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.TransferAero,
                    Name = "Транспорт (Авиа)",
                    IsActive = true,
                    NeedName = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true,
                    NeedTransport = true,
                    NeedDescription = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.TransferAuto,
                    Name = "Транспорт (Авто)",
                    IsActive = true,
                    NeedName = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true,
                    NeedTransport = true,
                    NeedDescription = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.TransferTrain,
                    Name = "Транспорт (ЖД)",
                    IsActive = true,
                    NeedName = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true,
                    NeedTransport = true,
                    NeedDescription = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.SpecializedPlaceChild,
                    Name = "Профильные лагеря дети",
                    IsActive = false,
                    NeedName = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.SpecializedTransportChild,
                    Name = "Профильные лагеря дети - транспорт",
                    IsActive = false
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.SpecializedPlaceAttendant,
                    Name = "Профильные лагеря педагоги/тренера",
                    IsActive = false
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.SpecializedTransportAttendant,
                    Name = "Профильные лагеря педагоги/тренера - транспорт",
                    IsActive = false
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.Insurance,
                    Name = "Страховка",
                    IsActive = true,
                    NeedName = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.AddonPlace,
                    Name = "Дополнительное место",
                    IsActive = true,
                    NeedName = true,
                    MayMustApprove = true,
                    NeedDescription = true
                },
                new TypeOfService
                {
                    Id = (long) ServiceEnum.Other,
                    Name = "Прочее",
                    IsActive = true,
                    NeedName = true,
                    NeedAnnouncement = true,
                    NeedConditions = true,
                    NeedDescription = true,
                    MayByDefault = true,
                    MayMustApprove = true,
                    MayRequared = true,
                    MayWithAccomodation = true
                });

            SetEidAndLastUpdateTicks(context.TypeOfService.ToList());
            context.SaveChanges();
        }
    }
}
