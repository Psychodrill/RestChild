using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Web.Common;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    public class TransportInfoController : BaseController
    {
		public WebTransportInfoController ApiController { get; set; }
		public StateController ApiStateController { get; set; }
		public WebDirectoryFlightsController ApiDirectoryFlightsController { get; set; }
		public WebCityController ApiCityController { get; set; }
		public WebRestYearController ApiRestYearController { get; set; }
		public WebVocabularyController ApiVocabularyController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCityController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiDirectoryFlightsController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiRestYearController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiVocabularyController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

        public ActionResult Search(TransportFilterModel filter)
        {
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Transport.View))
			{
				return RedirectToAvalibleAction();
			}
			SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new TransportFilterModel();
			filter.YearsOfRest = ApiRestYearController.Get().ToList();
			if (!filter.IsFilterSet)
			{
				filter.DateOfDepartureBegin = DateTime.Now.Date;
			}

			if (!filter.YearOfRestId.HasValue)
			{
				filter.YearOfRestId = filter.YearsOfRest?.FirstOrDefault(y => y.Year == DateTime.Now.Year)?.Id ??
										   filter.YearsOfRest?.LastOrDefault()?.Id;
			}

			filter.Result = ApiController.Get(filter);
			filter.States = ApiController.GetStates();
	        filter.Cities = ApiCityController.GetActive();
			return View("TransportInfoList", filter);
        }

	    public ActionResult Update(long id, string actionCode)
	    {
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Transport.View))
			{
				return RedirectToAvalibleAction();
			}
		    //var transportInfo = ApiController.Get(id);
		    var transportInfo = UnitOfWork.GetSet<TransportInfo>()
			    .Include(t => t.People.Select(d => d.Child))
			    .Include(t => t.People.Select(d => d.Child.DocumentType))
				.Include(t => t.People.Select(d => d.Counselors))
				.Include(t => t.People.Select(d => d.Counselors.DocumentType))
				.Include(t => t.People.Select(d => d.Applicant))
				.Include(t => t.Bout)
				.Include(t => t.Bout.Hotels)
				.Include(t => t.People.Select(d => d.Applicant.DocumentType)).FirstOrDefault(t=>t.Id == id);
		    if (transportInfo == null)
		    {
			    return RedirectToAction("Search");
		    }
			return GetViewResult(transportInfo, actionCode);
	    }

	    private TransportInfoModel GetViewModel(TransportInfo transport)
	    {
			var state = transport.StateId.HasValue ? ApiStateController.GetState(transport.StateId.Value) : null;
			var actions = transport.Id != 0 && transport.StateId.HasValue
				? ApiStateController.GetActions(state, StateMachineEnum.TransportState)
				: new List<StateMachineAction>();
			var isEditable = transport.StateId == StateMachineStateEnum.Transport.Forming && Security.HasRightForSomeOrganization(AccessRightEnum.TransportInfoManage);
			var result = new TransportInfoModel(transport)
			{
				IsEditable = isEditable,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#transportForm",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "TransportInfo",
						ReturnAction = "Search",
						NeedSaveButton = isEditable,
						NeedRemoveButton = false,
						JsFunctionToAction = "overrideConfirmStateButton",
						PostNoStatusActions = new List<NoStatusAction>
							                      {
								                      new NoStatusAction
									                      {
										                      Name = "Печать",
															  IconClass = "glyphicon glyphicon-print",
															  Action = "GetTransportSpreadSheet",
															  ActionParameters = new { Id = transport.Id }
									                      }
							                      }
					}
			};

		    return result;
	    }

		private ActionResult GetViewResult(TransportInfo transport, string actionCode)
		{
			var result = GetViewModel(transport);
			SetupVocabularies(result);
			if (transport.HistoryLinkId.HasValue)
			{
				result.State.PostNoStatusActions = result.State.PostNoStatusActions ?? new List<NoStatusAction>();
				result.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = string.Format("data-history-id=\"{0}\"", transport.HistoryLinkId)
				});
			}

			var errors = ApiController.GetErrors(transport.Id, actionCode);
			if (errors != null && errors.Any())
			{
				foreach (var error in errors)
				{
					ModelState.AddModelError(string.Empty, error);
				}
			}

			return View("TransportInfoEdit", result);
		}

	    private void SetupVocabularies(TransportInfoModel result)
	    {
		    if (result.Data.DepartureId.HasValue && result.Data.ArrivalId.HasValue && result.Data.YearOfRestId.HasValue)
		    {
				result.DirectoryFlights = ApiDirectoryFlightsController.Get(
				 result.Data.DepartureId.Value,
				 result.Data.ArrivalId.Value,
				 result.Data.YearOfRestId.Value);
		    }

			result.NotNeedTicketReasons = ApiVocabularyController.GetActiveNotNeedTicketReasons();
		    if (result.Data.Bout?.TransportInfoToId == result.Data.Id)
		    {
			    result.NotNeedTicketReasons = result.NotNeedTicketReasons.Where(r => r.Id == (long)NotNeedTicketReasonEnum.ComeSingly).ToList();
		    }

	    }

		public ActionResult Save(TransportInfoModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.TransportInfoManage))
			{
				return RedirectToAvalibleAction();
			}

			var data = model.BuildData();
			var entity = ApiController.Get(data.Id);
			if (entity == null)
			{
				return RedirectToAction("Search");
			}

			if (model.Data.StateId == StateMachineStateEnum.Transport.Forming)
			{
				var updatedLinks = model.GetLinksToPeople();

				foreach (var link in updatedLinks)
				{
					var linkData = link.BuildData();
					var persistedLink = entity.People.FirstOrDefault(p => p.Id == linkData.Id);
					if (persistedLink == null || persistedLink.NotNeedTicketReasonId.HasValue)
					{
						continue;
					}
					persistedLink.DirectoryFlightsId = linkData.DirectoryFlightsId;
					persistedLink.Wagon = linkData.Wagon;
					persistedLink.PlaceNumber = linkData.PlaceNumber;
					persistedLink.DateDeparture = linkData.DateDeparture;
				}

				entity.Venue = data.Venue;
				entity.DateCollection = data.DateCollection;
				entity.DateArrival = data.DateArrival;
				entity.DateOfDeparture = data.DateOfDeparture;
				ApiController.Put(entity.Id, entity);
			}

			if (!string.IsNullOrEmpty(model.StateMachineActionString))
			{
				ApiController.ChangeStatus(data.Id, model.StateMachineActionString);
			}

			return RedirectToAction("Update", new { id = data.Id });
		}

		#region Выгрука в Excel
		/// <summary>
		/// Выгрузка данных по транспорту в Excel
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult GetTransportSpreadSheet(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var transport = ApiController.Get(id);
			if (transport == null)
			{
				return RedirectToAction("Search");
			}

			var linkToPeoples = transport.People;

			if (!Security.HasRight(AccessRightEnum.Transport.View))
			{
				var orgs = AccessRightEnum.Transport.View.GetSecurityOrganiztion();
				linkToPeoples = linkToPeoples.Where(l => orgs.Contains(l.DirectoryFlights?.Contract?.SupplierId)).ToList();
			}

			var stream = new MemoryStream();
			using (var excelPackage = new ExcelPackage())
			{
				ExcelWorksheet commonData = excelPackage.Workbook.Worksheets.Add("Общая информация");

				commonData.Column(1).Width = 19.14;
				commonData.Column(2).Width = 19.14;
				SetWorksheetValue(commonData.Cells[1, 1], "Год кампании", ExcelBorderStyle.Thin, true);
				SetWorksheetValue(commonData.Cells[2, 1], "Дата отправления", ExcelBorderStyle.Thin, true);
				SetWorksheetValue(commonData.Cells[3, 1], "Город отправления", ExcelBorderStyle.Thin, true);
				SetWorksheetValue(commonData.Cells[4, 1], "Город прибытия", ExcelBorderStyle.Thin, true);
				SetWorksheetValue(commonData.Cells[1, 2], transport.NullSafe(t => t.YearOfRest.Name).FormatEx(false), ExcelBorderStyle.Thin);
				SetWorksheetValue(commonData.Cells[2, 2], transport.NullSafe(t => t.DateOfDeparture).FormatEx(), ExcelBorderStyle.Thin);
				SetWorksheetValue(commonData.Cells[3, 2], transport.NullSafe(t => t.Departure.Name).FormatEx(false), ExcelBorderStyle.Thin);
				SetWorksheetValue(commonData.Cells[4, 2], transport.NullSafe(t => t.Arrival.Name).FormatEx(false), ExcelBorderStyle.Thin);

				var familyCampers =
					linkToPeoples.Where(
						p =>
						(!p.NullSafe(link => link.Child.NotComeInPlaceOfRest) && !p.NullSafe(link => link.Applicant.NotComeInPlaceOfRest)
						&& p.Bout != null && p.Bout.Hotels != null && p.Bout.Hotels.HotelTypeId == (long)HotelTypeEnum.Hotel))
						.OrderBy(c => c.NullSafe(camper => camper.Bout.Hotels)).ThenBy(c => c.PartyId);

				var individualCampers =
					linkToPeoples.Where(
						p =>
						(!p.NullSafe(link => link.Child.NotComeInPlaceOfRest) && !p.NullSafe(link => link.Applicant.NotComeInPlaceOfRest)
						&& p.Bout != null && p.Bout.Hotels != null && p.Bout.Hotels.HotelTypeId == (long)HotelTypeEnum.Camp))
						.OrderBy(c => c.RequestId).ThenBy(c => c.ListOfChildsId);
				if (familyCampers.Any())
				{
					FormFamilyRestWorksheet(familyCampers, excelPackage);
				}

				if (individualCampers.Any())
				{
					FormIndividualRestWorksheet(individualCampers, excelPackage);
				}

				excelPackage.SaveAs(stream);
				stream.Seek(0, SeekOrigin.Begin);
				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Транспорт.xlsx");
			}
		}

		/// <summary>
		/// Формирование страницы семейного отдхыа в excel
		/// </summary>
		/// <param name="campers"></param>
		/// <param name="excelPackage"></param>
	    private void FormFamilyRestWorksheet(IEnumerable<LinkToPeople> campers, ExcelPackage excelPackage)
	    {
		    var familyCamperModels = new List<CamperModel>();
		    foreach (var camper in campers)
		    {
				Color? color;
				if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.ComeSingly)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#D0D0D0");
				}
				else if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.Hospitalized)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#f2dede");
				}
				else if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.LeftEarly)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#DEF2DE");
				}
				else if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.NotCome)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#FCECD4");
				}
				else
				{
					color = null;
				}

				string certificate = camper.Request != null
											? camper.Request.TypeOfRestId == (long)TypeOfRestEnum.CommercicalAddonRequest
											&& camper.Request.ParentRequest != null
												? camper.Request.ParentRequest.CertificateNumber
												: camper.Request.CertificateNumber
											: null;

			    familyCamperModels.Add(
				    new CamperModel()
				    {
					    ArrivalCode = camper.NullSafe(c => c.DirectoryFlights.CodeArrival).FormatEx(false),
					    ArrivalName = camper.NullSafe(c => c.DirectoryFlights.Arrival.Name).FormatEx(false),
					    DirectoryFlightNum = camper.NullSafe(c => c.DirectoryFlights.FilightNumber).FormatEx(false),
					    DirectoryFlightCode = camper.NullSafe(c => c.DirectoryFlights.Code).FormatEx(false),
					    DepartureName = camper.NullSafe(c => c.DirectoryFlights.Departure.Name).FormatEx(false),
					    DepartureCode = camper.NullSafe(c => c.DirectoryFlights.CodeDeparture).FormatEx(false),
					    DepartureDate = camper.DateDeparture.FormatEx(),
					    Category = camper.NullSafe(c => c.TypeOfLinkPeople.Name).FormatEx(false),
					    BirthPlace =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.PlaceOfBirth)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.PlaceOfBirth)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(c => c.AdministratorTour.PlaceOfBirth)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(c => c.Applicant.PlaceOfBirth)
										    : null).FormatEx(false),
					    BirthDate =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.DateOfBirth)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.DateOfBirth)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(c => c.AdministratorTour.DateOfBirth)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(c => c.Applicant.DateOfBirth)
										    : null),
					    DocIssue =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.DocumentSubjectIssue)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.DocumentSubjectIssue)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(c => c.AdministratorTour.DocumentSubjectIssue)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(c => c.Applicant.DocumentSubjectIssue)
										    : null).FormatEx(false),
					    DocNumber =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.DocumentNumber)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.DocumentNumber)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(c => c.AdministratorTour.DocumentNumber)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(c => c.Applicant.DocumentNumber)
										    : null).FormatEx(false),
					    DocSeries =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.DocumentSeria)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.DocumentSeria)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(c => c.AdministratorTour.DocumentSeria)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(c => c.Applicant.DocumentSeria)
										    : null).FormatEx(false),
					    DocType =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.DocumentType.Name)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.DocumentType.Name)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(c => c.AdministratorTour.DocumentType.Name)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(c => c.Applicant.DocumentType.Name)
										    : null).FormatEx(false),

					    HotelName = (camper?.Bout?.Hotels?.Name).FormatEx(false),
					    DateIncome = camper?.Bout?.DateIncome ?? camper?.Bout?.Tours?.FirstOrDefault()?.DateIncome,
					    Name =
						    camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
							    ? camper.NullSafe(c => c.Child.LastName + " " + c.Child.FirstName + " " + c.Child.MiddleName)
							    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
							       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
								    ? camper.NullSafe(c => c.Counselors.LastName + " " + c.Counselors.FirstName + " " + c.Counselors.MiddleName)
								    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
									    ? camper.NullSafe(
										    c =>
											    c.AdministratorTour.LastName + " " + c.AdministratorTour.FirstName + " " + c.AdministratorTour.MiddleName)
									    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
										    ? camper.NullSafe(
											    c =>
												    c.Applicant.LastName + " " + c.Applicant.FirstName + " " + c.Applicant.MiddleName)
										    : null).FormatEx(false),
					    NotNeedTicket =
						    camper.NotNeedTicketReason != null
							    ? "Да"
							    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child ||
							      camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
								    ? "Нет"
								    : (!camper.NeedTicket).FormatEx(),
					    PartyNumber = string.Empty,
					    PlaceNum = camper.PlaceNumber.FormatEx(false),
					    RequestNumber = camper.NullSafe(c => c.Request.RequestNumber).FormatEx(false),
					    TimeOfArrival = camper.NullSafe(c => c.DirectoryFlights.TimeOfArrival).FormatEx(format: "HH:mm"),
					    TimeOfDeparture =
						    camper.NullSafe(c => c.DirectoryFlights.TimeOfDeparture).FormatEx(format: "HH:mm"),
					    TransportType = camper.NullSafe(c => c.DirectoryFlights.TypeOfTransport.Name).FormatEx(false),
					    Wagon = camper.Wagon.FormatEx(false),
					    NotNeedTicketReason =
						    (camper.NotNeedTicketReason != null ? camper.NotNeedTicketReason.Name : string.Empty).FormatEx(false),
					    Certificate = certificate,
					    Color = color
				    });
		    }

			familyCamperModels = familyCamperModels.OrderBy(f => f.RequestNumber).ThenBy(f => f.Name).ToList();

			var familyRest = new ExcelTable<CamperModel>(
				excelPackage,
				new List<ExcelColumn<CamperModel>>()
				{
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.HotelName,
						Title = "Название оздоровительного учреждения",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Category,
						Title = "Категория",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.RequestNumber,
						Title = "Номер заявки",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Certificate,
						Title = "Номер путевки",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Name,
						Title = "ФИО",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocType,
						Title = "Тип документа",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocSeries,
						Title = "Серия документа",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocNumber,
						Title = "Номер документа",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocIssue,
						Title = "Когда и кем выдан",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.BirthDate.FormatEx(),
						Title = "Дата рождения",
						WordWrap = true
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
						Func = p => p.BirthPlace,
						Title = "Место рождения",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.NotNeedTicket,
						Title = "Не нужен билет",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DirectoryFlightCode,
						Title = "Код рейса",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DirectoryFlightNum,
						Title = "Номер рейса",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Wagon,
						Title = "Вагон",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.PlaceNum,
						Title = "Место",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.TimeOfDeparture,
						Title = "Время отбытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.TimeOfArrival,
						Title = "Время прибытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DepartureCode,
						Title = "Транспортный узел отбытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.ArrivalCode,
						Title = "Транспортный узел прибытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.TransportType,
						Title = "Вид транспорта",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DepartureName,
						Title = "Место отбытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.ArrivalName,
						Title = "Место прибытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.NotNeedTicketReason,
						Title = "Причина отказа от билета",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DepartureDate,
						Title = "Дата отправления",
						WordWrap = true
					}
				});

		    familyRest.DataBind(
			    "Семейный отдых",
				familyCamperModels.OrderBy(m => m.Certificate).ThenBy(m => m.Name).Select(m => new ExcelRow<CamperModel>() { Data = m, Height = 15, Color = m.Color }),
			    ExcelBorderStyle.Thin);
	    }

		/// <summary>
		/// Формирование страницы индивидуальго отдхыа в excel
		/// </summary>
		/// <param name="campers"></param>
		/// <param name="excelPackage"></param>
		private void FormIndividualRestWorksheet(IEnumerable<LinkToPeople> campers, ExcelPackage excelPackage)
		{
			var individualCamperModels = new List<CamperModel>();
			foreach (var camper in campers)
			{
				Color? color;
				if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.ComeSingly)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#D0D0D0");
				}
				else if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.Hospitalized)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#f2dede");
				}
				else if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.LeftEarly)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#DEF2DE");
				}
				else if (camper.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.NotCome)
				{
					color = System.Drawing.ColorTranslator.FromHtml("#FCECD4");
				}
				else
				{
					color = null;
				}

				string certificate = camper.Request != null
											? camper.Request.TypeOfRestId == (long)TypeOfRestEnum.CommercicalAddonRequest
											&& camper.Request.ParentRequest != null
												? camper.Request.ParentRequest.CertificateNumber
												: camper.Request.CertificateNumber
											: null;

				individualCamperModels.Add(
					new CamperModel()
					{
						ArrivalCode = camper.NullSafe(c => c.DirectoryFlights.CodeArrival).FormatEx(false),
						ArrivalName = camper.NullSafe(c => c.DirectoryFlights.Arrival.Name).FormatEx(false),
						DirectoryFlightNum = camper.NullSafe(c => c.DirectoryFlights.FilightNumber).FormatEx(false),
						DirectoryFlightCode = camper.NullSafe(c => c.DirectoryFlights.Code).FormatEx(false),
						DepartureName = camper.NullSafe(c => c.DirectoryFlights.Departure.Name).FormatEx(false),
						DepartureCode = camper.NullSafe(c => c.DirectoryFlights.CodeDeparture).FormatEx(false),
						DepartureDate = camper.DateDeparture.FormatEx(),
						Category = camper.NullSafe(c => c.TypeOfLinkPeople.Name).FormatEx(false),
						BirthPlace =
							camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
								? camper.NullSafe(c => c.Child.PlaceOfBirth)
								: (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
									? camper.NullSafe(c => c.Counselors.PlaceOfBirth)
									: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
										? camper.NullSafe(c => c.AdministratorTour.PlaceOfBirth)
										: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
											? camper.NullSafe(c => c.Applicant.PlaceOfBirth)
											: null).FormatEx(false),
						BirthDate =
							camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
								? camper.NullSafe(c => c.Child.DateOfBirth)
								: (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
									? camper.NullSafe(c => c.Counselors.DateOfBirth)
									: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
										? camper.NullSafe(c => c.AdministratorTour.DateOfBirth)
										: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
											? camper.NullSafe(c => c.Applicant.DateOfBirth)
											: null),
						DocIssue =
							camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
								? camper.NullSafe(c => c.Child.DocumentSubjectIssue)
								: (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
									? camper.NullSafe(c => c.Counselors.DocumentSubjectIssue)
									: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
										? camper.NullSafe(c => c.AdministratorTour.DocumentSubjectIssue)
										: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
											? camper.NullSafe(c => c.Applicant.DocumentSubjectIssue)
											: null).FormatEx(false),
						DocNumber =
							camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
								? camper.NullSafe(c => c.Child.DocumentNumber)
								: (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
									? camper.NullSafe(c => c.Counselors.DocumentNumber)
									: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
										? camper.NullSafe(c => c.AdministratorTour.DocumentNumber)
										: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
											? camper.NullSafe(c => c.Applicant.DocumentNumber)
											: null).FormatEx(false),
						DocSeries =
							camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
								? camper.NullSafe(c => c.Child.DocumentSeria)
								: (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
									? camper.NullSafe(c => c.Counselors.DocumentSeria)
									: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
										? camper.NullSafe(c => c.AdministratorTour.DocumentSeria)
										: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
											? camper.NullSafe(c => c.Applicant.DocumentSeria)
											: null).FormatEx(false),
						DocType =
							camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
								? camper.NullSafe(c => c.Child.DocumentType.Name)
								: (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
								   camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
									? camper.NullSafe(c => c.Counselors.DocumentType.Name)
									: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
										? camper.NullSafe(c => c.AdministratorTour.DocumentType.Name)
										: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
											? camper.NullSafe(c => c.Applicant.DocumentType.Name)
											: null).FormatEx(false),

						HotelName = camper.NullSafe(c => c.Bout.Hotels.Name).FormatEx(false),
						Name = GetLinkPeopleName(camper),
						NotNeedTicket =
							camper.NotNeedTicketReason != null
								? "Да"
								: camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child ||
								  camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
									? "Нет"
									: (!camper.NeedTicket).FormatEx(),
						DateIncome = camper?.Bout?.DateIncome ?? camper?.Bout?.Tours?.FirstOrDefault()?.DateIncome,
						PartyNumber = (camper?.Party?.PartyNumber).FormatEx(),
						PlaceNum = camper.PlaceNumber.FormatEx(false),
						RequestNumber = camper.NullSafe(c => c.Request.RequestNumber).FormatEx(false),
						ListName = camper.NullSafe(c => c.ListOfChilds.Name).FormatEx(),
						TimeOfArrival = camper.NullSafe(c => c.DirectoryFlights.TimeOfArrival).FormatEx(format: "HH:mm"),
						TimeOfDeparture =
							camper.NullSafe(c => c.DirectoryFlights.TimeOfDeparture).FormatEx(format: "HH:mm"),
						TransportType = camper.NullSafe(c => c.DirectoryFlights.TypeOfTransport.Name).FormatEx(false),
						Wagon = camper.Wagon.FormatEx(false),
						NotNeedTicketReason =
							(camper.NotNeedTicketReason != null ? camper.NotNeedTicketReason.Name : string.Empty).FormatEx(false),
						Certificate = certificate,
						Color = color
					});
			}

			individualCamperModels = individualCamperModels.OrderBy(f => f.PartyNumber).ThenBy(f => f.Name).ToList();

			var individualRest = new ExcelTable<CamperModel>(
				excelPackage,
				new List<ExcelColumn<CamperModel>>()
				{
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.HotelName,
						Title = "Название оздоровительного учреждения",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.PartyNumber,
						Title = "Номер отряда",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Category,
						Title = "Категория",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.RequestNumber,
						Title = "Номер заявки",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Certificate,
						Title = "Номер путевки",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.ListName,
						Title = "Номер списка",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.Name,
						Title = "ФИО",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocType,
						Title = "Тип документа",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocSeries,
						Title = "Серия документа",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocNumber,
						Title = "Номер документа",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DocIssue,
						Title = "Когда и кем выдан",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.BirthDate.FormatEx(),
						Title = "Дата рождения",
						WordWrap = true
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
						Func = p => p.BirthPlace,
						Title = "Место рождения",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.NotNeedTicket,
						Title = "Не нужен билет",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DirectoryFlightCode,
						Title = "Код рейса",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DirectoryFlightNum,
						Title = "Номер рейса",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.TimeOfDeparture,
						Title = "Время отбытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.TimeOfArrival,
						Title = "Время прибытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DepartureCode,
						Title = "Код места отбытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.ArrivalCode,
						Title = "Код места прибытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.TransportType,
						Title = "Вид транспорта",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DepartureName,
						Title = "Место отбытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.ArrivalName,
						Title = "Место прибытия",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.NotNeedTicketReason,
						Title = "Причина отказа от билета",
						WordWrap = true
					},
					new ExcelColumn<CamperModel>()
					{
						Func = p => p.DepartureDate,
						Title = "Дата отправления",
						WordWrap = true
					}
				});

			individualRest.DataBind(
				"Детские лагеря",
				individualCamperModels.OrderBy(m => m.Certificate).ThenBy(m => m.Name).Select(m => new ExcelRow<CamperModel>() { Data = m, Height = 15, Color = m.Color }),
				ExcelBorderStyle.Thin);
		}

	    internal static string GetLinkPeopleName(LinkToPeople camper)
	    {
		    return camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
			    ? camper.NullSafe(c => c.Child.LastName + " " + c.Child.FirstName + " " + c.Child.MiddleName)
			    : (camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor ||
			       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor ||
			       camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor
				    ? camper.NullSafe(c => c.Counselors.LastName + " " + c.Counselors.FirstName + " " + c.Counselors.MiddleName)
				    : camper.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator
					    ? camper.NullSafe(
						    c =>
							    c.AdministratorTour.LastName + " " + c.AdministratorTour.FirstName + " " + c.AdministratorTour.MiddleName)
					    : camper.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Attendant
						    ? camper.NullSafe(
							    c =>
								    c.Applicant.LastName + " " + c.Applicant.FirstName + " " + c.Applicant.MiddleName)
						    : null).FormatEx(false);
	    }

	    private void SetWorksheetValue(ExcelRange cell, string value, ExcelBorderStyle style, bool isBold = false)
	    {
			cell.Value = value;
			cell.Style.Border.Top.Style = style;
			cell.Style.Border.Bottom.Style = style;
			cell.Style.Border.Left.Style = style;
			cell.Style.Border.Right.Style = style;
			cell.Style.Font.Bold = isBold;
	    }
		#endregion


    }
}
