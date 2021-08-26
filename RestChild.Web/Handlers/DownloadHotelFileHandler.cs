using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class DownloadHotelFileHandler : DownloadImageHandlerBase
	{
		protected override string StorageRootPath => Settings.Default.StorageHotelFilesRootpath;
    }
}
