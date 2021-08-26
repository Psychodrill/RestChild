using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class DownloadImageHandlerBase : HttpTaskAsyncHandler
	{
		/// <summary>
		///     You will need to configure this handler in the Web.config file of your
		///     web and register it with IIS before being able to use it. For more information
		///     see the following link: http://go.microsoft.com/?linkid=8101007
		/// </summary>

		#region IHttpHandler Members
		public override bool IsReusable => true;

		protected virtual string StorageRootPath
		{
			get { return Settings.Default.RestPlaceImagesPath; }
		}

		public override async Task ProcessRequestAsync(HttpContext context)
		{
			try
			{
				var filePath = Path.Combine(StorageRootPath, context.Request.PathInfo.Substring(1));
				using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					var extension = Path.GetExtension(filePath);
					switch (extension)
					{
						case ".jpeg":
						case ".jpg":
						case ".jpe":
							context.Response.ContentType = "image/jpeg";
							break;
						case ".png":
							context.Response.ContentType = "image/png";
							break;
						case ".pdf":
							context.Response.ContentType = "application/pdf";
							break;
						case ".exe":
							throw new FileNotFoundException();
						case ".dll":
							throw new FileNotFoundException();
						case ".obj":
							throw new FileNotFoundException();
						default:
							context.Response.ContentType = "application/octet-stream";
							break;
					}

					await fs.CopyToAsync(context.Response.OutputStream);
					context.Response.StatusCode = (int) HttpStatusCode.OK;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			}
			catch (FileNotFoundException)
			{
				context.Response.StatusCode = (int) HttpStatusCode.NotFound;
			}
			catch (Exception)
			{
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			}
		}

		#endregion
	}
}
