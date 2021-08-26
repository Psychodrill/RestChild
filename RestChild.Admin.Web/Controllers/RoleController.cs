using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Admin.Web.Controllers.WebApi;

namespace RestChild.Admin.Web.Controllers
{
   [Authorize]
   public class RoleController : BaseController
   {
      public WebRoleController ApiRoleController { get; set; }


      public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
      {
         base.SetUnitOfWorkInRefClass(unitOfWork);
         ApiRoleController.SetUnitOfWorkInRefClass(unitOfWork);
      }

      public ActionResult Index()
      {
         return RedirectToAction("Search");
      }

      public ActionResult Search(string name = "", int pageNumber = 1, string error = null)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);
         var roles = ApiRoleController.Get(name, pageNumber);
         ViewBag.name = name;

         if (!string.IsNullOrWhiteSpace(error))
         {
            ModelState.AddModelError(string.Empty, error);
         }

         return View("RoleList", roles);
      }

      public ActionResult Insert()
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         var newRecord = new Role();

         var accessRights = GetAvalibleAccessRights();

         ViewBag.AvailableAccessRights = accessRights
            .Select(x => new SelectListItem
            {
               Text = x.Name,
               Value = x.Id.ToString(CultureInfo.InvariantCulture)
            });
         return View("RoleEdit", newRecord);
      }

      private List<AccessRight> GetAvalibleAccessRights()
      {
         var accessRights = UnitOfWork.GetSet<AccessRight>().OrderBy(a => a.Name).ToList();
         foreach (var code in accessRights.Where(r => !string.IsNullOrWhiteSpace(r.GroupCode)).ToList())
         {
            if (!Security.HasRight(code.GroupCode))
            {
               accessRights.Remove(code);
            }
         }

         return accessRights;
      }

      public ActionResult Update(long id)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         var record = ApiRoleController.Get(id);
         var accessRights = GetAvalibleAccessRights();

         ViewBag.AvailableAccessRights = accessRights
            .Select(accessRight => new SelectListItem
            {
               Text = accessRight.Name,
               Value = accessRight.Id.ToString(CultureInfo.InvariantCulture),
               Selected = record.AccessRights.Any(selectedAccessRight => accessRight.Id == selectedAccessRight.Id)
            });
         return View("RoleEdit", record);
      }

      [HttpPost]
      public ActionResult Save(Role role, IEnumerable<int> chosenAccessRights)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         if (!ModelState.IsValid)
         {
            ViewBag.AvailableAccessRights = new List<SelectListItem>();
            return View("RoleEdit", role);
         }

         if (chosenAccessRights != null)
         {
            role.AccessRights = chosenAccessRights.Select(x => new AccessRight() {Id = x}).ToList();
         }

         if (role.Id == 0)
         {
            ApiRoleController.Post(role);
         }
         else
         {
            ApiRoleController.Put(role.Id, role);
         }

         return RedirectToAction("Update", new {id = role.Id});
      }

      [HttpGet]
      public ActionResult Delete(int? pageNumber, long id, string name = "")
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         var role = UnitOfWork.GetById<Role>(id);

         var accountRolesQuery = UnitOfWork.GetSet<AccountRoles>();


         //если первого запроса достаточно, то второго долгого не будет
         if (accountRolesQuery.Any(i => i.RoleId == id))
         {
            return Search(name, pageNumber ?? 1,
               $"Нельзя удалить роль \"{role.Name}\", т.к. к ней привязаны пользователи");
         }

         UnitOfWork.Delete(role);
         UnitOfWork.SaveChanges();

         return RedirectToAction("Search");
      }
   }
}
