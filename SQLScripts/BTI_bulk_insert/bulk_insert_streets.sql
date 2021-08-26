IF OBJECT_ID('tempdb..#tmp') IS NOT NULL
   DROP TABLE #tmp

CREATE TABLE #tmp (
	Id nvarchar(32),
	Name nvarchar(1000),
	Name2 nvarchar(1000),
);


BULK INSERT #tmp 
FROM 'E:\Sql\street.csv'
WITH
(
	FORMATFILE = 'E:\Sql\street.fmt',
    FIRSTROW = 2,
	MAXERRORS = 0, 
    TABLOCK
);

WITH CONVERTED AS
(
	SELECT 
		CAST(SUBSTRING(Id, 2, LEN(Id) - 2) AS BIGINT) "Id",
		replace(SUBSTRING(Name, 2, LEN(Name) - 2),'""','"') "Name",
		replace(SUBSTRING(Name2, 2, LEN(Name2) - 2),'""','"') "Name2"
	FROM #tmp
)

update [dbo].[BtiStreet] set Name = N
from (SELECT Id i, case when isnull(Name,'')= '' then Name2 else Name end N FROM CONVERTED) t
where t.i = Id

DROP TABLE #tmp

