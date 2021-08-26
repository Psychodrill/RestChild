using System;
using System.Collections.Generic;

namespace RestChild.Web.Models.VisitQueue
{
   /// <summary>
   /// 
   /// </summary>
   public class BookingVisitGrid
   {
      /// <summary>
      /// 
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public IList<DateTime> Cells { get; set; }
   }
}
