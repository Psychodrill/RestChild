using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель фильтра поиска формы сведений о числености детей
    /// </summary>
    public class ChildrenNumberInformationFilterModel
    {
        public ChildrenNumberInformationFilterModel()
        {
            PageNumber = 1;
            Result = new CommonPagedList<MonitoringChildrenNumberInformation>(new List<MonitoringChildrenNumberInformation>(), PageNumber, Settings.Default.TablePageSize, 0);
        }

        /// <summary>
        ///     Год потребности
        /// </summary>
        public long? YearOfRest { get; set; }


        /// <summary>
        ///     Идентификатор объекта мониторинга
        /// </summary>
        public long? OrganisationId { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     Номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Результат
        /// </summary>
        public CommonPagedList<MonitoringChildrenNumberInformation> Result { get; set; }

        /// <summary>
        ///     Список годов потребности
        /// </summary>
        public IDictionary<long, string> YearsOfRest { get; set; }


        /// <summary>
        ///     Список объектов мониторинга
        /// </summary>
        public IDictionary<long, string> Organisations { get; set; }

        /// <summary>
        ///     Список стаусов
        /// </summary>
        public IDictionary<long, string> States { get; set; }
    }
}
