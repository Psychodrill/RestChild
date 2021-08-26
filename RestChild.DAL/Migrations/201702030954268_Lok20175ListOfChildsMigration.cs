namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok20175ListOfChildsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListTravelers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SignNumber = c.String(maxLength: 1000),
                        SignDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Name = c.String(),
                        YearOfRestId = c.Long(),
                        LinkToFileId = c.Long(),
                        HistoryLinkId = c.Long(),
                        StateMachineStateId = c.Long(),
                        TypeOfRestId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.LinkToFile", t => t.LinkToFileId)
                .ForeignKey("dbo.StateMachineState", t => t.StateMachineStateId)
                .ForeignKey("dbo.TypeOfRest", t => t.TypeOfRestId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.LinkToFileId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.StateMachineStateId)
                .Index(t => t.TypeOfRestId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.ListTravelersRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsIncluded = c.Boolean(nullable: false),
                        DateInclude = c.DateTime(precision: 7, storeType: "datetime2"),
                        RequestId = c.Long(),
                        ListTravelersId = c.Long(),
                        StateMachineStateId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.StateMachineState", t => t.StateMachineStateId)
                .ForeignKey("dbo.ListTravelers", t => t.ListTravelersId)
                .Index(t => t.RequestId)
                .Index(t => t.ListTravelersId)
                .Index(t => t.StateMachineStateId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Applicant", "ProxyDateOfIssure", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "NotaryName", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "ProxyEndDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Applicant", "ProxyNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.Applicant", "IsProxy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "SsoId", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListTravelers", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.ListTravelers", "TypeOfRestId", "dbo.TypeOfRest");
            DropForeignKey("dbo.ListTravelers", "StateMachineStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.ListTravelersRequest", "ListTravelersId", "dbo.ListTravelers");
            DropForeignKey("dbo.ListTravelersRequest", "StateMachineStateId", "dbo.StateMachineState");
            DropForeignKey("dbo.ListTravelersRequest", "RequestId", "dbo.Request");
            DropForeignKey("dbo.ListTravelers", "LinkToFileId", "dbo.LinkToFile");
            DropForeignKey("dbo.ListTravelers", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.ListTravelersRequest", new[] { "EidSendStatus" });
            DropIndex("dbo.ListTravelersRequest", new[] { "Eid" });
            DropIndex("dbo.ListTravelersRequest", new[] { "StateMachineStateId" });
            DropIndex("dbo.ListTravelersRequest", new[] { "ListTravelersId" });
            DropIndex("dbo.ListTravelersRequest", new[] { "RequestId" });
            DropIndex("dbo.ListTravelers", new[] { "EidSendStatus" });
            DropIndex("dbo.ListTravelers", new[] { "Eid" });
            DropIndex("dbo.ListTravelers", new[] { "TypeOfRestId" });
            DropIndex("dbo.ListTravelers", new[] { "StateMachineStateId" });
            DropIndex("dbo.ListTravelers", new[] { "HistoryLinkId" });
            DropIndex("dbo.ListTravelers", new[] { "LinkToFileId" });
            DropIndex("dbo.ListTravelers", new[] { "YearOfRestId" });
            DropColumn("dbo.Request", "SsoId");
            DropColumn("dbo.Applicant", "IsProxy");
            DropColumn("dbo.Applicant", "ProxyNumber");
            DropColumn("dbo.Applicant", "ProxyEndDate");
            DropColumn("dbo.Applicant", "NotaryName");
            DropColumn("dbo.Applicant", "ProxyDateOfIssure");
            DropTable("dbo.ListTravelersRequest");
            DropTable("dbo.ListTravelers");
        }
    }
}
