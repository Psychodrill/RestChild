using RestChild.Comon.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
   public partial class MGTBookingVisit
   {
      public bool IsCanceled => StatusId == (long)MGTVisitBookingStatuses.PrebookingCanceled || StatusId == (long)MGTVisitBookingStatuses.BookingCanceled || StatusId == (long)MGTVisitBookingStatuses.BookingMGTCanceled;

      public bool IsBooked => !IsCanceled && (StatusId == (long)MGTVisitBookingStatuses.BookingVisited || StatusId == (long)MGTVisitBookingStatuses.BookingUnvisited);
   }
}
