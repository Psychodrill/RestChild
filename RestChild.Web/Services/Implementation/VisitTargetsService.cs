using RestChild.Comon;
using RestChild.DAL;
using RestChild.Web.Logic;
using RestChild.Web.Models.VisitQueue;
using RestChild.Web.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Services.Implementation
{
   public class VisitTargetsService : IVisitTargetsService, ILogic
   {
      public IUnitOfWork UnitOfWork { get; set; }

      public VisitTargetsService()
      {
         UnitOfWork = WindsorHolder.Resolve<IUnitOfWork>();
      }

      public IEnumerable<VisitTarget> GetVisitTargets()
      {
         VisitTargetRepository _vr = new VisitTargetRepository(UnitOfWork);
         return _vr.GetAllValidVisitTargets().Select(ss => (VisitTarget)ss).ToList();
      }
   }
}
