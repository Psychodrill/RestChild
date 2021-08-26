namespace RestChild.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingNewFieldMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "BookingGuid", c => c.Guid());
            AddColumn("dbo.Booking", "TypeOfRestId", c => c.Long());
            CreateIndex("dbo.Booking", "TypeOfRestId");
            AddForeignKey("dbo.Booking", "TypeOfRestId", "dbo.TypeOfRest", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "TypeOfRestId", "dbo.TypeOfRest");
            DropIndex("dbo.Booking", new[] { "TypeOfRestId" });
            DropColumn("dbo.Booking", "TypeOfRestId");
            DropColumn("dbo.Request", "BookingGuid");
        }
    }
}
