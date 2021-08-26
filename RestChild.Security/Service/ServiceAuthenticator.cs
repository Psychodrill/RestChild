using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Security.Service
{
    /// <summary>
    /// Класс валидации пользователя
    /// </summary>
    public abstract class ServiceAuthenticator : System.IdentityModel.Selectors.UserNamePasswordValidator
    {
        protected abstract string GetUserName();

        protected abstract string GetPassword();

        /// <summary>
        /// Валидировать
        /// </summary>
        /// <param name="userName">Логин</param>
        /// <param name="password">Пароль</param>
        public override sealed void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            if (!string.Equals(userName, GetUserName(), StringComparison.Ordinal) || !string.Equals(password, GetPassword(), StringComparison.Ordinal))
                throw new System.IdentityModel.Tokens.SecurityTokenException("Unknown username or password");

        }
    }
}
