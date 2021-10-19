using System.Collections.Generic;
using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.Web.Models.VisitQueue;
using RestChild.Web.Services.Contract;

namespace RestChild.Web.Services.Implementation
{
    public class VisitBookingService : IVisitBookingService
    {
        /// <summary>
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public BookingResult Prebooking(Models.VisitQueue.Booking booking)
        {
            var lg = WindsorHolder.Resolve<ILogger>();
            using (var uw = WindsorHolder.Resolve<IUnitOfWork>())
            {
                var br = new BookingRepository(uw, lg);
                return br.Prebooking(booking, true);
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="SNILS"></param>
        /// <returns></returns>
        public bool PrebookingSNILSCheck(string SNILS)//, long DepartId)
        {
            var lg = WindsorHolder.Resolve<ILogger>();
            using (var uw = WindsorHolder.Resolve<IUnitOfWork>())
            {
                var br = new BookingRepository(uw, lg);
                return br.PrebookingSnilsCheck(SNILS,2); //, DepartId);  чудовищная затычка
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public BookingResult PrebookingCancellation(long BookingId)
        {
            var lg = WindsorHolder.Resolve<ILogger>();
            using (var uw = WindsorHolder.Resolve<IUnitOfWork>())
            {
                var br = new BookingRepository(uw, lg);
                return br.PrebookingCancellation(BookingId);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<BookingVisitGrid> GetVisitGrid(BookingVisitGridFilter filter)
        {
            var lg = WindsorHolder.Resolve<ILogger>();
            using (var uw = WindsorHolder.Resolve<IUnitOfWork>())
            {
                var br = new BookingRepository(uw, lg);
                return br.GetVisitGrid(filter);
            }
        }
    }
}
