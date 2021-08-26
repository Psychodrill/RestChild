begin tran

select Id into #TMP from dbo.Request where StatusId=8021 and IsFirstCompany=1 and IsDeleted=0

update dbo.Request set StatusId=1080, DeclineReasonId=201704, EidSendStatus=1 where Id in (select Id from #TMP) and StatusId = 8021

select * into #BT from dbo.Request where StatusId=1080 and DeclineReasonId=201704 and Id in (select Id from #TMP)

insert into dbo.HistoryRequest (Operation, OperationDate, RequestId, LastUpdateTick)
select 'Неучастие заявителя во втором этапе заявочной кампании в целях организации отдыха и оздоровления, предусматривающей необходимость выбора заявителем конкретной организации отдыха и оздоровления.' as Operation, getdate() as OperationDate, Id as RequestId, 0 as LastUpdateTick from #BT

insert into dbo.ExchangeUTS
([Message]
      ,[Processed]
      ,[QueueName]
      ,[Incoming]
      ,[FromOrgCode]
      ,[ToOrgCode]
      ,[MessageId]
      ,[ServiceNumber]
      ,[RequestId]
      ,[IsError]
      ,[DateCreate]
      ,[ErrorText]
      ,[ErrorDescription]
      ,[TypeOfRestId]
      ,[BookingGuid]
      ,[ToState]
      ,[IsErrorOnReleaseBooking]
      ,[LastUpdateTick]
      ,[Eid]
      ,[EidSendStatus]
      ,[EidSyncDate]
      ,[DateToSend])
select 
    '<?xml version="1.0" encoding="utf-16"?><StatusMessage xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://asguf.mos.ru/rkis_gu/coordinate/v5/"><ResponseDate>2017-05-03T00:00:00.6619867+03:00</ResponseDate>  <PlanDate xsi:nil="true" /><StatusCode>1080</StatusCode><Note>Неучастие заявителя во втором этапе заявочной кампании в целях организации отдыха и оздоровления, предусматривающей необходимость выбора заявителем конкретной организации отдыха и оздоровления.</Note><ServiceNumber>'+r.RequestNumber+'</ServiceNumber><ReasonCode>6</ReasonCode></StatusMessage>' as [Message]
    , 0 [Processed]
    , 'CAMPS.STATUS_OUT' [QueueName]
    , 0 [Incoming]
     , '2064' [FromOrgCode]
      ,'200902' [ToOrgCode]
      ,null [MessageId]
      ,RequestNumber [ServiceNumber]
      ,Id as [RequestId]
      ,0 [IsError]
      ,getdate() [DateCreate]
      ,null [ErrorText]
      ,null [ErrorDescription]
      ,[TypeOfRestId]
      ,null [BookingGuid]
      ,1080 [ToState]
      ,0 [IsErrorOnReleaseBooking]
      ,0 [LastUpdateTick]
      ,0 [Eid]
      ,1 [EidSendStatus]
      ,null [EidSyncDate]
      ,null [DateToSend]
from dbo.Request r where Id in (select Id from #BT)

commit tran
