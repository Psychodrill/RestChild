using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
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
using RestChild.Web.Models.Gift;
using RestChild.Web.Properties;
using StateMachineEnum = RestChild.Mobile.DAL.Enum.StateMachineEnum;

namespace RestChild.Web.Controllers
{
    public class GiftController : BaseMobileController
    {
        /// <summary>
        ///     список
        /// </summary>
        public ActionResult List(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.Gift.View))
            {
                return RedirectToAvailableAction();
            }

            model = model ?? new ListModel();

            var query = PrepareListModel(model);

            model.States = MobileUw.GetSet<State>().Where(s => s.StateMachineId == StateMachineEnum.Gift)
                .OrderBy(s => s.Name).ToArray();

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = model.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var items = query
                .OrderBy(b => b.Name)
                .ThenBy(b => b.Id).Skip(startRecord).Take(pageSize)
                .ToList();

            var ids = items.Select(i => (long?) i.Id).ToArray();

            var countQuery = MobileUw.GetSet<GiftReserved>()
                .Where(r => r.StateId == StateEnum.GiftReserved.Reserved || r.StateId == StateEnum.GiftReserved.Issued)
                .Where(r => ids.Contains(r.Gift.GiftId))
                .GroupBy(g => g.GiftId).Select(v => new {v.Key, Count = v.Sum(i => i.Count)}).ToList();

            model.Counts = countQuery.ToDictionary(d => d.Key ?? 0, d => d.Count);
            model.Result = new CommonPagedList<Gift>(items, pageNumber, pageSize, totalCount);

