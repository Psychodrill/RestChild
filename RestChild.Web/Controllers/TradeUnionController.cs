using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Castle.Core.Internal;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration.Filters;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Business.Export;
using RestChild.Web.Models.TradeUnion;
using DocumentType = RestChild.Domain.DocumentType;
using Settings = RestChild.Web.Properties.Settings;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     списки профсоюзов
    /// </summary>
    public class TradeUnionController : BaseController
    {
        public WebRestYearController ApiRestYearController { get; set; }

        public StateController ApiStateController { get; set; }

        public WebVocabularyController VocController { get; set; }

        public WebBtiDistrictsController DistrictController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRestYearController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
            VocController.SetUnitOfWorkInRefClass(UnitOfWork);
            DistrictController.SetUnitOfWorkInRefClass(UnitOfWork);
        }

        /// <summary>
        ///     подговтока таблицы для выгрузки в Excel
        /// </summary>
        private static BaseExcelTable GetTradeUnionExcelTable(TradeUnionList list)
        {
            var param = new List<Tuple<string, string>>
            {
                //new Tuple<string, string>("Наименование", list.Name),
                new Tuple<string, string>("Год кампании", list?.YearOfRest?.Name),
                new Tuple<string, string>("Лагерь", list?.Camp?.Name),
                new Tuple<string, string>("Профсоюз", list?.TradeUnion?.Name),
                new Tuple<string, string>("Смена", list?.GroupedTimeOfRest?.Name),
                new Tuple<string, string>("Дата с", list?.DateFrom.FormatEx()),
                new Tuple<string, string>("Дата по", list?.DateTo.FormatEx())
            };

            var firstHeaderRow = new List<ExcelHeader<TradeUnionCamper>>
            {
                new ExcelHeader<TradeUnionCamper> {Title = "Сведения о ребёнке", Column = 1, ColSpan = 14},
                new ExcelHeader<TradeUnionCamper> {Title = "Сведения о родителе", Column = 15, ColSpan = 7},
                new ExcelHeader<TradeUnionCamper>
                    {Title = "Сведения о родственнике-члене профсоюза", Column = 22, ColSpan = 8},
                new ExcelHeader<TradeUnionCamper> {Title = "Сведения о стоимости путевки", Column = 30, ColSpan = 5},
                new ExcelHeader<TradeUnionCamper> {Title = "Заехал", Column = 35, RowSpan = 2}
            };
            var secondHeaderRow = new List<ExcelHeader<TradeUnionCamper>>
            {
                new ExcelHeader<TradeUnionCamper> {Title = "Фамилия", Column = 1, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Имя", Column = 2, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Отчество", Column = 3, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Пол", Column = 4, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Дата рождения", Column = 5, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Место рождения", Column = 6, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Адрес регистрации", Column = 7, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "СНИЛС", Column = 8, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Обр. учреждение", Column = 9, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Вид документа", Column = 10, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Серия документа", Column = 11, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Номер документа", Column = 12, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper>
                    {Title = "Дата выдачи документа", Column = 13, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Кем выдан документ", Column = 14, ColSpan = 1, RowSpan = 1},

                new ExcelHeader<TradeUnionCamper> {Title = "Фамилия", Column = 15, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Имя", Column = 16, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Отчество", Column = 17, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Email", Column = 18, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Место работы", Column = 19, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Телефон", Column = 20, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Член профсоюза", Column = 21, ColSpan = 1, RowSpan = 1},

                new ExcelHeader<TradeUnionCamper>
                    {Title = "Родственник-член профсоюза", Column = 22, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper>
                    {Title = "Статус по отношению к ребёнку", Column = 23, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Фамилия", Column = 24, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Имя", Column = 25, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Отчество", Column = 26, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Email", Column = 27, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Место работы", Column = 28, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Телефон", Column = 29, ColSpan = 1, RowSpan = 1},

                new ExcelHeader<TradeUnionCamper> {Title = "Полная", Column = 30, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Средства родителей", Column = 31, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Средства профсоюза", Column = 32, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper> {Title = "Бюджетные средства", Column = 33, ColSpan = 1, RowSpan = 1},
                new ExcelHeader<TradeUnionCamper>
                    {Title = "Средства предприятия", Column = 34, ColSpan = 1, RowSpan = 1}
            };

            var columns = new List<ExcelColumn<TradeUnionCamper>>
            {
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.LastName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.FirstName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.MiddleName},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "", Func = r => r?.Child?.Male.FormatEx("Мужской", "Женский")},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.DateOfBirth},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.PlaceOfBirth},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "", Func = r => r?.Child?.Address?.ToString() ?? r?.AddressChild},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.Snils},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.SelectedSchool?.Name ?? r?.School},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.DocumentType?.Name},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.DocumentSeria},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.DocumentNumber},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.DocumentDateOfIssue},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Child?.DocumentSubjectIssue},

                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Parent?.LastName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Parent?.FirstName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Parent?.MiddleName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Parent?.Email},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.ParentPlaceWork},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Parent?.Phone},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.IsParentUnionist.FormatEx()},

                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.IsRelativeUnionist.FormatEx()},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.TradeUnionStatusByChild?.Name},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Unionist?.LastName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Unionist?.FirstName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Unionist?.MiddleName},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Unionist?.Email},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.RelativePlaceWork},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Unionist?.Phone},

                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.Summa},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.SummaParent},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.SummaTradeUnion},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.SummaBudget},
                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.SummaOrganization},

                new ExcelColumn<TradeUnionCamper> {Title = "", Func = r => r?.IsChecked.FormatEx()}
            };

            return new ExcelTable<TradeUnionCamper>(columns, new List<List<ExcelHeader<TradeUnionCamper>>>
            {
                firstHeaderRow,
                secondHeaderRow
            }, list?.Campers ?? new List<TradeUnionCamper>())
            {
                TableName = "Список",
                Parameters = param
            };
        }

        /// <summary>
        ///     выгрузка в Word
        /// </summary>
        public ActionResult Word(long id, bool cameChildren = false)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return RedirectToAvalibleAction();
            }

            var result = DocumentGeneration.WordProcessor.TradeUnionWord(UnitOfWork, new TradeUnionWordFilter
            {
                TradeUnionId = id,
                CameChildren = cameChildren
            });

            return File(result.FileBody, result.MimeType, result.FileName);
        }

        /// <summary>
        ///     выгрузка в Excel
        /// </summary>
        public ActionResult Excel(long id)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return RedirectToAvalibleAction();
            }

            var table = GetTradeUnionExcelTable(UnitOfWork.GetById<TradeUnionList>(id));

            table.DataBind("Список", ExcelBorderStyle.Thin);
            var fileStream = table.CreateExcel();

            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Список.xlsx");
        }

        /// <summary>
        ///     поиск списков
        /// </summary>
        public ActionResult List(TradeUnionSearch search)
        {
            SetUnitOfWorkInRefClass();

            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return RedirectToAvalibleAction();
            }

            search = search ?? new TradeUnionSearch();
            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = search.PageNumber <= 0 ? 1 : search.PageNumber;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            FillTradeUnionSearch(search, pageNumber, pageSize);

            return View(search);
        }

        /// <summary>
        ///     редактирование списка.
        /// </summary>
        public ActionResult Edit(long? id, string activeTab)
        {
            SetUnitOfWorkInRefClass();

            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return RedirectToAvalibleAction();
            }

            var entity = UnitOfWork.GetById<TradeUnionList>(id) ?? new TradeUnionList
            {
                StateId = StateMachineStateEnum.TradeUnion.Edit,
                State = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.TradeUnion.Edit)
            };

            if (entity.IsCashbackUse)
            {
                return RedirectToAvalibleAction();
            }

            var actions = entity.Id == 0
                ? new List<StateMachineAction>()
                : ApiStateController.GetActions(entity.State, StateMachineEnum.TradeUnionList);

            var isEditable = entity.StateId == StateMachineStateEnum.TradeUnion.Edit &&
                             Security.HasRight(AccessRightEnum.TradeUnionList.Edit);

            var isIncomeFlag = entity.StateId == StateMachineStateEnum.TradeUnion.Approved &&
                               Security.HasRight(AccessRightEnum.TradeUnionList.ToFinish);

            var model = new TradeUnionModel(entity)
            {
                ActiveTab = activeTab,
                IsEditable = isEditable,
                IsIncomeFlag = isIncomeFlag,
                State = new ViewModelState
                {
                    CanReturn = true,
                    Actions = actions,
                    NeedSaveButton = isEditable || isIncomeFlag,
                    State = entity.State,
                    FormSelector = "#requestForm",
                    ActionSelector = "#StateMachineActionString",
                    ReturnController = "CommercialTour",
                    ReturnAction = "List",
                    NeedRemoveButton = isEditable,
                    JsFunctionToAction = "confirmStateButtonTradeUnion",
                    CommentSelector = "#CommentToDeclined",
                    ActionWithComment = new List<string> {AccessRightEnum.TradeUnionList.ToDeclined}
                },
                YearOfRests = ApiRestYearController.Get().ToList(),
                TimeOfRests = UnitOfWork.GetSet<GroupedTimeOfRest>().Where(s => s.IsActive).OrderBy(s => s.Id).ToList()
            };

            if (!Security.HasRight(AccessRightEnum.TradeUnionList.View))
            {
                var orgs = AccessRightEnum.TradeUnionList.View.GetSecurityOrganiztion();

                var organizations = UnitOfWork.GetSet<Organization>().Where(o => !o.IsDeleted && orgs.Contains(o.Id))
                    .ToList();
                if (model.Data.Id == 0)
                {
                    if (organizations.Count(o => o.IsTradeUnion) == 1)
                    {
                        model.Data.TradeUnion = organizations.FirstOrDefault(o => o.IsTradeUnion);
                        model.Data.TradeUnionId = model.Data.TradeUnion?.Id;
                        model.OnlyOneTradeUnion = true;
                    }

                    if (organizations.Count(o => o.IsHotel) == 1)
                    {
                        model.Data.Camp = organizations.FirstOrDefault(o => o.IsHotel);
                        model.Data.CampId = model.Data.Camp?.Id;
                        model.OnlyOneCamp = true;
                    }
                }
                else if (!orgs.Contains(model.Data.CampId) && !orgs.Contains(model.Data.TradeUnionId))
                {
                    return RedirectToAction("List");
                }
                else
                {
                    if (organizations.Count(o => o.IsTradeUnion) == 1)
                    {
                        model.OnlyOneTradeUnion = true;
                    }

                    if (organizations.Count(o => o.IsHotel) == 1)
                    {
                        model.OnlyOneCamp = true;
                    }
                }
            }

            model.Data.YearOfRestId = model.Data.YearOfRestId ?? model.YearOfRests
                .Where(y => y.Year == DateTime.Now.Year).Select(y => y.Id).FirstOrDefault();
            model.DocumentTypes =
                UnitOfWork.GetSet<DocumentType>().Where(d => d.ForChild && !d.ForForeign).OrderBy(d => d.Name).ToList();
            model.StatusByChild =
                UnitOfWork.GetSet<TradeUnionStatusByChild>().OrderBy(d => d.Name).ToList();

            if (model.Data.Id > 0)
            {
                model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Печать",
                    IconClass = "glyphicon-print",
                    Controller = "TradeUnion",
                    Action = "Excel",
                    ActionParameters = new {id = model.Data.Id}
                });

                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Перечень информации о детях",
                    IconClass = "glyphicon-print",
                    Controller = "TradeUnion",
                    Action = "Word",
                    ActionParameters = new {id = model.Data.Id}
                });

                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Перечень информации о детях (заехавшие)",
                    IconClass = "glyphicon-print",
                    Controller = "TradeUnion",
                    Action = "Word",
                    ActionParameters = new {id = model.Data.Id, cameChildren = true}
                });
            }

            if (entity.HistoryLinkId.HasValue)
            {
                model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "История",
                    ButtonClass = "btn btn-default btn-hystory-link",
                    SomeAddon = $"data-history-id=\"{entity.HistoryLinkId}\""
                });
            }

            ViewBag.Districts = DistrictController.Get()
                .InsertAt(new BtiDistrict {Id = 0, Name = "-- Не выбрано --"});

            ViewBag.Regions = VocController.GetRegions().OrderBy(p => p.Id)
                .InsertAt(new BtiRegion {Id = 0, Name = "-- Не выбрано --"});


            model.DoubleChildren = new List<Person>();
            if (model.Data.Id > 0)
            {
                foreach (var camper in model.Data.Campers)
                {
                    if (camper.Child.PersonCheck.Any(pc => pc.PersonCheckResults.Any()))
                    {
                        var camps = camper.Child.PersonCheck.SelectMany(sx => sx.PersonCheckResults).ToList();
                        model.DoubleChildren.AddRange(camps);
                    }

                    if (camper.Child.PersonCheckResults.Any(ss => ss.IsProcessed && !ss.NotActual))
                    {
                        var personIds = new List<long>();
                        foreach (var pcr in camper.Child.PersonCheckResults.Where(ss => ss.IsProcessed && !ss.NotActual)
                            .ToList())
                        {
                            personIds.Add(pcr.PersonId.Value);
                            personIds.AddRange(pcr.PersonCheckResults.Select(ss => ss.Id).ToList());
                        }

                        var persIds = personIds.Distinct().Where(sx =>
                            sx != camper.ChildId && model.DoubleChildren.All(sa => sa.Id != sx)).ToList();

                        var persons = UnitOfWork.GetSet<Person>().Where(ss => persIds
                            .Any(sx => ss.Id == sx)).ToList();

                        model.DoubleChildren.AddRange(persons);
                    }
                }
            }

            return View(model);
        }

        /// <summary>
        ///     заглушка для того что бы не падало
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Save()
        {
            SetUnitOfWorkInRefClass();
            return RedirectToAction("List");
        }

        /// <summary>
        ///     удалить ребёнка
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> DeleteChild(long id)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return Content("Нет прав для удаления отдыхающего");
            }

            var entity = await UnitOfWork.GetByIdAsync<TradeUnionCamper>(id, CancellationToken.None);

            this.WriteHistory(entity.TradeUnion.HistoryLink, "Удаление ребёнка",
                $"Удалён ребёнок {GetChildName(UnitOfWork, entity)}");

            DeleteCamper(UnitOfWork, entity);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            return Content(string.Empty);
        }

        /// <summary>
        ///     сохранить ребёнка
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> SaveChild(string child)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return RedirectToAvalibleAction();
            }

            var data = JsonConvert.DeserializeObject<TradeUnionCamperModel>(child)?.BuildEntity();

            if (data == null || (data.Id == 0 && !data.TradeUnionId.HasValue))
            {
                return Content(string.Empty);
            }

            var tradeUnion = await UnitOfWork.GetByIdAsync<TradeUnionList>(data.TradeUnionId, CancellationToken.None);

            if (tradeUnion == null)
            {
                return Content(string.Empty);
            }

            var persisted = tradeUnion.Campers.FirstOrDefault(c => c.Id == data.Id);
            var diff = persisted == null ? "" : GetChildDiff(UnitOfWork, persisted, data);

            if (persisted != null && string.IsNullOrWhiteSpace(diff))
            {
                UnitOfWork.DetachAllEntitys();
                persisted = await UnitOfWork.GetByIdAsync<TradeUnionCamper>(data.Id, CancellationToken.None);
                return Content(JsonConvert.SerializeObject(new TradeUnionCamperModel(persisted)));
            }

            diff = persisted == null
                ? $"<ul><li>Добавлен ребёнок: {GetChildName(UnitOfWork, data)}</li></ul>"
                : $"<ul><li>Изменен ребёнок: {GetChildName(UnitOfWork, persisted)} <ul>{diff}</ul></li></ul>";

            tradeUnion.HistoryLink = this.WriteHistory(tradeUnion.HistoryLink, "Сохранение списка",
                diff);

            persisted = SaveCamper(UnitOfWork, persisted, data);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);
            UnitOfWork.DetachAllEntitys();
            persisted = await UnitOfWork.GetByIdAsync<TradeUnionCamper>(data.Id, CancellationToken.None);
            return Content(JsonConvert.SerializeObject(new TradeUnionCamperModel(persisted)));
        }

        /// <summary>
        ///     сохранение карточки списка.
        /// </summary>
        [HttpPost]
        public ActionResult Save(TradeUnionModel model)
        {
            SetUnitOfWorkInRefClass();

            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionList.View}))
            {
                return RedirectToAvalibleAction();
            }

            var isEditable = model?.Data?.StateId == StateMachineStateEnum.TradeUnion.Edit &&
                             Security.HasRight(AccessRightEnum.TradeUnionList.Edit);

            var isIncomeFlag = model?.Data?.StateId == StateMachineStateEnum.TradeUnion.Approved &&
                               Security.HasRight(AccessRightEnum.TradeUnionList.ToFinish);

            if (model?.Data == null)
            {
                if (model?.Data?.Id > 0)
                {
                    return RedirectToAction("Edit", new {id = model.Data.Id});
                }

                return RedirectToAction("List");
            }

            var entity = model.BuildData();

            if (entity.LastUpdateTick != UnitOfWork.GetLastUpdateTickById<TradeUnionList>(entity.Id))
            {
                SetRedicted();
                return RedirectToAction("Edit", new {id = entity.Id});
            }

            if (entity.GroupedTimeOfRestId == 0)
            {
                entity.GroupedTimeOfRestId = null;
            }

            if (entity.Id == 0)
            {
                // Сохранение адресов
                if (entity.Campers?.Count > 0)
                {
                    foreach (var camper in entity.Campers)
                    {
                        if (camper.Child != null)
                        {
                            AddressPrepareForSave(camper.Child, UnitOfWork);
                        }
                    }
                }

                entity = UnitOfWork.AddEntity(entity);
                entity.HistoryLink = this.WriteHistory(entity.HistoryLink, "Первое сохранение списка", "");
                entity.HistoryLinkId = entity.HistoryLink?.Id;

                if (entity.Campers?.Count > 0)
                {
                    foreach (var camper in entity.Campers)
                    {
                        SetPersonToDoubleCheck(UnitOfWork, camper);
                    }
                }

                UnitOfWork.SaveChanges();
            }
            else
            {
                var persisted = UnitOfWork.GetById<TradeUnionList>(entity.Id);

                if (isEditable)
                {
                    persisted.HistoryLink = this.WriteHistory(persisted.HistoryLink, "Сохранение списка",
                        GetDiff(UnitOfWork, entity, persisted));
                    persisted.LastUpdateTick = DateTime.Now.Ticks;
                    persisted.HistoryLinkId = persisted.HistoryLink?.Id;
                    persisted.CopyEntity(entity);
                    //UpdateChilds(persisted, entity);
                    UnitOfWork.SaveChanges();
                }
                else if (isIncomeFlag)
                {
                    persisted.HistoryLink = this.WriteHistory(persisted.HistoryLink, "Сохранение списка",
                        $"<ul>{GetChildrenDiff(persisted, entity)}</ul>");
                    persisted.HistoryLinkId = persisted.HistoryLink?.Id;
                    persisted.LastUpdateTick = DateTime.Now.Ticks;
                    UpdateChildrenChecked(persisted, entity);
                    UnitOfWork.SaveChanges();
                }

                if (!string.IsNullOrWhiteSpace(model.StateMachineActionString))
                {
                    if (model.StateMachineActionString == AccessRightEnum.TradeUnionList.ToOnAproving &&
                        (entity.Campers == null || !entity.Campers.Any()))
                    {
                        SetErrors(new List<string> {"Нельзя отправить список на утверждение без детей"});
                        return RedirectToAction("Edit", new {id = entity.Id});
                    }

                    var stateId = ChangeState(persisted.Id, model.StateMachineActionString, model.CommentToDeclined);
                    if (stateId == StateMachineStateEnum.Deleted)
                    {
                        return RedirectToAction("List");
                    }
                }
            }

            return RedirectToAction("Edit", new {id = entity.Id});
        }

        /// <summary>
        ///     Экспорт в Эксель результатов поиска профсоюзных списков, вместе с параметрами поиска.
        /// </summary>
        public ActionResult ExportTradeUnionListToExcel(TradeUnionSearch search)
        {
            search = search ?? new TradeUnionSearch();
            const int pageSize = 1000;
            var pageNumber = search.PageNumber <= 0 ? 1 : search.PageNumber;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            var file = ExportTradeUnionListToExcelFile(search, pageNumber, pageSize);
            if (search.Result.TotalItemCount <= pageSize)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    return FileAndDeleteOnClose(file,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Списки профсоюзов.xlsx");
                }
            }
            else
            {
                var files = new List<string> {file};
                var exportetRowsCount = search.Result.Count;
                while (exportetRowsCount < search.Result.TotalItemCount)
                {
                    var fn = ExportTradeUnionListToExcelFile(search, ++pageNumber, pageSize);
                    if (!string.IsNullOrWhiteSpace(fn))
                    {
                        files.Add(fn);
                    }

                    exportetRowsCount += search.Result.Count;
                }

                var tempFile = FirstRequestCompanyController.UnionFilesToZip(files, "Списки профсоюзов");

                return FileAndDeleteOnClose(tempFile, "application/zip", "Списки профсоюзов.zip");
            }

            return null;
        }

        /// <summary>
        ///     изменение статуса
        /// </summary>
        private long? ChangeState(long id, string stateMachineActionString, string commentToDeclined = null)
        {
            var list = UnitOfWork.GetById<TradeUnionList>(id);
            if (list == null)
            {
                return null;
            }

            using (var transaction = UnitOfWork.GetTransactionScope())
            {
                if (stateMachineActionString == "Delete")
                {
                    list.HistoryLink = ApiStateController.WriteHistory(list.HistoryLink, "Удаление списка",
                        string.Empty, StateMachineStateEnum.Deleted, list.StateId);
                    list.HistoryLinkId = list.HistoryLink?.Id;
                    list.StateId = StateMachineStateEnum.Deleted;
                    UnitOfWork.SaveChanges();
                    transaction.Complete();
                    return list.StateId;
                }

                if (!Security.HasRight(stateMachineActionString))
                {
                    SetErrors(new[] {"У вас нет прав для изменения статуса списка"});
                    return null;
                }

                var action = ApiStateController.GetAction(stateMachineActionString);
                if (action?.ToStateId != null)
                {
                    list.HistoryLink = ApiStateController.WriteHistory(list.HistoryLink,
                        $"Изменение статуса списка с \"{list.State?.Name}\" на \"{action.ToState?.Name}\"",
                        commentToDeclined, action.ToStateId, list.StateId);
                    list.HistoryLinkId = list.HistoryLink?.Id;
                    list.StateId = action.ToStateId;
                }

                UnitOfWork.SaveChanges();

                transaction.Complete();
            }

            return list.StateId;
        }

        /// <summary>
        ///     удаление ребёнка
        /// </summary>
        internal static void DeleteCamper(IUnitOfWork UnitOfWork, TradeUnionCamper camper)
        {
            if (camper.Child != null)
            {
                if (camper.Child.Address != null && camper.Child.Address.Id > 0)
                {
                    UnitOfWork.GetSet<Address>().Remove(camper.Child.Address);
                }

                var query = UnitOfWork.GetSet<TradeUnionPersonCheck>()
                    .Where(t => t.PersonId == camper.ChildId ||
                                t.PersonCheckResults.Any(x => x.Id == camper.ChildId)).ToList();
                if (query.Any())
                {
                    foreach (var t in query)
                    {
                        UnitOfWork.GetSet<TradeUnionPersonCheck>().Remove(t);
                    }
                }

                UnitOfWork.Delete(camper.Child);
                camper.Child = null;
                camper.ChildId = null;
            }

            if (camper.Parent != null)
            {
                UnitOfWork.Delete(camper.Parent);
                camper.Parent = null;
                camper.ParentId = null;
            }

            if (camper.Unionist != null)
            {
                UnitOfWork.Delete(camper.Unionist);
                camper.Unionist = null;
                camper.UnionistId = null;
            }

            UnitOfWork.Delete(camper);
        }

        /// <summary>
        /// сохранение ребёнка
        /// </summary>
        internal static TradeUnionCamper SaveCamper(IUnitOfWork UnitOfWork, TradeUnionCamper entity, TradeUnionCamper camper, bool setDCheck = true)
        {
            if (camper.TradeUnionStatusByChildId <= 0)
            {
                camper.TradeUnionStatusByChildId = null;
            }

            if (camper.SelectedSchoolId == 0)
            {
                camper.SelectedSchoolId = null;
            }

            camper.LastUpdateTick = DateTime.Now.Ticks;

            // Сохранение адресов
            if (camper.Child != null)
            {
                AddressPrepareForSave(camper.Child, UnitOfWork);
            }

            if (camper.Id == 0)
            {
                var c = UnitOfWork.AddEntity(camper, false);
                if (setDCheck)
                {
                    SetPersonToDoubleCheck(UnitOfWork, c);
                }

                return c;
            }

            entity?.CopyEntity(camper);
            entity?.Child?.CopyEntity(camper.Child);
            entity?.Unionist?.CopyEntity(camper.Unionist);
            entity?.Parent?.CopyEntity(camper.Parent);

            if (setDCheck)
            {
                //запустить проверку на дубли
                SetPersonToDoubleCheck(UnitOfWork, camper);
            }

            return entity;
        }

        /// <summary>
        ///     сохранение размещения.
        /// </summary>
        private void UpdateChildrenChecked(TradeUnionList persited, TradeUnionList entity)
        {
            foreach (var camper in entity.Campers)
            {
                var saved = persited.Campers.FirstOrDefault(a => a.Id == camper.Id);
                if (saved != null)
                {
                    saved.IsChecked = camper.IsChecked;
                }
            }
        }

        public static string GetChildName(IUnitOfWork UnitOfWork, TradeUnionCamper camper)
        {
            if (camper == null)
            {
                return string.Empty;
            }

            var docTypeName = camper.Child?.DocumentType?.Name ??
                              UnitOfWork.GetById<DocumentType>(camper.Child?.DocumentTypeId)?.Name;

            return
                $"{camper.Child?.LastName.FormatEx()} {camper.Child?.FirstName} {camper.Child?.MiddleName}, {camper.Child?.DateOfBirth.FormatEx()}, {docTypeName} {camper.Child?.DocumentSeria} {camper.Child?.DocumentNumber}";
        }

        /// <summary>
        ///     сохранение размещения.
        /// </summary>
        private string GetChildrenDiff(TradeUnionList persited, TradeUnionList entity)
        {
            var ticketsIds = entity.Campers.Select(a => a.Id).ToList();
            var res = new StringBuilder();

            foreach (var camper in persited.Campers.Where(a => !ticketsIds.Contains(a.Id)).ToList())
            {
                res.AppendLine(
                    $"<li>Удален ребёнок: {GetChildName(UnitOfWork, camper)}</li>");
            }

            foreach (var camper in entity.Campers)
            {
                camper.TradeUnionId = persited.Id;
                camper.LastUpdateTick = DateTime.Now.Ticks;

                if (camper.Id == 0)
                {
                    res.AppendLine(
                        $"<li>Добавлен ребёнок: {GetChildName(UnitOfWork, camper)}</li>");
                }
                else
                {
                    var saved = persited.Campers.FirstOrDefault(a => a.Id == camper.Id);
                    if (saved != null)
                    {
                        var ss = GetChildDiff(UnitOfWork, saved, camper);
                        if (!string.IsNullOrWhiteSpace(ss))
                        {
                            res.AppendLine(
                                $"<li>Изменен ребёнок: {GetChildName(UnitOfWork, saved)} <ul>{ss}</ul></li>");
                        }
                    }
                }
            }

            return res.ToString();
        }

        /// <summary>
        /// получить различия в ребёнке
        /// </summary>
        internal static string GetChildDiff(IUnitOfWork UnitOfWork, TradeUnionCamper entity, TradeUnionCamper camper)
        {
            var subRes = new StringBuilder();

            if (entity.Child?.LastName != camper.Child?.LastName)
            {
                subRes.AppendLine(
                    $"<li>Изменена фамилия ребёнка старое значение:'{entity.Child?.LastName}', новое значение:'{camper.Child?.LastName}'</li>");
            }

            if (entity.Child?.FirstName != camper.Child?.FirstName)
            {
                subRes.AppendLine(
                    $"<li>Изменено имя ребёнка старое значение:'{entity.Child?.FirstName}', новое значение:'{camper.Child?.FirstName}'</li>");
            }

            if (entity.Child?.MiddleName != camper.Child?.MiddleName)
            {
                subRes.AppendLine(
                    $"<li>Изменено отчество ребёнка старое значение:'{entity.Child?.MiddleName}', новое значение:'{camper.Child?.MiddleName}'</li>");
            }

            if (entity.Child?.Male != camper.Child?.Male)
            {
                subRes.AppendLine(
                    $"<li>Изменен пол ребёнка старое значение:'{(entity.Child?.Male ?? false ? "мужской" : "женский")}', новое значение:'{(camper.Child?.Male ?? false ? "мужской" : "женский")}'</li>");
            }

            if (entity.Child?.DateOfBirth != camper.Child?.DateOfBirth)
            {
                subRes.AppendLine(
                    $"<li>Изменена дата рождения ребёнка старое значение:'{entity.Child?.DateOfBirth.FormatEx()}', новое значение:'{camper.Child?.DateOfBirth.FormatEx()}'</li>");
            }

            if (entity.Child?.PlaceOfBirth != camper.Child?.PlaceOfBirth)
            {
                subRes.AppendLine(
                    $"<li>Изменено место рождения ребёнка старое значение:'{entity.Child?.PlaceOfBirth}', новое значение:'{camper.Child?.PlaceOfBirth}'</li>");
            }

            if (entity.Child?.DocumentTypeId != camper.Child?.DocumentTypeId)
            {
                var docTypeName = camper.Child?.DocumentType?.Name ??
                                  UnitOfWork.GetById<DocumentType>(camper.Child?.DocumentTypeId)?.Name;
                subRes.AppendLine(
                    $"<li>Изменен вид документа ребёнка старое значение:'{entity.Child?.DocumentType?.Name}', новое значение:'{docTypeName}'</li>");
            }

            if (entity.Child?.DocumentSeria != camper.Child?.DocumentSeria)
            {
                subRes.AppendLine(
                    $"<li>Изменена серия документа ребёнка старое значение:'{entity.Child?.DocumentSeria}', новое значение:'{camper.Child?.DocumentSeria}'</li>");
            }

            if (entity.Child?.DocumentNumber != camper.Child?.DocumentNumber)
            {
                subRes.AppendLine(
                    $"<li>Изменен номер документа ребёнка старое значение:'{entity.Child?.DocumentNumber}', новое значение:'{camper.Child?.DocumentNumber}'</li>");
            }

            if (entity.Child?.DocumentSubjectIssue != camper.Child?.DocumentSubjectIssue)
            {
                subRes.AppendLine(
                    $"<li>Изменен кем выдан документ ребёнка старое значение:'{entity.Child?.DocumentSubjectIssue}', новое значение:'{camper.Child?.DocumentSubjectIssue}'</li>");
            }

            if (entity.Child?.DocumentDateOfIssue != camper.Child?.DocumentDateOfIssue)
            {
                subRes.AppendLine(
                    $"<li>Изменен когда выдан документ ребёнка старое значение:'{entity.Child?.DocumentDateOfIssue.FormatEx()}', новое значение:'{camper.Child?.DocumentDateOfIssue.FormatEx()}'</li>");
            }

            var savedAddress = entity.Child?.Address?.ToString() ?? entity.AddressChild;
            if (camper.Child?.Address?.BtiAddress?.Id == 0)
            {
                camper.Child.Address.BtiAddress = null;
            }

            var changedAddress = camper.Child?.AddressId == null
                ? camper.AddressChild
                : camper.Child?.Address?.ToString() ?? camper.AddressChild;
            if (savedAddress != changedAddress)
            {
                subRes.AppendLine(
                    $"<li>Изменен адрес регистрации ребёнка старое значение:'{savedAddress}', новое значение:'{changedAddress}'</li>");
            }

            if (entity.SelectedSchoolId != camper.SelectedSchoolId)
            {
                var schoolName = camper.SelectedSchool?.Name ??
                                 UnitOfWork.GetById<School>(camper.SelectedSchoolId)?.Name;

                subRes.AppendLine(
                    $"<li>Изменено образовательное учреждение ребёнка старое значение:'{entity.SelectedSchool?.Name.FormatEx()}', новое значение:'{schoolName.FormatEx()}'</li>");
            }

            if (entity.IsScoolNotPresent != camper.IsScoolNotPresent)
            {
                subRes.AppendLine(
                    $"<li>Изменено учреждения нет в списке старое значение:'{entity.IsScoolNotPresent.FormatEx()}', новое значение:'{camper.IsScoolNotPresent.FormatEx()}'</li>");
            }

            if (entity.School != camper.School)
            {
                subRes.AppendLine(
                    $"<li>Изменено иное образовательное учреждение ребёнка старое значение:'{entity.School}', новое значение:'{camper.School}'</li>");
            }

            if (entity.Parent?.LastName != camper.Parent?.LastName)
            {
                subRes.AppendLine(
                    $"<li>Изменена фамилия родителя старое значение:'{entity.Parent?.LastName}', новое значение:'{camper.Parent?.LastName}'</li>");
            }

            if (entity.Parent?.FirstName != camper.Parent?.FirstName)
            {
                subRes.AppendLine(
                    $"<li>Изменено имя родителя старое значение:'{entity.Parent?.FirstName}', новое значение:'{camper.Parent?.FirstName}'</li>");
            }

            if (entity.Parent?.MiddleName != camper.Parent?.MiddleName)
            {
                subRes.AppendLine(
                    $"<li>Изменено отчество родителя старое значение:'{entity.Parent?.MiddleName}', новое значение:'{camper.Parent?.MiddleName}'</li>");
            }

            if (entity.Parent?.Phone != camper.Parent?.Phone)
            {
                subRes.AppendLine(
                    $"<li>Изменен телефон родителя старое значение:'{entity.Parent?.Phone}', новое значение:'{camper.Parent?.Phone}'</li>");
            }

            if (entity.Parent?.Email != camper.Parent?.Email)
            {
                subRes.AppendLine(
                    $"<li>Изменен email родителя старое значение:'{entity.Parent?.Email}', новое значение:'{camper.Parent?.Email}'</li>");
            }

            if (entity.ParentPlaceWork != camper.ParentPlaceWork)
            {
                subRes.AppendLine(
                    $"<li>Изменено место работы родителя старое значение:'{entity.ParentPlaceWork}', новое значение:'{camper.ParentPlaceWork}'</li>");
            }

            if (entity.Child?.Snils != camper.Child?.Snils)
            {
                subRes.AppendLine(
                    $"<li>Изменен СНИЛС родителя старое значение:'{entity.Child?.Snils}', новое значение:'{camper.Child?.Snils}'</li>");
            }

            if (entity.IsParentUnionist != camper.IsParentUnionist)
            {
                subRes.AppendLine(
                    $"<li>Изменен признак родитель член профсоюза старое значение:'{entity.IsParentUnionist.FormatEx()}', новое значение:'{camper.IsParentUnionist.FormatEx()}'</li>");
            }

            if (entity.IsRelativeUnionist != camper.IsRelativeUnionist)
            {
                subRes.AppendLine(
                    $"<li>Изменен признак родственник член профсоюза старое значение:'{entity.IsRelativeUnionist.FormatEx()}', новое значение:'{camper.IsRelativeUnionist.FormatEx()}'</li>");
            }

            if (entity.TradeUnionStatusByChildId != camper.TradeUnionStatusByChildId)
            {
                var tradeUnionStatusByChild = camper.TradeUnionStatusByChild?.Name ??
                                              UnitOfWork.GetById<TradeUnionStatusByChild>(
                                                  camper.TradeUnionStatusByChildId)?.Name;


                subRes.AppendLine(
                    $"<li>Изменен признак родственник член профсоюза старое значение:'{entity.TradeUnionStatusByChild?.Name}', новое значение:'{tradeUnionStatusByChild}'</li>");
            }

            // Профсоюз перенесен из сведений самого списка (был не множественный).
            if (entity.TradeUnionOrganizationId != camper.TradeUnionOrganizationId ||
                entity.TradeUnionOrganizationOther != camper.TradeUnionOrganizationOther)
            {
                var old = entity.TradeUnionOrganizationId > 0
                    ? entity.TradeUnionOrganization?.Name
                    : entity.TradeUnionOrganizationOther;
                var now = camper.TradeUnionOrganizationId > 0
                    ? UnitOfWork.GetById<Organization>(camper.TradeUnionOrganizationId)?.Name
                    : camper.TradeUnionOrganizationOther;
                subRes.AppendLine(
                    $"<li>Изменен профсоюз старое значение:'{old}', новое значение:'{now}'</li>");
            }

            //-----

            if (entity.Unionist?.LastName != camper.Unionist?.LastName)
            {
                subRes.AppendLine(
                    $"<li>Изменена фамилия родственника-члена профсоюза старое значение:'{entity.Unionist?.LastName}', новое значение:'{camper.Unionist?.LastName}'</li>");
            }

            if (entity.Unionist?.FirstName != camper.Unionist?.FirstName)
            {
                subRes.AppendLine(
                    $"<li>Изменено имя родственника-члена профсоюза старое значение:'{entity.Unionist?.FirstName}', новое значение:'{camper.Unionist?.FirstName}'</li>");
            }

            if (entity.Unionist?.MiddleName != camper.Unionist?.MiddleName)
            {
                subRes.AppendLine(
                    $"<li>Изменено отчество родственника-члена профсоюза старое значение:'{entity.Unionist?.MiddleName}', новое значение:'{camper.Unionist?.MiddleName}'</li>");
            }

            if (entity.Unionist?.Phone != camper.Unionist?.Phone)
            {
                subRes.AppendLine(
                    $"<li>Изменен телефон родственника-члена профсоюза старое значение:'{entity.Unionist?.Phone}', новое значение:'{camper.Unionist?.Phone}'</li>");
            }

            if (entity.Unionist?.Email != camper.Unionist?.Email)
            {
                subRes.AppendLine(
                    $"<li>Изменен email родственника-члена профсоюза старое значение:'{entity.Unionist?.Email}', новое значение:'{camper.Unionist?.Email}'</li>");
            }

            if (entity.RelativePlaceWork != camper.RelativePlaceWork)
            {
                subRes.AppendLine(
                    $"<li>Изменено место работы родственника-члена профсоюза старое значение:'{entity.RelativePlaceWork}', новое значение:'{camper.RelativePlaceWork}'</li>");
            }

            if (entity.Summa != camper.Summa)
            {
                subRes.AppendLine(
                    $"<li>Изменена полная стоимость старое значение:'{entity.Summa.FormatEx()}', новое значение:'{camper.Summa.FormatEx()}'</li>");
            }

            if (entity.SummaParent != camper.SummaParent)
            {
                subRes.AppendLine(
                    $"<li>Изменен размер средств родителей старое значение:'{entity.SummaParent.FormatEx()}', новое значение:'{camper.SummaParent.FormatEx()}'</li>");
            }

            if (entity.SummaTradeUnion != camper.SummaTradeUnion)
            {
                subRes.AppendLine(
                    $"<li>Изменен размер средств профсоюзов старое значение:'{entity.SummaTradeUnion.FormatEx()}', новое значение:'{camper.SummaTradeUnion.FormatEx()}'</li>");
            }

            if (entity.SummaBudget != camper.SummaBudget)
            {
                subRes.AppendLine(
                    $"<li>Изменен размер бюджетных средств старое значение:'{entity.SummaBudget.FormatEx()}', новое значение:'{camper.SummaBudget.FormatEx()}'</li>");
            }

            if (entity.SummaOrganization != camper.SummaOrganization)
            {
                subRes.AppendLine(
                    $"<li>Изменен размер средств предприятия старое значение:'{entity.SummaOrganization.FormatEx()}', новое значение:'{camper.SummaOrganization.FormatEx()}'</li>");
            }

            if (entity.IsChecked != camper.IsChecked)
            {
                subRes.AppendLine(
                    $"<li>Изменен признак заехал старое значение:'{entity.IsChecked.FormatEx()}', новое значение:'{camper.IsChecked.FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.CashbackEstimatedAmount) != camper.NullSafe(r => r.CashbackEstimatedAmount))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Расчетная сумма кэшбека' старое значение:'{entity.NullSafe(r => r.CashbackEstimatedAmount).FormatEx()}', новое значение:'{camper.NullSafe(r => r.CashbackEstimatedAmount).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.CashbackBaseEstimatedAmount) != camper.NullSafe(r => r.CashbackBaseEstimatedAmount))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'База для расчета суммы кэшбека' старое значение:'{entity.NullSafe(r => r.CashbackBaseEstimatedAmount).FormatEx()}', новое значение:'{camper.NullSafe(r => r.CashbackBaseEstimatedAmount).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.ContractDate) != camper.NullSafe(r => r.ContractDate))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Дата заключения договора' старое значение:'{entity.NullSafe(r => r.ContractDate).FormatEx()}', новое значение:'{camper.NullSafe(r => r.ContractDate).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.ContractNumber) != camper.NullSafe(r => r.ContractNumber))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Номер договора' старое значение:'{entity.NullSafe(r => r.ContractNumber).FormatEx()}', новое значение:'{camper.NullSafe(r => r.ContractNumber).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.FactDateIn) != camper.NullSafe(r => r.FactDateIn))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Фактическая дата заезда' старое значение:'{entity.NullSafe(r => r.FactDateIn).FormatEx()}', новое значение:'{camper.NullSafe(r => r.FactDateIn).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.FactDateOut) != camper.NullSafe(r => r.FactDateOut))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Фактическая дата выезда' старое значение:'{entity.NullSafe(r => r.FactDateOut).FormatEx()}', новое значение:'{camper.NullSafe(r => r.FactDateOut).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.CashbackRequested) != camper.NullSafe(r => r.CashbackRequested))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Кэшбек запрашивался' старое значение:'{entity.NullSafe(r => r.CashbackRequested).FormatEx()}', новое значение:'{camper.NullSafe(r => r.CashbackRequested).FormatEx()}'</li>");
            }

            if (entity.NullSafe(r => r.PrivilegePartId)  != camper.NullSafe(r => r.PrivilegePartId ))
            {
                subRes.AppendLine(
                    $"<li>Изменено поле 'Наименование' старое значение:'{entity.NullSafe(r => r.PrivilegePart.Name).FormatEx()}', новое значение:'{camper.NullSafe(r => r.PrivilegePart.Name).FormatEx()}'</li>");
            }

            return subRes.ToString();
        }

        internal static string GetDiff(IUnitOfWork UnitOfWork, TradeUnionList entity, TradeUnionList persisted)
        {
            var sb = new StringBuilder();

            if (persisted.Name != entity.Name)
            {
                sb.AppendLine(
                    $"<li>Изменено наименование старое значение:'{persisted.Name.FormatEx(string.Empty)}', новое значение:'{entity.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.DateFrom != entity.DateFrom)
            {
                sb.AppendLine(
                    $"<li>Изменена дата с старое значение:'{persisted.DateFrom.FormatEx(string.Empty)}', новое значение:'{entity.DateFrom.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.DateTo != entity.DateTo)
            {
                sb.AppendLine(
                    $"<li>Изменена дата по старое значение:'{persisted.DateTo.FormatEx(string.Empty)}', новое значение:'{entity.DateTo.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.YearOfRestId != entity.YearOfRestId)
            {
                sb.AppendLine(
                    $"<li>Изменена дата по старое значение:'{persisted.YearOfRest?.Name}', новое значение:'{UnitOfWork.GetById<YearOfRest>(entity.YearOfRestId)?.Name}'</li>");
            }

            if (persisted.CampId != entity.CampId)
            {
                sb.AppendLine(
                    $"<li>Изменен лагерь старое значение:'{persisted.Camp?.Name}', новое значение:'{UnitOfWork.GetById<YearOfRest>(entity.CampId)?.Name}'</li>");
            }

            // Профсоюз перенесен в сведения детей (теперь множественно).
            // if (persisted.TradeUnionId != entity.TradeUnionId)
            // {
            //     sb.AppendLine($"<li>Изменен профсоюз по старое значение:'{persisted.TradeUnion?.Name}', новое значение:'{UnitOfWork.GetById<Organization>(entity.TradeUnionId)?.Name}'</li>");
            // }

            if (persisted.GroupedTimeOfRestId != entity.GroupedTimeOfRestId)
            {
                sb.AppendLine(
                    $"<li>Изменена смена старое значение:'{persisted.GroupedTimeOfRest?.Name}', новое значение:'{UnitOfWork.GetById<GroupedTimeOfRest>(entity.GroupedTimeOfRestId)?.Name}'</li>");
            }

            if (persisted.NullSafe(r => r.IsCashbackUse) != entity.NullSafe(r => r.IsCashbackUse))
            {
                sb.AppendLine(
                    $"<li>Изменено поле 'Используется в Кэшбэке' старое значение:'{persisted.NullSafe(r => r.IsCashbackUse).FormatEx()}', новое значение:'{entity.NullSafe(r => r.IsCashbackUse).FormatEx()}'</li>");
            }

            //sb.AppendLine(GetChildrenDiff(persisted, entity));

            var res = sb.ToString();
            if (string.IsNullOrWhiteSpace(res))
            {
                return "Сведения о списке не изменились";
            }

            return $"<ul>{res}</ul>";
        }

        /// <summary>
        ///     Заполнение результатов поиска и значений параметров для отображения либо для выгрузки (при POST-е).
        /// </summary>
        private void FillTradeUnionSearch(TradeUnionSearch search, int pageNumber, int pageSize)
        {
            if (!Security.HasRight(AccessRightEnum.TradeUnionList.View))
            {
                var orgs = AccessRightEnum.TradeUnionList.View.GetSecurityOrganiztion();

                var organizations = UnitOfWork.GetSet<Organization>().Where(o => !o.IsDeleted && orgs.Contains(o.Id))
                    .ToList();

                if (organizations.Count(o => o.IsTradeUnion) == 1)
                {
                    search.TradeUnion = organizations.FirstOrDefault(o => o.IsTradeUnion);
                    search.TradeUnionId = search.TradeUnion?.Id;
                    search.OnlyOneTradeUnion = true;
                }

                if (organizations.Count(o => o.IsHotel) == 1)
                {
                    search.Camp = organizations.FirstOrDefault(o => o.IsHotel);
                    search.CampId = search.Camp?.Id;
                    search.OnlyOneCamp = true;
                }
            }

            search.YearOfRests = ApiRestYearController.Get().ToList();
            search.TimeOfRests = UnitOfWork.GetSet<GroupedTimeOfRest>().Where(s => s.IsActive).OrderBy(s => s.Id)
                .ToList();
            search.YearOfRestId = search.YearOfRestId ?? search.YearOfRests.Where(y => y.Year == DateTime.Now.Year)
                .Select(y => y.Id).FirstOrDefault();
            search.States = UnitOfWork.GetSet<StateMachineState>()
                .Where(s => s.StateMachineId == (long) StateMachineEnum.TradeUnionList).ToList();

            var query = UnitOfWork.GetSet<TradeUnionList>()
                .Include(t => t.Camp)
                .Include(t => t.Campers)
                .Include(t => t.State)
                .Where(s => s.StateId.HasValue && s.StateId != StateMachineStateEnum.Deleted && !s.IsCashbackUse)
                .Where(q => q.YearOfRestId == search.YearOfRestId);

            if (!Security.HasRight(AccessRightEnum.TradeUnionList.View))
            {
                var orgsId = AccessRightEnum.TradeUnionList.View.GetSecurityOrganiztion();
                query = query.Where(q => orgsId.Contains(q.CampId) || orgsId.Contains(q.TradeUnionId));
            }

            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                var s = search.Name.Trim().ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(s));
            }

            if (search.CampId.HasValue)
            {
                query = query.Where(q => q.CampId == search.CampId);
                search.Camp = UnitOfWork.GetById<Organization>(search.CampId);
            }

            if (search.TradeUnionId.HasValue)
            {
                query = query.Where(q => q.Campers.Any(c => c.TradeUnionOrganizationId == search.TradeUnionId));
                search.TradeUnion = UnitOfWork.GetById<Organization>(search.TradeUnionId);
            }

            if (search.StateId > 0)
            {
                query = query.Where(q => q.StateId == search.StateId);
            }

            if (search.TimeOfRestId > 0)
            {
                query = query.Where(q => q.GroupedTimeOfRestId == search.TimeOfRestId);
            }

            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var entity = query.OrderByDescending(t => t.Id).Skip(startRecord).Take(pageSize).ToList();

            search.Result = new CommonPagedList<TradeUnionList>(entity, pageNumber, pageSize, totalCount);
        }

        private string ExportTradeUnionListToExcelFile(TradeUnionSearch search, int pageNumber, int pageSize)
        {
            UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            UnitOfWork.Context.Configuration.ProxyCreationEnabled = false;
            UnitOfWork.AutoDetectChangesDisable();

            FillTradeUnionSearch(search, pageNumber, pageSize);

            var result = TradeUnionListExcelExport.GenerateFile(search);

            foreach (var entry in UnitOfWork.Context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            return result;
        }

        /// <summary>
        ///     Подготовка адреса к сохранению записи ребенка.
        /// </summary>
        private static void AddressPrepareForSave(Person postedCamperChild, IUnitOfWork unitOfWork)
        {
            if (postedCamperChild.Address != null)
            {
                postedCamperChild.Address.BtiAddress = null;
                postedCamperChild.Address.BtiDistrict = null;
                postedCamperChild.Address.BtiRegion = null;

                if (postedCamperChild.Address.BtiDistrictId == 0)
                {
                    postedCamperChild.Address.BtiDistrictId = null;
                }

                if (postedCamperChild.AddressId == null)
                {
                    postedCamperChild.Address = null;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(postedCamperChild.Address.FiasId))
                {
                    postedCamperChild.Address.BtiDistrictId = unitOfWork.GetSet<BtiDistrict>()
                        .Where(ss => ss.Name.ToLower() == postedCamperChild.Address.Region.ToLower())
                        .Select(ss => (long?) ss.Id).FirstOrDefault();
                    postedCamperChild.Address.BtiRegionId = unitOfWork.GetSet<BtiRegion>()
                        .Where(ss => ss.Name.ToLower() == postedCamperChild.Address.District.ToLower())
                        .Select(ss => (long?) ss.Id).FirstOrDefault();
                }

                if (postedCamperChild.Address.Id == 0)
                {
                    var a = unitOfWork.AddEntity(postedCamperChild.Address);
                    postedCamperChild.Address = a;
                    postedCamperChild.AddressId = a.Id;
                }
                else
                {
                    var saved = unitOfWork.GetById<Address>(postedCamperChild.Address.Id);
                    saved.BtiAddressId = postedCamperChild.Address.BtiAddressId;
                    saved.Appartment = postedCamperChild.Address.Appartment;
                    if (postedCamperChild.Address.BtiDistrictId != 0)
                    {
                        saved.BtiDistrictId = postedCamperChild.Address.BtiDistrictId;
                    }

                    saved.BtiRegionId = postedCamperChild.Address.BtiRegionId;
                    saved.Corpus = postedCamperChild.Address.Corpus;
                    saved.House = postedCamperChild.Address.House;
                    saved.Name = postedCamperChild.Address.Name;
                    saved.Street = postedCamperChild.Address.Street;
                    saved.Stroenie = postedCamperChild.Address.Stroenie;
                    saved.FiasId = postedCamperChild.Address.FiasId;
                }
            }
        }

        /// <summary>
        ///     Запустить проверку на дубли ребёнка (персоны)
        /// </summary>
        private static void SetPersonToDoubleCheck(IUnitOfWork UnitOfWork, TradeUnionCamper camper)
        {
            UnitOfWork.GetSet<TradeUnionPersonCheck>().Where(f =>
                    f.PersonId == camper.ChildId &&
                    !f.NotActual &&
                    f.PersonCheckType == (long) TradeUnionPersonCheckTypeEnum.ApplicantDouble)
                .ForEach(ss => ss.NotActual = true);

            UnitOfWork.SaveChanges();

            UnitOfWork.AddEntity(new TradeUnionPersonCheck
            {
                PersonId = camper.ChildId,
                PersonCheckType = (long) TradeUnionPersonCheckTypeEnum.ApplicantDouble
            });
        }
    }
}
