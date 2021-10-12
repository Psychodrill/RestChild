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

            var requests = unitOfWork.GetSet<ExchangeBaseRegistry>().Where(row => row.ResponseGuid != null && (
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

            if (filter?.DateFormingBegin.HasValue ?? false)
            {
                requests = requests.Where(row => row.SendDate >= filter.DateFormingBegin.Value);
            }
            if (filter?.DateFormingEnd.HasValue ?? false)
            {
                requests = requests.Where(row => row.SendDate <= filter.DateFormingEnd.Value);
            }
            if (filter?.ExchangeBaseRegistryTypeId.HasValue ??false)
            {
                requests = requests.Where(row => row.ExchangeBaseRegistryTypeId == filter.ExchangeBaseRegistryTypeId);
            }

            var applicants = unitOfWork.GetSet<Applicant>().Where(a => requests.Any(r => r.ApplicantId == a.Id)).AsQueryable();

            var childs = unitOfWork.GetSet<Child>().Where(c => requests.Any(r => r.ChildId == c.Id)).AsQueryable();
            var applications = unitOfWork.GetSet<Request>().Where(app => applicants.Any(a => a.Id == app.ApplicantId)).AsQueryable();

            var requestTypes = unitOfWork.GetSet<ExchangeBaseRegistryType>();

            //int checkr = requests.Count();
            //int checka = applicants.Count();
            //int checkapps = applications.Count();

            var results = requests.Join(applicants, r => r.ApplicantId, a => a.Id, (r, a) => new NotRespondedRequestsRow { RequestId = r.Id, Child = r.Child, Applicant = r.Applicant, ApplicantId= a.Id, RequestNumber = null, TypeOfRest = null, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryType.Name, RequestDateTime = r.SendDate })
                                  .Join(applications, re => re.ApplicantId, ap => ap.ApplicantId, (re, ap) => new NotRespondedRequestsRow { RequestId = re.RequestId, Child = re.Child, Applicant = re.Applicant, ApplicantId =re.ApplicantId, RequestNumber = ap.RequestNumber, TypeOfRest = ap.TypeOfRest.Name, ExchangeBaseRegistryTypeName = re.ExchangeBaseRegistryTypeName, RequestDateTime = re.RequestDateTime }).ToList();

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
            public long RequestId { get; set; }
            public string RequestNumber { get; set; }

            public string TypeOfRest { get; set; }

            public string Names
            {
                get
                {
                    return string.Concat(Applicant?.LastName,
                                        Applicant?.FirstName,
                                        Applicant?.MiddleName,
                                        Child?.LastName,
                                        Child?.FirstName,
                                        Child?.MiddleName);
                }
            }

            public Child Child {get; set;}

            public Applicant Applicant {get; set;}

            public string ExchangeBaseRegistryTypeName { get; set; }

            public DateTime? RequestDateTime { get; set; }

            public long ApplicantId { get; set; }

            public int ChildId { get; set; }

        }
    }
}
