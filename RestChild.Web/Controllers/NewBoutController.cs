using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml.Style;
using PagedList;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Mobile.DAL.Enum;
using RestChild.Mobile.DAL.RepositoryExtensions;
using RestChild.Mobile.Domain;
using RestChild.Web.Models.NewBout;
using RestChild.Web.Properties;
using Bout = RestChild.Mobile.Domain.Bout;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     управление заездами
    /// </summary>
    [Authorize]
    public partial class NewBoutController : BaseMobileController
    {
        /// <summary>
        ///     список
        /// </summary>
        public ActionResult List(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAvailableAction();
            }

            model = model ?? new ListModel();

            var query = PrepareNewBoutModel(model);

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = model.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var items = query
                .OrderByDescending(b => b.DateIncome)
                .ThenBy(b => b.Id).Skip(startRecord).Take(pageSize)
                .ToList();
            model.Result = new CommonPagedList<Bout>(items, pageNumber, pageSize, totalCount);

            return View(model);
        }

        /// <summary>
        ///     выгрузка
        /// </summary>
        public ActionResult ExcelList(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAvailableAction();
            }

            var query = PrepareNewBoutModel(model ?? new ListModel());

            var columns = new List<ExcelColumn<Bout>>
            {
                new ExcelColumn<Bout> {Title = "Место отдыха", Func = t => t.Camp?.Name ?? "-", Width = 42},
                new ExcelColumn<Bout> {Title = "Смена", Func = t => t.Change, Width = 35},
                new ExcelColumn<Bout> {Title = "Адрес", Func = t => t.Camp?.Address ?? "-", Width = 150}
            };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();


            var data = query.OrderBy(d => d.DateIncome).ToList();

            using (var excel = new ExcelTable<Bout>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Заезды");

                excel.TableName = "Заезды";

                excel.Parameters = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Год кампании:", model?.Year.ToString()),
                        new Tuple<string, string>("Место отдыха:", model.Camp?.Name),
                        new Tuple<string, string>("Смена:", model.Times?.Where(i => i.Id == model.GroupedTime).Select(x=>x.Name).FirstOrDefault()),
                        new Tuple<string, string>("Город:", model.City)
                    }
                    .Where(i => !String.IsNullOrWhiteSpace(i.Item2))
                    .ToList();

                excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Заезды.xlsx");
            }
        }

        /// <summary>
        ///     подготовить модель
        /// </summary>
        private IQueryable<Bout> PrepareNewBoutModel(ListModel model)
        {
            if (model.PageNumber <= 0)
            {
                model.PageNumber = 1;
            }

            var maxYear = MobileUw.GetSet<Bout>().Where(b => b.StateId != StateMachineStateEnum.Deleted)
                .Select(b => b.YearOfCompany).DefaultIfEmpty().Max();

            var minYear = MobileUw.GetSet<Bout>().Where(b => b.StateId != StateMachineStateEnum.Deleted)
                .Select(b => b.YearOfCompany).DefaultIfEmpty().Min();

            if (maxYear == 0 && minYear == 0)
            {
                maxYear = DateTime.Now.Year;
                minYear = DateTime.Now.Year;
            }

            if (!model.Year.HasValue)
            {
                model.Year = DateTime.Today.Year;
            }

            if (model.Year > maxYear)
            {
                model.Year = maxYear;
            }

            if (model.Year < minYear)
            {
                model.Year = minYear;
            }

            model.Years = Enumerable.Range(minYear, maxYear - minYear + 1).ToArray();
            model.Times = MobileUw.GetSet<GroupedTime>().OrderBy(g => g.Id).ToArray();
            model.Camp = MobileUw.GetById<Camp>(model.CampId);

            var query = MobileUw.GetSet<Bout>().Where(b =>
                b.StateId != StateMachineStateEnum.Deleted && b.GroupedTimeId.HasValue && b.DateIncome != default &&
                b.DateOutcome != default);

            query = query.Where(q => q.Campers.Any() || q.Personals.Any());

            if (model.CampId.HasValue)
            {
                query = query.Where(q => q.CampId == model.CampId);
            }

            query = query.Where(q => q.YearOfCompany == model.Year);

            if (model.GroupedTime.HasValue)
            {
                query = query.Where(q => q.GroupedTimeId == model.GroupedTime);
            }

            if (!string.IsNullOrWhiteSpace(model.City))
            {
                var city = model.City.ToLower().Trim();
                query = query.Where(q => q.Camp.NearestCity.ToLower().Contains(city));
            }

            return query;
        }

        /// <summary>
        ///     Карточка для управления
        /// </summary>
        public ActionResult Manage(long id, string activeTab)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAction("Index", "Home");
            }

            var entity = MobileUw.GetById<Bout>(id);
            if (entity == null || entity.StateId == StateEnum.Deleted)
            {
                return RedirectToAction("List");
            }

            var model = new ManageModel(entity) {HotelId = entity.Camp.Eid, ActiveTab = activeTab};
            model.CampsCollection = MobileUw.GetSet<Camp>().ToList();

            if (!model.HotelId.HasValue)
            {
                model.HotelId = UnitOfWork.GetSet<Hotels>().Where(h => h.Eid == entity.Camp.Id).Select(e => (long?)e.Id)
                    .FirstOrDefault();
            }

            return View(model);
        }

    }
}
