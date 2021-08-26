-- УДАЛЕНИЕ ВРЕМЕННЫХ ТАБЛИЦ для нескольких прогонов
--drop table #TMPHOTELS
--drop table #TMPTOUR
--drop table #TMPREQUEST
--drop table #TMPCHILD
--drop table #HISTORYTOUR
--drop table #TMPAPPLICANT

--- 

begin tran
select Id into #TMPTOUR from dbo.Tour where (ForMultipleStageCompany=1) and YearOfRestId=7--- удаление заездов на многоэтапную кампанию или по ИД лагерей, ограничение по году кампании (5 - 2018 год)
select HistoryLinkId as Id into #HISTORYTOUR from dbo.Tour where Id in (select Id from #TMPTOUR) and HistoryLinkId is not null
select Id into #TMPREQUEST from dbo.Request where (IsFirstCompany=1 or TourId in (select Id from #TMPTOUR)) and YearOfRestId=7 --- Удаление заявок на тестовые размещения или на многоэтапную кампанию, ограничение по году кампании (5 - 2018 год)
select Id into #TMPCHILD from dbo.Child where RequestId in (select Id from #TMPREQUEST)
select ApplicantId as Id into #TMPAPPLICANT from dbo.Request where Id in (select Id from dbo.#TMPREQUEST) and ApplicantId is not null
select Id as Id into #TMPAPPLICANT2 from dbo.Applicant where RequestId in (select Id from dbo.#TMPREQUEST) 
select Id as Id into #LTR from [dbo].[ListTravelersRequest] where RequestId in (select Id from dbo.#TMPREQUEST) 

