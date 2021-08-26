using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Helpers;
using RestChild.Comon;
using RestChild.Web.Properties;

namespace RestChild.Web.Handlers
{
	public class UploadImageHandler : IHttpHandler
	{
        // Return false in case your Managed Handler cannot be reused for another request.
        // Usually this would be false in case you have some state information preserved per request.
		public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
		{
			try
			{
				if (context.Request.Files.Count == 1)
				{
					var newFileName = Guid.NewGuid().ToString();
					switch (context.Request.Files[0].ContentType)
					{
						case "image/jpeg":
							newFileName = newFileName + ".jpeg";
							break;
						case "image/png":
							newFileName = newFileName + ".png";
							break;
						default:
							throw new UnsupportedMediaTypeException(string.Empty, new MediaTypeHeaderValue(context.Request.ContentType));
					}

					if (!string.IsNullOrWhiteSpace(context.Request.Form["FileToSave"]) && !System.IO.File.Exists(Path.Combine(Settings.Default.RestPlaceImagesPath, context.Request.Form["FileToSave"])))
					{
						newFileName = context.Request.Form["FileToSave"];
					}

					var newFilePath = Path.Combine(Settings.Default.RestPlaceImagesPath, newFileName);
					var x = context.Request.Form["x"].IntParse();
					var y = context.Request.Form["y"].IntParse();
					var x2 = context.Request.Form["x2"].IntParse();
					var y2 = context.Request.Form["y2"].IntParse();
					var image = new WebImage(context.Request.Files[0].InputStream);
					if (x == x2 || y == y2 || !x.HasValue || !y.HasValue || !x2.HasValue || !y2.HasValue)
					{
						image.Save(newFilePath);
					}
					else
					{
						var croped = image.Crop(y.Value, x.Value, image.Height - y2.Value, image.Width - x2.Value);
						croped.Save(newFilePath);
					}

					context.Response.StatusCode = (int) HttpStatusCode.OK;
					context.Response.Output.Write(newFileName);
				}
				else
				{
					context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
				}
			}
			catch (UnsupportedMediaTypeException)
			{
				context.Response.StatusCode = (int) HttpStatusCode.UnsupportedMediaType;
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
	}
}
