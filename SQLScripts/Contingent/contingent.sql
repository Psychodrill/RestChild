IF OBJECT_ID('tempdb..#tmp') IS NOT NULL
   DROP TABLE #tmp

CREATE TABLE #tmp (
	PK nvarchar(32),
	L_NA nvarchar(1000),
	NAM nvarchar(1000),
	P_NA nvarchar(1000),
	D_R nvarchar(1000),
	SEX nvarchar(1000),
	S_PA nvarchar(1000),
	N_PA nvarchar(1000),
	S_SV nvarchar(1000),
	N_SV nvarchar(1000),
	U_ID nvarchar(1000),
	OUID nvarchar(1000),
	SNILS nvarchar(1000),
	STAT nvarchar(1000)
);


BULK INSERT #tmp 
FROM 'D:\tmp\контингент_20151221.csv'
WITH
(	
	CODEPAGE = '1251',
	FORMATFILE = 'C:\Projects\RestChild\SQLScripts\Contingent\contingent.fmt',
    FIRSTROW = 2,
	MAXERRORS = 0, 
    TABLOCK
);

WITH CONVERTED AS
(
	SELECT 
		CAST(SUBSTRING(PK, 2, LEN(PK) - 1) AS BIGINT) Id,
		L_NA as LastName,
		NAM as FirstName,
		P_NA as MiddleName,
		convert(datetime2, D_R, 104) as BirthDate,
		case when SEX='Мужской' then 1 else 0 end as Sex,
		cast(U_ID as uniqueidentifier) as Guid,
		SNILS as Snils,
		S_SV as BirthCertSeria,
		N_SV as BirthCertNumber,
		S_PA as PassportSeria,
		N_PA as PassportNumber,
		cast(OUID as uniqueidentifier) as OrganizationGuid,
		SUBSTRING(STAT, 1, LEN(STAT) - 2) Status,
		getdate() DateChange, 
		3 ProcessedStatusId, 
		-1 LastUpdateTick, 
		isnull(L_NA,'') + '|' + isnull(NAM,'') + '|' + isnull(P_NA,'')+'|' + isnull(D_R,'') + '|'+ isnull(S_SV,NEWID ())+ '|'+isnull(N_SV,NEWID())  KeyOne, 
		isnull(L_NA,'') + '|' + isnull(NAM,'') + '|' + isnull(P_NA,'')+'|' + isnull(D_R,'') + '|'+ isnull(S_PA,NEWID ())+ '|'+isnull(N_PA,NEWID())  KeySecond
	FROM #tmp
)
insert into [dbo].[Contingent]
(Id, LastName, FirstName, MiddleName, BirthDate, Sex, Guid, Snils, BirthCertSeria, BirthCertNumber, PassportSeria, PassportNumber, OrganizationGuid, Status, DateChange, ProcessedStatusId, LastUpdateTick, KeyOne, KeySecond
)
SELECT * FROM CONVERTED
