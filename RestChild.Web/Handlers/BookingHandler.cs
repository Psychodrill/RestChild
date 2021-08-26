using RestChild.Comon;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace RestChild.Web.Handlers
{
   public class BookingHandler : IHttpHandler
   {
      public BookingHandler()
      {
      }

      public bool IsReusable => false;

      public void ProcessRequest(HttpContext context)
      {
         try
         {
            var text = new StreamReader(context.Request.InputStream).ReadToEnd();

            if (!string.IsNullOrWhiteSpace(text))
            {
               var unit = WindsorHolder.Resolve<IUnitOfWork>();

               try
               {
                  unit.AddEntity(new ScheduleMessage()
                  {
                     Message = text,
                     DateMessage = DateTime.Now,
                     Processed = false,
                     HasError = false,
                     ErrorMessage = string.Empty
                  });

                  unit.SaveChanges();
               }
               finally
               {
                  WindsorHolder.Release(unit);
               }

               context.Response.StatusCode = (int)HttpStatusCode.OK;
               return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
         }
         catch (ArgumentOutOfRangeException)
         {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
         }
         catch (Exception)
         {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
         }
      }
   }
}
