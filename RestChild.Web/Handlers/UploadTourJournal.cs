using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadTourJournal : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StorageProductFiles);
	}
}
