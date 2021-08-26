namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractAddMigrationTour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaceOfRest", "HistoryLinkId", c => c.Long());
            AddColumn("dbo.Tour", "StartBooking", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Tour", "EndBooking", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Tour", "HistoryLinkId", c => c.Long());
            CreateIndex("dbo.PlaceOfRest", "HistoryLinkId");
            CreateIndex("dbo.Tour", "HistoryLinkId");
            AddForeignKey("dbo.PlaceOfRest", "HistoryLinkId", "dbo.HistoryLink", "Id");
            AddForeignKey("dbo.Tour", "HistoryLinkId", "dbo.HistoryLink", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tour", "HistoryLinkId", "dbo.HistoryLink");
            DropForeignKey("dbo.PlaceOfRest", "HistoryLinkId", "dbo.HistoryLink");
            DropIndex("dbo.Tour", new[] { "HistoryLinkId" });
            DropIndex("dbo.PlaceOfRest", new[] { "HistoryLinkId" });
            DropColumn("dbo.Tour", "HistoryLinkId");
            DropColumn("dbo.Tour", "EndBooking");
            DropColumn("dbo.Tour", "StartBooking");
            DropColumn("dbo.PlaceOfRest", "HistoryLinkId");
        }
    }
}
