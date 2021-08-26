namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task115918_2Migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MonitoringHotel", "PlaceOfRestId", "dbo.PlaceOfRest");
            DropIndex("dbo.MonitoringHotel", new[] { "PlaceOfRestId" });
            AddColumn("dbo.MonitoringHotel", "RegionId", c => c.Long());
            CreateIndex("dbo.MonitoringHotel", "RegionId");
            AddForeignKey("dbo.MonitoringHotel", "RegionId", "dbo.StateDistrict", "Id");
            DropColumn("dbo.MonitoringHotel", "PlaceOfRestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MonitoringHotel", "PlaceOfRestId", c => c.Long());
            DropForeignKey("dbo.MonitoringHotel", "RegionId", "dbo.StateDistrict");
            DropIndex("dbo.MonitoringHotel", new[] { "RegionId" });
            DropColumn("dbo.MonitoringHotel", "RegionId");
            CreateIndex("dbo.MonitoringHotel", "PlaceOfRestId");
            AddForeignKey("dbo.MonitoringHotel", "PlaceOfRestId", "dbo.PlaceOfRest", "Id");
        }
    }
}
