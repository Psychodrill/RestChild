using System;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text.pdf.qrcode;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Контроллер лагерей всех регионов РФ
    /// </summary>
    [Authorize]
    public partial class OrganizationCampController : BaseController
    {
        /// <summary>
        ///     Вход по умолчанию
        /// </summary>
        public override ActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        ///     Список лагерей
        /// </summary>
        [Route("OrganizationCamp/List")]
        public ActionResult List(OrganizationCampSearchModel filterModel)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.CampView))
            {
                return RedirectToAvalibleAction();
            }

            //var q = UnitOfWork.GetSet<MonitoringHotel>().AsQueryable();

            //if (!string.IsNullOrWhiteSpace(filterModel.Name))
            //{
            //    q = q.Where(ss =>
            //        ss.ShortName.ToLower().Contains(filterModel.Name.ToLower()) ||
            //        ss.FullName.ToLower().Contains(filterModel.Name.ToLower()));
            //}

            //if (filterModel.RegionId.HasValue && filterModel.RegionId.Value > 0)
            //{
            //    q = q.Where(ss => ss.RegionId == filterModel.RegionId.Value);
            //}

            //if (!string.IsNullOrWhiteSpace(filterModel.Inn))
            //{
            //    q = q.Where(ss => ss.Inn.ToLower().Contains(filterModel.Inn.ToLower()));
            //}

            var q = GetOrganizationCampQuery(filterModel);

            var totalCount = q.Count();
            var entity = q.OrderBy(ss => ss.ShortName).Skip(filterModel.StartRecord)
                .Take(filterModel.PageSize).ToList();

            filterModel.Results = new CommonPagedList<OrganizationCampViewModel>(
                entity.Select(ss => new OrganizationCampViewModel(ss)).ToList(),
                filterModel.PageNumber, filterModel.PageSize, totalCount);

            FillCollections(filterModel, UnitOfWork);

            return View(filterModel);
        }

        /// <summary>
        ///     Создать лагерь
        /// </summary>
        [Route("OrganizationCamp/Add")]
        public ActionResult Add()
        {
            if (!Security.HasRight(AccessRightEnum.Organization.CampEdit))
            {
                return RedirectToAvalibleAction();
            }

            var res = new OrganizationCampViewModel(new MonitoringHotel());

            FillCollections(res, UnitOfWork);

            return View(nameof(Edit), res);
        }

        /// <summary>
        ///     Редактировать лагерь
        /// </summary>
        [Route("OrganizationCamp/Edit/{orgId}")]
        public ActionResult Edit(long orgId)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.CampEdit))
            {
                return RedirectToAvalibleAction();
            }

            var camp = UnitOfWork.GetById<MonitoringHotel>(orgId);
            var res = new OrganizationCampViewModel(camp);

            FillCollections(res, UnitOfWork);

            return View(res);
        }

        /// <summary>
        ///     Сохранить ГБУ
        /// </summary>
        [Route("OrganizationCamp/Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(OrganizationCampViewModel model)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.CampEdit))
            {
                return RedirectToAvalibleAction();
            }

            var camp = model.BuildData();

            if (camp.Id <= 0)
            {
                //создание нового лагеря
                UnitOfWork.AddEntity(camp);

                return RedirectToAction(nameof(Edit), new {orgId = camp.Id});
            }
            else
            {
                var presisted = UnitOfWork.GetById<MonitoringHotel>(model.Data.Id);

                presisted.FullName = camp.FullName;
                presisted.ShortName = camp.ShortName;
                presisted.FactAddress = camp.FactAddress;
                presisted.RegionId = camp.RegionId;
                presisted.Inn = camp.Inn;

                UnitOfWork.SaveChanges();

                return RedirectToAction(nameof(Edit), new {orgId = camp.Id});
            }
        }


        /// <summary>
        ///     Заполнить справочники в поисковой модели
        /// </summary>
        private static void FillCollections(OrganizationCampSearchModel model, IUnitOfWork uw)
        {
            model.Regions = uw.GetSet<StateDistrict>().Where(ss => ss.IsActive).ToList();
        }

        /// <summary>
        ///     Заполнить справочники в поисковой модели
        /// </summary>
        private static void FillCollections(OrganizationCampViewModel model, IUnitOfWork uw)
        {
            model.Regions = uw.GetSet<StateDistrict>().Where(ss => ss.IsActive).ToList();
        }

        private IQueryable<MonitoringHotel> GetOrganizationCampQuery(OrganizationCampSearchModel filter)
        {
            var q = UnitOfWork.GetSet<MonitoringHotel>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                q = q.Where(ss =>
                    ss.ShortName.ToLower().Contains(filter.Name.ToLower()) ||
                    ss.FullName.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.RegionId.HasValue && filter.RegionId.Value > 0)
            {
                q = q.Where(ss => ss.RegionId == filter.RegionId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Inn))
            {
                q = q.Where(ss => ss.Inn.ToLower().Contains(filter.Inn.ToLower()));
            }

            return q;
        }

    }
}
