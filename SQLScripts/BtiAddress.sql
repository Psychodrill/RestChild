/*
   18 августа 2016 г.20:57:45
   User: 
   Server: localhost
   Database: RestChild
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BtiAddress
	DROP CONSTRAINT [FK_dbo.BtiAddress_dbo.BtiStreet_BtiStreetId]
GO
ALTER TABLE dbo.BtiStreet SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BtiAddress
	DROP CONSTRAINT [FK_dbo.BtiAddress_dbo.BtiRegion_BtiRegionId]
GO
ALTER TABLE dbo.BtiRegion SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BtiAddress
	DROP CONSTRAINT [FK_dbo.BtiAddress_dbo.BtiDistrict_BtiDistrictId]
GO
ALTER TABLE dbo.BtiDistrict SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BtiAddress
	DROP CONSTRAINT DF__BtiAddres__LastU__200DB40D
GO
CREATE TABLE dbo.Tmp_BtiAddress
	(
	Id bigint NOT NULL IDENTITY (1, 1),
	FullAddress nvarchar(1000) NULL,
	Unom bigint NOT NULL,
	ShortAddress nvarchar(1000) NULL,
	Unod bigint NOT NULL,
	Status bigint NOT NULL,
	BtiDistrictId bigint NULL,
	BtiRegionId bigint NULL,
	BtiStreetId bigint NULL,
	LastUpdateTick bigint NOT NULL,
	Eid bigint NULL,
	EidSendStatus bigint NULL,
	EidSyncDate datetime2(7) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_BtiAddress SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_BtiAddress ADD CONSTRAINT
	DF__BtiAddres__LastU__200DB40D DEFAULT ((0)) FOR LastUpdateTick
GO
SET IDENTITY_INSERT dbo.Tmp_BtiAddress ON
GO
IF EXISTS(SELECT * FROM dbo.BtiAddress)
	 EXEC('INSERT INTO dbo.Tmp_BtiAddress (Id, FullAddress, Unom, ShortAddress, Unod, Status, BtiDistrictId, BtiRegionId, BtiStreetId, LastUpdateTick, Eid, EidSendStatus, EidSyncDate)
		SELECT Id, FullAddress, Unom, ShortAddress, Unod, Status, BtiDistrictId, BtiRegionId, BtiStreetId, LastUpdateTick, Eid, EidSendStatus, EidSyncDate FROM dbo.BtiAddress WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_BtiAddress OFF
GO
ALTER TABLE dbo.Address
	DROP CONSTRAINT [FK_dbo.Address_dbo.BtiAddress_BtiAddressId]
GO
DROP TABLE dbo.BtiAddress
GO
EXECUTE sp_rename N'dbo.Tmp_BtiAddress', N'BtiAddress', 'OBJECT' 
GO
ALTER TABLE dbo.BtiAddress ADD CONSTRAINT
	[PK_dbo.BtiAddress] PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_BtiDistrictId ON dbo.BtiAddress
	(
	BtiDistrictId
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_BtiRegionId ON dbo.BtiAddress
	(
	BtiRegionId
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_BtiStreetId ON dbo.BtiAddress
	(
	BtiStreetId
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX _dta_index_BtiAddress_5_437576597__K6_K3_1_2_4_5_7_8_9 ON dbo.BtiAddress
	(
	Status,
	Unom
	) INCLUDE (Id, FullAddress, ShortAddress, Unod, BtiDistrictId, BtiRegionId, BtiStreetId) 
 WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX _dta_index_BtiAddress_5_437576597__K9_K6_K7_K1_K8_4 ON dbo.BtiAddress
	(
	BtiStreetId,
	Status,
	BtiDistrictId,
	Id,
	BtiRegionId
	) INCLUDE (ShortAddress) 
 WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Eid ON dbo.BtiAddress
	(
	Eid
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_EidSendStatus ON dbo.BtiAddress
	(
	EidSendStatus
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.BtiAddress ADD CONSTRAINT
	[FK_dbo.BtiAddress_dbo.BtiDistrict_BtiDistrictId] FOREIGN KEY
	(
	BtiDistrictId
	) REFERENCES dbo.BtiDistrict
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BtiAddress ADD CONSTRAINT
	[FK_dbo.BtiAddress_dbo.BtiRegion_BtiRegionId] FOREIGN KEY
	(
	BtiRegionId
	) REFERENCES dbo.BtiRegion
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BtiAddress ADD CONSTRAINT
	[FK_dbo.BtiAddress_dbo.BtiStreet_BtiStreetId] FOREIGN KEY
	(
	BtiStreetId
	) REFERENCES dbo.BtiStreet
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Address ADD CONSTRAINT
	[FK_dbo.Address_dbo.BtiAddress_BtiAddressId] FOREIGN KEY
	(
	BtiAddressId
	) REFERENCES dbo.BtiAddress
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Address SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
