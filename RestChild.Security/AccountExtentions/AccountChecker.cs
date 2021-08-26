using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Security.AccountExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Security.AccountExtentions
{
    /// <summary>
    /// Проверка аккаунта
    /// </summary>
    public static class AccountChecker
    {
        private static ICollection<string> LoginCheck(IUnitOfWork UnitOfWork, string Login, long Id)
        {
            var errors = new List<string>();
            var _some = DateTime.Now.AddYears(-1);
            //((a.IsDeleted && a.DateDelete == null) || a.DateDelete >= _some)
            if (UnitOfWork.GetSet<Account>().Any(a => a.Id != Id && a.Login.ToLower() == Login.ToLower() && (!a.IsDeleted || (a.IsDeleted && (a.DateDelete != null || a.DateDelete >= _some)))))
            {
                errors.Add("Логин не может быть использован");
            }
            return errors;
        }

        /// <summary>
        /// проверка логина на уникальность
        /// </summary>
        public static ICollection<string> AccountCheck(IUnitOfWork UnitOfWork, string Login, long Id)
        {
            var errors = new List<string>();
            errors.AddRange(LoginCheck(UnitOfWork, Login, Id));
            return errors;
        }
    }
}
