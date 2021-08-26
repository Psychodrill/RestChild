using Castle.Core.Logging;
using Castle.DynamicProxy;
using RestChild.Comon;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RestChild.Admin.Web.Controllers
{
   /// <summary>
   /// LogAspect
   /// </summary>
   public class LogAspect : IInterceptor
   {
      /// <summary>
      /// LogAspect
      /// </summary>
      /// <param name="logger">logger</param>
      public LogAspect(ILogger logger)
      {
         Logger = logger;
      }

      public ILogger Logger { get; set; }

      public static string CreateInvocationLogString(IInvocation invocation, Exception exception)
      {
         var sb = new StringBuilder(100);
         sb.AppendFormat("Called: {0}.{1}(", invocation.TargetType.Name, invocation.Method.Name);
         foreach (var argument in invocation.Arguments)
         {
            var argumentDescription = argument == null ? "null" : argument.ToString();
            sb.Append(argumentDescription).Append(",");
         }

         if (invocation.Arguments.Any())
         {
            sb.Length--;
         }

         sb.Append(")");
         sb.AppendLine();

         if (HttpContext.Current != null)
         {
            sb.AppendLine("From");
            sb.AppendLine(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
            sb.AppendLine(HttpContext.Current.Request.Url.ToString());
            sb.AppendLine("Params:");
            sb.AppendLine(HttpContext.Current.Request.QueryString.ToString("\r\n"));
            sb.AppendLine(HttpContext.Current.Request.Form.ToString("\r \n"));
         }

         if (exception is HttpResponseException he)
         {
            sb.AppendLine(he.Response.ToString());
         }

         return sb.ToString();
      }

      private static void logOff()
      {
         HttpContext.Current.Session.Clear();
         HttpContext.Current.Session.Abandon();
         HttpContext.Current.User = null;
         System.Web.Security.FormsAuthentication.SignOut();
      }

      public void Intercept(IInvocation invocation)
      {
         try
         {
            invocation.Proceed();
            if (!(invocation.ReturnValue is Task task))
            {
               return;
            }

            void Action(Task t)
            {
               if (t.IsFaulted && t.Exception != null)
               {
                  Logger.Error(CreateInvocationLogString(invocation, t.Exception), t.Exception);
               }
            }

            task.ContinueWith(Action);
         }
         catch (Exception ex)
         {
            var logString = CreateInvocationLogString(invocation, ex);
            Logger.Error(logString, ex);

            if (!(ex is TargetInvocationException ti) || ti.InnerException == null)
            {
               throw;
            }

            ReflectionHelper.PreserveStackTrace(ti.InnerException);
            throw ti.InnerException;
         }
      }
   }
}
