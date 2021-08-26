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
        ///     Статусы заявления на визит в МПГУ
        /// </summary>
        private static void MgtVisitBookingVisitStatus(Context context)
        {
            context.MGTVisitBookingStatus.AddOrUpdate(s => s.Id, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.PrebookingRegistered,
                Name = "Пребронь зарегистрирована",
                LastUpdateTick = DateTime.Now.Ticks
            });
            context.MGTVisitBookingStatus.AddOrUpdate(s => s.Id, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.PrebookingCanceled,
                Name = "Пребронь аннулирована",
                ParrentId = (long) MGTVisitBookingStatuses.PrebookingRegistered,
                LastUpdateTick = DateTime.Now.Ticks
            }, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.BookingRegistered,
                Name = "Заявление зарегистрировано",
                ParrentId = (long) MGTVisitBookingStatuses.PrebookingRegistered,
                LastUpdateTick = DateTime.Now.Ticks
            });
            context.MGTVisitBookingStatus.AddOrUpdate(s => s.Id, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.BookingCanceled,
                Name = "Заявление отозвано заявителем",
                ParrentId = (long) MGTVisitBookingStatuses.BookingRegistered,
                LastUpdateTick = DateTime.Now.Ticks
            }, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.BookingMGTCanceled,
                Name = "Заявление отозвано по инициативе ГАУК Мосгортур",
                ParrentId = (long) MGTVisitBookingStatuses.BookingRegistered,
                LastUpdateTick = DateTime.Now.Ticks
            }, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.BookingVisited,
                Name = "Прием осуществлен",
                ParrentId = (long) MGTVisitBookingStatuses.BookingRegistered,
                LastUpdateTick = DateTime.Now.Ticks
            }, new MGTVisitBookingStatus
            {
                Id = (long) MGTVisitBookingStatuses.BookingUnvisited,
                Name = "Заявитель не явился на прием",
                ParrentId = (long) MGTVisitBookingStatuses.BookingRegistered,
                LastUpdateTick = DateTime.Now.Ticks
            });

            SetEidAndLastUpdateTicks(context.MGTVisitBookingStatus.ToList());
            context.SaveChanges();
        }
    }
}
