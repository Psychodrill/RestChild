namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoutAndTransportNewMemoMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransportInfo", "DateArrival", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TransportInfo", "Memo", c => c.String());
            AddColumn("dbo.TransportInfo", "MemoFile", c => c.String(maxLength: 1000));
            AddColumn("dbo.TransportInfo", "MemoLink", c => c.String(maxLength: 1000));
            DropColumn("dbo.Tour", "Memo");
            DropColumn("dbo.Tour", "MemoFile");
            DropColumn("dbo.Tour", "MemoLink");
            DropColumn("dbo.Bout", "VenueArrival");
            DropColumn("dbo.Bout", "VenueDeparture");
            DropColumn("dbo.Bout", "DateCollectionArrival");
            DropColumn("dbo.Bout", "DateCollectionDeparture");
            DropColumn("dbo.Bout", "Memo");
            DropColumn("dbo.Bout", "MemoFile");
            DropColumn("dbo.Bout", "MemoLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bout", "MemoLink", c => c.String(maxLength: 1000));
            AddColumn("dbo.Bout", "MemoFile", c => c.String(maxLength: 1000));
            AddColumn("dbo.Bout", "Memo", c => c.String());
            AddColumn("dbo.Bout", "DateCollectionDeparture", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Bout", "DateCollectionArrival", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Bout", "VenueDeparture", c => c.String());
            AddColumn("dbo.Bout", "VenueArrival", c => c.String());
            AddColumn("dbo.Tour", "MemoLink", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tour", "MemoFile", c => c.String(maxLength: 1000));
            AddColumn("dbo.Tour", "Memo", c => c.String());
            DropColumn("dbo.TransportInfo", "MemoLink");
            DropColumn("dbo.TransportInfo", "MemoFile");
            DropColumn("dbo.TransportInfo", "Memo");
            DropColumn("dbo.TransportInfo", "DateArrival");
        }
    }
}