            return View(model);
        }

        /// <summary>
        ///     выгрузка
        /// </summary>
        public ActionResult ExcelList(ListModel model)
        {
            if (!Security.HasRight(AccessRightEnum.Gift.View))
            {
                return RedirectToAvailableAction();
            }

            model = model ?? new ListModel();
            var query = PrepareListModel(model);

            model.States = MobileUw.GetSet<State>().Where(s => s.StateMachineId == StateMachineEnum.Gift)
                .OrderBy(s => s.Name).ToArray();

            var columns = new List<ExcelColumn<Gift>>
            {
                new ExcelColumn<Gift> {Title = "Название подарка", Func = t => t.Name ?? "-", Width = 72},
                new ExcelColumn<Gift> {Title = "Стоимость", Func = t => t.Price, Width = 20, HorizontalAlignment = ExcelHorizontalAlignment.Right},
                new ExcelColumn<Gift>{
                    Title = "Параметр",
                    FuncMulti = t =>  t.GiftParameters.Select(p => p.Name),
                    MultiValueColumn = true,
                    Width = 34
                },
                new ExcelColumn<Gift>
                {
                    Title = "Кол-во реализованных подарков",
                    FuncMulti = t => t.GiftParameters.Select(p =>
                        (object)(model?.Counts?.ContainsKey(p.Id) ?? false ? model.Counts[p.Id] : 0)),
                    MultiValueColumn = true,
                    Width = 34
                },
                new ExcelColumn<Gift>
                {
                    Title = "Остаток",
                    FuncMulti = t => t.GiftParameters.Select(p =>
                        (object)(p.Count - (model?.Counts?.ContainsKey(p.Id) ?? false ? model.Counts[p.Id] : 0))),
                    MultiValueColumn = true,
                    Width = 34
                },
                new ExcelColumn<Gift>
                {
                    Title = "Рейтинг",
                    Func = t => t.GiftParameters.SelectMany(p=>p.Reserved.Select(z => z.Rating)).Average(),
                    Width = 20,
                    HorizontalAlignment = ExcelHorizontalAlignment.Center
                },
                new ExcelColumn<Gift> {
                    Title = "Статус",
                    Func = t => t.State?.Name,
                    Width = 34,
                    HorizontalAlignment = ExcelHorizontalAlignment.Center
                }
            };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            var data = query.Include(c => c.GiftParameters).OrderBy(d => d.Name).ThenBy(d => d.Id).ToList();

            var ids = data.Select(d => (long?) d.Id).ToArray();

            var countQuery = MobileUw.GetSet<GiftReserved>()
                .Where(r => r.StateId == StateEnum.GiftReserved.Reserved || r.StateId == StateEnum.GiftReserved.Issued)
                .Where(r => ids.Contains(r.Gift.GiftId))
                .GroupBy(g => g.GiftId).Select(v => new {v.Key, Count = v.Sum(i => i.Count)}).ToList();

            model.Counts = countQuery.ToDictionary(d => d.Key ?? 0, d => d.Count);

            using (var excel = new ExcelTable<Gift>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Реестр подарков");

                excel.TableName = "Реестр подарков";

                string cost = null;
                if (model?.PriceFrom != null)
                {
                    cost += $" от {model?.PriceFrom}";
                }
                if (model?.PriceTo != null)
                {
                    cost += $" до {model?.PriceTo}";
                }

                string amountRemaining = null;
                if (model?.CountFrom != null)
                {
                    amountRemaining += $" от {model?.CountFrom}";
                }
                if (model?.CountTo != null)
                {
                    amountRemaining += $" до {model?.CountTo}";
                }

                excel.Parameters = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Название подарка:", model?.Name),
                        new Tuple<string, string>("Стоимость:", cost),
                        new Tuple<string, string>("Статус:", model.States?.Where(s => s.Id == model?.StateId).Select(x => x.Name).FirstOrDefault()),
                        new Tuple<string, string>("Осталось штук:", amountRemaining),
                    }
                .Where(i => !String.IsNullOrWhiteSpace(i.Item2))
                .ToList();

                excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Подарки.xlsx");
            }
        }


        /// <summary>
        ///     подготовить модель для поиска
        /// </summary>
        private IQueryable<Gift> PrepareListModel(ListModel model)
        {
            if (model.PageNumber <= 0)
            {
                model.PageNumber = 1;
            }

            var query = MobileUw.GetSet<Gift>().Where(b =>
                b.StateId != StateMachineStateEnum.Deleted);

            query = model.StateId.HasValue
                ? query.Where(q => q.StateId == model.StateId)
                : query.Where(q => q.StateId != StateEnum.Task.Archive);

            if (model.CountTo.HasValue || model.CountFrom.HasValue)
            {


                var countQuery = MobileUw.GetSet<GiftReserved>()
                    .Where(r => r.StateId == StateEnum.GiftReserved.Reserved ||
                                r.StateId == StateEnum.GiftReserved.Issued)
                    .GroupBy(g => g.GiftId).Select(v => new {v.Key, Count = v.Max(i=>i.Gift.Count) - v.Sum(i => i.Count)});

                if (model.CountTo.HasValue)
                {
                    countQuery = countQuery.Where(c => c.Count <= model.CountTo);
                }

                if (model.CountFrom.HasValue)
                {
                    countQuery = countQuery.Where(c => c.Count >= model.CountFrom);
                }

                query = query.Where(q =>
                    q.GiftParameters.Any(p => countQuery.Any(c => c.Key == p.Id)
                                              || (!p.Reserved.Any() && p.Count >= (model.CountFrom ?? p.Count) &&
                                                  p.Count <= (model.CountTo ?? p.Count))
                    ));
            }

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var t = model.Name.Trim().ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(t));
            }

            if (model.PriceFrom.HasValue)
            {
                query = query.Where(q => q.Price >= model.PriceFrom);
            }

            if (model.PriceTo.HasValue)
            {
                query = query.Where(q => q.Price <= model.PriceTo);
            }

            return query;
        }

        /// <summary>
        ///     Карточка для управления
        /// </summary>
        public ActionResult Manage(long? id)
        {
            if (!Security.HasRight(AccessRightEnum.Gift.View))
            {
                return RedirectToAvailableAction();
            }

            var entity = MobileUw.GetById<Gift>(id) ?? new Gift
            {
                StateId = StateEnum.Gift.Editing,
                State = MobileUw.GetById<State>(StateEnum.Gift.Editing)
            };

            if (entity.StateId == StateEnum.Deleted)
            {
                return RedirectToAction("List");
            }

            var canEdit = entity.StateId == StateEnum.Gift.Editing;

            var actions = new List<StateMachineAction>();

            if (entity.StateId == StateEnum.Gift.Editing)
            {
                actions.Add(new StateMachineAction
                {
                    ActionName = "Утвердить",
                    ActionCode = "toFormed",
                    Description = "Утвердить подарок?"
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

            var needRemoveButton = entity.GiftParameters?.All(g => !g.Reserved.Any()) ?? true;

            if (entity.StateId != StateEnum.Gift.Archive && !needRemoveButton)
            {
                actions.Add(new StateMachineAction
                {
                    ActionName = "Отправить в архив",
                    ActionCode = "toArchive",
                    Description = "Отправить подарок в архив?"
                });
            }

            var countQuery = MobileUw.GetSet<GiftReserved>()
                .Where(r => r.StateId == StateEnum.GiftReserved.Reserved || r.StateId == StateEnum.GiftReserved.Issued)
                .Where(r => r.Gift.GiftId == entity.Id)
                .GroupBy(g => g.GiftId).Select(v => new {v.Key, Count = v.Sum(i => i.Count)}).ToList();

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
                },
                Counts = countQuery.ToDictionary(d => d.Key ?? 0, d => d.Count)
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
            return RedirectToAction("List");
        }

        /// <summary>
        ///     разница
        /// </summary>
        private void Save(Gift source, Gift target)
        {
            source.Name = target.Name;
            source.Description = target.Description;
            source.DateStartUsing = target.DateStartUsing;
            source.Price = target.Price;
            source.NameForShop = target.NameForShop;
            SaveParams(source, target);

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
        ///     сравнение параметров
        /// </summary>
        private void SaveParams(Gift sourceEntity, Gift targetEntity)
        {
            var source = sourceEntity.GiftParameters ?? new List<GiftParameter>();
            var target = targetEntity.GiftParameters ?? new List<GiftParameter>();

            var values = target.Select(s => s.Id).ToList();

            var serviceForDelete = source.Where(s => values.All(i => i != s.Id)).ToList();
            foreach (var sForDelete in serviceForDelete)
            {
                var name = sForDelete.Name;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (!sForDelete.Reserved.Any())
                    {
                        MobileUw.Delete(sForDelete);
                    }
                }
            }

            foreach (var service in target)
            {
                var currentService = source.FirstOrDefault(s => s.Id == service.Id);
                if (currentService != null)
                {
                    var minValue = currentService.Reserved.Where(r => r.StateId == StateEnum.GiftReserved.Reserved
                                                                      || r.StateId == StateEnum.GiftReserved.Issued)
                        .Select(r => r.Count).DefaultIfEmpty().Sum();
                    if (service.Count < minValue)
                    {
                        service.Count = minValue;
                    }

                    currentService.Count = service.Count;
                }
                else
                {
                    service.GiftId = targetEntity.Id;
                    MobileUw.AddEntity(service);
                }
            }
        }

        /// <summary>
        ///     сравнение параметров
        /// </summary>
        public static string CompareParams(ICollection<GiftParameter> source, ICollection<GiftParameter> target)
        {
            var res = new StringBuilder();

            source = source ?? new List<GiftParameter>();
            target = target ?? new List<GiftParameter>();

            var values = target.Select(s => s.Id).ToList();

            var serviceForDelete = source.Where(s => values.All(i => i != s.Id)).ToList();
            foreach (var sForDelete in serviceForDelete)
            {
                var name = sForDelete.Name;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (!sForDelete.Reserved.Any())
                    {
                        res.AppendLine($"<li>{name} - удалена</li>");
                    }
                }
            }

            foreach (var service in target)
            {
                var currentService = source.FirstOrDefault(s => s.Id == service.Id);
                if (currentService != null)
                {
                    var diffSub = new StringBuilder();
                    var minValue = currentService.Reserved.Where(r => r.StateId == StateEnum.GiftReserved.Reserved
                                                                      || r.StateId == StateEnum.GiftReserved.Issued)
                        .Select(r => r.Count).DefaultIfEmpty().Sum();
                    if (service.Count < minValue)
                    {
                        service.Count = minValue;
                    }

                    diffSub.Compare(currentService, service, "Изменено количество", e => e.Count);
                    var diff = diffSub.ToString();
                    if (!string.IsNullOrWhiteSpace(diff))
                    {
                        var name = currentService.Name;
                        res.AppendLine($"<li>'{name}' - изменен <ul>{diff}</ul></li>");
                    }
                }
                else
                {
                    var name = service.Name;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        res.AppendLine($"<li>'{name}' - добавлен</li>");
                    }
                }
            }

            return res.ToString();
        }

        /// <summary>
        ///     разница
        /// </summary>
        private string GetDiff(Gift source, Gift target)
        {
            var sb = new StringBuilder();
            sb.Compare(source, target, "Изменено название подарка", e => e.Name);
            sb.Compare(source, target, "Изменено описание подарка", e => e.Description);
            sb.Compare(source, target, "Изменена стоимость подарка", e => e.Price, e => e.Price.FormatEx());
            sb.Compare(source, target, "Изменена дата начала использования после утверждения", e => e.DateStartUsing,
                e => e.DateStartUsing.FormatEx());
            sb.Compare(source, target, "Изменено название подарка для магазина", e => e.NameForShop);

            var parametersDiff = CompareParams(source.GiftParameters, target.GiftParameters);

            if (!string.IsNullOrWhiteSpace(parametersDiff))
            {
                sb.AppendLine($"<li>Изменены параметры подарка<ul>{parametersDiff}</ul></li>");
            }

            var file = target.Link?.Files?.FirstOrDefault();
            if (file != null)
            {
                sb.AppendLine($"<li>Добавлено фото подарка {file.FileName}</li>");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     проверить подарок
        /// </summary>
        private List<string> CheckGift(Gift entity)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.Add("Не указано название подарка");
            }

            if (entity.Price <= 0)
            {
                result.Add("Не указана стоимость подарка");
            }

            if (!(entity.Link?.Files?.Any() ?? false))
            {
                result.Add("Не добавлена фотография подарка");
            }

            if (!entity.GiftParameters.Any() || entity.GiftParameters.All(p => p.Count == 0))
            {
                result.Add("Не указаны параметры для подарка. Необходимо указать хотя бы один");
            }


            return result;
        }

        /// <summary>
        ///     сохранение карточки
        /// </summary>
        [HttpPost]
        public ActionResult Save(ManageModel model)
        {
            if (!Security.HasRight(AccessRightEnum.Gift.View))
            {
                return RedirectToAvailableAction();
            }

            var data = model?.BuildData();

            if (data == null)
            {
                return RedirectToAction("List");
            }

            var entity = MobileUw.GetById<Gift>(data.Id);

            if (entity == null)
            {
                entity = MobileUw.AddEntity(data);
                var link = MobileUw.WriteHistory(entity.Link, "Сохранение", "Добавление подарка");
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

                if (entity.StateId == StateEnum.Gift.Editing)
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
            entity = MobileUw.GetById<Gift>(id);

            if (!string.IsNullOrWhiteSpace(model.StateMachineActionString))
            {
                State toState = null;
                switch (model.StateMachineActionString)
                {
                    case "Delete":
                        toState = MobileUw.GetById<State>(StateEnum.Deleted);
                        break;
                    case "toFormed":
                        var errors = CheckGift(entity);

                        if (errors.Any())
                        {
                            SetErrors(errors);
                        }
                        else
                        {
                            toState = MobileUw.GetById<State>(StateEnum.Gift.Formed);
                        }

                        break;
                    case "toEdit":
                        toState = MobileUw.GetById<State>(StateEnum.Gift.Editing);
                        break;
                    case "toArchive":
                        toState = MobileUw.GetById<State>(StateEnum.Gift.Archive);
                        break;
                }

                if (toState != null)
                {
                    MobileUw.WriteHistory(entity.Link, "Изменение статуса",
                        $"Статус подарка изменен с {entity.State?.Name} на {toState.Name}",
                        toState.Id, entity.StateId, Security.GetCurrentAccountId());

                    entity.StateId = toState.Id;
                    MobileUw.SaveChanges();
                }
            }

            return RedirectToAction("Manage", new {id = entity.Id});
        }
    }
}
