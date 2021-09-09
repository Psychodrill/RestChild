using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Transactions;
using System.Web;
using Microsoft.Owin.Security;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Security.Cache;

namespace RestChild.Security
{
    /// <summary>
    ///     Базис безопасности
    /// </summary>
    public static class SecurityBasis
    {
        /// <summary>
        ///     Шаблон наименования события безопасности
        /// </summary>
        public const string SecurityOrgTemplate = "{0}-{1}";

        private static readonly ICacheService MemoryCache = new InMemoryCache();

        /// <summary>
        ///     получить значение
        /// </summary>
        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            var claim = identity?.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        /// <summary>
        ///     обновление
        /// </summary>
        public static void AddUpdateClaim(this IPrincipal currentPrincipal, string key, string value)
        {
            var identity = currentPrincipal?.Identity as ClaimsIdentity;
            if (identity == null)
                return;

            // check for existing claim and remove it
            var existingClaim = identity.FindFirst(key);
            if (existingClaim != null)
                identity.RemoveClaim(existingClaim);

            if (!string.IsNullOrWhiteSpace(value))
            {
                // add new claim
                identity.AddClaim(new Claim(key, value));
            }

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant =
               new AuthenticationResponseGrant(new ClaimsPrincipal(identity),
                  new AuthenticationProperties { IsPersistent = true });
        }

        /// <summary>
        ///     получить текущего пользователя
        /// </summary>
        public static long? GetCurrentAccountId()
        {
            return
               ((ClaimsIdentity)HttpContext.Current?.User?.Identity)?.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value.LongParse();
        }

        private static IAuthenticationManager AuthenticationManager => HttpContext.Current.GetOwinContext().Authentication;

        /// <summary>
        /// получить список организаций по праву
        /// </summary>
        public static long?[] GetSecurityOrganization(this string security)
        {
            return GetSecurity().Where(s => s.StartsWith(security)).Select(
               s =>
                  s.Replace(
                     string.Format(SecurityOrgTemplate, security, string.Empty),
                     string.Empty).LongParse()).Where(l => l.HasValue).ToArray();
        }

        /// <summary>
        ///     получить текущего пользователя
        /// </summary>
        public static IList<string> GetSecurity(long? id = null)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            var res = identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;

            var savedKey = res;

            var claimPresent = !string.IsNullOrEmpty(res);

            var dateExpirationString = identity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Expiration)?.Value;

            if (!string.IsNullOrEmpty(dateExpirationString))
            {
                var dateExpiration = DateTime.Parse(dateExpirationString, null, DateTimeStyles.RoundtripKind);
                if (dateExpiration < DateTime.Now)
                {
#if !DEBUG
                    res = null;
#endif
                    identity.RemoveClaim(identity.FindFirst(ClaimTypes.Expiration));
                }
            }
#if !DEBUG
            else
            {
                res = null;
            }
#endif

#if !DEBUG
            if (!string.IsNullOrWhiteSpace(res))
            {
                var sec = MemoryCache.Get<string[]>(res);
                if (sec != null && sec.Any())
                {
                    return sec;
                }
            }
#endif

