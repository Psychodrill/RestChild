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

           // var applications = allApplications.ToList();
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


            //var applicants = unitOfWork.GetSet<Applicant>().Where(apt => requests.Any(r => r.ApplicantId == apt.RequestId)).AsQueryable();
            //var childs = unitOfWork.GetSet<Child>().Where(ch => requests.Any(r => r.ChildId == ch.RequestId)).AsQueryable();

            var  applicants = applications.Select(app => app.Applicant).AsQueryable();
            var childs = applications.SelectMany(ch => ch.Child).AsQueryable();
            //ICollection<Applicant> attendants = applications.SelectMany(at => at.Attendant).ToList();
            //applicants.AddRange(attendants);
            //ICollection<NotRespondedRequestsRow> results = null;
            ICollection<NotRespondedRequestsRow> resultsByApplicants = requests.Join(applicants, r => r.Applicant, at => at, (r, at) => new NotRespondedRequestsRow { RequestId = at.RequestId, Child = null, Applicant = at, RequestNumber = null, TypeOfRest = null, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryType.Name, RequestDateTime = null })/*.Distinct().ToList();*/
                                                                               .Join(applications, ra => ra.Applicant, a => a.Applicant, (ra, a) => new NotRespondedRequestsRow { RequestId = a.Id, Child = null, Applicant = ra.Applicant, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = ra.ExchangeBaseRegistryTypeName, RequestDateTime = null }).Distinct().ToList();
            

            ICollection<NotRespondedRequestsRow> resultsByChilds = requests.Join(childs, r => r.Child, ch => ch, (r, ch) => new NotRespondedRequestsRow { RequestId = ch.RequestId, Child = ch, Applicant = null, RequestNumber = null, TypeOfRest = null, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryType.Name, RequestDateTime = null }).Distinct().ToList();

            ICollection<NotRespondedRequestsRow> unionResults = resultsByApplicants.Union(resultsByChilds).ToList();

            ICollection<NotRespondedRequestsRow> results = unionResults.Join(applications, r => r.RequestId, a => a.Id, (r, a) => new NotRespondedRequestsRow { RequestId = r.RequestId, Child = r.Child, Applicant = r.Applicant, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryTypeName, RequestDateTime = a.DateRequest, ApplicationId=r.ApplicationId }).ToList();

            //var exUTS = unitOfWork.GetSet<ExchangeUTS>().Where(ex => requests.Any(r => r.ServiceNumber.Contains(ex.ServiceNumber))).AsQueryable();

            //var applicants = unitOfWork.GetSet<Applicant>().Where(appls=>requests.Any(r => r.Id==appls.RequestId)).AsQueryable();

            //var results = requests.Join(exUTS, re => re.ServiceNumber.Substring(0, 30), ex => ex.ServiceNumber, (re, ex) => new NotRespondedRequestsRow { RequestId = re.Id, Child = re.Child, Applicant = re.Applicant, RequestNumber = ex.ServiceNumber, TypeOfRest = null, ExchangeBaseRegistryTypeName = re.ExchangeBaseRegistryType.Name, RequestDateTime = null,TimeOfRest=null, ApplicationId = ex.RequestId })
            //                      .Join(applications, r => r.ApplicationId, a => a.Id, (r, a) => new NotRespondedRequestsRow { RequestId = r.RequestId, Child = r.Child, Applicant = r.Applicant, RequestNumber = a.RequestNumber, TypeOfRest = a.TypeOfRest.Name, ExchangeBaseRegistryTypeName = r.ExchangeBaseRegistryTypeName, RequestDateTime = a.DateRequest, TimeOfRest = a.TimeOfRest, ApplicationId=r.ApplicationId }).Distinct().ToList();
            ////.Join(applicants, res => res.Applicant.Id, a => a.Id, (res, a) => new NotRespondedRequestsRow { RequestId = res.RequestId, Child = res.Child, Applicant = res.Applicant, RequestNumber = res.RequestNumber, TypeOfRest = res.TypeOfRest, ExchangeBaseRegistryTypeName = res.ExchangeBaseRegistryTypeName, RequestDateTime = res.RequestDateTime, TimeOfRest = res.TimeOfRest, ApplicationId = res.ApplicationId }).Distinct().ToList();

            //results.RemoveAll(x => x.TimeOfRest == null);//удаление записей с нарушенной целостностью данных
            //if (filter?.DateFormingBegin.HasValue ?? false)
            //{
            //    results = results.Where(res => res.RequestDateTime >= filter.DateFormingBegin.Value).ToList();
            //}
            //else
            //{
            //    //DateTime innerDate = new DateTime(DateTime.Now.Year, 1, 1);
            //    int maxYear = applications.Max(x => x.TimeOfRest.Year);
            //    results = results.Where(res => res.TimeOfRest.Year >= maxYear).ToList();
            //}
            //if (filter?.DateFormingEnd.HasValue ?? false)
            //{
            //    results = results.Where(res => res.RequestDateTime <= filter.DateFormingEnd.Value).ToList();
            //}
            //else
            //{
            //    int maxYear = applications.Max(x => x.TimeOfRest.Year);
            //    results = results.Where(res => res.TimeOfRest.Year <= maxYear).ToList();
            //}



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
