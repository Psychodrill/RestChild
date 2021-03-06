use [RestChild]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K90_K23_K61_K82_K73_K1_K34_K65_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_24_25_26_] ON [dbo].[Child]
(
	[BoutId] ASC,
	[Male] ASC,
	[IsLast] ASC,
	[PartyId] ASC,
	[IsDeleted] ASC,
	[Id] ASC,
	[RequestId] ASC,
	[ChildListId] ASC
)
INCLUDE ( 	[LastName],
	[FirstName],
	[MiddleName],
	[DocumentSeria],
	[DocumentNumber],
	[DocumentDateOfIssue],
	[DocumentSubjectIssue],
	[DateOfBirth],
	[BenefitDate],
	[BenefitNeverEnd],
	[BenefitEndDate],
	[BenefitNumber],
	[BenefitSubjectIssue],
	[BenefitDateOfIssure],
	[ForeginSeria],
	[ForeginNumber],
	[ForeginSubjectIssue],
	[ForeginDateOfIssue],
	[ForeginDateEnd],
	[SchoolNotPresent],
	[RegisteredInMoscow],
	[BenefitApprove],
	[BenefitApproveComment],
	[BenefitApproveHtml],
	[BenefitRequestNumber],
	[BenefitRequestDate],
	[BenefitAnswerNumber],
	[BenefitAnswerDate],
	[BenefitAnswerComment],
	[Snils],
	[BenefitRequestComment],
	[SchoolId],
	[DocumentTypeId],
	[BenefitTypeId],
	[AddressId],
	[BenefitDocTypeId],
	[ForeginTypeId],
	[StatusByChildId],
	[EntityId],
	[BenefitApproveTypeId],
	[TypeOfRestrictionId],
	[ApplicantId],
	[DocumentFileUrl],
	[DocumentFileTitle],
	[IsIncludeInInteragency],
	[IsApprovedInInteragency],
	[IsApprovedInInteragencySecondary],
	[BenefitGroupInvalidId],
	[IsIncludeInInteragencySecondary],
	[IsInvalid],
	[Key],
	[IntervalStart],
	[IntervalEnd],
	[KeySame],
	[BenefitApproveRequestDate],
	[TourVolumeId],
	[PaymentFileUrl],
	[PaymentFileTitle],
	[HaveMiddleName],
	[ContactPhone],
	[IncludeReasonId],
	[ExcludeReasonId],
	[ContactLastName],
	[ContactFirstName],
	[ContactMiddleName],
	[ContactHaveMiddleName],
	[PlaceOfBirth],
	[NotNeedTicketForward],
	[NotNeedTicketBackward],
	[Payed],
	[YearOfCompany],
	[TypeOfGroupCheckId],
	[NotComeInPlaceOfRest],
	[TicketId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K34_K73_K61_K65_K87_1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_] ON [dbo].[Child]
(
	[RequestId] ASC,
	[IsDeleted] ASC,
	[IsLast] ASC,
	[ChildListId] ASC,
	[Payed] ASC
)
INCLUDE ( 	[Id],
	[LastName],
	[FirstName],
	[MiddleName],
	[DocumentSeria],
	[DocumentNumber],
	[DocumentDateOfIssue],
	[DocumentSubjectIssue],
	[DateOfBirth],
	[BenefitDate],
	[BenefitNeverEnd],
	[BenefitEndDate],
	[BenefitNumber],
	[BenefitSubjectIssue],
	[BenefitDateOfIssure],
	[ForeginSeria],
	[ForeginNumber],
	[ForeginSubjectIssue],
	[ForeginDateOfIssue],
	[ForeginDateEnd],
	[SchoolNotPresent],
	[RegisteredInMoscow],
	[Male],
	[BenefitApprove],
	[BenefitApproveComment],
	[BenefitApproveHtml],
	[BenefitRequestNumber],
	[BenefitRequestDate],
	[BenefitAnswerNumber],
	[BenefitAnswerDate],
	[BenefitAnswerComment],
	[Snils],
	[BenefitRequestComment],
	[SchoolId],
	[DocumentTypeId],
	[BenefitTypeId],
	[AddressId],
	[BenefitDocTypeId],
	[ForeginTypeId],
	[StatusByChildId],
	[EntityId],
	[BenefitApproveTypeId],
	[TypeOfRestrictionId],
	[ApplicantId],
	[DocumentFileUrl],
	[DocumentFileTitle],
	[IsIncludeInInteragency],
	[IsApprovedInInteragency],
	[IsApprovedInInteragencySecondary],
	[BenefitGroupInvalidId],
	[IsIncludeInInteragencySecondary],
	[IsInvalid],
	[Key],
	[IntervalStart],
	[IntervalEnd],
	[KeySame],
	[BenefitApproveRequestDate],
	[TourVolumeId],
	[PaymentFileUrl],
	[PaymentFileTitle],
	[HaveMiddleName],
	[ContactPhone],
	[IncludeReasonId],
	[ExcludeReasonId],
	[ContactLastName],
	[ContactFirstName],
	[ContactMiddleName],
	[ContactHaveMiddleName],
	[PartyId],
	[PlaceOfBirth],
	[NotNeedTicketForward],
	[NotNeedTicketBackward],
	[YearOfCompany],
	[TypeOfGroupCheckId],
	[BoutId],
	[NotComeInPlaceOfRest],
	[TicketId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K43_K59_K58_57_65] ON [dbo].[Child]
(
	[EntityId] ASC,
	[IntervalEnd] ASC,
	[IntervalStart] ASC
)
INCLUDE ( 	[Key],
	[ChildListId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K65_K1_K57_K89_K88] ON [dbo].[Child]
(
	[ChildListId] ASC,
	[Id] ASC,
	[Key] ASC,
	[TypeOfGroupCheckId] ASC,
	[YearOfCompany] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K90_K61_K82_K73_K1_34] ON [dbo].[Child]
(
	[BoutId] ASC,
	[IsLast] ASC,
	[PartyId] ASC,
	[IsDeleted] ASC,
	[Id] ASC
)
INCLUDE ( 	[RequestId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K65_K1_K60_K89_K88] ON [dbo].[Child]
(
	[ChildListId] ASC,
	[Id] ASC,
	[KeySame] ASC,
	[TypeOfGroupCheckId] ASC,
	[YearOfCompany] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K34_K1_K38] ON [dbo].[Child]
(
	[RequestId] ASC,
	[Id] ASC,
	[AddressId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_757577737_89_46] ON [dbo].[Child]([TypeOfGroupCheckId], [ApplicantId])
go

CREATE STATISTICS [_dta_stat_757577737_60_1] ON [dbo].[Child]([KeySame], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_73_65] ON [dbo].[Child]([IsDeleted], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_38_1] ON [dbo].[Child]([AddressId], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_82_1] ON [dbo].[Child]([PartyId], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_89_60_1] ON [dbo].[Child]([TypeOfGroupCheckId], [KeySame], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_65_34_61] ON [dbo].[Child]([ChildListId], [RequestId], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_61_90_1] ON [dbo].[Child]([IsLast], [BoutId], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_61_73_43] ON [dbo].[Child]([IsLast], [IsDeleted], [EntityId])
go

CREATE STATISTICS [_dta_stat_757577737_88_34_1] ON [dbo].[Child]([YearOfCompany], [RequestId], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_59_57_1] ON [dbo].[Child]([IntervalEnd], [Key], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_89_36_35] ON [dbo].[Child]([TypeOfGroupCheckId], [DocumentTypeId], [SchoolId])
go

CREATE STATISTICS [_dta_stat_757577737_38_37_36] ON [dbo].[Child]([AddressId], [BenefitTypeId], [DocumentTypeId])
go

CREATE STATISTICS [_dta_stat_757577737_57_1_65] ON [dbo].[Child]([Key], [Id], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_89] ON [dbo].[Child]([Id], [RequestId], [TypeOfGroupCheckId])
go

CREATE STATISTICS [_dta_stat_757577737_89_1_46] ON [dbo].[Child]([TypeOfGroupCheckId], [Id], [ApplicantId])
go

CREATE STATISTICS [_dta_stat_757577737_43_1_61] ON [dbo].[Child]([EntityId], [Id], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_34_90_61] ON [dbo].[Child]([RequestId], [BoutId], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_1_61_82_73] ON [dbo].[Child]([Id], [IsLast], [PartyId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_757577737_1_89_57_65] ON [dbo].[Child]([Id], [TypeOfGroupCheckId], [Key], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_1_90_61_82] ON [dbo].[Child]([Id], [BoutId], [IsLast], [PartyId])
go

CREATE STATISTICS [_dta_stat_757577737_58_59_61_73] ON [dbo].[Child]([IntervalStart], [IntervalEnd], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_757577737_1_90_89_65] ON [dbo].[Child]([Id], [BoutId], [TypeOfGroupCheckId], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_1_65_60_89] ON [dbo].[Child]([Id], [ChildListId], [KeySame], [TypeOfGroupCheckId])
go

CREATE STATISTICS [_dta_stat_757577737_1_89_36_35] ON [dbo].[Child]([Id], [TypeOfGroupCheckId], [DocumentTypeId], [SchoolId])
go

CREATE STATISTICS [_dta_stat_757577737_73_61_1_90] ON [dbo].[Child]([IsDeleted], [IsLast], [Id], [BoutId])
go

CREATE STATISTICS [_dta_stat_757577737_88_60_1_34] ON [dbo].[Child]([YearOfCompany], [KeySame], [Id], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_61_73_34_43] ON [dbo].[Child]([IsLast], [IsDeleted], [RequestId], [EntityId])
go

CREATE STATISTICS [_dta_stat_757577737_61_57_1_58] ON [dbo].[Child]([IsLast], [Key], [Id], [IntervalStart])
go

CREATE STATISTICS [_dta_stat_757577737_43_61_59_73] ON [dbo].[Child]([EntityId], [IsLast], [IntervalEnd], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_757577737_65_34_90_61] ON [dbo].[Child]([ChildListId], [RequestId], [BoutId], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_59_61_73_34] ON [dbo].[Child]([IntervalEnd], [IsLast], [IsDeleted], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_61_82] ON [dbo].[Child]([Id], [RequestId], [IsLast], [PartyId])
go

CREATE STATISTICS [_dta_stat_757577737_38_65_36_35] ON [dbo].[Child]([AddressId], [ChildListId], [DocumentTypeId], [SchoolId])
go

CREATE STATISTICS [_dta_stat_757577737_1_60_89_34] ON [dbo].[Child]([Id], [KeySame], [TypeOfGroupCheckId], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_89_1_65_88_61] ON [dbo].[Child]([TypeOfGroupCheckId], [Id], [ChildListId], [YearOfCompany], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_90_61_82] ON [dbo].[Child]([Id], [RequestId], [BoutId], [IsLast], [PartyId])
go

CREATE STATISTICS [_dta_stat_757577737_61_89_57_65_1] ON [dbo].[Child]([IsLast], [TypeOfGroupCheckId], [Key], [ChildListId], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_61_89_60_65_1] ON [dbo].[Child]([IsLast], [TypeOfGroupCheckId], [KeySame], [ChildListId], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_34_43_59_58_61] ON [dbo].[Child]([RequestId], [EntityId], [IntervalEnd], [IntervalStart], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_58_61_73_34_43] ON [dbo].[Child]([IntervalStart], [IsLast], [IsDeleted], [RequestId], [EntityId])
go

CREATE STATISTICS [_dta_stat_757577737_1_90_23_61_82] ON [dbo].[Child]([Id], [BoutId], [Male], [IsLast], [PartyId])
go

CREATE STATISTICS [_dta_stat_757577737_60_89_88_65_61] ON [dbo].[Child]([KeySame], [TypeOfGroupCheckId], [YearOfCompany], [ChildListId], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_57_89_88_65_61] ON [dbo].[Child]([Key], [TypeOfGroupCheckId], [YearOfCompany], [ChildListId], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_34_38_37_36_35] ON [dbo].[Child]([RequestId], [AddressId], [BenefitTypeId], [DocumentTypeId], [SchoolId])
go

CREATE STATISTICS [_dta_stat_757577737_73_57_1_58_59] ON [dbo].[Child]([IsDeleted], [Key], [Id], [IntervalStart], [IntervalEnd])
go

CREATE STATISTICS [_dta_stat_757577737_61_82_73_1_34] ON [dbo].[Child]([IsLast], [PartyId], [IsDeleted], [Id], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_34_89_88_1_60] ON [dbo].[Child]([RequestId], [TypeOfGroupCheckId], [YearOfCompany], [Id], [KeySame])
go

CREATE STATISTICS [_dta_stat_757577737_61_65_73_87_34] ON [dbo].[Child]([IsLast], [ChildListId], [IsDeleted], [Payed], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_34_43_61_59_73] ON [dbo].[Child]([RequestId], [EntityId], [IsLast], [IntervalEnd], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_757577737_90_61_73_34_65] ON [dbo].[Child]([BoutId], [IsLast], [IsDeleted], [RequestId], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_61_73_87_90_65_34] ON [dbo].[Child]([IsLast], [IsDeleted], [Payed], [BoutId], [ChildListId], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_89_88_57_1_65_61] ON [dbo].[Child]([TypeOfGroupCheckId], [YearOfCompany], [Key], [Id], [ChildListId], [IsLast])
go

CREATE STATISTICS [_dta_stat_757577737_89_88_61_1_73_60] ON [dbo].[Child]([TypeOfGroupCheckId], [YearOfCompany], [IsLast], [Id], [IsDeleted], [KeySame])
go

CREATE STATISTICS [_dta_stat_757577737_90_61_82_73_1_34] ON [dbo].[Child]([BoutId], [IsLast], [PartyId], [IsDeleted], [Id], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_43_58_73_61_59_1] ON [dbo].[Child]([EntityId], [IntervalStart], [IsDeleted], [IsLast], [IntervalEnd], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_65_89_88_61_73_1] ON [dbo].[Child]([ChildListId], [TypeOfGroupCheckId], [YearOfCompany], [IsLast], [IsDeleted], [Id])
go

CREATE STATISTICS [_dta_stat_757577737_89_90_65_38_36_35] ON [dbo].[Child]([TypeOfGroupCheckId], [BoutId], [ChildListId], [AddressId], [DocumentTypeId], [SchoolId])
go

CREATE STATISTICS [_dta_stat_757577737_73_89_57_65_1_88] ON [dbo].[Child]([IsDeleted], [TypeOfGroupCheckId], [Key], [ChildListId], [Id], [YearOfCompany])
go

CREATE STATISTICS [_dta_stat_757577737_73_89_60_65_1_88] ON [dbo].[Child]([IsDeleted], [TypeOfGroupCheckId], [KeySame], [ChildListId], [Id], [YearOfCompany])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_65_23_61_82] ON [dbo].[Child]([Id], [RequestId], [ChildListId], [Male], [IsLast], [PartyId])
go

CREATE STATISTICS [_dta_stat_757577737_43_59_58_61_73_34] ON [dbo].[Child]([EntityId], [IntervalEnd], [IntervalStart], [IsLast], [IsDeleted], [RequestId])
go

CREATE STATISTICS [_dta_stat_757577737_65_89_88_61_73_57] ON [dbo].[Child]([ChildListId], [TypeOfGroupCheckId], [YearOfCompany], [IsLast], [IsDeleted], [Key])
go

CREATE STATISTICS [_dta_stat_757577737_57_58_59_61_73_43] ON [dbo].[Child]([Key], [IntervalStart], [IntervalEnd], [IsLast], [IsDeleted], [EntityId])
go

CREATE STATISTICS [_dta_stat_757577737_65_89_88_61_73_60] ON [dbo].[Child]([ChildListId], [TypeOfGroupCheckId], [YearOfCompany], [IsLast], [IsDeleted], [KeySame])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_65_90_23_61_82] ON [dbo].[Child]([Id], [RequestId], [ChildListId], [BoutId], [Male], [IsLast], [PartyId])
go

CREATE STATISTICS [_dta_stat_757577737_89_88_1_60_65_61_73] ON [dbo].[Child]([TypeOfGroupCheckId], [YearOfCompany], [Id], [KeySame], [ChildListId], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_757577737_1_57_58_59_61_73_43] ON [dbo].[Child]([Id], [Key], [IntervalStart], [IntervalEnd], [IsLast], [IsDeleted], [EntityId])
go

CREATE STATISTICS [_dta_stat_757577737_23_61_82_73_1_34_65] ON [dbo].[Child]([Male], [IsLast], [PartyId], [IsDeleted], [Id], [RequestId], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_1_38_89_90_65_36_35] ON [dbo].[Child]([Id], [AddressId], [TypeOfGroupCheckId], [BoutId], [ChildListId], [DocumentTypeId], [SchoolId])
go

CREATE STATISTICS [_dta_stat_757577737_89_88_61_1_73_57_65] ON [dbo].[Child]([TypeOfGroupCheckId], [YearOfCompany], [IsLast], [Id], [IsDeleted], [Key], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_65_57_58_59_61_73] ON [dbo].[Child]([Id], [RequestId], [ChildListId], [Key], [IntervalStart], [IntervalEnd], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_757577737_1_34_65_58_59_61_73_43] ON [dbo].[Child]([Id], [RequestId], [ChildListId], [IntervalStart], [IntervalEnd], [IsLast], [IsDeleted], [EntityId])
go

CREATE STATISTICS [_dta_stat_757577737_34_73_61_1_90_23_82_65] ON [dbo].[Child]([RequestId], [IsDeleted], [IsLast], [Id], [BoutId], [Male], [PartyId], [ChildListId])
go

CREATE STATISTICS [_dta_stat_757577737_34_43_58_73_61_59_1_57_65] ON [dbo].[Child]([RequestId], [EntityId], [IntervalStart], [IsDeleted], [IsLast], [IntervalEnd], [Id], [Key], [ChildListId])
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_BtiAddress_5_437576597__K9_K6_K7_K1_K8_4] ON [dbo].[BtiAddress]
(
	[BtiStreetId] ASC,
	[Status] ASC,
	[BtiDistrictId] ASC,
	[Id] ASC,
	[BtiRegionId] ASC
)
INCLUDE ( 	[ShortAddress]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_437576597_1_7] ON [dbo].[BtiAddress]([Id], [BtiDistrictId])
go

CREATE STATISTICS [_dta_stat_437576597_6_7_1] ON [dbo].[BtiAddress]([Status], [BtiDistrictId], [Id])
go

CREATE STATISTICS [_dta_stat_437576597_7_1_8_9] ON [dbo].[BtiAddress]([BtiDistrictId], [Id], [BtiRegionId], [BtiStreetId])
go

CREATE STATISTICS [_dta_stat_437576597_7_1_8_6] ON [dbo].[BtiAddress]([BtiDistrictId], [Id], [BtiRegionId], [Status])
go

CREATE STATISTICS [_dta_stat_437576597_9_1_7_6_8] ON [dbo].[BtiAddress]([BtiStreetId], [Id], [BtiDistrictId], [Status], [BtiRegionId])
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K17_K27_K29_K42_1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_18_19_20_21_22_23_24_25_26_28_30_31_] ON [dbo].[Applicant]
(
	[RequestId] ASC,
	[IsLast] ASC,
	[ChildListId] ASC,
	[Payed] ASC
)
INCLUDE ( 	[Id],
	[LastName],
	[FirstName],
	[MiddleName],
	[DocumentSeria],
	[DocumentNumber],
	[DocumentDateOfIssue],
	[DocumentSubjectIssue],
	[Phone],
	[Email],
	[Snils],
	[DocumentTypeId],
	[ApplicantTypeId],
	[IsAccomp],
	[IsApplicant],
	[EntityId],
	[ForeginSeria],
	[ForeginNumber],
	[ForeginSubjectIssue],
	[ForeginDateOfIssue],
	[ForeginDateEnd],
	[ForeginTypeId],
	[Key],
	[IntervalStart],
	[IntervalEnd],
	[TourVolumeId],
	[Male],
	[DateOfBirth],
	[Position],
	[HaveMiddleName],
	[ExcludeReasonId],
	[IncludeReasonId],
	[PaymentFileUrl],
	[PaymentFileTitle],
	[PlaceOfBirth],
	[NotNeedTicketForward],
	[NotNeedTicketBackward],
	[BoutId],
	[NotComeInPlaceOfRest],
	[TicketId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K17_K1_K14_K27_K24_K29_2_3_4_5_6_7_8_9_10_11_12_13_15_16_18_19_20_21_22_23_25_26_28_30_31_32_] ON [dbo].[Applicant]
(
	[RequestId] ASC,
	[Id] ASC,
	[IsAccomp] ASC,
	[IsLast] ASC,
	[Key] ASC,
	[ChildListId] ASC
)
INCLUDE ( 	[LastName],
	[FirstName],
	[MiddleName],
	[DocumentSeria],
	[DocumentNumber],
	[DocumentDateOfIssue],
	[DocumentSubjectIssue],
	[Phone],
	[Email],
	[Snils],
	[DocumentTypeId],
	[ApplicantTypeId],
	[IsApplicant],
	[EntityId],
	[ForeginSeria],
	[ForeginNumber],
	[ForeginSubjectIssue],
	[ForeginDateOfIssue],
	[ForeginDateEnd],
	[ForeginTypeId],
	[IntervalStart],
	[IntervalEnd],
	[TourVolumeId],
	[Male],
	[DateOfBirth],
	[Position],
	[HaveMiddleName],
	[ExcludeReasonId],
	[IncludeReasonId],
	[PaymentFileUrl],
	[PaymentFileTitle],
	[PlaceOfBirth],
	[NotNeedTicketForward],
	[NotNeedTicketBackward],
	[Payed],
	[BoutId],
	[NotComeInPlaceOfRest],
	[TicketId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K14_K27_K1_K17_K24_K29_2_3_4_5_6_7_8_9_10_11_12_13_15_16_18_19_20_21_22_23_25_26_28_30_31_32_] ON [dbo].[Applicant]
(
	[IsAccomp] ASC,
	[IsLast] ASC,
	[Id] ASC,
	[RequestId] ASC,
	[Key] ASC,
	[ChildListId] ASC
)
INCLUDE ( 	[LastName],
	[FirstName],
	[MiddleName],
	[DocumentSeria],
	[DocumentNumber],
	[DocumentDateOfIssue],
	[DocumentSubjectIssue],
	[Phone],
	[Email],
	[Snils],
	[DocumentTypeId],
	[ApplicantTypeId],
	[IsApplicant],
	[EntityId],
	[ForeginSeria],
	[ForeginNumber],
	[ForeginSubjectIssue],
	[ForeginDateOfIssue],
	[ForeginDateEnd],
	[ForeginTypeId],
	[IntervalStart],
	[IntervalEnd],
	[TourVolumeId],
	[Male],
	[DateOfBirth],
	[Position],
	[HaveMiddleName],
	[ExcludeReasonId],
	[IncludeReasonId],
	[PaymentFileUrl],
	[PaymentFileTitle],
	[PlaceOfBirth],
	[NotNeedTicketForward],
	[NotNeedTicketBackward],
	[Payed],
	[BoutId],
	[NotComeInPlaceOfRest],
	[TicketId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K1_2_3_4] ON [dbo].[Applicant]
(
	[Id] ASC
)
INCLUDE ( 	[LastName],
	[FirstName],
	[MiddleName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K1_2_3] ON [dbo].[Applicant]
(
	[Id] ASC
)
INCLUDE ( 	[LastName],
	[FirstName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K14_K1_K43] ON [dbo].[Applicant]
(
	[IsAccomp] ASC,
	[Id] ASC,
	[BoutId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Applicant_5_661577395__K1_4] ON [dbo].[Applicant]
(
	[Id] ASC
)
INCLUDE ( 	[MiddleName]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_661577395_1_16] ON [dbo].[Applicant]([Id], [EntityId])
go

CREATE STATISTICS [_dta_stat_661577395_1_43] ON [dbo].[Applicant]([Id], [BoutId])
go

CREATE STATISTICS [_dta_stat_661577395_1_29_14] ON [dbo].[Applicant]([Id], [ChildListId], [IsAccomp])
go

CREATE STATISTICS [_dta_stat_661577395_29_1_24] ON [dbo].[Applicant]([ChildListId], [Id], [Key])
go

CREATE STATISTICS [_dta_stat_661577395_17_14_27] ON [dbo].[Applicant]([RequestId], [IsAccomp], [IsLast])
go

CREATE STATISTICS [_dta_stat_661577395_27_16_1] ON [dbo].[Applicant]([IsLast], [EntityId], [Id])
go

CREATE STATISTICS [_dta_stat_661577395_1_14_43] ON [dbo].[Applicant]([Id], [IsAccomp], [BoutId])
go

CREATE STATISTICS [_dta_stat_661577395_27_42_29] ON [dbo].[Applicant]([IsLast], [Payed], [ChildListId])
go

CREATE STATISTICS [_dta_stat_661577395_24_27_14] ON [dbo].[Applicant]([Key], [IsLast], [IsAccomp])
go

CREATE STATISTICS [_dta_stat_661577395_1_14_27_24] ON [dbo].[Applicant]([Id], [IsAccomp], [IsLast], [Key])
go

CREATE STATISTICS [_dta_stat_661577395_1_24_27_25] ON [dbo].[Applicant]([Id], [Key], [IsLast], [IntervalStart])
go

CREATE STATISTICS [_dta_stat_661577395_29_17_43_27] ON [dbo].[Applicant]([ChildListId], [RequestId], [BoutId], [IsLast])
go

CREATE STATISTICS [_dta_stat_661577395_29_27_17_42] ON [dbo].[Applicant]([ChildListId], [IsLast], [RequestId], [Payed])
go

CREATE STATISTICS [_dta_stat_661577395_1_17_24_27] ON [dbo].[Applicant]([Id], [RequestId], [Key], [IsLast])
go

CREATE STATISTICS [_dta_stat_661577395_17_1_29_14] ON [dbo].[Applicant]([RequestId], [Id], [ChildListId], [IsAccomp])
go

CREATE STATISTICS [_dta_stat_661577395_27_42_43_29_17] ON [dbo].[Applicant]([IsLast], [Payed], [BoutId], [ChildListId], [RequestId])
go

CREATE STATISTICS [_dta_stat_661577395_29_14_27_1_17] ON [dbo].[Applicant]([ChildListId], [IsAccomp], [IsLast], [Id], [RequestId])
go

CREATE STATISTICS [_dta_stat_661577395_27_1_24_29_17] ON [dbo].[Applicant]([IsLast], [Id], [Key], [ChildListId], [RequestId])
go

CREATE STATISTICS [_dta_stat_661577395_24_25_26_27_14_16] ON [dbo].[Applicant]([Key], [IntervalStart], [IntervalEnd], [IsLast], [IsAccomp], [EntityId])
go

CREATE STATISTICS [_dta_stat_661577395_24_29_27_14_1_17] ON [dbo].[Applicant]([Key], [ChildListId], [IsLast], [IsAccomp], [Id], [RequestId])
go

CREATE STATISTICS [_dta_stat_661577395_14_27_1_17_24_25_26] ON [dbo].[Applicant]([IsAccomp], [IsLast], [Id], [RequestId], [Key], [IntervalStart], [IntervalEnd])
go

CREATE STATISTICS [_dta_stat_661577395_1_17_29_25_26_27_14] ON [dbo].[Applicant]([Id], [RequestId], [ChildListId], [IntervalStart], [IntervalEnd], [IsLast], [IsAccomp])
go

CREATE STATISTICS [_dta_stat_661577395_1_17_29_24_25_26_27_14] ON [dbo].[Applicant]([Id], [RequestId], [ChildListId], [Key], [IntervalStart], [IntervalEnd], [IsLast], [IsAccomp])
go

CREATE STATISTICS [_dta_stat_661577395_1_24_25_26_27_14_16_17] ON [dbo].[Applicant]([Id], [Key], [IntervalStart], [IntervalEnd], [IsLast], [IsAccomp], [EntityId], [RequestId])
go

CREATE STATISTICS [_dta_stat_661577395_25_26_27_14_16_1_17_29] ON [dbo].[Applicant]([IntervalStart], [IntervalEnd], [IsLast], [IsAccomp], [EntityId], [Id], [RequestId], [ChildListId])
go

CREATE STATISTICS [_dta_stat_661577395_1_24_25_26_27_16_29_17_14] ON [dbo].[Applicant]([Id], [Key], [IntervalStart], [IntervalEnd], [IsLast], [EntityId], [ChildListId], [RequestId], [IsAccomp])
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K15_K6_K7_K37_K16_1_2_3_4_5_8_9_10_11_12_13_17_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_34_] ON [dbo].[Request]
(
	[StatusId] ASC,
	[IsLast] ASC,
	[IsDeleted] ASC,
	[TourId] ASC,
	[ApplicantId] ASC
)
INCLUDE ( 	[Id],
	[AgentApplicant],
	[RequestNumber],
	[DateRequest],
	[UpdateDate],
	[Version],
	[ExternalUid],
	[ExternalSystem],
	[MainPlaces],
	[AdditionalPlaces],
	[IsDraft],
	[TypeOfRestId],
	[TimeOfRestId],
	[SubjectOfRestId],
	[AttendantTypeId],
	[PlaceOfRestAddonId],
	[PlaceOfRestId],
	[AgentId],
	[EntityId],
	[CreateUserId],
	[OrganizationId],
	[SourceId],
	[DeclineReasonId],
	[RequestNumberMpgu],
	[NeedEmail],
	[NeedSms],
	[DateChangeStatus],
	[HotelsId],
	[YearOfRestId],
	[BookingGuid],
	[CountPlace],
	[CountAttendants],
	[CertificateNumber],
	[ParentRequestId],
	[Price],
	[DateIncome],
	[DateOutcome]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K35_K17_K6_K7_K15_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_] ON [dbo].[Request]
(
	[YearOfRestId] ASC,
	[TypeOfRestId] ASC,
	[IsLast] ASC,
	[IsDeleted] ASC,
	[StatusId] ASC,
	[Id] ASC
)
INCLUDE ( 	[AgentApplicant],
	[RequestNumber],
	[DateRequest],
	[UpdateDate],
	[Version],
	[ExternalUid],
	[ExternalSystem],
	[MainPlaces],
	[AdditionalPlaces],
	[IsDraft],
	[ApplicantId],
	[TimeOfRestId],
	[SubjectOfRestId],
	[AttendantTypeId],
	[PlaceOfRestAddonId],
	[PlaceOfRestId],
	[AgentId],
	[EntityId],
	[CreateUserId],
	[OrganizationId],
	[SourceId],
	[DeclineReasonId],
	[RequestNumberMpgu],
	[NeedEmail],
	[NeedSms],
	[DateChangeStatus],
	[HotelsId],
	[BookingGuid],
	[TourId],
	[CountPlace],
	[CountAttendants],
	[CertificateNumber],
	[ParentRequestId],
	[Price],
	[DateIncome],
	[DateOutcome]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K35_K6_K15_K7_K17_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_28_29_30_31_32_33_] ON [dbo].[Request]
(
	[YearOfRestId] ASC,
	[IsLast] ASC,
	[StatusId] ASC,
	[IsDeleted] ASC,
	[TypeOfRestId] ASC,
	[Id] ASC
)
INCLUDE ( 	[AgentApplicant],
	[RequestNumber],
	[DateRequest],
	[UpdateDate],
	[Version],
	[ExternalUid],
	[ExternalSystem],
	[MainPlaces],
	[AdditionalPlaces],
	[IsDraft],
	[ApplicantId],
	[TimeOfRestId],
	[SubjectOfRestId],
	[AttendantTypeId],
	[PlaceOfRestAddonId],
	[PlaceOfRestId],
	[AgentId],
	[EntityId],
	[CreateUserId],
	[OrganizationId],
	[SourceId],
	[DeclineReasonId],
	[RequestNumberMpgu],
	[NeedEmail],
	[NeedSms],
	[DateChangeStatus],
	[HotelsId],
	[BookingGuid],
	[TourId],
	[CountPlace],
	[CountAttendants],
	[CertificateNumber],
	[ParentRequestId],
	[Price],
	[DateIncome],
	[DateOutcome]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K28_K35_K6_K7_K15_K17_K1_2_3_4_5_8_9_10_11_12_13_16_18_19_20_21_22_23_24_25_27_29_30_31_32_33_] ON [dbo].[Request]
(
	[SourceId] ASC,
	[YearOfRestId] ASC,
	[IsLast] ASC,
	[IsDeleted] ASC,
	[StatusId] ASC,
	[TypeOfRestId] ASC,
	[Id] ASC
)
INCLUDE ( 	[AgentApplicant],
	[RequestNumber],
	[DateRequest],
	[UpdateDate],
	[Version],
	[ExternalUid],
	[ExternalSystem],
	[MainPlaces],
	[AdditionalPlaces],
	[IsDraft],
	[ApplicantId],
	[TimeOfRestId],
	[SubjectOfRestId],
	[AttendantTypeId],
	[PlaceOfRestAddonId],
	[PlaceOfRestId],
	[AgentId],
	[EntityId],
	[CreateUserId],
	[OrganizationId],
	[DeclineReasonId],
	[RequestNumberMpgu],
	[NeedEmail],
	[NeedSms],
	[DateChangeStatus],
	[HotelsId],
	[BookingGuid],
	[TourId],
	[CountPlace],
	[CountAttendants],
	[CertificateNumber],
	[ParentRequestId],
	[Price],
	[DateIncome],
	[DateOutcome]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K16_K15_K7_K6_K1_K35] ON [dbo].[Request]
(
	[ApplicantId] ASC,
	[StatusId] ASC,
	[IsDeleted] ASC,
	[IsLast] ASC,
	[Id] ASC,
	[YearOfRestId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K6_K37_K1_K15_K7] ON [dbo].[Request]
(
	[IsLast] ASC,
	[TourId] ASC,
	[Id] ASC,
	[StatusId] ASC,
	[IsDeleted] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K16_6_7_15] ON [dbo].[Request]
(
	[ApplicantId] ASC
)
INCLUDE ( 	[IsLast],
	[IsDeleted],
	[StatusId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K15_K6_K5_K7_K35_K1_4_16] ON [dbo].[Request]
(
	[StatusId] ASC,
	[IsLast] ASC,
	[UpdateDate] ASC,
	[IsDeleted] ASC,
	[YearOfRestId] ASC,
	[Id] ASC
)
INCLUDE ( 	[DateRequest],
	[ApplicantId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K35_K6_K7_K15_K1_K16_17_28] ON [dbo].[Request]
(
	[YearOfRestId] ASC,
	[IsLast] ASC,
	[IsDeleted] ASC,
	[StatusId] ASC,
	[Id] ASC,
	[ApplicantId] ASC
)
INCLUDE ( 	[TypeOfRestId],
	[SourceId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K1_K37] ON [dbo].[Request]
(
	[Id] ASC,
	[TourId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K36_K7_K15] ON [dbo].[Request]
(
	[BookingGuid] ASC,
	[IsDeleted] ASC,
	[StatusId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K1_K4_5] ON [dbo].[Request]
(
	[Id] ASC,
	[DateRequest] ASC
)
INCLUDE ( 	[UpdateDate]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K7_K1_15] ON [dbo].[Request]
(
	[IsDeleted] ASC,
	[Id] ASC
)
INCLUDE ( 	[StatusId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_917578307_41_1] ON [dbo].[Request]([ParentRequestId], [Id])
go

CREATE STATISTICS [_dta_stat_917578307_24_7] ON [dbo].[Request]([EntityId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_1_25] ON [dbo].[Request]([Id], [CreateUserId])
go

CREATE STATISTICS [_dta_stat_917578307_19_1] ON [dbo].[Request]([SubjectOfRestId], [Id])
go

CREATE STATISTICS [_dta_stat_917578307_7_35] ON [dbo].[Request]([IsDeleted], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_6_17_1] ON [dbo].[Request]([IsLast], [TypeOfRestId], [Id])
go

CREATE STATISTICS [_dta_stat_917578307_7_37_15] ON [dbo].[Request]([IsDeleted], [TourId], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_25_15_1] ON [dbo].[Request]([CreateUserId], [StatusId], [Id])
go

CREATE STATISTICS [_dta_stat_917578307_24_1_7] ON [dbo].[Request]([EntityId], [Id], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_7_36_15] ON [dbo].[Request]([IsDeleted], [BookingGuid], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_37_15_6] ON [dbo].[Request]([TourId], [StatusId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_7_17_35] ON [dbo].[Request]([IsDeleted], [TypeOfRestId], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_37_1_15] ON [dbo].[Request]([TourId], [Id], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_7_1_35] ON [dbo].[Request]([IsDeleted], [Id], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_7_16_1_35] ON [dbo].[Request]([IsDeleted], [ApplicantId], [Id], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_1_15_35_6] ON [dbo].[Request]([Id], [StatusId], [YearOfRestId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_37_16_15_6] ON [dbo].[Request]([TourId], [ApplicantId], [StatusId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_1_16_15_7] ON [dbo].[Request]([Id], [ApplicantId], [StatusId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_1_15_35_7] ON [dbo].[Request]([Id], [StatusId], [YearOfRestId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_6_17_35_7] ON [dbo].[Request]([IsLast], [TypeOfRestId], [YearOfRestId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_1_15_5_7] ON [dbo].[Request]([Id], [StatusId], [UpdateDate], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_17_35_7_15] ON [dbo].[Request]([TypeOfRestId], [YearOfRestId], [IsDeleted], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_6_17_22_15] ON [dbo].[Request]([IsLast], [TypeOfRestId], [PlaceOfRestId], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_7_37_1_15] ON [dbo].[Request]([IsDeleted], [TourId], [Id], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_1_36_7_15] ON [dbo].[Request]([Id], [BookingGuid], [IsDeleted], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_16_1_6_4] ON [dbo].[Request]([ApplicantId], [Id], [IsLast], [DateRequest])
go

CREATE STATISTICS [_dta_stat_917578307_37_15_35_6] ON [dbo].[Request]([TourId], [StatusId], [YearOfRestId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_1_15_4_6] ON [dbo].[Request]([Id], [StatusId], [DateRequest], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_35_1_15_6_5] ON [dbo].[Request]([YearOfRestId], [Id], [StatusId], [IsLast], [UpdateDate])
go

CREATE STATISTICS [_dta_stat_917578307_35_6_7_28_15] ON [dbo].[Request]([YearOfRestId], [IsLast], [IsDeleted], [SourceId], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_6_15_17_1_28] ON [dbo].[Request]([IsLast], [StatusId], [TypeOfRestId], [Id], [SourceId])
go

CREATE STATISTICS [_dta_stat_917578307_6_4_7_15_35] ON [dbo].[Request]([IsLast], [DateRequest], [IsDeleted], [StatusId], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_1_15_17_35_6] ON [dbo].[Request]([Id], [StatusId], [TypeOfRestId], [YearOfRestId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_1_17_35_7_15] ON [dbo].[Request]([Id], [TypeOfRestId], [YearOfRestId], [IsDeleted], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_1_35_15_7_5] ON [dbo].[Request]([Id], [YearOfRestId], [StatusId], [IsDeleted], [UpdateDate])
go

CREATE STATISTICS [_dta_stat_917578307_35_6_7_15_16] ON [dbo].[Request]([YearOfRestId], [IsLast], [IsDeleted], [StatusId], [ApplicantId])
go

CREATE STATISTICS [_dta_stat_917578307_35_1_6_7_15] ON [dbo].[Request]([YearOfRestId], [Id], [IsLast], [IsDeleted], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_1_16_15_35_7] ON [dbo].[Request]([Id], [ApplicantId], [StatusId], [YearOfRestId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_7_17_22_15_35] ON [dbo].[Request]([IsDeleted], [TypeOfRestId], [PlaceOfRestId], [StatusId], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_35_1_6_4_7] ON [dbo].[Request]([YearOfRestId], [Id], [IsLast], [DateRequest], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_1_15_6_7_37] ON [dbo].[Request]([Id], [StatusId], [IsLast], [IsDeleted], [TourId])
go

CREATE STATISTICS [_dta_stat_917578307_1_16_15_6_5] ON [dbo].[Request]([Id], [ApplicantId], [StatusId], [IsLast], [UpdateDate])
go

CREATE STATISTICS [_dta_stat_917578307_1_6_15_7_17] ON [dbo].[Request]([Id], [IsLast], [StatusId], [IsDeleted], [TypeOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_6_15_7_33_1] ON [dbo].[Request]([IsLast], [StatusId], [IsDeleted], [DateChangeStatus], [Id])
go

CREATE STATISTICS [_dta_stat_917578307_15_17_35_28_6] ON [dbo].[Request]([StatusId], [TypeOfRestId], [YearOfRestId], [SourceId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_35_17_22_15_6] ON [dbo].[Request]([YearOfRestId], [TypeOfRestId], [PlaceOfRestId], [StatusId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_17_1_35_6_7] ON [dbo].[Request]([TypeOfRestId], [Id], [YearOfRestId], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_6_37_15_7_16] ON [dbo].[Request]([IsLast], [TourId], [StatusId], [IsDeleted], [ApplicantId])
go

CREATE STATISTICS [_dta_stat_917578307_6_1_15_5_7_35] ON [dbo].[Request]([IsLast], [Id], [StatusId], [UpdateDate], [IsDeleted], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_1_16_35_6_7_15] ON [dbo].[Request]([Id], [ApplicantId], [YearOfRestId], [IsLast], [IsDeleted], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_16_15_35_6_7_17] ON [dbo].[Request]([ApplicantId], [StatusId], [YearOfRestId], [IsLast], [IsDeleted], [TypeOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_16_22_28_15_19_18] ON [dbo].[Request]([ApplicantId], [PlaceOfRestId], [SourceId], [StatusId], [SubjectOfRestId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_28_1_35_15_6_7] ON [dbo].[Request]([SourceId], [Id], [YearOfRestId], [StatusId], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_7_15_17_1_28_35] ON [dbo].[Request]([IsDeleted], [StatusId], [TypeOfRestId], [Id], [SourceId], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_7_16_1_15_6_5] ON [dbo].[Request]([IsDeleted], [ApplicantId], [Id], [StatusId], [IsLast], [UpdateDate])
go

CREATE STATISTICS [_dta_stat_917578307_17_15_35_6_7_1] ON [dbo].[Request]([TypeOfRestId], [StatusId], [YearOfRestId], [IsLast], [IsDeleted], [Id])
go

CREATE STATISTICS [_dta_stat_917578307_15_17_1_28_35_6] ON [dbo].[Request]([StatusId], [TypeOfRestId], [Id], [SourceId], [YearOfRestId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_6_15_17_35_7_28] ON [dbo].[Request]([IsLast], [StatusId], [TypeOfRestId], [YearOfRestId], [IsDeleted], [SourceId])
go

CREATE STATISTICS [_dta_stat_917578307_7_16_1_6_4_15] ON [dbo].[Request]([IsDeleted], [ApplicantId], [Id], [IsLast], [DateRequest], [StatusId])
go

CREATE STATISTICS [_dta_stat_917578307_1_6_4_7_15_35] ON [dbo].[Request]([Id], [IsLast], [DateRequest], [IsDeleted], [StatusId], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_17_15_7_6_1_28_35] ON [dbo].[Request]([TypeOfRestId], [StatusId], [IsDeleted], [IsLast], [Id], [SourceId], [YearOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_17_16_22_28_15_19_18] ON [dbo].[Request]([TypeOfRestId], [ApplicantId], [PlaceOfRestId], [SourceId], [StatusId], [SubjectOfRestId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_15_35_6_7_37_17_22_16] ON [dbo].[Request]([StatusId], [YearOfRestId], [IsLast], [IsDeleted], [TourId], [TypeOfRestId], [PlaceOfRestId], [ApplicantId])
go

CREATE STATISTICS [_dta_stat_917578307_17_15_35_6_7_22_16_18] ON [dbo].[Request]([TypeOfRestId], [StatusId], [YearOfRestId], [IsLast], [IsDeleted], [PlaceOfRestId], [ApplicantId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_917578307_37_16_22_28_15_19_18_17_34] ON [dbo].[Request]([TourId], [ApplicantId], [PlaceOfRestId], [SourceId], [StatusId], [SubjectOfRestId], [TimeOfRestId], [TypeOfRestId], [HotelsId])
go

CREATE STATISTICS [_dta_stat_917578307_17_22_15_16_18_37_35_6_7] ON [dbo].[Request]([TypeOfRestId], [PlaceOfRestId], [StatusId], [ApplicantId], [TimeOfRestId], [TourId], [YearOfRestId], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_917578307_15_35_6_7_37_17_22_1_16] ON [dbo].[Request]([StatusId], [YearOfRestId], [IsLast], [IsDeleted], [TourId], [TypeOfRestId], [PlaceOfRestId], [Id], [ApplicantId])
go

CREATE STATISTICS [_dta_stat_917578307_17_22_15_1_16_18_37_35_6] ON [dbo].[Request]([TypeOfRestId], [PlaceOfRestId], [StatusId], [Id], [ApplicantId], [TimeOfRestId], [TourId], [YearOfRestId], [IsLast])
go

CREATE STATISTICS [_dta_stat_917578307_35_6_7_15_17_22_1_16_18_37] ON [dbo].[Request]([YearOfRestId], [IsLast], [IsDeleted], [StatusId], [TypeOfRestId], [PlaceOfRestId], [Id], [ApplicantId], [TimeOfRestId], [TourId])
go

CREATE STATISTICS [_dta_stat_917578307_35_16_22_28_15_19_18_17_37_34] ON [dbo].[Request]([YearOfRestId], [ApplicantId], [PlaceOfRestId], [SourceId], [StatusId], [SubjectOfRestId], [TimeOfRestId], [TypeOfRestId], [TourId], [HotelsId])
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_ExchangeBaseRegistry_5_789577851__K4_1_5_9] ON [dbo].[ExchangeBaseRegistry]
(
	[SendDate] ASC
)
INCLUDE ( 	[Id],
	[AcknolegmentGuid],
	[IsIncoming]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_ExchangeBaseRegistry_5_789577851__K12] ON [dbo].[ExchangeBaseRegistry]
(
	[IsProcessed] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_789577851_9_1_4] ON [dbo].[ExchangeBaseRegistry]([IsIncoming], [Id], [SendDate])
go

CREATE STATISTICS [_dta_stat_789577851_1_7_4] ON [dbo].[ExchangeBaseRegistry]([Id], [ResponseDate], [SendDate])
go

CREATE NONCLUSTERED INDEX [_dta_index_Address_5_405576483__K1_K8_K9] ON [dbo].[Address]
(
	[Id] ASC,
	[BtiAddressId] ASC,
	[BtiDistrictId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_405576483_8_9] ON [dbo].[Address]([BtiAddressId], [BtiDistrictId])
go

CREATE STATISTICS [_dta_stat_405576483_9_1] ON [dbo].[Address]([BtiDistrictId], [Id])
go

CREATE NONCLUSTERED INDEX [_dta_index_Booking_5_1454628225__K2_K9_1_3_4_5_6_7_8_10] ON [dbo].[Booking]
(
	[Code] ASC,
	[TourVolumeId] ASC
)
INCLUDE ( 	[Id],
	[BookingDate],
	[CountRooms],
	[CountPlace],
	[CountAttendants],
	[Canceled],
	[RequestId],
	[TypeOfRestId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1454628225_8_7] ON [dbo].[Booking]([RequestId], [Canceled])
go

CREATE STATISTICS [_dta_stat_1454628225_10_9] ON [dbo].[Booking]([TypeOfRestId], [TourVolumeId])
go

CREATE STATISTICS [_dta_stat_1454628225_7_1] ON [dbo].[Booking]([Canceled], [Id])
go

CREATE STATISTICS [_dta_stat_1454628225_10_8_9] ON [dbo].[Booking]([TypeOfRestId], [RequestId], [TourVolumeId])
go

CREATE STATISTICS [_dta_stat_1454628225_1_8_7] ON [dbo].[Booking]([Id], [RequestId], [Canceled])
go

CREATE STATISTICS [_dta_stat_1454628225_1_10_8_9] ON [dbo].[Booking]([Id], [TypeOfRestId], [RequestId], [TourVolumeId])
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_LinkToPeople_5_1479676319__K11_K1_2_3_4_5_6_7_8_9_10_12_13_14_15_16_17] ON [dbo].[LinkToPeople]
(
	[ChildId] ASC,
	[Id] ASC
)
INCLUDE ( 	[Wagon],
	[PlaceNumber],
	[NeedTicket],
	[AdministratorTourId],
	[TypeOfLinkPeopleId],
	[CounselorsId],
	[ApplicantId],
	[RequestId],
	[PartyId],
	[DirectoryFlightsId],
	[ListOfChildsId],
	[TransportId],
	[BoutId],
	[NotComeInPlaceOfRest],
	[NotNeedTicketReasonId]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1479676319_15_1] ON [dbo].[LinkToPeople]([BoutId], [Id])
go

CREATE NONCLUSTERED INDEX [_dta_index_ListOfChilds_5_1598628738__K5_K13_K1_K7_K3_K12_10] ON [dbo].[ListOfChilds]
(
	[LimitOnOrganizationId] ASC,
	[TourId] ASC,
	[Id] ASC,
	[TimeOfRestId] ASC,
	[IsDeleted] ASC,
	[StateId] ASC
)
INCLUDE ( 	[CountChild]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1598628738_3_1] ON [dbo].[ListOfChilds]([IsDeleted], [Id])
go

CREATE STATISTICS [_dta_stat_1598628738_3_12] ON [dbo].[ListOfChilds]([IsDeleted], [StateId])
go

CREATE STATISTICS [_dta_stat_1598628738_7_12_1] ON [dbo].[ListOfChilds]([TimeOfRestId], [StateId], [Id])
go

CREATE STATISTICS [_dta_stat_1598628738_5_3_12] ON [dbo].[ListOfChilds]([LimitOnOrganizationId], [IsDeleted], [StateId])
go

CREATE STATISTICS [_dta_stat_1598628738_12_2_3] ON [dbo].[ListOfChilds]([StateId], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_1598628738_5_1_2] ON [dbo].[ListOfChilds]([LimitOnOrganizationId], [Id], [IsLast])
go

CREATE STATISTICS [_dta_stat_1598628738_12_7_5] ON [dbo].[ListOfChilds]([StateId], [TimeOfRestId], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_1_5_3] ON [dbo].[ListOfChilds]([Id], [LimitOnOrganizationId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_1598628738_1_2_3] ON [dbo].[ListOfChilds]([Id], [IsLast], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_1598628738_7_12_13] ON [dbo].[ListOfChilds]([TimeOfRestId], [StateId], [TourId])
go

CREATE STATISTICS [_dta_stat_1598628738_1_13_7_3] ON [dbo].[ListOfChilds]([Id], [TourId], [TimeOfRestId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_1598628738_1_5_7_3] ON [dbo].[ListOfChilds]([Id], [LimitOnOrganizationId], [TimeOfRestId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_1598628738_3_5_1_2] ON [dbo].[ListOfChilds]([IsDeleted], [LimitOnOrganizationId], [Id], [IsLast])
go

CREATE STATISTICS [_dta_stat_1598628738_1_12_3_5] ON [dbo].[ListOfChilds]([Id], [StateId], [IsDeleted], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_2_5_13_1] ON [dbo].[ListOfChilds]([IsLast], [LimitOnOrganizationId], [TourId], [Id])
go

CREATE STATISTICS [_dta_stat_1598628738_1_5_2_12] ON [dbo].[ListOfChilds]([Id], [LimitOnOrganizationId], [IsLast], [StateId])
go

CREATE STATISTICS [_dta_stat_1598628738_5_1_7_12] ON [dbo].[ListOfChilds]([LimitOnOrganizationId], [Id], [TimeOfRestId], [StateId])
go

CREATE STATISTICS [_dta_stat_1598628738_3_7_12_5] ON [dbo].[ListOfChilds]([IsDeleted], [TimeOfRestId], [StateId], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_3_2_12_5] ON [dbo].[ListOfChilds]([IsDeleted], [IsLast], [StateId], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_13_12_3_7] ON [dbo].[ListOfChilds]([TourId], [StateId], [IsDeleted], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_1598628738_1_7_3_12_5] ON [dbo].[ListOfChilds]([Id], [TimeOfRestId], [IsDeleted], [StateId], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_1_12_2_3_5] ON [dbo].[ListOfChilds]([Id], [StateId], [IsLast], [IsDeleted], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_13_1_7_12_5] ON [dbo].[ListOfChilds]([TourId], [Id], [TimeOfRestId], [StateId], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_12_14_5_6_7] ON [dbo].[ListOfChilds]([StateId], [ListOfChildsCategoryId], [LimitOnOrganizationId], [PlaceOfRestId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_1598628738_13_12_3_1_5] ON [dbo].[ListOfChilds]([TourId], [StateId], [IsDeleted], [Id], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_13_12_2_3_5] ON [dbo].[ListOfChilds]([TourId], [StateId], [IsLast], [IsDeleted], [LimitOnOrganizationId])
go

CREATE STATISTICS [_dta_stat_1598628738_12_1_13_7_3] ON [dbo].[ListOfChilds]([StateId], [Id], [TourId], [TimeOfRestId], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_1598628738_12_1_14_5_6_7] ON [dbo].[ListOfChilds]([StateId], [Id], [ListOfChildsCategoryId], [LimitOnOrganizationId], [PlaceOfRestId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_1598628738_12_13_14_5_6_7] ON [dbo].[ListOfChilds]([StateId], [TourId], [ListOfChildsCategoryId], [LimitOnOrganizationId], [PlaceOfRestId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_1598628738_5_13_7_3_12_1] ON [dbo].[ListOfChilds]([LimitOnOrganizationId], [TourId], [TimeOfRestId], [IsDeleted], [StateId], [Id])
go

CREATE STATISTICS [_dta_stat_1598628738_3_5_13_1_2_12] ON [dbo].[ListOfChilds]([IsDeleted], [LimitOnOrganizationId], [TourId], [Id], [IsLast], [StateId])
go

CREATE STATISTICS [_dta_stat_1598628738_13_1_12_14_5_6_7] ON [dbo].[ListOfChilds]([TourId], [Id], [StateId], [ListOfChildsCategoryId], [LimitOnOrganizationId], [PlaceOfRestId], [TimeOfRestId])
go

CREATE STATISTICS [_dta_stat_1015674666_2] ON [dbo].[ReportRowData]([SortOrder])
go

CREATE STATISTICS [_dta_stat_1015674666_8_7] ON [dbo].[ReportRowData]([ReportTableHeadId], [RowId])
go

CREATE STATISTICS [_dta_stat_1150627142_14_17] ON [dbo].[Tour]([StateId], [LimitOnVedomstvoId])
go

CREATE STATISTICS [_dta_stat_1150627142_1_22] ON [dbo].[Tour]([Id], [BoutId])
go

CREATE STATISTICS [_dta_stat_1150627142_14_1] ON [dbo].[Tour]([StateId], [Id])
go

CREATE STATISTICS [_dta_stat_1150627142_1_13] ON [dbo].[Tour]([Id], [HotelsId])
go

CREATE STATISTICS [_dta_stat_1150627142_7_9_4] ON [dbo].[Tour]([TypeOfRestId], [YearOfRestId], [IsActive])
go

CREATE STATISTICS [_dta_stat_1150627142_13_1_14] ON [dbo].[Tour]([HotelsId], [Id], [StateId])
go

CREATE STATISTICS [_dta_stat_1150627142_1_14_22] ON [dbo].[Tour]([Id], [StateId], [BoutId])
go

CREATE STATISTICS [_dta_stat_1150627142_1_17_14] ON [dbo].[Tour]([Id], [LimitOnVedomstvoId], [StateId])
go

CREATE STATISTICS [_dta_stat_1150627142_1_9_8_14] ON [dbo].[Tour]([Id], [YearOfRestId], [TimeOfRestId], [StateId])
go

CREATE STATISTICS [_dta_stat_1150627142_14_22_13_1] ON [dbo].[Tour]([StateId], [BoutId], [HotelsId], [Id])
go

CREATE STATISTICS [_dta_stat_1150627142_8_9_14_17] ON [dbo].[Tour]([TimeOfRestId], [YearOfRestId], [StateId], [LimitOnVedomstvoId])
go

CREATE STATISTICS [_dta_stat_1150627142_1_9_7_14_4] ON [dbo].[Tour]([Id], [YearOfRestId], [TypeOfRestId], [StateId], [IsActive])
go

CREATE STATISTICS [_dta_stat_1150627142_1_7_18_9_4] ON [dbo].[Tour]([Id], [TypeOfRestId], [ContractId], [YearOfRestId], [IsActive])
go

CREATE STATISTICS [_dta_stat_1150627142_9_1_17_8_14] ON [dbo].[Tour]([YearOfRestId], [Id], [LimitOnVedomstvoId], [TimeOfRestId], [StateId])
go

CREATE STATISTICS [_dta_stat_1150627142_18_1_9_7_14] ON [dbo].[Tour]([ContractId], [Id], [YearOfRestId], [TypeOfRestId], [StateId])
go

CREATE STATISTICS [_dta_stat_1150627142_1_17_9_7_14_4] ON [dbo].[Tour]([Id], [LimitOnVedomstvoId], [YearOfRestId], [TypeOfRestId], [StateId], [IsActive])
go

CREATE STATISTICS [_dta_stat_1150627142_9_7_14_4_18_1] ON [dbo].[Tour]([YearOfRestId], [TypeOfRestId], [StateId], [IsActive], [ContractId], [Id])
go

CREATE STATISTICS [_dta_stat_1150627142_17_18_1_9_7_14] ON [dbo].[Tour]([LimitOnVedomstvoId], [ContractId], [Id], [YearOfRestId], [TypeOfRestId], [StateId])
go

CREATE STATISTICS [_dta_stat_1150627142_9_7_14_4_18_17_1] ON [dbo].[Tour]([YearOfRestId], [TypeOfRestId], [StateId], [IsActive], [ContractId], [LimitOnVedomstvoId], [Id])
go

CREATE STATISTICS [_dta_stat_1035150733_6_5] ON [dbo].[History]([AccountId], [LinkId])
go

CREATE STATISTICS [_dta_stat_1035150733_5_1] ON [dbo].[History]([LinkId], [Id])
go

CREATE STATISTICS [_dta_stat_1307151702_17_1] ON [dbo].[Counselors]([StateId], [Id])
go

CREATE STATISTICS [_dta_stat_1307151702_11_17] ON [dbo].[Counselors]([Male], [StateId])
go

CREATE STATISTICS [_dta_stat_1118627028_28_1] ON [dbo].[Hotels]([StateId], [Id])
go

CREATE STATISTICS [_dta_stat_309576141_11_1] ON [dbo].[Organization]([IsVedomstvo], [Id])
go

CREATE STATISTICS [_dta_stat_309576141_1_9_11] ON [dbo].[Organization]([Id], [IsLast], [IsVedomstvo])
go

CREATE STATISTICS [_dta_stat_309576141_12_9_11_8] ON [dbo].[Organization]([ParentId], [IsLast], [IsVedomstvo], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_309576141_9_11_8_1] ON [dbo].[Organization]([IsLast], [IsVedomstvo], [IsDeleted], [Id])
go

CREATE STATISTICS [_dta_stat_309576141_13_1_9_12_11] ON [dbo].[Organization]([EntityId], [Id], [IsLast], [ParentId], [IsVedomstvo])
go

CREATE STATISTICS [_dta_stat_309576141_1_12_9_11_8] ON [dbo].[Organization]([Id], [ParentId], [IsLast], [IsVedomstvo], [IsDeleted])
go

CREATE STATISTICS [_dta_stat_309576141_13_9_12_11_8_1] ON [dbo].[Organization]([EntityId], [IsLast], [ParentId], [IsVedomstvo], [IsDeleted], [Id])
go

CREATE STATISTICS [_dta_stat_1143675122_1_6] ON [dbo].[ReportTableRow]([Id], [TableId])
go

CREATE STATISTICS [_dta_stat_1026102696_1_3] ON [dbo].[HistoryRequest]([Id], [OperationDate])
go

CREATE STATISTICS [_dta_stat_1026102696_6_5] ON [dbo].[HistoryRequest]([AccountId], [RequestId])
go

CREATE STATISTICS [_dta_stat_1026102696_5_1_3] ON [dbo].[HistoryRequest]([RequestId], [Id], [OperationDate])
go

CREATE STATISTICS [_dta_stat_599673184_7_1] ON [dbo].[RequestFile]([RequestId], [Id])
go

CREATE STATISTICS [_dta_stat_599673184_5_6_7] ON [dbo].[RequestFile]([CreateUserId], [RequestFileTypeId], [RequestId])
go

