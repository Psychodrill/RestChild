using System.Linq;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers;

namespace RestChild.Web.Logic
{
   using Security = RestChild.Web.Controllers.Security;

	public class StateLogic : ILogic
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public StateMachineAction GetAction(string actionCode)
		{
			if (!Security.HasRight(actionCode))
			{
				return null;
			}

			return UnitOfWork.GetSet<StateMachineAction>().FirstOrDefault(a => a.ActionCode == actionCode);
		}
	}
}
