-- УДАЛЕНИЕ ВРЕМЕННЫХ ТАБЛИЦ для нескольких прогонов
--drop table #TMPHOTELS
--drop table #TMPTOUR
--drop table #TMPREQUEST
--drop table #TMPCHILD
--drop table #HISTORYTOUR
--drop table #TMPAPPLICANT

begin tran
select Id into #TMPHOTELS from dbo.Hotels where Id in (310,309,308) --- ИД лагерей для удаления
select Id into #TMPTOUR from dbo.Tour where HotelsId in (select Id from #TMPHOTELS) or ForMultipleStageCompany=1 --- удаление заездов на многоэтапную кампанию или по ИД лагерей
select HistoryLinkId as Id into #HISTORYTOUR from dbo.Tour where Id in (select Id from #TMPTOUR) and HistoryLinkId is not null
select Id into #TMPREQUEST from dbo.Request where IsFirstCompany=1 or TourId in (select Id from #TMPTOUR) --- Удаление заявок на тестовые размещения или на многоэтапную кампанию
select Id into #TMPCHILD from dbo.Child where RequestId in (select Id from #TMPREQUEST)
select ApplicantId as Id into #TMPAPPLICANT from dbo.Request where Id in (select Id from dbo.#TMPREQUEST) and ApplicantId is not null

delete from dbo.ExchangeUTS where RequestId in (select Id from #TMPREQUEST)
delete from dbo.Booking where RequestId in (select Id from #TMPREQUEST)
delete from dbo.RequestTimeOfRest where Request_Id in (select Id from #TMPREQUEST)
delete from dbo.ExchangeBaseRegistry where ChildId in (select Id from #TMPCHILD)
delete from dbo.Child where RequestId in (select Id from #TMPREQUEST)
delete from dbo.Applicant where RequestId in (select Id from #TMPREQUEST)
delete from dbo.HistoryRequest where RequestId in (select Id from #TMPREQUEST)
delete from dbo.RequestFile where RequestId  in (select Id from #TMPREQUEST)
delete from dbo.Request where Id  in (select Id from #TMPREQUEST)
delete from dbo.Applicant where Id in (select Id from #TMPAPPLICANT)

delete from dbo.TourVolume where TourId in (select Id from #TMPTOUR)
delete from dbo.Tour where Id in (select Id from #TMPTOUR)
delete from dbo.History where [LinkId] in (select Id from #HISTORYTOUR)
delete from dbo.HistoryLink where Id in (select Id from #HISTORYTOUR)

delete from dbo.FileHotel where HotelId in (select Id from #TMPHOTELS)
delete from dbo.TypeOfRooms where HotelId in (select Id from #TMPHOTELS)
delete from dbo.HotelContactPerson where HotelId in (select Id from #TMPHOTELS)
delete from dbo.Hotels where Id in (select Id from #TMPHOTELS)

--- Запуск скрипта до сюда.
--- При выполнении не должно быть ошибок. 
--- ЕСЛИ ЕСТЬ ОШИБКИ ТО 
--- rollback tran
--- ЕСЛИ ВСЕ ОК ТО 
--- commit tran 
