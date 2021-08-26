using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.Web.Models.VisitQueue;
using RestChild.Web.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Services.Implementation
{
    /// <summary>
    ///     Реализация сервиса работы с СУО
    /// </summary>
    public class VisitSUOService : IVisitSUOService
    {
        /// <summary>
        ///     Получить данные по ПИН коду
        /// </summary>
        public VisitSUOResult GetSUOVisitData(int PINCode)
        {
            var lg = WindsorHolder.Resolve<ILogger>();
            using (var uw = WindsorHolder.Resolve<IUnitOfWork>())
            {
                var br = new BookingRepository(uw, lg);
                return br.GetSuoVisitData(PINCode);
            }

        }
    }
}
