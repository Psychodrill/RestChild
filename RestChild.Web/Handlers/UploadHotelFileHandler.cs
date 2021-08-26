using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadHotelFileHandler : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StorageHotelFilesRootpath); //Path must! always end with '/'
    }
}
