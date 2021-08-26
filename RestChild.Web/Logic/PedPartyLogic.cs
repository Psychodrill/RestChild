using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Models.PedParty;

namespace RestChild.Web.Logic
{
	public class PedPartyLogic : ILogic
	{
		private static readonly string EditEventCode = "Изменение педотряда";
		private static readonly int _pageSize = 10;
		private readonly StateLogic _stateLogic;
		private readonly string DeletePedPartyEventCode = "Удаление педотряда";
		public IUnitOfWork UnitOfWork { get; set; }
		private readonly string ChangeStatusPedPartyEventCode = "Изменение статуса педотряда";

		public PedPartyLogic(StateLogic stateLogic)
		{
			_stateLogic = stateLogic;
		}

		public void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
			_stateLogic.SetUnitOfWorkInRefClass(UnitOfWork);
		}

		/// <summary>
		///     Сохранение педотряда
		/// </summary>
		/// <param name="data">Педотряд</param>
		/// <param name="userId">Пользователь совершающий действие</param>
		public void SavePedParty(PedParty data, long userId)
		{
			var now = DateTime.Now;

			if (data.Id == 0)
			{
				data.IsActive = true;

				AddNewPedParty(data, now);
			}
			else
			{
				UpdatePedParty(data, userId, now);
			}
		}

		private void UpdatePedParty(PedParty data, long userId, DateTime utcNow)
		{
			using (var ts = UnitOfWork.GetTransactionScope())
			{
				IQueryable<PedParty> query = UnitOfWork.GetSet<PedParty>();
				var entity = query.FirstOrDefault(i => i.Id == data.Id);
				if (entity != null)
				{
					entity.Name = data.Name;
					entity.City = data.City;

					entity.HistoryLink = this.WriteHistory(entity.HistoryLink, EditEventCode, string.Empty);
					entity.HistoryLinkId = entity.HistoryLink?.Id;
				}

				UnitOfWork.SaveChanges();
				ts.Complete();
			}
		}

		private void AddNewPedParty(PedParty data, DateTime lastUpdateTick)
		{
			var parties = UnitOfWork.GetSet<PedParty>();

			data.LastUpdateTick = lastUpdateTick.Ticks;
			data.HistoryLink = this.WriteHistory(data.HistoryLink, EditEventCode, string.Empty);
			data.HistoryLinkId = data.HistoryLink?.Id;
            parties.Add(data);
			UnitOfWork.SaveChanges();
		}

		/// <summary>
		///     Педотряды с пейджингом
		/// </summary>
		/// <param name="filter">Фильтр</param>
		/// <returns></returns>
		public PedPartyPagedList GetPedParties(PedPartyFilterModel filter)
		{
			filter.PageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
			var skip = (filter.PageNumber - 1)*_pageSize;

			var query = GetPedPartiesQuery(filter);

			var totalCount = query.Count();
			var list = query.OrderBy(i => i.Id)
				.Skip(skip)
				.Take(_pageSize)
				.ToArray();

			return new PedPartyPagedList(list, filter.PageNumber, _pageSize, totalCount);
		}

		/// <summary>
		///     Все сформированные педотряды
		/// </summary>
		/// <param name="pedPartyName">Название педотряда или его часть</param>
		/// <returns></returns>
		public IEnumerable<PedParty> GetAllFormedPedParties(string pedPartyName)
		{
			var filter = new PedPartyFilterModel
			{
				Name = string.IsNullOrWhiteSpace(pedPartyName) ? null : pedPartyName
			};

			var query = GetPedPartiesQuery(filter);

			query = query.Where(i => i.StateId.HasValue && i.StateId.Value == StateMachineStateEnum.PedParty.Formed);

			var list = query.OrderBy(i => i.Id)
				.ToArray()
				.Select(i => new PedParty(i))
				.ToArray();
			return list;
		}

		private IQueryable<PedParty> GetPedPartiesQuery(PedPartyFilterModel filter)
		{
			var query =
				UnitOfWork.GetSet<PedParty>().Where(h => h.StateId.HasValue && h.StateId.Value != StateMachineStateEnum.Deleted);

			if (!string.IsNullOrWhiteSpace(filter.Name))
			{
				query = query.Where(i => i.Name.ToLower().Contains(filter.Name.ToLower()));
			}

			if (!string.IsNullOrWhiteSpace(filter.City))
			{
				query = query.Where(i => i.City.ToLower().Contains(filter.City.ToLower()));
			}
			return query;
		}

		/// <summary>
		///     Изменить статус педотряда
		/// </summary>
		/// <param name="id">Id педотряда</param>
		/// <param name="stateMachineActionString">Код нового состояния</param>
		/// <param name="userId">Пользователь совершающий действие</param>
		/// <returns></returns>
		public bool ChangeStatusPedParty(long id, string stateMachineActionString, long userId)
		{
			if (GetErrorsOfChageStatus(id, stateMachineActionString).Any())
			{
				return false;
			}

			UpdateStatus(id, stateMachineActionString, userId);
			return true;
		}

		internal void UpdateStatus(long id, string stateMachineActionString, long userId)
		{
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var pedParty = UnitOfWork.GetById<PedParty>(id);

				if (stateMachineActionString == "Delete")
				{
					pedParty.HistoryLink = this.WriteHistory(pedParty.HistoryLink, DeletePedPartyEventCode, string.Empty, StateMachineStateEnum.Deleted, pedParty.StateId);
					pedParty.HistoryLinkId = pedParty.HistoryLink?.Id;
					pedParty.StateId = StateMachineStateEnum.Deleted;
				}
				else
				{
					var action = _stateLogic.GetAction(stateMachineActionString);
					if (action?.ToStateId != null)
					{
						pedParty.HistoryLink.Historys.Add(new History
						{
							AccountId = userId,
							EventCode = ChangeStatusPedPartyEventCode,
							DateChange = DateTime.Now,
							Commentary =
								$"Изменение статуса педотряда с \"{pedParty.NullSafe(c => c.State.Name)}\" на \"{action.NullSafe(a => a.ToState.Name)}\""
						});

						pedParty.HistoryLink = this.WriteHistory(pedParty.HistoryLink, ChangeStatusPedPartyEventCode,
							$"Изменение статуса педотряда с \"{pedParty?.State?.Name}\" на \"{action?.ToState?.Name}\"",
							action.ToStateId, pedParty.StateId);
						pedParty.HistoryLinkId = pedParty.HistoryLink?.Id;

						pedParty.StateId = action?.ToStateId ?? pedParty.StateId;
					}
				}

				UnitOfWork.SaveChanges();
				transaction.Complete();
			}
		}

		private IEnumerable<string> GetErrorsOfChageStatus(long pedPartyId,
			string stateMachineActionString)
		{
			var errors = new List<string>();
			var pedParty = UnitOfWork.GetById<PedParty>(pedPartyId);
			if (pedParty == null)
			{
				errors.Add("Не найден педотряд");
				return errors;
			}
			var action = _stateLogic.GetAction(stateMachineActionString);
			if (action == null)
			{
				errors.Add("Не найден переход в следующий статус педотряда");
			}
			else if (!action.ToStateId.HasValue)
			{
				errors.Add("Не найден заданный статус педотряда");
			}

			return errors;
		}
	}
}
