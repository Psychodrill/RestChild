using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
   public class ProgressStatus
   {
      public long Step { get; set; }

      public long Steps { get; set; }

      public string Message { get; set; }

      public float Percent => Steps == 0 ? 0 : Step * 100 / Steps;

      public bool IsError { get; set; }

      public bool IsComplete
      {
         get
         {
            return Percent >= 100;
         }
      }

      public DateTime LastUpdate { get; set; }
   }
}
