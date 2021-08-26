using System;
using System.Linq;
using System.Runtime.Caching;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Security.Logger;

namespace RestChild.Security
{
    /// <summary>
    /// Наблюдатель за аккаунтами
    /// </summary>
    public static class UserWatcher
    {
        internal const int SessionActiviteSpan = 6;
        private const string cacheName = "__aa_b_ib__SecurituSessionUserData({0})";
        private static readonly TimeSpan cacheLiveTime = new TimeSpan(0, SessionActiviteSpan, 0);
        private static readonly MemoryCache memoryCache = MemoryCache.Default;
        //SecuritySettings.SessionTimeout; // minutes
        //SecuritySettings.TimeLifePassword; //days
        //SecuritySettings.TimeLifeAccount; //days

        /// <summary>
        /// генереация имени для cache
        /// </summary>
        private static string GetCacheName(string sessionUid)
        {
            return string.Format(cacheName, sessionUid.ToLower());
        }

        /// <summary>
        /// достать из БД данные сессии пользователя
        /// </summary>
        internal static UserData UsersDataGet(string sessionUid)
        {
            //SecuritySettings.SessionTimeout = 20;
            //SecuritySettings.TimeLifePassword = 60;
            var value = memoryCache.Get(GetCacheName(sessionUid));
            if (value == null)
            {
                var preValue = GetUsersFomDb(sessionUid);
                if (preValue != null)
                {
                    value = preValue;
                    memoryCache.Set(GetCacheName(sessionUid), value, DateTimeOffset.Now.Add(cacheLiveTime));
                }
            }

            return value is UserData variable ? variable : null;
        }

        /// <summary>
        /// достать из БД данные сессии пользователя
        /// </summary>
        private static UserData GetUsersFomDb(string sessionUid)
        {
            var unit = WindsorHolder.Resolve<IUnitOfWork>();
            try
            {
                var result = unit.GetSet<AccountHistoryLogin>().Where(ss => ss.SessionUid == sessionUid).Select(sx =>
                   new UserData
                   {
                       lastActivity = sx.DateLastActivity,
                       dateExit = sx.DateExit,
                       stopSession = sx.StopSession,
                       passwordLastUpdate = sx.Account.DateLastChangePassword,
                       isTemporyPassword = sx.Account.IsTemporyPassword
                   }).FirstOrDefault();

                var some2 = DateTime.Now.AddHours(-2);
                var exits = unit.GetSet<AccountHistoryLogin>()
                   .Where(ss => ss.IsAuthorized && ss.DateExit == null && ss.DateLastActivity < some2).ToList();

                exits.ForEach(ss =>
                {
                    ss.DateExit = ss.DateLastActivity.AddMinutes(SecuritySettings.SessionTimeout);
                });

                unit.SaveChanges();

                //механизм блокировки учетной записи в случае ее неиспользования в течении определенного времени (90 суток)
                some2 = DateTime.Now.AddDays(SecuritySettings.SessionTimeout * -1);
                var users = unit.GetSet<Account>().GroupJoin(unit.GetSet<AccountHistoryLogin>(), user => user.Id,
                   history => history.AccountId,
                   (user, history) => new { Account = user, AccountHistoryLogins = history }).Where(ss =>
                     !ss.Account.IsDeleted
                     && ss.Account.IsActive
                     && (!ss.AccountHistoryLogins.Any() && ss.Account.DateCreate < some2 || ss.AccountHistoryLogins.Max(sx => sx.DateEnter) < some2)
                    ).Select(ss => ss.Account).ToList();

                foreach (var user in users)
                {
                    user.IsActive = false;
                    SecurityLogger.AddToLog(SecurityJournalEventType.UserDataChange,
                       $"Пользователь {user.Login} ({user.Id}) заблокирован",
                       $"Пользователь {user.Login} ({user.Id}) заблокирован по причине длительного (более {SecuritySettings.TimeLifeAccount} дней) неиспользования своего аккаунта");
                }

                unit.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                WindsorHolder.Release(unit);
            }
        }

        /// <summary>
        /// обновить данные пользовательской сессиии в БД
        /// </summary>
        internal static void UserUpdate(string sessionUid)
        {
            var unit = WindsorHolder.Resolve<IUnitOfWork>();
            try
            {
                memoryCache.Remove(GetCacheName(sessionUid));

                var exits = unit.GetSet<AccountHistoryLogin>().Where(ss => ss.SessionUid == sessionUid).ToList();
                exits.ForEach(ss => { ss.DateLastActivity = DateTime.Now; });
                unit.SaveChanges();
            }
            catch { }
            finally
            {
                WindsorHolder.Release(unit);
            }
        }

