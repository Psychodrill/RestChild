using System;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Security.Logger
{
    /// <summary>
    /// Логировщик событий безопасности
    /// </summary>
    public static class SecurityLogger
    {
        private const string userPassChange1 = "Пользователь изменил пароль";
        private const string userPassChange2 = "Пользователь {0} ({1}) сменил(а) пароль";
        private const string userRightName = "Права пользователя изменены";
        private const string userRightSet = "Пользователю {0} ({1}) назначено право \"{2}\"";
        private const string userRightDelete = "С пользователя {0} ({1}) снято право \"{2}\"";
        private const string userRoleName = "Роли пользователя изменены";
        private const string userRoleSet = "Пользователю {0} ({1}) назначена роль \"{2}\"";
        private const string userRoleDelete = "С пользователя {0} ({1}) снята роль \"{2}\"";
        private const string userDataChange1 = "Данные пользователя были изменены";
        private const string userDataChange2 = "Данные ({2}) пользователя {0} ({1}) были изменены";
        private const string userProcess = "Запущен процесс/формирование отчета";
        private const string userProcessData = "Пользователь {0} ({1}) запустил процесс/отчет \"{2}\"";

        /// <summary>
        /// добавить в лог
        /// </summary>
        public static void AddToLog(SecurityJournalEventType EventType, string EventName, string EventDescription, long? AuthorId = null, string Browser = null)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLog(uw, EventType, EventName, EventDescription, AuthorId, Browser);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог
        /// </summary>
        public static void AddToLog(IUnitOfWork UnitOfWork, SecurityJournalEventType EventType, string EventName, string EventDescription, long? AuthorId = null, string Browser = null)
        {
            var user = UnitOfWork.GetById<Account>(AuthorId);

            UnitOfWork.AddEntity(new SecurityJournal
            {
                DateEvent = DateTime.Now,
                SecurityJournalTypeId = (long)EventType,
                EventName = EventName,
                Description = EventDescription,
                UserName = user == null ? string.Empty : $"{user.Name} ({user.Id})",
                Brouser = Browser
            });
        }

        /// <summary>
        /// добавить в лог
        /// </summary>
        public static void AddToLog(SecurityJournalEventType EventType, string EventName, string EventDescription, string Author, string Browser = null)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLog(uw, EventType, EventName, EventDescription, Author, Browser);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог
        /// </summary>
        public static void AddToLog(IUnitOfWork UnitOfWork, SecurityJournalEventType EventType, string EventName, string EventDescription, string Author, string Browser = null)
        {
            UnitOfWork.AddEntity(new SecurityJournal
            {
                DateEvent = DateTime.Now,
                SecurityJournalTypeId = (long)EventType,
                EventName = EventName,
                Description = EventDescription,
                UserName = Author,
                Brouser = Browser
            });
        }

        /// <summary>
        /// добавить в лог (изменение пароля)
        /// </summary>
        public static void AddToLogUserPasswordChange(long User, long AuthorId)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLogUserPasswordChange(uw, User, AuthorId);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог (изменение пароля)
        /// </summary>
        public static void AddToLogUserPasswordChange(IUnitOfWork UnitOfWork, long UserId, long AuthorId)
        {
            var user = UnitOfWork.GetById<Account>(UserId);

            var descr = string.Format(userPassChange2, user.Name, user.Id);

            AddToLog(UnitOfWork, SecurityJournalEventType.UserDataChange, userPassChange1, descr, AuthorId);

            AddEmailEvent(UnitOfWork, SecurityJournalEventType.UserDataChange, userDataChange1, descr, AuthorId);
        }

        /// <summary>
        /// добавить в лог (изменение настроек безопасности пользователя)
        /// </summary>
        public static void AddToLogUserDataChange(long User, long AuthorId, params string[] Fields)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLogUserDataChange(uw, User, AuthorId, Fields);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог (изменение настроек безопасности пользователя)
        /// </summary>
        public static void AddToLogUserDataChange(IUnitOfWork UnitOfWork, long UserId, long AuthorId, params string[] Fields)
        {
            if (Fields.Length < 1)
            {
                return;
            }

            var user = UnitOfWork.GetById<Account>(UserId);

            var desct = string.Format(userDataChange2, user.Name, user.Id, string.Join(", ", Fields));

            AddToLog(UnitOfWork, SecurityJournalEventType.UserDataChange, userDataChange1, desct, AuthorId);

            AddEmailEvent(UnitOfWork, SecurityJournalEventType.UserDataChange, userDataChange1, desct, AuthorId);
        }

        /// <summary>
        /// добавить в лог (изменение прав пользователя)
        /// </summary>
        public static void AddToLogUserRightChange(long RightId, bool Set, long UserId, long AuthorId)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLogUserRightChange(uw, RightId, Set, UserId, AuthorId);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог (изменение прав пользователя)
        /// </summary>
        public static void AddToLogUserRightChange(IUnitOfWork UnitOfWork, long RightId, bool Set, long UserId, long AuthorId)
        {
            var right = UnitOfWork.GetById<AccessRight>(RightId);
            var user = UnitOfWork.GetById<Account>(UserId);
            AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, userRightName,
                Set
                    ? string.Format(userRightSet, user.Name, user.Id, right.Name)
                    : string.Format(userRightDelete, user.Name, user.Id, right.Name), AuthorId);
        }

        /// <summary>
        /// добавить в лог (изменение роли пользователя)
        /// </summary>
        public static void AddToLogUserRoleChange(long RoleId, bool Set, long UserId, long AuthorId)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLogUserRoleChange(uw, RoleId, Set, UserId, AuthorId);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог (изменение роли пользователя)
        /// </summary>
        public static void AddToLogUserRoleChange(IUnitOfWork UnitOfWork, long RoleId, bool Set, long UserId, long AuthorId)
        {
            var role = UnitOfWork.GetById<Role>(RoleId);
            var user = UnitOfWork.GetById<Account>(UserId);
            AddToLog(UnitOfWork, SecurityJournalEventType.RightsAndRoles, userRightName,
                Set
                    ? string.Format(userRoleSet, user.Name, user.Id, role.Name)
                    : string.Format(userRoleDelete, user.Name, user.Id, role.Name), AuthorId);
        }

        /// <summary>
        /// добавить в лог (запуск процесса)
        /// </summary>
        public static void AddToLogProcess(string ProcessName, long AuthorId, string Browser = null)
        {
            var uw = new UnitOfWork();
            try
            {
                AddToLogProcess(uw, ProcessName, AuthorId, Browser);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог (запуск процесса)
        /// </summary>
        public static void AddToLogProcess(IUnitOfWork UnitOfWork, string ProcessName, long AuthorId, string Browser = null)
        {
            var user = UnitOfWork.GetById<Account>(AuthorId);
            AddToLog(UnitOfWork, SecurityJournalEventType.Processes, userProcess,
                string.Format(userProcessData, user.Name, user.Id, ProcessName), AuthorId, Browser);
        }

        /// <summary>
        /// добавить в лог (запуск процесса)
        /// </summary>
        public static void AddToLogProcess(IUnitOfWork UnitOfWork, string ProcessName, string ProcessDescription, long AuthorId, string Browser = null)
        {
            var user = UnitOfWork.GetById<Account>(AuthorId);
            AddToLog(UnitOfWork, SecurityJournalEventType.Processes, userProcess,
                string.Format(ProcessDescription, user.Name, user.Id, ProcessName), AuthorId, Browser);
        }

        /// <summary>
        /// добавить в лог (запуск процесса)
        /// </summary>
        public static void AddToLogNamedProcess(IUnitOfWork UnitOfWork, string ProcessName, string ProcessDescription, long AuthorId, string Browser = null)
        {
            var user = UnitOfWork.GetById<Account>(AuthorId);
            AddToLog(UnitOfWork, SecurityJournalEventType.Processes, ProcessName,
                string.Format(ProcessDescription, user.Name, user.Id, ProcessName), AuthorId, Browser);
        }

        /// <summary>
        /// добавить в лог (событие)
        /// </summary>
        public static void AddEmailEvent(SecurityJournalEventType EventType, string EventName, string EventDescription,
            long AuthorId, string Browser = null)
        {
            var uw = new UnitOfWork();
            try
            {
                AddEmailEvent(uw, EventType, EventName, EventDescription, AuthorId, Browser);
            }
            finally
            {
                uw.Dispose();
            }
        }

        /// <summary>
        /// добавить в лог (событие)
        /// </summary>
        public static void AddEmailEvent(IUnitOfWork UnitOfWork, SecurityJournalEventType EventType, string EventName,
            string EventDescription, long AuthorId, string Browser = null)
        {
            var admins = UnitOfWork.GetSet<AccountRights>()
                .Where(ss =>
                    ss.AccountId != AuthorId && ss.AccessRight.Code == AccessRightEnum.Security.Login &&
                    !ss.Account.IsDeleted && ss.Account.IsActive).Select(ss => ss.Account).ToList();

            foreach (var admin in admins.Where(ss =>
                ss.Login.Contains("@") || !string.IsNullOrWhiteSpace(ss.Email) && ss.Email.Contains("@")))
            {
                UnitOfWork.AddEntity(new SendEmailAndSms
                {
                    IsSmsSended = true,
                    SmsMessage = null,
                    Phone = null,
                    EmailTitle = "Оповещение информационной безопасности в системе «Аис «Отдых»",
                    Email = string.IsNullOrWhiteSpace(admin.Email) ? admin.Login : admin.Email,
                    EmailMessage = $"{EventName}<br/><br/><br/>{EventDescription}",
                    DateCreate = DateTime.Now,
                    IsEmailSended = false
                });
            }
        }
    }
}
