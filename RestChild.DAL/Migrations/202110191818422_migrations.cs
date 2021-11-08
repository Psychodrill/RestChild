namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeisureFacilities", "StateDistrictId", c => c.Long());
            AddColumn("dbo.LeisureFacilities", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.LeisureFacilities", "StateDistrictId");
            CreateIndex("dbo.LeisureFacilities", "HistoryLinkId");
            AddForeignKey("dbo.LeisureFacilities", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.LeisureFacilities", "StateDistrictId", "dbo.StateDistrict", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeisureFacilities", "StateDistrictId", "dbo.StateDistrict");
            DropForeignKey("dbo.LeisureFacilities", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.LeisureFacilities", new[] { "HistoryLinkId" });
            DropIndex("dbo.LeisureFacilities", new[] { "StateDistrictId" });
            DropColumn("dbo.LeisureFacilities", "HistoryLinkId");
            DropColumn("dbo.LeisureFacilities", "StateDistrictId");
        }
    }
}
