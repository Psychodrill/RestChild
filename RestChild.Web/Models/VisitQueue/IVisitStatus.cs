using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Web.Models.VisitQueue
{
   public interface IVisitStatus
   {
      long Id { get; }

      string Name { get; }
   }
}
