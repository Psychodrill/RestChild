namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115918Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonitoringGBU",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortName = c.String(nullable: false, maxLength: 1000),
                        FullName = c.String(maxLength: 1000),
                        FactAddress = c.String(maxLength: 1000),
                        AddressId = c.Long(),
                        OrganisationId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.Organization", t => t.OrganisationId)
                .Index(t => t.AddressId)
                .Index(t => t.OrganisationId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringSmallLeisureInfoData",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastUploadData = c.DateTime(precision: 7, storeType: "datetime2"),
                        ChildrenCountPost = c.Int(),
                        ChildernCountCovered = c.Int(),
                        MoneyOutcome = c.Decimal(precision: 32, scale: 4),
                        NoteOne = c.String(maxLength: 1000),
                        NoteTwo = c.String(maxLength: 1000),
                        NoteThree = c.String(maxLength: 1000),
                        MonitoringSmallLeisureInfoId = c.Long(),
                        GBUId = c.Long(),
                        SmallLeisureTypeId = c.Long(),
                        SmallLeisureSubtypeId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MonitoringSmallLeisureInfo", t => t.MonitoringSmallLeisureInfoId)
                .ForeignKey("dbo.SmallLeisureSubtype", t => t.SmallLeisureSubtypeId)
                .ForeignKey("dbo.SmallLeisureType", t => t.SmallLeisureTypeId)
                .ForeignKey("dbo.MonitoringGBU", t => t.GBUId)
                .Index(t => t.MonitoringSmallLeisureInfoId)
                .Index(t => t.GBUId)
                .Index(t => t.SmallLeisureTypeId)
                .Index(t => t.SmallLeisureSubtypeId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringSmallLeisureInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        YearOfRestId = c.Long(),
                        HistoryLinkId = c.Long(),
                        StateId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.StateId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.SmallLeisureSubtype",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        SmallLeisureTypeId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SmallLeisureType", t => t.SmallLeisureTypeId)
                .Index(t => t.SmallLeisureTypeId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.SmallLeisureType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        MergeSubtypes = c.Boolean(nullable: false, defaultValue: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringChildrenNumberInformation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HistoryLinkId = c.Long(),
                        StateId = c.Long(),
                        OrganisationId = c.Long(),
                        YearOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Organization", t => t.OrganisationId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.StateId)
                .Index(t => t.OrganisationId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringHotelData",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChildrenNumberInformationId = c.Long(),
                        HotelId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MonitoringHotel", t => t.HotelId)
                .ForeignKey("dbo.MonitoringChildrenNumberInformation", t => t.ChildrenNumberInformationId)
                .Index(t => t.ChildrenNumberInformationId)
                .Index(t => t.HotelId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringHotel",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShortName = c.String(nullable: false, maxLength: 1000),
                        FullName = c.String(maxLength: 1000),
                        FactAddress = c.String(maxLength: 1000),
                        Inn = c.String(maxLength: 1000),
                        AddressId = c.Long(),
                        PlaceOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .Index(t => t.AddressId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringTourData",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateIn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateOut = c.DateTime(precision: 7, storeType: "datetime2"),
                        PlanChildrenCount = c.Int(),
                        FactChildrenCount = c.Int(),
                        HotelDataId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MonitoringHotelData", t => t.HotelDataId)
                .Index(t => t.HotelDataId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringFinanceInformation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StateId = c.Long(),
                        OrganisationId = c.Long(),
                        HistoryLinkId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Organization", t => t.OrganisationId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .Index(t => t.StateId)
                .Index(t => t.OrganisationId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringFinantialData",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FinanceType = c.Int(nullable: false),
                        Plan = c.Decimal(precision: 32, scale: 4),
                        Jan = c.Decimal(precision: 32, scale: 4),
                        Feb = c.Decimal(precision: 32, scale: 4),
                        Mar = c.Decimal(precision: 32, scale: 4),
                        Apr = c.Decimal(precision: 32, scale: 4),
                        May = c.Decimal(precision: 32, scale: 4),
                        Jun = c.Decimal(precision: 32, scale: 4),
                        Jul = c.Decimal(precision: 32, scale: 4),
                        Aug = c.Decimal(precision: 32, scale: 4),
                        Sep = c.Decimal(precision: 32, scale: 4),
                        Oct = c.Decimal(precision: 32, scale: 4),
                        Nov = c.Decimal(precision: 32, scale: 4),
                        Dec = c.Decimal(precision: 32, scale: 4),
                        FinanceInformationId = c.Long(),
                        MonitoringFinancialSourceId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MonitoringFinancialSource", t => t.MonitoringFinancialSourceId)
                .ForeignKey("dbo.MonitoringFinanceInformation", t => t.FinanceInformationId)
                .Index(t => t.FinanceInformationId)
                .Index(t => t.MonitoringFinancialSourceId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.MonitoringFinancialSource",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        ForDevelopment = c.Boolean(nullable: false, defaultValue: false),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Organization", "IsInMonitoring", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonitoringFinanceInformation", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.MonitoringFinanceInformation", "OrganisationId", "dbo.Organization");
            DropForeignKey("dbo.MonitoringFinanceInformation", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.MonitoringFinantialData", "FinanceInformationId", "dbo.MonitoringFinanceInformation");
            DropForeignKey("dbo.MonitoringFinantialData", "MonitoringFinancialSourceId", "dbo.MonitoringFinancialSource");
            DropForeignKey("dbo.MonitoringChildrenNumberInformation", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.MonitoringChildrenNumberInformation", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.MonitoringChildrenNumberInformation", "OrganisationId", "dbo.Organization");
            DropForeignKey("dbo.MonitoringHotelData", "ChildrenNumberInformationId", "dbo.MonitoringChildrenNumberInformation");
            DropForeignKey("dbo.MonitoringTourData", "HotelDataId", "dbo.MonitoringHotelData");
            DropForeignKey("dbo.MonitoringHotel", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.MonitoringHotelData", "HotelId", "dbo.MonitoringHotel");
            DropForeignKey("dbo.MonitoringHotel", "AddressId", "dbo.Address");
            DropForeignKey("dbo.MonitoringChildrenNumberInformation", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.MonitoringGBU", "OrganisationId", "dbo.Organization");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "GBUId", "dbo.MonitoringGBU");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "SmallLeisureTypeId", "dbo.SmallLeisureType");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "SmallLeisureSubtypeId", "dbo.SmallLeisureSubtype");
            DropForeignKey("dbo.SmallLeisureSubtype", "SmallLeisureTypeId", "dbo.SmallLeisureType");
            DropForeignKey("dbo.MonitoringSmallLeisureInfo", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.MonitoringSmallLeisureInfo", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.MonitoringSmallLeisureInfoData", "MonitoringSmallLeisureInfoId", "dbo.MonitoringSmallLeisureInfo");
            DropForeignKey("dbo.MonitoringSmallLeisureInfo", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.MonitoringGBU", "AddressId", "dbo.Address");
            DropIndex("dbo.MonitoringFinancialSource", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringFinancialSource", new[] { "Eid" });
            DropIndex("dbo.MonitoringFinantialData", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringFinantialData", new[] { "Eid" });
            DropIndex("dbo.MonitoringFinantialData", new[] { "MonitoringFinancialSourceId" });
            DropIndex("dbo.MonitoringFinantialData", new[] { "FinanceInformationId" });
            DropIndex("dbo.MonitoringFinanceInformation", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringFinanceInformation", new[] { "Eid" });
            DropIndex("dbo.MonitoringFinanceInformation", new[] { "HistoryLinkId" });
            DropIndex("dbo.MonitoringFinanceInformation", new[] { "OrganisationId" });
            DropIndex("dbo.MonitoringFinanceInformation", new[] { "StateId" });
            DropIndex("dbo.MonitoringTourData", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringTourData", new[] { "Eid" });
            DropIndex("dbo.MonitoringTourData", new[] { "HotelDataId" });
            DropIndex("dbo.MonitoringHotel", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringHotel", new[] { "Eid" });
            DropIndex("dbo.MonitoringHotel", new[] { "PlaceOfRestId" });
            DropIndex("dbo.MonitoringHotel", new[] { "AddressId" });
            DropIndex("dbo.MonitoringHotelData", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringHotelData", new[] { "Eid" });
            DropIndex("dbo.MonitoringHotelData", new[] { "HotelId" });
            DropIndex("dbo.MonitoringHotelData", new[] { "ChildrenNumberInformationId" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "Eid" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "YearOfRestId" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "OrganisationId" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "StateId" });
            DropIndex("dbo.MonitoringChildrenNumberInformation", new[] { "HistoryLinkId" });
            DropIndex("dbo.SmallLeisureType", new[] { "EidSendStatus" });
            DropIndex("dbo.SmallLeisureType", new[] { "Eid" });
            DropIndex("dbo.SmallLeisureSubtype", new[] { "EidSendStatus" });
            DropIndex("dbo.SmallLeisureSubtype", new[] { "Eid" });
            DropIndex("dbo.SmallLeisureSubtype", new[] { "SmallLeisureTypeId" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "Eid" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "StateId" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "HistoryLinkId" });
            DropIndex("dbo.MonitoringSmallLeisureInfo", new[] { "YearOfRestId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "Eid" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "SmallLeisureSubtypeId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "SmallLeisureTypeId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "GBUId" });
            DropIndex("dbo.MonitoringSmallLeisureInfoData", new[] { "MonitoringSmallLeisureInfoId" });
            DropIndex("dbo.MonitoringGBU", new[] { "EidSendStatus" });
            DropIndex("dbo.MonitoringGBU", new[] { "Eid" });
            DropIndex("dbo.MonitoringGBU", new[] { "OrganisationId" });
            DropIndex("dbo.MonitoringGBU", new[] { "AddressId" });
            DropColumn("dbo.Organization", "IsInMonitoring");
            DropTable("dbo.MonitoringFinancialSource");
            DropTable("dbo.MonitoringFinantialData");
            DropTable("dbo.MonitoringFinanceInformation");
            DropTable("dbo.MonitoringTourData");
            DropTable("dbo.MonitoringHotel");
            DropTable("dbo.MonitoringHotelData");
            DropTable("dbo.MonitoringChildrenNumberInformation");
            DropTable("dbo.SmallLeisureType");
            DropTable("dbo.SmallLeisureSubtype");
            DropTable("dbo.MonitoringSmallLeisureInfo");
            DropTable("dbo.MonitoringSmallLeisureInfoData");
            DropTable("dbo.MonitoringGBU");
        }
    }
}
