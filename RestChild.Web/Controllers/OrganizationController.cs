using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Контроллер организаций
    /// </summary>
	[Authorize]
	public class OrganizationController : BaseController
	{
		public WebApi.OrganizationController ApiController { get; set; }


        /// <summary>
        ///     Список организаций
        /// </summary>
        public ActionResult List(OrganizationSearchModel filterModel)
		{
			filterModel.Name = filterModel.Name ?? string.Empty;
			filterModel.PageNumber = filterModel.PageNumber == 0 || filterModel.ChangeOrgType ? 1 : filterModel.PageNumber;
			filterModel.OrganizationType = filterModel.OrganizationType == 0 ? 1 : filterModel.OrganizationType;

			if (!Security.HasRight(AccessRightEnum.Organization.View))
			{
				return RedirectToAvalibleAction();
			}
			ViewBag.Name = filterModel.Name;

			var commonPagedList = ApiController.List(filterModel);

			return View(new OrganizationPagedList() {
				Name = filterModel.Name,
				OrganizationType  = filterModel.OrganizationType,
				CommonPagedList = commonPagedList,
				OivId = filterModel.OivId,
				Oivs = UnitOfWork.GetSet<Organization>().Where(o=>o.IsVedomstvo && !o.IsDeleted).OrderBy(o=>o.Name).ToList(),
				StateDistricts = UnitOfWork.GetSet<StateDistrict>().Where(o => o.IsActive).OrderBy(o => o.Name).ToList(),
				StateDistrictId = filterModel.StateDistrictId
                });
		}

        /// <summary>
        ///     Редактировать/создать новую организацию
        /// </summary>

        public ActionResult Edit(long id = 0)
		{
			if (!Security.HasRight(AccessRightEnum.Organization.Edit))
			{
				return RedirectToAction("List");
			}

			var entity = ApiController.Get(id) ?? new Organization {IsDeleted = false, IsLast = true};
			var vm = new OrganizationViewModel(entity)
			{
				StateDistricts = UnitOfWork.GetSet<StateDistrict>().OrderBy(s => s.Name).ToList(),
				Contracts = UnitOfWork.GetSet<Contract>().Where(c=>c.SupplierId == entity.Id && c.StateId.HasValue && c.StateId != StateMachineStateEnum.Deleted && c.StateId == StateMachineStateEnum.Contract.Active).OrderBy(c=>c.SignDate).ToList()
			};

			if (vm.Parent != null)
			{
				vm.Parent = ApiController.GetLastVersion(vm.Parent);
			}

			return View(vm);
		}

		/// <summary>
		///     Переход на поиск
		/// </summary>
		[HttpGet]
		public ActionResult Save()
		{
			return RedirectToAction("List");
		}

		/// <summary>
		///     Сохранение организации
		/// </summary>
		[HttpPost]
		public ActionResult Save(OrganizationViewModel model)
		{
			model.Data.TypeOfTransport = new List<TypeOfTransport>();

			SetUnitOfWorkInRefClass();
			if (UnitOfWork.GetLastUpdateTickById<Organization>(model.Data.Id) != model.Data.LastUpdateTick)
			{
				SetRedicted();
				return RedirectToAction("Edit", new { id = model.Data.Id });
			}

			var typeOfTransportsIds = new List<long>();
			if (model.SelectedOrganizationTypes != null)
				typeOfTransportsIds.AddRange(model.SelectedOrganizationTypes.Split(',').Select(Int32.Parse).Select(organizationType => (long) organizationType));

			var data = model.BuildData();
			data.LastUpdateTick = DateTime.Now.Ticks;
			data = ApiController.Save(data, typeOfTransportsIds);

			return RedirectToAction("Edit", new {id = data.Id});
		}
	}
}
