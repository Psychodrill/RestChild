using System.Collections.Generic;
using System.Security.Claims;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Security;

namespace RestChild.Admin.Web.Controllers
{
   public static class Security
   {
      //public const string SecurityOrgTemplate = "{0}-{1}";

      /// <summary>
      ///     получить текущего пользоваителя
      /// </summary>
      /// <returns></returns>
      public static long? GetCurrentAccountId()
      {
         return SecurityBasis.GetCurrentAccountId();
      }

      /// <summary>
      /// получить список организаций по праву
      /// </summary>
      public static long?[] GetSecurityOrganization(this string security)
      {
         return SecurityBasis.GetSecurityOrganization(security);
      }

      /// <summary>
      /// закешировать безопасность
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
      /// наличие права
      /// </summary>
      public static bool HasRight(string code)
      {
         return SecurityBasis.HasRight(code);
      }

      /// <summary>
      /// наличие права
      /// </summary>
      public static bool HasAnyRights(string[] codes)
      {
         return SecurityBasis.HasAnyRights(codes);
      }

      /// <summary>
      /// наличие права
      /// </summary>
      public static bool HasAnyRightsForSomeOrganization(string[] codes)
      {
         return SecurityBasis.HasAnyRightsForSomeOrganization(codes);
      }

      /// <summary>
      /// наличие права хоть для одной организации
      /// </summary>
      public static bool HasRightForSomeOrganization(string code)
      {
         return SecurityBasis.HasRightForSomeOrganization(code);
      }
   }
}
