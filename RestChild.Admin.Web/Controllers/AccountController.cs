using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Admin.Web.Controllers.WebApi;
using RestChild.Admin.Web.Models;
using RestChild.Comon.Extensions;
using RestChild.Security;

namespace RestChild.Admin.Web.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтами пользователей
    /// </summary>
    [Authorize]
    [HandleError]
    public class AccountController : BaseController
    {
        #region Constructor

        public WebAccountController WebAccountController { get; set; }

        public WebRoleController ApiRolesController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            WebAccountController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRolesController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        #endregion

        /// <summary>
        /// Список/поиск аккаунтов
        /// </summary>
        public ActionResult AccountList(string search, int pageNumber = 1, int pageSize = 15)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.AccountManage))
            {
                return RedirectToAvaliableAction();
            }

            ViewBag.Search = search;

            var pager = new PagerState(pageNumber, pageSize);
            return View(WebAccountController.AccountList(search, pager));
        }

        /// <summary>
        /// Управление аккаунтом
        /// </summary>
        /// <param name="id">Идентификатор аккаунта</param>
        public ActionResult Manage(long? id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (id != Security.GetCurrentAccountId() && !Security.HasRight(AccessRightEnum.AccountManage))
            {
                return RedirectToAvaliableAction();
            }

            var account = WebAccountController.GetAccount(id ?? 0) ?? new Account { IsActive = true };
            var vm = new AccountManageViewModel(account);

            SetupModel(vm);

            return View(vm);
        }

        /// <summary>
        /// Настройка модели аккаунта
        /// </summary>
        private void SetupModel(AccountManageViewModel vm)
        {
            vm.Rights.AddRange(
               GetRights()
                  .Where(r => !r.ForOrganization && vm.Rights.All(vmr => vmr.AccessRightId != r.Id))
                  .Select(r => new AccountRights { AccessRight = r, AccessRightId = r.Id, Id = 0 })
                  .ToList());

            foreach (var code in vm.Rights.Where(r => !string.IsNullOrWhiteSpace(r.AccessRight.GroupCode)).ToList())
            {
                if (!Security.HasRight(code.AccessRight.GroupCode))
                {
                    vm.Rights.Remove(code);
                }
            }

            vm.Rights = vm.Rights.OrderBy(r => r.AccessRight.Name).ToList();

            vm.AccessRights = GetRights().Where(a => a.ForOrganization).OrderBy(a => a.Name).ToList();
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
                if (role.AccessRights.Any(a => !string.IsNullOrWhiteSpace(a.GroupCode) && !Security.HasRight(a.GroupCode)))
                {
                    vm.AccessRoles.Remove(role);
                }
            }
        }

        /// <summary>
        /// Удалить аккаунт
        /// </summary>
        [HttpGet]
        public ActionResult DeleteUser(long? accountId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.AccountManage))
            {
                return RedirectToAvaliableAction();
            }

            var account = WebAccountController.GetAccount(accountId ?? 0);
            account.IsDeleted = true;
            account.DateDelete = DateTime.Now;

            account = WebAccountController.SaveAccount(account);

            return RedirectToAction("Manage", new { @id = accountId.Value });
        }

        /// <summary>
        /// Сохранить данные аккаунта
        /// </summary>
        [HttpPost]
        public ActionResult SaveAccount(AccountManageViewModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (model.Data.Id != Security.GetCurrentAccountId() && !Security.HasRight(AccessRightEnum.AccountManage))
            {
                return RedirectToAvaliableAction();
            }

            var errors = new List<string>();
            var account = model.BuildData();
            errors.AddRange(RestChild.Security.AccountExtentions.AccountChecker.AccountCheck(UnitOfWork, model.Data.Login, model.Data.Id));
            if (model.Data.Id < 1)
            {
                if (string.IsNullOrWhiteSpace(model.Data.Password) || string.IsNullOrWhiteSpace(model.Data.Salt))
                {
                    errors.Add("Пароль и/или повтор пароля не задан");
                }
                else if (model.Data.Password != model.Data.Salt)
                {
                    errors.Add("Пароль и повтор пароля не совпадают");
                }
                else
                {
                    errors.AddRange(RestChild.Security.AccountExtentions.PasswordSetReposytory.CheckPassword(UnitOfWork, model.Data.Password, 0));
                }
            }

            if (!errors.Any())
            {
                account = WebAccountController.SaveAccount(account);
            }
            else
            {
                if (account.Rights != null && Security.HasRight(AccessRightEnum.AccountManage))
                {
                    var accessRights = GetRights().ToList();
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

            return RedirectToAction("Manage", new { id = account.Id });
        }

        /// <summary>
        /// Изменить пароль аккаунта
        /// </summary>
        public ActionResult ChangePassword(long id, string password, string passwordConfirm)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (id != Security.GetCurrentAccountId() && !Security.HasRight(AccessRightEnum.AccountManage))
            {
                return RedirectToAvaliableAction();
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
                errors.AddRange(RestChild.Security.AccountExtentions.PasswordSetReposytory.AccountSetPassword(UnitOfWork, password, id));
            }

            if (errors != null)
            {
                vm.ErrorMessage = $"<ul>{string.Join("", errors.Select(e => $"<li>{e}</li>").ToList())}</ul>";
                return View("Manage", vm);
            }

            return RedirectToAction("Manage", new { id = id });
        }

        #region Login/Logout

        /// <summary>
        /// Страница авторизации
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Security.GetCurrentAccountId().HasValue)
            {
                return RedirectToLocal(returnUrl);
            }

            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                if (cookie == "__RequestVerificationToken")
                {
                    continue;
                }

                var httpCookie = Response.Cookies[cookie];
                if (httpCookie != null) httpCookie.Expires = DateTime.Now.AddDays(-1);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        /// <summary>
        /// Авторизация
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var accountBlockSpan = SecuritySettings.AccountBlockSpan;
            var maxCountUnsuccess = SecuritySettings.MaxCountUnsuccess;
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (ModelState.IsValid)
            {
                Account account = UnitOfWork.GetSet<Account>().GetAccountByLogin(model.UserName);

                if (account != null)
                {
                    if (!SecurityBasis.AccountIsIB(account) && !SecurityBasis.AccountIsAdmin(account))
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

                        ModelState.AddModelError("", "Недостаточно прав для доступа.");

                        return View(model);
                    }


                    if (accountBlockSpan > 0 && account.CountUnsuccess > 0)
                    {
                        if (DateTime.Now.AddMinutes(-accountBlockSpan) < account.DateLastUnsuccess && maxCountUnsuccess <= account.CountUnsuccess)
                        {
                            ModelState.AddModelError("", $"Превышено количество неуспешных попыток авторизации. Пользователь временно заблокирован. Повтор авторизации через {accountBlockSpan} мин.");
                            return View(model);
                        }
                    }

                    byte[] hash = PasswordUtility.GetPasswordHash(model.Password, Convert.FromBase64String(account.Salt));

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

                ModelState.AddModelError("", account != null && accountBlockSpan > 0 && account.CountUnsuccess >= maxCountUnsuccess ? $"Превышено количество неуспешных попыток авторизации. Пользователь временно заблокирован. Повтор авторизации через {accountBlockSpan} мин." : "Неверный пользователь или пароль.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            var _sessionUid = HttpContext.User.GetClaimValue(ClaimTypes.UserData);
            AuthenticationManager.SignOut();
            UserWatcher.UserSessionExitMark(UnitOfWork, _sessionUid);
            return RedirectToAvaliableAction();
        }

        #endregion

        #region Helpers

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void SignIn(Account user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Name) },
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

            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);

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

            return RedirectToAvaliableAction();
        }

        private IEnumerable<AccessRight> GetRights()
        {
            return UnitOfWork.GetSet<AccessRight>().ToList();
        }

        #endregion

        #region Force Change Password

        /// <summary>
        /// Смена пароля при входе 
        /// </summary>
        [HttpGet]
        public ActionResult ForceChangePassword(string returnUrl, bool? first = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var _sessionUid = HttpContext.User.GetClaimValue(ClaimTypes.UserData);
            if (!UserWatcher.UserForceChangePasswordNeed(UnitOfWork, _sessionUid))
            {
                RedirectToAvaliableAction();
            }

            ForceChangePasswordModelBase model = new ForceChangePasswordModelBase()
            {
                FirstTimeAuth = first.Value,
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        /// <summary>
        /// Сохранение смены пароля при входе
        /// </summary>
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

            var _passErrors = RestChild.Security.AccountExtentions.PasswordSetReposytory.AccountSetPassword(UnitOfWork, model.NewPassword, Security.GetCurrentAccountId().Value);
            if (_passErrors != null && _passErrors.Count > 0)
            {
                ModelState.AddModelError(string.Empty, string.Join(", ", _passErrors));
                model.NewPassword = model.NewPasswordRpt = string.Empty;
                return View(model);
            }

            UserWatcher.UserDataDrop(HttpContext.User.GetClaimValue(ClaimTypes.UserData));

            if (string.IsNullOrWhiteSpace(model.ReturnUrl))
            {
                return RedirectToAvaliableAction();
            }

            return Redirect(model.ReturnUrl);
        }

        /// <summary>
        /// Установить признак принудительного смены пароля аккаунта при его следующем входе
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForceChangePasswordSet(long? accountId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.AccountManage))
            {
                return RedirectToAvaliableAction();
            }

            var account = WebAccountController.GetAccount(accountId ?? 0);
            account.IsTemporyPassword = true;
            account = WebAccountController.SaveAccount(account);

            return RedirectToAction("Manage", new { @id = accountId.Value });
        }

        #endregion
    }
}
