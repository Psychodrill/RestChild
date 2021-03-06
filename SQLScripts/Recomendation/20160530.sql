use [RestChild]
go

SET ANSI_PADDING ON

go

CREATE NONCLUSTERED INDEX [_dta_index_Child_5_757577737__K60_K1_K89_K34_K88_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_29_] ON [dbo].[Child]
(
	[KeySame] ASC,
	[Id] ASC,
	[TypeOfGroupCheckId] ASC,
	[RequestId] ASC,
	[YearOfCompany] ASC
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
	[IsLast],
	[BenefitApproveRequestDate],
	[TourVolumeId],
	[ChildListId],
	[PaymentFileUrl],
	[PaymentFileTitle],
	[HaveMiddleName],
	[IsDeleted],
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
	[Payed],
	[BoutId],
	[NotComeInPlaceOfRest],
	[DocumentSeriaCertOfBirth],
	[DocumentNumberCertOfBirth],
	[KeyOther],
	[OfferInRequestId],
	[ForeginName],
	[ForeginLastName],
	[AmountOfCompensation],
	[CostOfRide],
	[RequestInformationVoucherId],
	[CostOfTour],
	[ToursId],
	[LastUpdateTick],
	[ContingentGuid],
	[EkisId],
	[EkisNeedSend],
	[DocumentTypeCertOfBirthId],
	[Inn],
	[Infant],
	[TypeViolationId],
	[Eid],
	[EidSendStatus],
	[EidSyncDate]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Request_5_917578307__K15_K1_K6_K7_K24] ON [dbo].[Request]
(
	[StatusId] ASC,
	[Id] ASC,
	[IsLast] ASC,
	[IsDeleted] ASC,
	[EntityId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_917578307_70_1_6_7_24_15] ON [dbo].[Request]([IsApplicantOrganization], [Id], [IsLast], [IsDeleted], [EntityId], [StatusId])
go

