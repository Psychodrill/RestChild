using RestChild.Comon;

namespace RestChild.Admin.Web.Common
{
	public interface ILogic
	{
		IUnitOfWork UnitOfWork { get; set; }
	}
}
