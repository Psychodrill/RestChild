using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Castle.Core.Logging;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Extensions;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Security;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    [Authorize]
    [HandleError]
    public class AccountController : BaseController
    {
        public WebAccountController WebAccountController { get; set; }

        public WebAccessRightsController ApiAccessRightsController { get; set; }

        public WebRoleController ApiRolesController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            WebAccountController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiAccessRightsController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRolesController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            ViewBag.ReturnUrl = returnUrl;

            var _sessionUid = HttpContext.User.GetClaimValue(ClaimTypes.UserData);
            if (!string.IsNullOrEmpty(_sessionUid))
            {
                AuthenticationManager.SignOut();
                UserWatcher.UserSessionExitMark(UnitOfWork, _sessionUid);
                return RedirectToAvalibleAction();
            }

            return View();
        }

        public ActionResult Manage(long? id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (id != Security.GetCurrentAccountId())
            {
                return RedirectToAvalibleAction();
            }

            var account = WebAccountController.GetAccount(id ?? 0) ?? new Account {IsActive = true};
            var vm = new AccountManageViewModel(account);

            SetupModel(vm);

            return View(vm);
        }

        private void SetupModel(AccountManageViewModel vm)
        {
            vm.Rights.AddRange(
                ApiAccessRightsController.Get()
                    .Where(r => !r.ForOrganization && vm.Rights.All(vmr => vmr.AccessRightId != r.Id))
                    .Select(r => new AccountRights {AccessRight = r, AccessRightId = r.Id, Id = 0})
                    .ToList());

            foreach (var code in vm.Rights.Where(r => !string.IsNullOrWhiteSpace(r.AccessRight.GroupCode)).ToList())
            {
                if (!Security.HasRight(code.AccessRight.GroupCode))
                {
                    vm.Rights.Remove(code);
                }
            }

            vm.Rights = vm.Rights.OrderBy(r => r.AccessRight.Name).ToList();

            vm.AccessRights = ApiAccessRightsController.Get().Where(a => a.ForOrganization).OrderBy(a => a.Name)
                .ToList();
            foreach (var code in vm.AccessRights.Where(r => !string.IsNullOrWhiteSpace(r.GroupCode)).ToList())
            {
                if (!Security.HasRight(code.GroupCode))
                {
                    vm.AccessRights.Remove(code);
                }
            }

            vm.AccessRoles = ApiRolesController.Get().OrderBy(r => r.Name).ToList();

            foreach (var role in vm.AccessRoles.ToList())
            {
                if (role.AccessRights.Any(a =>
                    !string.IsNullOrWhiteSpace(a.GroupCode) && !Security.HasRight(a.GroupCode)))
                {
                    vm.AccessRoles.Remove(role);
                }
            }
        }

        [HttpPost]
        public ActionResult SaveAccount(AccountManageViewModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var account = model.BuildData();
            var errors =
                RestChild.Security.AccountExtentions.AccountChecker.AccountCheck(UnitOfWork, model.Data.Login,
                    model.Data.Id);

            if (errors == null || !errors.Any())
            {
                account = WebAccountController.SaveAccount(account);
            }
            else
            {
                if (account.Rights != null && Security.HasRight(AccessRightEnum.AccountManage))
                {
                    var accessRights = ApiAccessRightsController.Get().ToList();
                    foreach (var right in account.Rights)
                    {
                        right.AccessRight = accessRights.FirstOrDefault(ar => ar.Id == right.AccessRightId);
                    }
                }

                if (account.Roles != null && Security.HasRight(AccessRightEnum.AccountManage))
                {
                    var roles = ApiRolesController.Get().ToList();
                    foreach (var role in account.Roles)
                    {
                        role.Role = roles.FirstOrDefault(r => r.Id == role.RoleId);
                    }
                }

                var newModel = new AccountManageViewModel(account);
                newModel.ErrorMessage = $"<ul>{string.Join("", errors.Select(e => $"<li>{e}</li>").ToList())}</ul>";
                SetupModel(newModel);
                return View("Manage", newModel);
            }

            return RedirectToAction("Manage", new {id = account.Id});
        }

        public ActionResult ChangePassword(long id, string password, string passwordConfirm)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (id != Security.GetCurrentAccountId())
            {
                return RedirectToAvalibleAction();
            }

            List<string> errors = new List<string>();
            if (password != passwordConfirm)
            {
                errors.Add("Пароль и повтор пароля не совпадают.");
            }

            var account = WebAccountController.GetAccount(id);
            var vm = new AccountManageViewModel(account);

            SetupModel(vm);

            if (errors.Count() < 1)
            {
                errors.AddRange(
                    RestChild.Security.AccountExtentions.PasswordSetReposytory.AccountSetPassword(UnitOfWork, password,
                        id));
            }

            if (errors != null)
            {
                vm.ErrorMessage = $"<ul>{string.Join("", errors.Select(e => $"<li>{e}</li>").ToList())}</ul>";
                return View("Manage", vm);
            }

            return RedirectToAction("Manage", new {id = id});
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var accountBlockSpan = SecuritySettings.AccountBlockSpan;
            var maxCountUnSuccess = SecuritySettings.MaxCountUnsuccess;
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (ModelState.IsValid)
            {
                Account account = UnitOfWork.GetSet<Account>().GetAccountByLogin(model.UserName);

                if (account != null)
                {
                    if (SecurityBasis.AccountIsIB(account) || SecurityBasis.AccountIsAdmin(account))
                    {
                        UnitOfWork.AddEntity(new AccountHistoryLogin
                        {
                            DateEnter = DateTime.Now,
                            //DateLastActivity = DateTime.Now,
                            UserAgent = Request.UserAgent,
                            RemoteAddr = RequestHelpers.GetClientIpAddress(Request),
                            StopSession = true,
                            Login = model.UserName,
                            IsAuthorized = false,
                        });

                        ModelState.AddModelError("", "Не верные права для доступа.");

                        return View(model);
                    }

                    if (accountBlockSpan > 0 && account.CountUnsuccess > 0)
                    {
                        if (DateTime.Now.AddMinutes(-accountBlockSpan) < account.DateLastUnsuccess &&
                            maxCountUnSuccess <= account.CountUnsuccess)
                        {
                            ModelState.AddModelError("",
                                $"Превышено количество не успешных попыток авторизации. Пользователь временно заблокирован. Повтор авторизации через {accountBlockSpan} мин.");
                            return View(model);
                        }
                    }

                    byte[] hash =
                        PasswordUtility.GetPasswordHash(model.Password, Convert.FromBase64String(account.Salt));

                    if (hash.SequenceEqual(Convert.FromBase64String(account.Password)))
                    {
                        SignIn(account, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                }

                if (account != null)
                {
                    if (accountBlockSpan > 0)
                    {
                        if (DateTime.Now.AddMinutes(-accountBlockSpan) > account.DateLastUnsuccess)
                        {
                            account.CountUnsuccess = 0;
                        }
                    }

                    account.DateLastUnsuccess = DateTime.Now;
                    account.CountUnsuccess += 1;
                    UnitOfWork.SaveChanges();
                }

                UnitOfWork.AddEntity(new AccountHistoryLogin
                {
                    DateEnter = DateTime.Now,
                    //DateLastActivity = DateTime.Now,
                    UserAgent = Request.UserAgent,
                    RemoteAddr = RequestHelpers.GetClientIpAddress(Request),
                    StopSession = true,
                    Login = model.UserName,
                    IsAuthorized = false,
                });

                ModelState.AddModelError("",
                    account != null && accountBlockSpan > 0 && account.CountUnsuccess >= maxCountUnSuccess
                        ? $"Превышено количество неуспешных попыток авторизации. Пользователь временно заблокирован. Повтор авторизации через {accountBlockSpan} мин."
                        : "Неверный пользователь или пароль.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            var _sessionUid = HttpContext.User.GetClaimValue(ClaimTypes.UserData);
            AuthenticationManager.SignOut();
            UserWatcher.UserSessionExitMark(UnitOfWork, _sessionUid);
            return RedirectToAvalibleAction();
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void SignIn(Account user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, user.Name)},
                DefaultAuthenticationTypes.ApplicationCookie,
                ClaimTypes.Name,
                ClaimTypes.Role);

            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString(CultureInfo.InvariantCulture)));

            var timeout = SecuritySettings.SessionTimeout;
            if (timeout > 0)
            {
                identity.AddClaim(new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(timeout).ToString("o")));
            }

            var session = $"{Guid.NewGuid().ToString()}::{user.Id}";
            SecurityBasis.SaveSecurity(user.Id, identity, Security.GetSecurity(user.Id), session);

            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);

            user.CountUnsuccess = 0;
            user.DateLastUnsuccess = null;

            UnitOfWork.AddEntity(new AccountHistoryLogin
            {
                AccountId = user.Id,
                DateEnter = DateTime.Now,
                SessionUid = session,
                UserAgent = Request.UserAgent,
                RemoteAddr = RequestHelpers.GetClientIpAddress(Request),
                Login = user.Login,
                IsAuthorized = true,
                DateLastActivity = DateTime.Now
            });
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAvalibleAction();
        }

        #endregion

        #region Force Change Password

        [HttpGet]
        public ActionResult ForceChangePassword(string returnUrl, bool? first = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var sessionUid = HttpContext.User.GetClaimValue(ClaimTypes.UserData);
            if (!UserWatcher.UserForceChangePasswordNeed(UnitOfWork, sessionUid))
            {
                RedirectToAvalibleAction();
            }

            ForceChangePasswordModelBase model = new ForceChangePasswordModelBase()
            {
                FirstTimeAuth = first.Value,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForceChangePassword(ForceChangePasswordModelBase model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (model.NewPassword != model.NewPasswordRpt)
            {
                ModelState.AddModelError(string.Empty, "Пароль и повтор пароля не совпадают");
                model.NewPassword = model.NewPasswordRpt = string.Empty;
                return View(model);
            }

            var _passErrors = RestChild.Security.AccountExtentions.PasswordSetReposytory.AccountSetPassword(UnitOfWork,
                model.NewPassword, Security.GetCurrentAccountId().Value);
            if (_passErrors != null && _passErrors.Count > 0)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", _passErrors));
                model.NewPassword = model.NewPasswordRpt = string.Empty;
                return View(model);
            }

            UserWatcher.UserDataDrop(HttpContext.User.GetClaimValue(ClaimTypes.UserData));

            if (string.IsNullOrWhiteSpace(model.ReturnUrl))
            {
                return RedirectToAvalibleAction();
            }

            return Redirect(model.ReturnUrl);
        }

        #endregion
    }
}
