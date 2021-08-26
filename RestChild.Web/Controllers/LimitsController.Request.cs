using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models.Limits;

namespace RestChild.Web.Controllers
{
	/// <summary>
	/// работа с заявками на квоту
	/// </summary>
	public partial class LimitsController
	{
		#region Работа с заявками от организаций

		/// <summary>
		///     получение списка возможных учреждений.
		/// </summary>
		public ActionResult RequestList(LimitRequestModel model)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Limit.Request.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass();

			model = model ?? new LimitRequestModel
			{
				YearOfRestId = 0
			};

			var years = ApiController.GetYearsOfRest().ToList();

			var year = years.FirstOrDefault(y => y.Year == DateTime.Now.Year) ?? years.LastOrDefault();

			model.ListOfYears = years;

			if (model.YearOfRestId == 0)
			{
				model.YearOfRestId = year?.Id ?? 0;
			}

			model.OrgName = UnitOfWork.GetById<Organization>(model.OrgId)?.Name;

			RequestListFillCanAddRequest(model);
			RequestFillRequests(model);

			return View(model);
		}

		private void RequestListFillCanAddRequest(LimitRequestModel model)
		{
			model.CanAddRequest = Security.HasRight(AccessRightEnum.Limit.Request.Edit);

			var curId = Security.GetCurrentAccountId();
			var orgParentsFirstIds =
				UnitOfWork.GetSet<Account>()
					.Where(a => a.Id == curId)
					.SelectMany(a => a.Rights)
					.Where(a => a.AccessRight.Code == AccessRightEnum.Limit.Request.View)
					.Select(a => a.Organization.ParentId ?? a.OrganizationId).Distinct();

			var forAll = Security.HasRight(AccessRightEnum.Limit.Request.View, null);

			var rolesSecondId =
				UnitOfWork.GetSet<Role>()
					.Where(a => a.AccessRights.Any(r => r.Code == AccessRightEnum.Limit.Request.View))
					.Select(r => (long?) r.Id);
			var orgParentsSecondIds =
				UnitOfWork.GetSet<Account>()
					.Where(a => a.Id == curId)
					.SelectMany(a => a.Roles)
					.Where(a => rolesSecondId.Contains(a.RoleId))
					.Select(r => r.Organization.ParentId ?? r.OrganizationId).Distinct();

			var limitOnVedomstvos = UnitOfWork.GetSet<LimitOnVedomstvo>()
				.Where(
					lv => lv.YearOfRestId == model.YearOfRestId && lv.StateId == StateMachineStateEnum.Limit.Oiv.GatheringRequirements);

			if (forAll)
			{
				model.Oivs = limitOnVedomstvos.ToList();
			}
			else if (orgParentsFirstIds.Count() > 50 || orgParentsSecondIds.Count() > 50)
			{
				model.Oivs = limitOnVedomstvos.Where(l => orgParentsFirstIds.Contains(l.OrganizationId) || orgParentsSecondIds.Contains(l.OrganizationId)).ToList();
			}
			else
			{
				var orgFirstIdsArray = orgParentsFirstIds.ToArray();
				var orgSecondIdsArray = orgParentsSecondIds.ToArray();
				model.Oivs = limitOnVedomstvos
					.Where(l => orgFirstIdsArray.Contains(l.OrganizationId) || orgSecondIdsArray.Contains(l.OrganizationId))
					.ToList();
			}

			model.TimeOfRests = UnitOfWork.GetSet<GroupedTimeOfRest>().Where(t => t.IsActive).OrderBy(t => t.Id).ToList();
			model.Categorys = UnitOfWork.GetSet<ListOfChildsCategory>().Where(t => t.IsActive).OrderBy(t => t.Name).ToList();
			model.CanAddRequest = model.CanAddRequest && model.Oivs.Any();
		}

