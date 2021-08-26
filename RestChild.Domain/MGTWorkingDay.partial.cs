using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
   public partial class MGTWorkingDay
   {
      [IgnoreDataMember]
      [NotMapped]
      public int WindowCount { get; set; }

      [IgnoreDataMember]
      [NotMapped]
      public int BookingCount { get; set; }

      [IgnoreDataMember]
      [NotMapped]
      public int BookingMaximum { get; set; }

      /// <summary>
      /// Начало работы
      /// </summary>
      [Display(Description = "Начало работы")]
      [IgnoreDataMember]
      [NotMapped]
      public DateTime BeginTime { get; set; }

      /// <summary>
      /// Окончание работы
      /// </summary>
      [Display(Description = "Окончание работы")]
      [IgnoreDataMember]
      [NotMapped]
      public DateTime EndTime { get; set; }

   }
}
