using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Extensions.Filter;
using RestChild.Mobile.DAL.RepositoryExtensions;
using RestChild.Mobile.Domain;
using RestChild.Web.Models.NewBout;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     реестр детей
    /// </summary>
    public partial class NewBoutController
    {
        /// <summary>
        ///     подготовить модель
        /// </summary>
        private IQueryable<Account> PrepareChildListModel(ChildListModel model)
        {
            if (model.PageNumber <= 0)
            {
                model.PageNumber = 1;
            }

            model.Times = MobileUw.GetSet<GroupedTime>().OrderBy(g => g.Id).ToArray();
            model.Camp = MobileUw.GetById<Camp>(model.CampId);

            var query = MobileUw.GetSet<Account>().Where(b =>
                b.IsDeleted != true && b.IsBlocked != true);

            query = query.Where(q => q.Campers.Any());

            var bouts = MobileUw.GetSet<Bout>().AsQueryable();

            var boutPresent = false;

            if (model.CampId.HasValue)
            {
                boutPresent = true;
                bouts = bouts.Where(q => q.CampId == model.CampId);
            }

            if (model.GroupedTime.HasValue)
            {
                boutPresent = true;
                bouts = bouts.Where(q => q.GroupedTimeId == model.GroupedTime);
            }

            if (!string.IsNullOrWhiteSpace(model.City))
            {
                boutPresent = true;
                var city = model.City.ToLower().Trim();
                bouts = bouts.Where(q => q.Camp.NearestCity.ToLower().Contains(city));
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var t = model.Name.ToLower().Trim();
                query = query.Where(q =>
                    q.Name.ToLower().Contains(t) || q.Campers.Any(c => c.Name.ToLower().Contains(t)));
            }

            if (boutPresent)
            {
                query = query.Where(q => q.Campers.Any(c => bouts.Any(b => b.Id == c.BoutId)));
            }

            return query;
        }

        /// <summary>
        ///     список
        /// </summary>
        public ActionResult ChildList(ChildListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAvailableAction();
            }

            model = model ?? new ChildListModel();

            var query = PrepareChildListModel(model);

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = model.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var items = query
                .OrderBy(b => b.Name)
                .ThenBy(b => b.Id).Skip(startRecord).Take(pageSize)
                .ToList();
            model.Result = new CommonPagedList<Account>(items, pageNumber, pageSize, totalCount);

            return View(model);
        }

        /// <summary>
        ///     выгрузка
        /// </summary>
        public ActionResult ExcelChildList(ChildListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAvailableAction();
            }

            var query = PrepareChildListModel(model ?? new ChildListModel());

            var columns = new List<ExcelColumn<Account>>
            {
                new ExcelColumn<Account> {Title = "Псевдоним", Func = t => t.Name ?? "-", Width = 42},
                new ExcelColumn<Account>
                {
                    Title = "ФИО", Func = t => string.Join("\n", t.Campers.OrderByDescending(c => c.Id).Select(c=>c.Name).Distinct()),
                    Width = 35
                },
                new ExcelColumn<Account>
                    {Title = "E-mail", Func = t => t.Email, Width = 30},
                new ExcelColumn<Account>
                    {Title = "Телефон", Func = t => t.Phone, Width = 15},
                new ExcelColumn<Account>
                    {Title = "Заданий выполнено", Func = t => t.TaskCount, Width = 10},
                new ExcelColumn<Account>
                    {Title = "Сумма баллов всего", Func = t => t.Points.ToString("0"), Width = 10},
                new ExcelColumn<Account>
                    {Title = "Баллов можно потратить", Func = t => t.PointsOnAccount.ToString("0"), Width = 10}
            };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();


            var data = query.OrderBy(b => b.Name)
                .ThenBy(b => b.Id).ToList();

            using (var excel = new ExcelTable<Account>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Реестр статистики по детям");

                excel.TableName = "Реестр статистики по детям";

                excel.Parameters = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("ФИО:", model?.Name),
                        new Tuple<string, string>("Место отдыха:", model.Camp?.Name),
                        new Tuple<string, string>("Смена:", model.Times?.Where(i => i.Id == model?.GroupedTime).Select(x=>x.Name).FirstOrDefault()),
                        new Tuple<string, string>("Город:", model?.City)
                    }
                .Where(i => !String.IsNullOrWhiteSpace(i.Item2))
                .ToList();

                excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Отдыхающие.xlsx");
            }
        }


        /// <summary>
        ///     Карточка для управления
        /// </summary>
        public ActionResult ManageChild(long id, string activeTab)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAction("Index", "Home");
            }

            var entity = MobileUw.GetById<Account>(id);
            if (entity == null)
            {
                return RedirectToAction("List");
            }

            var model = new ChildModel(entity) {ActiveTab = activeTab};

            return View(model);
        }
    }
}
