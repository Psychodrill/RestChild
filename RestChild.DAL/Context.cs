namespace RestChild.DAL
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using Domain;

	internal partial class Context : DbContext
	{
		static Context()
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
		}

		public Context()
			: base("RestChild")
		{
		}

		public Context(string connString)
			: base(connString)
		{
		}

        /// <summary>
        ///     RestChild.Domain.AccessRight
        /// </summary>
        public DbSet<AccessRight> AccessRight { get; set; }

        /// <summary>
        ///     RestChild.Domain.Accommodation
        /// </summary>
        public DbSet<Accommodation> Accommodation { get; set; }

        /// <summary>
        ///     RestChild.Domain.AccommodationChildren
        /// </summary>
        public DbSet<AccommodationChildren> AccommodationChildren { get; set; }

        /// <summary>
        ///     RestChild.Domain.Account
        /// </summary>
        public DbSet<Account> Account { get; set; }

        /// <summary>
        ///     RestChild.Domain.AccountHistoryLogin
        /// </summary>
        public DbSet<AccountHistoryLogin> AccountHistoryLogin { get; set; }

        /// <summary>
        ///     RestChild.Domain.AccountRights
        /// </summary>
        public DbSet<AccountRights> AccountRights { get; set; }

        /// <summary>
        ///     RestChild.Domain.AccountRoles
        /// </summary>
        public DbSet<AccountRoles> AccountRoles { get; set; }

        /// <summary>
        ///     RestChild.Domain.AddonServices
        /// </summary>
        public DbSet<AddonServices> AddonServices { get; set; }

        /// <summary>
        ///     RestChild.Domain.AddonServicesLink
        /// </summary>
        public DbSet<AddonServicesLink> AddonServicesLink { get; set; }

        /// <summary>
        ///     RestChild.Domain.AddonServicesPaymentType
        /// </summary>
        public DbSet<AddonServicesPaymentType> AddonServicesPaymentType { get; set; }

        /// <summary>
        ///     RestChild.Domain.AddonServicesPhoto
        /// </summary>
        public DbSet<AddonServicesPhoto> AddonServicesPhoto { get; set; }

        /// <summary>
        ///     RestChild.Domain.AddonServicesPrice
        /// </summary>
        public DbSet<AddonServicesPrice> AddonServicesPrice { get; set; }

        /// <summary>
        ///     RestChild.Domain.Address
        /// </summary>
        public DbSet<Address> Address { get; set; }

        /// <summary>
        ///     RestChild.Domain.AdministratorTour
        /// </summary>
        public DbSet<AdministratorTour> AdministratorTour { get; set; }

        /// <summary>
        ///     RestChild.Domain.Applicant
        /// </summary>
        public DbSet<Applicant> Applicant { get; set; }

        /// <summary>
        ///     RestChild.Domain.AverageRestPrice
        /// </summary>
        public DbSet<AverageRestPrice> AverageRestPrice { get; set; }

        /// <summary>
        ///     RestChild.Domain.Beneficiaries
        /// </summary>
        public DbSet<Beneficiaries> Beneficiaries { get; set; }

        /// <summary>
        ///     RestChild.Domain.BenefitTypeERL
        /// </summary>
        public DbSet<BenefitTypeERL> BenefitTypeERL { get; set; }

        /// <summary>
        ///     RestChild.Domain.Booking
        /// </summary>
        public DbSet<Booking> Booking { get; set; }

        /// <summary>
        ///     RestChild.Domain.BookingCommercial
        /// </summary>
        public DbSet<BookingCommercial> BookingCommercial { get; set; }

        /// <summary>
        ///     RestChild.Domain.Bout
        /// </summary>
        public DbSet<Bout> Bout { get; set; }

        /// <summary>
        ///     RestChild.Domain.BoutAttendant
        /// </summary>
        public DbSet<BoutAttendant> BoutAttendant { get; set; }

        /// <summary>
        ///     RestChild.Domain.BoutJournal
        /// </summary>
        public DbSet<BoutJournal> BoutJournal { get; set; }

        /// <summary>
        ///     RestChild.Domain.BoutJournalFile
        /// </summary>
        public DbSet<BoutJournalFile> BoutJournalFile { get; set; }

        /// <summary>
        ///     RestChild.Domain.BoutJournalType
        /// </summary>
        public DbSet<BoutJournalType> BoutJournalType { get; set; }

        /// <summary>
        ///     RestChild.Domain.Calculation
        /// </summary>
        public DbSet<Calculation> Calculation { get; set; }

        /// <summary>
        ///     RestChild.Domain.CategoryIncident
        /// </summary>
        public DbSet<CategoryIncident> CategoryIncident { get; set; }

        /// <summary>
        ///     RestChild.Domain.Certificate
        /// </summary>
        public DbSet<Certificate> Certificate { get; set; }

        /// <summary>
        ///     RestChild.Domain.CertificateToApply
        /// </summary>
        public DbSet<CertificateToApply> CertificateToApply { get; set; }

        /// <summary>
        ///     RestChild.Domain.CertificateToApplyAccount
        /// </summary>
        public DbSet<CertificateToApplyAccount> CertificateToApplyAccount { get; set; }

        /// <summary>
        ///     RestChild.Domain.Child
        /// </summary>
        public DbSet<Child> Child { get; set; }

        /// <summary>
        ///     RestChild.Domain.ChildIncludeExcludeReason
        /// </summary>
        public DbSet<ChildIncludeExcludeReason> ChildIncludeExcludeReason { get; set; }

        /// <summary>
        ///     RestChild.Domain.ChildUniqe
        /// </summary>
        public DbSet<ChildUniqe> ChildUniqe { get; set; }

        /// <summary>
        ///     RestChild.Domain.City
        /// </summary>
        public DbSet<City> City { get; set; }

        /// <summary>
        ///     RestChild.Domain.ClothingSize
        /// </summary>
        public DbSet<ClothingSize> ClothingSize { get; set; }

        /// <summary>
        ///     RestChild.Domain.Contract
        /// </summary>
        public DbSet<Contract> Contract { get; set; }

        /// <summary>
        ///     RestChild.Domain.ContractAddonAgreement
        /// </summary>
        public DbSet<ContractAddonAgreement> ContractAddonAgreement { get; set; }

        /// <summary>
        ///     RestChild.Domain.CouncelorComment
        /// </summary>
        public DbSet<CouncelorComment> CouncelorComment { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorCource
        /// </summary>
        public DbSet<CounselorCource> CounselorCource { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorFile
        /// </summary>
        public DbSet<CounselorFile> CounselorFile { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorHighSchool
        /// </summary>
        public DbSet<CounselorHighSchool> CounselorHighSchool { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorPractice
        /// </summary>
        public DbSet<CounselorPractice> CounselorPractice { get; set; }

        /// <summary>
        ///     RestChild.Domain.Counselors
        /// </summary>
        public DbSet<Counselors> Counselors { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorSkill
        /// </summary>
        public DbSet<CounselorSkill> CounselorSkill { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorsStopListReason
        /// </summary>
        public DbSet<CounselorsStopListReason> CounselorsStopListReason { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTask
        /// </summary>
        public DbSet<CounselorTask> CounselorTask { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTaskCommentary
        /// </summary>
        public DbSet<CounselorTaskCommentary> CounselorTaskCommentary { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTaskExecutorType
        /// </summary>
        public DbSet<CounselorTaskExecutorType> CounselorTaskExecutorType { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTaskFile
        /// </summary>
        public DbSet<CounselorTaskFile> CounselorTaskFile { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTaskReportFile
        /// </summary>
        public DbSet<CounselorTaskReportFile> CounselorTaskReportFile { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTest
        /// </summary>
        public DbSet<CounselorTest> CounselorTest { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTestAnswer
        /// </summary>
        public DbSet<CounselorTestAnswer> CounselorTestAnswer { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTestAnswerVariant
        /// </summary>
        public DbSet<CounselorTestAnswerVariant> CounselorTestAnswerVariant { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTestQuestion
        /// </summary>
        public DbSet<CounselorTestQuestion> CounselorTestQuestion { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTestQuestionType
        /// </summary>
        public DbSet<CounselorTestQuestionType> CounselorTestQuestionType { get; set; }

        /// <summary>
        ///     RestChild.Domain.CounselorTestSubject
        /// </summary>
        public DbSet<CounselorTestSubject> CounselorTestSubject { get; set; }

        /// <summary>
        ///     RestChild.Domain.Country
        /// </summary>
        public DbSet<Country> Country { get; set; }

        /// <summary>
        ///     RestChild.Domain.DeletedRecord
        /// </summary>
        public DbSet<DeletedRecord> DeletedRecord { get; set; }

        /// <summary>
        ///     RestChild.Domain.DiningOptions
        /// </summary>
        public DbSet<DiningOptions> DiningOptions { get; set; }

        /// <summary>
        ///     RestChild.Domain.DirectoryFlights
        /// </summary>
        public DbSet<DirectoryFlights> DirectoryFlights { get; set; }

        /// <summary>
        ///     RestChild.Domain.Discount
        /// </summary>
        public DbSet<Discount> Discount { get; set; }

        /// <summary>
        ///     RestChild.Domain.DiscountCard
        /// </summary>
        public DbSet<DiscountCard> DiscountCard { get; set; }

        /// <summary>
        ///     RestChild.Domain.Drug
        /// </summary>
        public DbSet<Drug> Drug { get; set; }

        /// <summary>
        ///     RestChild.Domain.ERLBenefitStatus
        /// </summary>
        public DbSet<ERLBenefitStatus> ERLBenefitStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.ERLPersonStatus
        /// </summary>
        public DbSet<ERLPersonStatus> ERLPersonStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.EventGeography
        /// </summary>
        public DbSet<EventGeography> EventGeography { get; set; }

        /// <summary>
        ///     RestChild.Domain.FileOrLink
        /// </summary>
        public DbSet<FileOrLink> FileOrLink { get; set; }

        /// <summary>
        ///     RestChild.Domain.ForeginPassport
        /// </summary>
        public DbSet<ForeginPassport> ForeginPassport { get; set; }

        /// <summary>
        ///     RestChild.Domain.FormOfRest
        /// </summary>
        public DbSet<FormOfRest> FormOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.FunctioningType
        /// </summary>
        public DbSet<FunctioningType> FunctioningType { get; set; }

        /// <summary>
        ///     RestChild.Domain.GroupedTimeOfRest
        /// </summary>
        public DbSet<GroupedTimeOfRest> GroupedTimeOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.History
        /// </summary>
        public DbSet<History> History { get; set; }

        /// <summary>
        ///     RestChild.Domain.HistoryLink
        /// </summary>
        public DbSet<HistoryLink> HistoryLink { get; set; }

        /// <summary>
        ///     RestChild.Domain.HotelBlock
        /// </summary>
        public DbSet<HotelBlock> HotelBlock { get; set; }

        /// <summary>
        ///     RestChild.Domain.HotelBlockDate
        /// </summary>
        public DbSet<HotelBlockDate> HotelBlockDate { get; set; }

        /// <summary>
        ///     RestChild.Domain.HotelContactPerson
        /// </summary>
        public DbSet<HotelContactPerson> HotelContactPerson { get; set; }

        /// <summary>
        ///     RestChild.Domain.HotelPlacement
        /// </summary>
        public DbSet<HotelPlacement> HotelPlacement { get; set; }

        /// <summary>
        ///     RestChild.Domain.HotelPrice
        /// </summary>
        public DbSet<HotelPrice> HotelPrice { get; set; }

        /// <summary>
        ///     RestChild.Domain.LimitOnOrganizationRequest
        /// </summary>
        public DbSet<LimitOnOrganizationRequest> LimitOnOrganizationRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.LinkToFile
        /// </summary>
        public DbSet<LinkToFile> LinkToFile { get; set; }

        /// <summary>
        ///     RestChild.Domain.LinkToPeople
        /// </summary>
        public DbSet<LinkToPeople> LinkToPeople { get; set; }

        /// <summary>
        ///     RestChild.Domain.ListTravelers
        /// </summary>
        public DbSet<ListTravelers> ListTravelers { get; set; }

        /// <summary>
        ///     RestChild.Domain.ListTravelersRequest
        /// </summary>
        public DbSet<ListTravelersRequest> ListTravelersRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.ListTravelersRequestDetail
        /// </summary>
        public DbSet<ListTravelersRequestDetail> ListTravelersRequestDetail { get; set; }

        /// <summary>
        ///     RestChild.Domain.MatrialStatus
        /// </summary>
        public DbSet<MatrialStatus> MatrialStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTBookingVisit
        /// </summary>
        public DbSet<MGTBookingVisit> MGTBookingVisit { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTVisitBookingMPGUStatus
        /// </summary>
        public DbSet<MGTVisitBookingMPGUStatus> MGTVisitBookingMPGUStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTVisitBookingMPGUStatusModel
        /// </summary>
        public DbSet<MGTVisitBookingMPGUStatusModel> MGTVisitBookingMPGUStatusModel { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTVisitBookingPerson
        /// </summary>
        public DbSet<MGTVisitBookingPerson> MGTVisitBookingPerson { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTVisitBookingPersonType
        /// </summary>
        public DbSet<MGTVisitBookingPersonType> MGTVisitBookingPersonType { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTVisitBookingStatus
        /// </summary>
        public DbSet<MGTVisitBookingStatus> MGTVisitBookingStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTVisitTarget
        /// </summary>
        public DbSet<MGTVisitTarget> MGTVisitTarget { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTWindowWorkingPeriod
        /// </summary>
        public DbSet<MGTWindowWorkingPeriod> MGTWindowWorkingPeriod { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTDepartment
        /// </summary>
        public DbSet<MGTDepartment> MGTDepartment { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTWorkingDay
        /// </summary>
        public DbSet<MGTWorkingDay> MGTWorkingDay { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTWorkingDaysHistory
        /// </summary>
        public DbSet<MGTWorkingDaysHistory> MGTWorkingDaysHistory { get; set; }

        /// <summary>
        ///     RestChild.Domain.MGTWorkingDayWindow
        /// </summary>
        public DbSet<MGTWorkingDayWindow> MGTWorkingDayWindow { get; set; }

        /// <summary>
        ///     RestChild.Domain.MilitaryDuty
        /// </summary>
        public DbSet<MilitaryDuty> MilitaryDuty { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringChildrenNumberInformation
        /// </summary>
        public DbSet<MonitoringChildrenNumberInformation> MonitoringChildrenNumberInformation { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringFinancialData
        /// </summary>
        public DbSet<MonitoringFinancialData> MonitoringFinancialData { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringFinancialInformation
        /// </summary>
        public DbSet<MonitoringFinancialInformation> MonitoringFinancialInformation { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringFinancialSource
        /// </summary>
        public DbSet<MonitoringFinancialSource> MonitoringFinancialSource { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringGBU
        /// </summary>
        public DbSet<MonitoringGBU> MonitoringGBU { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringHotel
        /// </summary>
        public DbSet<MonitoringHotel> MonitoringHotel { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringHotelData
        /// </summary>
        public DbSet<MonitoringHotelData> MonitoringHotelData { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringSmallLeisureInfo
        /// </summary>
        public DbSet<MonitoringSmallLeisureInfo> MonitoringSmallLeisureInfo { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringSmallLeisureInfoData
        /// </summary>
        public DbSet<MonitoringSmallLeisureInfoData> MonitoringSmallLeisureInfoData { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringSmallLeisureInfoGBU
        /// </summary>
        public DbSet<MonitoringSmallLeisureInfoGBU> MonitoringSmallLeisureInfoGBU { get; set; }

        /// <summary>
        ///     RestChild.Domain.MonitoringTourData
        /// </summary>
        public DbSet<MonitoringTourData> MonitoringTourData { get; set; }

        /// <summary>
        ///     RestChild.Domain.NotNeedTicketReason
        /// </summary>
        public DbSet<NotNeedTicketReason> NotNeedTicketReason { get; set; }

        /// <summary>
        ///     RestChild.Domain.OfferInRequest
        /// </summary>
        public DbSet<OfferInRequest> OfferInRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.Okved
        /// </summary>
        public DbSet<Okved> Okved { get; set; }

        /// <summary>
        ///     RestChild.Domain.OrganisatorCollaborator
        /// </summary>
        public DbSet<OrganisatorCollaborator> OrganisatorCollaborator { get; set; }

        /// <summary>
        ///     RestChild.Domain.OrganizationBank
        /// </summary>
        public DbSet<OrganizationBank> OrganizationBank { get; set; }

        /// <summary>
        ///     RestChild.Domain.OrganizationCollaboratorPostType
        /// </summary>
        public DbSet<OrganizationCollaboratorPostType> OrganizationCollaboratorPostType { get; set; }

        /// <summary>
        ///     RestChild.Domain.OrphanageAddress
        /// </summary>
        public DbSet<OrphanageAddress> OrphanageAddress { get; set; }

        /// <summary>
        ///     RestChild.Domain.Party
        /// </summary>
        public DbSet<Party> Party { get; set; }

        /// <summary>
        ///     RestChild.Domain.Payment
        /// </summary>
        public DbSet<Payment> Payment { get; set; }

        /// <summary>
        ///     RestChild.Domain.PedParty
        /// </summary>
        public DbSet<PedParty> PedParty { get; set; }

        /// <summary>
        ///     RestChild.Domain.Person
        /// </summary>
        public DbSet<Person> Person { get; set; }

        /// <summary>
        ///     RestChild.Domain.PlaceOfRest
        /// </summary>
        public DbSet<PlaceOfRest> PlaceOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.PlaceOfRestTypeOfRest
        /// </summary>
        public DbSet<PlaceOfRestTypeOfRest> PlaceOfRestTypeOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.Product
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        ///     RestChild.Domain.Pupil
        /// </summary>
        public DbSet<Pupil> Pupil { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilDose
        /// </summary>
        public DbSet<PupilDose> PupilDose { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilGroup
        /// </summary>
        public DbSet<PupilGroup> PupilGroup { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilGroupListCollaborator
        /// </summary>
        public DbSet<PupilGroupListCollaborator> PupilGroupListCollaborator { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilGroupListMember
        /// </summary>
        public DbSet<PupilGroupListMember> PupilGroupListMember { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilGroupListMemberDrugDose
        /// </summary>
        public DbSet<PupilGroupListMemberDrugDose> PupilGroupListMemberDrugDose { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilGroupListTransfer
        /// </summary>
        public DbSet<PupilGroupListTransfer> PupilGroupListTransfer { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilGroupVacationPeriod
        /// </summary>
        public DbSet<PupilGroupVacationPeriod> PupilGroupVacationPeriod { get; set; }

        /// <summary>
        ///     RestChild.Domain.PupilsHealthStatus
        /// </summary>
        public DbSet<PupilsHealthStatus> PupilsHealthStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.Relative
        /// </summary>
        public DbSet<Relative> Relative { get; set; }

        /// <summary>
        ///     RestChild.Domain.RelativeUniqe
        /// </summary>
        public DbSet<RelativeUniqe> RelativeUniqe { get; set; }

        /// <summary>
        ///     RestChild.Domain.RelativeUniqeApplicant
        /// </summary>
        public DbSet<RelativeUniqeApplicant> RelativeUniqeApplicant { get; set; }

        /// <summary>
        ///     RestChild.Domain.ReportRowData
        /// </summary>
        public DbSet<ReportRowData> ReportRowData { get; set; }

        /// <summary>
        ///     RestChild.Domain.ReportSheet
        /// </summary>
        public DbSet<ReportSheet> ReportSheet { get; set; }

        /// <summary>
        ///     RestChild.Domain.ReportTable
        /// </summary>
        public DbSet<ReportTable> ReportTable { get; set; }

        /// <summary>
        ///     RestChild.Domain.ReportTableHead
        /// </summary>
        public DbSet<ReportTableHead> ReportTableHead { get; set; }

        /// <summary>
        ///     RestChild.Domain.ReportTableRow
        /// </summary>
        public DbSet<ReportTableRow> ReportTableRow { get; set; }

        /// <summary>
        ///     RestChild.Domain.RepresentInterest
        /// </summary>
        public DbSet<RepresentInterest> RepresentInterest { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestAccommodation
        /// </summary>
        public DbSet<RequestAccommodation> RequestAccommodation { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestAccommodationLink
        /// </summary>
        public DbSet<RequestAccommodationLink> RequestAccommodationLink { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestEvent
        /// </summary>
        public DbSet<RequestEvent> RequestEvent { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestEventPlanied
        /// </summary>
        public DbSet<RequestEventPlanied> RequestEventPlanied { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestFile
        /// </summary>
        public DbSet<RequestFile> RequestFile { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestFileType
        /// </summary>
        public DbSet<RequestFileType> RequestFileType { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestForPeriodOfRest
        /// </summary>
        public DbSet<RequestForPeriodOfRest> RequestForPeriodOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestInformationVoucher
        /// </summary>
        public DbSet<RequestInformationVoucher> RequestInformationVoucher { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestInformationVoucherAttendant
        /// </summary>
        public DbSet<RequestInformationVoucherAttendant> RequestInformationVoucherAttendant { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestPlaceOfRest
        /// </summary>
        public DbSet<RequestPlaceOfRest> RequestPlaceOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestService
        /// </summary>
        public DbSet<RequestService> RequestService { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestStatusChainForMpgu
        /// </summary>
        public DbSet<RequestStatusChainForMpgu> RequestStatusChainForMpgu { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestStatusCshedSendAndSignDocument
        /// </summary>
        public DbSet<RequestStatusCshedSendAndSignDocument> RequestStatusCshedSendAndSignDocument { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestStatusForMpgu
        /// </summary>
        public DbSet<RequestStatusForMpgu> RequestStatusForMpgu { get; set; }

        /// <summary>
        ///     RestChild.Domain.RequestsTimeOfRest
        /// </summary>
        public DbSet<RequestsTimeOfRest> RequestsTimeOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.ResponsibilityForTask
        /// </summary>
        public DbSet<ResponsibilityForTask> ResponsibilityForTask { get; set; }

        /// <summary>
        ///     RestChild.Domain.Agent
        /// </summary>
        public DbSet<Agent> Agent { get; set; }

        /// <summary>
        ///     RestChild.Domain.AnalyticsViewRow
        /// </summary>
        public DbSet<AnalyticsViewRow> AnalyticsViewRow { get; set; }

        /// <summary>
        ///     RestChild.Domain.AnalyticsViewRowType
        /// </summary>
        public DbSet<AnalyticsViewRowType> AnalyticsViewRowType { get; set; }

        /// <summary>
        ///     RestChild.Domain.ApplicantType
        /// </summary>
        public DbSet<ApplicantType> ApplicantType { get; set; }

        /// <summary>
        ///     RestChild.Domain.AttendantType
        /// </summary>
        public DbSet<AttendantType> AttendantType { get; set; }

        /// <summary>
        ///     RestChild.Domain.BenefitApproveType
        /// </summary>
        public DbSet<BenefitApproveType> BenefitApproveType { get; set; }

        /// <summary>
        ///     RestChild.Domain.BenefitGroupInvalid
        /// </summary>
        public DbSet<BenefitGroupInvalid> BenefitGroupInvalid { get; set; }

        /// <summary>
        ///     RestChild.Domain.BenefitType
        /// </summary>
        public DbSet<BenefitType> BenefitType { get; set; }

        /// <summary>
        ///     RestChild.Domain.BtiAddress
        /// </summary>
        public DbSet<BtiAddress> BtiAddress { get; set; }

        /// <summary>
        ///     RestChild.Domain.BtiDistrict
        /// </summary>
        public DbSet<BtiDistrict> BtiDistrict { get; set; }

        /// <summary>
        ///     RestChild.Domain.BtiRegion
        /// </summary>
        public DbSet<BtiRegion> BtiRegion { get; set; }

        /// <summary>
        ///     RestChild.Domain.BtiStreet
        /// </summary>
        public DbSet<BtiStreet> BtiStreet { get; set; }

        /// <summary>
        ///     RestChild.Domain.DeclineReason
        /// </summary>
        public DbSet<DeclineReason> DeclineReason { get; set; }

        /// <summary>
        ///     RestChild.Domain.DocumentType
        /// </summary>
        public DbSet<DocumentType> DocumentType { get; set; }

        /// <summary>
        ///     RestChild.Domain.ExchangeBaseRegistry
        /// </summary>
        public DbSet<ExchangeBaseRegistry> ExchangeBaseRegistry { get; set; }

        /// <summary>
        ///     RestChild.Domain.ExchangeBaseRegistryType
        /// </summary>
        public DbSet<ExchangeBaseRegistryType> ExchangeBaseRegistryType { get; set; }

        /// <summary>
        ///     RestChild.Domain.ExchangeUTS
        /// </summary>
        public DbSet<ExchangeUTS> ExchangeUTS { get; set; }

        /// <summary>
        ///     RestChild.Domain.ExcludeDays
        /// </summary>
        public DbSet<ExcludeDays> ExcludeDays { get; set; }

        /// <summary>
        ///     RestChild.Domain.FileHotel
        /// </summary>
        public DbSet<FileHotel> FileHotel { get; set; }

        /// <summary>
        ///     RestChild.Domain.FileOfTour
        /// </summary>
        public DbSet<FileOfTour> FileOfTour { get; set; }

        /// <summary>
        ///     RestChild.Domain.FileType
        /// </summary>
        public DbSet<FileType> FileType { get; set; }

        /// <summary>
        ///     RestChild.Domain.HistoryInteragencyRequest
        /// </summary>
        public DbSet<HistoryInteragencyRequest> HistoryInteragencyRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.HistoryRequest
        /// </summary>
        public DbSet<HistoryRequest> HistoryRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.Hotels
        /// </summary>
        public DbSet<Hotels> Hotels { get; set; }

        /// <summary>
        ///     RestChild.Domain.HotelType
        /// </summary>
        public DbSet<HotelType> HotelType { get; set; }

        /// <summary>
        ///     RestChild.Domain.InteragencyRequest
        /// </summary>
        public DbSet<InteragencyRequest> InteragencyRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.InteragencyRequestBenefitType
        /// </summary>
        public DbSet<InteragencyRequestBenefitType> InteragencyRequestBenefitType { get; set; }

        /// <summary>
        ///     RestChild.Domain.InteragencyRequestResult
        /// </summary>
        public DbSet<InteragencyRequestResult> InteragencyRequestResult { get; set; }

        /// <summary>
        ///     RestChild.Domain.LimitOnOrganization
        /// </summary>
        public DbSet<LimitOnOrganization> LimitOnOrganization { get; set; }

        /// <summary>
        ///     RestChild.Domain.LimitOnVedomstvo
        /// </summary>
        public DbSet<LimitOnVedomstvo> LimitOnVedomstvo { get; set; }

        /// <summary>
        ///     RestChild.Domain.ListOfChilds
        /// </summary>
        public DbSet<ListOfChilds> ListOfChilds { get; set; }

        /// <summary>
        ///     RestChild.Domain.ListOfChildsCategory
        /// </summary>
        public DbSet<ListOfChildsCategory> ListOfChildsCategory { get; set; }

        /// <summary>
        ///     RestChild.Domain.Organization
        /// </summary>
        public DbSet<Organization> Organization { get; set; }

        /// <summary>
        ///     RestChild.Domain.Request
        /// </summary>
        public DbSet<Request> Request { get; set; }

        /// <summary>
        ///     RestChild.Domain.RestrictionGroup
        /// </summary>
        public DbSet<RestrictionGroup> RestrictionGroup { get; set; }

        /// <summary>
        ///     RestChild.Domain.Role
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        ///     RestChild.Domain.RoomRates
        /// </summary>
        public DbSet<RoomRates> RoomRates { get; set; }

        /// <summary>
        ///     RestChild.Domain.ScheduleMessage
        /// </summary>
        public DbSet<ScheduleMessage> ScheduleMessage { get; set; }

        /// <summary>
        ///     RestChild.Domain.School
        /// </summary>
        public DbSet<School> School { get; set; }

        /// <summary>
        ///     RestChild.Domain.SearchFormSetting
        /// </summary>
        public DbSet<SearchFormSetting> SearchFormSetting { get; set; }

        /// <summary>
        ///     RestChild.Domain.SecurityJournal
        /// </summary>
        public DbSet<SecurityJournal> SecurityJournal { get; set; }

        /// <summary>
        ///     RestChild.Domain.SecurityJournalType
        /// </summary>
        public DbSet<SecurityJournalType> SecurityJournalType { get; set; }

        /// <summary>
        ///     RestChild.Domain.SecuritySetting
        /// </summary>
        public DbSet<SecuritySetting> SecuritySetting { get; set; }

        /// <summary>
        ///     RestChild.Domain.SendEmailAndSms
        /// </summary>
        public DbSet<SendEmailAndSms> SendEmailAndSms { get; set; }

        /// <summary>
        ///     RestChild.Domain.SendEmailAndSmsAttachment
        /// </summary>
        public DbSet<SendEmailAndSmsAttachment> SendEmailAndSmsAttachment { get; set; }

        /// <summary>
        ///     RestChild.Domain.ServiceBlock
        /// </summary>
        public DbSet<ServiceBlock> ServiceBlock { get; set; }

        /// <summary>
        ///     RestChild.Domain.ServiceBlockDate
        /// </summary>
        public DbSet<ServiceBlockDate> ServiceBlockDate { get; set; }

        /// <summary>
        ///     RestChild.Domain.SignInfo
        /// </summary>
        public DbSet<SignInfo> SignInfo { get; set; }

        /// <summary>
        ///     RestChild.Domain.Skill
        /// </summary>
        public DbSet<Skill> Skill { get; set; }

        /// <summary>
        ///     RestChild.Domain.SkillsGroup
        /// </summary>
        public DbSet<SkillsGroup> SkillsGroup { get; set; }

        /// <summary>
        ///     RestChild.Domain.SkillVocabulary
        /// </summary>
        public DbSet<SkillVocabulary> SkillVocabulary { get; set; }

        /// <summary>
        ///     RestChild.Domain.SmallLeisureSubtype
        /// </summary>
        public DbSet<SmallLeisureSubtype> SmallLeisureSubtype { get; set; }

        /// <summary>
        ///     RestChild.Domain.SmallLeisureType
        /// </summary>
        public DbSet<SmallLeisureType> SmallLeisureType { get; set; }

        /// <summary>
        ///     RestChild.Domain.Source
        /// </summary>
        public DbSet<Source> Source { get; set; }

        /// <summary>
        ///     RestChild.Domain.StateDistrict
        /// </summary>
        public DbSet<StateDistrict> StateDistrict { get; set; }

        /// <summary>
        ///     RestChild.Domain.StateMachine
        /// </summary>
        public DbSet<StateMachine> StateMachine { get; set; }

        /// <summary>
        ///     RestChild.Domain.StateMachineAction
        /// </summary>
        public DbSet<StateMachineAction> StateMachineAction { get; set; }

        /// <summary>
        ///     RestChild.Domain.StateMachineFromStatus
        /// </summary>
        public DbSet<StateMachineFromStatus> StateMachineFromStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.StateMachineState
        /// </summary>
        public DbSet<StateMachineState> StateMachineState { get; set; }

        /// <summary>
        ///     RestChild.Domain.Status
        /// </summary>
        public DbSet<Status> Status { get; set; }

        /// <summary>
        ///     RestChild.Domain.StatusAction
        /// </summary>
        public DbSet<StatusAction> StatusAction { get; set; }

        /// <summary>
        ///     RestChild.Domain.StatusByChild
        /// </summary>
        public DbSet<StatusByChild> StatusByChild { get; set; }

        /// <summary>
        ///     RestChild.Domain.StatusInteragencyRequest
        /// </summary>
        public DbSet<StatusInteragencyRequest> StatusInteragencyRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.StatusResult
        /// </summary>
        public DbSet<StatusResult> StatusResult { get; set; }

        /// <summary>
        ///     RestChild.Domain.SubjectOfRest
        /// </summary>
        public DbSet<SubjectOfRest> SubjectOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.SubjectOfRestClassification
        /// </summary>
        public DbSet<SubjectOfRestClassification> SubjectOfRestClassification { get; set; }

        /// <summary>
        ///     RestChild.Domain.Tag
        /// </summary>
        public DbSet<Tag> Tag { get; set; }

        /// <summary>
        ///     RestChild.Domain.Ticket
        /// </summary>
        public DbSet<Ticket> Ticket { get; set; }

        /// <summary>
        ///     RestChild.Domain.TicketLink
        /// </summary>
        public DbSet<TicketLink> TicketLink { get; set; }

        /// <summary>
        ///     RestChild.Domain.TieColor
        /// </summary>
        public DbSet<TieColor> TieColor { get; set; }

        /// <summary>
        ///     RestChild.Domain.TimeOfRest
        /// </summary>
        public DbSet<TimeOfRest> TimeOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.Tour
        /// </summary>
        public DbSet<Tour> Tour { get; set; }

        /// <summary>
        ///     RestChild.Domain.TourAccommodation
        /// </summary>
        public DbSet<TourAccommodation> TourAccommodation { get; set; }

        /// <summary>
        ///     RestChild.Domain.TourCountry
        /// </summary>
        public DbSet<TourCountry> TourCountry { get; set; }

        /// <summary>
        ///     RestChild.Domain.TourPrice
        /// </summary>
        public DbSet<TourPrice> TourPrice { get; set; }

        /// <summary>
        ///     RestChild.Domain.TourTransport
        /// </summary>
        public DbSet<TourTransport> TourTransport { get; set; }

        /// <summary>
        ///     RestChild.Domain.TourTransportPrice
        /// </summary>
        public DbSet<TourTransportPrice> TourTransportPrice { get; set; }

        /// <summary>
        ///     RestChild.Domain.TourVolume
        /// </summary>
        public DbSet<TourVolume> TourVolume { get; set; }

        /// <summary>
        ///     RestChild.Domain.TradeUnionCamper
        /// </summary>
        public DbSet<TradeUnionCamper> TradeUnionCamper { get; set; }

        /// <summary>
        ///     RestChild.Domain.TradeUnionCamperPrivilegePart
        /// </summary>
        public DbSet<TradeUnionCamperPrivilegePart> TradeUnionCamperPrivilegePart { get; set; }

        /// <summary>
        ///     RestChild.Domain.TradeUnionList
        /// </summary>
        public DbSet<TradeUnionList> TradeUnionList { get; set; }

        /// <summary>
        ///     RestChild.Domain.TradeUnionPersonCheck
        /// </summary>
        public DbSet<TradeUnionPersonCheck> TradeUnionPersonCheck { get; set; }

        /// <summary>
        ///     RestChild.Domain.TradeUnionStatusByChild
        /// </summary>
        public DbSet<TradeUnionStatusByChild> TradeUnionStatusByChild { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselors
        /// </summary>
        public DbSet<TrainingCounselors> TrainingCounselors { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsGroupTest
        /// </summary>
        public DbSet<TrainingCounselorsGroupTest> TrainingCounselorsGroupTest { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsPlace
        /// </summary>
        public DbSet<TrainingCounselorsPlace> TrainingCounselorsPlace { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsResult
        /// </summary>
        public DbSet<TrainingCounselorsResult> TrainingCounselorsResult { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsResultStatus
        /// </summary>
        public DbSet<TrainingCounselorsResultStatus> TrainingCounselorsResultStatus { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsTest
        /// </summary>
        public DbSet<TrainingCounselorsTest> TrainingCounselorsTest { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsTime
        /// </summary>
        public DbSet<TrainingCounselorsTime> TrainingCounselorsTime { get; set; }

        /// <summary>
        ///     RestChild.Domain.TrainingCounselorsType
        /// </summary>
        public DbSet<TrainingCounselorsType> TrainingCounselorsType { get; set; }

        /// <summary>
        ///     RestChild.Domain.TransportInfo
        /// </summary>
        public DbSet<TransportInfo> TransportInfo { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfCalculation
        /// </summary>
        public DbSet<TypeOfCalculation> TypeOfCalculation { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfCamp
        /// </summary>
        public DbSet<TypeOfCamp> TypeOfCamp { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfDrug
        /// </summary>
        public DbSet<TypeOfDrug> TypeOfDrug { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfEducation
        /// </summary>
        public DbSet<TypeOfEducation> TypeOfEducation { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfGroupCheck
        /// </summary>
        public DbSet<TypeOfGroupCheck> TypeOfGroupCheck { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfLimitList
        /// </summary>
        public DbSet<TypeOfLimitList> TypeOfLimitList { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfLinkPeople
        /// </summary>
        public DbSet<TypeOfLinkPeople> TypeOfLinkPeople { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfRest
        /// </summary>
        public DbSet<TypeOfRest> TypeOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfRestBenefitRestriction
        /// </summary>
        public DbSet<TypeOfRestBenefitRestriction> TypeOfRestBenefitRestriction { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfRestERL
        /// </summary>
        public DbSet<TypeOfRestERL> TypeOfRestERL { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfRestriction
        /// </summary>
        public DbSet<TypeOfRestriction> TypeOfRestriction { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfRestSubtype
        /// </summary>
        public DbSet<TypeOfRestSubtype> TypeOfRestSubtype { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfRooms
        /// </summary>
        public DbSet<TypeOfRooms> TypeOfRooms { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfService
        /// </summary>
        public DbSet<TypeOfService> TypeOfService { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfSubRestriction
        /// </summary>
        public DbSet<TypeOfSubRestriction> TypeOfSubRestriction { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfTransfer
        /// </summary>
        public DbSet<TypeOfTransfer> TypeOfTransfer { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfTransport
        /// </summary>
        public DbSet<TypeOfTransport> TypeOfTransport { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeOfTransportInRequest
        /// </summary>
        public DbSet<TypeOfTransportInRequest> TypeOfTransportInRequest { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypePriceCalculation
        /// </summary>
        public DbSet<TypePriceCalculation> TypePriceCalculation { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeRequestInformationVoucher
        /// </summary>
        public DbSet<TypeRequestInformationVoucher> TypeRequestInformationVoucher { get; set; }

        /// <summary>
        ///     RestChild.Domain.TypeViolation
        /// </summary>
        public DbSet<TypeViolation> TypeViolation { get; set; }

        /// <summary>
        ///     RestChild.Domain.YearOfRest
        /// </summary>
        public DbSet<YearOfRest> YearOfRest { get; set; }

        /// <summary>
        ///     RestChild.Domain.Numerator
        /// </summary>
        public DbSet<Numerator> Numerator { get; set; }

        /// <summary>
        ///     RestChild.Domain.LeisureFacilities
        /// </summary>
       // public DbSet<LeisureFacilities> leisureFacilities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(32, 4));
			modelBuilder.Properties<decimal>().Where(e => e.Name  == "Latitude" || e.Name == "Longitude").Configure(c => c.HasPrecision(32, 10));
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Entity<Numerator>().HasKey(t => new { t.Id, t.Key });
			modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
			modelBuilder.Entity<Organization>().HasOptional(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId);
			modelBuilder.Entity<Organization>().HasOptional(x => x.Entity).WithMany().HasForeignKey(x => x.EntityId);
			modelBuilder.Entity<ReportRowData>().HasRequired(x => x.Row).WithMany(t => t.RowData).HasForeignKey(d => d.RowId).WillCascadeOnDelete(true);
			modelBuilder.Entity<ReportTableRow>().HasRequired(x => x.Table).WithMany(t => t.Rows).HasForeignKey(d => d.TableId).WillCascadeOnDelete(true);
			modelBuilder.Entity<ReportTableHead>().HasRequired(x => x.ReportTable).WithMany(t => t.ReportTableHeads).HasForeignKey(d => d.ReportTableId).WillCascadeOnDelete(true);
			modelBuilder.Entity<ReportTable>().HasRequired(x => x.ReportSheet).WithMany(t => t.ReportTables).HasForeignKey(d => d.ReportSheetId).WillCascadeOnDelete(true);
			modelBuilder.Entity<TourVolume>().HasOptional(x => x.TourAccommodation).WithMany(t => t.Volumes).HasForeignKey(d => d.TourAccommodationId).WillCascadeOnDelete(true);
			modelBuilder.Entity<RoomRates>().HasOptional(x => x.TourAccommodation).WithMany(t => t.RoomRates).HasForeignKey(d => d.TourAccommodationId).WillCascadeOnDelete(true);
			modelBuilder.Properties().Where(e => e.Name == "RowVersion").Configure(c => c.IsRowVersion());
		}
	}
}
