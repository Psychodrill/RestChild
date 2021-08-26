using System;
using System.Linq;
using System.Transactions;
using Common.Logging;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;

namespace RestChild.Booking.Logic.Logic
{
	public class IndexRestChecker
	{
		private enum UpdateType
		{
			Request,
			ListChild
		}

		private readonly ILog _logger;
		private readonly RestChildrenIndex _restChildrenIndex;

		public IndexRestChecker(ILog logger)
		{
			_logger = logger;
			_restChildrenIndex = new RestChildrenIndex(logger);
		}

		public void Check()
		{
			try
			{
				CheckInternal();
			}
			catch (Exception e)
			{
				_logger.Error("Ошибка при попытке обновить индекс отдыхающих", e);
				throw;
			}
		}

		private void CheckInternal()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				var errorCount = 0;
				var maxErrorCount = 5;
				var isRecordUpdated = false;
				do
				{
					try
					{
						isRecordUpdated = TryUpdateRecord(unitOfWork);
						if (!isRecordUpdated)
							break;
					}
					catch (Exception e)
					{
						errorCount++;
						//в случае ошибки на одной и той же записи, остальные не будут обработаны
						_logger.Error("Ошибка при попытке обновить индекс отдыхающих", e);
					}
				} while (isRecordUpdated || errorCount < maxErrorCount);
			}
		}

		/// <summary>
		///		Проверяет, есть ли запись для обновления
		/// </summary>
		/// <param name="unitOfWork"></param>
		/// <returns>True - запись была обвновлена, False - обновлять нечего</returns>
		private bool TryUpdateRecord(UnitOfWork unitOfWork)
		{
			bool haveRecord;
			_logger.Info("Update index TryUpdateRecord started...");

			var updaIndexInfo = GetUpdateInfo(unitOfWork);

			if (updaIndexInfo != null)
			{
				switch (updaIndexInfo.UpdateType)
				{
					case UpdateType.Request:
						_restChildrenIndex.UpdateRequestIndex(unitOfWork, updaIndexInfo.Id);
						_logger.Info("Update index index updated requestId=" + updaIndexInfo.Id);
						ClearRequestIndexFlag(unitOfWork, updaIndexInfo.Id);
						_logger.Info("Update index index flag updated requestId=" + updaIndexInfo.Id);
						break;
					case UpdateType.ListChild:
						_restChildrenIndex.UpdateListChildIndex(unitOfWork, updaIndexInfo.Id);
						_logger.Info("Update index index updated ListChild=" + updaIndexInfo.Id);
						ClearChildListIndexFlag(unitOfWork, updaIndexInfo.Id);
						_logger.Info("Update index index flag updated ListChild=" + updaIndexInfo.Id);
						break;
					default:
						throw new ArgumentOutOfRangeException("UpdateType");
				}

				haveRecord = true;
			}
			else
				haveRecord = false;

			_logger.Info("Update index TryUpdateRecord finished");

			return haveRecord;
		}

		private UpdateIndexInfo GetUpdateInfo(IUnitOfWork unitOfWork)
		{
			var requestToUpdate = unitOfWork
				.GetSet<Request>()
				.Where(i => i.ForIndex)
				.Select(i => i.Id)
				.FirstOrDefault();

			if (requestToUpdate != 0)
			{
				_logger.InfoFormat("Update index on Request id={0}", requestToUpdate);
				return new UpdateIndexInfo
				{
					UpdateType = UpdateType.Request,
					Id = requestToUpdate
				};
			}

			var listOfChildIdToUpdate = unitOfWork.GetSet<ListOfChilds>()
				.Where(i => i.ForIndex)
				.Select(i => i.Id)
				.FirstOrDefault();

			if (listOfChildIdToUpdate != 0)
			{
				_logger.InfoFormat("Update index on ListOfChild id={0}", requestToUpdate);
				return new UpdateIndexInfo
				{
					UpdateType = UpdateType.ListChild,
					Id = listOfChildIdToUpdate
				};
			}

			_logger.Info("Update index not found record");

			return null;
		}

		private void ClearRequestIndexFlag(IUnitOfWork unitOfWork, long requestId)
		{
			var entity = new Request()
			{
				Id = requestId
			};

			var request = unitOfWork.GetOrAttachEntity(entity);

			request.ForIndex = false;
			unitOfWork.Context.Entry(request).Property(i => i.ForIndex).IsModified = true;
			unitOfWork.SaveChanges();
		}

		private void ClearChildListIndexFlag(IUnitOfWork unitOfWork, long chlidListId)
		{
			var entity = new ListOfChilds()
			{
				Id = chlidListId
			};

			var childList = unitOfWork.GetOrAttachEntity(entity);

			childList.ForIndex = false;
			unitOfWork.Context.Entry(childList).Property(i => i.ForIndex).IsModified = true;
			unitOfWork.SaveChanges();
		}

		private class UpdateIndexInfo
		{
			public UpdateType UpdateType { get; set; }
			public long Id { get; set; }
		}
	}
}
