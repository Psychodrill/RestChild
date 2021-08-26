using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Booking.Logic.Services
{
   public class BookingServiceAuthenticator : Security.Service.ServiceAuthenticator
   {
      private const string _uname = "Booking.BookingService.UserName";
      private const string _pass = "Booking.BookingService.Password";

      protected override string GetPassword()
      {
         return System.Configuration.ConfigurationManager.AppSettings[_uname];
      }

      protected override string GetUserName()
      {
         return System.Configuration.ConfigurationManager.AppSettings[_pass];
      }
   }
}
