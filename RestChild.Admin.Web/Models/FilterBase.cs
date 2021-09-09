using RestChild.Comon;
using RestChild.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Admin.Web.Models
{
   public abstract class FilterBase<T> where T : IEntityBase
   {
       public CommonPagedList<T> Result { get; set; }

      public int PageNumber { get; set; }

      public FilterBase()
      {
         PageNumber = 1;
      }
   }
}
