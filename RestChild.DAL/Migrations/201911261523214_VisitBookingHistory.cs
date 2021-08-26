namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisitBookingHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MGTBookingVisit", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.MGTBookingVisit", "HistoryLinkId");
            AddForeignKey("dbo.MGTBookingVisit", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MGTBookingVisit", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.MGTBookingVisit", new[] { "HistoryLinkId" });
            DropColumn("dbo.MGTBookingVisit", "HistoryLinkId");
        }
    }
}
