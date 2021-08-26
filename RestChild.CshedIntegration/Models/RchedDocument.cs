using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.CshedIntegration.Models
{
   /// <summary>
   /// набор данных для передачи в РЦХЭД
   /// </summary>
   public class RchedDocument
   {
      public RchedDocument()
      {
         signRequest = false;
      }

      /// <summary>
      /// Документ
      /// </summary>
      public byte[] RequestDocument { get; set; }

      /// <summary>
      /// Тип запроса
      /// </summary>
      public RequestFileType RequestFileType { get; set; }

      /// <summary>
      /// Идентификатор запроса
      /// </summary>
      public long RequestId { get; set; }

      /// <summary>
      /// SSOId
      /// </summary>
      public string SsoId { get; set; }

      /// <summary>
      /// Признак необходимости подписи документа
      /// </summary>
      public bool SignRequest
      {
         get
         {
            return signRequest && !string.IsNullOrWhiteSpace(CertificateId);
         }
         set
         {
            signRequest = value;
         }
      }            
      private bool signRequest;

      /// <summary>
      /// Сертификат для подписи
      /// </summary>
      public string CertificateId { get; set; }

      /// <summary>
      /// идентификатор документа в ЦХЭД
      /// </summary>
      public string docId { get; set; }
   }
}
