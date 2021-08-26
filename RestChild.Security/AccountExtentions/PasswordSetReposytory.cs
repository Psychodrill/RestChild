using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestChild.Security.AccountExtentions
{
    /// <summary>
    /// Установщик паролей
    /// </summary>
    public static class PasswordSetReposytory
    {
        /// <summary>
        /// Проверка пароля
        /// </summary>
        public static ICollection<string> CheckPassword(IUnitOfWork UnitOfWork, string NewPassword, long AccountId)
        {
            var errors = new List<string>();

            if (!CheckPasswordStrength(NewPassword))
            {
                errors.Add(string.Format("Пароль не удовлетворяет требованиям безопасности. Пароль должен быть {0} и более символов, должен содержать прописные и строчные латинские буквы, а также цифры или специальные символы", SecuritySettings.MinLenPassword));
            }
            if (AccountId > 0 && CheckPasswordPreviousMatch(UnitOfWork, AccountId, NewPassword))
            {
                errors.Add("Пароль не может быть идентичен предыдущему");
            }

            return errors;
        }

        /// <summary>
        /// Установка пароля
        /// </summary>
        public static ICollection<string> AccountSetPassword(IUnitOfWork UnitOfWork, string NewPassword, long AccountId)
        {
            var errors = new List<string>();
            errors.AddRange(CheckPassword(UnitOfWork, NewPassword, AccountId));

            if (errors.Count() < 1)
            {
                ChangePassword(UnitOfWork, AccountId, NewPassword);
            }

            return errors;
        }

        private static bool CheckPasswordPreviousMatch(IUnitOfWork UnitOfWork, long id, string password)
        {
            var account = UnitOfWork.GetById<Account>(id);
            var pwdHash = PasswordUtility.GetPasswordHash(password, Convert.FromBase64String(account.Salt));
            return pwdHash.SequenceEqual(Convert.FromBase64String(account.Password));
        }

        private static void ChangePassword(IUnitOfWork UnitOfWork, long id, string password)
        {
            var user = UnitOfWork.GetById<Account>(id);
            if (user == null || string.IsNullOrEmpty(password)) return;
            var salt = PasswordUtility.GenerateSalt();
            user.Password = Convert.ToBase64String(PasswordUtility.GetPasswordHash(password, salt));
            user.Salt = Convert.ToBase64String(salt);

            user.LastUpdateTick = DateTime.Now.Ticks;
            user.DateUpdate = DateTime.Now;
            user.CreateUserId = SecurityBasis.GetCurrentAccountId() ?? user.CreateUserId;
            user.DateLastChangePassword = DateTime.Now;
            user.IsTemporyPassword = false;

            var cms = UnitOfWork.GetSet<Counselors>().Where(c => c.LinkedAccountId == user.Id).ToList();
            foreach (var c in cms)
            {
                c.Password = user.Password;
                c.Salt = user.Salt;
            }

            var adms = UnitOfWork.GetSet<AdministratorTour>().Where(c => c.LinkedAccountId == user.Id).ToList();
            foreach (var c in adms)
            {
                c.Password = user.Password;
                c.Salt = user.Salt;
            }

            Logger.SecurityLogger.AddToLogUserPasswordChange(UnitOfWork, id, SecurityBasis.GetCurrentAccountId().Value);

            UnitOfWork.SaveChanges();
        }

        private static bool CheckPasswordStrength(string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
                return false;

            if (Password.Length < SecuritySettings.MinLenPassword)
                return false;


            var _r1 = Regex.Match(Password, "[0-9]", RegexOptions.ECMAScript).Success;
            var _r2 = Regex.Match(Password, "[!,@,#,$,%,^,&,*,?,_,~,-,(,),.]", RegexOptions.ECMAScript).Success;

            if (!_r1 && !_r2)
                return false;


            var _r3 = Regex.Match(Password, @"[a-z]", RegexOptions.ECMAScript).Success;
            var _r4 = Regex.Match(Password, @"[A-Z]", RegexOptions.ECMAScript).Success;

            if (!_r3 || !_r4)
                return false;

            return true;
        }
    }
}
