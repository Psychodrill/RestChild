
update dbo.Bout set TransportInfoFromId = t.Id
from (
select ti.Id, min(lp.BoutId) as BoutId 
from dbo.TransportInfo ti
inner join dbo.LinkToPeople lp on lp.TransportId = ti.Id
where ti.Id not in (select b.TransportInfoFromId from dbo.Bout b where b.TransportInfoFromId is not null)
and ti.Id not in (select b.TransportInfoToId from dbo.Bout b where b.TransportInfoToId is not null)
and ti.DepartureId = 0
group by ti.Id
) t
where t.BoutId = dbo.Bout.Id

update dbo.Bout set TransportInfoToId = t.Id
from (
select ti.Id, min(lp.BoutId) as BoutId 
from dbo.TransportInfo ti
inner join dbo.LinkToPeople lp on lp.TransportId = ti.Id
where ti.Id not in (select b.TransportInfoFromId from dbo.Bout b where b.TransportInfoFromId is not null)
and ti.Id not in (select b.TransportInfoToId from dbo.Bout b where b.TransportInfoToId is not null)
and ti.ArrivalId = 0
group by ti.Id
) t
where t.BoutId = dbo.Bout.Id

delete from dbo.TransportInfo 
where Id not in (select b.TransportInfoFromId from dbo.Bout b where b.TransportInfoFromId is not null)
and Id not in (select b.TransportInfoToId from dbo.Bout b where b.TransportInfoToId is not null)
