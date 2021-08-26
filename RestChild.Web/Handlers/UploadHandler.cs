using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadHandler : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StorageRootPath);
	}
}
