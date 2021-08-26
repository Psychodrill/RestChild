namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LimitRequestMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LimitOnOrganizationRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Volume = c.Int(nullable: false),
                        ApprovedVolume = c.Int(),
                        DateFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        Comment = c.String(),
                        Name = c.String(maxLength: 1000),
                        HistoryLinkId = c.Long(),
                        OrganizationId = c.Long(),
                        PlaceOfRestId = c.Long(),
                        LimitOnVedomstvoId = c.Long(),
                        StateId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.PlaceOfRest", t => t.PlaceOfRestId)
                .ForeignKey("dbo.StateMachineState", t => t.StateId)
                .ForeignKey("dbo.LimitOnVedomstvo", t => t.LimitOnVedomstvoId)
                .Index(t => t.HistoryLinkId)
                .Index(t => t.OrganizationId)
                .Index(t => t.PlaceOfRestId)
                .Index(t => t.LimitOnVedomstvoId)
                .Index(t => t.StateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LimitOnOrganizationRequest", "LimitOnVedomstvoId", "dbo.LimitOnVedomstvo");
            DropForeignKey("dbo.LimitOnOrganizationRequest", "StateId", "dbo.StateMachineState");
            DropForeignKey("dbo.LimitOnOrganizationRequest", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropForeignKey("dbo.LimitOnOrganizationRequest", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.LimitOnOrganizationRequest", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "StateId" });
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "LimitOnVedomstvoId" });
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "PlaceOfRestId" });
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "OrganizationId" });
            DropIndex("dbo.LimitOnOrganizationRequest", new[] { "HistoryLinkId" });
            DropTable("dbo.LimitOnOrganizationRequest");
        }
    }
}
