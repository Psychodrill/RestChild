using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models
{
    public class OrganizationCampSearchModel : BaseFilterModel<OrganizationCampViewModel>
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        ///     Регион
        /// </summary>
        public long? RegionId { get; set; }

        /// <summary>
        ///     Список регионов
        /// </summary>
        public ICollection<StateDistrict> Regions { get; set; }
    }
}
