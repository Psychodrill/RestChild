using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Mobile.DAL.Enum;
using RestChild.Mobile.DAL.RepositoryExtensions;
using RestChild.Mobile.Domain;
using RestChild.Web.Models;
using RestChild.Web.Models.Task;
using RestChild.Web.Properties;
using Bout = RestChild.Mobile.Domain.Bout;
using StateMachineEnum = RestChild.Mobile.DAL.Enum.StateMachineEnum;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Контроллер для заданий
    /// </summary>
    public class TaskController : BaseMobileController
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

            var query = PrepareTaskModel(model);

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = model.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var items = query
                .OrderBy(b => b.Name)
                .ThenBy(b => b.Id).Skip(startRecord).Take(pageSize)
                .ToList();
            model.Result = new CommonPagedList<BoutTask>(items, pageNumber, pageSize, totalCount);

            return View(model);
        }

        /// <summary>
        ///     выгрузка
        /// </summary>
        public ActionResult ExcelList(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.Task.View))
            {
                return RedirectToAvailableAction();
            }

            var query = PrepareTaskModel(model ?? new ListModel());

            var columns = new List<ExcelColumn<BoutTask>>
            {
                new ExcelColumn<BoutTask> {Title = "Название задания", Func = t => t.Name ?? "-", Width = 42},
                new ExcelColumn<BoutTask> {Title = "Лагерь/Период", Func = t => t.Bout?.Name, Width = 42},
                new ExcelColumn<BoutTask>
                    {Title = "Кол-во выбравших задание", Func = t => t.CamperTasks?.Count(c=>c.CamperId.HasValue) ?? 0, Width = 10},
                new ExcelColumn<BoutTask>
                {
                    Title = "Кол-во отказавшихся от задания",
                    Func = t => t.CamperTasks?.Count(c => c.StateId == StateEnum.CamperTask.Canceled && c.CamperId.HasValue), Width = 10
                },
                new ExcelColumn<BoutTask>
                {
                    Title = "Кол-во успешно завершивших задание",
                    Func = t => t.CamperTasks?.Count(c => c.StateId == StateEnum.CamperTask.Done && c.CamperId.HasValue), Width = 10
                },
                new ExcelColumn<BoutTask>
                {
                    Title = "Продолжительность задания", Func = t =>
                    {
                        var timesheet = string.IsNullOrWhiteSpace(t.Timesheet)
                            ? new Timesheet()
                            : JsonConvert.DeserializeObject<Timesheet>(t.Timesheet);
                        return timesheet.DurationText;
                    },
                    Width = 10
                },
                new ExcelColumn<BoutTask>
                {
                    Title = "Общая продолжительность выполнения задания", Func = t =>
                    {
                        return Timesheet.FormatTime(Convert.ToInt32(t.CamperTasks.Select(ct =>
                            ct.CompliteDate.HasValue && ct.AcceptDate.HasValue
                                ? (ct.CompliteDate - ct.AcceptDate).Value.TotalMinutes
                                : 0).DefaultIfEmpty().Average()));
                    },
                    Width = 10
                },
                new ExcelColumn<BoutTask> {Title = "Рейтинг", Func = t => t.CamperTasks.Select(x=>x.Rating).Average(), Width = 10},
                new ExcelColumn<BoutTask> {Title = "Стоимость", Func = t => t.Price, Width = 30, HorizontalAlignment = ExcelHorizontalAlignment.Right},
                new ExcelColumn<BoutTask>
                {
                    Title = "Статус", Func = t => t.State?.Name, HorizontalAlignment = ExcelHorizontalAlignment.Center,
                    Width = 30
                }
            };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            var data = query.OrderBy(d => d.Name).ThenBy(d => d.Id).ToList();

            using (var excel = new ExcelTable<BoutTask>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Реестр заданий");

                excel.TableName = "Реестр заданий";

                string cost = null;
                if (model?.PriceFrom != null)
                {
                    cost += $" от {model?.PriceFrom}";
                }
                if (model?.PriceTo != null)
                {
                    cost += $" до {model?.PriceTo}";
                }

                string amount = null;
                if (model?.CountFrom != null)
                {
                    amount += $" от {model?.CountFrom}";
                }
                if (model?.CountTo != null)
                {
                    amount += $" до {model?.CountTo}";
                }

                excel.Parameters = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Название задания:", model?.Name),
                        new Tuple<string, string>("Лагерь для проведения:", model.Camp?.Name),
                        new Tuple<string, string>("Стоимость:", cost),
                        new Tuple<string, string>("Статус:", model.States?.Where(s => s.Id == model?.StateId).Select(x => x.Name).FirstOrDefault()),
                        new Tuple<string, string>("Одновременное количество:", amount),
                    }
                    .Where(i => !String.IsNullOrWhiteSpace(i.Item2))
                    .ToList();

                excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Задания.xlsx");
            }
        }

        /// <summary>
        ///     подготовить модель для поиска
        /// </summary>
        private IQueryable<BoutTask> PrepareTaskModel(ListModel model)
        {
            if (model.PageNumber <= 0)
            {
                model.PageNumber = 1;
            }

            model.Camp = MobileUw.GetById<Camp>(model.CampId);
            model.States = MobileUw.GetSet<State>().Where(s => s.StateMachineId == StateMachineEnum.BoutTask)
                .OrderBy(d => d.Name).ToArray();

            var query = MobileUw.GetSet<BoutTask>().Where(b =>
                b.StateId != StateMachineStateEnum.Deleted);

            query = model.StateId.HasValue
                ? query.Where(q => q.StateId == model.StateId)
                : query.Where(q => q.StateId != StateEnum.BoutTask.Archive);

            if (model.PriceTo.HasValue)
            {
                query = query.Where(q => q.Price <= model.PriceTo);
            }

            if (model.PriceFrom.HasValue)
            {
                query = query.Where(q => q.Price >= model.PriceFrom);
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var t = model.Name.Trim().ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(t));
            }

            if (model.CountFrom.HasValue)
            {
                query = query.Where(q => q.CountOnBout >= model.CountFrom);
            }

            if (model.CountTo.HasValue)
            {
                query = query.Where(q => q.CountOnBout <= model.CountTo);
            }

            if (model.CampId.HasValue)
            {
                query = query.Where(q => q.Bout.CampId == model.CampId);
            }

            return query;
        }

        /// <summary>
        ///     Карточка для управления
        /// </summary>
        public ActionResult Manage(long? id, long? bid)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAction("Index", "Home");
            }

            var entity = MobileUw.GetById<BoutTask>(id) ?? new BoutTask
            {
                StateId = StateEnum.BoutTask.Editing,
                State = MobileUw.GetById<State>(StateEnum.BoutTask.Editing),
                Bout = MobileUw.GetById<Bout>(bid),
                CountOnBout = 1
            };

            if (!entity.BoutId.HasValue)
            {
                if (entity.Bout == null)
                {
                    return RedirectToAction("List", "NewBout");
                }

                entity.BoutId = entity.Bout.Id;
            }

            if (entity.StateId == StateEnum.Deleted)
            {
                return RedirectToAction("List", "NewBout");
            }

            var canEdit = entity.StateId == StateEnum.BoutTask.Editing;

            var actions = new List<StateMachineAction>();

            if (entity.StateId == StateEnum.BoutTask.Editing)
            {
                actions.Add(new StateMachineAction
                {
                    ActionName = "Сформировать",
                    ActionCode = "toFormed",
                    Description = "Сформировать задание?"
                });
            }
            else
            {
                actions.Add(new StateMachineAction
                {
                    ActionName = "В редактирование",
                    ActionCode = "toEdit",
                    Description = "Перевести в статус редактирование?"
                });
            }

            var needRemoveButton = entity.CamperTasks?.Any(c => c.CamperId.HasValue) ?? false;

            if (entity.StateId != StateEnum.BoutTask.Archive && !needRemoveButton)
            {
                actions.Add(new StateMachineAction
                {
                    ActionName = "Отправить в архив",
                    ActionCode = "toArchive",
                    Description = "Отправить задание в архив?"
                });
            }

            var model = new ManageModel(entity)
            {
                CanEdit = canEdit,
                State = new ViewModelState
                {
                    State = new StateMachineState
                    {
                        Id = entity.State.Id,
                        Name = entity.State.Name
                    },
                    FormSelector = "#mainForm",
                    ActionSelector = "#StateMachineActionString",
                    Actions = actions,
                    NeedSaveButton = canEdit,
                    NeedRemoveButton = needRemoveButton,
                    PostNoStatusActions = new List<NoStatusAction>()
                }
            };

            if (entity.LinkId.HasValue)
            {
                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "История",
                    ButtonClass = "btn btn-default btn-hystory-link",
                    SomeAddon = $"data-history-id=\"{model.Data.LinkId}\""
                });
            }

            return View(model);
        }


        /// <summary>
        ///     сохранение карточки
        /// </summary>
        [HttpGet]
        public ActionResult Save()
        {
            return RedirectToAction("List", "NewBout");
        }

        /// <summary>
        ///     разница
        /// </summary>
        private void Save(BoutTask source, BoutTask target)
        {
            source.Name = target.Name;
            source.Description = target.Description;
            source.StartDate = target.StartDate;
            source.FinishDate = target.FinishDate;
            source.Timesheet = target.Timesheet;
            source.CountOnBout = target.CountOnBout;
            source.Price = target.Price;

            var file = target.Link?.Files?.FirstOrDefault();
            if (file != null)
            {
                var sourceFile = source.Link?.Files?.FirstOrDefault();
                if (sourceFile != null)
                {
                    sourceFile.FileUrl = file.FileUrl;
                    sourceFile.FileName = file.FileName;
                    sourceFile.DateCreate = DateTime.Now;
                }
                else
                {
                    file.LinkId = source.LinkId;
                    MobileUw.AddEntity(file);
                }
            }
        }

        /// <summary>
        ///     период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        private string GetPeriodInfo(DateTime? start, DateTime? finish)
        {
            if (!start.HasValue || !finish.HasValue)
            {
                return string.Empty;
            }

            return $"в {start:HH:mm}, продолжительностью {(finish.Value - start.Value).TotalMinutes:F0} минут";
        }

        /// <summary>
        ///     разница
        /// </summary>
        private string GetDiff(BoutTask source, BoutTask target)
        {
            var sb = new StringBuilder();
            sb.Compare(source, target, "Изменено название задания", e => e.Name);
            sb.Compare(source, target, "Изменено описание задания", e => e.Description);
            sb.Compare(source, target, "Изменена стоимость задания", e => e.Price, e => e.Price.FormatEx());
            sb.Compare(source, target, "Изменено одновременное кол-во на заезд", e => e.CountOnBout);

            var sourceTimesheet = string.IsNullOrWhiteSpace(source.Timesheet)
                ? string.Empty
                : JsonConvert.DeserializeObject<Timesheet>(source.Timesheet).ToString();

            var targetTimesheet = string.IsNullOrWhiteSpace(target.Timesheet)
                ? string.Empty
                : JsonConvert.DeserializeObject<Timesheet>(target.Timesheet).ToString();

            if (sourceTimesheet != targetTimesheet)
            {
                sb.AppendLine(
                    $"<li>Изменено расписание задания, старое значение - '{sourceTimesheet}', новое значение - '{targetTimesheet}'</li>");
            }

            var file = target.Link?.Files?.FirstOrDefault();
            if (file != null)
            {
                sb.AppendLine($"<li>Добавлено фото задания {file.FileName}</li>");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     создание заданий для того что бы брать
        /// </summary>
        private void ProcessTask(BoutTask entity)
        {
            var tasks = entity.CamperTasks.Where(t => t.StateId != StateEnum.CamperTask.Canceled).ToList();

            var notification = MobileUw.GetSet<Notification>().Select(n => n.CamperTaskId);
            var toRemove = tasks.Where(c => !c.CamperId.HasValue && !notification.Contains(c.Id)).ToList();
            foreach (var task in toRemove)
            {
                tasks.Remove(task);
                MobileUw.Delete(task);
            }

            if (entity.Bout == null || entity.Bout.DateOutcome < DateTime.Now ||
                entity.StateId != StateEnum.BoutTask.Formed)
            {
                return;
            }

            var timesheet = string.IsNullOrWhiteSpace(entity.Timesheet)
                ? null
                : JsonConvert.DeserializeObject<Timesheet>(entity.Timesheet);

            if (timesheet == null)
            {
                return;
            }

            var time = TimeSpan.Parse(timesheet.Time);
            if (timesheet.State == TimesheetState.Simple)
            {
                var date = timesheet.Start + time;
                var countTask = tasks.Count(c => c.TaskDate == date);

                var camperTask = new CamperTask
                {
                    Price = entity.Price,
                    StateId = StateEnum.CamperTask.UnAssign,
                    BoutTaskId = entity.Id,
                    DurationToDeclaine = timesheet.Refuse,
                    TaskDate = date,
                    AvailabilityStart = date.AddMinutes(-timesheet.AvailableBefore),
                    AvailabilityFinish = date.AddMinutes(timesheet.AvailableAfter),
                    TaskFinishDate = date.AddMinutes(timesheet.Duration)
                };

                if (countTask >= entity.CountOnBout
                    || tasks.Any(c => c.TaskDate == date && !c.CamperId.HasValue)
                    || camperTask.AvailabilityFinish < DateTime.Now)
                {
                    return;
                }

                MobileUw.AddEntity(camperTask);
                return;
            }

            var currentDate = entity.StartDate ?? entity.Bout.DateIncome;
            var index = 0;
            while (currentDate <= entity.FinishDate && currentDate <= entity.Bout.DateOutcome)
            {
                var process = timesheet.State == TimesheetState.EveryDay && timesheet.EveryDay > 0 &&
                              index % timesheet.EveryDay == 0;

                process |= timesheet.State == TimesheetState.EveryWorkDay &&
                           currentDate.DayOfWeek != DayOfWeek.Saturday &&
                           currentDate.DayOfWeek != DayOfWeek.Sunday;

                process |= timesheet.State == TimesheetState.EveryWeek && (
                    timesheet.Every1 && currentDate.DayOfWeek == DayOfWeek.Monday ||
                    timesheet.Every2 && currentDate.DayOfWeek == DayOfWeek.Tuesday ||
                    timesheet.Every3 && currentDate.DayOfWeek == DayOfWeek.Wednesday ||
                    timesheet.Every4 && currentDate.DayOfWeek == DayOfWeek.Thursday ||
                    timesheet.Every5 && currentDate.DayOfWeek == DayOfWeek.Friday ||
                    timesheet.Every6 && currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    timesheet.Every7 && currentDate.DayOfWeek == DayOfWeek.Sunday);

                var taskDate = currentDate + time;

                var camperTask = new CamperTask
                {
                    Price = entity.Price,
                    StateId = StateEnum.CamperTask.UnAssign,
                    BoutTaskId = entity.Id,
                    DurationToDeclaine = timesheet.Refuse,
                    TaskDate = taskDate,
                    AvailabilityStart = taskDate.AddMinutes(-timesheet.AvailableBefore),
                    AvailabilityFinish = taskDate.AddMinutes(timesheet.AvailableAfter),
                    TaskFinishDate = taskDate.AddMinutes(timesheet.Duration)
                };

                if (process
                    && tasks.Count(t => t.TaskDate == camperTask.TaskDate) < entity.CountOnBout
                    && !tasks.Any(c => c.TaskDate == camperTask.TaskDate && !c.CamperId.HasValue)
                    && camperTask.AvailabilityFinish > DateTime.Now)
                {
                    MobileUw.AddEntity(camperTask);
                }

                currentDate = currentDate.AddDays(1);
                index++;
            }
        }

        /// <summary>
        ///     проверить задание
        /// </summary>
        private List<string> CheckTask(BoutTask entity)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.Add("Не указано название задания");
            }

            if (entity.Price <= 0)
            {
                result.Add("Не указана стоимость задания");
            }

            if (entity.CountOnBout <= 0)
            {
                result.Add("Не указано одновременное количество заданий");
            }

            if (!(entity.Link?.Files?.Any() ?? false))
            {
                result.Add("Не добавлена фотография задания");
            }

            var timesheet = string.IsNullOrWhiteSpace(entity.Timesheet)
                ? null
                : JsonConvert.DeserializeObject<Timesheet>(entity.Timesheet);

            if (timesheet == null)
            {
                result.Add("Не указано расписание задачи");
            }
            else
            {
                if (timesheet.Start < entity.Bout.DateIncome || timesheet.Start > entity.Bout.DateOutcome)
                {
                    result.Add("Дата выполнения задания выходит за сроки заезда");
                }

                if (timesheet.State == TimesheetState.EveryDay)
                {
                    if (timesheet.EveryDay <= 0)
                    {
                        result.Add("Не указан интервал повторения или он не корректен");
                    }
                }

                if (string.IsNullOrWhiteSpace(timesheet.Time))
                {
                    result.Add("Не указано время начала");
                }

                if (timesheet.State == TimesheetState.EveryWeek && !timesheet.Every1 && !timesheet.Every2 &&
                    !timesheet.Every3 && !timesheet.Every4
                    && !timesheet.Every5 && !timesheet.Every6 && !timesheet.Every7)
                {
                    result.Add("Не указан интервал повторения или он не корректен");
                }
            }

            return result;
        }

        /// <summary>
        ///     сохранение карточки
        /// </summary>
        [HttpPost]
        public ActionResult Save(ManageModel model)
        {
            if (!Security.HasRight(AccessRightEnum.NewBout.View))
            {
                return RedirectToAvailableAction();
            }

            var data = model?.BuildData();

            if (data == null)
            {
                return RedirectToAction("List", "NewBout");
            }

            var entity = MobileUw.GetById<BoutTask>(data.Id);

            if (entity == null)
            {
                entity = MobileUw.AddEntity(data);
                var link = MobileUw.WriteHistory(entity.Link, "Сохранение", "Добавление задания",
                    Security.GetCurrentAccountId());
                if (entity.Link == null)
                {
                    entity.LinkId = link.Id;
                }
            }
            else
            {
                if (entity.LastUpdateTick != data.LastUpdateTick)
                {
                    SetRedirected();
                    return RedirectToAction("Manage", new {id = entity.Id});
                }

                if (entity.StateId == StateEnum.BoutTask.Editing)
                {
                    var diff = GetDiff(entity, data);
                    if (!string.IsNullOrWhiteSpace(diff))
                    {
                        Save(entity, data);
                        var link = MobileUw.WriteHistory(entity.Link, "Сохранение", diff,
                            Security.GetCurrentAccountId());
                        if (entity.Link == null)
                        {
                            entity.LinkId = link.Id;
                        }
                    }
                }
            }

            MobileUw.SaveChanges();
            var id = entity.Id;
            MobileUw.DetachAllEntitys();
            entity = MobileUw.GetById<BoutTask>(id);

            if (!string.IsNullOrWhiteSpace(model.StateMachineActionString))
            {
                MobileUw.BeginTransaction();
                State toState = null;
                switch (model.StateMachineActionString)
                {
                    case "Delete":
                        toState = MobileUw.GetById<State>(StateEnum.Deleted);
                        break;
                    case "toFormed":
                        var errors = CheckTask(entity);

                        if (errors.Any())
                        {
                            SetErrors(errors);
                        }
                        else
                        {
                            toState = MobileUw.GetById<State>(StateEnum.BoutTask.Formed);
                        }

                        break;
                    case "toEdit":
                        toState = MobileUw.GetById<State>(StateEnum.BoutTask.Editing);
                        break;
                    case "toArchive":
                        toState = MobileUw.GetById<State>(StateEnum.BoutTask.Archive);
                        break;
                }

                if (toState != null)
                {
                    MobileUw.WriteHistory(entity.Link, "Изменение статуса",
                        $"Статус задания изменен с {entity.State?.Name} на {toState.Name}",
                        toState.Id, entity.StateId, Security.GetCurrentAccountId());

                    entity.StateId = toState.Id;
                    MobileUw.SaveChanges();

                    ProcessTask(entity);
                }

                MobileUw.Commit();
            }


            return RedirectToAction("Manage", new {id = entity.Id});
        }
    }
}
