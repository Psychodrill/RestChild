using RestChild.Comon;
using RestChild.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Admin.Web.Models
{
   public abstract class SecurityJournalFilterBase<T> where T : IEntityBase
   {
      public DateTime? DateFrom { get; set; }
      public DateTime? DateTo { get; set; }

      public CommonPagedList<T> Result { get; set; }

      public int PageNumber { get; set; }

      public SecurityJournalFilterBase()
      {
         PageNumber = 1;
      }
   }
}
