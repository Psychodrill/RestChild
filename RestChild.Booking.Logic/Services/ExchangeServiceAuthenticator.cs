using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Booking.Logic.Services
{
   public class ExchangeServiceAuthenticator : Security.Service.ServiceAuthenticator
   {
      private const string _uname = "Booking.ExchangeService.UserName";
      private const string _pass = "Booking.ExchangeService.Password";

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
