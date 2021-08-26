using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.CshedIntegration.CshedRefBiz;
using UtilsSmev.Crypto;

namespace RestChild.CshedIntegration
{
    /// <summary>
    ///     Работа с РЦХЭД
    /// </summary>
    public static class CshwdClient
    {
        private static StoreLocation? storeLocation;
        private static X509FindType? findType;
        private static StoreName? storeName;

        /// <summary>
        ///     Хранилище сертификатов
        /// </summary>
        private static StoreLocation StoreLocation
        {
            get
            {
                if (!storeLocation.HasValue)
                {
                    storeLocation = (StoreLocation) Enum.Parse(typeof(StoreLocation),
                        ConfigurationManager.AppSettings["clientCertStoreLocation"], true);
                }

                return storeLocation.Value;
            }
        }

        /// <summary>
        ///     Тип
        /// </summary>
        private static X509FindType FindType
        {
            get
            {
                if (!findType.HasValue)
                {
                    findType = (X509FindType) Enum.Parse(typeof(X509FindType),
                        ConfigurationManager.AppSettings["clientCertFindType"], true);
                }

                return findType.Value;
            }
        }

        /// <summary>
        ///     Наименование хранилища
        /// </summary>
        private static StoreName StoreName
        {
            get
            {
                if (!storeName.HasValue)
                {
                    storeName = (StoreName) Enum.Parse(typeof(StoreName),
                        ConfigurationManager.AppSettings["clientCertStoreName"], true);
                }

                return storeName.Value;
            }
        }

        ///// <summary>
        ///// Сохранить документ в РЦХЭД
        ///// </summary>
        //public static string SendDocument(RchedDocument Doc, Castle.Core.Logging.ILogger Logger)
        //{
        //   using (var csClient = new CshedRefBiz.CustomWebServiceImplClient())
        //   {
        //      if (csClient.ClientCredentials != null)
        //      {
        //         csClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["CshedLogin"];
        //         csClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["CshedPass"];
        //      }

        //      try
        //      {
        //         var mimeType = MimeTypeMap.GetMimeType(".docx");

        //         var zdoc = new CshedRefBiz.CreateDocumentRequest
        //         {
        //            Document = Doc.RequestDocument,
        //            DocumentClass = Doc.RequestFileType.CodeChed,
        //            SSOID = string.IsNullOrWhiteSpace(Doc.SsoId) ? "0" : Doc.SsoId,
        //            FromSystemCode = ConfigurationManager.AppSettings["CshedLogin"],
        //            ServerStore = ConfigurationManager.AppSettings["CshedServerStore"],
        //            properties = new[]
        //            {
        //               new CshedRefBiz.Property {Name = "ASGUF_Code", Value = Doc.RequestFileType.CodeAsGuf},
        //               new CshedRefBiz.Property {Name = "MimeType", Value = mimeType},
        //               new CshedRefBiz.Property {Name = "DocumentTitle", Value = "Уведомление.docx"},
        //            }
        //         };

        //         var docId = csClient.CreateDocument(zdoc);

        //         Logger?.Info($"RestChild.CshedIntegration.SendDocument - document saved in CHED(ReqId={Doc.RequestId}, CHED={docId})");

        //         Console.WriteLine(docId);

        //         if(Doc.SignRequest)
        //         {
        //            var cryptoHelper = new CryptoHelper();
        //            uint nErrorCode;
        //            string error;

        //            var certificate = GetCertificate(Doc.CertificateId);
        //            string certificateB64 = Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
        //            var algorithm = cryptoHelper.GetCertificateAlgorithm(certificateB64, out nErrorCode, out error);

        //            var pinCode = ConfigurationManager.AppSettings["clientCertPinCode"] ?? string.Empty;

        //            var hash = cryptoHelper.GetHashesB64(Doc.RequestDocument, false, out nErrorCode, out error);
        //            Console.WriteLine($"nerror1 length:{nErrorCode} - {error}");

        //            var sign = cryptoHelper.SignHashPkcs7(hash[algorithm.Value], certificateB64, pinCode, storeLocation, SignAlgorithmType.CMS, null, null, null, null, out nErrorCode, out error);
        //            Console.WriteLine($"nerror2 length:{nErrorCode} - {error}");

        //            Console.WriteLine($"sign length:{sign.Length}");

        //            var response = csClient.CreateAnnotation(new CshedRefBiz.CreateAnnotationRequest
        //            {
        //               Document = sign,
        //               Description = "подпись",
        //               MimeType = mimeType,
        //               DocumentId = docId,
        //               SignType = CshedRefBiz.signType.CADES,
        //               Properties = new[]
        //               {
        //                  new CshedRefBiz.Property {Name = "ENO", Value = "123456"},
        //               }
        //            });

        //            Logger?.Info($"RestChild.CshedIntegration.SendDocument - document signed in CHED(ReqId={Doc.RequestId}, CHED={docId}, MSG={response})");

        //            return $"RestChild.CshedIntegration.SendDocument - document signed in CHED(ReqId={Doc.RequestId}, CHED={docId}, MSG={response})";
        //         }

        //         return docId;
        //      }
        //      catch (Exception ex)
        //      {
        //         Logger?.Error($"RestChild.CshedIntegration.SendDocument - Ошибка загрузки/подписания файла в РЦХЭД ReqId={Doc.RequestId}", ex);
        //         throw ex;
        //      }
        //   }
        //}

        //public static string SignDocument(RchedDocument Doc, Castle.Core.Logging.ILogger Logger)
        //{
        //   try
        //   {
        //      using (var csClient = new CshedRefBiz.CustomWebServiceImplClient())
        //      {
        //         if (csClient.ClientCredentials != null)
        //         {
        //            csClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["CshedLogin"];
        //            csClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["CshedPass"];
        //         }

