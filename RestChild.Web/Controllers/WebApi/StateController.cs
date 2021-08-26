using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Logic;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class StateController : BaseController
	{
		private readonly StateLogic _stateLogic;

		public StateController(StateLogic stateLogic)
		{
			_stateLogic = stateLogic;
		}

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			_stateLogic.SetUnitOfWorkInRefClass(unitOfWork);
        }

		/// <summary>
		/// получит список статусов
		/// </summary>
		public List<StateMachineState> GetStates(StateMachineEnum machine)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long?)machine)
					.ToList()
					.Select(s => new StateMachineState(s))
					.OrderBy(s=>s.Name)
					.ToList();
		}

		/// <summary>
		///     заполнение возможных статусов для перехода.
		/// </summary>
		internal List<StateMachineAction> GetActions(StateMachineState state, StateMachineEnum stateMachine)
		{
			if (state == null)
			{
				return new List<StateMachineAction>();
			}

			var secs = Security.GetSecurity();
			var actions =
				UnitOfWork.GetSet<StateMachineAction>()
					.Where(sm => sm.FromStates.Select(fs => fs.FromStateId).Contains(state.Id) && sm.StateMachineId == (long)stateMachine).Include(s => s.ToState)
					.ToList();
			return actions.Where(a => secs.Contains(a.ActionCode) && !a.IsSystemAction)
				.OrderBy(a => a.ActionName)
				.Select(s => new StateMachineAction(s) {ToState = new StateMachineState(s.ToState)})
				.ToList();
		}

		/// <summary>
		///     получить целевой статус перехода.
		/// </summary>
		internal StateMachineAction GetAction(string actionCode)
		{
			return _stateLogic.GetAction(actionCode);
		}

		/// <summary>
		///     получить целевой статус перехода.
		/// </summary>
		internal StateMachineState GetNextState(long? stateId, string actionCode)
		{
			if (!Security.HasRight(actionCode))
			{
				return null;
			}

			return
				UnitOfWork.GetSet<StateMachineAction>()
					.FirstOrDefault(a => a.ActionCode == actionCode && a.FromStates.Select(fs => fs.FromStateId).Contains(stateId))
					.NullSafe(s => s.ToState);
		}

		/// <summary>
		///     получение статуса
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		internal StateMachineState GetState(long id)
		{
			return UnitOfWork.GetById<StateMachineState>(id);
		}
	}
}
