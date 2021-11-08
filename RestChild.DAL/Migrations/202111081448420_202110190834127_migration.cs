namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202110190834127_migration : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.LeisureFacilities", "HistoryLinkId", "dbo.HistoryLink");
            //DropForeignKey("dbo.LeisureFacilities", "StateDistrictId", "dbo.StateDistrict");
            //DropIndex("dbo.LeisureFacilities", new[] { "Eid" });
            //DropIndex("dbo.LeisureFacilities", new[] { "EidSendStatus" });
            //DropIndex("dbo.LeisureFacilities", new[] { "StateDistrictId" });
            //DropIndex("dbo.LeisureFacilities", new[] { "HistoryLinkId" });
            DropTable("dbo.LeisureFacilities");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.LeisureFacilities", "HistoryLinkId");
            CreateIndex("dbo.LeisureFacilities", "StateDistrictId");
            CreateIndex("dbo.LeisureFacilities", "EidSendStatus");
            CreateIndex("dbo.LeisureFacilities", "Eid");
            AddForeignKey("dbo.LeisureFacilities", "StateDistrictId", "dbo.StateDistrict", "Id");
            AddForeignKey("dbo.LeisureFacilities", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
    }
}
