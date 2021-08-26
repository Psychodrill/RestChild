using System.ComponentModel.DataAnnotations;

namespace RestChild.Comon.Enumeration
{
    /// <summary>
    ///     Настройки безопасности
    /// </summary>
    public static class SecuritySettingEnum
    {
        /// <summary>
        ///     Таймаут сессии (мин)
        /// </summary>
        [Display(Name = "Таймаут сессии (мин)")]
        public static long SessionTimeout = 1;

        /// <summary>
        ///     Максимум неуспешных попыток авторизации
        /// </summary>
        [Display(Name = "Максимум неуспешных попыток авторизации")]
        public static long MaxCountUnsuccess = 2;

        /// <summary>
        ///     Время блокировки аккаунта (мин.)
        /// </summary>
        [Display(Name = "Время блокировки аккаунта (мин.)")]
        public static long AccountBlockSpan = 3;

        /// <summary>
        ///     Срок хранения журнала безопасности
        /// </summary>
        [Display(Name = "Срок хранения журнала безопасности (дней)")]
        public static long TimeLogStorage = 4;

        /// <summary>
        ///     Минимальное число знаков пароля
        /// </summary>
        [Display(Name = "Минимальное число знаков пароля")]
        public static long MinLenPassword = 5;

        /// <summary>
        ///     Период действия пароля (дни)
        /// </summary>
        [Display(Name = "Период действия пароля (дни)")]
        public static long TimeLifePassword = 6;


        /// <summary>
        ///     Максимальное время неиспользования аккаунта (дни)
        /// </summary>
        [Display(Name = "Максимальное время неиспользования аккаунта (дни)")]
        public static long TimeLifeAccount = 7;
    }
}
