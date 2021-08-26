using System.Data.Entity.Migrations;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Заполнение источников финансирования для мониторинга
        /// </summary>
        private static void MonitoringFinancialSource(Context context)
        {
            var i = 1;
            context.MonitoringFinancialSource.AddOrUpdate(f => f.Id,
            new MonitoringFinancialSource {
                //1
                Id = i++,
                Name = "Общий объем финансирования (тыс. руб.), в том числе:",
                IsActive = true,
                ShowInForm = false,
                Code = "01"
            },
            new MonitoringFinancialSource
            {
                //2
                Id = i++,
                Name = "Объем финансовых средств, направленных на развитие инфраструктуры организаций отдыха детей и их оздоровления (тыс.руб.), в том числе:",
                IsActive = true,
                ShowInForm = false,
                Code = "08"
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства бюджета субъекта РФ (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "02",
                ParrentId = 1
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства муниципальных бюджетов (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "03",
                ParrentId = 1
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства профсоюзных организаций (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "04",
                ParrentId = 1
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства родителей/законных представителей (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "05",
                ParrentId = 1
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства предприятий и организаций (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "06",
                ParrentId = 1
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "иные источники (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "07",
                ParrentId = 1
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства бюджета субъектов РФ (тыс.руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "09",
                ParrentId = 2
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "средства муниципальных бюджетов (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "10",
                ParrentId = 2
            },
            new MonitoringFinancialSource
            {
                Id = i++,
                Name = "иные источники (тыс. руб.)",
                IsActive = true,
                ShowInForm = true,
                Code = "11",
                ParrentId = 2
            });
        }
    }
}
