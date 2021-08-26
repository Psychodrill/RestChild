namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketDateDepartureMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ticket", "DateOfDeparture", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ticket", "DateOfDeparture", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
