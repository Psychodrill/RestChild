using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using RestChild.Comon;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     LogAspect
    /// </summary>
    public class LogAspect : IInterceptor
    {
        /// <summary>
        ///     LogAspect
        /// </summary>
        /// <param name="logger">logger</param>
        public LogAspect(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
                var task = invocation.ReturnValue as Task;
                if (task == null)
                {
                    return;
                }

                Action<Task> action = t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        Logger.Error(CreateInvocationLogString(invocation, t.Exception), t.Exception);
                    }
                };

                task.ContinueWith(action);
            }
            catch (Exception ex)
            {
                var logString = CreateInvocationLogString(invocation, ex);
                Logger.Error(logString, ex);

                var ti = ex as TargetInvocationException;
                if (ti != null && ti.InnerException != null)
                {
                    ReflectionHelper.PreserveStackTrace(ti.InnerException);
                    throw ti.InnerException;
                }

                throw;
            }
        }

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

            var he = exception as HttpResponseException;
            if (he != null)
            {
                sb.AppendLine(he.Response.ToString());
            }

            return sb.ToString();
        }
    }
}
