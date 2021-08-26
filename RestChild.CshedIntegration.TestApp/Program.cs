using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.CshedIntegration.TestApp
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            Console.WriteLine("Begin");
            using (var unitOfWork = new UnitOfWork())
            {
               //ILogger Logger = WindsorHolder.Resolve<ILogger>();
               /*if(false)
               {
                  var doc = new Models.RchedDocument
                  {
                     RequestFileType = unitOfWork.GetSet<RequestFileType>().FirstOrDefault(t => t.Id == 10),
                     RequestDocument = GetFile(),
                     RequestId = 666666,
                     SignRequest = true,
                     CertificateId = ConfigurationManager.AppSettings["serverCert"] //"ep-ov"
                  };

                  //var result = CshwdClient.SendDocument(doc, null);

                  Console.WriteLine("Result:");
                  //Console.WriteLine(result);
                  Console.WriteLine("End");
               }*/


               if (true)
               {
                  var doc = new Models.RchedDocument
                  {
                     RequestFileType = unitOfWork.GetSet<RequestFileType>().FirstOrDefault(t => t.Id == 10),
                     RequestDocument = GetFilePdf(),
                     RequestId = 666666,
                     SignRequest = true,
                     docId = "8e8f7f0d-b763-4426-9311-33a3425a9dab",
                     CertificateId = ConfigurationManager.AppSettings["serverCert"] //"ep-ov"
                  };

                  //var result = CshwdClient.SignDocument(doc, null);

                  Console.WriteLine("Result:");
                  //Console.WriteLine(result);
                  Console.WriteLine("End");

               }
            }
         }
         catch (Exception ez)
         {
            Console.WriteLine("Error:");
            Console.WriteLine(ez.Message);
         }
         Console.ReadKey();
      }

      private static byte[] GetFile()
      {
         return File.ReadAllBytes("C:\\Temp\\СhedTestFile.docx");
      }

      private static byte[] GetFilePdf()
      {
         return File.ReadAllBytes("C:\\Temp\\СhedTestCertFile.pdf");
      }

   }
}
