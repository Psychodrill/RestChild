using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Comon.ToExcel;
using RestChild.Extensions.Filter;


namespace RestChild.Web.Logic.AnalyticReport
{

    /// <summary>
    /// Логика отчёта по неудовлетворённым запросам
    /// </summary>
    public static class NotRespondedRequests
    {

        /// <summary>
        ///     Неудовлетворённые запросы
        /// </summary>
        public static BaseExcelTable GetNotRespondedRequests(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
        {

            var applications = unitOfWork.GetSet<Request>().Where(r => r.IsDeleted == false).AsQueryable();

            if (filter?.DateFormingBegin.HasValue ?? false)
            {
                applications = applications.Where(apps => apps.DateRequest >= filter.DateFormingBegin.Value);
            }
            else
            {
                //DateTime innerDate = new DateTime(DateTime.Now.Year, 1, 1);
                int maxYear = applications.Max(x => x.TimeOfRest.Year);
                applications = applications.Where(apps => apps.TimeOfRest.Year >= maxYear);
            }
            if (filter?.DateFormingEnd.HasValue ?? false)
            {
                applications = applications.Where(apps => apps.DateRequest <= filter.DateFormingEnd.Value);
            }
            else
            {
                int maxYear = applications.Max(x => x.TimeOfRest.Year);
                applications = applications.Where(res => res.TimeOfRest.Year <= maxYear);
            }
            //var sf = applications.ToList();
            var requests = unitOfWork.GetSet<ExchangeBaseRegistry>().Where(row => row.ResponseGuid == null && (
                                                                                  row.ExchangeBaseRegistryTypeId == -1 || //Запрос наличия заключения ЦПМПК
                                                                                  row.ExchangeBaseRegistryTypeId == 10209 || //Запрос паспортного досье по СНИЛС
                                                                                  row.ExchangeBaseRegistryTypeId == 8255 ||// Запрос СНИЛС по ФИО
                                                                                  row.ExchangeBaseRegistryTypeId == 260 ||//Наличие льготной категории
                                                                                  row.ExchangeBaseRegistryTypeId == 10211 ||//Получение регистрации по месту жительства
                                                                                  row.ExchangeBaseRegistryTypeId == 3091 ||//Предоставление из ЕГР ЗАГС сведений об актах гражданского состояния
                                                                                  row.ExchangeBaseRegistryTypeId == 10214 ||//Проверка адреса регистрации ребенка (МВД)
                                                                                  row.ExchangeBaseRegistryTypeId == -2 ||//Проверка законного представительства внутри АИС ДО
                                                                                  row.ExchangeBaseRegistryTypeId == 22 ||//Проверка родства
                                                                                  row.ExchangeBaseRegistryTypeId == 10244))// Проверка СНИЛС
                                                                                  .AsQueryable();

            if (filter?.ExchangeBaseRegistryTypeId.HasValue ?? false)
            {
                requests = requests.Where(row => row.ExchangeBaseRegistryTypeId == filter.ExchangeBaseRegistryTypeId);
            }
            //var sdf = requests.ToList();
            var applicants = applications.Select(app => app.Applicant).AsQueryable();
            var childs = applications.SelectMany(ch => ch.Child).AsQueryable();

            var attendants = applications.SelectMany(at => at.Attendant).AsQueryable();

            ICollection<NotRespondedRequestsRow> resultsByApplicants = requests.Join(applications, ra => ra.Applicant.Id, a => a.Applicant.Id, (ra, a) => new NotRespondedRequestsRow { RequestId = a.Id, Child = null, Applicant = a.Applicant, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = ra.ExchangeBaseRegistryType.Name, RequestDateTime = a.DateRequest }).ToList();

            //ICollection<NotRespondedRequestsRow> resultsByAgents = requests.Join(applications, ra => ra.Applicant.Id, a => a.Agent.Id, (ra, a) => new NotRespondedRequestsRow { RequestId = a.Id, Child = null, Applicant = a.Agent, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = ra.ExchangeBaseRegistryType.Name, RequestDateTime = a.DateRequest }).ToList();

            ICollection<NotRespondedRequestsRow> resultsByAttendants = requests.Join(attendants, r => r.Applicant.Id, at => at.Id, (r, at) => new NotRespondedRequestsRow { RequestId = at.RequestId, Child = null, Applicant = at, RequestNumber = null, TypeOfRest = null, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryType.Name, RequestDateTime = null })
                                                                               .Join(applications, ra => ra.RequestId, a => a.Id, (ra, a) => new NotRespondedRequestsRow { RequestId = a.Id, Child = null, Applicant = ra.Applicant, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = ra.ExchangeBaseRegistryTypeName, RequestDateTime = a.DateRequest }).ToList();

            ICollection<NotRespondedRequestsRow> resultsByChilds = requests.Join(childs, r => r.Child.Id, ch => ch.Id, (r, ch) => new NotRespondedRequestsRow { RequestId = ch.RequestId, Child = ch, Applicant = null, RequestNumber = null, TypeOfRest = null, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryType.Name, RequestDateTime = null })
                                                                           .Join(applications, ra => ra.RequestId, a => a.Id, (ra, a) => new NotRespondedRequestsRow { RequestId = a.Id, Child = ra.Child, Applicant = null, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = ra.ExchangeBaseRegistryTypeName, RequestDateTime = a.DateRequest }).ToList();

            ICollection<NotRespondedRequestsRow> unionResults = resultsByApplicants.Union(resultsByAttendants).ToList();


            ICollection<NotRespondedRequestsRow> results = unionResults.Union(resultsByChilds).ToList();


            var columns = new List<ExcelColumn<NotRespondedRequestsRow>>
            {

                new ExcelColumn<NotRespondedRequestsRow> {Title = "Номер заявления", Func = r => r.RequestNumber},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Цель обращения", Func = r => r.TypeOfRest},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "ФИО(того, на кого запрос)", Func = r => r.Names},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Запрос", Func = r => r.ExchangeBaseRegistryTypeName},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Дата заявления", Func = r => r.RequestDateTime},


            };

            return new ExcelTable<NotRespondedRequestsRow>(columns, results.OrderBy(ss => ss.RequestNumber));
        }

        public class NotRespondedRequestsRow
        {
            //public long? ApplicationId { get; set; }
            public long? RequestId { get; set; }
            public string RequestNumber { get; set; }

            public string TypeOfRest { get; set; }

            public string Names
            {
                get
                {
                    return string.Concat(Applicant?.LastName,
                                        " ",
                                        Applicant?.FirstName,
                                        " ",
                                        Applicant?.MiddleName,
                                        Child?.LastName,
                                        " ",
                                        Child?.FirstName,
                                        " ",
                                        Child?.MiddleName);
                }
            }


            public Child Child { get; set; }

            public Applicant Applicant { get; set; }

            //public Agent Agent { get; set; }

            public string ExchangeBaseRegistryTypeName { get; set; }

            public DateTime? RequestDateTime { get; set; }

        }
    }
}
