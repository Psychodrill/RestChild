IF OBJECT_ID('tempdb..#tmp') IS NOT NULL
   DROP TABLE #tmp

CREATE TABLE #tmp (
	Id nvarchar(32),
	Name nvarchar(1000),
	Givz nvarchar(32)
);


BULK INSERT #tmp 
FROM 'D:\devel\topcase\RestChild\SQLScripts\BTI_bulk_insert\region.csv'
WITH
(
	FORMATFILE = 'D:\devel\topcase\RestChild\SQLScripts\BTI_bulk_insert\region.fmt',
    FIRSTROW = 2,
	MAXERRORS = 0, 
    TABLOCK
);

WITH CONVERTED AS
(
	SELECT 
		CAST(SUBSTRING(Id, 2, LEN(Id) - 2) AS BIGINT) "Id",
		SUBSTRING(Name, 2, LEN(Name) - 2) "Name",
		CAST(SUBSTRING(Id, 2, LEN(Id) - 2) AS BIGINT) / 100 "BtiDistrictId",
		CAST(SUBSTRING(Givz, 2, LEN(Givz) - 2) AS BIGINT) "Givz",
		'True' "IsVisible"
	FROM #tmp
)

INSERT INTO [dbo].[BtiRegion] (Id, Name, BtiDistrictId, Givz, IsVisible)
SELECT * FROM CONVERTED

DROP TABLE #tmp
update dbo.BtiRegion set Name = REPLACE(Name, '""', '"') 