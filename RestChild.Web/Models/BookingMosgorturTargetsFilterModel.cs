using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models
{
   public class BookingMosgorturTargetsFilterModel
   {
      public int PageNumber { get; set; }

      public BookingMosgorturTargetsFilterModel()
      {
         PageNumber = 1;
      }

      public string Name { get; set; }

      public CommonPagedList<MGTVisitTarget> Targets { get; set; }
   }
}