        //         var mimeType = MimeTypeMap.GetMimeType(".pdf");

        //         var cryptoHelper = new CryptoHelper();
        //         uint nErrorCode;
        //         string error;

        //         var certificate = GetCertificate(Doc.CertificateId);
        //         string certificateB64 = Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
        //         var algorithm = cryptoHelper.GetCertificateAlgorithm(certificateB64, out nErrorCode, out error);

        //         var pinCode = ConfigurationManager.AppSettings["clientCertPinCode"] ?? string.Empty;

        //         var hash = cryptoHelper.GetHashesB64(Doc.RequestDocument, false, out nErrorCode, out error);
        //         Console.WriteLine($"nerror1 length:{nErrorCode} - {error}");

        //         var sign = cryptoHelper.SignHashPkcs7(hash[algorithm.Value], certificateB64, pinCode, storeLocation, SignAlgorithmType.CMS, null, null, null, null, out nErrorCode, out error);
        //         Console.WriteLine($"nerror2 length:{nErrorCode} - {error}");

        //         Console.WriteLine($"sign length:{sign.Length}");

        //         var response = csClient.CreateAnnotation(new CshedRefBiz.CreateAnnotationRequest
        //         {
        //            Document = sign,
        //            Description = "подпись",
        //            MimeType = mimeType,
        //            DocumentId = Doc.docId,
        //            SignType = CshedRefBiz.signType.CADES,
        //            Properties = new[]
        //            {
        //                  new CshedRefBiz.Property {Name = "ENO", Value = "123456"},
        //               }
        //         });

        //         return $"RestChild.CshedIntegration.SignDocument - document signed in CHED(ReqId={Doc.RequestId}, CHED={Doc.docId}, MSG={response})";
        //      }
        //   }
        //   catch(Exception ez)
        //   {
        //      throw ez;
        //   }
        //}

        /// <summary>
        ///     отправить документ в РЦХЭД
        /// </summary>
        public static string SendDocumentToCshed(ICshedDocument Doc, ILogger Logger = null)
        {
            using (var csClient = new CustomWebServiceImplClient())
            {
                if (csClient.ClientCredentials != null)
                {
                    csClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["CshedLogin"];
                    csClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["CshedPass"];
                }

                var zdoc = new CreateDocumentRequest
                {
                    Document = Doc.FileBody,
                    DocumentClass = Doc.CodeChed,
                    SSOID = string.IsNullOrWhiteSpace(Doc.SsoId) ? "0" : Doc.SsoId,
                    FromSystemCode = ConfigurationManager.AppSettings["CshedLogin"],
                    ServerStore = ConfigurationManager.AppSettings["CshedServerStore"],
                    properties = new[]
                    {
                        new Property {Name = "ASGUF_Code", Value = Doc.CodeAsGuf},
                        new Property {Name = "MimeType", Value = Doc.MimeType},
                        new Property {Name = "DocumentTitle", Value = Doc.FileName}
                    }
                };

                var docId = csClient.CreateDocument(zdoc);

                Logger?.Info(
                    $"RestChild.CshedIntegration.SendDocumentToCshed - document saved in CHED(ReqId={Doc.RequestId}, CHED={docId})");

                //Console.WriteLine(docId);

                return docId;
            }
        }

        /// <summary>
        ///     подписать документ в РЦХЭД
        /// </summary>
        public static string SignDocumentToCshed(ICshedDocument Doc, string docId, string certificateKey, ILogger Logger = null)
        {
            using (var csClient = new CustomWebServiceImplClient())
            {
                if (csClient.ClientCredentials != null)
                {
                    csClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["CshedLogin"];
                    csClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["CshedPass"];
                }

                var cryptoHelper = new CryptoHelper();
                uint nErrorCode;
                string error;

                var certificate = GetCertificate(certificateKey ?? ConfigurationManager.AppSettings["serverCert"]);
                var certificateB64 = Convert.ToBase64String(certificate.Export(X509ContentType.Cert));
                var algorithm = cryptoHelper.GetCertificateAlgorithm(certificateB64, out nErrorCode, out error);

                if(!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error);

                var pinCode = ConfigurationManager.AppSettings["documentCertPinCode"] ?? string.Empty;

                var hash = cryptoHelper.GetHashesB64(Doc.FileBody, false, out nErrorCode, out error);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error);

                var sign = cryptoHelper.SignHashPkcs7(hash[algorithm.Value], certificateB64, pinCode, storeLocation, SignAlgorithmType.CMS, null, null, null, null, out nErrorCode, out error);

                if (!string.IsNullOrWhiteSpace(error))
                    throw new Exception(error + $" cert={(certificateKey ?? ConfigurationManager.AppSettings["serverCert"])}");

                var response = csClient.CreateAnnotation(new CreateAnnotationRequest
                {
                    Document = sign,
                    Description = "подпись",
                    MimeType = Doc.MimeType,
                    DocumentId = docId,
                    SignType = signType.CADES
                });

                Logger?.Info(
                    $"RestChild.CshedIntegration.SignDocumentToCshed - document signed in CHED(ReqId={Doc.RequestId}, CHED={docId}, MSG={response})");

                return response;
            }
        }

        /// <summary>
        ///     Извлечь сертификат
        /// </summary>
        private static X509Certificate2 GetCertificate(string certificate)
        {
            var store = new X509Store(StoreName, StoreLocation);
            store.Open(OpenFlags.ReadOnly);

            //сертификат
            var coll = store.Certificates.Find(FindType, certificate.Trim(), true);

            if (coll.Count == 0)
            {
                throw new FileNotFoundException($"Сертификат клиента не найден. Отпечаток {certificate}");
            }

            return coll[0];
        }
    }
}
