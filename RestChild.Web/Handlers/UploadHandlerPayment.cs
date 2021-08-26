using System.IO;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadHandlerPayment : UploadHandlerBase
	{
		protected override string StorageRoot => Path.Combine(Settings.Default.StoragePayment); //Path should! always end with '/'
    }
}
