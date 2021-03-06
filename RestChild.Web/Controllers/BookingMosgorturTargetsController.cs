using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Контроллер работы с целями обращения визитов в МГТ
    /// </summary>
    public class BookingMosgorturTargetsController : BaseController
    {
        public WebBookingMosgorturTargetsController ApiController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        /// <summary>
        ///     Index
        /// </summary>
        public override ActionResult Index()
        {
            return RedirectToAction(nameof(Search));
        }

        /// <summary>
        ///     Список целей обращения и их поиск
        /// </summary>
        public ActionResult Search(BookingMosgorturTargetsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturBookingTargetsView) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsViewClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsViewBookingDepartment))
            {
                return RedirectToAvalibleAction();
            }

            if (filter == null)
                filter = new BookingMosgorturTargetsFilterModel();
            if (Security.HasRight(AccessRightEnum.MosgorturBookingTargetsViewClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsViewBookingDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsView))
                filter.DepartmentId = 3;
            if (Security.HasRight(AccessRightEnum.MosgorturBookingTargetsViewBookingDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsViewClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsView))
                filter.DepartmentId = 4;
            ViewBag.Departments = ApiController.GetDepartments();
            filter.Targets = ApiController.Get(filter);
            return View("BookingTargetList", filter);
        }

        /// <summary>
        ///     Добавить цель обращения
        /// </summary>
        public ActionResult Insert()
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEdit) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditBookingDepartment))
            {
                return RedirectToAvalibleAction();
            }
            ViewBag.Departments = ApiController.GetDepartments();
            return View("BookingTargetEdit", new MGTVisitTarget());
        }

        /// <summary>
        ///     Редактировать цель обращения
        /// </summary>
        public ActionResult Update(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEdit) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditBookingDepartment))
            {
                return RedirectToAvalibleAction();
            }
            ViewBag.Departments = ApiController.GetDepartments();
            return View("BookingTargetEdit", UnitOfWork.GetById<MGTVisitTarget>(id));
        }

        /// <summary>
        ///     Сохранить цель обращения
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(MGTVisitTarget VisitTarget)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEdit) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditBookingDepartment))
            {
                return RedirectToAvalibleAction();
            }

            if (!ModelState.IsValid)
            {
                return View("BookingTargetEdit", VisitTarget);
            }

            var b = UnitOfWork.GetById<MGTVisitTarget>(VisitTarget.Id);
            if (b == null)
            {
                b = new MGTVisitTarget();
                b.Id = UnitOfWork.GetSet<MGTVisitTarget>().Max(s => s.Id) + 1;
            }
            if (Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditBookingDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEdit))
                VisitTarget.DepartmentId = 3;
            if (Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditBookingDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEditClientDepartment) && !Security.HasRight(AccessRightEnum.MosgorturBookingTargetsEdit))
                VisitTarget.DepartmentId = 4;
            b.Name = VisitTarget.Name;
            b.IsActive = VisitTarget.IsActive;
            b.Description = VisitTarget.Description;
            b.IsForMPGU = VisitTarget.IsForMPGU;
            b.DepartmentId = VisitTarget.DepartmentId;
            if (VisitTarget.Id < 1)
            {
                UnitOfWork.AddEntity(b);
            }
            UnitOfWork.SaveChanges();
            return RedirectToAction("Update", new { id = b.Id });
        }
    }
}
