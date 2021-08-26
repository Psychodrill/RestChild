IF OBJECT_ID('tempdb..#tmp') IS NOT NULL
   DROP TABLE #tmp

CREATE TABLE #tmp (
	Id nvarchar(32),
	Name nvarchar(1000),
);


BULK INSERT #tmp 
FROM 'D:\devel\topcase\RestChild\SQLScripts\Schools_bulk_insert\schools.csv'
WITH
(
	FORMATFILE = 'D:\devel\topcase\RestChild\SQLScripts\Schools_bulk_insert\schools.fmt',
    FIRSTROW = 2,
	MAXERRORS = 0, 
    TABLOCK
);

WITH CONVERTED AS
(
	SELECT 
		CAST(SUBSTRING(Id, 2, LEN(Id) - 2) AS BIGINT) "Id",
		SUBSTRING(Name, 2, LEN(Name) - 2) "Name"
	FROM #tmp
)

INSERT INTO [dbo].[School] (Id, Name)
SELECT * FROM CONVERTED

DROP TABLE #tmp