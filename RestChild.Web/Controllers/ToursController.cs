using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Comon.ToExcel;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Web.Common;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    [Authorize]
    public class ToursController : BaseController
    {
        /// <summary>
        /// Gets or sets the api controller.
        /// </summary>
        public WebToursController ApiController { get; set; }

        public WebRestTimeController ApiRestTimeController { get; set; }

        public WebRestYearController ApiRestYearController { get; set; }

        public WebRestTypeController ApiRestTypeController { get; set; }

        public WebSubjectOfRestController ApiSubjectOfRestController { get; set; }

        public WebHotelsController ApiHotelsController { get; set; }

        public WebRestTimeController ApiTimeofRestController { get; set; }

        public WebRestPlaceController ApiPlaceRestController { get; set; }

        public WebApi.LimitsController ApiLimitsController { get; set; }

        public StateController ApiStateController { get; set; }

        public PdfController Pdf { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRestTimeController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRestYearController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRestTypeController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiSubjectOfRestController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiHotelsController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiTimeofRestController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiPlaceRestController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiLimitsController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
            Pdf.SetUnitOfWorkInRefClass(unitOfWork);
        }

        // GET: Tours
        public override ActionResult Index()
        {
            return RedirectToAction(nameof(Search));
        }

        public ActionResult Search(ToursFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursView) && !Security.HasRight(AccessRightEnum.Tour.WorkWithServices))
            {
                return RedirectToAvalibleAction();
            }

            filter = filter ?? new ToursFilterModel { MoreThenSelectedYear = true };
            filter.YearsOfRests = ApiRestYearController.Get().ToList();

            if (!filter.YearOfRestId.HasValue)
            {
                filter.MoreThenSelectedYear = true;
            }

            filter.YearOfRestId = filter.YearOfRestId ?? filter.YearsOfRests.Where(y => y.Year == DateTime.Now.Year)
                                     .Select(y => y.Id).FirstOrDefault();
            filter.Result = ApiController.Get(filter);
            filter.TypesOfRests = ApiRestTypeController.GetForTour().ToList();
            filter.States = ApiController.GetStates();
            filter.PlacesOfRest = ApiPlaceRestController.Get();
            var timeOfRest = filter.TimeOfRestId.HasValue
               ? ApiTimeofRestController.Get(filter.TimeOfRestId.Value)
               : null;
            filter.TimeOfRestName = timeOfRest != null ? timeOfRest.Name : string.Empty;
            var hotel = filter.HotelId.HasValue
               ? ApiHotelsController.Get(filter.HotelId.Value)
               : null;
            filter.HotelName = hotel != null ? hotel.Name : string.Empty;
            filter.AccessAddonServices = Security.HasRight(AccessRightEnum.Tour.WorkWithServices);
            filter.TypesOfService =
                UnitOfWork.GetSet<TypeOfService>().Where(t => t.IsActive).OrderBy(t => t.Name).ToArray();
            filter.GroupRestrictions =
                UnitOfWork.GetSet<RestrictionGroup>().Where(t => !t.IsDeleted).OrderBy(p => p.Id).ToList();

            return View("ToursList", filter);
        }

        public ActionResult Insert()
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage))
            {
                return RedirectToAvalibleAction();
            }

            var newRecod = new Tour
            {
                StateId = (int)StateMachineStateEnum.Tour.Formation,
                State = ApiStateController.GetState((int)StateMachineStateEnum.Tour.Formation),
                IsActive = true
            };
            var result = new TourModel(newRecod, true, Security.HasRight(AccessRightEnum.Tour.WorkWithServices))
            {
                State =
                  new ViewModelState
                  {
                      Actions = new List<StateMachineAction>(),
                      State = newRecod.State,
                      FormSelector = "#toursForm",
                      ActionSelector = "#StateMachineActionString",
                      CanReturn = true,
                      ReturnController = "Tours",
                      ReturnAction = "Search",
                      NeedSaveButton = true
                  },
                GroupRestrictions = UnitOfWork.GetSet<RestrictionGroup>().Where(t => !t.IsDeleted).OrderBy(p => p.Id).ToList()
            };

            SetupVocabularies(result);

            return View("ToursEdit", result);
        }

        public ActionResult Update(long id, string actionCode, string activeTab)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage) &&
                !Security.HasRight(AccessRightEnum.Tour.WorkWithServices))
            {
                return RedirectToAvalibleAction();
            }

            var res = Esep.SaveSignsIfNeed(UnitOfWork);
            if (res != null && res.Any())
            {
                var first = res.First();

                var errors = ApiController.ChangeStatus(first.EntityId, first.ActionCode, first.SignInfoId);
                if (errors.Any())
                {
                    SetErrors(errors);
                }
            }

            var tour = ApiController.Get(id);
            if (tour == null)
            {
                return RedirectToAction("Search");
            }

            tour = new Tour(tour, 3)
            {
                Volumes =
                  tour.Volumes?.Select(v => new TourVolume(v) { TypeOfRooms = new TypeOfRooms(v.TypeOfRooms) }).ToList(),
                Hotels = new Hotels(tour.Hotels)
                {
                    TypeOfRooms = tour.Hotels?.TypeOfRooms.Select(tr => new TypeOfRooms(tr)).ToList() ??
                                new List<TypeOfRooms>(),
                    Accommodation = tour.Hotels?.Accommodation.Select(tr => new Accommodation(tr)).ToList() ??
                                  new List<Accommodation>(),
                    DiningOptions = UnitOfWork.GetSet<DiningOptions>().Where(d => d.IsActive).OrderBy(d => d.Id).ToList()
                     .Select(tr => new DiningOptions(tr)).ToList()
                },
                RoomRates = tour.RoomRates.Select(rr => new RoomRates(rr, 1)).ToList(),
                Services = tour.Services?.Where(s => s.IsActive).ToList() ?? new List<AddonServices>(),
            };

            return GetTourViewResult(tour, actionCode, activeTab);
        }

        private ActionResult GetTourViewResult(Tour tour, string actionCode, string activeTab = null)
        {
            var state = tour.StateId.HasValue ? ApiStateController.GetState(tour.StateId.Value) : null;
            var actions = ApiStateController.GetActions(state, StateMachineEnum.TourState);
            var postNoStatusActions = new List<NoStatusAction>();

            if (tour.TypeOfRest == null && tour.TypeOfRestId.HasValue)
            {
                tour.TypeOfRest = UnitOfWork.GetById<TypeOfRest>(tour.TypeOfRestId);
            }

            if (tour.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp && tour.Id != 0)
            {
                postNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Печать",
                    IconClass = "glyphicon glyphicon-print",
                    Action = "GetSpreadsheetForSpecializedCamp",
                    ActionParameters = new { id = tour.Id }
                });
            }

            var result = new TourModel(tour,
               tour.StateId == StateMachineStateEnum.Tour.Formation && Security.HasRight(AccessRightEnum.ToursManage),
               Security.HasRight(AccessRightEnum.Tour.WorkWithServices))
            {
                State =
                  new ViewModelState
                  {
                      Actions = actions,
                      PostNoStatusActions = postNoStatusActions,
                      State = state,
                      FormSelector = "#toursForm",
                      ActionSelector = "#StateMachineActionString",
                      CanReturn = true,
                      ReturnController = "Tours",
                      ReturnAction = "Search",
                      NeedSaveButton =
                        (tour.StateId == StateMachineStateEnum.Tour.Formation &&
                         Security.HasRight(AccessRightEnum.ToursManage)) ||
                        Security.HasRight(AccessRightEnum.Tour.WorkWithServices),
                      NeedRemoveButton =
                        tour.StateId == StateMachineStateEnum.Tour.Formation,
                      Sign = tour.SignInfo,
                      JsFunctionToAction = "confirmStateButtonTour"
                  },
                ActiveTab = activeTab,
                GroupRestrictions = UnitOfWork.GetSet<RestrictionGroup>().Where(t => !t.IsDeleted).OrderBy(p => p.Id).ToList()
            };

            if (tour.HistoryLinkId.HasValue)
            {
                result.State.PostNoStatusActions = result.State.PostNoStatusActions ?? new List<NoStatusAction>();
                result.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "История",
                    ButtonClass = "btn btn-default btn-hystory-link",
                    SomeAddon = $"data-history-id=\"{tour.HistoryLinkId}\""
                });
            }

            result.LoadCollections(ApiController);

            result.OtherToursInYear =
               UnitOfWork.GetSet<Tour>()
                  .Where(
                     t =>
                        t.HotelsId == tour.HotelsId && t.Id != tour.Id && t.StateId != StateMachineStateEnum.Deleted &&
                        t.StateId.HasValue && t.YearOfRestId == tour.YearOfRestId)
                  .ToList();

            if (!string.IsNullOrEmpty(actionCode))
            {
                var errors = ApiController.GetErrorsOfChageStatus(tour.Id, actionCode);
                if (errors != null && errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }

            if (tour.Services?.Any(s => s.Curator == null && s.CuratorId.HasValue) ?? false)
            {
                var ss = tour.Services.Where(s => s.Curator == null && s.CuratorId.HasValue).ToList();
                var cids = ss.Select(s => s.CuratorId).ToList();

                var dict = UnitOfWork.GetSet<Account>().Where(a => cids.Contains(a.Id)).ToDictionary(d => d.Id, d => d);
                foreach (var s in ss)
                {
                    s.Curator = dict[s.CuratorId ?? 0];
                }
            }

            if (tour.Services?.Any(s => s.TypeOfRooms == null && s.TypeOfRoomsId.HasValue) ?? false)
            {
                var ss = tour.Services.Where(s => s.TypeOfRooms == null && s.TypeOfRoomsId.HasValue).ToList();
                var cids = ss.Select(s => s.TypeOfRoomsId).ToList();

                var dict = UnitOfWork.GetSet<TypeOfRooms>().Where(a => cids.Contains(a.Id)).ToDictionary(d => d.Id, d => d);
                foreach (var s in ss)
                {
                    s.TypeOfRooms = dict[s.TypeOfRoomsId ?? 0];
                }
            }

            if (tour.Services?.Any(s => s.TypeOfService == null && s.TypeOfServiceId.HasValue) ?? false)
            {
                var ss = tour.Services.Where(s => s.TypeOfService == null && s.TypeOfServiceId.HasValue).ToList();
                var cids = ss.Select(s => s.TypeOfServiceId).ToList();

                var dict = UnitOfWork.GetSet<TypeOfService>().Where(a => cids.Contains(a.Id))
                   .ToDictionary(d => d.Id, d => d);
                foreach (var s in ss)
                {
                    s.TypeOfService = dict[s.TypeOfServiceId ?? 0];
                }
            }

            SetupVocabularies(result);
            return View("ToursEdit", result);
        }

        [HttpGet]
        public ActionResult Save()
        {
            return RedirectToAction("Search");
        }

        [HttpPost]
        public ActionResult Save(TourModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage) && (model?.Data?.Id ?? 0) == 0)
            {
                return RedirectToAvalibleAction();
            }

            if (!Security.HasRight(AccessRightEnum.Tour.WorkWithServices) &&
                !Security.HasRight(AccessRightEnum.ToursManage))
            {
                return RedirectToAvalibleAction();
            }

            var tour = model?.BuildData() ?? new Tour();
            tour.TimeOfRest = tour.TimeOfRestId.HasValue ? ApiTimeofRestController.Get(tour.TimeOfRestId.Value) : null;
            if (tour.TimeOfRest != null)
            {
                tour.GroupedTimeOfRestId = tour.TimeOfRest?.GroupedTimeOfRestId;
            }

            var isRelationsValids = ValidateRelations(model, tour);
            if ((!ModelState.IsValid || !ValidateModel(tour)) && tour.StateId == StateMachineStateEnum.Tour.Formation ||
                !isRelationsValids)
            {
                tour.Hotels = tour.HotelsId.HasValue ? ApiHotelsController.Get(tour.HotelsId.Value) : null;
                tour.LimitOnVedomstvo = tour.LimitOnVedomstvoId.HasValue
                   ? ApiLimitsController.GetLimitOnVedomstvo(tour.LimitOnVedomstvoId.Value)
                   : null;
                model.LoadCollections(ApiController);

                return GetTourViewResult(tour, model.StateMachineActionString, model.ActiveTab);
            }

            if (tour.Id == 0)
            {
                ApiController.Post(tour, tour.Services?.ToList() ?? new List<AddonServices>());
            }
            else
            {
                if (UnitOfWork.GetLastUpdateTickById<Tour>(tour.Id) != tour.LastUpdateTick)
                {
                    SetRedicted();
                    return RedirectToAction("Update", new { id = tour.Id, activeTab = model.ActiveTab });
                }

                if (tour.StateId == StateMachineStateEnum.Tour.Formation && Security.HasRight(AccessRightEnum.ToursManage))
                {
                    ApiController.Put(tour.Id, tour, tour.Services?.ToList() ?? new List<AddonServices>());
                }

                if (!string.IsNullOrEmpty(model.StateMachineActionString))
                {
                    var action = ApiStateController.GetAction(model.StateMachineActionString);
                    if (action != null && action.NeedSign)
                    {
                        var errors = ApiController.GetErrorsOfChageStatus(tour.Id, model.StateMachineActionString).ToList();
                        if (errors.Any())
                        {
                            SetErrors(errors);
                            return RedirectToAction("Update",
                               new { id = tour.Id, actionCode = model.StateMachineActionString, activeTab = model.ActiveTab });
                        }

                        var data = Pdf.GetDataForSign(tour.Id, SignTypeEnum.TourInfo);
                        if (data != null)
                        {
                            data.ActionCode = model.StateMachineActionString;
                            data.Commentary = string.Empty;
                            var esep = new Esep();
                            var upload = esep.UploadFilesToEsep(new List<DataForSign> { data });
                            var url = esep.UrlToEsep(upload.Select(u => u.FileAccessCode).ToList(), Esep.FullReturnUrl(
                               Url.Action("Update", "Tours",
                                  new
                                  {
                                      id = tour.Id,
                                      actioCode = string.Empty
                                  })), Guid.NewGuid().ToString());
                            return Redirect(url);
                        }
                    }

                    var errs = ApiController.ChangeStatus(tour.Id, model.StateMachineActionString);
                    if (errs.Any())
                    {
                        SetErrors(errs);
                    }

                    return RedirectToAction("Update", new { id = tour.Id, activeTab = model.ActiveTab });
                }
            }

            return RedirectToAction("Update", new { id = tour.Id, activeTab = model.ActiveTab });
        }

        /// <summary>
        /// Отчет по размещению в профильном лагере
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSpreadsheetForSpecializedCamp(long id)
        {
            var tour = UnitOfWork.GetById<Tour>(id);
            if (tour == null)
            {
                return RedirectToAction("Search");
            }

            using (var excelPackage = new ExcelPackage())
            {
                var commonInfoWorksheet = excelPackage.Workbook.Worksheets.Add("Общая информация");
                SpreadsheetSpecializedCampsCommonInfo(commonInfoWorksheet, tour);
                SpreadsheetSpecializedCampCampers(excelPackage, tour);
                var memoryStream = new MemoryStream();
                excelPackage.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   "Размещения в профильном лагере.xlsx");
            }
        }

        /// <summary>
        /// Информация об отдыхающих для отчета по размещению в профильном лагере
        /// </summary>
        /// <param name="excelPackage"></param>
        /// <param name="tour"></param>
        private void SpreadsheetSpecializedCampCampers(ExcelPackage excelPackage, Tour tour)
        {
            var columns = new List<ExcelColumn<CamperModel>>
         {
            new ExcelColumn<CamperModel>()
            {
               Func = c => c.LastName.FormatEx(false),
               Title = "Фамилия",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>()
            {
               Func = c => c.FirstName.FormatEx(false),
               Title = "Имя",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>()
            {
               Func = c => c.MiddleName.FormatEx(false),
               Title = "Отчество",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>()
            {
               Func =
                  c => c.Category.FormatEx(false),
               Title = "Роль",
               WordWrap = true,
               Width = 20
            },
            new ExcelColumn<CamperModel>()
            {
               Func =
                  c =>
                     c.IsMale.FormatEx(
                        "-",
                        "Мужской",
                        "Женский"),
               Title = "Пол",
               WordWrap = true,
               Width = 10
            },
            new ExcelColumn<CamperModel>()
            {
               Func = c => c.BirthDate.FormatEx(),
               Title = "Дата рождения",
               WordWrap = true,
               Width = 15
            },
            new ExcelColumn<CamperModel>()
            {
               Func =
                  c => StaticHelpers.GetAgeInYears(c.BirthDate, c.DateIncome).FormatEx(),
               Title = "Возраст",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>()
            {
               Func =
                  c =>
                     string.Format(
                        "{0} {1}",
                        c.DocSeries,
                        c.DocNumber).FormatEx(false),
               Title = "Номер документа",
               WordWrap = true,
               Width = 20
            },
            new ExcelColumn<CamperModel>()
            {
               Func = c => c.DocIssue.FormatEx(),
               Title = "Выдан",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>
            {
               Func =
                  c => c.DocIssueDate.FormatEx(),
               Title = "Дата выдачи документа",
               WordWrap = true,
               Width = 24
            },
            new ExcelColumn<CamperModel>
            {
               Func =
                  c => c.BirthPlace.FormatEx(false),
               Title = "Место рождения",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>
            {
               Func =
                  c => c.Address.FormatEx(false),
               Title = "Адрес регистрации",
               WordWrap = true,
               Width = 35
            },
            new ExcelColumn<CamperModel>
            {
               Func =
                  c =>
                     c.ApplicantName.FormatEx(false),
               Title = "ФИО",
               WordWrap = true
            },
            new ExcelColumn<CamperModel>
            {
               Func =
                  c =>
                     c.ApplicantPhone.FormatEx(false),
               Title = "Телефон",
               WordWrap = true,
               Width = 15
            },
            new ExcelColumn<CamperModel>
            {
               Func =
                  c =>
                     c.OrganizationName.FormatEx(false),
               Title = "Учреждение",
               WordWrap = true,
               Width = 60
            },
            new ExcelColumn<CamperModel>
            {
               Func = c => c.ListName.FormatEx(false),
               Title = "Список",
               WordWrap = true
            },
         };

            var children = UnitOfWork.GetSet<Child>().Where(c => !c.IsDeleted && c.ChildList.TourId == tour.Id).ToList();
            var attendants = UnitOfWork.GetSet<Applicant>().Where(a => a.ChildList.TourId == tour.Id).ToList();
            List<CamperModel> campers =
               children.Select(
                  c =>
                  {
                      var relationship =
                      c.BaseRegistryInfo.FirstOrDefault(
                            b => !b.NotActual && b.ExchangeBaseRegistryTypeId ==
                                 (long)ExchangeBaseRegistryTypeEnum.Relationship).Parse()?.RelationshipCheckResults
                         ?.FirstOrDefault();

                      return new CamperModel()
                      {
                          LastName = c.LastName,
                          FirstName = c.FirstName,
                          MiddleName = c.MiddleName,
                          Category = "Ребенок",
                          IsMale = c.Male,
                          BirthDate = c.DateOfBirth,
                          DateIncome = c.ChildList?.Tour?.DateIncome,
                          DocSeries = c.DocumentSeria,
                          DocNumber = c.DocumentNumber,
                          DocIssue = c.DocumentSubjectIssue,
                          DocIssueDate = c.DocumentDateOfIssue,
                          BirthPlace = c.PlaceOfBirth,
                          ApplicantName = c.ContactLastName + " " + c.ContactFirstName + " " + c.ContactMiddleName,
                          ApplicantPhone = c.ContactPhone,
                          OrganizationName = c.ChildList?.LimitOnOrganization?.Organization?.Name,
                          ListName = c.ChildList?.Name,
                          Address = c.Address != null ? c.Address.ToString().FormatEx(false) : "-",
                          FatherFio =
                         $"{relationship?.FatherLastName} {relationship?.FatherFirstName} {relationship?.FatherPatronymic}",
                          FatherBirthDate = $"{relationship?.FatherBirthDate.FormatEx()}",
                          MotherFio =
                         $"{relationship?.MotherLastName} {relationship?.MotherFirstName} {relationship?.MotherPatronymic}",
                          MotherBirthDate = $"{relationship?.MotherBirthDate.FormatEx()}"
                      };
                  }).ToList();

            campers.AddRange(
               attendants.Select(
                  a =>
                     new CamperModel()
                     {
                         LastName = a.LastName,
                         FirstName = a.FirstName,
                         MiddleName = a.MiddleName,
                         Category = "Сопровождающий",
                         IsMale = a.Male,
                         BirthDate = a.DateOfBirth,
                         DateIncome = a.ChildList?.Tour?.DateIncome,
                         DocSeries = a.DocumentSeria,
                         DocNumber = a.DocumentNumber,
                         DocIssue = a.DocumentSubjectIssue,
                         DocIssueDate = a.DocumentDateOfIssue,
                         BirthPlace = a.PlaceOfBirth,
                         OrganizationName =
                           a.ChildList != null && a.ChildList.LimitOnOrganization != null
                                               && a.ChildList.LimitOnOrganization.Organization != null
                              ? a.ChildList.LimitOnOrganization.Organization.Name
                              : null,
                         ListName = a.ChildList != null ? a.ChildList.Name : null,
                         Address = "-"
                     }));

            if (!campers.Any())
            {
                return;
            }

            var campersWorksheet = excelPackage.Workbook.Worksheets.Add("Отдыхающие");
            var excelTable = new ExcelTable<CamperModel>(columns);
            excelTable.DataBind(campersWorksheet, campers.OrderBy(c => c.ListName).ThenBy(c => c.Name),
               ExcelBorderStyle.Medium, 2);
            campersWorksheet.Cells[1, 1, 1, 12].Merge = true;
            campersWorksheet.Cells[1, 13, 1, 14].Merge = true;
            campersWorksheet.Cells[1, 15, 1, 16].Merge = true;
            SetTabelBorder(campersWorksheet.Cells[1, 1, 1, 12]);
            SetTabelBorder(campersWorksheet.Cells[1, 13, 1, 14]);
            SetTabelBorder(campersWorksheet.Cells[1, 15, 1, 16]);
            campersWorksheet.Cells[1, 13, 1, 14].Value = "Родитель (законный представитель)";
            campersWorksheet.Cells[1, 13, 1, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            campersWorksheet.Cells[1, 13, 1, 14].Style.Font.Bold = true;
        }

        /// <summary>
        /// Общаяя информация для отчета по размещению в профильном лагере
        /// </summary>
        /// <param name="commonInfoWorksheet"></param>
        /// <param name="tour"></param>
        private static void SpreadsheetSpecializedCampsCommonInfo(ExcelWorksheet commonInfoWorksheet, Tour tour)
        {
            commonInfoWorksheet.Cells[1, 1].Value = "ОИВ";
            commonInfoWorksheet.Cells[2, 1].Value = "Наименование места отдыха";
            commonInfoWorksheet.Cells[3, 1].Value = "Время отдыха";
            commonInfoWorksheet.Cells[4, 1].Value = "Дата начала";
            commonInfoWorksheet.Cells[5, 1].Value = "Дата окончания";
            commonInfoWorksheet.Cells[6, 1].Value = "Контракт";

            commonInfoWorksheet.Cells[1, 2].Value =
               tour.NullSafe(t => t.LimitOnVedomstvo.Organization.Name).FormatEx(false);
            commonInfoWorksheet.Cells[2, 2].Value = tour.NullSafe(t => t.Hotels.Name).FormatEx(false);
            commonInfoWorksheet.Cells[3, 2].Value = tour.NullSafe(t => t.TimeOfRest.Name).FormatEx(false);
            commonInfoWorksheet.Cells[4, 2].Value = tour.DateIncome.FormatEx();
            commonInfoWorksheet.Cells[5, 2].Value = tour.DateOutcome.FormatEx();
            commonInfoWorksheet.Cells[6, 2].Value = tour.NullSafe(t => t.Contract.SignNumber).FormatEx(false);


            commonInfoWorksheet.Cells[1, 1, 6, 1].Style.Font.Bold = true;
            var excelRange = commonInfoWorksheet.Cells[1, 1, 6, 2];
            SetTabelBorder(excelRange);

            commonInfoWorksheet.Column(1).Width = 30;
            commonInfoWorksheet.Column(2).Width = 60;
        }

        private static void SetTabelBorder(ExcelRange excelRange)
        {
            excelRange.Style.Border.Top.Style = ExcelBorderStyle.Medium;
            excelRange.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            excelRange.Style.Border.Left.Style = ExcelBorderStyle.Medium;
            excelRange.Style.Border.Right.Style = ExcelBorderStyle.Medium;
        }

        private bool ValidateRelations(TourModel tour, Tour tourData)
        {
            if (tourData != null && tourData.StateId == StateMachineStateEnum.Tour.Formation &&
                !string.IsNullOrEmpty(tour.StateMachineActionString) && tour.StateMachineActionString != null)
            {
                bool isValid = true;

                if (tourData.YearOfRestId == null)
                {
                    isValid = false;
                    ModelState.AddModelError("Data.YearOfRestId", "Необходимо указать год отдыха");
                }

                if (tourData.TypeOfRestId == null)
                {
                    isValid = false;
                    ModelState.AddModelError("Data.TypeOfRestId", "Необходимо указать вид отдыха");
                }

                if (tourData.HotelsId == null)
                {
                    isValid = false;
                    ModelState.AddModelError("Data.HotelsId", "Необходимо указать место отдыха");
                }

                if (tourData.TypeOfRest?.NeedSubject ?? false)
                {
                    if (!tourData.SubjectOfRestId.HasValue || tourData.SubjectOfRestId == 0)
                    {
                        isValid = false;
                        ModelState.AddModelError("Data.SubjectOfRestId", "Необходимо указать тематику смены");
                    }
                }

                return isValid;
            }

            return true;
        }

        private bool ValidateModel(Tour model)
        {
            var isOk = true;
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Ошибка получения данных");
                return false;
            }

            if (!model.TypeOfRestId.HasValue)
            {
                isOk = false;
                ModelState.AddModelError("Data.TypeOfRestId", "Необходимо указать вид отдыха");
            }

            if (!model.YearOfRestId.HasValue)
            {
                isOk = false;
                ModelState.AddModelError("Data.YearOfRestId", "Необходимо указать год кампании");
            }

            if (!model.HotelsId.HasValue)
            {
                isOk = false;
                ModelState.AddModelError("Data.HotelsId", "Необходимо указать место отдыха");
            }

            if (!model.TimeOfRestId.HasValue)
            {
                isOk = false;
                ModelState.AddModelError("Data.TimeOfRestId", "Необходимо указать время отдыха");
            }

            if (model.TypeOfRest == null && model.TypeOfRestId.HasValue)
            {
                model.TypeOfRest = UnitOfWork.GetById<TypeOfRest>(model.TypeOfRestId);
            }

            if (model.TypeOfRest?.NeedBookingDate ?? false)
            {
                if (!model.StartBooking.HasValue)
                {
                    isOk = false;
                    ModelState.AddModelError("Data.StartBooking", "Необходимо указать дату начала записи");
                }

                if (!model.EndBooking.HasValue)
                {
                    isOk = false;
                    ModelState.AddModelError("Data.EndBooking", "Необходимо указать дату окончания записи");
                }
            }

            if (model.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp)
            {
                var limit = UnitOfWork.GetById<LimitOnVedomstvo>(model.LimitOnVedomstvoId);
                if (!model.TourPrice.HasValue && limit?.TypeOfLimitListId != (long)TypeOfLimitListEnum.Orphan)
                {
                    isOk = false;
                    ModelState.AddModelError("Data.TourPrice", "Необходимо указать стоимость для ребенка, руб");
                }

                if (!model.TourPriceAttendant.HasValue && limit?.TypeOfLimitListId != (long)TypeOfLimitListEnum.Orphan)
                {
                    isOk = false;
                    ModelState.AddModelError("Data.TourPriceAttendant",
                       "Необходимо указать стоимость для тренера или педагога, руб");
                }
            }

            return isOk;
        }

        private void SetupVocabularies(TourModel tour)
        {
            tour.TimesOfRest = ApiRestTimeController.Get().ToList();
            if ((tour.Data?.Id ?? 0) == 0 || !(tour.Data?.TypeOfRestId.HasValue ?? false))
            {
                tour.TypesOfRest = ApiRestTypeController.GetForTour().ToList();
            }
            else
            {
                tour.TypesOfRest = new List<TypeOfRest> { UnitOfWork.GetById<TypeOfRest>(tour.Data.TypeOfRestId) };
            }

            tour.SubjectsOfRest = ApiSubjectOfRestController.Get().ToList();
            if ((tour.Data?.SubjectOfRestId.HasValue ?? false))
            {
                if (!tour.SubjectsOfRest.Select(s => s.Id).Contains(tour.Data.SubjectOfRestId.Value))
                {
                    tour.SubjectsOfRest.Add(UnitOfWork.GetById<SubjectOfRest>(tour.Data.SubjectOfRestId.Value));
                }
            }

            tour.YearsOfRest = ApiRestYearController.Get().ToList();

            if (!(tour.Data?.YearOfRestId.HasValue ?? true))
            {
                tour.Data.YearOfRestId = tour.YearsOfRest?.FirstOrDefault(y => y.Year == DateTime.Now.Year)?.Id ??
                                         tour.YearsOfRest?.LastOrDefault()?.Id;
            }

            if (tour.Data != null && tour.Data.Hotels != null)
            {
                tour.TypeOfRooms = tour.Data.Hotels.TypeOfRooms.OrderBy(a => a.Name).ToList();
                tour.TypeOfRooms.Insert(0, new TypeOfRooms { Id = -1, Name = "-- Не выбрано --" });
                tour.DiningOptions = tour.Data.Hotels.DiningOptions.OrderBy(a => a.Name).ToList();
                tour.DiningOptions.Insert(0, new DiningOptions { Id = -1, Name = "-- Не выбрано --" });
                tour.Accommodations = tour.Data.Hotels.Accommodation.OrderBy(a => a.Name).ToList();
                tour.Accommodations.Insert(0, new Accommodation { Id = -1, Name = "-- Не выбрано --" });
            }
            else
            {
                tour.TypeOfRooms = new List<TypeOfRooms>();
                tour.DiningOptions = new List<DiningOptions>();
                tour.Accommodations = new List<Accommodation>();
            }
        }
    }
}
