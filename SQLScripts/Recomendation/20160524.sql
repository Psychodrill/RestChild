use [RestChild]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Organization_5_309576141__K12_1_2_3_4_5_6_7_8_9_11_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_29_30_31_32_33_] ON [dbo].[Organization]
(
	[ParentId] ASC
)
INCLUDE ( 	[Id],
	[Name],
	[ShortName],
	[ExternalUid],
	[Inn],
	[Kpp],
	[Ogrn],
	[IsDeleted],
	[IsLast],
	[IsVedomstvo],
	[EntityId],
	[Phone],
	[EkisSourcePk],
	[EkisExternalPk],
	[EkisStatus],
	[EkisGuid],
	[IsVedOrganization],
	[IsContractor],
	[IsTransport],
	[TargetOrganizationPk],
	[LastUpdateTick],
	[Address],
	[Email],
	[ContactPerson],
	[StateDistrictId],
	[Comment],
	[Commission],
	[CuratorId],
	[LatinName],
	[Ownership],
	[PostAdderss],
	[HeadPerson],
	[IsTradeUnion],
	[IsHotel],
	[Eid],
	[EidSendStatus],
	[EidSyncDate],
	[CountInTour]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_309576141_12_8] ON [dbo].[Organization]([ParentId], [IsDeleted])
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K57_1] ON [dbo].[Request]
(
	[ForIndex] ASC
)
INCLUDE ( 	[Id]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_ListOfChilds_5_1598628738__K20] ON [dbo].[ListOfChilds]
(
	[ForIndex] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1598628738_1_5_12] ON [dbo].[ListOfChilds]([Id], [LimitOnOrganizationId], [StateId])
go

CREATE STATISTICS [_dta_stat_757577737_108_34_49_55] ON [dbo].[Child]([EkisNeedSend], [RequestId], [IsIncludeInInteragency], [IsIncludeInInteragencySecondary])
go

