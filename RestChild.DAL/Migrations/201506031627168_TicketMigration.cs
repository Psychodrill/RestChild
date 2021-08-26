namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateOfDeparture = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DirectoryFlightsId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DirectoryFlights", t => t.DirectoryFlightsId)
                .Index(t => t.DirectoryFlightsId);
            
            AddColumn("dbo.Applicant", "TicketId", c => c.Long());
            AddColumn("dbo.Child", "TicketId", c => c.Long());
            AddColumn("dbo.Calculation", "TicketId", c => c.Long());
            CreateIndex("dbo.Applicant", "TicketId");
            CreateIndex("dbo.Child", "TicketId");
            CreateIndex("dbo.Calculation", "TicketId");
            AddForeignKey("dbo.Child", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.Applicant", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.Calculation", "TicketId", "dbo.Ticket", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calculation", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Applicant", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Child", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.Ticket", "DirectoryFlightsId", "dbo.DirectoryFlights");
            DropIndex("dbo.Calculation", new[] { "TicketId" });
            DropIndex("dbo.Ticket", new[] { "DirectoryFlightsId" });
            DropIndex("dbo.Child", new[] { "TicketId" });
            DropIndex("dbo.Applicant", new[] { "TicketId" });
            DropColumn("dbo.Calculation", "TicketId");
            DropColumn("dbo.Child", "TicketId");
            DropColumn("dbo.Applicant", "TicketId");
            DropTable("dbo.Ticket");
        }
    }
}
