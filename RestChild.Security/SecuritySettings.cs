using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Enumeration;

namespace RestChild.Security
{
    /// <summary>
    /// получить настройки безопасности
    /// </summary>
    public static class SecuritySettings
    {
        /// <summary>
        /// время жизни кэша
        /// </summary>
        public static readonly TimeSpan TimeToLive = new TimeSpan(0, 5, 0);
        private static readonly MemoryCache memoryCache = MemoryCache.Default;

        private static IDictionary<long, string> SecuritySettingsType { get; set; }

        static SecuritySettings()
        {
            SecuritySettingsType = GetSetting(typeof(SecuritySettingEnum));
        }

        /// <summary>
        /// Таймаут сессии (мин)
        /// </summary>
        public static long SessionTimeout
        {
            get => GetValue<long>(SecuritySettingEnum.SessionTimeout);
            set => SetValue(SecuritySettingEnum.SessionTimeout, value);
        }

        /// <summary>
        /// Максимум неуспешных попыток авторизации
        /// </summary>
        public static long MaxCountUnsuccess
        {
            get => GetValue<long>(SecuritySettingEnum.MaxCountUnsuccess);
            set => SetValue(SecuritySettingEnum.MaxCountUnsuccess, value);
        }

        /// <summary>
        ///  Время блокировки аккаунта (мин.)
        /// </summary>
        public static long AccountBlockSpan
        {
            get => GetValue<long>(SecuritySettingEnum.AccountBlockSpan);
            set => SetValue(SecuritySettingEnum.AccountBlockSpan, value);
        }

        /// <summary>
        /// Срок хранения журнала безопасности
        /// </summary>
        public static long TimeLogStorage
        {
            get => GetValue<long>(SecuritySettingEnum.TimeLogStorage);
            set => SetValue(SecuritySettingEnum.TimeLogStorage, value);
        }

        /// <summary>
        /// Минимальное число знаков пароля
        /// </summary>
        public static long MinLenPassword
        {
            get => GetValue<long>(SecuritySettingEnum.MinLenPassword);
            set => SetValue(SecuritySettingEnum.MinLenPassword, value);
        }

        /// <summary>
        /// Период действия пароля (дни)
        /// </summary>
        public static long TimeLifePassword
        {
            get => GetValue<long>(SecuritySettingEnum.TimeLifePassword);
            set => SetValue(SecuritySettingEnum.TimeLifePassword, value);
        }

        /// <summary>
        /// Максимальное время неиспользования аккаунта (дни)
        /// </summary>
        public static long TimeLifeAccount
        {
            get => GetValue<long>(SecuritySettingEnum.TimeLifeAccount);
            set => SetValue(SecuritySettingEnum.TimeLifeAccount, value);
        }

        /// <summary>
        /// получить значение
        /// </summary>
        public static T GetValue<T>(long key)
        {
            var value = memoryCache.Get(key.ToString());

            if (value == null)
            {
                var uw = WindsorHolder.Resolve<IUnitOfWork>();
                try
                {
                    var jsonValue = uw.GetSet<Domain.SecuritySetting>().FirstOrDefault(s => s.Id == key)?.ValueJson;
                    if (!string.IsNullOrWhiteSpace(jsonValue))
                    {
                        value = JsonConvert.DeserializeObject<T>(jsonValue);
                        memoryCache.Set(key.ToString(), value, DateTimeOffset.Now.Add(TimeToLive));
                    }
                }
                finally
                {
                    WindsorHolder.Release(uw);
                }
            }

            return value is T variable ? variable : default(T);
        }

        /// <summary>
        /// сохранение настройки безопасности
        /// </summary>
        public static void SetValue<T>(long key, T value)
        {
            memoryCache.Set(key.ToString(), value, DateTimeOffset.Now.Add(TimeToLive));
            var uw = WindsorHolder.Resolve<IUnitOfWork>();
            try
            {
                var entity = uw.GetSet<Domain.SecuritySetting>().FirstOrDefault(s => s.Id == key);
                if (entity != null)
                {
                    entity.ValueJson = value == null ? null : JsonConvert.SerializeObject(value);
                    uw.SaveChanges();
                }
                else
                {
                    uw.AddEntity(new Domain.SecuritySetting
                    {
                        Id = key,
                        ValueJson = value == null ? null : JsonConvert.SerializeObject(value),
                        Name = SecuritySettingsType.ContainsKey(key) ? SecuritySettingsType[key] : "Не указано"
                    });
                }
            }
            finally
            {
                WindsorHolder.Release(uw);
            }
        }

        /// <summary>
        /// получить список элементов
        /// </summary>
        private static IDictionary<long, string> GetSetting(Type type)
        {
            return type
               .GetFields(BindingFlags.Public | BindingFlags.Static)
               .Where(f => f.FieldType == typeof(string))
               .Select(f =>
                  new Tuple<long, string>((long)f.GetValue(null), f.GetCustomAttribute<DisplayAttribute>(false)?.Name))
               .ToDictionary(d => d.Item1, d => d.Item2);
        }
    }
}
