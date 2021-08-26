select a.Email, r.RequestNumber, r.RequestNumberMpgu into #TMP
from dbo.Request r
inner join dbo.Applicant a on a.Id=r.ApplicantId
where r.StatusId=1050 and r.IsFirstCompany=1 and not exists(select 1 from dbo.ExchangeBaseRegistry e where e.IsProcessed=0 and e.ChildId in (select Id from dbo.Child c where c.RequestId=r.Id)) and r.[NeedSendForBenefit]=0
and a.Email is not null

INSERT INTO [dbo].[SendEmailAndSms] ([Email], [EmailMessage],[EmailTitle],[IsEmailSended],[IsSmsSended],[DateCreate],[LastUpdateTick],[Eid]) 
select Email,
  'Добрый день!<br/><br/>Ваше заявление на отдых и оздоровление включено в список для рассмотрения комиссией. Ожидайте дополнительную информацию о результатах рассмотрения вашего заявления. <br/>Информацию о возможности участия во втором этапе заявочной кампании будет предоставлена Вам не позднее 10 апреля. <br/>Следите за обновлениями в личном кабинете на сайте mos.ru и на своей электронной почте.<br/>Номер(а) заявки(ок):<br/>' + 
  replace(replace((select RequestNumber + case when RequestNumberMpgu is not null then '(номер ПГУ:' + RequestNumberMpgu + ')' else '' end + 'br/' as 'data()' from #TMP r where r.[Email] = t.[Email] for xml path('')),'(номер',' (номер'),'br/','<br/>')  
  , 'ЛОК 2017',0,1,getdate(),0,0
from #TMP t
group by Email


---------------------------------------

select a.Email, r.RequestNumber, r.RequestNumberMpgu into #TMP
from dbo.Request r
inner join dbo.Applicant a on a.Id=r.ApplicantId
where r.StatusId=1050 and r.IsFirstCompany=1 and not exists(select 1 from dbo.ExchangeBaseRegistry e where e.IsProcessed=0 and e.ChildId in (select Id from dbo.Child c where c.RequestId=r.Id)) and r.[NeedSendForBenefit]=0
and a.Email is not null

delete from #TMP where RequestNumber in (
select r.RequestNumber
from dbo.Request r
inner join dbo.Applicant a on a.Id=r.ApplicantId
where r.StatusId=1050 and r.IsFirstCompany=1 and not exists(select 1 from dbo.ExchangeBaseRegistry e where e.IsProcessed=0 and e.ChildId in (select Id from dbo.Child c where c.RequestId=r.Id)) and r.[NeedSendForBenefit]=0
and a.Email is not null and r.IsDeleted=0
)

select Email,
  replace(replace((select RequestNumber + case when RequestNumberMpgu is not null then '(номер ПГУ:' + RequestNumberMpgu + ')' else '' end + 'br/' as 'data()' from #TMP r where r.[Email] = t.[Email] for xml path('')),'(номер',' (номер'),'br/','; ')  
from #TMP t
group by Email
