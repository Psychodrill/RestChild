namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Leisure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeisureFacilities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Abbreviated = c.String(),
                        Fullname = c.String(),
                        ActualAdress = c.String(),
                        Inn = c.String(maxLength: 1000),
                        LastUpdateTick = c.Long(nullable: false),
                        Eid = c.Long(),
                        EidSendStatus = c.Long(),
                        EidSyncDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        StateDistrictId = c.Long(),
                        HistoryLinkId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLink", t => t.HistoryLinkId)
                .ForeignKey("dbo.StateDistrict", t => t.StateDistrictId)
                .Index(t => t.Eid)
                .Index(t => t.EidSendStatus)
                .Index(t => t.StateDistrictId)
                .Index(t => t.HistoryLinkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeisureFacilities", "StateDistrictId", "dbo.StateDistrict");
            DropForeignKey("dbo.LeisureFacilities", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.LeisureFacilities", new[] { "HistoryLinkId" });
            DropIndex("dbo.LeisureFacilities", new[] { "StateDistrictId" });
            DropIndex("dbo.LeisureFacilities", new[] { "EidSendStatus" });
            DropIndex("dbo.LeisureFacilities", new[] { "Eid" });
            DropTable("dbo.LeisureFacilities");
        }
    }
}
