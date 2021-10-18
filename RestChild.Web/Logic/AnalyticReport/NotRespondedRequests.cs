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


            var applications = unitOfWork.GetSet<Request>().AsQueryable();

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


            var exUTS = unitOfWork.GetSet<ExchangeUTS>().Where(ex => requests.Any(r => r.ServiceNumber.Contains(ex.ServiceNumber))).AsQueryable();


            var results = requests.Join(exUTS, re => re.ServiceNumber.Substring(0, 30), ex => ex.ServiceNumber, (re, ex) => new NotRespondedRequestsRow { RequestId = re.Id, Child = re.Child, Applicant = re.Applicant, RequestNumber = ex.ServiceNumber, TypeOfRest = null, ExchangeBaseRegistryTypeName = re.ExchangeBaseRegistryType.Name, RequestDateTime = null, ApplicationId = ex.RequestId })
                                  .Join(applications, r => r.ApplicationId, a => a.Id, (r, a) => new NotRespondedRequestsRow { RequestId = r.RequestId, Child = r.Child, Applicant = r.Applicant, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryTypeName, RequestDateTime = a.DateRequest, ApplicationId=r.ApplicationId }).Distinct().ToList();



            if (filter?.DateFormingBegin.HasValue ?? false)
            {
                results = results.Where(res => res.RequestDateTime >= filter.DateFormingBegin.Value).ToList();
            }
            else
            {
                DateTime innerDate = new DateTime(DateTime.Now.Year, 1, 1);
                results = results.Where(res => res.RequestDateTime >= innerDate).ToList();
            }
            if (filter?.DateFormingEnd.HasValue ?? false)
            {
                results = results.Where(res => res.RequestDateTime <= filter.DateFormingEnd.Value).ToList();
            }
            else
            {
                DateTime innerDate = new DateTime(DateTime.Now.Year, 12, 31);
                results = results.Where(res => res.RequestDateTime <= innerDate).ToList();
            }


            var columns = new List<ExcelColumn<NotRespondedRequestsRow>>
            {

                new ExcelColumn<NotRespondedRequestsRow> {Title = "Номер заявления", Func = r => r.RequestNumber},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Цель обращения", Func = r => r.TypeOfRest},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "ФИО(того, на кого запрос)", Func = r => r.Names},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Запрос", Func = r => r.ExchangeBaseRegistryTypeName},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Дата запроса", Func = r => r.RequestDateTime},


            };

            return new ExcelTable<NotRespondedRequestsRow>(columns, results.OrderBy(ss => ss.RequestNumber));
        }

        public class NotRespondedRequestsRow
        {
            public long? ApplicationId { get; set; }
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
                                        ", ",
                                        Child?.LastName,
                                        " ",
                                        Child?.FirstName,
                                        " ",
                                        Child?.MiddleName);
                }
            }

            public Child Child {get; set;}

            public Applicant Applicant {get; set;}

            public string ExchangeBaseRegistryTypeName { get; set; }

            public DateTime? RequestDateTime { get; set; }
                       


        }
    }
}
