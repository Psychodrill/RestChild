using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Extensions.Filter;
using RestChild.Mobile.DAL.Enum;
using RestChild.Mobile.DAL.RepositoryExtensions;
using RestChild.Mobile.Domain;
using RestChild.Web.Models.GiftReserved;
using RestChild.Web.Properties;
using StateMachineEnum = RestChild.Mobile.DAL.Enum.StateMachineEnum;

namespace RestChild.Web.Controllers
{
    public class GiftReservedController : BaseMobileController
    {
        /// <summary>
        ///     список
        /// </summary>
        public ActionResult List(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.GiftReserved.View))
            {
                return RedirectToAvailableAction();
            }

            OutOfDateGiftMassCancel();

            model = model ?? new ListModel();

            var query = PrepareListModel(model);

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = model.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var items = query
                .OrderBy(b => b.Gift.Name)
                .ThenBy(b => b.Id).Skip(startRecord).Take(pageSize)
                .ToList();
            model.Result = new CommonPagedList<GiftReserved>(items, pageNumber, pageSize, totalCount);

            return View(model);
        }

        /// <summary>
        ///     выгрузка
        /// </summary>
        public ActionResult ExcelList(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.GiftReserved.View))
            {
                return RedirectToAvailableAction();
            }

            var query = PrepareListModel(model ?? new ListModel());

            var columns = new List<ExcelColumn<GiftReserved>>
            {
                new ExcelColumn<GiftReserved>
                {
                    Title = "ФИО ребенка",
                    Func = t => t.Owner?.Campers?.OrderByDescending(c => c.Id).FirstOrDefault()?.Name ??
                                t.Owner?.Name ?? "-",
                    Width = 42
                },
                new ExcelColumn<GiftReserved>
                {
                    Title = "Дата рождения",
                    Func = t => t.Owner?.Campers?.OrderByDescending(c => c.Id).FirstOrDefault()?.DateOfBirth ??
                                t.Owner?.DateOfBirth,
                    Width = 19
                },
                new ExcelColumn<GiftReserved>
                {
                    Title = "Лагерь/смена",
                    Func = t => t.Owner?.Campers?.OrderByDescending(c => c.Id).FirstOrDefault()?.Bout?.Name ?? "Не в лагере",
                    Width = 60
                },
                new ExcelColumn<GiftReserved> {Title = "Название подарка", Func = t => t.Gift?.Gift?.Name, Width = 60},
                new ExcelColumn<GiftReserved>
                    {Title = "Описание подарка", Func = t => t.Gift?.Gift?.Description, Width = 80},
                new ExcelColumn<GiftReserved> {Title = "Цена", Func = t => t.Gift?.Gift?.Price, Width = 25},
                new ExcelColumn<GiftReserved> {Title = "Параметр", Func = t => t.Gift?.Name, Width = 33},
                new ExcelColumn<GiftReserved> {Title = "Кол-во", Func = t => t.Count, Width = 10},
                new ExcelColumn<GiftReserved>
                {
                    Title = "Дата бронирования",
                    Func = t => ((t.StateId == StateEnum.GiftReserved.Reserved ||
                                  t.StateId == StateEnum.GiftReserved.Issued) && t.DateReserved != null)
                        ? t.DateReserved
                        : null,
                    Width = 25
                },
                new ExcelColumn<GiftReserved> {Title = "Статус", Func = t => t.State?.Name, Width = 30}
            };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            var data = query.OrderBy(d => d.Gift.Name).ThenBy(d => d.Id).ToList();

            using (var excel = new ExcelTable<GiftReserved>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Реестр выдачи подарков");

                excel.TableName = "Реестр выдачи подарков";

                string price = null;
                if (model?.PriceFrom != null)
                {
                    price += $" от {model?.PriceFrom}";
                }

                if (model?.PriceTo != null)
                {
                    price += $" до {model?.PriceTo}";
                }

                excel.Parameters = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("ФИО ребенка:", model?.Child),
                        new Tuple<string, string>("Цена подарка:", price),
                        new Tuple<string, string>("Название подарка:", model?.Name),
                        new Tuple<string, string>("Параметр подарка:", model?.Parameter),
                        new Tuple<string, string>("Статус:",
                            model.Statuses?.Where(s => s.Id == model?.StatusId).Select(x => x.Name).FirstOrDefault()),
                    }
                    .Where(i => !String.IsNullOrWhiteSpace(i.Item2))
                    .ToList();

                excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Зарезервированные подарки.xlsx");
            }
        }

        /// <summary>
        ///     подготовить модель для поиска
        /// </summary>
        private IQueryable<GiftReserved> PrepareListModel(ListModel model)
        {
            if (model.PageNumber <= 0)
            {
                model.PageNumber = 1;
            }

            model.Statuses = MobileUw.GetSet<State>().Where(s => s.StateMachineId == StateMachineEnum.GiftReserved)
                .OrderBy(t => t.Name).ToArray();

            var query = MobileUw.GetSet<GiftReserved>().Where(b =>
                b.StateId != StateMachineStateEnum.Deleted);

            if (model.StatusId.HasValue)
            {
                query = query.Where(q => q.StateId == model.StatusId);
            }

            if (model.PriceFrom.HasValue)
            {
                query = query.Where(q => q.Price >= model.PriceFrom);
            }

            if (model.PriceTo.HasValue)
            {
                query = query.Where(q => q.Price <= model.PriceTo);
            }

            if (model.ReservedFrom.HasValue)
            {
                query = query.Where(q => q.DateReserved >= model.ReservedFrom.Value);
            }

            if (model.ReservedTo.HasValue)
            {
                var d1 = model.ReservedTo.Value.Date.AddDays(1);
                query = query.Where(q => q.DateReserved < d1);
            }

            if (!string.IsNullOrWhiteSpace(model.Child))
            {
                var t = model.Child.ToLower().Trim();
                query = query.Where(q => q.Owner.Campers.Any(c => c.Name.Contains(t)));
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var t = model.Name.ToLower().Trim();
                query = query.Where(q => q.Gift.Gift.Name.Contains(t));
            }

            if (!string.IsNullOrWhiteSpace(model.Parameter))
            {
                var t = model.Parameter.ToLower().Trim();
                query = query.Where(q => q.Gift.Name.Contains(t));
            }

            if (model.BoutId != null && model.BoutId > 0)
            {
                query = query.Where(q => q.Owner.Campers.Any(c => c.BoutId == model.BoutId));
                model.BName = MobileUw.GetSet<Bout>().GetById(model.BoutId.Value).Name;
            }

            return query;
        }

        /// <summary>
        ///     Сброс всех подарков
        /// </summary>
        public ActionResult GiftMassCancel()
        {
            if (!Security.HasRight(AccessRightEnum.GiftReserved.MassCancel))
            {
                return RedirectToAvailableAction();
            }

            var gr = MobileUw.GetSet<GiftReserved>().Where(b =>
                b.StateId == StateEnum.GiftReserved.Reserved).ToList();

            gr.ForEach(ss =>
            {
                var description = $"Отмена выдачи подарка: {ss?.Gift?.Gift?.Name} {ss?.Gift?.Name} для ребенка: {ss?.Owner?.Name}";
                ss.StateId = StateEnum.GiftReserved.Refusal;

                var link = MobileUw.WriteHistory(ss.Link, "Массовая отмена выдачи подарков", description,
                    Security.GetCurrentAccountId());
                if (ss.Link == null)
                {
                    ss.LinkId = link.Id;
                }

            });
            MobileUw.SaveChanges();


            return RedirectToAction(nameof(List));
        }

        /// <summary>
        ///     Сброс всех зарезервированных подарков 20 декабря в 0:00
        /// </summary>
        public void OutOfDateGiftMassCancel()
        {
            var today = DateTime.Now;

            var dayX = new DateTime(today.Year, 12, 20);

            if (today <= dayX)
            {
                dayX = dayX.AddYears(-1);
            }

            var gr = MobileUw.GetSet<GiftReserved>().Where(b =>
                b.StateId == StateEnum.GiftReserved.Reserved && b.DateReserved <= dayX).ToList();

            gr.ForEach(ss =>
            {
                ss.StateId = StateEnum.GiftReserved.Refusal;

                var description = $"Отмена выдачи подарка: {ss?.Gift?.Gift?.Name} {ss?.Gift?.Name} для ребенка: {ss?.Owner?.Name}";
                var link = MobileUw.WriteHistory(ss.Link, "Автоматическая отмена выдачи подарков", description,
                    Security.GetCurrentAccountId());
                if (ss.Link == null)
                {
                    ss.LinkId = link.Id;
                }

            });
            MobileUw.SaveChanges();
        }
    }
}
