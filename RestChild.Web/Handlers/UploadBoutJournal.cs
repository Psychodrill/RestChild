using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadBoutJournal : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StorageBoutJournal); //Path should! always end with '/'
    }
}
