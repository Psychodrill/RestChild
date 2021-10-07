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
        /// Сформировать отчёт по неудовлетворённым запросам
        /// </summary>
        //    public static BaseExcelTable GetNotRespondedRequests(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
        //    {
        //        var yearOfRestIds = String.Empty;
        //        if (filter.NextYearsIncluded)
        //        {
        //            var yors = unitOfWork.GetSet<YearOfRest>().Where(y => y.Id >= filter.YearOfRestId).OrderBy(x => x.Id);
        //            yearOfRestIds = string.Join(",", yors.Select(y => y.Id).ToList());
        //        }
        //        else
        //        {
        //            yearOfRestIds = filter.YearOfRestId.ToString();
        //        }

        //        var query = $@"

        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP') IS NOT NULL DROP TABLE #B09DFB1D_TMP
        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP2') IS NOT NULL DROP TABLE #B09DFB1D_TMP2
        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP3') IS NOT NULL DROP TABLE #B09DFB1D_TMP3
        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP4') IS NOT NULL DROP TABLE #B09DFB1D_TMP4

        //        select c.ChildUniqeId, count(*) as cnt into #B09DFB1D_TMP
        //        from dbo.Child c
        //        inner join dbo.Request r on r.Id = c.RequestId
        //        where r.YearOfRestId  in ({yearOfRestIds}) and r.TypeOfRestId not in (7,11)
        //        group by c.ChildUniqeId


        //        select a.RelativeUniqeId, count(*) as cnt into #B09DFB1D_TMP2
        //        from dbo.Applicant a
        //        inner join dbo.Request r on r.ApplicantId = a.Id
        //        where r.TypeOfRestId in (13,14)	and r.YearOfRestId  in ({yearOfRestIds})
        //        group by a.RelativeUniqeId


        //        select
        //         r.Id, tr.R into #B09DFB1D_TMP3
        //        from
        //        dbo.Request r
        //        inner join dbo.Status s on s.Id = r.StatusId
        //        inner join dbo.TypeOfRest t on t.Id = r.TypeOfRestId
        //        left join (
        //         select
        //          r.Id, case when min(t.cnt) = 1 then 1
        //            when min(t.cnt)=2 then 2 else 3 end as R
        //         from dbo.Applicant a
        //         inner join dbo.Request r on r.ApplicantId = a.Id
        //         inner join #B09DFB1D_TMP2 t on t.RelativeUniqeId = a.RelativeUniqeId
        //         group by r.Id
        //         union all
        //         select
        //          a.RequestId, case
        //		            when min(t.cnt) = 1 or min(case when a.BenefitTypeId in (38,39,40) then 1 else 2 end)=1 then 1
        //		            when min(t.cnt) = 2 then 2 else 3 end as R
        //         from dbo.Child a
        //         inner join #B09DFB1D_TMP t on t.ChildUniqeId = a.ChildUniqeId
        //         where a.RequestId is not null
        //         group by a.RequestId
        //        ) tr on tr.Id = r.Id
        //        where r.IsDeleted=0
        //        --and r.StatusId in (1050, 1052, 1055)"
        //        + "\n" + (filter.StatusId != null ? $" and r.StatusId = {filter.StatusId} " : " ") + "\n" +
        //        @"--and r.YearOfRestId=7
        //        select c.RequestId,
        //        min(isnull(tr.RestrictionGroupId,3)) as RestrictionGroupId into #B09DFB1D_TMP4
        //        from
        //        dbo.Child c
        //        left join dbo.TypeOfSubRestriction tr on tr.Id=c.TypeOfSubRestrictionId
        //        group by c.RequestId

        //        select tr.Name as Purpose, right('00' + cast(t.Month as varchar),2) + '.' + right('00' + cast(t.[DayOfMonth] as varchar),2) + '-' + t.Name as Period, p.Name as Region, case when tr.Id=14 then 1 else r.CountAttendants end CountAttendants, r.CountPlace,
        //        rg.Name as IsInvalid, t3.R as Queue, count(*) as Quantity, st.Name as Status
        //        from
        //        dbo.Request r
        //        inner join #B09DFB1D_TMP3 t3 on t3.Id = r.Id
        //        left join #B09DFB1D_TMP4 i on i.RequestId = r.Id
        //        left join dbo.RestrictionGroup rg on rg.Id= isnull(i.RestrictionGroupId,3)
        //        left join dbo.TimeOfRest t on t.Id=r.TimeOfRestId
        //        left join dbo.PlaceOfRest p on p.Id=r.PlaceOfRestId
        //        left join dbo.TypeOfRest tr on tr.Id=r.TypeOfRestId
        //        inner join dbo.Status st on st.Id=r.StatusId
        //        where r.IsDeleted=0
        //        --and r.YearOfRestId=7
        //        and r.IsFirstCompany=1
        //        and (r.[CountAttendants]>0 or tr.Id=14)
        //        group by tr.Name,t.Month, t.[DayOfMonth], t.Name, p.Name, case when tr.Id=14 then 1 else r.CountAttendants end, r.CountPlace, rg.Name, t3.R, st.Name
        //        order by tr.Name, t.Month, t.[DayOfMonth], t3.R

        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP') IS NOT NULL DROP TABLE #B09DFB1D_TMP
        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP2') IS NOT NULL DROP TABLE #B09DFB1D_TMP2
        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP3') IS NOT NULL DROP TABLE #B09DFB1D_TMP3
        //        IF OBJECT_ID('tempdb..#B09DFB1D_TMP4') IS NOT NULL DROP TABLE #B09DFB1D_TMP4

        //        ";

        //        try
        //        {
        //            var data = unitOfWork.Database.SqlQuery<ReportRow>(query);
        //            var res = new ExcelTable<ReportRow>(new List<ExcelColumn<ReportRow>>
        //            {
        //                //new ExcelColumn<ReportRow> {Title = "Цель обращения", Func = r => r.Purpose},
        //                //new ExcelColumn<ReportRow> {Title = "Период", Func = r => r.Period},
        //                //new ExcelColumn<ReportRow> {Title = "Регион", Func = r => r.Region},
        //                //new ExcelColumn<ReportRow> {Title = "Сопровождение", Func = r => r.CountAttendants},
        //                //new ExcelColumn<ReportRow> {Title = "Дети", Func = r => r.CountPlace},
        //                //new ExcelColumn<ReportRow> {Title = "Признак что коляска", Func = r => r.IsInvalid},
        //                //new ExcelColumn<ReportRow> {Title = "Очередь", Func = r => r.Queue},
        //                //new ExcelColumn<ReportRow> {Title = "Количество", Func = r => r.Quantity},
        //                //new ExcelColumn<ReportRow> {Title = "Вид номера", Func = r => r.CountAttendants?.ToString() +"+"+ r.CountPlace?.ToString()},
        //                //new ExcelColumn<ReportRow> {Title = "Статус", Func = r => r.Status}
        //            }, data)
        //            {
        //                TableName = $"Номерной фонд",
        //            };
        //            return res;
        //        }
        //        catch
        //        {

        //        }
        //        return null;
        //    }

        //}

        //public class NotRespondedRequestsReportRow
        //{
        //    public string Purpose { get; set; }
        //    public string Period { get; set; }
        //    public string Region { get; set; }
        //    public int? CountAttendants { get; set; }
        //    public int? CountPlace { get; set; }
        //    public string IsInvalid { get; set; }
        //    public int? Queue { get; set; }
        //    public int? Quantity { get; set; }
        //    public string Status { get; set; }

        ///// <summary>
        /////     ЕГИСО
        ///// </summary>
        //public static BaseExcelTable GetNotRespondedRequests(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
        //{
        //    var requsts = unitOfWork.GetSet<Request>().Where(r =>
        //            r.StatusId == StateMachineStateEnum.Request.CertificateIssued && !r.IsDeleted && !r.IsDraft)
        //        .AsQueryable();

        //    if (filter?.YearOfRestId.HasValue ?? false)
        //    {
        //        requsts = requsts.Where(r => r.YearOfRestId == filter.YearOfRestId.Value);
        //    }
        //    if (filter?.DateFormingBegin.HasValue ?? false)
        //    {
        //        requsts = requsts.Where(r => r.DateRequest >= filter.DateFormingBegin.Value);
        //    }
        //    if (filter?.DateFormingEnd.HasValue ?? false)
        //    {
        //        requsts = requsts.Where(r => r.DateRequest <= filter.DateFormingEnd.Value);
        //    }

        //    var rest = unitOfWork.GetSet<TypeOfRest>()
        //        .Where(ss => ss.Id == (long)TypeOfRestEnum.ChildRest || ss.ParentId == (long)TypeOfRestEnum.ChildRest ||
        //                     ss.Id == (long)TypeOfRestEnum.RestWithParents || ss.ParentId == (long)TypeOfRestEnum.RestWithParents ||
        //                     ss.Id == (long)TypeOfRestEnum.RestWithParentsInvalidOrphanComplex || ss.ParentId == (long)TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
        //                     ss.Id == (long)TypeOfRestEnum.ChildRestCamps || ss.ParentId == (long)TypeOfRestEnum.ChildRestCamps ||
        //                     ss.Id == (long)TypeOfRestEnum.TentChildrenCamp || ss.ParentId == (long)TypeOfRestEnum.TentChildrenCamp)
        //        .Select(ss => ss.Id).Distinct().ToList();

        //    var cert = unitOfWork.GetSet<TypeOfRest>()
        //        .Where(ss => ss.Id == (long)TypeOfRestEnum.Money || ss.ParentId == (long)TypeOfRestEnum.Money)
        //        .Select(ss => ss.Id).Distinct().ToList();

        //    var comps = new List<long>() { (long)TypeOfRestEnum.Compensation, (long)TypeOfRestEnum.CompensationYouthRest };

        //    var columns = new List<ExcelColumn<EGISORow>>
        //    {
        //        new ExcelColumn<EGISORow> {Title = "Номер заявления", Func = r => r.RequestNumber},
        //        new ExcelColumn<EGISORow> {Title = "Дата подачи заявления", Func = r => r.RequestDate},
        //        new ExcelColumn<EGISORow> {Title = "Статус заявления", Func = r => r.RequestStatus},
        //        new ExcelColumn<EGISORow> {Title = "Дата оказания услуги", Func = r => r.RequestDateIssured},
        //        new ExcelColumn<EGISORow> {Title = "Цель обращения", Func = r => r.TypeOfRest},
        //        new ExcelColumn<EGISORow> {Title = "Фамилия", Func = r => r.LastName},
        //        new ExcelColumn<EGISORow> {Title = "Имя", Func = r => r.FirstName},
        //        new ExcelColumn<EGISORow> {Title = "Отчество", Func = r => r.MiddleName},
        //        new ExcelColumn<EGISORow> {Title = "Пол", Func = r => r.Sex},
        //        new ExcelColumn<EGISORow> {Title = "Дата рождения", Func = r => r.DateOfBirth},
        //        new ExcelColumn<EGISORow> {Title = "СНИЛС", Func = r => r.SNILS},
        //        new ExcelColumn<EGISORow> {Title = "Документ удостоверяющий личность", Func = r => r.DocumentType},
        //        new ExcelColumn<EGISORow> {Title = "Серия", Func = r => r.DocumentSeria},
        //        new ExcelColumn<EGISORow> {Title = "Номер", Func = r => r.DocumentNumber},
        //        new ExcelColumn<EGISORow> {Title = "Дата выдачи", Func = r => r.DocumentDate},
        //        new ExcelColumn<EGISORow> {Title = "Кем выдан", Func = r => r.DocumentIssured},
        //        new ExcelColumn<EGISORow> {Title = "Вид ограничения", Func = r => r.TypeOfRestriction},
        //        new ExcelColumn<EGISORow> {Title = "Подвид ограничения", Func = r => r.TypeOfSubRestriction},
        //        new ExcelColumn<EGISORow> {Title = "Вид льготы", Func = r => r.BenefitType},
        //        new ExcelColumn<EGISORow> {Title = "Сумма (руб.)", Func = r => r.RequestSumm,  }
        //    };


        //    var results = new List<EGISORow>();
        //    foreach (var req in requsts.ToList())
        //    {
        //        if (req.Child?.Any() ?? false)
        //        {
        //            foreach (var c in req.Child)
        //            {
        //                var summ = string.Empty;
        //                if (comps.Contains(req.TypeOfRestId.Value))
        //                {
        //                    summ = c.AmountOfCompensation?.ToString("C");
        //                }
        //                else
        //                {
        //                    summ = unitOfWork.GetSet<AverageRestPrice>()
        //                        .Where(ss => ss.YearOfRestId == req.YearOfRestId && ss.TypeOfRestId == req.TypeOfRestId)
        //                        .Select(ss => ss.Price).FirstOrDefault().ToString("C");
        //                }

        //                results.Add(new EGISORow
        //                {
        //                    RequestNumber = req.RequestNumber,
        //                    RequestDate = req.DateRequest,
        //                    RequestStatus = req.Status.Name,
        //                    RequestDateIssured = req.DateChangeStatus,
        //                    TypeOfRest = req.TypeOfRest.Name,
        //                    FirstName = c.FirstName,
        //                    LastName = c.LastName,
        //                    MiddleName = c.MiddleName,
        //                    Sex = c.Male ? "Мужской" : "Женский",
        //                    DateOfBirth = c.DateOfBirth,
        //                    SNILS = c.Snils,
        //                    DocumentType = c.DocumentType.Name,
        //                    DocumentSeria = c.DocumentSeria,
        //                    DocumentNumber = c.DocumentNumber,
        //                    DocumentDate = c.DocumentDateOfIssue,
        //                    DocumentIssured = c.DocumentSubjectIssue,
        //                    TypeOfRestriction = c.TypeOfRestriction?.Name,
        //                    TypeOfSubRestriction = c.TypeOfSubRestriction?.Name,
        //                    BenefitType = c.BenefitType?.Name,
        //                    RequestSumm = summ
        //                });
        //            }
        //        }

        //        if (req.InformationVouchers?.Any() ?? false)
        //        {
        //            foreach (var iv in req.InformationVouchers)
        //            {
        //                foreach (var ap in iv.AttendantsPrice)
        //                {
        //                    if (req.ApplicantId == ap.ApplicantId)
        //                    {
        //                        var summ = string.Empty;
        //                        if (comps.Contains(req.TypeOfRestId.Value))
        //                        {
        //                            summ = ap.AmountOfCompensation?.ToString("C");
        //                        }

        //                        results.Add(new EGISORow
        //                        {
        //                            RequestNumber = req.RequestNumber,
        //                            RequestDate = req.DateRequest,
        //                            RequestStatus = req.Status.Name,
        //                            RequestDateIssured = req.DateChangeStatus,
        //                            TypeOfRest = req.TypeOfRest.Name,
        //                            FirstName = req.Applicant.FirstName,
        //                            LastName = req.Applicant.LastName,
        //                            MiddleName = req.Applicant.MiddleName,
        //                            Sex = (req.Applicant.Male ?? true) ? "Мужской" : "Женский",
        //                            DateOfBirth = req.Applicant.DateOfBirth,
        //                            SNILS = req.Applicant.Snils,
        //                            DocumentType = req.Applicant.DocumentType?.Name,
        //                            DocumentSeria = req.Applicant.DocumentSeria,
        //                            DocumentNumber = req.Applicant.DocumentNumber,
        //                            DocumentDate = req.Applicant.DocumentDateOfIssue,
        //                            DocumentIssured = req.Applicant.DocumentSubjectIssue,
        //                            BenefitType = req.Applicant.BenefitType?.Name,
        //                            RequestSumm = summ
        //                        });
        //                        break;
        //                    }
        //                }
        //            }
        //        }


        //        if (req.Attendant?.Any() ?? false)
        //        {
        //            foreach (var a in req.Attendant)
        //            {
        //                var summ = string.Empty;
        //                if (comps.Contains(req.TypeOfRestId.Value))
        //                {
        //                    summ = req.InformationVouchers.SelectMany(sx => sx.AttendantsPrice)
        //                        .Where(sx => sx.ApplicantId == a.Id).Select(sx => sx.AmountOfCompensation)
        //                        .FirstOrDefault()?.ToString("C");
        //                }

        //                results.Add(new EGISORow
        //                {
        //                    RequestNumber = req.RequestNumber,
        //                    RequestDate = req.DateRequest,
        //                    RequestStatus = req.Status.Name,
        //                    RequestDateIssured = req.DateChangeStatus,
        //                    TypeOfRest = req.TypeOfRest.Name,
        //                    FirstName = a.FirstName,
        //                    LastName = a.LastName,
        //                    MiddleName = a.MiddleName,
        //                    Sex = (a.Male ?? true) ? "Мужской" : "Женский",
        //                    DateOfBirth = a.DateOfBirth,
        //                    SNILS = a.Snils,
        //                    DocumentType = a.DocumentType?.Name,
        //                    DocumentSeria = a.DocumentSeria,
        //                    DocumentNumber = a.DocumentNumber,
        //                    DocumentDate = a.DocumentDateOfIssue,
        //                    DocumentIssured = a.DocumentSubjectIssue,
        //                    BenefitType = a.BenefitType?.Name,
        //                    RequestSumm = summ
        //                });
        //            }
        //        }
        //    }

        //    return new ExcelTable<EGISORow>(columns, results.OrderBy(ss => ss.RequestNumber));
        //}
    }
}
