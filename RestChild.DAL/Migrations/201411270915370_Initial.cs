namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessRight",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(maxLength: 1000),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 1000),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Password = c.String(nullable: false, maxLength: 1000),
                        Salt = c.String(nullable: false, maxLength: 1000),
                        DateCreate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        DateUpdate = c.DateTime(),
                        Email = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        ShortName = c.String(maxLength: 1000),
                        ExternalUid = c.String(maxLength: 1000),
                        Inn = c.String(maxLength: 1000),
                        Kpp = c.String(maxLength: 1000),
                        Ogrn = c.String(maxLength: 1000),
                        IsDeleted = c.Boolean(nullable: false),
                        IsLast = c.Boolean(nullable: false),
                        VedomstvoId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vedomstvo", t => t.VedomstvoId)
                .Index(t => t.VedomstvoId);
            
            CreateTable(
                "dbo.Vedomstvo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        ShortName = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        Appartment = c.String(maxLength: 1000),
                        Street = c.String(maxLength: 1000),
                        House = c.String(maxLength: 1000),
                        Corpus = c.String(maxLength: 1000),
                        Stroenie = c.String(maxLength: 1000),
                        BtiAddressId = c.Long(),
                        BtiDistrictId = c.Long(),
                        BtiRegionId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BtiAddress", t => t.BtiAddressId)
                .ForeignKey("dbo.BtiDistrict", t => t.BtiDistrictId)
                .ForeignKey("dbo.BtiRegion", t => t.BtiRegionId)
                .Index(t => t.BtiAddressId)
                .Index(t => t.BtiDistrictId)
                .Index(t => t.BtiRegionId);
            
            CreateTable(
                "dbo.BtiAddress",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FullAddress = c.String(maxLength: 1000),
                        Unom = c.Long(nullable: false),
                        ShortAddress = c.String(maxLength: 1000),
                        Unod = c.Long(nullable: false),
                        Status = c.Long(nullable: false),
                        BtiDistrictId = c.Long(),
                        BtiRegionId = c.Long(),
                        BtiStreetId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BtiDistrict", t => t.BtiDistrictId)
                .ForeignKey("dbo.BtiRegion", t => t.BtiRegionId)
                .ForeignKey("dbo.BtiStreet", t => t.BtiStreetId)
                .Index(t => t.BtiDistrictId)
                .Index(t => t.BtiRegionId)
                .Index(t => t.BtiStreetId);
            
            CreateTable(
                "dbo.BtiDistrict",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BtiRegion",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        BtiDistrictId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BtiDistrict", t => t.BtiDistrictId)
                .Index(t => t.BtiDistrictId);
            
            CreateTable(
                "dbo.BtiStreet",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        ProxyDateOfIssure = c.DateTime(),
                        NotaryName = c.String(maxLength: 1000),
                        ProxyEndDate = c.DateTime(),
                        ProxyNumber = c.String(maxLength: 1000),
                        Snils = c.String(maxLength: 1000),
                        DocumentTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId)
                .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                        Owner = c.String(maxLength: 1000),
                        GlobalUid = c.String(maxLength: 1000),
                        ForChild = c.Boolean(nullable: false),
                        ForForeign = c.Boolean(nullable: false),
                        ForOther = c.Boolean(nullable: false),
                        BaseRegistryUid = c.String(),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.AnalyticsViewRow",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        ByDay = c.Int(nullable: false),
                        ByHour = c.Int(nullable: false),
                        ByWeek = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Applicant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        Snils = c.String(maxLength: 1000),
                        DocumentTypeId = c.Long(),
                        ApplicantTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicantType", t => t.ApplicantTypeId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.ApplicantTypeId);
            
            CreateTable(
                "dbo.ApplicantType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Attendant",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        Phone = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        RequestId = c.Long(),
                        DocumentTypeId = c.Long(),
                        EntityId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId)
                .ForeignKey("dbo.Attendant", t => t.EntityId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.RequestId)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Child",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        DateOfBirth = c.DateTime(),
                        BenefitDate = c.DateTime(),
                        BenefitNeverEnd = c.Boolean(nullable: false),
                        BenefitEndDate = c.DateTime(),
                        BenefitNumber = c.String(maxLength: 1000),
                        BenefitSubjectIssue = c.String(maxLength: 1000),
                        BenefitDateOfIssure = c.DateTime(),
                        ForeginSeria = c.String(maxLength: 1000),
                        ForeginNumber = c.String(maxLength: 1000),
                        ForeginSubjectIssue = c.String(),
                        ForeginDateOfIssue = c.DateTime(),
                        ForeginDateEnd = c.DateTime(),
                        SchoolNotPresent = c.Boolean(nullable: false),
                        RegisteredInMoscow = c.Boolean(nullable: false),
                        Male = c.Boolean(nullable: false),
                        BenefitApprove = c.Boolean(nullable: false),
                        BenefitApproveComment = c.String(),
                        BenefitApproveHtml = c.String(),
                        BenefitRequestNumber = c.String(maxLength: 1000),
                        BenefitRequestDate = c.DateTime(),
                        BenefitAnswerNumber = c.String(maxLength: 1000),
                        BenefitAnswerDate = c.DateTime(),
                        BenefitAnswerComment = c.String(),
                        Snils = c.String(maxLength: 1000),
                        BenefitRequestComment = c.String(),
                        RequestId = c.Long(),
                        SchoolId = c.Long(),
                        DocumentTypeId = c.Long(),
                        BenefitTypeId = c.Long(),
                        AddressId = c.Long(),
                        BenefitDocTypeId = c.Long(),
                        ForeginTypeId = c.Long(),
                        StatusByChildId = c.Long(),
                        AttendantId = c.Long(),
                        EntityId = c.Long(),
                        BenefitApproveTypeId = c.Long(),
                        TypeOfRestrictionId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.BenefitApproveType", t => t.BenefitApproveTypeId)
                .ForeignKey("dbo.DocumentType", t => t.BenefitDocTypeId)
                .ForeignKey("dbo.BenefitType", t => t.BenefitTypeId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId)
                .ForeignKey("dbo.Child", t => t.EntityId)
                .ForeignKey("dbo.DocumentType", t => t.ForeginTypeId)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.School", t => t.SchoolId)
                .ForeignKey("dbo.StatusByChild", t => t.StatusByChildId)
                .ForeignKey("dbo.TypeOfRestriction", t => t.TypeOfRestrictionId)
                .ForeignKey("dbo.Attendant", t => t.AttendantId)
                .Index(t => t.RequestId)
                .Index(t => t.SchoolId)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.BenefitTypeId)
                .Index(t => t.AddressId)
                .Index(t => t.BenefitDocTypeId)
                .Index(t => t.ForeginTypeId)
                .Index(t => t.StatusByChildId)
                .Index(t => t.AttendantId)
                .Index(t => t.EntityId)
                .Index(t => t.BenefitApproveTypeId)
                .Index(t => t.TypeOfRestrictionId);
            
            CreateTable(
                "dbo.ExchangeBaseRegistry",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RequestGuid = c.String(maxLength: 1000),
                        ServiceNumber = c.String(maxLength: 1000),
                        SendDate = c.DateTime(),
                        AcknolegmentGuid = c.String(maxLength: 1000),
                        ResponseGuid = c.String(maxLength: 1000),
                        ResponseDate = c.DateTime(),
                        OperationType = c.String(maxLength: 1000),
                        IsIncoming = c.Boolean(nullable: false),
                        RequestText = c.String(),
                        ResponseText = c.String(),
                        IsProcessed = c.Boolean(nullable: false),
                        ChildId = c.Long(),
                        ExchangeBaseRegistryTypeId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExchangeBaseRegistryType", t => t.ExchangeBaseRegistryTypeId)
                .ForeignKey("dbo.Child", t => t.ChildId)
                .Index(t => t.ChildId)
                .Index(t => t.ExchangeBaseRegistryTypeId);
            
            CreateTable(
                "dbo.ExchangeBaseRegistryType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BenefitApproveType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BenefitType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        ExnternalUid = c.String(maxLength: 1000),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AgentApplicant = c.Boolean(),
                        RequestNumber = c.String(maxLength: 1000),
                        DateRequest = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        IsLast = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Version = c.Int(),
                        ExternalUid = c.String(maxLength: 1000),
                        ExternalSystem = c.String(maxLength: 1000),
                        MainPlaces = c.Int(),
                        AdditionalPlaces = c.Int(),
                        IsDraft = c.Boolean(nullable: false),
                        RequestCurrentPeriodId = c.Long(),
                        StatusId = c.Long(),
                        ApplicantId = c.Long(),
                        TypeOfRestId = c.Long(),
                        TimeOfRestId = c.Long(),
                        SubjectOfRestId = c.Long(),
                        AttendantTypeId = c.Long(),
                        PlaceOfRestAddonId = c.Long(),
                        PlaceOfRestId = c.Long(),
                        AgentId = c.Long(),
                        EntityId = c.Long(),
                        CreateUserId = c.Long(),
                        VedomstvoId = c.Long(),
                        OrganizationId = c.Long(),
                        SourceId = c.Long(),
                        DeclineReasonId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agent", t => t.AgentId)
                .ForeignKey("dbo.Applicant", t => t.ApplicantId)
                .ForeignKey("dbo.AttendantType", t => t.AttendantTypeId)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .ForeignKey("dbo.DeclineReason", t => t.DeclineReasonId)
                .ForeignKey("dbo.Request", t => t.EntityId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestAddonId)
                .ForeignKey("dbo.RequestCurrentPeriod", t => t.RequestCurrentPeriodId)
                .ForeignKey("dbo.Source", t => t.SourceId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.SubjectOfRest", t => t.SubjectOfRestId)
                .ForeignKey("dbo.TimeOfRest", t => t.TimeOfRestId)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .ForeignKey("dbo.Vedomstvo", t => t.VedomstvoId)
                .Index(t => t.RequestCurrentPeriodId)
                .Index(t => t.StatusId)
                .Index(t => t.ApplicantId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.TimeOfRestId)
                .Index(t => t.SubjectOfRestId)
                .Index(t => t.AttendantTypeId)
                .Index(t => t.PlaceOfRestAddonId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.AgentId)
                .Index(t => t.EntityId)
                .Index(t => t.CreateUserId)
                .Index(t => t.VedomstvoId)
                .Index(t => t.OrganizationId)
                .Index(t => t.SourceId)
                .Index(t => t.DeclineReasonId);
            
            CreateTable(
                "dbo.AttendantType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeclineReason",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlaceOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(),
                        PhotoUrl = c.String(maxLength: 1000),
                        IsForegin = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        PriceBasePlace = c.Decimal(precision: 18, scale: 2),
                        PriceAddonPlace = c.Decimal(precision: 18, scale: 2),
                        ZoneOfSea = c.Boolean(nullable: false),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.RequestCurrentPeriod",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        IsClosed = c.Boolean(nullable: false),
                        DateFirstStage = c.DateTime(),
                        DateSecondStage = c.DateTime(),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.Source",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        ExternalUid = c.String(maxLength: 1000),
                        MpguName = c.String(nullable: false, maxLength: 1000),
                        MpguDescription = c.String(),
                        MpguComment = c.String(),
                        IsFinal = c.Boolean(nullable: false),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.StatusAction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 1000),
                        Name = c.String(nullable: false, maxLength: 1000),
                        ToStatusId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.ToStatusId)
                .Index(t => t.ToStatusId);
            
            CreateTable(
                "dbo.SubjectOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Description = c.String(),
                        PhotoUrl = c.String(maxLength: 1000),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.TimeOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        FactorDependence = c.Decimal(precision: 18, scale: 2),
                        TypeOfRestId = c.Long(),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.TypeOfRest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        ServiceCode = c.String(maxLength: 1000),
                        ForMPGU = c.Boolean(nullable: false),
                        ParentId = c.Long(),
                        CreateUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.CreateUserId)
                .ForeignKey("dbo.TypeOfRest", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.CreateUserId);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusByChild",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfRestriction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExchangeUTS",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(),
                        Processed = c.Boolean(nullable: false),
                        QueueName = c.String(maxLength: 1000),
                        DateCreate = c.DateTime(nullable: false),
                        Incoming = c.Boolean(nullable: false),
                        FromOrgCode = c.String(maxLength: 1000),
                        ToOrgCode = c.String(maxLength: 1000),
                        MessageId = c.String(maxLength: 1000),
                        ServiceNumber = c.String(maxLength: 1000),
                        RequestId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .Index(t => t.RequestId);
            
            CreateTable(
                "dbo.Numerator",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Key = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.Key });
            
            CreateTable(
                "dbo.AccountOrganization",
                c => new
                    {
                        Account_Id = c.Long(nullable: false),
                        Organization_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_Id, t.Organization_Id })
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.Organization_Id, cascadeDelete: true)
                .Index(t => t.Account_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.AccountRole",
                c => new
                    {
                        Account_Id = c.Long(nullable: false),
                        Role_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_Id, t.Role_Id })
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Account_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.AccountVedomstvo",
                c => new
                    {
                        Account_Id = c.Long(nullable: false),
                        Vedomstvo_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_Id, t.Vedomstvo_Id })
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .ForeignKey("dbo.Vedomstvo", t => t.Vedomstvo_Id, cascadeDelete: true)
                .Index(t => t.Account_Id)
                .Index(t => t.Vedomstvo_Id);
            
            CreateTable(
                "dbo.AccessRightAccount",
                c => new
                    {
                        AccessRight_Id = c.Long(nullable: false),
                        Account_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccessRight_Id, t.Account_Id })
                .ForeignKey("dbo.AccessRight", t => t.AccessRight_Id, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.AccessRight_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.AccessRightRole",
                c => new
                    {
                        AccessRight_Id = c.Long(nullable: false),
                        Role_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccessRight_Id, t.Role_Id })
                .ForeignKey("dbo.AccessRight", t => t.AccessRight_Id, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.AccessRight_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.StatusStatusAction",
                c => new
                    {
                        Status_Id = c.Long(nullable: false),
                        StatusAction_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Status_Id, t.StatusAction_Id })
                .ForeignKey("dbo.Status", t => t.Status_Id, cascadeDelete: true)
                .ForeignKey("dbo.StatusAction", t => t.StatusAction_Id, cascadeDelete: true)
                .Index(t => t.Status_Id)
                .Index(t => t.StatusAction_Id);            			
        }
       
		
 
        public override void Down()
        {
            DropForeignKey("dbo.ExchangeUTS", "RequestId", "dbo.Request");
            DropForeignKey("dbo.Attendant", "RequestId", "dbo.Request");
            DropForeignKey("dbo.Attendant", "EntityId", "dbo.Attendant");
            DropForeignKey("dbo.Attendant", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Child", "AttendantId", "dbo.Attendant");
            DropForeignKey("dbo.Child", "TypeOfRestrictionId", "dbo.TypeOfRestriction");
            DropForeignKey("dbo.Child", "StatusByChildId", "dbo.StatusByChild");
            DropForeignKey("dbo.Child", "SchoolId", "dbo.School");
            DropForeignKey("dbo.Child", "RequestId", "dbo.Request");
            DropForeignKey("dbo.Request", "VedomstvoId", "dbo.Vedomstvo");
            DropForeignKey("dbo.Request", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.Request", "TimeOfRestId", "dbo.TimeOfRest");
            DropForeignKey("dbo.TimeOfRest", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TypeOfRest", "ParentId", "dbo.TypeOfRest");
            DropForeignKey("dbo.TypeOfRest", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.TimeOfRest", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Request", "SubjectOfRestId", "dbo.SubjectOfRest");
            DropForeignKey("dbo.SubjectOfRest", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Request", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Status", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.StatusStatusAction", "StatusAction_Id", "dbo.StatusAction");
            DropForeignKey("dbo.StatusStatusAction", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.StatusAction", "ToStatusId", "dbo.Status");
            DropForeignKey("dbo.Request", "SourceId", "dbo.Source");
            DropForeignKey("dbo.Request", "RequestCurrentPeriodId", "dbo.RequestCurrentPeriod");
            DropForeignKey("dbo.RequestCurrentPeriod", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Request", "PlaceOfRestAddonId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.Request", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.PlaceOfRest", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Request", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Request", "EntityId", "dbo.Request");
            DropForeignKey("dbo.Request", "DeclineReasonId", "dbo.DeclineReason");
            DropForeignKey("dbo.Request", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Request", "AttendantTypeId", "dbo.AttendantType");
            DropForeignKey("dbo.Request", "ApplicantId", "dbo.Applicant");
            DropForeignKey("dbo.Request", "AgentId", "dbo.Agent");
            DropForeignKey("dbo.Child", "ForeginTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Child", "EntityId", "dbo.Child");
            DropForeignKey("dbo.Child", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Child", "BenefitTypeId", "dbo.BenefitType");
            DropForeignKey("dbo.BenefitType", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Child", "BenefitDocTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Child", "BenefitApproveTypeId", "dbo.BenefitApproveType");
            DropForeignKey("dbo.ExchangeBaseRegistry", "ChildId", "dbo.Child");
            DropForeignKey("dbo.ExchangeBaseRegistry", "ExchangeBaseRegistryTypeId", "dbo.ExchangeBaseRegistryType");
            DropForeignKey("dbo.Child", "AddressId", "dbo.Address");
            DropForeignKey("dbo.Applicant", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.Applicant", "ApplicantTypeId", "dbo.ApplicantType");
            DropForeignKey("dbo.ApplicantType", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Agent", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.DocumentType", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.Address", "BtiRegionId", "dbo.BtiRegion");
            DropForeignKey("dbo.Address", "BtiDistrictId", "dbo.BtiDistrict");
            DropForeignKey("dbo.Address", "BtiAddressId", "dbo.BtiAddress");
            DropForeignKey("dbo.BtiAddress", "BtiStreetId", "dbo.BtiStreet");
            DropForeignKey("dbo.BtiAddress", "BtiRegionId", "dbo.BtiRegion");
            DropForeignKey("dbo.BtiRegion", "BtiDistrictId", "dbo.BtiDistrict");
            DropForeignKey("dbo.BtiAddress", "BtiDistrictId", "dbo.BtiDistrict");
            DropForeignKey("dbo.AccessRightRole", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.AccessRightRole", "AccessRight_Id", "dbo.AccessRight");
            DropForeignKey("dbo.AccessRightAccount", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.AccessRightAccount", "AccessRight_Id", "dbo.AccessRight");
            DropForeignKey("dbo.AccountVedomstvo", "Vedomstvo_Id", "dbo.Vedomstvo");
            DropForeignKey("dbo.AccountVedomstvo", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.AccountRole", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.AccountRole", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.Role", "CreateUserId", "dbo.Account");
            DropForeignKey("dbo.AccountOrganization", "Organization_Id", "dbo.Organization");
            DropForeignKey("dbo.AccountOrganization", "Account_Id", "dbo.Account");
            DropForeignKey("dbo.Organization", "VedomstvoId", "dbo.Vedomstvo");
            DropForeignKey("dbo.Account", "CreateUserId", "dbo.Account");
            DropIndex("dbo.StatusStatusAction", new[] { "StatusAction_Id" });
            DropIndex("dbo.StatusStatusAction", new[] { "Status_Id" });
            DropIndex("dbo.AccessRightRole", new[] { "Role_Id" });
            DropIndex("dbo.AccessRightRole", new[] { "AccessRight_Id" });
            DropIndex("dbo.AccessRightAccount", new[] { "Account_Id" });
            DropIndex("dbo.AccessRightAccount", new[] { "AccessRight_Id" });
            DropIndex("dbo.AccountVedomstvo", new[] { "Vedomstvo_Id" });
            DropIndex("dbo.AccountVedomstvo", new[] { "Account_Id" });
            DropIndex("dbo.AccountRole", new[] { "Role_Id" });
            DropIndex("dbo.AccountRole", new[] { "Account_Id" });
            DropIndex("dbo.AccountOrganization", new[] { "Organization_Id" });
            DropIndex("dbo.AccountOrganization", new[] { "Account_Id" });
            DropIndex("dbo.ExchangeUTS", new[] { "RequestId" });
            DropIndex("dbo.TypeOfRest", new[] { "CreateUserId" });
            DropIndex("dbo.TypeOfRest", new[] { "ParentId" });
            DropIndex("dbo.TimeOfRest", new[] { "CreateUserId" });
            DropIndex("dbo.TimeOfRest", new[] { "TypeOfRestId" });
            DropIndex("dbo.SubjectOfRest", new[] { "CreateUserId" });
            DropIndex("dbo.StatusAction", new[] { "ToStatusId" });
            DropIndex("dbo.Status", new[] { "CreateUserId" });
            DropIndex("dbo.RequestCurrentPeriod", new[] { "CreateUserId" });
            DropIndex("dbo.PlaceOfRest", new[] { "CreateUserId" });
            DropIndex("dbo.Request", new[] { "DeclineReasonId" });
            DropIndex("dbo.Request", new[] { "SourceId" });
            DropIndex("dbo.Request", new[] { "OrganizationId" });
            DropIndex("dbo.Request", new[] { "VedomstvoId" });
            DropIndex("dbo.Request", new[] { "CreateUserId" });
            DropIndex("dbo.Request", new[] { "EntityId" });
            DropIndex("dbo.Request", new[] { "AgentId" });
            DropIndex("dbo.Request", new[] { "PlaceOfRestId" });
            DropIndex("dbo.Request", new[] { "PlaceOfRestAddonId" });
            DropIndex("dbo.Request", new[] { "AttendantTypeId" });
            DropIndex("dbo.Request", new[] { "SubjectOfRestId" });
            DropIndex("dbo.Request", new[] { "TimeOfRestId" });
            DropIndex("dbo.Request", new[] { "TypeOfRestId" });
            DropIndex("dbo.Request", new[] { "ApplicantId" });
            DropIndex("dbo.Request", new[] { "StatusId" });
            DropIndex("dbo.Request", new[] { "RequestCurrentPeriodId" });
            DropIndex("dbo.BenefitType", new[] { "CreateUserId" });
            DropIndex("dbo.ExchangeBaseRegistry", new[] { "ExchangeBaseRegistryTypeId" });
            DropIndex("dbo.ExchangeBaseRegistry", new[] { "ChildId" });
            DropIndex("dbo.Child", new[] { "TypeOfRestrictionId" });
            DropIndex("dbo.Child", new[] { "BenefitApproveTypeId" });
            DropIndex("dbo.Child", new[] { "EntityId" });
            DropIndex("dbo.Child", new[] { "AttendantId" });
            DropIndex("dbo.Child", new[] { "StatusByChildId" });
            DropIndex("dbo.Child", new[] { "ForeginTypeId" });
            DropIndex("dbo.Child", new[] { "BenefitDocTypeId" });
            DropIndex("dbo.Child", new[] { "AddressId" });
            DropIndex("dbo.Child", new[] { "BenefitTypeId" });
            DropIndex("dbo.Child", new[] { "DocumentTypeId" });
            DropIndex("dbo.Child", new[] { "SchoolId" });
            DropIndex("dbo.Child", new[] { "RequestId" });
            DropIndex("dbo.Attendant", new[] { "EntityId" });
            DropIndex("dbo.Attendant", new[] { "DocumentTypeId" });
            DropIndex("dbo.Attendant", new[] { "RequestId" });
            DropIndex("dbo.ApplicantType", new[] { "CreateUserId" });
            DropIndex("dbo.Applicant", new[] { "ApplicantTypeId" });
            DropIndex("dbo.Applicant", new[] { "DocumentTypeId" });
            DropIndex("dbo.DocumentType", new[] { "CreateUserId" });
            DropIndex("dbo.Agent", new[] { "DocumentTypeId" });
            DropIndex("dbo.BtiRegion", new[] { "BtiDistrictId" });
            DropIndex("dbo.BtiAddress", new[] { "BtiStreetId" });
            DropIndex("dbo.BtiAddress", new[] { "BtiRegionId" });
            DropIndex("dbo.BtiAddress", new[] { "BtiDistrictId" });
            DropIndex("dbo.Address", new[] { "BtiRegionId" });
            DropIndex("dbo.Address", new[] { "BtiDistrictId" });
            DropIndex("dbo.Address", new[] { "BtiAddressId" });
            DropIndex("dbo.Role", new[] { "CreateUserId" });
            DropIndex("dbo.Organization", new[] { "VedomstvoId" });
            DropIndex("dbo.Account", new[] { "CreateUserId" });
            DropTable("dbo.StatusStatusAction");
            DropTable("dbo.AccessRightRole");
            DropTable("dbo.AccessRightAccount");
            DropTable("dbo.AccountVedomstvo");
            DropTable("dbo.AccountRole");
            DropTable("dbo.AccountOrganization");
            DropTable("dbo.Numerator");
            DropTable("dbo.ExchangeUTS");
            DropTable("dbo.TypeOfRestriction");
            DropTable("dbo.StatusByChild");
            DropTable("dbo.School");
            DropTable("dbo.TypeOfRest");
            DropTable("dbo.TimeOfRest");
            DropTable("dbo.SubjectOfRest");
            DropTable("dbo.StatusAction");
            DropTable("dbo.Status");
            DropTable("dbo.Source");
            DropTable("dbo.RequestCurrentPeriod");
            DropTable("dbo.PlaceOfRest");
            DropTable("dbo.DeclineReason");
            DropTable("dbo.AttendantType");
            DropTable("dbo.Request");
            DropTable("dbo.BenefitType");
            DropTable("dbo.BenefitApproveType");
            DropTable("dbo.ExchangeBaseRegistryType");
            DropTable("dbo.ExchangeBaseRegistry");
            DropTable("dbo.Child");
            DropTable("dbo.Attendant");
            DropTable("dbo.ApplicantType");
            DropTable("dbo.Applicant");
            DropTable("dbo.AnalyticsViewRow");
            DropTable("dbo.DocumentType");
            DropTable("dbo.Agent");
            DropTable("dbo.BtiStreet");
            DropTable("dbo.BtiRegion");
            DropTable("dbo.BtiDistrict");
            DropTable("dbo.BtiAddress");
            DropTable("dbo.Address");
            DropTable("dbo.Role");
            DropTable("dbo.Vedomstvo");
            DropTable("dbo.Organization");
            DropTable("dbo.Account");
            DropTable("dbo.AccessRight");
        }
    }
}
