using RestChild.Admin.Web.Properties;
using RestChild.Comon;
using RestChild.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Admin.Web.Models
{
   public class SecurityJournalEntranceFilterModel : SecurityJournalFilterBase<Domain.AccountHistoryLogin>
   {
      public ushort? LoginStatus { get; set; }
   }
}