select * into [BackupDb].dbo.ExchangeUTS20191101 from dbo.ExchangeUTS where RequestId in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.Booking20191101  from dbo.Booking where RequestId in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.RequestTimeOfRest20191101  from dbo.RequestTimeOfRest where Request_Id in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.ExchangeBaseRegistry20191101_0  from dbo.ExchangeBaseRegistry where ChildId in (select Id from #TMPCHILD)
select * into [BackupDb].dbo.ExchangeBaseRegistry20191101_1  from dbo.ExchangeBaseRegistry where ApplicantId in (select Id from #TMPAPPLICANT)
select * into [BackupDb].dbo.ExchangeBaseRegistry20191101_2  from dbo.ExchangeBaseRegistry where ApplicantId in (select Id from #TMPAPPLICANT2)
select * into [BackupDb].dbo.[ListTravelersRequestDetail20191101_0]  from dbo.[ListTravelersRequestDetail] where ChildId in (select Id from #TMPCHILD) or [ListTravelersRequestId] in (select Id from #LTR)
select * into [BackupDb].dbo.[ListTravelersRequestDetail20191101_1]  from dbo.[ListTravelersRequestDetail] where ApplicantId in (select Id from #TMPAPPLICANT)
select * into [BackupDb].dbo.[ListTravelersRequestDetail20191101_2]  from dbo.[ListTravelersRequestDetail] where ApplicantId in (select Id from #TMPAPPLICANT2)
select * into [BackupDb].dbo.[ListTravelersRequestDetail20191101_3]  from [dbo].[ListTravelersRequest] where Id in (select Id from #LTR)
select * into [BackupDb].dbo.Child20191101  from dbo.Child where RequestId in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.Applicant20191101_0  from dbo.Applicant where RequestId in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.HistoryRequest20191101  from dbo.HistoryRequest where RequestId in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.RequestFile20191101  from dbo.RequestFile where RequestId  in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.Request20191101  from dbo.Request where Id  in (select Id from #TMPREQUEST)
select * into [BackupDb].dbo.Applicant20191101_1  from dbo.Applicant where Id in (select Id from #TMPAPPLICANT)

select * into [BackupDb].dbo.TourVolume20191101  from dbo.TourVolume where TourId in (select Id from #TMPTOUR)
select * into [BackupDb].dbo.Tour20191101  from dbo.Tour where Id in (select Id from #TMPTOUR)
select * into [BackupDb].dbo.History20191101  from dbo.History where [LinkId] in (select Id from #HISTORYTOUR)
select * into [BackupDb].dbo.HistoryLink20191101  from dbo.HistoryLink where Id in (select Id from #HISTORYTOUR)

delete from [dbo].[ChildUniqeRelativeUniqe] where [ChildUniqe_Id] in (select Id from [dbo].[ChildUniqe] where [LastInfoId] in (select Id from [BackupDb].dbo.Child20191101))
delete from [dbo].[RelativeUniqeApplicant] where [ApplicantId] in (select Id from [BackupDb].dbo.Applicant20191101_1) or ([ApplicantId] in (select Id from [BackupDb].dbo.Applicant20191101_0) or [ApplicantId] in (select Id from [BackupDb].dbo.Applicant20191101_1))

delete from dbo.ExchangeUTS where RequestId in (select Id from #TMPREQUEST) and Id in (select Id from [BackupDb].dbo.ExchangeUTS20191101)
delete from dbo.Booking where RequestId in (select Id from #TMPREQUEST) and Id in (select Id from [BackupDb].dbo.Booking20191101)
delete from dbo.RequestTimeOfRest where Request_Id in (select Id from #TMPREQUEST) 
delete from dbo.ExchangeBaseRegistry where ChildId in (select Id from #TMPCHILD) and Id in (select Id from [BackupDb].dbo.ExchangeBaseRegistry20191101_0)
delete from dbo.ExchangeBaseRegistry where ApplicantId in (select Id from #TMPAPPLICANT) and Id in (select Id from [BackupDb].dbo.ExchangeBaseRegistry20191101_1)
delete from dbo.ExchangeBaseRegistry where ApplicantId in (select Id from #TMPAPPLICANT2) and Id in (select Id from [BackupDb].dbo.ExchangeBaseRegistry20191101_2)
delete from dbo.[ListTravelersRequestDetail] where ChildId in (select Id from #TMPCHILD) or [ListTravelersRequestId] in (select Id from #LTR)
delete from dbo.[ListTravelersRequestDetail] where ApplicantId in (select Id from #TMPAPPLICANT)
delete from dbo.[ListTravelersRequestDetail] where ApplicantId in (select Id from #TMPAPPLICANT2)
delete from [dbo].[ListTravelersRequest] where Id in (select Id from #LTR)

update dbo.Child set [ChildUniqeId] = null where RequestId in (select Id from #TMPREQUEST) and Id in (select Id from [BackupDb].dbo.Child20191101)
delete from [dbo].[ChildUniqe] where [LastInfoId] in (select Id from [BackupDb].dbo.Child20191101) and Id not in (select [ChildUniqeId] from dbo.Child where [ChildUniqeId] is not null)
update [dbo].[ChildUniqe] set LastInfoId=null where [LastInfoId] in (select Id from [BackupDb].dbo.Child20191101)
delete from dbo.Child where RequestId in (select Id from #TMPREQUEST) and Id in (select Id from [BackupDb].dbo.Child20191101)
update dbo.Applicant set [RelativeUniqeId] = null where RequestId in (select Id from #TMPREQUEST) and (Id in (select Id from [BackupDb].dbo.Applicant20191101_0) or Id in (select Id from [BackupDb].dbo.Applicant20191101_1))
delete from [dbo].[RelativeUniqe] where LastInfoId in (select Id from dbo.Applicant where RequestId in (select Id from #TMPREQUEST) and (Id in (select Id from [BackupDb].dbo.Applicant20191101_0) or Id in (select Id from [BackupDb].dbo.Applicant20191101_1)))
		and Id not in (select [RelativeUniqeId] from dbo.Applicant where [RelativeUniqeId] is not null)
update [dbo].[RelativeUniqe] set LastInfoId=null where [LastInfoId] in (select Id from dbo.Applicant where RequestId in (select Id from #TMPREQUEST) and (Id in (select Id from [BackupDb].dbo.Applicant20191101_0) or Id in (select Id from [BackupDb].dbo.Applicant20191101_1)))
delete from dbo.Applicant where RequestId in (select Id from #TMPREQUEST) and (Id in (select Id from [BackupDb].dbo.Applicant20191101_0) or Id in (select Id from [BackupDb].dbo.Applicant20191101_1))
delete from dbo.HistoryRequest where RequestId in (select Id from #TMPREQUEST)
delete from dbo.RequestFile where RequestId  in (select Id from #TMPREQUEST)
delete from dbo.Request where Id  in (select Id from #TMPREQUEST) and Id in (select Id from [BackupDb].dbo.Request20191101 )
update dbo.Applicant set [RelativeUniqeId] = null where Id in (select Id from #TMPAPPLICANT) and Id in (select Id from [BackupDb].dbo.Applicant20191101_1)
delete from [dbo].[RelativeUniqe] where (LastInfoId in (select Id from [BackupDb].dbo.Applicant20191101_1) or LastInfoId in (select Id from [BackupDb].dbo.Applicant20191101_0))
	and Id not in (select [RelativeUniqeId] from dbo.Applicant where [RelativeUniqeId] is not null)
update [dbo].[RelativeUniqe] set LastInfoId=null where [LastInfoId] in (select Id from [BackupDb].dbo.Applicant20191101_1)
delete from dbo.Applicant where Id in (select Id from #TMPAPPLICANT) and Id in (select Id from [BackupDb].dbo.Applicant20191101_1)

delete from dbo.TourVolume where TourId in (select Id from #TMPTOUR) and Id in (select Id from [BackupDb].dbo.TourVolume20191101)
delete from dbo.Tour where Id in (select Id from #TMPTOUR) and Id in (select Id from [BackupDb].dbo.Tour20191101)
delete from dbo.History where [LinkId] in (select Id from #HISTORYTOUR) and Id in (select Id from [BackupDb].dbo.History20191101)
delete from dbo.HistoryLink where Id in (select Id from #HISTORYTOUR) and Id in (select Id from [BackupDb].dbo.HistoryLink20191101)

update dbo.YearOfRest set ReceptionOfApplicationsCompleted=0, ListComplited=0, TourOpened=0 where Id=7

--- Запуск скрипта до сюда.
--- При выполнении не должно быть ошибок. 
--- ЕСЛИ ЕСТЬ ОШИБКИ ТО 
--- rollback tran
--- ЕСЛИ ВСЕ ОК ТО 
--- commit tran 

