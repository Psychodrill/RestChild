using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.VisitQueue
{
   public class VisitStatus : IVisitStatus
   {
      public long Id { get; set; }

      public string Name { get; set; }
   }
}
