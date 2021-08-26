namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransportAndPlaceMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bout", "VenueArrival", c => c.String());
            AddColumn("dbo.Bout", "VenueDeparture", c => c.String());
            AddColumn("dbo.Bout", "DateCollectionArrival", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Bout", "DateCollectionDeparture", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Bout", "TransportInfoFromId", c => c.Long());
            AddColumn("dbo.Bout", "TransportInfoToId", c => c.Long());
            AddColumn("dbo.AdministratorTour", "Phone", c => c.String(maxLength: 1000));
            AddColumn("dbo.AdministratorTour", "Email", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "Phone", c => c.String(maxLength: 1000));
            AddColumn("dbo.Counselors", "Email", c => c.String(maxLength: 1000));
            AddColumn("dbo.TransportInfo", "Venue", c => c.String());
            AddColumn("dbo.TransportInfo", "DateCollection", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Bout", "TransportInfoFromId");
            CreateIndex("dbo.Bout", "TransportInfoToId");
            AddForeignKey("dbo.Bout", "TransportInfoFromId", "dbo.TransportInfo", "Id");
            AddForeignKey("dbo.Bout", "TransportInfoToId", "dbo.TransportInfo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bout", "TransportInfoToId", "dbo.TransportInfo");
            DropForeignKey("dbo.Bout", "TransportInfoFromId", "dbo.TransportInfo");
            DropIndex("dbo.Bout", new[] { "TransportInfoToId" });
            DropIndex("dbo.Bout", new[] { "TransportInfoFromId" });
            DropColumn("dbo.TransportInfo", "DateCollection");
            DropColumn("dbo.TransportInfo", "Venue");
            DropColumn("dbo.Counselors", "Email");
            DropColumn("dbo.Counselors", "Phone");
            DropColumn("dbo.AdministratorTour", "Email");
            DropColumn("dbo.AdministratorTour", "Phone");
            DropColumn("dbo.Bout", "TransportInfoToId");
            DropColumn("dbo.Bout", "TransportInfoFromId");
            DropColumn("dbo.Bout", "DateCollectionDeparture");
            DropColumn("dbo.Bout", "DateCollectionArrival");
            DropColumn("dbo.Bout", "VenueDeparture");
            DropColumn("dbo.Bout", "VenueArrival");
        }
    }
}
