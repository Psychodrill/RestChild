using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Admin.Web.Models
{
   public class SecurityJournalActiveSessionsFilterModel : SecurityJournalFilterBase<Domain.AccountHistoryLogin>
   {
      public string UserInfo { get; set; }
   }
}
