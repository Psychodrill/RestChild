using RestChild.Admin.Web.Models;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Admin.Web.Controllers
{
   public class SecuritySettingsController : BaseController
   {
      public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
      {
         base.SetUnitOfWorkInRefClass(unitOfWork);
      }

      [HttpGet]
      public ActionResult Index()
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasAnyRights(new[] { AccessRightEnum.Security.SecuritySettingsView, AccessRightEnum.Security.SecuritySettingsEdit }))
         {
            return RedirectToAvaliableAction();
         }

         SecuritySettingsModel result = new SecuritySettingsModel()
         {
            AccountBlockSpan = SecuritySettings.AccountBlockSpan,
            MaxCountUnsuccess = SecuritySettings.MaxCountUnsuccess,
            MinLenPassword = SecuritySettings.MinLenPassword,
            TimeLifePassword = SecuritySettings.TimeLifePassword,
            TimeLogStorage = SecuritySettings.TimeLogStorage,
            SessionLifeTime = SecuritySettings.SessionTimeout,
            TimeLifeAccount = SecuritySettings.TimeLifeAccount
         };

         ViewBag.HasEditRight = Security.HasRight(AccessRightEnum.Security.SecuritySettingsEdit);

         return View(result);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult SaveSettings(SecuritySettingsModel model)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         if (!Security.HasAnyRights(new[] { AccessRightEnum.Security.SecuritySettingsEdit }))
         {
            return RedirectToAvaliableAction();
         }

         try
         {
            if (ModelState.IsValid)
            {
               SecuritySettings.AccountBlockSpan = model.AccountBlockSpan;
               SecuritySettings.MaxCountUnsuccess = model.MaxCountUnsuccess;
               SecuritySettings.MinLenPassword = model.MinLenPassword;
               SecuritySettings.SessionTimeout = model.SessionLifeTime;
               SecuritySettings.TimeLifePassword = model.TimeLifePassword;
               SecuritySettings.TimeLogStorage = model.TimeLogStorage;
               SecuritySettings.TimeLifeAccount = model.TimeLifeAccount;
            }
         }
         catch (Exception ez)
         {
            model.ErrorMessage = ez.Message;
         }

         ViewBag.HasEditRight = true;

         return View("Index", model);
      }
    }
}
