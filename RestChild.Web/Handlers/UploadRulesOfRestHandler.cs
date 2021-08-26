using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadRulesOfRestHandler : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StorageRulesOfRest); //Path should! always end with '/'
    }
}
