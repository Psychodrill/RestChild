using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using RestChild.Web.Models.TradeUnionCashback;
using DocumentType = RestChild.Domain.DocumentType;
using Settings = RestChild.Web.Properties.Settings;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     списки профсоюзов
    /// </summary>
    public class TradeUnionCashbackController : BaseController
    {
        public WebRestYearController ApiRestYearController { get; set; }

        public WebVocabularyController VocController { get; set; }

        public WebBtiDistrictsController DistrictController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRestYearController.SetUnitOfWorkInRefClass(unitOfWork);
            VocController.SetUnitOfWorkInRefClass(UnitOfWork);
            DistrictController.SetUnitOfWorkInRefClass(UnitOfWork);
        }

        /// <summary>
        ///     поиск списков
        /// </summary>
        public ActionResult List(TradeUnionCashbackFilterModel filter)
        {
            SetUnitOfWorkInRefClass();

            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionCashback.ListView}))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.TradeUnionCashback.ListView.GetSecurityOrganiztion();

            filter = filter ?? new TradeUnionCashbackFilterModel();
            filter.YearOfRestId = UnitOfWork.GetSet<YearOfRest>().Where(y => y.Year == DateTime.Now.Year)
                .Select(y => y.Id).FirstOrDefault();

            var q = UnitOfWork.GetSet<TradeUnionList>()
                .Include(t => t.Camp)
                .Include(t => t.Campers)
                .Where(ss => ss.IsCashbackUse).AsQueryable();

            if (filter.OrganizationId.HasValue && filter.OrganizationId.Value > 0)
            {
                q = q.Where(ss => ss.CampId == filter.OrganizationId.Value);
            }

            if (filter.YearOfRestId.HasValue && filter.YearOfRestId.Value > 0)
            {
                q = q.Where(ss => ss.YearOfRestId == filter.YearOfRestId.Value);
            }

            if (filter.TimeOfRestId.HasValue && filter.TimeOfRestId.Value > 0)
            {
                q = q.Where(ss => ss.GroupedTimeOfRestId == filter.TimeOfRestId.Value);
            }

            var tc = q.Count();

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = filter.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            q = q.OrderByDescending(t => t.Id).Skip(startRecord).Take(pageSize);

            filter.Results = new CommonPagedList<TradeUnionList>(q.ToList(), pageNumber, pageSize, tc);

            var orgs = new List<Organization>();

            if (organizationIds.Any())
            {
                orgs = UnitOfWork.GetSet<Organization>().Where(s => organizationIds.Contains(s.Id)).ToList();
            }
            else
            {
                orgs = UnitOfWork.GetSet<Organization>().Where(s => !s.IsDeleted && s.ESNSIType != null).ToList();
            }

            orgs.Insert(0, new Organization() {Id = 0, Name = "-- Не выбрано --"});
            filter.Organizations = orgs.ToDictionary(ss => ss.Id, sx => sx.Name);

            var gtor = UnitOfWork.GetSet<GroupedTimeOfRest>().Where(s => s.IsActive).OrderBy(s => s.Id).ToList();
            gtor.Insert(0, new GroupedTimeOfRest {Id = 0, Name = "-- Не выбрано --"});

            filter.TimeOfRests = gtor.ToDictionary(ss => ss.Id, sx => sx.Name);

            return View(filter);
        }

        /// <summary>
        ///     редактирование списка.
        /// </summary>
        public ActionResult Edit(long? id)
        {
            SetUnitOfWorkInRefClass();

            if (!Security.HasAnyRightsForSomeOrganization(new[]
                {AccessRightEnum.TradeUnionCashback.ListView, AccessRightEnum.TradeUnionCashback.ListEdit}))
            {
                return RedirectToAvalibleAction();
            }

            var entity = UnitOfWork.GetById<TradeUnionList>(id) ?? new TradeUnionList
            {
                StateId = StateMachineStateEnum.TradeUnion.Edit,
                State = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.TradeUnion.Edit)
            };

            var isEditable = Security.HasRightForSomeOrganization(AccessRightEnum.TradeUnionCashback.ListEdit);

            var model = new TradeUnionCashbackModel(entity)
            {
                ActiveTab = null,
                IsEditable = isEditable,
                IsIncomeFlag = false,
                State = new ViewModelState
                {
                    CanReturn = true,
                    Actions = new List<StateMachineAction>(),
                    NeedSaveButton = true,
                    State = entity.State,
                    FormSelector = "#requestForm",
                    ActionSelector = "#StateMachineActionString",
                    ReturnController = "CommercialTour",
                    ReturnAction = "List",
                    NeedRemoveButton = false,
                    JsFunctionToAction = "confirmStateButtonTradeUnion",
                    CommentSelector = "#CommentToDeclined",
                },
                YearOfRests = ApiRestYearController.Get().ToList(),
                TimeOfRests = UnitOfWork.GetSet<GroupedTimeOfRest>().Where(s => s.IsActive).OrderBy(s => s.Id).ToList()
            };

            var orgs = AccessRightEnum.TradeUnionCashback.ListEdit.GetSecurityOrganiztion();

            var organizations = new List<Organization>();

            if (orgs.Any())
            {
                organizations = UnitOfWork.GetSet<Organization>().Where(o => !o.IsDeleted && orgs.Contains(o.Id))
                    .ToList();
            }
            else
            {
                organizations = UnitOfWork.GetSet<Organization>().Where(o => !o.IsDeleted && o.ESNSIType != null)
                    .ToList();
            }

            model.Data.YearOfRestId = UnitOfWork.GetSet<YearOfRest>().Where(y => y.Year == DateTime.Now.Year)
                .Select(y => y.Id).FirstOrDefault();
            model.DocumentTypes =
                UnitOfWork.GetSet<DocumentType>().Where(d => d.ForChild && !d.ForForeign).OrderBy(d => d.Name).ToList();
            model.DocumentTypesForParent =
                UnitOfWork.GetSet<DocumentType>().Where(d => d.ForApplicant && !d.ForForeign).OrderBy(d => d.Name)
                    .ToList();
            model.StatusByChild =
                UnitOfWork.GetSet<TradeUnionStatusByChild>().OrderBy(d => d.Name).ToList();
            model.Organizations = organizations;
            var tucpp = UnitOfWork.GetSet<TradeUnionCamperPrivilegePart>().Where(ss => ss.IsActive).ToList();
            tucpp.Insert(0, new TradeUnionCamperPrivilegePart() {Id = 0, Name = "-- Не выбрано --"});
            model.PrivilegeParts = tucpp;

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
            return RedirectToAction(nameof(List));
        }

        /// <summary>
        ///     сохранение карточки списка
        /// </summary>
        [HttpPost]
        public ActionResult Save(TradeUnionCashbackModel model)
        {
            SetUnitOfWorkInRefClass();
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionCashback.ListEdit}))
            {
                return RedirectToAvalibleAction();
            }

            if (model?.Data == null)
            {
                if (model?.Data?.Id > 0)
                {
                    return RedirectToAction(nameof(Edit), new {id = model.Data.Id});
                }

                return RedirectToAction(nameof(List));
            }

            var entity = model.BuildData();
            entity.IsCashbackUse = true;

            if (entity.LastUpdateTick != UnitOfWork.GetLastUpdateTickById<TradeUnionList>(entity.Id))
            {
                SetRedicted();
                return RedirectToAction(nameof(Edit), new {id = entity.Id});
            }

            if (entity.GroupedTimeOfRestId == 0)
            {
                entity.GroupedTimeOfRestId = null;
            }

            if (entity.Id == 0)
            {
                entity = UnitOfWork.AddEntity(entity);
                entity.HistoryLink = this.WriteHistory(entity.HistoryLink,
                    "Первое сохранение списка претендентов на кэшбек", string.Empty);
                entity.HistoryLinkId = entity.HistoryLink?.Id;

                UnitOfWork.SaveChanges();
            }
            else
            {
                var persisted = UnitOfWork.GetById<TradeUnionList>(entity.Id);
                persisted.HistoryLink = this.WriteHistory(persisted.HistoryLink, "Сохранение списка",
                    TradeUnionController.GetDiff(UnitOfWork, entity, persisted));
                persisted.LastUpdateTick = DateTime.Now.Ticks;
                persisted.HistoryLinkId = persisted.HistoryLink?.Id;
                persisted.CopyEntity(entity);
                UnitOfWork.SaveChanges();
            }

            return RedirectToAction(nameof(Edit), new {id = entity.Id});
        }

        /// <summary>
        ///     сохранение признака запрашивался кэшбэк
        /// </summary>
        public ActionResult SaveCashbackRequested(long? id, bool cashbackRequested)
        {
            var camper = UnitOfWork.GetById<TradeUnionCamper>(id);

            var diff = new StringBuilder();
            if (camper.NullSafe(r => r.CashbackRequested) != cashbackRequested)
            {
                diff.AppendLine(
                    $"<li>Изменено поле 'Кэшбек запрашивался' старое значение:'{camper.NullSafe(r => r.CashbackRequested).FormatEx()}', новое значение:'{cashbackRequested.FormatEx()}'</li>");
            }

            camper.TradeUnion.HistoryLink = this.WriteHistory(camper.TradeUnion.HistoryLink,
                "Изменение признака 'Запрашивался кэшбэк'", $"{diff}");
            camper.CashbackRequested = cashbackRequested;

            UnitOfWork.SaveChanges();

            return Content(string.Empty);
        }

        /// <summary>
        ///     удалить ребёнка
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> DeleteChild(long id)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionCashback.ListEdit}))
            {
                return Content("Нет прав для удаления отдыхающего");
            }

            var entity = await UnitOfWork.GetByIdAsync<TradeUnionCamper>(id, CancellationToken.None);

            this.WriteHistory(entity.TradeUnion.HistoryLink, "Удаление ребёнка",
                $"Удалён ребёнок {TradeUnionController.GetChildName(UnitOfWork, entity)}");

            TradeUnionController.DeleteCamper(UnitOfWork, entity);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            return Content(string.Empty);
        }

        /// <summary>
        ///     сохранить ребёнка
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> SaveChild(string child)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.TradeUnionCashback.ListEdit}))
            {
                return RedirectToAvalibleAction();
            }

            var data = JsonConvert.DeserializeObject<TradeUnionCashbackCamperModel>(child)?.BuildEntity();

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
            var diff = persisted == null
                ? string.Empty
                : TradeUnionController.GetChildDiff(UnitOfWork, persisted, data);

            if (persisted != null && string.IsNullOrWhiteSpace(diff))
            {
                UnitOfWork.DetachAllEntitys();
                persisted = await UnitOfWork.GetByIdAsync<TradeUnionCamper>(data.Id, CancellationToken.None);
                return Content(JsonConvert.SerializeObject(new TradeUnionCamperModel(persisted)));
            }

            diff = persisted == null
                ? $"<ul><li>Добавлен ребёнок: {TradeUnionController.GetChildName(UnitOfWork, data)}</li></ul>"
                : $"<ul><li>Изменен ребёнок: {TradeUnionController.GetChildName(UnitOfWork, persisted)} <ul>{diff}</ul></li></ul>";

            tradeUnion.HistoryLink = this.WriteHistory(tradeUnion.HistoryLink, "Сохранение списка", diff);

            persisted = TradeUnionController.SaveCamper(UnitOfWork, persisted, data, false);

            await UnitOfWork.SaveChangesAsync(CancellationToken.None);
            UnitOfWork.DetachAllEntitys();
            persisted = await UnitOfWork.GetByIdAsync<TradeUnionCamper>(data.Id, CancellationToken.None);
            return Content(JsonConvert.SerializeObject(new TradeUnionCamperModel(persisted)));
        }

        #region Претенденты на кэшбэк

        /// <summary>
        ///     Поиск претендентов на кэшбэк
        /// </summary>
        public ActionResult Search(TradeUnionCamperCashbackFilterModel filter)
        {
            if (!Security.HasRight(AccessRightEnum.TradeUnionCashback.RegistryView))
            {
                return RedirectToAction("Index", "Home");
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            var pageSize = Settings.Default.TablePageSize;

            filter.PageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
            var skip = (filter.PageNumber - 1) * pageSize;

            var query = GetTradeUnionCamperQuery(filter);

            var totalCount = query.Count();
            var list = query.OrderBy(i => i.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToArray();

            filter.Results = new CommonPagedList<TradeUnionCamper>(list, filter.PageNumber, pageSize, totalCount);

            var org = UnitOfWork.GetSet<Organization>().Where(s => !s.IsDeleted && s.ESNSIType != null)
                .OrderBy(s => s.Id).ToList();
            org.Insert(0, new Organization {Id = 0, Name = "-- Не выбрано --"});
            filter.Camps = org.ToDictionary(ss => ss.Id, sx => sx.Name);

            var gtor = UnitOfWork.GetSet<GroupedTimeOfRest>().Where(s => s.IsActive).OrderBy(s => s.Id).ToList();
            gtor.Insert(0, new GroupedTimeOfRest {Id = 0, Name = "-- Не выбрано --"});
            filter.Shifts = gtor.ToDictionary(ss => ss.Id, sx => sx.Name);

            return View("TradeUnionCamperCashbackRegistry", filter);
        }

        /// <summary>
        ///     Получить запрос на основе фильтра
        /// </summary>
        private IQueryable<TradeUnionCamper> GetTradeUnionCamperQuery(TradeUnionCamperCashbackFilterModel filter)
        {
            var query = UnitOfWork.GetSet<TradeUnionCamper>().Where(h => h.TradeUnion.IsCashbackUse).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.FIO))
            {
                var items = filter.FIO.Split(' ').Where(ss => !string.IsNullOrWhiteSpace(ss))
                    .Select(ss => ss.ToLower().Replace("ё", "е")).ToArray();
                foreach (var item in items)
                {
                    query = query.Where(r =>
                        r.Child.LastName.ToLower().Replace("ё", "е").Contains(item) ||
                        r.Child.FirstName.ToLower().Replace("ё", "е").Contains(item) ||
                        r.Child.MiddleName.ToLower().Replace("ё", "е").Contains(item));
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.SNILS))
            {
                query = query.Where(r => r.Child.Snils == filter.SNILS);
            }

            if (!string.IsNullOrWhiteSpace(filter.DocumentSeria))
            {
                query = query.Where(r => r.Child.DocumentSeria == filter.DocumentSeria);
            }

            if (!string.IsNullOrWhiteSpace(filter.DocumentNumber))
            {
                query = query.Where(r => r.Child.DocumentNumber == filter.DocumentNumber);
            }

            if (filter.CampId.HasValue && filter.CampId.Value > 0)
            {
                query = query.Where(r => r.TradeUnion.CampId == filter.CampId.Value);
            }

            if (filter.ShiftId.HasValue && filter.ShiftId.Value > 0)
            {
                query = query.Where(r => r.TradeUnion.GroupedTimeOfRest.Id == filter.ShiftId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.RepresentativeFIO))
            {
                var items = filter.RepresentativeFIO.Split(' ').Where(ss => !string.IsNullOrWhiteSpace(ss))
                    .Select(ss => ss.ToLower().Replace("ё", "е")).ToArray();

                foreach (var item in items)
                {
                    query = query.Where(r =>
                        r.Parent.LastName.ToLower().Replace("ё", "е").Contains(item) ||
                        r.Parent.FirstName.ToLower().Replace("ё", "е").Contains(item) ||
                        r.Parent.MiddleName.ToLower().Replace("ё", "е").Contains(item));
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.RepresentativeSNILS))
            {
                query = query.Where(r => r.Parent.Snils == filter.RepresentativeSNILS);
            }

            if (filter.CashbackRequested)
            {
                query = query.Where(ss => ss.CashbackRequested == true);
            }

            return query;
        }

        /// <summary>
        ///     Экспорт в Excel
        /// </summary>
        public ActionResult ExcelList(TradeUnionCamperCashbackFilterModel model)
        {
            if (!Security.HasRight(AccessRightEnum.TradeUnionCashback.RegistryView))
            {
                return RedirectToAction("Index", "Home");
            }

            var query = GetTradeUnionCamperQuery(model ?? new TradeUnionCamperCashbackFilterModel());


            var columns = new List<ExcelColumn<TradeUnionCamper>>
            {
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Уникальный ключ", Func = t => GenerateChildEKEY(t) ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Тип лагеря", Func = t => t.TradeUnion?.Camp?.ESNSIType ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "ИНН лагеря", Func = t => t.TradeUnion?.Camp?.Inn ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "ОКАТО региона по местонахождению лагеря", Func = t => t.TradeUnion?.Camp?.OKATO ?? "*",
                    Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Фактический адрес лагеря", Func = t => t.TradeUnion?.Camp?.Address ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Тип ДУЛ ребенка из путевки",
                    Func = t => GetChildDocTypeAsString(t.Child?.DocumentType) ?? "*", Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Серия ДУЛ ребенка из путевки", Func = t => t.Child?.DocumentSeria ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Номер ДУЛ ребенка из путевки", Func = t => t.Child?.DocumentNumber ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Признак льготы", Func = t => t.PrivilegePart?.Id.ToString() ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                    {Title = "Номер договора", Func = t => t.ContractNumber ?? "*", Width = 20},
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Дата заключения договора", Func = t => t.ContractDate?.ToString("dd.MM.yyyy") ?? "*",
                    Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Серия паспорта РФ законного представителя (из путевки)",
                    Func = t => t.Parent?.DocumentSeria ?? "*", Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Номер паспорта РФ законного представителя (из путевки)",
                    Func = t => t.Parent?.DocumentNumber ?? "*", Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Дата начала путевки", Func = t => t.TradeUnion?.DateFrom?.ToString("dd.MM.yyyy") ?? "*",
                    Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Дата окончания путевки", Func = t => t.TradeUnion?.DateTo?.ToString("dd.MM.yyyy") ?? "*",
                    Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Дата начала фактического пребывания",
                    Func = t => t.FactDateIn?.ToString("dd.MM.yyyy") ?? "*", Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Дата окончания фактического пребывания",
                    Func = t => t.FactDateOut?.ToString("dd.MM.yyyy") ?? "*", Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Стоимость путевки по договору",
                    Func = t => (t.Summa != null ? string.Format("{0:0.00}", t.Summa) : "*"), Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Размер выплаты",
                    Func = t =>
                        (t.TradeUnion?.Campers.Where(x => x.ChildId == t.ChildId).FirstOrDefault()
                            ?.CashbackBaseEstimatedAmount != null
                            ? string.Format("{0:0.00}",
                                t.TradeUnion?.Campers.Where(x => x.ChildId == t.ChildId).FirstOrDefault()
                                    ?.CashbackBaseEstimatedAmount)
                            : "*"),
                    Width = 20
                },
                new ExcelColumn<TradeUnionCamper>
                {
                    Title = "Размер выплаты",
                    Func = t =>
                        (t.TradeUnion?.Campers.Where(x => x.ChildId == t.ChildId).FirstOrDefault()
                            ?.CashbackEstimatedAmount != null
                            ? string.Format("{0:0.00}",
                                t.TradeUnion?.Campers.Where(x => x.ChildId == t.ChildId).FirstOrDefault()
                                    ?.CashbackEstimatedAmount)
                            : "*"),
                    Width = 20
                },
            };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            var data = query.ToList();

            using (var excel = new ExcelTable<TradeUnionCamper>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Реестр претендентов");

                excel.TableName = "Реестр претендентов";

                excel.Parameters = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Название:",
                            "Список оказанных услуг для компенсации оплаты детского отдыха"),
                        new Tuple<string, string>("Код справочника:", "chd.return.money.vacation.child"),
                        new Tuple<string, string>("Группа доступа:", "МИНИСТЕРСТВО ПРОСВЕЩЕНИЯ РОССИЙСКОЙ ФЕДЕРАЦИИ"),
                    }
                    .Where(i => !String.IsNullOrWhiteSpace(i.Item2))
                    .ToList();

                excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Реестр претендентов на кэшбэк.xlsx");
            }
        }

        /// <summary>
        ///     Экспорт в CSV
        /// </summary>
        public ActionResult ExportCSV(TradeUnionCamperCashbackFilterModel model)
        {
            if (!Security.HasRight(AccessRightEnum.TradeUnionCashback.RegistryExportCSV))
            {
                return RedirectToAction("Index", "Home");
            }

            var csv = new StringBuilder();

            csv.Append("E_KEY;");
            csv.Append("TYPE_ORG;");
            csv.Append("INN;");
            csv.Append("OKATO;");
            csv.Append("ADDRESS_F;");
            csv.Append("TAPE_DUL_CHILD;");
            csv.Append("SER_SVID_ROJ;");
            csv.Append("NUM_SVID_ROJ;");
            csv.Append("TYPE_PRIVEL;");
            csv.Append("DATE_DOGOVOR;");
            csv.Append("NUM_DOGOVOR;");
            csv.Append("SER_DUL;");
            csv.Append("NUM_DUL;");
            csv.Append("START_D;");
            csv.Append("END_D;");
            csv.Append("F_DATE_START;");
            csv.Append("F_DATE_END;");
            csv.Append("SUM;");
            csv.Append("BASE_PAY;");
            csv.Append("PAY;");
            csv.Append("autoKey;");

            csv.AppendLine();

            var query = GetTradeUnionCamperQuery(model ?? new TradeUnionCamperCashbackFilterModel());

            var data = query.ToList();

            foreach (var el in data)
            {
                csv.AppendLine(
                    $"{GenerateChildEKEY(el) ?? "*"};" +
                    $"{el.TradeUnion?.Camp?.ESNSIType ?? "*"};" +
                    $"{el.TradeUnion?.Camp?.Inn ?? "*"};" +
                    $"{el.TradeUnion?.Camp?.OKATO ?? "*"};" +
                    $"{el.TradeUnion?.Camp?.Address ?? "*"};" +
                    $"{GetChildDocTypeAsString(el.Child?.DocumentType) ?? "*"};" +
                    $"{el.Child?.DocumentSeria ?? "*"};" +
                    $"{el.Child?.DocumentNumber ?? "*"};" +
                    $"{el.PrivilegePart?.Id.ToString() ?? "*"};" +
                    $"{el.ContractDate?.ToString("dd.MM.yyyy") ?? "*"};" +
                    $"{el.ContractNumber ?? "*"};" +
                    $"{el.Parent?.DocumentSeria ?? "*"};" +
                    $"{el.Parent?.DocumentNumber ?? "*"};" +
                    $"{el.TradeUnion?.DateFrom?.ToString("dd.MM.yyyy") ?? "*"};" +
                    $"{el.TradeUnion?.DateTo?.ToString("dd.MM.yyyy") ?? "*"};" +
                    $"{el.FactDateIn?.ToString("dd.MM.yyyy") ?? "*"};" +
                    $"{el.FactDateOut?.ToString("dd.MM.yyyy") ?? "*"};" +
                    $"{(el.Summa != null ? string.Format("{0:0.00}", el.Summa) : "*")};" +
                    $"{(el.TradeUnion?.Campers?.Where(x => x.ChildId == el.ChildId).FirstOrDefault()?.CashbackBaseEstimatedAmount != null ? string.Format("{0:0.00}", el.TradeUnion?.Campers?.Where(x => x.ChildId == el.ChildId).FirstOrDefault()?.CashbackBaseEstimatedAmount) : "*")};" +
                    $"{(el.TradeUnion?.Campers.Where(x => x.ChildId == el.ChildId).FirstOrDefault()?.CashbackEstimatedAmount != null ? string.Format("{0:0.00}", el.TradeUnion?.Campers.Where(x => x.ChildId == el.ChildId).FirstOrDefault()?.CashbackEstimatedAmount) : "*")};" +
                    ";");
            }

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream, Encoding.GetEncoding("Windows-1251")))
            {
                writer.Write(csv.ToString());
                writer.Flush();
                return File(memoryStream.ToArray(), "application/octet-stream", "Реестр претендентов на кэшбэк.csv");
            }
        }

        /// <summary>
        ///     Получить тип ДУЛ ребенка
        /// </summary>
        private string GetChildDocTypeAsString(DocumentType documentType)
        {
            if (documentType != null)
            {
                int dt;
                switch (documentType.Id)
                {
                    case ((long) DocumentTypeEnum.CertOfBirth):
                    {
                        dt = 1;
                        return dt.ToString();
                    }
                    case ((long) DocumentTypeEnum.PassportRF):
                    {
                        dt = 2;
                        return dt.ToString();
                    }
                    default:
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///     Сформировать уникальный ключ парсинга E_KEY для ребенка (OKATO + H_NUM + ChildId)
        /// </summary>
        private string GenerateChildEKEY(TradeUnionCamper camper)
        {
            var key =
                $"{Format(camper?.TradeUnion?.Camp?.OKATO, 4) ?? "0000"}{Format(camper?.TradeUnion?.Camp?.ESNSIType, 4) ?? "0000"}{String.Format("{0:000000}", camper.Id)}";
            return key;
        }

        /// <summary>
        ///     Подготовить для E_KEY
        /// </summary>
        private string Format(string str, int maxLength)
        {
            if (str != null)
            {
                var length = str.Length;
                if (length < maxLength)
                {
                    var insertsCount = maxLength - length;
                    return new string('0', insertsCount) + str;
                }
                else if (length > maxLength)
                {
                    return str.Substring(0, 4);
                }

                return str;
            }

            return null;
        }

        #endregion
    }
}
