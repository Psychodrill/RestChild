using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     модель заполненная
    /// </summary>
    public class FilledFormModel
    {

        /// <summary>
        ///     Год потребности
        /// </summary>
        public long? YearOfRest { get; set; }

        /// <summary>
        ///     Список годов потребности
        /// </summary>
        public YearOfRest[] YearsOfRest { get; set; }
    }
}
