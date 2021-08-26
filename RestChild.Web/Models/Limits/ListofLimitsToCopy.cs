using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Limits
{
   public struct ListofLimitsToCopy //: ViewModelBase<ListOfChilds>
   {
      public long ListId { get; set; }

      public string PlaceOfRest { get; set; }

      public string PlaceOfRestAdress { get; set; }

      public DateTime? DateIncome { get; set; }

      public DateTime? DateOutcome { get; set; }

      public int LimitVolume { get; set; }

      public string LimitType { get; set; }

      public int Childreninlist { get; set; }
   }
}
