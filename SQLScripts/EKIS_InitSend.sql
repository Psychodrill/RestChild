update Hotels set EkisNeedSend = 1 where StateId<>-1
update Tour set EkisNeedSend = 1 where StateId<>-1 and (TypeOfRestId >=0 or TypeOfRestId=-1)

update Child set EkisNeedSend = 1 where IsDeleted = 0 and RequestId in (select Id from Request where StatusId in (1075) and IsDeleted=0)
update Child set EkisNeedSend = 1 where IsDeleted = 0 and ChildListId in (select Id from ListOfChilds where StateId in (2,3,4) and IsDeleted=0
and LimitOnOrganizationId in (Select Id from LimitOnOrganization where StateId in (11) and IsDeleted = 0))