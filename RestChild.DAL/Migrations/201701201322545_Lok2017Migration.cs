namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lok2017Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestEvent",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        EventCode = c.Guid(nullable: false),
                        Name = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RequestEventPlanied",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EventCode = c.Guid(nullable: false),
                        DateEvent = c.DateTime(precision: 7, storeType: "datetime2"),
                        Processed = c.Boolean(nullable: false),
                        DateCreate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PlanDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AccountId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RequestStatusChainForMpgu",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        YearOfRestId = c.Long(),
                        StatusActionId = c.Long(),
                        DeclineReasonId = c.Long(),
                        RequestEventId = c.Long(),
                        StatusId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeclineReason", t => t.DeclineReasonId)
                .ForeignKey("dbo.RequestEvent", t => t.RequestEventId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.StatusAction", t => t.StatusActionId)
                .ForeignKey("dbo.YearOfRest", t => t.YearOfRestId)
                .Index(t => t.YearOfRestId)
                .Index(t => t.StatusActionId)
                .Index(t => t.DeclineReasonId)
                .Index(t => t.RequestEventId)
                .Index(t => t.StatusId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            CreateTable(
                "dbo.RequestStatusForMpgu",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Status = c.Long(nullable: false),
                        ReasonCode = c.String(maxLength: 1000),
                        Commentary = c.String(),
                        Name = c.String(),
                        OrderField = c.Int(nullable: false),
                        ChainId = c.Long(),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequestStatusChainForMpgu", t => t.ChainId)
                .Index(t => t.ChainId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus);
            
            AddColumn("dbo.Applicant", "BenefitTypeId", c => c.Long());
            AddColumn("dbo.TypeOfRest", "ServiceCodeFirstCompany", c => c.String(maxLength: 1000));
            AddColumn("dbo.TypeOfRest", "MayBeMoney", c => c.Boolean(nullable: false));
            AddColumn("dbo.TypeOfRest", "NotChildren", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "IsFirstCompany", c => c.Boolean(nullable: false));
            AddColumn("dbo.Request", "RequestOnMoney", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Applicant", "BenefitTypeId");
            AddForeignKey("dbo.Applicant", "BenefitTypeId", "dbo.BenefitType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestStatusChainForMpgu", "YearOfRestId", "dbo.YearOfRest");
            DropForeignKey("dbo.RequestStatusForMpgu", "ChainId", "dbo.RequestStatusChainForMpgu");
            DropForeignKey("dbo.RequestStatusChainForMpgu", "StatusActionId", "dbo.StatusAction");
            DropForeignKey("dbo.RequestStatusChainForMpgu", "StatusId", "dbo.Status");
            DropForeignKey("dbo.RequestStatusChainForMpgu", "RequestEventId", "dbo.RequestEvent");
            DropForeignKey("dbo.RequestStatusChainForMpgu", "DeclineReasonId", "dbo.DeclineReason");
            DropForeignKey("dbo.RequestEventPlanied", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Applicant", "BenefitTypeId", "dbo.BenefitType");
            DropIndex("dbo.RequestStatusForMpgu", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestStatusForMpgu", new[] { "Eid" });
            DropIndex("dbo.RequestStatusForMpgu", new[] { "ChainId" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "Eid" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "StatusId" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "RequestEventId" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "DeclineReasonId" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "StatusActionId" });
            DropIndex("dbo.RequestStatusChainForMpgu", new[] { "YearOfRestId" });
            DropIndex("dbo.RequestEventPlanied", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestEventPlanied", new[] { "Eid" });
            DropIndex("dbo.RequestEventPlanied", new[] { "AccountId" });
            DropIndex("dbo.RequestEvent", new[] { "EidSendStatus" });
            DropIndex("dbo.RequestEvent", new[] { "Eid" });
            DropIndex("dbo.Applicant", new[] { "BenefitTypeId" });
            DropColumn("dbo.Request", "RequestOnMoney");
            DropColumn("dbo.Request", "IsFirstCompany");
            DropColumn("dbo.TypeOfRest", "NotChildren");
            DropColumn("dbo.TypeOfRest", "MayBeMoney");
            DropColumn("dbo.TypeOfRest", "ServiceCodeFirstCompany");
            DropColumn("dbo.Applicant", "BenefitTypeId");
            DropTable("dbo.RequestStatusForMpgu");
            DropTable("dbo.RequestStatusChainForMpgu");
            DropTable("dbo.RequestEventPlanied");
            DropTable("dbo.RequestEvent");
        }
    }
}
