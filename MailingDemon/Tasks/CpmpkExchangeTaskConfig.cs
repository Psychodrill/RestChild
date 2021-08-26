namespace MailingDemon.Tasks
{
    /// <summary>
    ///     настройки таска обмена с ЦПМПК
    /// </summary>
    public class CpmpkExchangeTaskConfig : BaseConfig
    {
        /// <summary>
        ///     вид ограничения
        /// </summary>
        public long TypeOfRestrictionId { get; set; }

        /// <summary>
        ///     адрес сервиса
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     логин
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        ///     пароль
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        ///     АПИ Кей
        /// </summary>
        public string ApiKey { get; set; }
    }
}
