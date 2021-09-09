namespace RestChild.Web.Models
{
    /// <summary>
    ///     Модель фильтра реестра ГБУ
    /// </summary>
    public class OrganizationGBUSearchModel : BaseFilterModel<OrganizationGBUViewModel>
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Идентификатор префектуры
        /// </summary>
        public long? OrganisationId { get; set; }


        /// <summary>
        ///     Наименование префектуры
        /// </summary>
        public string OrganisationName { get; set; }
    }
}
