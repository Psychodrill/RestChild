using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Logic;
using RestChild.Web.Models;
using RestChild.Web.Models.MonitoringHotel;
using Settings = RestChild.Web.Properties.Settings;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Контроллер поиска объектов отдыха
    /// </summary>
    public class MonitoringHotelController : BaseController
    {
        public StateController ApiStateController { get; set; }


        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
        }


        /// <summary>
        ///     Поиск объектов отдыха
        /// </summary>
        public ActionResult Search(MonitoringHotelFilterModel filter)
        {
            if (!Security.HasRight(AccessRightEnum.VocabularyManage))
            {
                return RedirectToAction("Index", "Home");
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            var pageSize = Settings.Default.TablePageSize;

            filter.PageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
            var skip = (filter.PageNumber - 1) * pageSize;

            var query = GetMonitoringHotelsQuery(filter);

            var totalCount = query.Count();
            var list = query.OrderBy(i => i.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToArray();

            filter.Results = new CommonPagedList<MonitoringHotel>(list, filter.PageNumber, pageSize, totalCount);
            return View("MonitoringHotelList", filter);
        }


        /// <summary>
        ///     Получение коллекции IQueryable для поиска объектов отдыха на основе фильтра
        /// </summary>
        private IQueryable<MonitoringHotel> GetMonitoringHotelsQuery(MonitoringHotelFilterModel filter)
        {
            var query =
                UnitOfWork.GetAll<MonitoringHotel>().Where(x => !x.IsDeleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(i =>
                    i.ShortName.ToLower().Contains(filter.Name.ToLower()) ||
                    i.FullName.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.INN))
            {
                query = query.Where(a => a.Inn == filter.INN);
            }

            return query;
        }


        /// <summary>
        ///     Добавить новый объект отдыха
        /// </summary>
        public ActionResult Insert()
        {
            var monitoringHotel = new MonitoringHotel();
            var viewModel = CreateModel(monitoringHotel);
            viewModel.State.NeedRemoveButton = false;
            return View("MonitoringHotelEdit", viewModel);
        }


        /// <summary>
        ///     Обновить объект отдыха
        /// </summary>
        public ActionResult Edit(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var entity = UnitOfWork.GetSet<MonitoringHotel>().FirstOrDefault(i => i.Id == id);
            return View("MonitoringHotelEdit", CreateModel(entity));
        }


        /// <summary>
        ///     Сохранить объект отдыха
        /// </summary>
        [HttpPost]
        public ActionResult Save(MonitoringHotelModel monitoringHotelModel)
        {
            var currentAccountId = Security.GetCurrentAccountId();

            if (!Security.HasRight(AccessRightEnum.VocabularyManage) || !currentAccountId.HasValue)
            {
                return RedirectToAction("Search");
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            var data = monitoringHotelModel.BuildData();
            if (monitoringHotelModel.DistrictId > 0)
            {
                data.RegionId = monitoringHotelModel.DistrictId;
            }
            else
            {
                data.RegionId = null;
            }

            if (!ModelState.IsValid)
            {
                return View("MonitoringHotelEdit", CreateModel(monitoringHotelModel.Data));
            }

            if (data.Id == 0)
            {
                data = UnitOfWork.AddEntity(data);
                data.HistoryLink = this.WriteHistory(data.HistoryLink,
                    "Первое сохранение объекта отдыха", string.Empty);
                data.HistoryLinkId = data.HistoryLink?.Id;

                UnitOfWork.SaveChanges();

                return RedirectToAction("Edit", new {id = data.Id});
            }

            else if (data.Id > 1)
            {
                var entity = UnitOfWork.GetById<MonitoringHotel>(data.Id);

                var sb = new StringBuilder();

                if (entity.NullSafe(a => a.ShortName) != data.NullSafe(a => a.ShortName))
                {
                    sb.AppendLine(
                        $"<li>Изменено поле 'Сокращенное название' старое значение:'{entity.NullSafe(r => r.ShortName).FormatEx()}', новое значение:'{data.NullSafe(r => r.ShortName).FormatEx()}'</li>");
                }

                if (entity.NullSafe(a => a.FullName) != data.NullSafe(a => a.FullName))
                {
                    sb.AppendLine(
                        $"<li>Изменено поле 'Полное название' старое значение:'{entity.NullSafe(r => r.FullName).FormatEx()}', новое значение:'{data.NullSafe(r => r.FullName).FormatEx()}'</li>");
                }

                if (entity.NullSafe(a => a.FactAddress) != data.NullSafe(a => a.FactAddress))
                {
                    sb.AppendLine(
                        $"<li>Изменено поле 'Фактический адрес' старое значение:'{entity.NullSafe(r => r.FactAddress).FormatEx()}', новое значение:'{data.NullSafe(r => r.FactAddress).FormatEx()}'</li>");
                }

                if (entity.NullSafe(a => a.Inn) != data.NullSafe(a => a.Inn))
                {
                    sb.AppendLine(
                        $"<li>Изменено поле 'ИНН' старое значение:'{entity.NullSafe(r => r.Inn).FormatEx()}', новое значение:'{data.NullSafe(r => r.Inn).FormatEx()}'</li>");
                }

                if (entity.NullSafe(a => a.RegionId) != data.NullSafe(a => a.RegionId))
                {
                    var oldName = UnitOfWork.GetSet<MonitoringHotel>().Where(x => x.RegionId == entity.RegionId)
                        .FirstOrDefault()?.Region?.Name;
                    var newName = UnitOfWork.GetSet<StateDistrict>().Where(x => x.Id == data.RegionId).FirstOrDefault()
                        ?.Name;

                    sb.AppendLine(
                        $"<li>Изменено поле 'Регион' старое значение:'{oldName.FormatEx(val: "Не выбрано")}', новое значение:'{newName.FormatEx(val: "Не выбрано")}'</li>");
                }

                var diff = sb.ToString();


                if (!diff.IsNullOrEmpty())
                {
                    if (data.HistoryLinkId.HasValue)
                    {
                        entity.ShortName = data.ShortName;
                        entity.FullName = data.FullName;
                        entity.FactAddress = data.FactAddress;
                        entity.Inn = data.Inn;
                        entity.RegionId = data.RegionId;

                        var history = UnitOfWork.GetById<HistoryLink>(data.HistoryLinkId);
                        history = this.WriteHistory(history, "Изменение объекта отдыха", $"<ul>{diff}</ul>");
                        UnitOfWork.SaveChanges();
                    }
                }

                return RedirectToAction("Edit", new {id = data.Id});
            }

            return RedirectToAction("Search");
        }


        /// <summary>
        ///     Удалить (отправить в архив) объект отдыха
        /// </summary>
        public ActionResult Delete(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var entity = UnitOfWork.GetSet<MonitoringHotel>().FirstOrDefault(i => i.Id == id);
            entity.IsDeleted = true;
            UnitOfWork.SaveChanges();
            return RedirectToAction("Search");
        }

        /// <summary>
        ///     Создать модель для передачи в представление
        /// </summary>
        private MonitoringHotelModel CreateModel(MonitoringHotel entity)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var model = new MonitoringHotelModel(entity);

            model.State = new ViewModelState();
            model.IsEditable = true;
            model.State.NeedSaveButton = true;
            model.State.NeedRemoveButton = true;
            model.State.CanReturn = true;
            model.State.ReturnController = "MonitoringHotel";
            model.State.ReturnAction = "Search";

            model.Data.HistoryLinkId = entity.HistoryLinkId;

            model.Data.RegionId = entity.RegionId;

            var districts = UnitOfWork.GetSet<StateDistrict>().Where(s => s.IsActive)
                .OrderBy(s => s.Id).ToList();
            districts.Insert(0, new StateDistrict() {Id = 0, Name = "-- Не выбрано --"});
            model.Districts = districts.ToDictionary(ss => ss.Id, sx => sx.Name);
            model.DistrictId = entity.RegionId;

            return model;
        }
    }
}
