begin tran 

--- Иванов
insert into [dbo].[CertificateToApply]
([CertificateKey], [ByDefault], [NotificationType], [AccountId], [LastUpdateTick])
select '27 6b a7 dc 93 ab 75 19 56 c5 da 40 74 7c bd be b1 21 fd 1d' as [CertificateKey], 0 [ByDefault], nd as [NotificationType]
 , (select Id from dbo.Account where Login='IIvanov@mosgortur.ru') as AccountId, 0 as LastUpdateTick from
(
select 'SaveCertificateToRequest' as nd union all
select 'NotificationRegistration' as nd union all
select 'NotificationRefuse' as nd union all
select 'NotificationWaitApplicant' as nd union all
select 'NotificationOfService' as nd union all
select 'NotificationOfNeedToChoose' as nd union all
select 'NotificationAboutDecision' as nd
) t 

insert into [dbo].[CertificateToApplyAccount]
([ForExcept], [ForSystemAccount], [AccountId], [CertificateToApplyId], [LastUpdateTick])
select 0 as [ForExcept], 1 [ForSystemAccount], null [AccountId], c.Id [CertificateToApplyId], 0 [LastUpdateTick]
from [dbo].[CertificateToApply] c

insert into [dbo].[CertificateToApplyAccount]
([ForExcept], [ForSystemAccount], [AccountId], [CertificateToApplyId], [LastUpdateTick])
select 0 as [ForExcept], 0 [ForSystemAccount], a.Id [AccountId], c.Id [CertificateToApplyId], 0 [LastUpdateTick]
from dbo.Account a 
inner join [dbo].[CertificateToApply] c on 1=1
where Login in ('BKrasnov@mosgortur.ru',
'EBuhareva@mosgortur.ru',
'RLukinov@mosgortur.ru',
'tfilatova@mosgortur.ru',
'IIvanov@mosgortur.ru')

--- Галкина
insert into [dbo].[CertificateToApply]
([CertificateKey], [ByDefault], [NotificationType], [AccountId], [LastUpdateTick])
select '5c 79 ad 17 65 86 62 76 8a 8d fa f8 20 d3 3e cc f2 09 8e e6' as [CertificateKey], 1 [ByDefault], nd as [NotificationType]
 , (select Id from dbo.Account where Login='Kgalkina@mosgortur.ru')as AccountId, 0 as LastUpdateTick from
(
select 'SaveCertificateToRequest' as nd union all
select 'NotificationRegistration' as nd union all
select 'NotificationRefuse' as nd union all
select 'NotificationWaitApplicant' as nd union all
select 'NotificationOfService' as nd union all
select 'NotificationOfNeedToChoose' as nd union all
select 'NotificationAboutDecision' as nd
) t

commit tran