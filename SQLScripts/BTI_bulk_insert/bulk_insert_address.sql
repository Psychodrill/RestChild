IF OBJECT_ID('tempdb..#tmp') IS NOT NULL
   DROP TABLE #tmp

CREATE TABLE #tmp (
	Id nvarchar(32),
	FullAddress nvarchar(1000),
	Unom nvarchar(32),
	Dom nvarchar(32),
	Vlad nvarchar(32),
	Korpus nvarchar(32),
	Stroenie nvarchar(32),
	District nvarchar(1032),
	Region nvarchar(1032),
	Street nvarchar(1032),
	Unod nvarchar(32),
	Status nvarchar(1032),
);

BULK INSERT #tmp
FROM 'D:\BTI_bulk_insert\address.csv'
WITH
(
	FORMATFILE = 'D:\BTI_bulk_insert\address.fmt',
    FIRSTROW = 2,
	MAXERRORS = 0, 
    TABLOCK
);


WITH CONVERTED AS
(
	SELECT 
		CAST(SUBSTRING(Id, 2, LEN(Id) - 2) AS BIGINT) "Id",
		SUBSTRING(FullAddress, 2, LEN(FullAddress) - 2) "FullAddress",
		CAST(SUBSTRING(Unom, 2, LEN(Unom) - 2) AS BIGINT) "Unom",
		CASE 
			WHEN CHARINDEX(',', FullAddress) = 0 THEN FullAddress
			ELSE SUBSTRING(FullAddress, CHARINDEX(',', FullAddress) + 1, LEN(FullAddress) - CHARINDEX(',', FullAddress) - 1)
		END "ShortAddress",
		CASE
			WHEN CHARINDEX(':', Street) > 2 THEN CAST (SUBSTRING(Street, 2, CHARINDEX(':', Street) - 2) AS BIGINT)
			ELSE NULL
		END "StreetId",
		CASE
			WHEN CHARINDEX(':', Region) > 2 THEN CAST (SUBSTRING(Region, 2, CHARINDEX(':', Region) - 2) AS BIGINT)
			ELSE NULL
		END "RegionId",
		CASE
			WHEN CHARINDEX(':', District) > 2 THEN CAST (SUBSTRING(District, 2, CHARINDEX(':', District) - 2) AS BIGINT)
			ELSE NULL
		END "DistrictId",
		CAST(SUBSTRING(Unod, 2, LEN(Unod) - 2) AS BIGINT) "Unod",
		CASE
			WHEN CHARINDEX(':', Status) > 2 THEN CAST (SUBSTRING(Status, 2, CHARINDEX(':', Status) - 2) AS BIGINT)
			ELSE NULL
		END "Status"
	FROM #tmp
)


INSERT INTO [dbo].[BtiAddress] (Id, FullAddress, Unom, ShortAddress, BtiStreetId, BtiRegionId, BtiDistrictId, Unod, Status)
SELECT * FROM CONVERTED
WHERE CONVERTED.Status IN (1, 2)

DROP TABLE #tmp

update dbo.BtiAddress set FullAddress = REPLACE(FullAddress, '""', '"') 