using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class PartyController : BaseController
	{
		public WebPartyController ApiController { get; set; }
		public WebHotelsController ApiHotelsController { get; set; }
		public WebRestTimeController ApiTimeOfRestController { get; set; }
		public WebSubjectOfRestController ApiSubjectOfRestController { get; set; }
		public WebCounselorsController ApiCounselorsController { get; set; }
		public WebAdministratorTourController ApiAdministratorTourController { get; set; }
		public WebVocabularyController ApiVocabularyController { get; set; }
		public WebTransportInfoController ApiTransportInfoController { get; set; }


		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelsController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiTimeOfRestController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiSubjectOfRestController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCounselorsController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiAdministratorTourController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiVocabularyController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiTransportInfoController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Form()
		{
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			return View(new PartyModel());
		}

		public ActionResult GetChilds(PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			filter.SubjectsOfRest = ApiSubjectOfRestController.Get();
			if (!filter.OnlyNotAdded.HasValue)
			{
				filter.OnlyNotAdded = true;
			}

			if (filter.IsGrouped)
			{
				var data = ApiController.GetChildsGrouped(filter);
				data.IsBoutAdministartor = ApiAdministratorTourController.GetAdministratorToursForAccount().Any(a => a.Bouts != null && a.Bouts.Any(b => b.Id == data.BoutsId));
				data.NotNeedTicketReasons = ApiVocabularyController.GetActiveNotNeedTicketReasons();
				return View("GroupedChilds", data);
			}

			var ungrouped = ApiController.GetChildsUngrouped(filter);
			ungrouped.IsBoutAdministartor = ApiAdministratorTourController.GetAdministratorToursForAccount().Any(a => a.Bouts != null && a.Bouts.Any(b => b.Id == ungrouped.BoutsId));
			ungrouped.NotNeedTicketReasons = ApiVocabularyController.GetActiveNotNeedTicketReasons();
			return View("UngroupedChilds", ungrouped);
		}

		public ActionResult GetParties(PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var parties = ApiController.GetParties(filter);
			parties.IsBoutAdministartor = ApiAdministratorTourController.GetAdministratorToursForAccount().Any(a => a.Bouts != null && a.Bouts.Any(b => b.Id == parties.BoutsId));
			parties.NotNeedTicketReasons = ApiVocabularyController.GetActiveNotNeedTicketReasons();
			SetupTransport(filter);
			return View("PartyList", parties);
		}

		private void SetupTransport(PartyFilter filter)
		{
			var counselorIds = filter.Parties != null
									? filter.Parties.Where(p => p.Data.Counselors != null)
											.SelectMany(p => p.Data.Counselors)
											.Select(c => c.Id).ToList() : new List<long>();

			var counselorTransport =
				ApiTransportInfoController.GetLinksToPeople()
					.Where(
						l =>
						l.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Counselor && l.CounselorsId.HasValue
						&& (counselorIds.Contains(l.CounselorsId.Value))
						&& l.BoutId == filter.BoutsId)
						.ToList();

			filter.CounselorTransportForward =
				counselorTransport.Where(l => l.Transport != null && l.Transport.DepartureId == (long)CityEnum.Moscow)
					.GroupBy(l => l.CounselorsId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.CounselorsId.Value);

			filter.CounselorTransportBackward =
				counselorTransport.Where(l => l.Transport != null && l.Transport.ArrivalId == (long)CityEnum.Moscow)
					.GroupBy(l => l.CounselorsId, (l, v) => v.OrderBy(vv => vv.Id).FirstOrDefault())
					.ToDictionary(l => l.CounselorsId.Value);
		}
	}
}
