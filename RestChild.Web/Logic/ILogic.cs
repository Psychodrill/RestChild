using RestChild.Comon;

namespace RestChild.Web.Logic
{
	public interface ILogic
	{
		IUnitOfWork UnitOfWork { get; set; }
	}
}
