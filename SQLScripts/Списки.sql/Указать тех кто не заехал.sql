select distinct 'update dbo.Child set [TypeViolationId]=3 where Id='  + cast(Eid as varchar) from dbo.Child where Id in (
select ChildId from [dbo].[LinkToPeople]
where NotComeInPlaceOfRest=1 and BoutId in (select Id from dbo.Bout where YearOfRestId=3) 
and ChildId is not null
) and Eid is not null and Id in (
select ChildId from [dbo].[LinkToPeople]
where NotComeInPlaceOfRest=1 and BoutId in (select Id from dbo.Bout where YearOfRestId=3) and TransportId is not null
and ChildId is not null
)


select distinct 'update dbo.Applicant set [TypeViolationId]=null where TypeViolationId = 3 and Id='  + cast(Eid as varchar) from dbo.Applicant where Id in (
select ApplicantId from [dbo].[LinkToPeople]
where NotComeInPlaceOfRest=1 and BoutId in (select Id from dbo.Bout where YearOfRestId=3) 
and ApplicantId is not null 
) and Eid is not null
and Id not in (
select ApplicantId from [dbo].[LinkToPeople]
where NotComeInPlaceOfRest=1 and BoutId in (select Id from dbo.Bout where YearOfRestId=3) 
and ApplicantId is not null and TransportId is not null
)


select * from dbo.Applicant where Id in (
select ApplicantId from [dbo].[LinkToPeople]
where NotComeInPlaceOfRest=1 and BoutId in (select Id from dbo.Bout where YearOfRestId=3) 
and ApplicantId is not null 
) and Eid is not null and Eid=278345


select * from dbo.LinkToPeople where ApplicantId=278345


select * from dbo.Applicant where Eid=297499
select * from dbo.LinkToPeople where ApplicantId=297499