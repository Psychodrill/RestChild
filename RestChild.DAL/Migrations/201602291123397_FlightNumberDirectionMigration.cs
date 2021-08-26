namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightNumberDirectionMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddonServicesLink", "TicketId", c => c.Long());
            AddColumn("dbo.Ticket", "FlightNumber", c => c.String(maxLength: 1000));
            AddColumn("dbo.TourTransport", "DirectoryFlightsId", c => c.Long());
            CreateIndex("dbo.AddonServicesLink", "TicketId");
            CreateIndex("dbo.TourTransport", "DirectoryFlightsId");
            AddForeignKey("dbo.AddonServicesLink", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.TourTransport", "DirectoryFlightsId", "dbo.DirectoryFlights", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourTransport", "DirectoryFlightsId", "dbo.DirectoryFlights");
            DropForeignKey("dbo.AddonServicesLink", "TicketId", "dbo.Ticket");
            DropIndex("dbo.TourTransport", new[] { "DirectoryFlightsId" });
            DropIndex("dbo.AddonServicesLink", new[] { "TicketId" });
            DropColumn("dbo.TourTransport", "DirectoryFlightsId");
            DropColumn("dbo.Ticket", "FlightNumber");
            DropColumn("dbo.AddonServicesLink", "TicketId");
        }
    }
}