            id = id ?? GetCurrentAccountId() ?? 0;

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var uw = WindsorHolder.Resolve<IUnitOfWork>();
                try
                {
                    var user = uw.GetById<Account>(id);
                    if (user == null || !user.IsActive)
                    {
                        AuthenticationManager.SignOut();
                        return new string[0];
                    }

                    var rights = uw.GetSet<AccountRights>().Where(a => a.AccountId == id).Include(a => a.AccessRight)
                       .ToArray();

                    var result =
                       rights.Select(
                          s =>
                             s.OrganizationId.HasValue && s.AccessRight.ForOrganization
                                ? string.Format(SecurityOrgTemplate, s.AccessRight.Code, s.Organization.Id)
                                : s.AccessRight.Code).ToList();

                    result.AddRange(user.Roles.SelectMany(r => r.Role.AccessRights.Select(s =>
                       r.OrganizationId.HasValue && s.ForOrganization
                          ? string.Format(SecurityOrgTemplate, s.Code, r.OrganizationId)
                          : s.Code)));

                    identity?.AddClaim(new Claim(ClaimTypes.Expiration, DateTime.Now.AddHours(1).ToString("o")));
                    if (claimPresent)
                    {
                        identity.RemoveClaim(identity.FindFirst(ClaimTypes.UserData));
                    }

                    result = result.Distinct().ToList();

                    SaveSecurity(id.Value, identity, result, savedKey);

                    return result.Distinct().ToList();
                }
                finally
                {
                    WindsorHolder.Release(uw);
                }
            }
        }

        /// <summary>
        ///     закэшировать безопасность
        /// </summary>
        public static void SaveSecurity(long accountId, ClaimsIdentity identity, IList<string> security, string key)
        {
            if (identity == null || accountId == 0)
            {
                return;
            }

            key = string.IsNullOrWhiteSpace(key) ? $"{Guid.NewGuid().ToString()}::{accountId}" : key;
            MemoryCache.Set(key, security.ToArray());
            identity.AddClaim(new Claim(ClaimTypes.UserData, key));
        }

        /// <summary>
        ///     наличие права
        /// </summary>
        public static bool HasRight(string code)
        {
            return GetSecurity().Contains(code);
        }

        /// <summary>
        ///     наличие права
        /// </summary>
        public static bool HasAnyRights(string[] codes)
        {
            var security = GetSecurity();
            return codes.Any(code => security.Contains(code));
        }

        /// <summary>
        ///     наличие права
        /// </summary>
        public static bool HasAnyRightsForSomeOrganization(string[] codes)
        {
            var security = GetSecurity();
            return codes.Any(code => security.Any(s => s.StartsWith(code)));
        }

        /// <summary>
        ///     наличие права хоть для одной организации
        /// </summary>
        public static bool HasRightForSomeOrganization(string code)
        {
            return GetSecurity().Any(s => s.StartsWith(code));
        }

        /// <summary>
        ///     наличие права для организации
        /// </summary>
        public static bool HasRight(string code, long? organizationId)
        {
            var sec = GetSecurity();
            return sec.Contains(code) || sec.Contains(string.Format(SecurityOrgTemplate, code, organizationId));
        }

        /// <summary>
        ///     получить имя текущего пользоваителя
        /// </summary>
        public static string GetCurrentAccountName()
        {
            return GetCurrentAccount()?.Name;
        }

        /// <summary>
        ///     получить текущего пользоваителя
        /// </summary>
        public static Account GetCurrentAccount()
        {
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var uw = WindsorHolder.Resolve<IUnitOfWork>();
                try
                {
                    var account = uw.GetById<Account>(GetCurrentAccountId() ?? 0);
                    if (account == null)
                    {
                        return null;
                    }

                    return new Account(account);
                }
                finally
                {
                    WindsorHolder.Release(uw);
                }
            }
        }

        /// <summary>
        ///     Проверяем евляется ли пользователь администратором ИБ
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool AccountIsIB(Account account)
        {
            string[] _ibRights = new string[] {
            AccessRightEnum.Security.Login,
            AccessRightEnum.Security.JournalEntrance,
            AccessRightEnum.Security.JournalProceses,
            AccessRightEnum.Security.JournalRoles,
            AccessRightEnum.Security.JournalSecurity,
            AccessRightEnum.Security.JournalSessions,
            AccessRightEnum.Security.SecuritySettingsEdit,
            AccessRightEnum.Security.SecuritySettingsView,
            AccessRightEnum.Security.StopSessions
         };

            var rr = GetSecurity(account.Id);

            return rr.Any(ss => _ibRights.Contains(ss));
        }

        /// <summary>
        ///     Проверяем евляется ли пользователь администратором системы
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool AccountIsAdmin(Account account)
        {
            string[] _ibRights = new string[] { AccessRightEnum.AccountManage };

            var rr = GetSecurity(account.Id);

            return rr.Any(ss => _ibRights.Contains(ss));
        }
    }
}
