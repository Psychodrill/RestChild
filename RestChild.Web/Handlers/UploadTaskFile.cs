using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadTaskFile : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StorageCounselorTaskPath); //Path should! always end with '/'
    }
}
