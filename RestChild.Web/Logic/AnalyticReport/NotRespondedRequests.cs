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

            var requests = unitOfWork.GetSet<ExchangeBaseRegistry>().Where(row => (row.ResponseGuid == null || row.ResponseGuid == string.Empty) &&
                                                                                  (row.ExchangeBaseRegistryTypeId == -1 || //Запрос наличия заключения ЦПМПК
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

            var applicants = unitOfWork.GetSet<Applicant>().Where(a => requests.Any(r => r.ApplicantId==a.Id)).ToList();
            
            var childs = unitOfWork.GetSet<Child>().Where(c => requests.Any(r => r.ChildId == c.Id)).ToList();
            var applications = unitOfWork.GetSet<Request>().Where(app => applicants.Any(a => a.Id==app.Id)).ToList();



            //var results = requests.Join(childs, r => r.ChildId, c => c.Id, (r, c) => new NotRespondedRequestsRow { RequestId = r.Id, Names = c.FirstName })
            //                      .Join(applicants, r => r.ApplicantId, a => a.Id, (r, a) => new NotRespondedRequestsRow { RequestId = r.RequestId, Names = a.FirstName, })
            //                      .Join(applications, r => r.ApplicantId, a => a.ApplicantId, (r, a) => new NotRespondedRequestsRow { RequestId = r.RequestId, Names = a.FirstName, })


            //var results = requests.Join(applicants, r => r.ApplicantId, a => a.Id, (r, a) => new NotRespondedRequestsRow { RequestId = r.Id,RequestNumber=a.RequestN, Names = a.FirstName,  });
            //var results = requests.

            var columns = new List<ExcelColumn<NotRespondedRequestsRow>>
            {
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Номер заявления", Func = r => r.RequestNumber},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Цель обращения", Func = r => r.TypeOfRest},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "ФИО(того, на кого запрос)", Func = r => r.Names},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Запрос", Func = r => r.ExchangeBaseRegistryType},
                new ExcelColumn<NotRespondedRequestsRow> {Title = "Дата запроса", Func = r => r.RequestDateTime}

            };


            var results = new List<NotRespondedRequestsRow>();

            foreach (var req in requests)
            {

            }
            //foreach (var req in requsts.ToList())
            //{
            //    if (req.Child?.Any() ?? false)
            //    {
            //        foreach (var c in req.Child)
            //        {
            //            var summ = string.Empty;
            //            if (comps.Contains(req.TypeOfRestId.Value))
            //            {
            //                summ = c.AmountOfCompensation?.ToString("C");
            //            }
            //            else
            //            {
            //                summ = unitOfWork.GetSet<AverageRestPrice>()
            //                    .Where(ss => ss.YearOfRestId == req.YearOfRestId && ss.TypeOfRestId == req.TypeOfRestId)
            //                    .Select(ss => ss.Price).FirstOrDefault().ToString("C");
            //            }

            //            results.Add(new EGISORow
            //            {
            //                RequestNumber = req.RequestNumber,
            //                RequestDate = req.DateRequest,
            //                RequestStatus = req.Status.Name,
            //                RequestDateIssured = req.DateChangeStatus,
            //                TypeOfRest = req.TypeOfRest.Name,
            //                FirstName = c.FirstName,
            //                LastName = c.LastName,
            //                MiddleName = c.MiddleName,
            //                Sex = c.Male ? "Мужской" : "Женский",
            //                DateOfBirth = c.DateOfBirth,
            //                SNILS = c.Snils,
            //                DocumentType = c.DocumentType.Name,
            //                DocumentSeria = c.DocumentSeria,
            //                DocumentNumber = c.DocumentNumber,
            //                DocumentDate = c.DocumentDateOfIssue,
            //                DocumentIssured = c.DocumentSubjectIssue,
            //                TypeOfRestriction = c.TypeOfRestriction?.Name,
            //                TypeOfSubRestriction = c.TypeOfSubRestriction?.Name,
            //                BenefitType = c.BenefitType?.Name,
            //                RequestSumm = summ
            //            });
            //        }
            //    }

            //    if (req.InformationVouchers?.Any() ?? false)
            //    {
            //        foreach (var iv in req.InformationVouchers)
            //        {
            //            foreach (var ap in iv.AttendantsPrice)
            //            {
            //                if (req.ApplicantId == ap.ApplicantId)
            //                {
            //                    var summ = string.Empty;
            //                    if (comps.Contains(req.TypeOfRestId.Value))
            //                    {
            //                        summ = ap.AmountOfCompensation?.ToString("C");
            //                    }

            //                    results.Add(new EGISORow
            //                    {
            //                        RequestNumber = req.RequestNumber,
            //                        RequestDate = req.DateRequest,
            //                        RequestStatus = req.Status.Name,
            //                        RequestDateIssured = req.DateChangeStatus,
            //                        TypeOfRest = req.TypeOfRest.Name,
            //                        FirstName = req.Applicant.FirstName,
            //                        LastName = req.Applicant.LastName,
            //                        MiddleName = req.Applicant.MiddleName,
            //                        Sex = (req.Applicant.Male ?? true) ? "Мужской" : "Женский",
            //                        DateOfBirth = req.Applicant.DateOfBirth,
            //                        SNILS = req.Applicant.Snils,
            //                        DocumentType = req.Applicant.DocumentType?.Name,
            //                        DocumentSeria = req.Applicant.DocumentSeria,
            //                        DocumentNumber = req.Applicant.DocumentNumber,
            //                        DocumentDate = req.Applicant.DocumentDateOfIssue,
            //                        DocumentIssured = req.Applicant.DocumentSubjectIssue,
            //                        BenefitType = req.Applicant.BenefitType?.Name,
            //                        RequestSumm = summ
            //                    });
            //                    break;
            //                }
            //            }
            //        }
            //    }


            //    if (req.Attendant?.Any() ?? false)
            //    {
            //        foreach (var a in req.Attendant)
            //        {
            //            var summ = string.Empty;
            //            if (comps.Contains(req.TypeOfRestId.Value))
            //            {
            //                summ = req.InformationVouchers.SelectMany(sx => sx.AttendantsPrice)
            //                    .Where(sx => sx.ApplicantId == a.Id).Select(sx => sx.AmountOfCompensation)
            //                    .FirstOrDefault()?.ToString("C");
            //            }

            //            results.Add(new EGISORow
            //            {
            //                RequestNumber = req.RequestNumber,
            //                RequestDate = req.DateRequest,
            //                RequestStatus = req.Status.Name,
            //                RequestDateIssured = req.DateChangeStatus,
            //                TypeOfRest = req.TypeOfRest.Name,
            //                FirstName = a.FirstName,
            //                LastName = a.LastName,
            //                MiddleName = a.MiddleName,
            //                Sex = (a.Male ?? true) ? "Мужской" : "Женский",
            //                DateOfBirth = a.DateOfBirth,
            //                SNILS = a.Snils,
            //                DocumentType = a.DocumentType?.Name,
            //                DocumentSeria = a.DocumentSeria,
            //                DocumentNumber = a.DocumentNumber,
            //                DocumentDate = a.DocumentDateOfIssue,
            //                DocumentIssured = a.DocumentSubjectIssue,
            //                BenefitType = a.BenefitType?.Name,
            //                RequestSumm = summ
            //            });
            //        }
            //    }
            //}

            return new ExcelTable<NotRespondedRequestsRow>(columns, results.OrderBy(ss => ss.RequestNumber));
        }

        public class NotRespondedRequestsRow
        {
            public long RequestId { get; set; }
            public string RequestNumber { get; set; }

            public string TypeOfRest { get; set; }

            public string Names { get; set; }

            public string ExchangeBaseRegistryType { get; set; }

            public DateTime RequestDateTime { get; set; }

            public int ApplicantId { get; set; }

            public int ChildId { get; set; }

        }
    }
}
