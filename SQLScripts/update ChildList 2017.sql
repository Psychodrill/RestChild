update dbo.LimitOnVedomstvo set TypeOfLimitListId=2, EidSendStatus=1 where YearOfRestId=4 and TypeOfLimitListId is null
update dbo.LimitOnVedomstvo set TypeOfLimitListId=1, EidSendStatus=1 where TypeOfLimitListId is null
update dbo.LimitOnOrganization set TypeOfLimitListId = 2, EidSendStatus=1 where LimitOnVedomstvoId in (select Id from LimitOnVedomstvo where TypeOfLimitListId = 2) and TypeOfLimitListId is null
update dbo.LimitOnOrganization set TypeOfLimitListId = 1, EidSendStatus=1 where TypeOfLimitListId is null
update dbo.ListOfChilds set TypeOfLimitListId = 2, EidSendStatus=1 where LimitOnOrganizationId in (select Id from dbo.LimitOnOrganization where TypeOfLimitListId = 2) and TypeOfLimitListId is null
update dbo.ListOfChilds set TypeOfLimitListId = 1, EidSendStatus=1 where TypeOfLimitListId is null
update dbo.Child set Payed=1, EidSendStatus=1 where ChildListId in (select Id from dbo.ListOfChilds where TypeOfLimitListId=2)