using System.IO;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Common;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
	public class SignController : BaseController
	{
		
		/// <summary>
		///     получить информацию об ЭП
		/// </summary>
		public ActionResult GetSignInfo(long id, string url)
		{
			var esep = new Esep();

			var si = UnitOfWork.GetById<SignInfo>(id);

			return
				Redirect(esep.UrlEsepVerifySign(
					System.IO.File.ReadAllBytes(Path.Combine(Settings.Default.StorageSign, si.FileUrl)), url));
		}
	}
}