namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReleaseBookingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExchangeUTS", "TypeOfRestId", c => c.Long());
            AddColumn("dbo.ExchangeUTS", "BookingGuid", c => c.Guid());
            AddColumn("dbo.ExchangeUTS", "ToState", c => c.Long());
            AddColumn("dbo.ExchangeUTS", "IsErrorOnReleaseBooking", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExchangeUTS", "IsErrorOnReleaseBooking");
            DropColumn("dbo.ExchangeUTS", "ToState");
            DropColumn("dbo.ExchangeUTS", "BookingGuid");
            DropColumn("dbo.ExchangeUTS", "TypeOfRestId");
        }
    }
}
