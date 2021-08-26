using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
   public class MGTWorkingDayWindowModel
   {
      public struct BookingTargets
      {
         public long Id { get; set; }

         public string Name { get; set; }

         public bool IsSet { get; set; }
      }

      public long Id { get; set; }

      [Required(ErrorMessage = "Необходимо задать временные интервалы")]
      public List<MGTWorkingDayWindowTimeIntervalModel> TimeIntervals { get; set; }

      [Required(ErrorMessage = "Необходимо задать цели обращения")]
      public List<long> SelectedTargets { get; set; }

      public bool IsDeleted { get; set; }

      public MGTWorkingDayWindowModel()
      {
         TimeIntervals = new List<MGTWorkingDayWindowTimeIntervalModel>() { new MGTWorkingDayWindowTimeIntervalModel { TimeFromString = "08:00", TimeToString = "20:00", IsDeleted = false } };
      }

      public int? WindowNumber { get; set; }
   }
}