		private void RequestFillRequests(LimitRequestModel model)
		{
			var curId = Security.GetCurrentAccountId() ?? 0;
			var forAll = Security.HasRight(AccessRightEnum.Limit.Request.View, null);

			var q =
				UnitOfWork.GetSet<LimitOnOrganizationRequest>()
					.Where(
						l =>
							l.StateId != StateMachineStateEnum.Deleted && l.StateId.HasValue &&
							l.LimitOnVedomstvo.YearOfRestId == model.YearOfRestId &&
							l.LimitOnVedomstvo.StateId != StateMachineStateEnum.Deleted);

			if (model.OrgId > 0)
			{
				q = q.Where(r => r.OrganizationId == model.OrgId);
			}

			if (model.OivId > 0)
			{
				q = q.Where(r => r.LimitOnVedomstvoId == model.OivId);
			}

			if (!string.IsNullOrEmpty(model.Name))
			{
				var ss = model.Name.ToLower();
				q = q.Where(r => r.Name.ToLower().Contains(ss));
			}

			if (!forAll)
			{
				var rolesId =
					UnitOfWork.GetSet<Role>()
						.Where(a => a.AccessRights.Any(r => r.Code == AccessRightEnum.Limit.Request.View))
						.Select(r => (long?) r.Id);

				var orgFirstIds =
					UnitOfWork.GetSet<Account>()
						.Where(a => a.Id == curId)
						.SelectMany(a => a.Rights)
						.Where(a => a.AccessRight.Code == AccessRightEnum.Limit.Request.View)
						.Select(a => a.OrganizationId);


				var orgSecondIds =
					UnitOfWork.GetSet<Account>()
						.Where(a => a.Id == curId)
						.SelectMany(a => a.Roles)
						.Where(a => rolesId.Contains(a.RoleId))
						.Select(r => r.OrganizationId);

				var oivsId = model.Oivs.Select(o => (long?) o.Id).ToList();
				var org =
					UnitOfWork.GetSet<Organization>()
						.Where(o => !o.IsDeleted && (oivsId.Contains(o.Id) || oivsId.Contains(o.ParentId)));

				if (orgFirstIds.Count() > 50 || orgSecondIds.Count() > 50)
				{
					q = q.Where(l => orgFirstIds.Contains(l.OrganizationId) || orgSecondIds.Contains(l.OrganizationId) ||
					                 orgFirstIds.Contains(l.Organization.ParentId) || orgSecondIds.Contains(l.Organization.ParentId));
					org = org.Where(l => orgFirstIds.Contains(l.Id) || orgSecondIds.Contains(l.Id) ||
					                     orgFirstIds.Contains(l.ParentId) || orgSecondIds.Contains(l.ParentId));
				}
				else
				{
					var orgFirstIdsArray = orgFirstIds.ToArray();
					var orgSecondIdsArray = orgSecondIds.ToArray();
					q = q.Where(l => orgFirstIdsArray.Contains(l.OrganizationId) || orgSecondIdsArray.Contains(l.OrganizationId) ||
					                 orgFirstIdsArray.Contains(l.Organization.ParentId) ||
					                 orgSecondIdsArray.Contains(l.Organization.ParentId));
					org = org.Where(l => orgFirstIdsArray.Contains(l.Id) || orgSecondIdsArray.Contains(l.Id) ||
					                     orgFirstIdsArray.Contains(l.ParentId) || orgSecondIdsArray.Contains(l.ParentId));
				}

				var orgCount = org.Count();

				if (orgCount == 0)
				{
					model.CanAddRequest = false;
					model.CanSelectOrganization = false;
				}
				if (orgCount == 1)
				{
					model.Organization = org.FirstOrDefault();
					model.CanSelectOrganization = false;
					if (model.Organization == null)
					{
						model.CanAddRequest = false;
					}
				}
				else
				{
					model.CanSelectOrganization = true;
				}
			}
			else
			{
				model.CanSelectOrganization = true;
			}

			model.Requests = q.ToList();

			model.Actions = new Dictionary<long, List<StateMachineAction>>();

			// добавить статусы
			var statuses = model.Requests.Select(r => r.State).Distinct().ToList();
			foreach (var s in statuses)
			{
				if (!model.Actions.ContainsKey(s.Id))
				{
					model.Actions.Add(s.Id, StateController.GetActions(s,StateMachineEnum.LimitRequest));
				}
			}
		}

		#endregion

