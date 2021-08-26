using System;
using System.Configuration;
using System.Linq;
using System.Net;
using MailingDemon.Properties;
using RestChild.Comon;
using RestChild.Comon.Config;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
	[Task]
	public class RejectRequestAutomatic : BaseTask
	{
		protected override void Execute()
		{
			Logger.Info("RejectRequestAutomatic started");
			using (var unitOfWork = new UnitOfWork())
			{
				#region Перевод в статус ожидания прихода заявителя

				//ToWaintApplicant(unitOfWork);

				#endregion

				#region Отказ тем кто не пришел

				ToRejectApplicantByTime(unitOfWork);

				#endregion

				#region Отказ тем кто не участвовал во втором этапе

				if (ConfigurationManager.AppSettings["needRejectAsNotParticipateInSecondStage"] == "RejectAsNotParticipateInSecondStage")
				{
					ToRejectAsNotParticipateInSecondStage(unitOfWork);
				}

				#endregion
			}

			Logger.Info("RejectRequestAutomatic finished");
		}

		private void ToRejectAsNotParticipateInSecondStage(IUnitOfWork unitOfWork)
		{
			var now = DateTime.Now.Date;

			DateTime dayReject = unitOfWork.AddWorkDays(DateTime.Now.Date,
				-(ConfigurationManager.AppSettings["CountDaysForRejectRepared"].IntParse() ?? 0));

			var yearOfRestIds = unitOfWork.GetSet<Request>()
				.Where(r => r.StatusId == (long) StatusEnum.DecisionMaking && !r.IsDeleted && r.IsFirstCompany)
				.Where(r => (r.YearOfRest.DateSecondStageClose.HasValue && r.YearOfRest.DateSecondStageClose < now && !r.Repared) ||
				            (r.YearOfRest.DateSecondStageClose.HasValue && r.YearOfRest.DateSecondStageClose < now && r.DateChangeStatus < dayReject && r.Repared))
				.Select(r => r.YearOfRestId)
				.Distinct();

			var requestIds =
				unitOfWork.GetSet<Request>()
					.Where(
						r =>
							r.IsLast && !r.IsDraft && !r.IsDeleted && r.StatusId == (long) StatusEnum.DecisionMaking &&
							yearOfRestIds.Contains(r.YearOfRestId))
					.Select(c => new {RequestId = c.Id, TypeOfRest = c.TypeOfRestId})
					.ToList();

            foreach (var request in requestIds)
			{
				var url = string.Empty;
				try
                {
                    var status = AccessRightEnum.Status.ToReject;

                    url = string.Format(ConfigurationManager.AppSettings["requestRejectByTime"], request.RequestId, status, Settings.Default.NotParticipateInSecondStage);
					var req = WebRequest.Create(url);
					using (var res = req.GetResponse())
					{
						res.Close();
					}
				}
				catch (Exception ex)
				{
					Logger.Error("ToRejectAsNotParticipateInSecondStage error - " + url, ex);
				}
			}
		}

		private void ToRejectApplicantByTime(IUnitOfWork unitOfWork)
		{
			DateTime dayReject = unitOfWork.AddWorkDays(DateTime.Now.Date,
				-(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));

			var requestIds =
				unitOfWork.GetSet<Request>()
					.Where(
						r =>
							r.IsLast && !r.IsDraft && !r.IsDeleted && r.DateChangeStatus < dayReject &&
							r.StatusId == (long) StatusEnum.WaitApplicant).Select(c => new { RequestId = c.Id, TypeOfRest = c.TypeOfRestId })
					.ToList();

			foreach (var request in requestIds)
			{
				var url = string.Empty;
				try
				{
					url = string.Format(ConfigurationManager.AppSettings["requestRejectByTime"], request.RequestId,
						AccessRightEnum.Status.ToReject, DeclineSectionProcess.GetDeclineReason("NoDocuments", request.TypeOfRest) ??  Settings.Default.NoDocuments);
					var req = WebRequest.Create(url);
					using (var res = req.GetResponse())
					{
						res.Close();
					}
				}
				catch (Exception ex)
				{
					Logger.Error("ToRejectApplicantByTime error - " + url, ex);
				}
			}
		}

		//private void ToRejectApplicantByRequest(IUnitOfWork unitOfWork)
		//{
		//	DateTime dayInvite = unitOfWork.AddWorkDays(DateTime.Now.Date,
		//		-(ConfigurationManager.AppSettings["CountDaysForInvite"].IntParse() ?? 0));

		//	DateTime dayForService = unitOfWork.AddWorkDays(DateTime.Now.Date,
		//		-(ConfigurationManager.AppSettings["CountDaysForService"].IntParse() ?? 0));

		//	IQueryable<long?> query = unitOfWork.GetSet<InteragencyRequest>()
		//		.Where(
		//			i =>
		//				i.StatusInteragencyRequestId == (long)StatusInteragencyRequestEnum.Sended && i.IsSecondaryRequest &&
		//				i.RequsetDate < dayInvite)
		//		.Select(i => (long?)i.Id);

		//	IQueryable<long?> request =
		//		unitOfWork.GetSet<InteragencyRequestResult>()
		//			.Where(r => query.Contains(r.InteragencyRequestId))
		//			.Select(r => r.ChildId)
		//			.Distinct();

		//	var requestIds =
		//		unitOfWork.GetSet<Child>()
		//			.Where(
		//				c =>
		//					request.Contains(c.EntityId) && c.IsLast && !c.Request.IsDeleted && c.Request.IsLast &&
		//					c.Request.StatusId == (long) StatusEnum.ApplicantCome && c.Request.DateRequest < dayForService)
		//			.Select(c => new  { RequestId= c.RequestId ?? 0, TypeOfRest = c.Request.TypeOfRestId }).Distinct().ToList();

		//	foreach (var requestData in requestIds)
		//	{
		//		try
		//		{
		//			WebRequest req =
		//				WebRequest.Create(string.Format(ConfigurationManager.AppSettings["requestRejectByTime"], requestData.RequestId,
		//					AccessRightEnum.Status.ToReject,
		//					DeclineSectionProcess.GetDeclineReason("DsznNotAnswer", requestData.TypeOfRest) ?? Settings.Default.DsznNotAnswer));
		//			using (var res = req.GetResponse())
		//			{
		//				res.Close();
		//			}

		//		}
		//		catch (Exception ex)
		//		{
		//			Logger.Error("ToRejectApplicantByRequest error", ex);
		//		}
		//	}
		//}

		//private void ToWaintApplicant(IUnitOfWork unitOfWork)
		//{
		//	DateTime dayInvite = unitOfWork.AddWorkDays(DateTime.Now.Date,
		//		-(ConfigurationManager.AppSettings["CountDaysForInvite"].IntParse() ?? 0));

		//	IQueryable<long?> query = unitOfWork.GetSet<InteragencyRequest>()
		//		.Where(
		//			i =>
		//				i.StatusInteragencyRequestId == (long) StatusInteragencyRequestEnum.Sended && !i.IsSecondaryRequest &&
		//				i.RequsetDate < dayInvite)
		//		.Select(i => (long?) i.Id);

		//	IQueryable<long?> request =
		//		unitOfWork.GetSet<InteragencyRequestResult>()
		//			.Where(r => query.Contains(r.InteragencyRequestId))
		//			.Select(r => r.ChildId)
		//			.Distinct();

		//	List<long> requestIds =
		//		unitOfWork.GetSet<Child>()
		//			.Where(
		//				c =>
		//					request.Contains(c.EntityId) && c.IsLast && !c.Request.IsDeleted && c.Request.IsLast &&
		//					c.Request.StatusId == (long) StatusEnum.Send)
		//			.Select(c => c.RequestId ?? 0)
		//			.Distinct().ToList();

		//	foreach (long requestId in requestIds)
		//	{
		//		try
		//		{
		//			WebRequest req =
		//				WebRequest.Create(string.Format(ConfigurationManager.AppSettings["requestRejectByTime"], requestId,
		//					AccessRightEnum.Status.ToWaitApplicant, 0));
		//			using (var res = req.GetResponse())
		//			{
		//				res.Close();
		//			}

		//		}
		//		catch (Exception ex)
		//		{
		//			Logger.Error("ToWaintApplicant error", ex);
		//		}
		//	}
		//}
	}
}
