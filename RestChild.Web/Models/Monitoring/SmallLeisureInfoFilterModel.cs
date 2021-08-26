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
    ///     Модель фильтра поиска формы сведений о финансах
    /// </summary>
    public class SmallLeisureInfoFilterModel
    {
        public SmallLeisureInfoFilterModel()
        {
            PageNumber = 1;
            Result = new CommonPagedList<MonitoringSmallLeisureInfo>(new List<MonitoringSmallLeisureInfo>(), PageNumber, Settings.Default.TablePageSize, 0);
            Months = new Dictionary<int, string>() {
                { 1, "Январь" },
                { 2, "Февраль" },
                { 3, "Март" },
                { 4, "Апрель" },
                { 5, "Май" },
                { 6, "Июнь" },
                { 7, "Июль" },
                { 8, "Август" },
                { 9, "Сетнябрь" },
                { 10, "Октябрь" },
                { 11, "Ноябрь" },
                { 12, "Декабрь" },
            };
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
        ///     Месяц
        /// </summary>
        public int? Month { get; set; }

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
        public CommonPagedList<MonitoringSmallLeisureInfo> Result { get; set; }

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

        /// <summary>
        ///     Список месяцев
        /// </summary>
        public IReadOnlyDictionary<int, string> Months { get; set; }
    }
}
