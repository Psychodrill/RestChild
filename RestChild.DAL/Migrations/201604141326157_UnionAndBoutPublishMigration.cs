namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnionAndBoutPublishMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastName = c.String(maxLength: 1000),
                        FirstName = c.String(maxLength: 1000),
                        MiddleName = c.String(maxLength: 1000),
                        HaveMiddleName = c.Boolean(nullable: false),
                        DocumentSeria = c.String(maxLength: 1000),
                        DocumentNumber = c.String(maxLength: 1000),
                        DocumentDateOfIssue = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocumentSubjectIssue = c.String(maxLength: 1000),
                        DateOfBirth = c.DateTime(precision: 7, storeType: "datetime2"),
                        Male = c.Boolean(nullable: false),
                        PlaceOfBirth = c.String(maxLength: 1000),
                        Snils = c.String(),
                        Phone = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TradeUnionCamper",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AddressChild = c.String(maxLength: 1000),
                        School = c.String(maxLength: 1000),
                        ParentPlaceWork = c.String(maxLength: 1000),
                        IsParentUnionist = c.Boolean(nullable: false),
                        IsRelativeUnionist = c.Boolean(nullable: false),
                        Summa = c.Decimal(precision: 32, scale: 4),
                        SummaParent = c.Decimal(precision: 32, scale: 4),
                        SummaTradeUnion = c.Decimal(precision: 32, scale: 4),
                        SummaBudget = c.Decimal(precision: 32, scale: 4),
                        SummaOrganization = c.Decimal(precision: 32, scale: 4),
                        RelativePlaceWork = c.String(maxLength: 1000),
                        ChildId = c.Long(),
                        ParentId = c.Long(),
                        TradeUnionId = c.Long(),
                        TradeUnionStatusByChildId = c.Long(),
                        UnionistId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.ChildId)
                .ForeignKey("dbo.Person", t => t.ParentId)
                .ForeignKey("dbo.TradeUnionList", t => t.TradeUnionId)
                .ForeignKey("dbo.TradeUnionStatusByChild", t => t.TradeUnionStatusByChildId)
                .ForeignKey("dbo.Person", t => t.UnionistId)
                .Index(t => t.ChildId)
                .Index(t => t.ParentId)
                .Index(t => t.TradeUnionId)
                .Index(t => t.TradeUnionStatusByChildId)
                .Index(t => t.UnionistId);
            
            CreateTable(
                "dbo.TradeUnionList",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        YearOfRestId = c.Long(),
                        HistoryLinkId = c.Long(),
                        CampId = c.Long(),
                        TradeUnionId = c.Long(),
                        GroupedTimeOfRestId = c.Long(),
                        StateId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organization", t => t.CampId)
                .ForeignKey("dbo.GroupedTimeOfRest", t => t.GroupedTimeOfRestId)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.Organization", t => t.TradeUnionId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.CampId)
                .Index(t => t.TradeUnionId)
                .Index(t => t.GroupedTimeOfRestId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.TradeUnionStatusByChild",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Organization", "IsTradeUnion", c => c.Boolean(nullable: false));
            AddColumn("dbo.Organization", "IsHotel", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bout", "IsPublishOnSite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradeUnionCamper", "UnionistId", "dbo.Person");
            DropForeignKey("dbo.TradeUnionCamper", "TradeUnionStatusByChildId", "dbo.TradeUnionStatusByChild");
            DropForeignKey("dbo.TradeUnionCamper", "TradeUnionId", "dbo.TradeUnionList");
            DropForeignKey("dbo.TradeUnionList", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.TradeUnionList", "TradeUnionId", "dbo.Organization");
            DropForeignKey("dbo.TradeUnionList", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.TradeUnionList", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.TradeUnionList", "GroupedTimeOfRestId", "dbo.GroupedTimeOfRest");
            DropForeignKey("dbo.TradeUnionList", "CampId", "dbo.Organization");
            DropForeignKey("dbo.TradeUnionCamper", "ParentId", "dbo.Person");
            DropForeignKey("dbo.TradeUnionCamper", "ChildId", "dbo.Person");
            DropIndex("dbo.TradeUnionList", new[] { "StateId" });
            DropIndex("dbo.TradeUnionList", new[] { "GroupedTimeOfRestId" });
            DropIndex("dbo.TradeUnionList", new[] { "TradeUnionId" });
            DropIndex("dbo.TradeUnionList", new[] { "CampId" });
            DropIndex("dbo.TradeUnionList", new[] { "HistoryLinkId" });
            DropIndex("dbo.TradeUnionList", new[] { "YearOfRestId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "UnionistId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "TradeUnionStatusByChildId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "TradeUnionId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "ParentId" });
            DropIndex("dbo.TradeUnionCamper", new[] { "ChildId" });
            DropColumn("dbo.Bout", "IsPublishOnSite");
            DropColumn("dbo.Organization", "IsHotel");
            DropColumn("dbo.Organization", "IsTradeUnion");
            DropTable("dbo.TradeUnionStatusByChild");
            DropTable("dbo.TradeUnionList");
            DropTable("dbo.TradeUnionCamper");
            DropTable("dbo.Person");
        }
    }
}
