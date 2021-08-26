IF OBJECT_ID('tempdb..#tmp') IS NOT NULL
   DROP TABLE #tmp

CREATE TABLE #tmp (
	Id nvarchar(32),
	Name nvarchar(1000),
	Givz nvarchar(32),
);


BULK INSERT #tmp 
FROM 'D:\devel\topcase\RestChild\SQLScripts\BTI_bulk_insert\district.csv'
WITH
(
	FORMATFILE = 'D:\devel\topcase\RestChild\SQLScripts\BTI_bulk_insert\district.fmt',
    FIRSTROW = 2,
	MAXERRORS = 0, 
    TABLOCK
);


WITH CONVERTED AS
(
	SELECT 
		CAST(SUBSTRING(Id, 2, LEN(Id) - 2) AS BIGINT) "Id",
		SUBSTRING(Name, 2, LEN(Name) - 2) "Name",
		CAST(SUBSTRING(Givz, 2, LEN(Givz) - 2) AS BIGINT) "Givz",
		'True' "IsVisible"
	FROM #tmp
)

INSERT INTO [dbo].[BtiDistrict] (Id, Name, Givz, IsVisible)
SELECT * FROM CONVERTED

DROP TABLE #tmp
