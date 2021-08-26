using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	public class CertificateController : BaseController
	{
		internal void GenerateCertificateNumber(ListOfChilds list)
		{
			if (list.StateId != StateMachineStateEnum.Limit.List.Formation)
			{
				var number = UnitOfWork.GetNextNumber($"CN-{list.Tour.YearOfRestId}");
				list.CertificateNumber = $"000000{number}/{list.Tour.YearOfRest.Name.Substring(2)}";
				list.CertificateNumber = list.CertificateNumber.Substring(list.CertificateNumber.Length - 9);
			}
		}
	}
}