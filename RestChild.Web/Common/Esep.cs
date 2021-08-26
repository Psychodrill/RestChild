using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using DocumentFormat.OpenXml.Drawing;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Controllers;
using RestChild.Web.EsepService;
using RestChild.Web.FileService;
using RestChild.Web.Properties;
using Credentials = RestChild.Web.FileService.Credentials;
using FileInfo = RestChild.Web.FileService.FileInfo;

namespace RestChild.Web.Common
{
   using Security = RestChild.Web.Controllers.Security;

	public class Esep
	{
		public IList<EsepFileAccessCode> UploadFilesToEsep(IEnumerable<DataForSign> dataForSign)
		{
         RestChild.Security.Logger.SecurityLogger.AddToLog(SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с АИС РУЛ", "UploadFilesToEsep", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         var esepFile = new FileServiceClient();
			var result = new List<EsepFileAccessCode>();
			foreach (var data in dataForSign)
			{
				var request = new UploadFileExRequest
				{
					Credentials = new Credentials
					{
						Login = ConfigurationManager.AppSettings["EsepLogin"],
						Password = ConfigurationManager.AppSettings["EsepPassword"]
					},
					Description = !String.IsNullOrEmpty(data.Title) ? data.Title : "Подтверждение корректности введенных данных",
					File = new File
					{
						Info = new FileInfo { Name = (!String.IsNullOrEmpty(data.Name) ? data.Name : "Карточка"), MimeType = data.MimeType ?? "text/html" },
						Body = data.Content
					},
					StorageType = StorageType.Temporary
				};

				var res = esepFile.UploadFileEx(request);

				if (res.RequestResult.WasSuccessful)
				{
					result.Add(new EsepFileAccessCode
					{
						EntityId = data.EntityId,
						FileAccessCode = res.FileAccessCode,
						SignType = data.SignType,
						ActionCode = data.ActionCode,
						Title = data.Title,
						Commentary = data.Commentary
					});
				}
				else
				{
					log4net.LogManager.GetLogger(GetType()).Error(String.Format("Ошибка ЕСЭП: {0}; {1};", res.RequestResult.ErrorCode, res.RequestResult.ErrorMessage));
					return null;
				}
			}

			if (result != null && result.Count > 0)
			{
				HttpContext.Current.Session["FileAccessCodes"] = result;
			}

			return result;
		}

		public string UrlToEsep(IList<string> fileAccessCodes, string returnUrl, string guid)
		{
			var esep = new EsepServiceClient();

			var req = new CreateUIToSingRequest
			{
				Credentials = new EsepService.Credentials
				{
					Login = ConfigurationManager.AppSettings["EsepLogin"],
					Password = ConfigurationManager.AppSettings["EsepPassword"]
				},
				ClienSignatureFormat = ClientSignatureFormat.CMS,
				ClientSigningMode = ClientSigningMode.Batch,
				ClientSignatureKind = SignatureKind.Attached,
				ServerSignRequired = false,
				ServerSignatureFormat = ServerSignatureFormat.CMS,
				Oids = new string[0],
				FileAccessCodes = fileAccessCodes.ToArray(),
				ReturnUrl = returnUrl
			};

			var res = esep.CreateUIToSingEx(req);

			if (!res.RequestResult.WasSuccessful)
			{
				log4net.LogManager.GetLogger(GetType()).Error(String.Format("Ошибка ЕСЭП: {0}; {1};", res.RequestResult.ErrorCode, res.RequestResult.ErrorMessage));
				return null;
			}

			return res.Url;
		}

		public byte[] GetSignedFromEsep(string fileAccessCode)
		{
			var esepFile = new FileServiceClient();

         RestChild.Security.Logger.SecurityLogger.AddToLog(SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с АИС РУЛ", "GetSignedFromEsep", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         var res = esepFile.DownloadFileEx(new DownloadFileExRequest
			{
				Credentials = new Credentials
				{
					Login = ConfigurationManager.AppSettings["EsepLogin"],
					Password = ConfigurationManager.AppSettings["EsepPassword"]
				},
				FileAccessCode = fileAccessCode
			});

			if (res.RequestResult.WasSuccessful)
			{
				return res.File.Body;
			}

			return null;
		}

		public byte[] GetDocForSign(string actionUrl)
		{
			var baseUrl = ConfigurationManager.AppSettings["LocalSystemUrl"];
			if (String.IsNullOrWhiteSpace(baseUrl))
				baseUrl = "http://localhost";
			var req = HttpWebRequest.Create(baseUrl + actionUrl);
			using (var resp = req.GetResponse())
			{
				using (var stream = resp.GetResponseStream())
				{
					var res = new byte[resp.ContentLength];
					stream.Read(res, 0, Convert.ToInt32(resp.ContentLength));
					stream.Close();
					resp.Close();
					return res;
				}
			}
		}

		/// <summary>
		/// сохранить файл если нужно
		/// </summary>
		public static IList<EsepFileAccessCode> SaveSignsIfNeed(IUnitOfWork unitOfWork)
		{
			var codes = HttpContext.Current.Session["FileAccessCodes"] as IList<EsepFileAccessCode>;
			if (HttpContext.Current.Request["result"] == "0" && codes == null)
				log4net.LogManager.GetLogger(typeof(Esep)).WarnFormat("Нет FileAccessCodes когда result = 0. Url:{0} Ref:{1}",
					HttpContext.Current.Request.Url, HttpContext.Current.Request.UrlReferrer);
			if (codes == null)
				return null;

			HttpContext.Current.Session.Remove("FileAccessCode");
			HttpContext.Current.Session["FileAccessCodes"] = null;
			if (HttpContext.Current.Request["result"] != null && HttpContext.Current.Request["result"] == "0")
			{
				var isError = false;
				var haveData = false;
				var esep = new Esep();
				foreach (var code in codes.Where(c => !String.IsNullOrWhiteSpace(c.FileAccessCode) && c.EntityId > 0))
				{
					var file = esep.GetSignedFromEsep(code.FileAccessCode);
					var guid = code.FileAccessCode;
					code.FileAccessCode = null;
					if (file != null)
					{
						string fileShortName;
						string fileName;

						do
						{
							fileShortName = string.Format("{0}.data", Guid.NewGuid().ToString());
							fileName = System.IO.Path.Combine(Settings.Default.StorageSign, fileShortName);
						} while (System.IO.File.Exists(fileName));

						System.IO.File.WriteAllBytes(fileName, file);

						var res = new SignInfo
						{
							AccountId = Security.GetCurrentAccountId(),
							SignDate = DateTime.Now,
							Title = code.Title,
							FileUrl =fileShortName
						};

						res = unitOfWork.AddEntity(res);
						code.SignInfoId = res.Id;
						haveData = true;
					}
					else
					{
						log4net.LogManager.GetLogger(typeof(Esep)).WarnFormat("Из ESEP не вернулся файл {1}. Url:{0} Ref:{2}",
							HttpContext.Current.Request.Url, guid, HttpContext.Current.Request.UrlReferrer);
						isError = true;
					}
				}

				return haveData && !isError
					? codes.Where(c => c.EntityId > 0 && c.SignInfoId.HasValue).ToList()
					: null;
			}

			return null ;
		}

		public static string FullReturnUrl(string relativeUrl)
		{
			var baseUrl = Settings.Default.SystemUrl;
			if (String.IsNullOrWhiteSpace(baseUrl))
				return HttpContext.Current.Request.UrlReferrer.ToString();
			return baseUrl + relativeUrl;
		}

		public string UrlEsepVerifySign(byte[] signed, string returnUrl)
		{
         RestChild.Security.Logger.SecurityLogger.AddToLog(SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с АИС РУЛ", "UrlEsepVerifySign", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         var esepFile = new FileServiceClient();
			var request = new UploadFileExRequest
			{
				Credentials = new Credentials
				{
					Login = ConfigurationManager.AppSettings["EsepLogin"],
					Password = ConfigurationManager.AppSettings["EsepPassword"]
				},
				Description = "Данные ЭП",
				File = new File
				{
					Info = new FileInfo { Name = "Карточка", MimeType = "application/pdf" },
					Body = signed
				},
				StorageType = StorageType.Temporary
			};

			var res = esepFile.UploadFileEx(request);

			if (!res.RequestResult.WasSuccessful)
			{
				log4net.LogManager.GetLogger(GetType()).Error(String.Format("Ошибка ЕСЭП: {0}; {1};", res.RequestResult.ErrorCode, res.RequestResult.ErrorMessage));
				return null;
			}

			var esep = new EsepServiceClient();
			var req = new CreateUIToShowDocumentsRequest
			{
				Credentials = new EsepService.Credentials
				{
					Login = ConfigurationManager.AppSettings["EsepLogin"],
					Password = ConfigurationManager.AppSettings["EsepPassword"]
				},
				FileAccessCodes = new[] { res.FileAccessCode },
				ReturnUrl = returnUrl
			};
			var url = esep.CreateUIToShowDocuments(req);

			if (!res.RequestResult.WasSuccessful)
			{
				log4net.LogManager.GetLogger(GetType()).Error(String.Format("Ошибка ЕСЭП: {0}; {1};", res.RequestResult.ErrorCode, res.RequestResult.ErrorMessage));
				return null;
			}

			return url.Url;
		}
	}

	#region Help Classes

	[Serializable]
	[DataContract]
	public class DataForSign
	{
		[DataMember]
		public byte[] Content { get; set; }
		[DataMember]
		public string ActionCode { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string MimeType { get; set; }
		[DataMember]
		public string Title { get; set; }
		[DataMember]
		public long EntityId { get; set; }
		[DataMember]
		public SignTypeEnum SignType { get; set; }
		[DataMember]
		public string Commentary { get; set; }

	}

	[Serializable]
	[DataContract]
	public class EsepFileAccessCode
	{
		[DataMember]
		public long EntityId { get; set; }

		[DataMember]
		public string ActionCode { get; set; }

		[DataMember]
		public SignTypeEnum SignType { get; set; }

		[DataMember]
		public string FileAccessCode { get; set; }

		[DataMember]
		public string Title { get; set; }

		[IgnoreDataMember]
		public long? SignInfoId { get; set; }

		[DataMember]
		public string Commentary { get; set; }
	}

	#endregion
}
