namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightNumberDirection2Migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestService", "DateFrom", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.RequestService", "DateTo", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.RequestService", "DirectoryFlightsId", c => c.Long());
            CreateIndex("dbo.RequestService", "DirectoryFlightsId");
            AddForeignKey("dbo.RequestService", "DirectoryFlightsId", "dbo.DirectoryFlights", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestService", "DirectoryFlightsId", "dbo.DirectoryFlights");
            DropIndex("dbo.RequestService", new[] { "DirectoryFlightsId" });
            DropColumn("dbo.RequestService", "DirectoryFlightsId");
            DropColumn("dbo.RequestService", "DateTo");
            DropColumn("dbo.RequestService", "DateFrom");
        }
    }
}
