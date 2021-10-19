using RestChild.Web.Models.VisitQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestChild.Web.Services.Contract
{
   [ServiceContract]
   public interface IVisitBookingService
    {
        [OperationContract]
        IEnumerable<BookingVisitGrid> GetVisitGrid(BookingVisitGridFilter filter);

        [OperationContract]
        BookingResult Prebooking(Models.VisitQueue.Booking booking);

        [OperationContract]
        BookingResult PrebookingCancellation(long BookingId);

        [OperationContract]
        bool PrebookingSNILSCheck(string SNILS);//, long DepartId);
    }
}
