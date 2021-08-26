using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class DownloadPaymentFileHandler : DownloadImageHandlerBase
	{
		protected override string StorageRootPath => Settings.Default.StoragePayment;
    }
}
