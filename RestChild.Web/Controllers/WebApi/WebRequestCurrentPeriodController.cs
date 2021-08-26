using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	///     WebApi-контроллер для заявочных компаний
	/// </summary>
	[Authorize]
	public class WebRequestCurrentPeriodController : WebGenericRestController<YearOfRest>
	{
		/// <summary>
		/// отметить что включен
		/// </summary>
		[HttpPost, HttpGet]
		public int[] SetIncluded(long id, bool included)
		{
			var listRequest = UnitOfWork.GetById<ListTravelersRequest>(id);

			var yearOfCompany = listRequest?.ListTravelers?.YearOfRest;
			if (!(yearOfCompany?.ListComplited ?? true))
			{

				listRequest.IsIncluded = included;
				if (included)
				{
					listRequest.DateInclude = listRequest.DateInclude ?? DateTime.Now;
				}
				else
				{
					listRequest.DateInclude = null;
				}

				listRequest.ListTravelers.HistoryLink = UnitOfWork.WriteHistory(listRequest.ListTravelers.HistoryLink,
					$"Включение/исключение из списка",
					$"Заявление {listRequest?.Request?.RequestNumber} {(included ? "включено в список" : "исключено из списка")}",
					Security.GetCurrentAccountId());

				listRequest.ListTravelers.HistoryLinkId = listRequest.ListTravelers.HistoryLink?.Id;
				UnitOfWork.SaveChanges();
			}

			return new []
			{
				UnitOfWork.GetSet<ListTravelersRequest>()
					.Where(l => l.ListTravelersId == listRequest.ListTravelersId && l.IsIncluded)
					.Sum(l => (int?) (l.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ? 1 : l.Request.CountPlace)) ??
				0,
				UnitOfWork.GetSet<ListTravelersRequest>()
					.Where(l => l.ListTravelersId == listRequest.ListTravelersId && l.IsIncluded)
					.Sum(
						l => (int?) (l.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ? 0 : l.Request.CountAttendants)) ??
				0
			};
		}

		/// <summary>
		/// получить детализацию по 
		/// </summary>
		[HttpPost, HttpGet]
		public DetailInfo[] GetDetail(long id)
		{
			var listRequest = UnitOfWork.GetById<ListTravelersRequest>(id);

			var deails = listRequest.Details.FirstOrDefault()?.Detail;

			if (string.IsNullOrWhiteSpace(deails))
			{
				return new DetailInfo[0];
			}

			return JsonConvert.DeserializeObject<DetailInfo[]>(deails);
		}

		/// <summary>
		/// отметить что включен
		/// </summary>
		internal void IncludeRequest(ListTravelersRequest[] listRequests)
		{
			var listTravelers = listRequests.Select(l=>l.ListTravelers).FirstOrDefault();

			var historyLinkId = listTravelers?.HistoryLinkId;

			if (!historyLinkId.HasValue && listTravelers != null)
			{
				var historyLink = UnitOfWork.AddEntity(new HistoryLink());
				historyLinkId = historyLink?.Id;
				listTravelers.HistoryLinkId = historyLinkId;
				UnitOfWork.SaveChanges();
			}

			var listNumber = new List<string>();

			foreach (var listRequest in listRequests)
			{
				if (!listRequest.IsIncluded)
				{
					listRequest.IsIncluded = true;
					listRequest.DateInclude = listRequest.DateInclude ?? DateTime.Now;
					listNumber.Add(listRequest.Request.RequestNumber);
				}
			}

			if (historyLinkId.HasValue)
			{
				UnitOfWork.AddEntity(new History
				{
					AccountId = Security.GetCurrentAccountId(),
					EventCode = $"Включение/исключение из списка",
					DateChange = DateTime.Now,
					Commentary = $"Заявления {string.Join(", ", listNumber)} включены в список",
					LinkId = historyLinkId,
					LastUpdateTick = DateTime.Now.Ticks
				}, false);
			}

			UnitOfWork.SaveChanges();
		}

		/// <summary>
		/// отметить что включен
		/// </summary>
		internal void ExcludeAll(long id)
		{
			var listRequests = UnitOfWork.GetSet<ListTravelersRequest>().Where(r => r.ListTravelersId == id && r.IsIncluded).ToList();

			var listTravelers = UnitOfWork.GetById<ListTravelers>(id);

			var historyLinkId = listTravelers.HistoryLinkId;

			if (!historyLinkId.HasValue)
			{
				var historyLink = UnitOfWork.AddEntity(new HistoryLink());
				historyLinkId = historyLink?.Id;
				listTravelers.HistoryLinkId = historyLinkId;
				UnitOfWork.SaveChanges();
			}
			var listNumber = new List<string>();

			foreach (var listRequest in listRequests)
			{
				listRequest.IsIncluded = false;
				listRequest.DateInclude = null;
				listNumber.Add(listRequest.Request.RequestNumber);
			}

			if (historyLinkId.HasValue)
			{
				UnitOfWork.AddEntity(new History
				{
					AccountId = Security.GetCurrentAccountId(),
					EventCode = $"Включение/исключение из списка",
					DateChange = DateTime.Now,
					Commentary = $"Заявления {string.Join(", ", listNumber)} исключены из списока",
					LinkId = historyLinkId,
					LastUpdateTick = DateTime.Now.Ticks
				}, false);
			}
			UnitOfWork.SaveChanges();
		}

		/// <summary>
		///     Поиск заявочных кампаний
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		/// <returns>Список найденных лет отдыха</returns>
		public CommonPagedList<YearOfRest> Get(string name, int pageNumber)
		{
			var query = UnitOfWork.GetSet<YearOfRest>().Where(place => place.Name.Contains(name));

			var pageSize = Settings.Default.TablePageSize;
			var startRecord = (pageNumber - 1)*pageSize;
			var totalCount = query.Count();
			var entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<YearOfRest>(entity, pageNumber, pageSize, totalCount);
		}

		/// <summary>
		/// отправка событий
		/// </summary>
		/// <param name="id">ИД кампании</param>
		/// <param name="eventCode"></param>
		/// <param name="planDate">дата отправки статусов</param>
		/// <returns></returns>
		[HttpPost, HttpGet]
		public bool SendEvent(long id, string eventCode, string planDate)
		{
			if (!Security.HasRight(eventCode))
			{
				return false;
			}
			var eventGuid = new Guid(eventCode);
			if (UnitOfWork.GetSet<RequestEventPlanied>().Any(s => s.EventCode == eventGuid && !s.Processed))
			{
				return true;
			}

			var yearOfRest = UnitOfWork.GetById<YearOfRest>(id);
			if (yearOfRest == null)
			{
				return false;
			}

			if (eventCode == AccessRightEnum.Status.FcToRanging)
			{
				if (yearOfRest.ReceptionOfApplicationsCompleted)
				{
					return false;
				}

				yearOfRest.ReceptionOfApplicationsCompleted = true;
			}

			if (eventCode == AccessRightEnum.Status.FcToIncludedInList)
			{
				if (yearOfRest.ListComplited)
				{
					return false;
				}

				yearOfRest.ListComplited = true;
			}

			if (eventCode == AccessRightEnum.Status.FcToDecisionMaking)
			{
				if (yearOfRest.TourOpened)
				{
					return false;
				}

				yearOfRest.TourOpened = true;
			}

			UnitOfWork.AddEntity(new RequestEventPlanied
			{
				AccountId = Security.GetCurrentAccountId(),
				DateCreate = DateTime.Now,
				EventCode = eventGuid,
				PlanDate = planDate.TryParseDateDdMmYyyyHh(),
				
			});

			UnitOfWork.SaveChanges();
			return true;
		}

		public IEnumerable<YearOfRest> Get()
		{
			return UnitOfWork.GetSet<YearOfRest>().ToList();
		}
	}
}
