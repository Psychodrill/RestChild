using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
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
	[System.Web.Mvc.Authorize]
	public class BoutController : BaseController
	{
		public WebBoutController ApiController { get; set; }

		public StateController ApiStateController { get; set; }

		public WebRestTimeController ApiRestTimeController { get; set; }

		public WebHotelsController ApiHotelsController { get; set; }

		public WebHotelTypeController ApiHotelTypeController { get; set; }

		public WebRestTimeController ApiTimeOfRestController { get; set; }

		public WebCounselorsController ApiCounselorController { get; set; }

		public GroupedTimeOfRestController ApiGroupedTimeOfRestController { get; set; }

		public WebAdministratorTourController ApiAdministratorTourController { get; set; }

		public WebRestYearController ApiYearController { get; set; }

		public WebVocabularyController ApiVocabularyController { get; set; }

		public WebTransportInfoController ApiTransportInfoController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiRestTimeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelsController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiTimeOfRestController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCounselorController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiGroupedTimeOfRestController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiAdministratorTourController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiYearController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiTransportInfoController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Search(BoutFilterModel filter)
		{
         if (!Security.HasAnyRights(new[] { AccessRightEnum.Bout.Counselor, AccessRightEnum.BoutManage, AccessRightEnum.Bout.AdministratorTour }))
         {
            return RedirectToAvalibleAction();
         }

         SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new BoutFilterModel();
			filter.YearOfRestId = filter.YearOfRestId ?? ApiYearController.GetCurrentYearId();
			filter.Result = ApiController.Get(filter);
			filter.GroupedTimesOfRest = ApiGroupedTimeOfRestController.Get();
			filter.Hotels = filter.HotelsId.HasValue ? ApiHotelsController.Get(filter.HotelsId.Value) : null;
			filter.States =
				ApiController.GetStates().InsertAt(new StateMachineState {Id = (long) BoutAggregatedStates.New, Name = "Новый"});
			filter.YearsOfRest = ApiYearController.Get().ToList();
			filter.HotelTypes = ApiHotelTypeController.Get().ToList();
			return View("BoutList", filter);
		}

		public ActionResult Insert(long? hotelId, long? groupedTimeOfRestId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = new Bout
			{
				StateId = StateMachineStateEnum.Bout.Editing,
				State = new StateMachineState
				{
					Name = "Новый"
				},
				HotelsId = hotelId,
				GroupedTimeOfRestId = groupedTimeOfRestId,
				Hotels = ApiHotelsController.Get(hotelId ?? 0),
				GroupedTimeOfRest = ApiGroupedTimeOfRestController.Get(groupedTimeOfRestId ?? 0),
				Tours = new List<Tour>()
			};
			return GetViewResult(bout);
		}

		public ActionResult Update(long id, string actionCode, string activeTab)
		{
			try
			{
				SetUnitOfWorkInRefClass(UnitOfWork);
				var bout = ApiController.Get(id);
				if (bout == null)
				{
					return RedirectToAction("Search");
				}

				return GetViewResult(bout, actionCode, activeTab);
			}
			catch (HttpResponseException e)
			{
				Logger.Error("Ошибка открытия заезда", e);
				return RedirectToAction("Search");
			}
		}

		public ActionResult GetPersonal(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = ApiController.Get(id);
			var boutModel = new BoutModel(bout) {IsEditable = bout.StateId == StateMachineStateEnum.Bout.Editing};
			SetupTransportVocabularies(boutModel);
			return View("BoutPersonal", boutModel);
		}

		/// <summary>
		/// сохранение заезда
		/// </summary>
		public ActionResult Save(BoutModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = model.BuildData();
			if (!ModelState.IsValid && bout.StateId == StateMachineStateEnum.Bout.Editing)
			{
				bout.Hotels = bout.HotelsId.HasValue ? ApiHotelsController.Get(bout.HotelsId.Value) : null;
				return GetViewResult(model.BuildData(), model.StateMachineActionString, model.ActiveTab);
			}
			if (bout.Id == 0)
			{
				ApiController.Post(bout);
			}
			else
			{
				var entity = UnitOfWork.GetById<Bout>(bout.Id);
				if (entity == null)
				{
					return RedirectToAction("Search");
				}

				if (bout.LastUpdateTick != entity.LastUpdateTick)
				{
					SetRedicted();
					return RedirectToAction("Update", new { id = bout.Id, activeTab = model.ActiveTab });
				}

				bout = ApiController.Put(bout.Id, bout);

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var errors = ApiController.ChangeStatus(bout.Id, model.StateMachineActionString);
					if (errors.Any())
					{
						SetErrors(errors);
					}
				}
			}

			return RedirectToAction("Update", new {id = bout.Id, activeTab = model.ActiveTab});
		}

		private ActionResult GetViewResult(Bout bout, string actionCode = null, string activeTab = null)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var state = bout.State ?? (bout.StateId.HasValue ? ApiStateController.GetState(bout.StateId.Value) : null);
			var actions = bout.Id != 0 ? ApiStateController.GetActions(state, StateMachineEnum.BoutState) : new List<StateMachineAction>();
			var requests =
				bout.Chidren.Where(c => !c.IsDeleted)
					.Select(c => c.Request)
					.Union(bout.Applicants.Select(c => c.Request))
					.Where(r => r != null)
					.ToList();
			var addonApplicants = bout.Applicants.Where(a => a.IsAccomp && a.Request == null);
			foreach (var addonApplicant in addonApplicants)
			{
				if (!requests.Any(r => r.ApplicantId == addonApplicant.Id))
				{
					var findedRequest = UnitOfWork.GetSet<Request>().FirstOrDefault(r => r.ApplicantId == addonApplicant.Id);
					if (findedRequest != null)
					{
						if (findedRequest.ParentRequestId.HasValue)
						{
							try
							{
								requests.Insert(requests.FindIndex(p => p.Id == findedRequest.ParentRequestId) + 1, findedRequest);
							}
							catch
							{
								requests.Add(findedRequest);
							}
						}
						else
						{
							requests.Add(findedRequest);
						}
					}
				}
			}

			var isEditable = bout.StateId != StateMachineStateEnum.Bout.Closed;
			var result = new BoutModel(bout)
			{
				IsEditable = isEditable,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#boutForm",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "Bout",
						ReturnAction = "Search",
						NeedSaveButton = isEditable,
						JsFunctionToAction = "confirmStateButtonBout",
						PostNoStatusActions = new List<NoStatusAction>
						{
							new NoStatusAction
							{
								Name = "Печать",
								IconClass = "glyphicon-print",
								Controller = "Bout",
								Action = "GetSpreadsheet",
								ActionParameters = new {id = bout.Id}
							}
						}
					},
				ActiveTab = activeTab,
				Requests = requests
			};

			if (bout.HistoryLinkId.HasValue)
			{
				result.State.PostNoStatusActions = result.State.PostNoStatusActions ?? new List<NoStatusAction>();
				result.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = $"data-history-id=\"{bout.HistoryLinkId}\""
				});
			}

			if (result.State.Actions != null && !result.CanTransferToConfirmedState())
			{
				result.State.Actions = result.State.Actions.Where(a => a.Id != StateMachineStateEnum.Bout.Confirmed).ToList();
			}

			if (!string.IsNullOrEmpty(actionCode))
			{
				var errors = ApiController.GetErrorsOfChageStatus(bout.Id, actionCode);
				if (errors != null && errors.Any())
				{
					foreach (var error in errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}
				}
			}
			SetupVocabularies(result);
			return View("BoutEdit", result);
		}

		/// <summary>
		///     Выгрузка по заезду
		/// </summary>
		/// <returns></returns>
		public ActionResult GetSpreadsheet(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = ApiController.GetWithChilds(id);
			if (bout == null)
			{
				return RedirectToAction("Search");
			}

			if (bout.NullSafe(b => b.Hotels.HotelTypeId) == (long) HotelTypeEnum.Camp)
			{
				return GetSpreadsheetForCamp(bout);
			}
			return GetSpreadsheetForHotel(bout);
		}

		/// <summary>
		///     Выгрузка по заезду в лагерь
		/// </summary>
		/// <returns></returns>
		private ActionResult GetSpreadsheetForCamp(Bout bout)
		{
			var campersColumns = CreateCampersColumns();
			var personalColumns = CreatePersonalExcelColumns();


			using (var excel = new ExcelTable<CamperModel>(personalColumns))
			{
				const int startRow = 5;


				FormPersonal(bout, excel, startRow);
				excel.ColumnsInternal = campersColumns;
				FormPartiesSpreadsheet(bout, excel, startRow);

				return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Заезд.xlsx");
			}
		}

		private static List<ExcelColumn<CamperModel>> CreateCampersColumns()
		{
			var campersColumns = new List<ExcelColumn<CamperModel>>
			{
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Name.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Category.FormatEx(false),
					Title = "Ребенок/вожатый",
					WordWrap = true,
					Width = 20
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.RequestNumber.FormatEx(false),
					Title = "Номер заявления",
					WordWrap = true,
					Width = 35
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Certificate.FormatEx(false),
					Title = "Номер путевки",
					WordWrap = true,
					Width = 16
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => !c.IsMale.HasValue ? "-" : c.IsMale.Value ? "Мужской" : "Женский",
					Title = "Пол",
					WordWrap = true,
					Width = 11
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.BirthDate.FormatEx(),
					Title = "Дата рождения",
					WordWrap = true,
					Width = 16
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => StaticHelpers.GetAgeInYears(c.BirthDate, c.DateIncome).FormatEx(),
					Title = "Возраст",
					WordWrap = true,
					Width = 10
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => $"{c.DocSeries} {c.DocNumber}".FormatEx(false),
					Title = "Номер документа",
					WordWrap = true,
					Width = 20
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.DocIssueDate.FormatEx(),
					Title = "Дата выдачи документа",
					WordWrap = true,
					Width = 25
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.DocIssue.FormatEx(),
					Title = "Кем выдан документ",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.BirthPlace.FormatEx(false),
					Title = "Место рождения",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Address,
					Title = "Адрес регистрации",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.ApplicantName.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.ApplicantEmail.FormatEx(false),
					Title = "E-Mail",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.ApplicantPhone.FormatEx(false),
					Title = "Телефон",
					WordWrap = true,
					Width = 18
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.SubjectOfRest.FormatEx(false),
					Title = "Программа",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.GroupedDateTime.FormatEx(false),
					Title = "Даты путешествия/даты смены",
					WordWrap = true,
					Width = 32
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.MotherFio.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.MotherBirthDate.FormatEx(false),
					Title = "Дата рождения",
					WordWrap = true,
					Width = 19
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.FatherFio.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.FatherBirthDate.FormatEx(false),
					Title = "Дата рождения",
					WordWrap = true,
					Width = 19
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.NotNeedTicketReasonTo.FormatEx(false),
					Title = "В место отдыха",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.NotNeedTicketReasonFrom.FormatEx(false),
					Title = "Из места отдыха",
					WordWrap = true
				}
			};
			return campersColumns;
		}

		private static List<ExcelColumn<CamperModel>> CreatePersonalExcelColumns()
		{
			var personalColumns = new List<ExcelColumn<CamperModel>>
			{
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.Name.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.Category.FormatEx(false),
					Title = "Должность",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.RequestNumber.FormatEx(
								false),
					Title = "Номер заявления",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.Certificate.FormatEx(
								false),
					Title = "Номер путевки",
					WordWrap = true,
					Width = 25
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.BirthDate.FormatEx(),
					Title = "Дата рождения",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							string.Format(
								"{0} {1}",
								c.DocSeries,
								c.DocNumber)
								.FormatEx(false),
					Title = "Номер документа",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.DocIssueDate.FormatEx(),
					Title =
						"Дата выдачи документа",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.DocIssue,
					Title =
						"Кем выдан документ",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.BirthPlace.FormatEx(
								false),
					Title = "Место рождения",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.GroupedDateTime.FormatEx
								(false),
					Title =
						"Даты путешествия/даты смены",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func =
						c =>
							c.Phone.FormatEx(false),
					Title =
						"Телефон",
					WordWrap = true
				},
			};
			return personalColumns;
		}

		private static void FormPersonal(Bout bout, ExcelTable<CamperModel> excel, int startRow)
		{
			var dateIncome = bout.DateIncome ?? bout.Tours.FirstOrDefault()?.DateIncome;
			var dateOutcome = bout.DateOutcome ?? bout.Tours.FirstOrDefault()?.DateOutcome;

			var personal = new List<CamperModel>();

			if (bout.AdministratorTours != null)
			{
				personal.AddRange(
					bout.AdministratorTours.Select(
						c =>
							new CamperModel
							{
								Name = c.GetFio(),
								Category = "Администратор смены",
								BirthDate = c.DateOfBirth,
								DocSeries = c.DocumentSeria,
								DocNumber = c.DocumentNumber,
								DocIssueDate = c.DocumentDateOfIssue,
								DocIssue = c.DocumentSubjectIssue,
								BirthPlace = c.PlaceOfBirth,
								Address = "-",
								DateOutcome = dateOutcome,
								DateIncome = dateIncome,
								GroupedDateTime =
									bout.GroupedTimeOfRest != null
										? $"{bout.GroupedTimeOfRest.Name}, {bout.DateIncome.FormatEx(string.Empty)}-{bout.DateOutcome.FormatEx(string.Empty)}"
										: $"{bout.DateIncome.FormatEx()}-{bout.DateOutcome.FormatEx()}",
								Phone = c.Phone
							}));
			}

			if (bout.SeniorCounselors != null)
			{
				personal.AddRange(
					bout.SeniorCounselors.Select(
						c =>
							new CamperModel
							{
								Name = c.GetFio(),
								Category = "Старший вожатый",
								BirthDate = c.DateOfBirth,
								DocIssue = c.DocumentSubjectIssue,
								DocSeries = c.DocumentSeria,
								DocNumber = c.DocumentNumber,
								DocIssueDate = c.DocumentDateOfIssue,
								BirthPlace = c.PlaceOfBirth,
								Address = "-",
								DateOutcome = dateOutcome,
								DateIncome = dateIncome,
								IsMale = c.Male,
								GroupedDateTime =
									bout.GroupedTimeOfRest != null
										? $"{bout.GroupedTimeOfRest.Name}, {bout.DateIncome.FormatEx(string.Empty)}-{bout.DateOutcome.FormatEx(string.Empty)}"
										: $"{bout.DateIncome.FormatEx()}-{bout.DateOutcome.FormatEx()}",
								Phone = c.Phone
							}));
			}

			if (bout.SwingCounselors != null)
			{
				personal.AddRange(
					bout.SwingCounselors.Select(
						c =>
							new CamperModel
							{
								Name = c.GetFio(),
								Category = "Подменный вожатый",
								BirthDate = c.DateOfBirth,
								DocSeries = c.DocumentSeria,
								DocNumber = c.DocumentNumber,
								DocIssueDate = c.DocumentDateOfIssue,
								BirthPlace = c.PlaceOfBirth,
								Address = "-",
								DocIssue = c.DocumentSubjectIssue,
								DateOutcome = dateOutcome,
								DateIncome = dateIncome,
								IsMale = c.Male,
								GroupedDateTime =
									bout.GroupedTimeOfRest != null
										? $"{bout.GroupedTimeOfRest.Name}, {bout.DateIncome.FormatEx(string.Empty)}-{bout.DateOutcome.FormatEx(string.Empty)}"
										: $"{bout.DateIncome.FormatEx()}-{bout.DateOutcome.FormatEx()}",
								Phone = c.Phone
							}));
			}

			var personalWorksheet = excel.CreateExcelWorksheet("Персонал");
			if (personal.Any())
			{
				excel.DataBind(
					personalWorksheet,
					personal.OrderBy(m => m.LastName).ThenBy(m => m.Name),
					ExcelBorderStyle.Thin,
					startRow);

				personalWorksheet.Cells[1, 1].Value = "Даты путешествия/даты смены";
				personalWorksheet.Column(1).Width = 31;
				personalWorksheet.Cells[2, 1].Value = "Лагерь";
				personalWorksheet.Cells[3, 1].Value = "Статус";

				personalWorksheet.Cells[1, 2].Value = bout.GroupedTimeOfRest != null
					? bout.NullSafe(b => b.GroupedTimeOfRest.Name).FormatEx(false)
					: bout.DateIncome.FormatEx();
				personalWorksheet.Cells[2, 2].Value = bout.NullSafe(b => b.Hotels.Name).FormatEx(false);
				personalWorksheet.Cells[3, 2].Value = bout.State?.Name.FormatEx(false);
			}
			else
			{
				personalWorksheet.Cells[1, 1].Value = "Даты путешествия/даты смены";
				personalWorksheet.Cells[2, 1].Value = "Лагерь";
				personalWorksheet.Cells[3, 1].Value = "Статус";
				personalWorksheet.Column(1).Width = 31;

				personalWorksheet.Cells[1, 2].Value = bout.GroupedTimeOfRest != null
					? bout.NullSafe(b => b.GroupedTimeOfRest.Name).FormatEx(false)
					: bout.DateIncome.FormatEx();
				personalWorksheet.Cells[2, 2].Value = bout.NullSafe(b => b.Hotels.Name).FormatEx(false);
				personalWorksheet.Cells[3, 2].Value = bout.State?.Name.FormatEx(false);
				personalWorksheet.Cells[4, 1].Value = "Персонал отсутствует";
			}
		}

		private static void FormPartiesSpreadsheet(Bout bout, ExcelTable<CamperModel> excel, int startRow)
		{
			if (bout.Partys != null && bout.Partys.Any())
			{
				var dateIncome = bout.DateIncome ?? bout.Tours.FirstOrDefault()?.DateIncome;
				var dateOutcome = bout.DateOutcome ?? bout.Tours.FirstOrDefault()?.DateOutcome;
				var hashSet = new HashSet<string>();


				foreach (var party in bout.Partys.Where(p => p.StateId != StateMachineStateEnum.Deleted))
				{
					var wsName = "Отряд " + party.PartyNumber;
					var index = 0;
					var name = wsName + (index == 0 ? string.Empty : $"_{index}");
					while (hashSet.Contains(name))
					{
						index++;
						name = wsName + (index == 0 ? string.Empty : $"_{index}");
					}

					var excelWorksheet = excel.CreateExcelWorksheet(name);
					hashSet.Add(name);
					var childs = party.Childs?.Where(c => !c.IsDeleted).ToList() ?? new List<Child>();
					var counselors = party.Counselors?.Where(c => c.StateId != StateMachineStateEnum.Deleted).ToList() ??
					                 new List<Counselors>();

					var campers = childs.Select(
						c =>
						{
							var groupedTimeOfRest = c.Bout != null ? c.Bout.NullSafe(b => b.GroupedTimeOfRest.Name) : string.Empty;
							var tour = c.Request?.Tour ?? c.ChildList?.Tour;
							var certificate = c.Request != null
								? c.Request.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest
								  && c.Request.ParentRequest != null
									? c.Request.ParentRequest.CertificateNumber
									: c.Request.CertificateNumber
								: null;
							var relationship =
								c.BaseRegistryInfo.FirstOrDefault(
									b => !b.NotActual && b.ExchangeBaseRegistryTypeId == (long)ExchangeBaseRegistryTypeEnum.Relationship).Parse()?.RelationshipCheckResults?.FirstOrDefault();

							var linkTo = c.LinkToPeoples.FirstOrDefault(l => l.BoutId == c.BoutId && l.TransportId == c.Bout.TransportInfoToId);
							var linkFrom = c.LinkToPeoples.FirstOrDefault(l => l.BoutId == c.BoutId && l.TransportId == c.Bout.TransportInfoFromId);

							return new CamperModel
							{
								Name = c.GetFio(),
								Category = "Ребенок",
								BirthDate = c.DateOfBirth,
								DocSeries = c.DocumentSeria,
								DocNumber = c.DocumentNumber,
								DocIssueDate = c.DocumentDateOfIssue,
								BirthPlace = c.PlaceOfBirth,
								Address = c.Address != null ? c.Address.ToString().FormatEx(false) : "-",
								ApplicantName =
									c.Request != null && c.Request.Applicant != null
										? c.Request.Applicant.GetFio()
										: null,
								ApplicantEmail =
									c.Request != null && c.Request.Applicant != null ? c.Request.Applicant.Email : null,
								ApplicantPhone =
									c.Request != null && c.Request.Applicant != null ? c.Request.Applicant.Phone : null,
								SubjectOfRest =
									(c.Request != null && c.Request.SubjectOfRest != null
										? c.Request.SubjectOfRest.Name
										: string.Empty).FormatEx(false),
								DateIncome = tour?.DateIncome ?? dateIncome,
								DateOutcome = tour?.DateOutcome ?? dateOutcome,
								GroupedDateTime =
									tour != null
										? $"{groupedTimeOfRest}, {tour.DateIncome.FormatEx(string.Empty)}-{tour.DateOutcome.FormatEx(string.Empty)}"
										: groupedTimeOfRest,
								RequestNumber = c.Request?.RequestNumber,
								Certificate = certificate,
								IsMale = c.Male,
								DocIssue = c.DocumentSubjectIssue,
								FatherFio = $"{relationship?.FatherLastName} {relationship?.FatherFirstName} {relationship?.FatherPatronymic}",
								FatherBirthDate = $"{relationship?.FatherBirthDate.FormatEx()}",
								MotherFio = $"{relationship?.MotherLastName} {relationship?.MotherFirstName} {relationship?.MotherPatronymic}",
								MotherBirthDate = $"{relationship?.MotherBirthDate.FormatEx()}",
								NotNeedTicketReasonFrom = linkFrom?.NotNeedTicketReason?.Name,
								NotNeedTicketReasonTo = linkTo?.NotNeedTicketReason?.Name
							};
						}).ToList();

					campers.AddRange(
						counselors.Select(
							c =>
								new CamperModel
								{
									Name = c.GetFio(),
									Category = "Вожатый",
									BirthDate = c.DateOfBirth,
									DocSeries = c.DocumentSeria,
									DocNumber = c.DocumentNumber,
									DocIssueDate = c.DocumentDateOfIssue,
									DocIssue = c.DocumentSubjectIssue,
									BirthPlace = c.PlaceOfBirth,
									Address = "-",
									IsMale = c.Male,
									ApplicantName = null,
									ApplicantEmail = null,
									ApplicantPhone = null,
									SubjectOfRest = null,
									DateOutcome = dateOutcome,
									DateIncome = dateIncome,
									GroupedDateTime =
										bout.GroupedTimeOfRest != null
											? $"{bout.GroupedTimeOfRest.Name}"
											: $"{bout.DateIncome.FormatEx()}-{bout.DateOutcome.FormatEx()}"
								})
							.ToList());

					if (party.Childs != null && party.Childs.Any())
					{
						excel.DataBind(
							excelWorksheet,
							campers.OrderBy(m => m.Category).ThenBy(m=>m.LastName).ThenBy(m => m.Name).ThenBy(m=>m.MiddleName),
							ExcelBorderStyle.Thin,
							startRow);
					}
					excelWorksheet.Cells[1, 1].Value = "Лагерь";
					excelWorksheet.Cells[2, 1].Value = "Отряд";
					excelWorksheet.Cells[3, 1].Value = "Статус отряда";

					excelWorksheet.Cells[1, 2].Value = bout.NullSafe(b => b.Hotels.Name).FormatEx(false);
					excelWorksheet.Cells[2, 2].Value =
						(party.PartyNumber.HasValue ? "Отряд " + party.PartyNumber : null).FormatEx(false);
					excelWorksheet.Cells[3, 2].Value = party.State?.Name.FormatEx(false);

					var firstColumnBlock = excelWorksheet.Cells[4, 1, 4, 12];
					var secondColumnBlock = excelWorksheet.Cells[4, 13, 4, 15];
					var thirthColumnBlock = excelWorksheet.Cells[4, 16, 4, 17];
					var fourthColumnBlock = excelWorksheet.Cells[4, 18, 4, 19];
					var fifthColumnBlock = excelWorksheet.Cells[4, 20, 4, 21];
					var sixthColumnBlock = excelWorksheet.Cells[4, 22, 4, 23];

					firstColumnBlock.Merge = true;
					secondColumnBlock.Merge = true;
					thirthColumnBlock.Merge = true;
					fourthColumnBlock.Merge = true;
					fifthColumnBlock.Merge = true;
					sixthColumnBlock.Merge = true;

					firstColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					firstColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					firstColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					firstColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

					secondColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					secondColumnBlock.Style.Font.Bold = true;
					secondColumnBlock.Value = "Заявитель";

					thirthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					thirthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					thirthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					thirthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

					fourthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					fourthColumnBlock.Style.Font.Bold = true;
					fourthColumnBlock.Value = "Мать";

					fifthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					fifthColumnBlock.Style.Font.Bold = true;
					fifthColumnBlock.Value = "Отец";

					sixthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					sixthColumnBlock.Style.Font.Bold = true;
					sixthColumnBlock.Value = "Отказ от билетов";


					excelWorksheet.Cells.Style.Font.Size = 12;
				}
			}
		}

		/// <summary>
		///     Выгрузка по заезду в отель
		/// </summary>
		/// <returns></returns>
		private ActionResult GetSpreadsheetForHotel(Bout bout)
		{
			var campers = new List<CamperModel>();
			var requests = new List<Request>();

			if (bout.Chidren != null)
			{
				requests.AddRange(bout.Chidren.Select(c => c.Request).Where(r => r != null).ToList());

				campers.AddRange(
					bout.Chidren.Where(c => !c.IsDeleted).Select(
						c =>
						{
							var certificate = c.Request != null
								? c.Request.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest
								  && c.Request.ParentRequest != null
									? c.Request.ParentRequest.CertificateNumber
									: c.Request.CertificateNumber
								: null;

							var relationship =
								c.BaseRegistryInfo.FirstOrDefault(
									b => !b.NotActual && b.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Relationship).Parse()?.RelationshipCheckResults?.FirstOrDefault();

							var linkTo = c.LinkToPeoples.FirstOrDefault(l => l.BoutId == c.BoutId && l.TransportId == c.Bout.TransportInfoToId);
							var linkFrom = c.LinkToPeoples.FirstOrDefault(l => l.BoutId == c.BoutId && l.TransportId == c.Bout.TransportInfoFromId);

							return new CamperModel
							{
								Name = c.GetFio(),
								Category = "Ребенок",
								BirthDate = c.DateOfBirth,
								DocSeries = c.DocumentSeria,
								DocNumber = c.DocumentNumber,
								DocIssueDate = c.DocumentDateOfIssue,
								DocIssue = c.DocumentSubjectIssue,
								BirthPlace = c.PlaceOfBirth,
								Address = c.Address != null ? c.Address.ToString().FormatEx(false) : "-",
								RequestId = c.RequestId,
								IsMale = c.Male,
								ApplicantName =
									c.Request != null && c.Request.Applicant != null
										? c.Request.Applicant.GetFio()
										: string.Empty,
								ApplicantEmail =
									c.Request != null && c.Request.Applicant != null
										? c.Request.Applicant.Email
										: string.Empty,
								ApplicantPhone =
									c.Request != null && c.Request.Applicant != null
										? c.Request.Applicant.Phone
										: string.Empty,
								DateIncome =
									c.Request != null && c.Request.Tour != null
										? c.Request.Tour.DateIncome
										: null,
								DateOutcome =
									c.Request != null && c.Request.Tour != null
										? c.Request.Tour.DateOutcome
										: null,
								RequestNumber = c.Request != null ? c.Request.RequestNumber : string.Empty,
								Certificate = certificate,
								FatherFio = $"{relationship?.FatherLastName} {relationship?.FatherFirstName} {relationship?.FatherPatronymic}",
								FatherBirthDate = $"{relationship?.FatherBirthDate.FormatEx()}",
								MotherFio = $"{relationship?.MotherLastName} {relationship?.MotherFirstName} {relationship?.MotherPatronymic}",
								MotherBirthDate = $"{relationship?.MotherBirthDate.FormatEx()}",
								NotNeedTicketReasonFrom = linkFrom?.NotNeedTicketReason?.Name,
								NotNeedTicketReasonTo = linkTo?.NotNeedTicketReason?.Name
							};
						}));
			}


			if (bout.Applicants != null)
			{
				var applicantIds = bout.Applicants.Select(a => a.Id).ToList();
				requests.AddRange(bout.Applicants.Select(c => c.Request).Where(r => r != null).ToList());
				requests.AddRange(UnitOfWork.GetSet<Request>().Where(r => applicantIds.Contains(r.ApplicantId ?? 0)).ToList());

				campers.AddRange(
					bout.Applicants.Select(
						c =>
						{
							var request = c.Request ?? requests.FirstOrDefault(r => r.ApplicantId == c.Id);
							var requestId = request?.Id;
							var certificate = request != null
								? request.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest
								  && request.ParentRequest != null
									? request.ParentRequest.CertificateNumber
									: request.CertificateNumber
								: null;

							var linkTo = c.LinkToPeoples.FirstOrDefault(l => l.BoutId == c.BoutId && l.TransportId == c.Bout.TransportInfoToId);
							var linkFrom = c.LinkToPeoples.FirstOrDefault(l => l.BoutId == c.BoutId && l.TransportId == c.Bout.TransportInfoFromId);
							return new CamperModel
							{
								Name = c.GetFio(),
								Category = "Сопровождающий",
								BirthDate = c.DateOfBirth,
								DocSeries = c.DocumentSeria,
								IsMale = c.Male,
								DocNumber = c.DocumentNumber,
								DocIssueDate = c.DocumentDateOfIssue,
								DocIssue = c.DocumentSubjectIssue,
								BirthPlace = c.PlaceOfBirth,
								Address = "-",
								RequestId = requestId,
								ApplicantName =
									request != null && request.Applicant != null
										? request.Applicant.GetFio()
										: string.Empty,
								ApplicantEmail =
									request?.Applicant != null
										? request.Applicant.Email
										: string.Empty,
								ApplicantPhone =
									request != null && request.Applicant != null
										? request.Applicant.Phone
										: string.Empty,
								DateIncome =
									request != null && request.Tour != null
										? request.Tour.DateIncome
										: null,
								DateOutcome =
									request != null && request.Tour != null
										? request.Tour.DateOutcome
										: null,
								RequestNumber = request != null ? request.RequestNumber : string.Empty,
								Certificate = certificate,
								NotNeedTicketReasonFrom = linkFrom?.NotNeedTicketReason?.Name,
								NotNeedTicketReasonTo = linkTo?.NotNeedTicketReason?.Name
							};
						}));
			}
			var hotelCampersColumns = CreateHotelCampersColumns();
			var personalColumns = CreatePersonalExcelColumns();

			using (var excel = new ExcelTable<CamperModel>(personalColumns))
			{
				const int startRow = 5;

				FormPersonal(bout, excel, startRow - 1);
				excel.ColumnsInternal = hotelCampersColumns;
				var excelWorksheet = excel.CreateExcelWorksheet("Заезд");

				if (campers.Any())
				{
					excel.DataBind(
						excelWorksheet,
						campers.OrderBy(m => m.Certificate).ThenBy(m => m.Name),
						ExcelBorderStyle.Thin,
						startRow);
					excelWorksheet.Cells[1, 1].Value = "Даты путешествия/даты смены";
					excelWorksheet.Cells[2, 1].Value = "Отель";
					excelWorksheet.Cells[3, 1].Value = "Статус";
					excelWorksheet.Column(1).Width = 31;
					excelWorksheet.Cells[1, 2].Value = bout.GroupedTimeOfRest != null
						? bout.NullSafe(b => b.GroupedTimeOfRest.Name).FormatEx(false)
						: bout.DateIncome.FormatEx();
					excelWorksheet.Cells[2, 2].Value = bout.NullSafe(b => b.Hotels.Name).FormatEx(false);
					excelWorksheet.Cells[3, 2].Value = bout.State?.Name.FormatEx(false);

					var firstColumnBlock = excelWorksheet.Cells[4, 1, 4, 11];
					var secondColumnBlock = excelWorksheet.Cells[4, 12, 4, 14];
					var thirthColumnBlock = excelWorksheet.Cells[4, 15, 4, 15];
					var fourthColumnBlock = excelWorksheet.Cells[4, 16, 4, 17];
					var fifthColumnBlock = excelWorksheet.Cells[4, 18, 4, 19];
					var sixthColumnBlock = excelWorksheet.Cells[4, 20, 4, 21];

					firstColumnBlock.Merge = true;
					secondColumnBlock.Merge = true;
					fourthColumnBlock.Merge = true;
					fifthColumnBlock.Merge = true;
					sixthColumnBlock.Merge = true;

					firstColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					firstColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					firstColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					firstColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

					secondColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					secondColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					secondColumnBlock.Style.Font.Bold = true;
					secondColumnBlock.Value = "Заявитель";

					thirthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					thirthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					thirthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					thirthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

					fourthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					fourthColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					fourthColumnBlock.Style.Font.Bold = true;
					fourthColumnBlock.Value = "Мать";

					fifthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					fifthColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					fifthColumnBlock.Style.Font.Bold = true;
					fifthColumnBlock.Value = "Отец";

					sixthColumnBlock.Style.Border.Left.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.Border.Top.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.Border.Right.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					sixthColumnBlock.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					sixthColumnBlock.Style.Font.Bold = true;
					sixthColumnBlock.Value = "Отказ от билетов";

					excelWorksheet.Cells.Style.Font.Size = 12;
				}
				else
				{
					excelWorksheet.Cells[1, 1].Value = "Даты путешествия/даты смены";
					excelWorksheet.Cells[2, 1].Value = "Отель";
					excelWorksheet.Cells[3, 1].Value = "Статус";
					excelWorksheet.Column(1).Width = 31;

					excelWorksheet.Cells[1, 2].Value = bout.GroupedTimeOfRest != null
						? bout.NullSafe(b => b.GroupedTimeOfRest.Name).FormatEx(false)
						: bout.DateIncome.FormatEx();
					excelWorksheet.Cells[2, 2].Value = bout.NullSafe(b => b.Hotels.Name).FormatEx(false);
					excelWorksheet.Cells[3, 2].Value = bout.State?.Name.FormatEx(false);
					excelWorksheet.Cells[4, 1].Value = "Отдыхающие отсутствуют";
				}

				return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Заезд.xlsx");
			}
		}

		private static List<ExcelColumn<CamperModel>> CreateHotelCampersColumns()
		{
			var hotelCampersColumns = new List<ExcelColumn<CamperModel>>
			{
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Name.FormatEx(false),
					Title = "ФИО",
					WordWrap = true,
					Width = 38
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Category.FormatEx(false),
					Title = "Ребенок / сопровождающий",
					WordWrap = true,
					Width = 31
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.RequestNumber.FormatEx(false),
					Title = "Номер заявления",
					WordWrap = true,
					Width = 38
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Certificate.FormatEx(false),
					Title = "Номер путевки",
					WordWrap = true,
					Width = 18
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.BirthDate.FormatEx(),
					Title = "Дата рождения",
					WordWrap = true,
					Width = 17
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => StaticHelpers.GetAgeInYears(c.BirthDate, c.DateIncome).FormatEx(),
					Title = "Возраст",
					WordWrap = true,
					Width = 10
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => $"{c.NullSafe(cm => cm.DocSeries)} {c.NullSafe(cm => cm.DocNumber)}".FormatEx(false),
					Title = "Номер документа",
					WordWrap = true,
					Width = 19
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.DocIssueDate.FormatEx(),
					Title = "Дата выдачи документа",
					WordWrap = true,
					Width = 25
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.DocIssue.FormatEx(),
					Title = "Кем выдан документ",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.BirthPlace.FormatEx(false),
					Title = "Место рождения",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.Address,
					Title = "Адрес регистрации",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.ApplicantName.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.ApplicantEmail.FormatEx(false),
					Title = "E-Mail",
					WordWrap = true,
					Width = 30
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.ApplicantPhone.FormatEx(false),
					Title = "Телефон",
					WordWrap = true,
					Width = 20
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => $"{c.DateIncome.FormatEx()}-{c.DateOutcome.FormatEx()}",
					Title = "Даты путешествия/даты смены",
					WordWrap = true,
					Width = 33
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.MotherFio.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.MotherBirthDate.FormatEx(false),
					Title = "Дата рождения",
					WordWrap = true,
					Width = 19
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.FatherFio.FormatEx(false),
					Title = "ФИО",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.FatherBirthDate.FormatEx(false),
					Title = "Дата рождения",
					WordWrap = true,
					Width = 19
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.NotNeedTicketReasonTo.FormatEx(false),
					Title = "В место отдыха",
					WordWrap = true
				},
				new ExcelColumn<CamperModel>
				{
					Func = c => c.NotNeedTicketReasonFrom.FormatEx(false),
					Title = "Из места отдыха",
					WordWrap = true
				}
			};

			return hotelCampersColumns;
		}

		private void SetupVocabularies(BoutModel bout)
		{
			bout.TimesOfRest = ApiRestTimeController.Get().ToList();
			bout.IsBoutAdministartor =
				ApiAdministratorTourController.GetAdministratorToursForAccount()
					.Any(a => a.Bouts != null && a.Bouts.Any(b => b.Id == bout.Data.Id));
			bout.NotNeedTicketReasons = ApiVocabularyController.GetActiveNotNeedTicketReasons();
			SetupTransportVocabularies(bout);
		}

		private void SetupTransportVocabularies(BoutModel bout)
		{
			var administratorsIds = bout.Data.AdministratorTours != null
				? bout.Data.AdministratorTours.Select(t => t.Id).ToList()
				: new List<long>();
			var seniorCounselorsIds = bout.Data.SeniorCounselors != null
				? bout.Data.SeniorCounselors.Select(t => t.Id).ToList()
				: new List<long>();
			var swingCounselorsIds = bout.Data.SwingCounselors != null
				? bout.Data.SwingCounselors.Select(t => t.Id).ToList()
				: new List<long>();

			var administratorTransport =
				ApiTransportInfoController.GetLinksToPeople()
					.Where(
						l =>
							l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator && l.AdministratorTourId.HasValue
							&& administratorsIds.Contains(l.AdministratorTourId.Value)
							&& l.BoutId == bout.Data.Id)
					.ToList();

			bout.AdministratorTransportForward =
				administratorTransport.Where(l => l.Transport != null && l.Transport.DepartureId == (long) CityEnum.Moscow)
					.GroupBy(l => l.AdministratorTourId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.AdministratorTourId.Value);
			bout.AdministratorTransportBackward =
				administratorTransport.Where(l => l.Transport != null && l.Transport.ArrivalId == (long) CityEnum.Moscow)
					.GroupBy(l => l.AdministratorTourId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.AdministratorTourId.Value);

			var counselorTransport =
				ApiTransportInfoController.GetLinksToPeople()
					.Where(
						l =>
							(l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor
							 || l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor) && l.CounselorsId.HasValue
							&& (seniorCounselorsIds.Contains(l.CounselorsId.Value) || (swingCounselorsIds.Contains(l.CounselorsId.Value)))
							&& l.BoutId == bout.Data.Id)
					.ToList();

			bout.CounselorTransportForward =
				counselorTransport.Where(l => l.Transport != null && l.Transport.DepartureId == (long) CityEnum.Moscow)
					.GroupBy(l => l.CounselorsId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.CounselorsId.Value);

			bout.CounselorTransportBackward =
				counselorTransport.Where(l => l.Transport != null && l.Transport.ArrivalId == (long) CityEnum.Moscow)
					.GroupBy(l => l.CounselorsId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.CounselorsId.Value);


			var attendantTransport =
				ApiTransportInfoController.GetLinksToPeople()
					.Where(
						l =>
							(l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant)
							&& l.ApplicantId.HasValue
							&& (seniorCounselorsIds.Contains(l.ApplicantId.Value) || (swingCounselorsIds.Contains(l.ApplicantId.Value)))
							&& l.BoutId == bout.Data.Id)
					.ToList();

			bout.AttendantTransportForward =
				attendantTransport.Where(l => l.Transport != null && l.Transport.DepartureId == (long) CityEnum.Moscow)
					.GroupBy(l => l.ApplicantId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.ApplicantId.Value);

			bout.AttendantTransportBackward =
				attendantTransport.Where(l => l.Transport != null && l.Transport.ArrivalId == (long) CityEnum.Moscow)
					.GroupBy(l => l.ApplicantId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.ApplicantId.Value);
		}

		/// <summary>
		///     работа с журналом
		/// </summary>
		public ActionResult BoutJournal(long? id, long? boutId,int? boutJournalType)
		{
			if ((!id.HasValue || id <= 0) && (!boutId.HasValue || boutId <= 0))
			{
				return RedirectToAction("Search");
			}

			var journal = UnitOfWork.GetById<BoutJournal>(id ?? 0) ??
			              new BoutJournal {Bout = UnitOfWork.GetById<Bout>(boutId ?? 0), BoutId = boutId, BoutJournalTypeId = boutJournalType};
			var model = new BoutJournalModel(journal);
			model.Partys =
				UnitOfWork.GetSet<Party>()
					.Where(p => p.BoutsId == model.Data.BoutId && p.StateId == StateMachineStateEnum.Party.Formed)
					.OrderBy(p => p.PartyNumber)
					.ToList()
					.Select(p => new Party(p))
					.ToList();

			if (journal.BoutJournalTypeId == (long) BoutJournalTypeEnum.Event && journal.Id == 0)
			{
				journal.ForSite = true;
			}

			if (journal.BoutJournalTypeId == (long)BoutJournalTypeEnum.Incident)
			{
				model.Incidents = UnitOfWork.GetSet<CategoryIncident>().Where(i => i.ParentId.HasValue).ToArray();
			}

			return View(model);
		}

		/// <summary>
		///     сохранение записи
		/// </summary>
		public ActionResult BoutJournalSave(BoutJournalModel model)
		{
			var entity = model.BuildData();
			if (entity.CategoryIncidentId <= 0)
			{
				entity.CategoryIncidentId = null;
			}

			var account = Security.GetCurrentAccountId();
			var admin = UnitOfWork.GetSet<AdministratorTour>().FirstOrDefault(a => a.LinkedAccountId == account);
			var couns = UnitOfWork.GetSet<Counselors>().FirstOrDefault(a => a.LinkedAccountId == account);

			if (entity.Id == 0)
			{
				entity.DateCreate = DateTime.Now;
				entity.DateChange = DateTime.Now;
				entity.AdministratorTourId = admin?.Id;
				entity.CounselorsId = couns?.Id;
				entity = UnitOfWork.AddEntity(entity);
			}
			else
			{
				var data = entity;
				entity = UnitOfWork.GetById<BoutJournal>(data.Id);
				entity.Title = data.Title;
				entity.Description = data.Description;
				entity.IsArchive = data.IsArchive;
				entity.EventDate = data.EventDate;
				entity.DateChange = DateTime.Now;
				entity.PartyId = data.PartyId;
				entity.ForSite = data.ForSite;
				entity.CategoryIncidentId = data.CategoryIncidentId;
				entity = UnitOfWork.Update(entity);

				var fileToExclude = entity.Files.Where(fl => !data.Files.Select(f => f.Id).Contains(fl.Id));
				foreach (var file in data?.Files?.Where(f => f.Id == 0) ?? new List<BoutJournalFile>())
				{
					file.BoutJournalId = entity.Id;
					UnitOfWork.AddEntity(file);
				}

				foreach (var file in fileToExclude)
				{
					UnitOfWork.Delete(file);
				}
			}

			return RedirectToAction("BoutJournal", new {id = entity.Id});
		}
	}
}
