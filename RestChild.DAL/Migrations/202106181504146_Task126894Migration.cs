namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task126894Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonitoringHotel", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.MonitoringHotel", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.MonitoringHotel", "HistoryLinkId");
            AddForeignKey("dbo.MonitoringHotel", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MonitoringHotel", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.MonitoringHotel", new[] { "HistoryLinkId" });
            DropColumn("dbo.MonitoringHotel", "HistoryLinkId");
            DropColumn("dbo.MonitoringHotel", "IsDeleted");
        }
    }
}
