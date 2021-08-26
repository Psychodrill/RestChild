using System;

namespace RestChild.Web.Models.VisitQueue
{
   public class BookingVisitGridFilter
   {
      public long VisitTargetId { get; set; }

      public DateTime DateFrom { get; set; }

      public DateTime? DateTo { get; set; }

      public int VisitSlotsCount { get; set; }
   }
}
