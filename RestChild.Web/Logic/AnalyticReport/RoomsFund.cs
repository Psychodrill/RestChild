using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers;

namespace RestChild.Web.Logic.AnalyticReport
{
    /// <summary>
    ///     Логика отчета по номерному фонду
    /// </summary>
	public static class RoomsFund
	{
        /// <summary>
        ///     Сформировать отчет по номерному фонду
        /// </summary>
        public static BaseExcelTable GetRoomsFund(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
        {
            var year = unitOfWork.GetSet<YearOfRest>().Where(y => y.Id == filter.YearOfRestId).FirstOrDefault().Year;

            var yearOfRestIds = string.Empty;

            long? typeOfRestId = filter.TypeOfRestId.GetValueOrDefault(0);

            string statuses = filter.StatusIds.IfEmptyValue(null);

            if (filter.NextYearsIncluded)
            {
                var yors = unitOfWork.GetSet<YearOfRest>().Where(y => y.Year >= year).OrderBy(x => x.Id);
                yearOfRestIds = string.Join(",", yors.Select(y => y.Id).ToList());
            }
            else
            {
                yearOfRestIds = filter.YearOfRestId.ToString();
            }

            var query = $@"

            IF OBJECT_ID('tempdb..#B09DFB1D_TMP') IS NOT NULL DROP TABLE #B09DFB1D_TMP
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP2') IS NOT NULL DROP TABLE #B09DFB1D_TMP2
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP3') IS NOT NULL DROP TABLE #B09DFB1D_TMP3
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP4') IS NOT NULL DROP TABLE #B09DFB1D_TMP4

            select c.ChildUniqeId, count(*) as cnt into #B09DFB1D_TMP
            from dbo.Child c
            inner join dbo.Request r on r.Id = c.RequestId
            where r.YearOfRestId  in ({yearOfRestIds}) and r.TypeOfRestId not in (7,11)
            group by c.ChildUniqeId


            select a.RelativeUniqeId, count(*) as cnt into #B09DFB1D_TMP2
            from dbo.Applicant a
            inner join dbo.Request r on r.ApplicantId = a.Id
            where r.TypeOfRestId in (13,14)	and r.YearOfRestId  in ({yearOfRestIds})
            group by a.RelativeUniqeId


            select
	            r.Id, tr.R into #B09DFB1D_TMP3
            from
            dbo.Request r
            inner join dbo.Status s on s.Id = r.StatusId
            inner join dbo.TypeOfRest t on t.Id = r.TypeOfRestId
            left join (
	            select
		            r.Id, case when min(t.cnt) = 1 then 1
				            when min(t.cnt)=2 then 2 else 3 end as R
	            from dbo.Applicant a
	            inner join dbo.Request r on r.ApplicantId = a.Id
	            inner join #B09DFB1D_TMP2 t on t.RelativeUniqeId = a.RelativeUniqeId
	            group by r.Id
	            union all
	            select
		            a.RequestId, case
						            when min(t.cnt) = 1 or min(case when a.BenefitTypeId in (38,39,40) then 1 else 2 end)=1 then 1
						            when min(t.cnt) = 2 then 2 else 3 end as R
	            from dbo.Child a
	            inner join #B09DFB1D_TMP t on t.ChildUniqeId = a.ChildUniqeId
	            where a.RequestId is not null
	            group by a.RequestId
            ) tr on tr.Id = r.Id
            where r.IsDeleted=0 AND (r.StatusId >0 AND r.StatusId not in (1030,1046,7704, 1058, 1080, 1090))

            SELECT ParentId, Id INTO #typeOfRestIds
			from TypeOfRest 
			WHERE Id ={typeOfRestId} OR ParentId ={typeOfRestId} OR ParentId IN (SELECT  Id FROM [RestChildAiso].[dbo].[TypeOfRest] WHERE ParentId ={typeOfRestId}) 

            select c.RequestId,
            min(isnull(tr.RestrictionGroupId,3)) as RestrictionGroupId into #B09DFB1D_TMP4
            from
            dbo.Child c
            left join dbo.TypeOfSubRestriction tr on tr.Id=c.TypeOfSubRestrictionId
            group by c.RequestId

            select tr.Name as Purpose, right('00' + cast(t.Month as varchar),2) + '.' + right('00' + cast(t.[DayOfMonth] as varchar),2) + '-' + t.Name as Period, p.Name as Region, case when tr.Id=14 then 1 else r.CountAttendants end CountAttendants, r.CountPlace,
            rg.Name as IsInvalid, t3.R as Queue, count(*) as Quantity, st.Name as Status
            from
            dbo.Request r
            inner join #B09DFB1D_TMP3 t3 on t3.Id = r.Id
            left join #B09DFB1D_TMP4 i on i.RequestId = r.Id
            left join dbo.RestrictionGroup rg on rg.Id= isnull(i.RestrictionGroupId,3)
            left join dbo.TimeOfRest t on t.Id=r.TimeOfRestId
            left join dbo.PlaceOfRest p on p.Id=r.PlaceOfRestId
            left join dbo.TypeOfRest tr on tr.Id=r.TypeOfRestId
            inner join dbo.Status st on st.Id=r.StatusId
            where r.IsDeleted=0
            and r.YearOfRestId IN ({yearOfRestIds})
            and r.IsFirstCompany=1
            --and (r.[CountAttendants]>0 or tr.Id=14)
            and (r.TypeOfRestId in (SELECT Id FROM #typeOfRestIds) or {typeOfRestId} =0)
            and (r.StatusId IN ({statuses??"0"}) OR 0 IN ({statuses ?? "0"}))
            group by tr.Name,t.Month, t.[DayOfMonth], t.Name, p.Name, case when tr.Id=14 then 1 else r.CountAttendants end, r.CountPlace, rg.Name, t3.R, st.Name
            order by tr.Name, t.Month, t.[DayOfMonth], t3.R

            DROP TABLE #typeOfRestIds
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP') IS NOT NULL DROP TABLE #B09DFB1D_TMP
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP2') IS NOT NULL DROP TABLE #B09DFB1D_TMP2
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP3') IS NOT NULL DROP TABLE #B09DFB1D_TMP3
            IF OBJECT_ID('tempdb..#B09DFB1D_TMP4') IS NOT NULL DROP TABLE #B09DFB1D_TMP4

            ";

            try
            {
                var data = unitOfWork.Database.SqlQuery<ReportRow>(query);
                var res = new ExcelTable<ReportRow>(new List<ExcelColumn<ReportRow>>
                {
                    new ExcelColumn<ReportRow> {Title = "Цель обращения", Func = r => r.Purpose},
                    new ExcelColumn<ReportRow> {Title = "Период", Func = r => r.Period},
                    new ExcelColumn<ReportRow> {Title = "Регион", Func = r => r.Region},
                    new ExcelColumn<ReportRow> {Title = "Сопровождение", Func = r => r.CountAttendants},
                    new ExcelColumn<ReportRow> {Title = "Дети", Func = r => r.CountPlace},
                    new ExcelColumn<ReportRow> {Title = "Признак что коляска", Func = r => r.IsInvalid},
                    new ExcelColumn<ReportRow> {Title = "Очередь", Func = r => r.Queue},
                    new ExcelColumn<ReportRow> {Title = "Количество", Func = r => r.Quantity},
                    new ExcelColumn<ReportRow> {Title = "Вид номера", Func = r => r.CountAttendants?.ToString() +"+"+ r.CountPlace?.ToString()},
                    new ExcelColumn<ReportRow> {Title = "Статус", Func = r => r.Status}
                }, data)
                {
                    TableName = $"Номерной фонд",
                };
                return res;
            }
            catch(Exception ex)
            {

            }
            return null;
        }
    }

    /// <summary>
    ///     Модель для строки отчета
    /// </summary>
    public class ReportRow
    {
        public string Purpose { get; set; }
        public string Period { get; set; }
        public string Region { get; set; }
        public int? CountAttendants { get; set; }
        public int? CountPlace { get; set; }
        public string IsInvalid { get; set; }
        public int? Queue { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }
    }
}
