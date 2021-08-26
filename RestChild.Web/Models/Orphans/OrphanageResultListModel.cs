namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель приюта для списка
    /// </summary>
    public class OrphanageResultListModel
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Краткое наименование
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        ///     Адреса
        /// </summary>
        public string[] Address { get; set; }

        /// <summary>
        ///     Директор
        /// </summary>
        public string DirectorName { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     E-mail
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        ///     ФИО (ответственного за отдых)
        /// </summary>
        public string FioRfr { get; set; }

        /// <summary>
        ///     Телефон (ответственного за отдых)
        /// </summary>
        public string PhoneRfr { get; set; }

        /// <summary>
        ///     E-mail (ответственного за отдых)
        /// </summary>
        public string EMailRfr { get; set; }

    }
}
