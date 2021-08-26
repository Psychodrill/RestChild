using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Security
{
    /// <summary>
    /// Класс авторизации
    /// </summary>
    public class AuthorizeWithAnonTimeoutAttribute : AuthorizeAttribute
    {
        private const string logoff = "LogOff";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var result = base.AuthorizeCore(httpContext);

            if (result)
            {
                if (httpContext.Request.RawUrl.IndexOf(logoff, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    return result;
                }


                var user = httpContext.User;
                if (user != null)
                {
                    var sessionUid = user.GetClaimValue(ClaimTypes.UserData);
                    if (!string.IsNullOrWhiteSpace(sessionUid))
                    {
                        var userData = UserWatcher.UsersDataGet(sessionUid);

                        #region User Update Activity / Force LogOff

                        if (userData == null)
                        {
                            logOff(httpContext);
                            return false;
                        }

                        if (userData.kickOff)
                        {
                            logOff(httpContext);
                            UserWatcher.UserExitSet(sessionUid);
                            return false;
                        }

                        if (userData.updateActivity)
                        {
                            UserWatcher.UserUpdate(sessionUid);
                        }

                        #endregion

                        #region User Force Change Password

                        if (userData.forceChangePassword)
                        {
                            var rd = httpContext.Request.RequestContext.RouteData;
                            var currentAction = rd.GetRequiredString("action");
                            var currentController = rd.GetRequiredString("controller");
                            //string currentArea = rd.Values["area"] as string;

                            if (!string.Equals(currentAction, "ForceChangePassword") ||
                                !string.Equals(currentController, "Account"))
                            {
                                httpContext.Response.Redirect(
                                   $"/Account/ForceChangePassword/?returnUrl={httpContext.Request.RawUrl}{(userData.firstTimeLogin ? "&first=true" : string.Empty)}");
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        //User Authorized but SessionUid is empty.
                        result = false;
                    }
                }
            }

            return result;
        }

        private void logOff(HttpContextBase httpContext)
        {
            httpContext.GetOwinContext().Authentication.SignOut();
        }
    }
}
