namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentsNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "TourPriceAttendant", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tour", "TourPriceAttendant");
        }
    }
}
