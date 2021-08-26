namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TourPriceAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tour", "TourPrice", c => c.Decimal(precision: 30, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tour", "TourPrice");
        }
    }
}
