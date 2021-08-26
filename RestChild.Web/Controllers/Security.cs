using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Security;

namespace RestChild.Web.Controllers
{
    public static class Security
    {
        public const string SecurityOrgTemplate = "{0}-{1}";

        /// <summary>
        ///     получить список организаций по праву
        /// </summary>
        public static long?[] GetSecurityOrganiztion(this string security)
        {
            return GetSecurity().Where(s => s.StartsWith(security)).Select(
               s =>
                  s.Replace(
                     string.Format(SecurityOrgTemplate, security, string.Empty),
                     string.Empty).LongParse()).Where(l => l.HasValue).ToArray();
        }

        /// <summary>
        ///     получить текущего пользоваителя
        /// </summary>
        /// <returns></returns>
        public static long? GetCurrentAccountId()
        {
            return SecurityBasis.GetCurrentAccountId();
        }

        /// <summary>
        ///     получить список организаций по праву
        /// </summary>
        public static long?[] GetSecurityOrganization(this string security)
        {
            return SecurityBasis.GetSecurityOrganization(security);
        }

        /// <summary>
        ///     закешировать безопасность
        /// </summary>
        public static void SaveSecurity(long accountId, ClaimsIdentity identity, IList<string> security, string key)
        {
            SecurityBasis.SaveSecurity(accountId, identity, security, key);
        }

        /// <summary>
        ///     получить текущего пользоваителя
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetSecurity(long? id = null)
        {
            return SecurityBasis.GetSecurity(id);
        }

        /// <summary>
        ///     наличие права
        /// </summary>
        public static bool HasRight(string code)
        {
            return SecurityBasis.HasRight(code);
        }

        /// <summary>
        ///     наличие права
        /// </summary>
        public static bool HasAnyRights(string[] codes)
        {
            return SecurityBasis.HasAnyRights(codes);
        }

        /// <summary>
        ///     наличие права
        /// </summary>
        public static bool HasAnyRightsForSomeOrganization(params string[] codes)
        {
            return SecurityBasis.HasAnyRightsForSomeOrganization(codes);
        }

        /// <summary>
        ///     наличие права хоть для одной организации
        /// </summary>
        public static bool HasRightForSomeOrganization(string code)
        {
            return SecurityBasis.HasRightForSomeOrganization(code);
        }

        /// <summary>
        ///     наличие права для органиации
        /// </summary>
        public static bool HasRight(string code, long? organizationId)
        {
            return SecurityBasis.HasRight(code, organizationId);
        }

        /// <summary>
        ///     получить текущего пользоваителя
        /// </summary>
        public static Account GetCurrentAccount()
        {
            var uw = WindsorHolder.Resolve<IUnitOfWork>();
            try
            {
                return uw.GetById<Account>(GetCurrentAccountId() ?? 0);
            }
            finally
            {
                WindsorHolder.Release(uw);
            }
        }

        /// <summary>
        ///     аккаунт для ИБ
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool AccountIsIB(Account account)
        {
            return SecurityBasis.AccountIsIB(account);
        }
    }
}
