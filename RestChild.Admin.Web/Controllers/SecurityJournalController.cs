using RestChild.Admin.Web.Controllers.WebApi;
using RestChild.Admin.Web.Models;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Admin.Web.Controllers
{
    public class SecurityJournalController : BaseController
    {
      public SecurityJournalApiController SecurityJournalApi { get; set; }

      public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
      {
         base.SetUnitOfWorkInRefClass(unitOfWork);
         SecurityJournalApi.SetUnitOfWorkInRefClass(unitOfWork);
      }

      public ActionResult JournalEntrance(SecurityJournalEntranceFilterModel filter)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.JournalEntrance))
         {
            return RedirectToAvaliableAction();
         }

         filter = filter ?? new SecurityJournalEntranceFilterModel();
         filter.Result = SecurityJournalApi.GetJournalEntrance(filter);

         return View(filter);
      }

      public ActionResult JournalActiveSessions(SecurityJournalActiveSessionsFilterModel filter, string msg)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.JournalSessions))
         {
            return RedirectToAvaliableAction();
         }

         filter = filter ?? new SecurityJournalActiveSessionsFilterModel();
         filter.Result = SecurityJournalApi.GetJournalActiveSessions(filter);

         ViewBag.HasStopRight = Security.HasRight(AccessRightEnum.Security.StopSessions);

         return View(filter);
      }

      public ActionResult JournalStopedSessions(SecurityJournalActiveSessionsFilterModel filter, string msg)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.JournalSessions))
         {
            return RedirectToAvaliableAction();
         }

         filter = filter ?? new SecurityJournalActiveSessionsFilterModel();
         filter.Result = SecurityJournalApi.GetJournalStoppedSessions(filter);

         return View(filter);
      }

      public ActionResult JournalSecurityEvents(SecurityJournalDefaultFilterModel filter)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.JournalSecurity))
         {
            return RedirectToAvaliableAction();
         }
         filter = filter ?? new SecurityJournalDefaultFilterModel();
         filter.Result = SecurityJournalApi.GetJournalSecurityEvents(filter);
         return View(filter);
      }

      public ActionResult JournalOutSystemIteractionEvents(SecurityJournalDefaultFilterModel filter)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.IteractionsWithOutSystems))
         {
            return RedirectToAvaliableAction();
         }
         filter = filter ?? new SecurityJournalDefaultFilterModel();
         filter.Result = SecurityJournalApi.GetJournalOutSystemIteractionEvents(filter);
         return View(filter);
      }

      public ActionResult JournalRightsAndRoles(SecurityJournalDefaultFilterModel filter)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.JournalRoles))
         {
            return RedirectToAvaliableAction();
         }
         filter = filter ?? new SecurityJournalDefaultFilterModel();
         filter.Result = SecurityJournalApi.GetRightsAndRoles(filter);
         return View(filter);
      }

      public ActionResult JournalProcesses(SecurityJournalDefaultFilterModel filter)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasRight(AccessRightEnum.Security.JournalProceses))
         {
            return RedirectToAvaliableAction();
         }
         filter = filter ?? new SecurityJournalDefaultFilterModel();
         filter.Result = SecurityJournalApi.GetJournalProcesses(filter);
         return View(filter);
      }
   }
}
