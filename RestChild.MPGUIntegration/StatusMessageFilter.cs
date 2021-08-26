using System;

namespace RestChild.MPGUIntegration
{
   public struct StatusMessageFilter
   {
      public struct BaseDeclarant
      {
         public string LastName { get; set; }

         public string FirstName { get; set; }

         public string MiddleName { get; set; }

         public DateTime? BirthDate { get; set; }

         public string MobilePhone { get; set; }

         public string WorkPhone { get; set; }

         public string EMail { get; set; }
      }

      public string ServiceNumber { get; set; }

      public int StatusCode { get; set; }

      public string StatusTitle { get; set; }

      public DateTime StatusDate { get; set; }

      public string Note { get; set; }

      public string PINCode { get; set; }

      public DateTime? Cell { get; set; }

      public long? BookingId { get; set; }

      public string StatusReason { get; set; }

      public string PurposeValue { get; set; }

      public string KidsCountValue { get; set; }

      public BaseDeclarant? Declarant { get; set; }

      public StatusMessageFilter(string ServiceNumber, int StatusCode, string StatusTitle, DateTime StatusDate)
      {
         this.ServiceNumber = ServiceNumber;
         this.StatusCode = StatusCode;
         this.StatusTitle = StatusTitle;
         this.StatusDate = StatusDate;

         Note = null;
         PINCode = null;
         Cell = null;
         BookingId = null;
         StatusReason = null;
         Declarant = null;
         PurposeValue = null;
         KidsCountValue = null;
      }
   }
}
