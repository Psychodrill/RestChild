using System;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Контроллер ГБУ подведомственных префектурам
    /// </summary>
    [Authorize]
    public partial class OrganizationGBUController : BaseController
    {
        /// <summary>
        ///     Вход по умолчанию
        /// </summary>
        public override ActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        ///     Список ГБУ
        /// </summary>
        [Route("OrganizationGBU/List")]
        public ActionResult List(OrganizationGBUSearchModel filterModel)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.GBUView))
            {
                return RedirectToAvalibleAction();
            }

            var q = GetOrganizationGBUQuery(filterModel);

            var totalCount = q.Count();
            var entity = q.OrderBy(ss => ss.ShortName).Skip(filterModel.StartRecord).Take(filterModel.PageSize).ToList();

            filterModel.Results = new CommonPagedList<OrganizationGBUViewModel>(
                entity.Select(ss => new OrganizationGBUViewModel(ss)).ToList(), filterModel.PageNumber, filterModel.PageSize,
                totalCount);

            return View(filterModel);
        }

        /// <summary>
        ///     Редактировать ГБУ
        /// </summary>
        [Route("OrganizationGBU/Edit/{orgId}")]
        public ActionResult Edit(long orgId)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.GBUEdit))
            {
                return RedirectToAvalibleAction();
            }

            var gbu = UnitOfWork.GetById<MonitoringGBU>(orgId);

            return View(new OrganizationGBUViewModel(gbu));
        }

        /// <summary>
        ///     Создать ГБУ
        /// </summary>
        [Route("OrganizationGBU/Add")]
        public ActionResult Add()
        {
            if (!Security.HasRight(AccessRightEnum.Organization.GBUEdit))
            {
                return RedirectToAvalibleAction();
            }

            return View(nameof(Edit), new OrganizationGBUViewModel(new MonitoringGBU()));
        }

        /// <summary>
        ///     Сохранить ГБУ
        /// </summary>
        [Route("OrganizationGBU/Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(OrganizationGBUViewModel model)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.GBUEdit))
            {
                return RedirectToAvalibleAction();
            }

            var gbu = model.BuildData();

            if (gbu.Id <= 0)
            {
                //создание нового ГБУ
                UnitOfWork.AddEntity(gbu);

                return RedirectToAction(nameof(Edit), new {orgId = gbu.Id});
            }
            else
            {
                var presisted = UnitOfWork.GetById<MonitoringGBU>(model.Data.Id);

                presisted.FullName = gbu.FullName;
                presisted.ShortName = gbu.ShortName;
                presisted.FactAddress = gbu.FactAddress;
                presisted.OrganisationId = gbu.OrganisationId;

                UnitOfWork.SaveChanges();

                return RedirectToAction(nameof(Edit), new {orgId = gbu.Id});
            }
        }

        private IQueryable<MonitoringGBU> GetOrganizationGBUQuery(OrganizationGBUSearchModel filter)
        {
            var q = UnitOfWork.GetSet<MonitoringGBU>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                q = q.Where(ss =>
                    ss.ShortName.ToLower().Contains(filter.Name.ToLower()) ||
                    ss.FullName.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.OrganisationId.HasValue && filter.OrganisationId.Value > 0)
            {
                q = q.Where(ss => ss.OrganisationId == filter.OrganisationId.Value);
                filter.OrganisationName = UnitOfWork.GetSet<Organization>()
                    .Where(ss => ss.Id == filter.OrganisationId.Value).Select(ss => ss.Name).FirstOrDefault();
            }

            return q;
        }
    }
}
