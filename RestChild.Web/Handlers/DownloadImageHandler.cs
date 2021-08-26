namespace RestChild.Web.Handlers
{
	public class DownloadImageHandler : DownloadImageHandlerBase
	{
		protected override string StorageRootPath
		{
			get { return base.StorageRootPath; }
		}
	}
}