        /// <summary>
        /// метод установки времени выхода пользователя из системы
        /// </summary>
        internal static void UserExitSet(string sessionUid)
        {
            if (string.IsNullOrWhiteSpace(sessionUid))
            {
                return;
            }

            var unit = WindsorHolder.Resolve<IUnitOfWork>();
            try
            {
                UserExitSet(unit, sessionUid);
            }
            catch
            {
            }
            finally
            {
                WindsorHolder.Release(unit);
            }
        }

        /// <summary>
        /// метод установки времени выхода пользователя из системы
        /// </summary>
        private static void UserExitSet(IUnitOfWork unit, string SessionUid)
        {
            var exits = unit.GetSet<AccountHistoryLogin>().Where(ss => ss.SessionUid == SessionUid).ToList();
            exits.ForEach(ss => { ss.DateExit = DateTime.Now; });
            unit.SaveChanges();
        }

        /// <summary>
        /// метод установки признака завершения сессии после выхода пользователя из сиcтемы
        /// </summary>
        public static void UserSessionExitMark(IUnitOfWork unit, string sessionUid)
        {
            UserDataDrop(GetCacheName(sessionUid));
            UserExitSet(unit, sessionUid);
        }

        /// <summary>
        /// метод принудительно выхода пользователя из системы
        /// </summary>
        public static void UserSessionStop(IUnitOfWork unit, string sessionUid)
        {
            UserDataDrop(GetCacheName(sessionUid));
            var exits = unit.GetSet<AccountHistoryLogin>().Where(ss => ss.SessionUid == sessionUid).ToList();
            exits.ForEach(ss => { ss.StopSession = true; });
            unit.SaveChanges();
        }

        /// <summary>
        /// метод проверки признака необходимости сброса пароля
        /// </summary>
        public static bool UserForceChangePasswordNeed(IUnitOfWork unit, string sessionUid)
        {
            var _ud = UsersDataGet(sessionUid);
            return _ud != null && _ud.forceChangePassword;
        }

        /// <summary>
        /// обнулить информацию о пользовательской сессии
        /// </summary>
        /// <param name="SessionUid"></param>
        public static void UserDataDrop(string SessionUid)
        {
            memoryCache.Remove(GetCacheName(SessionUid));
        }

        internal sealed class UserData
        {
            /// <summary>
            /// признак необходимости принудительного выхода из системы
            /// </summary>
            public bool kickOff
            {
                get
                {
                    var some = DateTime.Now.AddMinutes(SecuritySettings.SessionTimeout * -1);
                    return stopSession || lastActivity <= some || dateExit.HasValue;
                }
            }

            /// <summary>
            /// признак необходимости обновить дату активности
            /// </summary>
            public bool updateActivity
            {
                get
                {
                    var spanMinutes = SecuritySettings.SessionTimeout - 2;
                    if (spanMinutes > 15)
                        spanMinutes = 15;

                    var some = DateTime.Now.AddMinutes((spanMinutes * -1));
                    return lastActivity <= some;
                }
            }

            /// <summary>
            /// дата последней активности пользователя
            /// </summary>
            public DateTime lastActivity { get; set; }

            /// <summary>
            /// дата завершения сессии
            /// </summary>
            public DateTime? dateExit { get; set; }

            /// <summary>
            /// признак необходимости завершить сессию
            /// </summary>
            public bool stopSession { get; set; }

            /// <summary>
            /// дата последнего обновления пароля
            /// </summary>
            public DateTime? passwordLastUpdate { get; set; }

            /// <summary>
            /// признак принудительного сброса пароля
            /// </summary>
            public bool forceChangePassword
            {
                get
                {
                    var _some = DateTime.Now.AddDays(SecuritySettings.TimeLifePassword * -1);
                    return isTemporyPassword || !passwordLastUpdate.HasValue || passwordLastUpdate.Value <= _some;
                }
            }

            /// <summary>
            /// признак первого входа в ситему
            /// </summary>
            public bool firstTimeLogin => !passwordLastUpdate.HasValue;

            /// <summary>
            /// признак временного пароля
            /// </summary>
            public bool isTemporyPassword { get; set; }
        }
    }
}
