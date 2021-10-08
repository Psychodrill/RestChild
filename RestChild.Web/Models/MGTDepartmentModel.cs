using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
   public class MGTDepartmentModel
   {

      public long Id { get; set; }

      [Required(ErrorMessage = "Необходимо задать название отдела")]
      public string Name { get; set; }
      public string Description { get; set; }
   }
}