		/// <summary>
		///     получение списка возможных учреждений.
		/// </summary>
		public ActionResult RequestOivExcel(long oivLimitId)
		{
			var entity = UnitOfWork.GetById<LimitOnVedomstvo>(oivLimitId);

			var requests =
				UnitOfWork.GetSet<LimitOnOrganizationRequest>()
					.Where(
						l => l.StateId == StateMachineStateEnum.LimitRequest.Approved && l.StateId.HasValue && l.LimitOnVedomstvoId == oivLimitId &&
						     l.LimitOnVedomstvo.StateId != StateMachineStateEnum.Deleted).ToList();

			var summary =
				requests.GroupBy(g => new {g.PlaceOfRestId, g.GroupedTimeOfRest?.Name})
					.Select(
						g =>
							new Tuple<string, string, int>(g.FirstOrDefault()?.PlaceOfRest.Name, g.Key.Name,
								g.Select(d => d.Volume).Sum()))
					.ToList();

			using (var package = new ExcelPackage())
			{

				using (
					var excel = new ExcelTable<Tuple<string, string, int>>(new List<ExcelColumn<Tuple<string, string, int>>>
					{
						new ExcelColumn<Tuple<string, string, int>> {Title = "Регион отдыха", Func = r => r.Item1, Width = 31},
						new ExcelColumn<Tuple<string, string, int>> {Title = "Время отдыха", Func = r => r.Item2, Width = 31},
						new ExcelColumn<Tuple<string, string, int>> {Title = "Количество", Func = r => r.Item3, Width = 31}
					}))
				{
					excel.Parameters = new List<Tuple<string, string>>
					{
						new Tuple<string, string>("Год кампании", entity?.YearOfRest?.Name),
						new Tuple<string, string>("ОИВ", entity?.Organization?.Name)
					};
					excel.TableName = "Общие сведения";
					var excelWorksheet = package.Workbook.Worksheets.Add("Общие сведения");

					if (summary.Any())
					{
						excel.DataBind(
							excelWorksheet,
							summary,
							ExcelBorderStyle.Thin);
						excelWorksheet.Cells.Style.Font.Size = 12;
					}
					else
					{
						excelWorksheet.Cells[1, 1].Value = "Заявки отсутствуют";
					}
				}

				using (var excel = new ExcelTable<LimitOnOrganizationRequest>(package, GetRequestColumns()))
				{
					var excelWorksheet = excel.CreateExcelWorksheet("Заявки");

					if (requests.Any())
					{
						excel.DataBind(
							excelWorksheet,
							requests,
							ExcelBorderStyle.Thin);
						excelWorksheet.Cells.Style.Font.Size = 12;
					}
					else
					{
						excelWorksheet.Cells[1, 1].Value = "Заявки отсутствуют";
					}

					return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Заявки{DateTime.Now.ToString("_dd.MM.yyyy_HH:mm")}.xlsx");
				}
			}
		}

		/// <summary>
		///     получение списка возможных учреждений.
		/// </summary>
		public ActionResult RequestExcel(LimitRequestModel model)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Limit.Request.View))
			{
				return RedirectToAvalibleAction();
			}

			model = model ?? new LimitRequestModel
			{
				YearOfRestId = 0
			};

			var years = ApiController.GetYearsOfRest().ToList();

			var year = years.FirstOrDefault(y => y.Year == DateTime.Now.Year) ?? years.LastOrDefault();

			model.ListOfYears = years;

			if (model.YearOfRestId == 0)
			{
				model.YearOfRestId = year?.Id ?? 0;
			}

			model.OrgName = UnitOfWork.GetById<Organization>(model.OrgId)?.Name;

			RequestListFillCanAddRequest(model);
			RequestFillRequests(model);

			using (var excel = new ExcelTable<LimitOnOrganizationRequest>(GetRequestColumns()))
			{
				excel.Parameters = new List<Tuple<string, string>>
				{
					new Tuple<string, string>("Год кампании", UnitOfWork.GetById<YearOfRest>(model.YearOfRestId)?.Name),
					new Tuple<string, string>("ОИВ", UnitOfWork.GetById<Organization>(model.OivId)?.Name),
					new Tuple<string, string>("Организация", UnitOfWork.GetById<Organization>(model.OrgId)?.Name),
					new Tuple<string, string>("Наименование", model.Name)
				};

				excel.TableName = "Заявки на квоты";

				var excelWorksheet = excel.CreateExcelWorksheet("Заявки");

				if (model.Requests.Any())
				{
					excel.DataBind(
						excelWorksheet,
						model.Requests,
						ExcelBorderStyle.Thin);
					excelWorksheet.Cells.Style.Font.Size = 12;
				}
				else
				{
					excelWorksheet.Cells[1, 1].Value = "Заявки отсутствуют";
				}

				return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Заявки{DateTime.Now.ToString("_dd.MM.yyyy_HH:mm")}.xlsx");
			}
		}

		private static List<ExcelColumn<LimitOnOrganizationRequest>> GetRequestColumns()
		{
			return new List<ExcelColumn<LimitOnOrganizationRequest>> {
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Ведомство", Func = r => r.LimitOnVedomstvo?.Organization?.Name, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Организация", Func = r => r.Organization?.Name, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Наименование", Func = r => r.Name, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Категория", Func = r => r.ListOfChildsCategory?.Name, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Регион отдыха", Func = r => r.PlaceOfRest?.Name, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Желаемое время отдыха", Func = r => r.GroupedTimeOfRest?.Name, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Кол-во детей", Func = r => r.Volume, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Кол-во сопровождающих (вожатых)", Func = r => r.VolumeCounselor, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Кол-во сопровождающих (сопровождающих)", Func = r => r.VolumeAttendant, Width = 31 },
				new ExcelColumn<LimitOnOrganizationRequest> { Title = "Статус", Func = r => r.State?.Name, Width = 31 },
			};
		}
	}
}
